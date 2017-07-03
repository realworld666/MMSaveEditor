// Decompiled with JetBrains decompiler
// Type: SaveLoadDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SaveLoadDialog : UIDialogBox
{
  private SaveLoadDialog.Mode mMode = SaveLoadDialog.Mode.Load;
  [SerializeField]
  private UIGridList gridList;
  [SerializeField]
  private InputField inputField;
  [SerializeField]
  private Button actionButton;
  [SerializeField]
  private TextMeshProUGUI actionButtonLabel;
  [SerializeField]
  private Toggle localDirectoryToggle;
  [SerializeField]
  private Toggle cloudDirectoryToggle;
  [SerializeField]
  private GameObject headingLoadGame;
  [SerializeField]
  private GameObject headingSaveGame;
  [SerializeField]
  private UICharacterPortrait playerPortrait;
  [SerializeField]
  private GameObject workshopLogo;
  [SerializeField]
  private Flag playerNationality;
  [SerializeField]
  private TextMeshProUGUI playerNameLabel;
  [SerializeField]
  private TextMeshProUGUI playerJobTitle;
  [SerializeField]
  private TextMeshProUGUI gameTypeLabel;
  [SerializeField]
  private TextMeshProUGUI gameTimeLabel;
  [SerializeField]
  private TextMeshProUGUI championshipLabel;
  [SerializeField]
  private TextMeshProUGUI progressLabel;
  [SerializeField]
  private TextMeshProUGUI fileSizeLabel;
  private SaveFileInfo mSelectedSaveFile;

  public SaveLoadDialog.Mode mode
  {
    get
    {
      return this.mMode;
    }
    set
    {
      this.mMode = value;
    }
  }

  public static void ShowLoadDialogueOrAskForConfirmation()
  {
    SaveFileInfo savedOrLoadedFile = App.instance.saveSystem.mostRecentlyManuallySavedOrLoadedFile;
    if (Game.IsActive() && (savedOrLoadedFile == null || savedOrLoadedFile.gameInfo.gameTime != Game.instance.time.now) && Game.instance.isCareer && App.instance.saveSystem.NeedSaveConfirmation())
    {
      GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
      string inTitle = Localisation.LocaliseID("PSG_10010631", (GameObject) null);
      string inText = Localisation.LocaliseID("PSG_10010632", (GameObject) null);
      string inCancelString = Localisation.LocaliseID("PSG_10009077", (GameObject) null);
      string inConfirmString = Localisation.LocaliseID("PSG_10009078", (GameObject) null);
      scSoundManager.Instance.Pause(true, false, false);
      Action inCancelAction = (Action) (() => scSoundManager.Instance.UnPause(false));
      dialog.Show(inCancelAction, inCancelString, new Action(SaveLoadDialog.ShowLoad), inConfirmString, inText, inTitle);
    }
    else
    {
      scSoundManager.Instance.Pause(true, false, false);
      SaveLoadDialog.ShowLoad();
    }
  }

  public static void ShowLoad()
  {
    SaveLoadDialog dialog = UIManager.instance.dialogBoxManager.GetDialog<SaveLoadDialog>();
    dialog.mode = SaveLoadDialog.Mode.Load;
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
  }

  public static void ShowSave()
  {
    SaveLoadDialog dialog = UIManager.instance.dialogBoxManager.GetDialog<SaveLoadDialog>();
    dialog.mode = SaveLoadDialog.Mode.Save;
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
  }

  private void Start()
  {
    this.localDirectoryToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnLocalDirectory));
    this.cloudDirectoryToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnCloudDirectory));
  }

  protected override void OnEnable()
  {
    base.OnEnable();
    this.actionButton.onClick.RemoveAllListeners();
    switch (this.mode)
    {
      case SaveLoadDialog.Mode.Save:
        this.actionButtonLabel.text = Localisation.LocaliseID("PSG_10009322", (GameObject) null);
        this.actionButton.onClick.AddListener(new UnityAction(this.OnSaveButton));
        this.inputField.interactable = true;
        break;
      case SaveLoadDialog.Mode.Load:
        this.actionButtonLabel.text = Localisation.LocaliseID("PSG_10004579", (GameObject) null);
        this.actionButton.onClick.AddListener(new UnityAction(this.OnLoadButton));
        this.inputField.interactable = false;
        break;
    }
    GameUtility.SetActive(this.headingLoadGame, this.mMode == SaveLoadDialog.Mode.Load);
    GameUtility.SetActive(this.headingSaveGame, this.mMode == SaveLoadDialog.Mode.Save);
    this.localDirectoryToggle.isOn = false;
    this.cloudDirectoryToggle.isOn = true;
    this.Refresh();
    scSoundManager.Instance.Pause(true, false, false);
  }

  public void Refresh()
  {
    if (this.localDirectoryToggle.isOn)
      this.OnLocalDirectory(true);
    else
      this.OnCloudDirectory(true);
  }

  public override void Hide()
  {
    scSoundManager.Instance.UnPause(false);
    base.Hide();
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    this.gridList.DestroyListItems();
    App.instance.uiTeamColourManager.SetToPlayerTeamColour();
    if (!Game.IsActive())
      return;
    Game.instance.time.UnPause(GameTimer.PauseType.UI);
  }

  public void SetSelectedFile(SaveFileInfo inSaveFileInfo)
  {
    this.mSelectedSaveFile = inSaveFileInfo;
    if (this.mSelectedSaveFile == null || this.mSelectedSaveFile.isBroken || !this.mSelectedSaveFile.IsValidDLC())
    {
      this.playerPortrait.gameObject.SetActive(false);
      this.playerNationality.gameObject.SetActive(false);
      this.playerNameLabel.text = "-";
      this.playerJobTitle.text = "-";
      this.gameTypeLabel.text = "-";
      this.gameTimeLabel.text = "-";
      this.championshipLabel.text = "-";
      this.progressLabel.text = "-";
      this.fileSizeLabel.text = "-";
      this.inputField.text = this.mode != SaveLoadDialog.Mode.Save ? string.Empty : App.instance.saveSystem.nextSaveName;
      App.instance.uiTeamColourManager.SetToPlayerTeamColour();
      this.actionButton.interactable = false;
    }
    else
    {
      GameUtility.SetActive(this.playerPortrait.gameObject, !this.mSelectedSaveFile.IsWorkshopSave());
      GameUtility.SetActive(this.workshopLogo, this.mSelectedSaveFile.IsWorkshopSave());
      this.playerPortrait.SetPortrait(this.mSelectedSaveFile.gameInfo.playerPortrait, this.mSelectedSaveFile.gameInfo.playerGender, this.mSelectedSaveFile.gameInfo.playerAge, this.mSelectedSaveFile.gameInfo.teamLogoID, this.mSelectedSaveFile.gameInfo.teamColor, UICharacterPortraitBody.BodyType.Chairman, -1, -1);
      this.playerNationality.gameObject.SetActive(true);
      this.playerNationality.SetNationality(this.mSelectedSaveFile.gameInfo.playerNationality);
      this.playerNameLabel.text = this.mSelectedSaveFile.gameInfo.playerName;
      StringVariableParser.subject = new Person();
      StringVariableParser.subject.gender = this.mSelectedSaveFile.gameInfo.playerGender;
      if (this.mSelectedSaveFile.gameInfo.teamName != string.Empty)
        this.playerJobTitle.text = Localisation.LocaliseEnum((Enum) Contract.Job.TeamPrincipal);
      else
        this.playerJobTitle.text = Localisation.LocaliseEnum((Enum) Contract.Job.Unemployed);
      StringVariableParser.subject = (Person) null;
      this.gameTypeLabel.text = !this.mSelectedSaveFile.gameInfo.isChallenge ? Localisation.LocaliseID("PSG_10001429", (GameObject) null) : Localisation.LocaliseID("PSG_10010629", (GameObject) null);
      this.gameTimeLabel.text = GameUtility.FormatDateTimeToShortDateString(this.mSelectedSaveFile.gameInfo.gameTime) + " " + this.mSelectedSaveFile.gameInfo.gameTime.ToShortTimeString();
      this.championshipLabel.text = this.mSelectedSaveFile.gameInfo.championship;
      StringVariableParser.intValue1 = this.mSelectedSaveFile.gameInfo.seasonNumber;
      StringVariableParser.intValue2 = this.mSelectedSaveFile.gameInfo.raceNumber;
      StringVariableParser.intValue3 = this.mSelectedSaveFile.gameInfo.racesInSeason;
      this.progressLabel.text = Localisation.LocaliseID("PSG_10010630", (GameObject) null);
      this.fileSizeLabel.text = ((float) this.mSelectedSaveFile.fileInfo.Length / 1048576f).ToString("0.0", (IFormatProvider) Localisation.numberFormatter) + " MB";
      this.inputField.text = this.mSelectedSaveFile.GetDisplayName();
      App.instance.uiTeamColourManager.SetCurrentColour(this.mSelectedSaveFile.gameInfo.teamColor, true, this.gameObject);
      this.actionButton.interactable = true;
    }
    if (this.mode == SaveLoadDialog.Mode.Save)
      this.actionButton.interactable = true;
    for (int inIndex = 0; inIndex < this.gridList.itemCount; ++inIndex)
    {
      UISaveFileInfoEntry saveFileInfoEntry = this.gridList.GetItem<UISaveFileInfoEntry>(inIndex);
      saveFileInfoEntry.SetSelected(saveFileInfoEntry.saveFileInfo == this.mSelectedSaveFile);
    }
  }

  private void OnLocalDirectory(bool inValue)
  {
    if (!inValue)
      return;
    App.instance.saveSystem.SetDirectoryLocation(SaveSystem.DirectoryLocation.Local);
    this.RefreshList();
  }

  private void OnCloudDirectory(bool inValue)
  {
    if (!inValue)
      return;
    App.instance.saveSystem.SetDirectoryLocation(SaveSystem.DirectoryLocation.Cloud);
    this.RefreshList();
  }

  private void RefreshList()
  {
    this.gridList.DestroyListItems();
    int fileCount = App.instance.saveSystem.fileCount;
    for (int inIndex = 0; inIndex < fileCount; ++inIndex)
      this.gridList.CreateListItem<UISaveFileInfoEntry>().SetSaveFileInfo(App.instance.saveSystem.GetSaveFileInfo(inIndex));
    if (this.mode == SaveLoadDialog.Mode.Save)
      this.SetSelectedFile(App.instance.saveSystem.mostRecentlyManuallySavedOrLoadedFile);
    else
      this.SetSelectedFile(App.instance.saveSystem.mostRecentlyManuallySavedOrLoadedOrOtherwiseNewestFile);
  }

  private void OnLoadButton()
  {
    if (this.mSelectedSaveFile == null)
      return;
    Action onConfirmValidation = (Action) (() =>
    {
      scSoundManager.Instance.SessionEnd(false);
      scMusicController.Stop();
      this.Hide();
      if (App.instance.modManager.IsAssetLoadRequiredForActiveMods())
      {
        UIManager.instance.dialogBoxManager.GetDialog<WorkshopAssetLoading>().SetLoadingCompleteAction(WorkshopAssetLoading.LoadingCompleteAction.LoadSpecificSave, this.mSelectedSaveFile);
        UIManager.instance.dialogBoxManager.Show("WorkshopAssetLoading");
      }
      else
        App.instance.saveSystem.Load(this.mSelectedSaveFile, false);
    });
    if (!App.instance.modManager.modValidator.ValidateSubscribedModsOnContinueGame(this.mSelectedSaveFile, onConfirmValidation))
      return;
    onConfirmValidation();
  }

  private void OnSaveButton()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SaveLoadDialog.\u003COnSaveButton\u003Ec__AnonStorey63 buttonCAnonStorey63 = new SaveLoadDialog.\u003COnSaveButton\u003Ec__AnonStorey63();
    // ISSUE: reference to a compiler-generated field
    buttonCAnonStorey63.\u003C\u003Ef__this = this;
    // ISSUE: reference to a compiler-generated field
    buttonCAnonStorey63.saveName = this.inputField.text;
    // ISSUE: reference to a compiler-generated field
    bool flag = App.instance.saveSystem.SavingWithSaveNameWouldOverwriteExistingSave(buttonCAnonStorey63.saveName);
    // ISSUE: reference to a compiler-generated method
    Action inConfirmAction = new Action(buttonCAnonStorey63.\u003C\u003Em__97);
    if (flag)
    {
      this.Hide();
      GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
      Action inCancelAction = (Action) (() => SaveLoadDialog.ShowSave());
      scSoundManager.Instance.Pause(true, false, false);
      string inTitle = Localisation.LocaliseID("PSG_10009091", (GameObject) null);
      string inText = Localisation.LocaliseID("PSG_10009092", (GameObject) null);
      string inCancelString = Localisation.LocaliseID("PSG_10009077", (GameObject) null);
      string inConfirmString = Localisation.LocaliseID("PSG_10009078", (GameObject) null);
      dialog.Show(inCancelAction, inCancelString, inConfirmAction, inConfirmString, inText, inTitle);
    }
    else
      inConfirmAction();
  }

  public override void OnCancelButtonClicked()
  {
    base.OnCancelButtonClicked();
  }

  public enum Mode
  {
    Save,
    Load,
  }
}
