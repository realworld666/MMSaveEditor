// Decompiled with JetBrains decompiler
// Type: SaveSystem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using LZ4ps;
using MM2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;
using Wenzil.Console;

public class SaveSystem
{
  private static readonly string cachedPersistentDataPath = Application.persistentDataPath;
  private static readonly string saveFileSubdirectoryLocal = "Saves";
  private static readonly string saveFileSubdirectoryCloud = "Cloud" + (object) System.IO.Path.DirectorySeparatorChar + "Saves";
  public static readonly string saveLocationLocal = System.IO.Path.Combine(SaveSystem.cachedPersistentDataPath, SaveSystem.saveFileSubdirectoryLocal);
  public static readonly string saveLocationCloud = System.IO.Path.Combine(SaveSystem.cachedPersistentDataPath, SaveSystem.saveFileSubdirectoryCloud);
  private static readonly int saveFileVersion = 4;
  private SaveSystem.DirectoryLocation mSelectedDirectoryLocation = SaveSystem.DirectoryLocation.Cloud;
  private List<SaveFileInfo> mSaveFileList = new List<SaveFileInfo>();
  public const string fileExtension = "sav";
  private const string fileExtensionWithDot = ".sav";
  private const int saveSizeLimit = 268435456;
  private const int saveFileSignature = 1932684653;
  private const string lastSaveDurationPreferencesKey = "LastSaveDuration";
  private const string lastLoadDurationPreferencesKey = "LastLoadDuration";
  private const float defaultSaveDurationPrediction = 10f;
  private const float defaultLoadDurationPrediction = 25f;
  public Action OnSaveComplete;
  public Action OnLoadComplete;
  public Action OnGameReady;
  public static string saveLocationOverride;
  private fsSerializer serializer;
  private SaveSystem.Status mStatus;
  private Exception mErrorException;
  private SaveFileInfo mMostRecentSave;
  private SaveFileInfo mMostRecentlyManuallySavedOrLoadedFile;
  private Game loadedGame;
  private DateTime timeCurrentOperationStarted;
  private DateTime timeLastestOperationCompleted;
  private DateTime timeCurrentOperationPredictedToFinish;
  private bool shouldUnpauseOnSaveComplete;

  public float CurrentOperationProgress
  {
    get
    {
      return Mathf.Clamp01((float) ((DateTime.Now - this.timeCurrentOperationStarted).TotalSeconds / (this.timeCurrentOperationPredictedToFinish - this.timeCurrentOperationStarted).TotalSeconds));
    }
  }

  private float SaveDurationPrediction
  {
    get
    {
      return PlayerPrefs.GetFloat("LastSaveDuration", 10f);
    }
  }

  private float LoadDurationPrediction
  {
    get
    {
      return PlayerPrefs.GetFloat("LastLoadDuration", 25f);
    }
  }

  public string nextSaveName
  {
    get
    {
      if (this.mMostRecentlyManuallySavedOrLoadedFile != null)
        return this.mMostRecentlyManuallySavedOrLoadedFile.GetDisplayName();
      return this.defaultSaveName;
    }
  }

  public string defaultSaveName
  {
    get
    {
      if (!Game.IsActive())
      {
        Debug.LogWarning((object) "Trying to generate a default save name whilst a game isn't active - this is unexpected!", (UnityEngine.Object) null);
        return "Autosave";
      }
      string str = Game.instance.player.name + " - " + Game.instance.player.team.name;
      string name = str;
      int num;
      for (num = 1; this.SavingWithSaveNameWouldOverwriteExistingSave(name) && num <= 10; name = str + " " + (object) num)
        ++num;
      if (num >= 10)
        return str;
      return name;
    }
  }

  public SaveFileInfo mostRecentSave
  {
    get
    {
      return this.mMostRecentSave;
    }
  }

  public SaveFileInfo mostRecentlyManuallySavedOrLoadedFile
  {
    get
    {
      return this.mMostRecentlyManuallySavedOrLoadedFile;
    }
  }

  public SaveFileInfo mostRecentlyManuallySavedOrLoadedOrOtherwiseNewestFile
  {
    get
    {
      return this.mMostRecentlyManuallySavedOrLoadedFile ?? this.mMostRecentSave;
    }
  }

  public int fileCount
  {
    get
    {
      return this.mSaveFileList.Count;
    }
  }

  public SaveSystem.Status status
  {
    get
    {
      return this.mStatus;
    }
  }

  public Exception errorException
  {
    get
    {
      return this.mErrorException;
    }
  }

  static SaveSystem()
  {
    ConsoleCommandsDatabase.RegisterCommand("ForceSave", new ConsoleCommandCallback(SaveSystem.ForceSave), "Force a save", string.Empty);
  }

  public SaveSystem()
  {
    SaveSystem.EnsureDirectoriesExist();
    this.serializer = SaveSystem.CreateAndConfigureSerializer();
  }

  private static void EnsureDirectoriesExist()
  {
    if (Directory.Exists(SaveSystem.saveLocationCloud))
      return;
    Directory.CreateDirectory(SaveSystem.saveLocationCloud);
  }

  private static fsSerializer CreateAndConfigureSerializer()
  {
    return new fsSerializer() { Config = { DefaultMemberSerialization = fsMemberSerialization.OptOut, SerializeAttributes = new System.Type[1]{ typeof (fsPropertyAttribute) }, IgnoreSerializeAttributes = new System.Type[2]{ typeof (NonSerializedAttribute), typeof (fsIgnoreAttribute) }, SerializeEnumsAsInteger = true } };
  }

  public void OnStartingNewGame()
  {
    this.mMostRecentlyManuallySavedOrLoadedFile = (SaveFileInfo) null;
  }

  public void SetDirectoryLocation(SaveSystem.DirectoryLocation inDirectoryLocation)
  {
    if (this.mStatus != SaveSystem.Status.InActive)
      return;
    this.mSelectedDirectoryLocation = inDirectoryLocation;
    this.Refresh();
  }

  public void Update()
  {
    if (this.mStatus == SaveSystem.Status.SaveComplete || this.mStatus == SaveSystem.Status.SaveFailed)
    {
      bool flag = this.mStatus == SaveSystem.Status.SaveFailed;
      GC.Collect();
      this.mStatus = SaveSystem.Status.InActive;
      if (this.shouldUnpauseOnSaveComplete)
        Game.instance.time.UnPause(GameTimer.PauseType.Game);
      if (Game.IsActive())
        scSoundManager.Instance.UnPause(false);
      if (this.OnSaveComplete != null)
      {
        this.OnSaveComplete();
        this.OnSaveComplete = (Action) null;
      }
      if (flag)
      {
        Debug.LogError((object) "Saving game failed", (UnityEngine.Object) null);
        if (this.mErrorException != null)
          Debug.LogException(this.mErrorException);
        GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
        string inTitle = Localisation.LocaliseID("PSG_10009079", (GameObject) null);
        string inText = Localisation.LocaliseID("PSG_10010998", (GameObject) null);
        string inCancelString = Localisation.LocaliseID("PSG_10009081", (GameObject) null);
        dialog.Show((Action) null, inCancelString, (Action) null, (string) null, inText, inTitle);
      }
      else
      {
        float totalSeconds = (float) (DateTime.Now - this.timeCurrentOperationStarted).TotalSeconds;
        Debug.LogFormat("Save completed in {0} seconds", (object) totalSeconds);
        this.UpdateSaveDurationPrediction(totalSeconds);
        this.timeLastestOperationCompleted = DateTime.Now;
      }
    }
    if (this.mStatus != SaveSystem.Status.LoadComplete && this.mStatus != SaveSystem.Status.LoadFailed)
      return;
    bool flag1 = this.mStatus == SaveSystem.Status.LoadFailed;
    this.mStatus = SaveSystem.Status.InActive;
    if (!flag1)
    {
      if (Game.instance != null)
      {
        App.instance.gameStateManager.SetState(GameState.Type.Null, GameStateManager.StateChangeType.ChangeInstantly, false);
        UIManager.instance.LeaveScreenEarlyReadyForNextScreen();
        Game.instance.Destroy();
        Game.instance = (Game) null;
        GC.Collect();
      }
      GameUtility.Assert(this.loadedGame != null, "loadedGame != null", (UnityEngine.Object) null);
      Game.instance = this.loadedGame;
      this.loadedGame = (Game) null;
      if (this.OnGameReady != null)
      {
        this.OnGameReady();
        this.OnGameReady = (Action) null;
      }
      Game.instance.OnLoad();
      float totalSeconds = (float) (DateTime.Now - this.timeCurrentOperationStarted).TotalSeconds;
      this.timeLastestOperationCompleted = DateTime.Now;
      Debug.LogFormat("Load completed in {0} seconds", (object) totalSeconds);
      this.UpdateLoadDurationPrediction(totalSeconds);
    }
    if (this.OnLoadComplete != null)
    {
      this.OnLoadComplete();
      this.OnLoadComplete = (Action) null;
    }
    if (!flag1)
      return;
    Debug.LogError((object) "Loading game failed", (UnityEngine.Object) null);
    GenericConfirmation dialog1 = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
    string inTitle1 = Localisation.LocaliseID("PSG_10010995", (GameObject) null);
    string inText1 = Localisation.LocaliseID("PSG_10010999", (GameObject) null);
    string inCancelString1 = Localisation.LocaliseID("PSG_10009081", (GameObject) null);
    dialog1.Show((Action) null, inCancelString1, (Action) null, (string) null, inText1, inTitle1);
  }

  private void UpdateSaveDurationPrediction(float lastSaveDuration)
  {
    PlayerPrefs.SetFloat("LastSaveDuration", lastSaveDuration);
    PlayerPrefs.Save();
  }

  private void UpdateLoadDurationPrediction(float lastLoadDuration)
  {
    PlayerPrefs.SetFloat("LastLoadDuration", lastLoadDuration);
    PlayerPrefs.Save();
  }

  public void Refresh()
  {
    if (this.mStatus != SaveSystem.Status.InActive)
      return;
    this.InnerRefresh();
  }

  private void InnerRefresh()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SaveSystem.\u003CInnerRefresh\u003Ec__AnonStorey5E refreshCAnonStorey5E = new SaveSystem.\u003CInnerRefresh\u003Ec__AnonStorey5E();
    this.mSaveFileList = new List<SaveFileInfo>();
    this.mMostRecentSave = (SaveFileInfo) null;
    string[] files = Directory.GetFiles(this.GetDirectoryPath());
    for (int index = 0; index < files.Length; ++index)
    {
      if (files[index].EndsWith(".sav"))
      {
        FileInfo fileInfo = new FileInfo(files[index]);
        try
        {
          using (FileStream fileStream = File.Open(files[index], FileMode.Open, FileAccess.Read, FileShare.Read))
          {
            using (BinaryReader binaryReader = new BinaryReader((Stream) fileStream))
            {
              if (binaryReader.ReadInt32() != 1932684653)
                throw new SaveException("Save file is not a valid save file for this game");
              int num1 = binaryReader.ReadInt32();
              if (num1 < SaveSystem.saveFileVersion)
                throw new SaveException("Save file is an old format, and no upgrade path exists - must be from an old unsupported development version");
              if (num1 > SaveSystem.saveFileVersion)
                throw new SaveException("Save file version is newer than the game version! It's either corrupt, or the game executable is out of date");
              int num2 = binaryReader.ReadInt32();
              int outputLength = binaryReader.ReadInt32();
              binaryReader.ReadInt32();
              binaryReader.ReadInt32();
              if (outputLength > 268435456)
                throw new SaveException("Save file header size is apparently way too big - file has either been tampered with or become corrupt");
              fsData data;
              fsResult fsResult1 = fsJsonParser.Parse(Encoding.UTF8.GetString(LZ4Codec.Decode32(binaryReader.ReadBytes(num2), 0, num2, outputLength)), out data);
              if (fsResult1.Failed)
                Debug.LogErrorFormat("Error reported whilst parsing serialized SaveFileInfo string: {0}", (object) fsResult1.FormattedMessages);
              SaveFileInfo instance = (SaveFileInfo) null;
              fsResult fsResult2 = this.serializer.TryDeserialize<SaveFileInfo>(data, ref instance);
              if (fsResult2.Failed)
                Debug.LogErrorFormat("Error reported whilst deserializing SaveFileInfo: {0}", (object) fsResult2.FormattedMessages);
              instance.fileInfo = fileInfo;
              this.mSaveFileList.Add(instance);
              if (instance.IsValidDLC())
              {
                if (!instance.isBroken)
                {
                  if (this.mMostRecentSave != null)
                  {
                    if (!(fileInfo.LastWriteTimeUtc > this.mMostRecentSave.fileInfo.LastWriteTimeUtc))
                      continue;
                  }
                  this.mMostRecentSave = instance;
                }
              }
            }
          }
        }
        catch (Exception ex)
        {
          this.mSaveFileList.Add(new SaveFileInfo()
          {
            fileInfo = fileInfo,
            isBroken = true
          });
        }
      }
    }
    // ISSUE: reference to a compiler-generated field
    refreshCAnonStorey5E.datesByFilenameNoAutoSave = new Dictionary<string, DateTime>();
    using (List<SaveFileInfo>.Enumerator enumerator = this.mSaveFileList.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        SaveFileInfo current = enumerator.Current;
        if (!current.isBroken)
        {
          string filenameNoAutoSave = current.FilenameNoAutoSave;
          // ISSUE: reference to a compiler-generated field
          if (!refreshCAnonStorey5E.datesByFilenameNoAutoSave.ContainsKey(filenameNoAutoSave))
          {
            // ISSUE: reference to a compiler-generated field
            refreshCAnonStorey5E.datesByFilenameNoAutoSave.Add(filenameNoAutoSave, current.saveInfo.date);
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (current.saveInfo.date > refreshCAnonStorey5E.datesByFilenameNoAutoSave[filenameNoAutoSave])
            {
              // ISSUE: reference to a compiler-generated field
              refreshCAnonStorey5E.datesByFilenameNoAutoSave[filenameNoAutoSave] = current.saveInfo.date;
            }
          }
        }
      }
    }
    // ISSUE: reference to a compiler-generated method
    this.mSaveFileList.Sort(new Comparison<SaveFileInfo>(refreshCAnonStorey5E.\u003C\u003Em__83));
  }

  public void AutoSave()
  {
    if (!App.instance.preferencesManager.GetSettingBool(Preference.pName.Game_Autosave, false))
      return;
    App.instance.StartCoroutine(this.AutoSaveNextFrame());
  }

  public void TryDispatchDelayedAutoSave()
  {
    if (!Game.IsActive() || !Game.instance.queuedAutosave)
      return;
    Game.instance.queuedAutosave = false;
    this.AutoSave();
  }

  [DebuggerHidden]
  public IEnumerator AutoSaveNextFrame()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new SaveSystem.\u003CAutoSaveNextFrame\u003Ec__Iterator7() { \u003C\u003Ef__this = this };
  }

  public void ManualSave()
  {
    this.SaveImplementation(this.nextSaveName, false, false);
  }

  public void ManualSaveAs(string saveName)
  {
    this.SaveImplementation(saveName, false, true);
  }

  private void SaveImplementation(string saveName, bool isAutoSave, bool forceOverwrite)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SaveSystem.\u003CSaveImplementation\u003Ec__AnonStorey5F implementationCAnonStorey5F = new SaveSystem.\u003CSaveImplementation\u003Ec__AnonStorey5F();
    // ISSUE: reference to a compiler-generated field
    implementationCAnonStorey5F.saveName = saveName;
    // ISSUE: reference to a compiler-generated field
    implementationCAnonStorey5F.isAutoSave = isAutoSave;
    // ISSUE: reference to a compiler-generated field
    implementationCAnonStorey5F.forceOverwrite = forceOverwrite;
    // ISSUE: reference to a compiler-generated field
    implementationCAnonStorey5F.\u003C\u003Ef__this = this;
    if (this.mStatus != SaveSystem.Status.InActive)
      return;
    if (Game.IsActive() && Game.instance.isCareer)
    {
      StringVariableParser.ResetAllStaticReferences();
      this.timeCurrentOperationStarted = DateTime.Now;
      this.timeCurrentOperationPredictedToFinish = this.timeCurrentOperationStarted + TimeSpan.FromSeconds((double) this.SaveDurationPrediction);
      this.mStatus = SaveSystem.Status.Saving;
      if (!Game.instance.time.isPaused)
      {
        this.shouldUnpauseOnSaveComplete = true;
        Game.instance.time.Pause(GameTimer.PauseType.Game);
      }
      else
        this.shouldUnpauseOnSaveComplete = false;
      scSoundManager.Instance.Pause(true, false, false);
      UIManager.instance.dialogBoxManager.Show("SaveLoadStatus");
      Game.instance.OnSave();
      // ISSUE: reference to a compiler-generated method
      new Thread(new ThreadStart(implementationCAnonStorey5F.\u003C\u003Em__84)).Start();
    }
    else
    {
      if (this.OnSaveComplete == null)
        return;
      this.OnSaveComplete();
      this.OnSaveComplete = (Action) null;
    }
  }

  private void SaveData(string inSaveName, bool isAutoSave, bool forceOverwrite)
  {
    this.serializer = SaveSystem.CreateAndConfigureSerializer();
    if (inSaveName == string.Empty)
      inSaveName = this.defaultSaveName;
    string str = SaveSystem.SanitiseFileNameCharacters(inSaveName);
    try
    {
      fsData data1;
      fsResult fsResult1 = this.serializer.TrySerialize<SaveFileInfo>(SaveFileInfo.Create(inSaveName, isAutoSave), out data1);
      if (fsResult1.Failed)
        Debug.LogErrorFormat("Failed to serialise SaveFileInfo: {0}", (object) fsResult1.FormattedMessages);
      string s1 = fsJsonPrinter.CompressedJson(data1);
      fsData data2;
      fsResult fsResult2 = this.serializer.TrySerialize<Game>(Game.instance, out data2);
      if (fsResult2.Failed)
        Debug.LogErrorFormat("Failed to serialise Game: {0}", (object) fsResult2.FormattedMessages);
      string s2 = fsJsonPrinter.CompressedJson(data2);
      byte[] bytes1 = Encoding.UTF8.GetBytes(s1);
      byte[] bytes2 = Encoding.UTF8.GetBytes(s2);
      GameUtility.Assert(bytes1.Length < 268435456 && bytes2.Length < 268435456, "Uh-oh. Ben has underestimated how large save files might get, and we're about to save a file so large it will be detected as corrupt when loading. Best increase the limit!", (UnityEngine.Object) null);
      byte[] buffer1 = LZ4Codec.Encode32(bytes1, 0, bytes1.Length);
      byte[] buffer2 = LZ4Codec.Encode32(bytes2, 0, bytes2.Length);
      string nextAutoSaveName = this.GetNextAutoSaveName(inSaveName);
      string pathFromSaveName = this.GetFullPathFromSaveName(nextAutoSaveName);
      bool flag = isAutoSave || App.instance.preferencesManager.GetSettingBool(Preference.pName.Game_RollManualSaves, false) && !forceOverwrite;
      if (flag && !FileUtils.TryDeleteFileIfExists(pathFromSaveName))
        Debug.LogFormat("Couldn't delete oldest rolling save file whilst rotating saves. The rest of the save may not work. Failed deleting {0}.", (object) pathFromSaveName);
      string inSaveName1 = !flag ? str : nextAutoSaveName;
      FileInfo fileInfo = new FileInfo(!flag ? this.GetFullPathFromSaveName(inSaveName1) : pathFromSaveName);
      using (FileStream fileStream = File.Create(fileInfo.FullName))
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) fileStream))
        {
          binaryWriter.Write(1932684653);
          binaryWriter.Write(SaveSystem.saveFileVersion);
          binaryWriter.Write(buffer1.Length);
          binaryWriter.Write(bytes1.Length);
          binaryWriter.Write(buffer2.Length);
          binaryWriter.Write(bytes2.Length);
          binaryWriter.Write(buffer1);
          binaryWriter.Write(buffer2);
        }
      }
      this.InnerRefresh();
      if (!isAutoSave)
        this.mMostRecentlyManuallySavedOrLoadedFile = this.GetSaveFileInfoWithFileInfo(fileInfo);
      this.mStatus = SaveSystem.Status.SaveComplete;
    }
    catch (Exception ex)
    {
      this.mStatus = SaveSystem.Status.SaveFailed;
      this.mErrorException = ex;
    }
  }

  private SaveFileInfo GetSaveFileInfoWithFileInfo(FileInfo fileInfo)
  {
    for (int index = 0; index < this.mSaveFileList.Count; ++index)
    {
      if (this.mSaveFileList[index].fileInfo.FullName == fileInfo.FullName)
        return this.mSaveFileList[index];
    }
    return (SaveFileInfo) null;
  }

  public void LoadMostRecentFile()
  {
    if (this.mStatus != SaveSystem.Status.InActive)
      return;
    if (this.mMostRecentSave != null)
    {
      this.Load(this.mMostRecentSave, true);
    }
    else
    {
      if (this.OnLoadComplete == null)
        return;
      this.OnLoadComplete();
      this.OnLoadComplete = (Action) null;
    }
  }

  public void Load(SaveFileInfo inSaveFileInfo, bool isContinuingGame = false)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SaveSystem.\u003CLoad\u003Ec__AnonStorey60 loadCAnonStorey60 = new SaveSystem.\u003CLoad\u003Ec__AnonStorey60();
    // ISSUE: reference to a compiler-generated field
    loadCAnonStorey60.inSaveFileInfo = inSaveFileInfo;
    // ISSUE: reference to a compiler-generated field
    loadCAnonStorey60.isContinuingGame = isContinuingGame;
    // ISSUE: reference to a compiler-generated field
    loadCAnonStorey60.\u003C\u003Ef__this = this;
    // ISSUE: reference to a compiler-generated field
    if (this.mStatus != SaveSystem.Status.InActive || !loadCAnonStorey60.inSaveFileInfo.fileInfo.Exists)
      return;
    this.timeCurrentOperationStarted = DateTime.Now;
    this.timeCurrentOperationPredictedToFinish = this.timeCurrentOperationStarted + TimeSpan.FromSeconds((double) this.LoadDurationPrediction);
    this.mStatus = SaveSystem.Status.Loading;
    UIManager.instance.dialogBoxManager.Show("SaveLoadStatus");
    // ISSUE: reference to a compiler-generated method
    new Thread(new ThreadStart(loadCAnonStorey60.\u003C\u003Em__85)).Start();
  }

  public void LoadSaveWithName(string inSaveName)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: reference to a compiler-generated method
    SaveFileInfo inSaveFileInfo = this.mSaveFileList.Find(new Predicate<SaveFileInfo>(new SaveSystem.\u003CLoadSaveWithName\u003Ec__AnonStorey61() { inSaveName = inSaveName }.\u003C\u003Em__86));
    if (inSaveFileInfo == null)
      return;
    this.Load(inSaveFileInfo, false);
  }

  public void LoadDataSync(SaveFileInfo saveFile, ref Game targetGame)
  {
    this.LoadData(saveFile, false, ref targetGame);
  }

  private void LoadData(SaveFileInfo saveFile, bool isContinuingGame, ref Game targetGame)
  {
    this.serializer = SaveSystem.CreateAndConfigureSerializer();
    GameUtility.Assert(!saveFile.isBroken, "Trying to load a broken save; UI shouldn't allow this to happen", (UnityEngine.Object) null);
    if (targetGame != null)
    {
      Debug.LogError((object) "Loading a game while a loaded game is waiting to be made current - looks like two loads have happened at the same time!", (UnityEngine.Object) null);
      targetGame = (Game) null;
    }
    try
    {
      this.mMostRecentlyManuallySavedOrLoadedFile = saveFile;
      using (FileStream fileStream = File.Open(saveFile.fileInfo.FullName, FileMode.Open))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) fileStream))
        {
          if (binaryReader.ReadInt32() != 1932684653)
            throw new SaveException("Save file is not a valid save file for this game");
          int num1 = binaryReader.ReadInt32();
          if (num1 < SaveSystem.saveFileVersion)
            throw new SaveException("Save file is an old format, and no upgrade path exists - must be from an old unsupported development version");
          if (num1 > SaveSystem.saveFileVersion)
            throw new SaveException("Save file version is newer than the game version! It's either corrupt, or the game executable is out of date");
          int count = binaryReader.ReadInt32();
          binaryReader.ReadInt32();
          int num2 = binaryReader.ReadInt32();
          int outputLength = binaryReader.ReadInt32();
          if (outputLength > 268435456)
            throw new SaveException("Save file game data size is apparently way too big - file has either been tampered with or become corrupt");
          binaryReader.ReadBytes(count);
          fsData data;
          fsResult fsResult1 = fsJsonParser.Parse(Encoding.UTF8.GetString(LZ4Codec.Decode32(binaryReader.ReadBytes(num2), 0, num2, outputLength)), out data);
          if (fsResult1.Failed)
            Debug.LogErrorFormat("Error reported whilst parsing serialized Game data string: {0}", (object) fsResult1.FormattedMessages);
          fsResult fsResult2 = this.serializer.TryDeserialize<Game>(data, ref targetGame);
          if (fsResult2.Failed)
            Debug.LogErrorFormat("Error reported whilst deserializing Game data: {0}", (object) fsResult2.FormattedMessages);
          foreach (object rawMessage in fsResult2.RawMessages)
            Debug.LogWarning(rawMessage, (UnityEngine.Object) null);
        }
      }
      if (isContinuingGame && saveFile.saveInfo.isAutoSave)
      {
        SaveFileInfo infoWithFileInfo = this.GetSaveFileInfoWithFileInfo(new FileInfo(saveFile.GetPathOfMainSaveIfExists()));
        if (infoWithFileInfo != null)
          this.mMostRecentlyManuallySavedOrLoadedFile = infoWithFileInfo;
      }
      App.instance.teamColorManager.SetPlayersColourFromSave(saveFile.gameInfo.teamColor);
      this.mStatus = SaveSystem.Status.LoadComplete;
    }
    catch (Exception ex)
    {
      Debug.LogException(ex);
      this.mErrorException = ex;
      this.OnGameReady = (Action) null;
      this.mStatus = SaveSystem.Status.LoadFailed;
    }
  }

  public bool NeedSaveConfirmation()
  {
    return this.GetSecondsSinceLastSaveLoadAction() > 30;
  }

  private int GetSecondsSinceLastSaveLoadAction()
  {
    return (DateTime.Now - this.timeLastestOperationCompleted).Seconds;
  }

  public void Delete(SaveFileInfo inSaveFileInfo)
  {
    if (!File.Exists(inSaveFileInfo.fileInfo.FullName))
      return;
    try
    {
      File.Delete(inSaveFileInfo.fileInfo.FullName);
      this.Refresh();
    }
    catch (IOException ex)
    {
    }
  }

  public void Debug_DeleteAll()
  {
    this.Debug_DeleteAll(SaveSystem.DirectoryLocation.Cloud);
    this.Debug_DeleteAll(SaveSystem.DirectoryLocation.Local);
  }

  private void Debug_DeleteAll(SaveSystem.DirectoryLocation location)
  {
    string[] files = Directory.GetFiles(this.GetDirectoryPath(location));
    if (files.Length <= 0)
      return;
    for (int index = 0; index < files.Length; ++index)
    {
      if (files[index].EndsWith(".sav") && File.Exists(files[index]))
        File.Delete(files[index]);
    }
  }

  private string GetDirectoryPath()
  {
    return this.GetDirectoryPath(this.mSelectedDirectoryLocation);
  }

  private string GetDirectoryPath(SaveSystem.DirectoryLocation location)
  {
    if (SaveSystem.saveLocationOverride != null)
      return SaveSystem.saveLocationOverride;
    if (location == SaveSystem.DirectoryLocation.Cloud)
      return SaveSystem.saveLocationCloud;
    return SaveSystem.saveLocationLocal;
  }

  private string GetNextAutoSaveName(string inDisplayName)
  {
    string saveName = SaveFileInfo.DisplayNameToSaveName(inDisplayName);
    string str = SaveFileInfo.TrimAutoSaves(inDisplayName);
    int settingInt = App.instance.preferencesManager.GetSettingInt(Preference.pName.Game_NumberRollingAutosaves, false);
    Debug.Assert(settingInt >= 0);
    Debug.Assert(settingInt <= 5);
    bool[] flagArray = new bool[6];
    int index1 = 0;
    int num1 = -1;
    int num2 = -1;
    Debug.Assert(this.mSaveFileList != null, "mSaveFileList != null");
    for (; index1 < this.mSaveFileList.Count; ++index1)
    {
      SaveFileInfo mSaveFile = this.mSaveFileList[index1];
      if (!mSaveFile.isBroken)
      {
        if (mSaveFile.FilenameNoAutoSave == saveName)
        {
          if (mSaveFile.AutoSaveVersion < flagArray.Length)
            flagArray[mSaveFile.AutoSaveVersion] = true;
          if (num1 == -1)
            num1 = index1;
        }
        else if (num1 != -1)
          break;
      }
    }
    if (!flagArray[0])
      return str;
    if (num1 != -1)
      num2 = index1 - num1;
    int index2 = num1 + settingInt - 1;
    if (index2 < num1 + num2 - 1)
    {
      int num3 = this.mSaveFileList[index2].AutoSaveVersion;
      if (num3 == 0)
        num3 = settingInt <= 1 ? this.mSaveFileList[index2 + 1].AutoSaveVersion : this.mSaveFileList[index2 - 1].AutoSaveVersion;
      return string.Format("{0} ({1})", (object) str, (object) num3);
    }
    for (int index3 = 1; index3 < flagArray.Length; ++index3)
    {
      if (!flagArray[index3])
        return string.Format("{0} ({1})", (object) str, (object) index3);
    }
    return string.Format("{0} ({1})", (object) str, (object) 0);
  }

  private string GetFullPathFromSaveName(string inSaveName)
  {
    string str = "Save" + SaveSystem.SanitiseFileNameCharacters(inSaveName);
    string directoryPath = this.GetDirectoryPath();
    int num = 260 - (directoryPath.Length + 1 + str.Length + ".sav".Length + 1);
    if (num < 0)
    {
      if (-num > str.Length)
        throw new PathTooLongException("Path to save game is too long, even for blank save file name! Save directory must be a very long path");
      str = str.Substring(0, str.Length + num);
    }
    return System.IO.Path.Combine(directoryPath, str + ".sav");
  }

  public static string SanitiseFileNameCharacters(string s)
  {
    string str = s;
    foreach (char invalidFileNameChar in System.IO.Path.GetInvalidFileNameChars())
      str = str.Replace(invalidFileNameChar, '_');
    foreach (char invalidPathChar in System.IO.Path.GetInvalidPathChars())
      str = str.Replace(invalidPathChar, '_');
    return str;
  }

  public SaveFileInfo GetSaveFileInfo(int inIndex)
  {
    return this.mSaveFileList[inIndex];
  }

  public bool SavingWithSaveNameWouldOverwriteExistingSave(string name)
  {
    return File.Exists(this.GetFullPathFromSaveName(name));
  }

  private static ConsoleCommandResult ForceSave(params string[] inStrings)
  {
    if (!((UnityEngine.Object) App.instance != (UnityEngine.Object) null) || App.instance.saveSystem == null)
      return ConsoleCommandResult.Failed("Couldn't find saveSystem");
    App.instance.saveSystem.SaveData(App.instance.saveSystem.nextSaveName, false, true);
    if (App.instance.saveSystem.mStatus == SaveSystem.Status.SaveFailed)
      return ConsoleCommandResult.Failed("Save Failed");
    return ConsoleCommandResult.Succeeded((string) null);
  }

  public enum DirectoryLocation
  {
    Local,
    Cloud,
  }

  public enum Status
  {
    InActive,
    Loading,
    LoadComplete,
    LoadFailed,
    Saving,
    SaveComplete,
    SaveFailed,
  }
}
