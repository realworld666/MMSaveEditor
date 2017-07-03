// Decompiled with JetBrains decompiler
// Type: ModdingSystem.ModManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using Steamworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace ModdingSystem
{
  public class ModManager
  {
    private string mActiveModsSaveFileName = "ActiveMods.txt";
    private List<SteamMod> mAllUserSubscribedMods = new List<SteamMod>();
    private List<SteamMod> mUserPublishedMods = new List<SteamMod>();
    private List<SteamMod> mAllWorkshopItems = new List<SteamMod>();
    private List<SteamMod> mSubscribedWorkshopItems = new List<SteamMod>();
    public Action<bool> OnOverlayScreenActivatedAction;
    private Callback<GameOverlayActivated_t> mGameOverlayActivated;
    public SteamModFetcher modFetcher;
    private SteamModPublisher mModPublisher;
    private SteamModSubscriber mModSubscriber;
    private SteamModVoter mModVoter;
    private SteamModValidator mModValidator;
    private ModLoader mModLoader;

    public SteamModPublisher modPublisher
    {
      get
      {
        return this.mModPublisher;
      }
    }

    public SteamModSubscriber modSubscriber
    {
      get
      {
        return this.mModSubscriber;
      }
    }

    public List<SteamMod> allUserSubscribedMods
    {
      get
      {
        return this.mAllUserSubscribedMods;
      }
    }

    public List<SteamMod> userPublishedMods
    {
      get
      {
        return this.mUserPublishedMods;
      }
    }

    public List<SteamMod> allWorkshopItems
    {
      get
      {
        return this.mAllWorkshopItems;
      }
    }

    public List<SteamMod> subscribedWorkshopItems
    {
      get
      {
        return this.mSubscribedWorkshopItems;
      }
    }

    public ModLoader modLoader
    {
      get
      {
        return this.mModLoader;
      }
    }

    public SteamModVoter modVoter
    {
      get
      {
        return this.mModVoter;
      }
    }

    public SteamModValidator modValidator
    {
      get
      {
        return this.mModValidator;
      }
    }

    public void Start()
    {
      this.modFetcher = new SteamModFetcher();
      this.modFetcher.Initialise();
      this.mModPublisher = new SteamModPublisher();
      this.mModPublisher.Initialise();
      this.mModSubscriber = new SteamModSubscriber(this.modFetcher);
      this.mModValidator = new SteamModValidator(this);
      this.mModVoter = new SteamModVoter();
      this.mModLoader = new ModLoader();
    }

    public void LoadSubscribedMods()
    {
      if (!SteamManager.Initialized)
        return;
      this.mGameOverlayActivated = Callback<GameOverlayActivated_t>.Create(new Callback<GameOverlayActivated_t>.DispatchDelegate(this.OnGameOverlayActivated));
      this.modFetcher.OnFetchSubscribedSuccess += new Action<List<SteamMod>, uint>(this.LoadActiveModsSavedFile);
      this.modFetcher.GetAllUserSubscribedMods(1U);
      this.mModPublisher.LoadStagingModFolderInfo();
    }

    public void ClearQueriedItemsLists()
    {
      this.mUserPublishedMods.Clear();
      this.mAllWorkshopItems.Clear();
      this.mSubscribedWorkshopItems.Clear();
    }

    public void OpenItemWebpageOnOverlay(PublishedFileId_t inPublishedFileId)
    {
      if (!SteamManager.Initialized)
        Debug.Log((object) "ModManager::OpenItemWebpageOnOverlay - trying to open webpage item when Steam is not Initialized!", (UnityEngine.Object) null);
      else
        SteamFriends.ActivateGameOverlayToWebPage("steam://url/CommunityFilePage/" + (object) inPublishedFileId);
    }

    public void OpenItemWebpageOnOverlay(SteamMod inSteamMod)
    {
      this.OpenItemWebpageOnOverlay(inSteamMod.modDetails.m_nPublishedFileId);
    }

    public void OpenWebpageLinkOnOverlay(string inLink)
    {
      if (!SteamManager.Initialized)
        Debug.Log((object) "ModManager::OpenWebpageOnOverlay - trying to open webpage item when Steam is not Initialized!", (UnityEngine.Object) null);
      else
        SteamFriends.ActivateGameOverlayToWebPage(inLink);
    }

    public void OpenWorkshopOnOverlay()
    {
      if (!SteamManager.Initialized)
        Debug.Log((object) "ModManager::OpenWorkshopOnOverlay - trying to open webpage item when Steam is not Initialized!", (UnityEngine.Object) null);
      else
        SteamFriends.ActivateGameOverlayToWebPage("http://steamcommunity.com/app/" + SteamManager.MM2AppID.ToString() + "/workshop/");
    }

    private void OnGameOverlayActivated(GameOverlayActivated_t inActivatedCallback)
    {
      if (this.OnOverlayScreenActivatedAction == null)
        return;
      this.OnOverlayScreenActivatedAction((int) inActivatedCallback.m_bActive != 0);
    }

    public void SaveActiveModsFile()
    {
      if (!SteamManager.Initialized)
      {
        Debug.Log((object) "ModManager::SaveActiveMods - trying to save active mods when Steam is not Initialized!", (UnityEngine.Object) null);
      }
      else
      {
        StringBuilder stringBuilder = new StringBuilder();
        List<SteamMod> userSubscribedMods = this.mAllUserSubscribedMods;
        for (int index = 0; index < userSubscribedMods.Count; ++index)
        {
          ulong publishedFileId = userSubscribedMods[index].modDetails.m_nPublishedFileId.m_PublishedFileId;
          int num = !userSubscribedMods[index].isActive ? 0 : 1;
          stringBuilder.AppendLine(((long) publishedFileId).ToString() + "," + (object) num);
        }
        File.WriteAllText(System.IO.Path.Combine(Application.persistentDataPath, this.mActiveModsSaveFileName), stringBuilder.ToString());
      }
    }

    public List<SavedSubscribedModInfo> NewGameSaveSubscribedModsInfo()
    {
      List<SavedSubscribedModInfo> subscribedModInfoList = new List<SavedSubscribedModInfo>();
      if (SteamManager.Initialized)
      {
        List<SteamMod> userSubscribedMods = this.mAllUserSubscribedMods;
        for (int index = 0; index < userSubscribedMods.Count; ++index)
        {
          if (userSubscribedMods[index].isActive)
            subscribedModInfoList.Add(new SavedSubscribedModInfo()
            {
              id = userSubscribedMods[index].modID,
              name = userSubscribedMods[index].modDetails.m_rgchTitle,
              isNewGameRequired = userSubscribedMods[index].HasTag(StagingMod.newGameRequiredTag)
            });
        }
      }
      return subscribedModInfoList;
    }

    public void ReviseSubscribedModsInfo(ref List<SavedSubscribedModInfo> inOldSubscribedModsInfo)
    {
      if (!SteamManager.Initialized)
        return;
      for (int index = inOldSubscribedModsInfo.Count - 1; index >= 0; --index)
      {
        SteamMod subscribedMod = this.GetSubscribedMod(inOldSubscribedModsInfo[index].id);
        if (subscribedMod != null)
        {
          if (!subscribedMod.isActive)
            inOldSubscribedModsInfo.RemoveAt(index);
        }
        else
          inOldSubscribedModsInfo.RemoveAt(index);
      }
      List<SteamMod> userSubscribedMods = this.mAllUserSubscribedMods;
      for (int index1 = 0; index1 < userSubscribedMods.Count; ++index1)
      {
        bool flag = false;
        for (int index2 = 0; index2 < inOldSubscribedModsInfo.Count; ++index2)
        {
          if ((long) userSubscribedMods[index1].modID == (long) inOldSubscribedModsInfo[index2].id)
          {
            flag = true;
            break;
          }
        }
        if (!flag && userSubscribedMods[index1].isActive)
          inOldSubscribedModsInfo.Add(new SavedSubscribedModInfo()
          {
            id = userSubscribedMods[index1].modID,
            name = userSubscribedMods[index1].modDetails.m_rgchTitle,
            isNewGameRequired = userSubscribedMods[index1].HasTag(StagingMod.newGameRequiredTag)
          });
      }
    }

    private void LoadActiveModsSavedFile(List<SteamMod> inSteamModList, uint inTotalMatchingResults)
    {
      if (!SteamManager.Initialized)
      {
        Debug.Log((object) "ModManager::LoadActiveModsSavedFile - trying to save active mods when Steam is not Initialized!", (UnityEngine.Object) null);
      }
      else
      {
        this.modFetcher.OnFetchSubscribedSuccess -= new Action<List<SteamMod>, uint>(this.LoadActiveModsSavedFile);
        string path = System.IO.Path.Combine(Application.persistentDataPath, this.mActiveModsSaveFileName);
        if (!File.Exists(path))
          return;
        this.ParseActiveModsData(File.ReadAllText(path));
      }
    }

    private void ParseActiveModsData(string inActiveModsText)
    {
      if (!SteamManager.Initialized)
      {
        Debug.Log((object) "ModManager::LoadActiveModsSavedFile - trying to save active mods when Steam is not Initialized!", (UnityEngine.Object) null);
      }
      else
      {
        using (StringReader stringReader = new StringReader(inActiveModsText))
        {
          string str;
          while ((str = stringReader.ReadLine()) != null)
          {
            string[] strArray = str.Split(',');
            ulong result1 = 0;
            if (ulong.TryParse(strArray[0], out result1))
            {
              int result2 = 0;
              if (int.TryParse(strArray[1], out result2))
              {
                bool inActive = result2 == 1;
                SteamMod subscribedMod = this.GetSubscribedMod(result1);
                if (subscribedMod != null)
                  subscribedMod.SetActive(inActive);
              }
            }
          }
        }
      }
    }

    public string GetModStatus(ulong inModID)
    {
      if (SteamManager.Initialized)
      {
        if (!SteamMod.HasFlagState((PublishedFileId_t) inModID, 1U))
          return Localisation.LocaliseID("PSG_10012088", (GameObject) null);
        if (!SteamMod.HasFlagState((PublishedFileId_t) inModID, 4U))
          return Localisation.LocaliseID("PSG_10012089", (GameObject) null);
        if (SteamMod.HasFlagState((PublishedFileId_t) inModID, 8U))
          return Localisation.LocaliseID("PSG_10012090", (GameObject) null);
        if (SteamMod.HasFlagState((PublishedFileId_t) inModID, 16U) || SteamMod.HasFlagState((PublishedFileId_t) inModID, 32U))
          return Localisation.LocaliseID("PSG_10012091", (GameObject) null);
        SteamMod subscribedMod = this.GetSubscribedMod(inModID);
        if (subscribedMod != null && !subscribedMod.isActive)
          return Localisation.LocaliseID("PSG_10012092", (GameObject) null);
        if (!SteamManager.IsSteamOnline())
          return Localisation.LocaliseID("PSG_10011724", (GameObject) null);
      }
      return string.Empty;
    }

    public bool IsModAlreadyInList(List<SteamMod> inListToCheck, ulong inPublishedID)
    {
      for (int index = 0; index < inListToCheck.Count; ++index)
      {
        if ((long) inListToCheck[index].modDetails.m_nPublishedFileId.m_PublishedFileId == (long) inPublishedID)
          return true;
      }
      return false;
    }

    public bool IsModAlreadyInList(List<SteamMod> inListToCheck, PublishedFileId_t inPublishedID)
    {
      return this.IsModAlreadyInList(inListToCheck, inPublishedID.m_PublishedFileId);
    }

    public bool IsModAlreadyInList(List<SteamMod> inListToCheck, SteamMod inSteamMod)
    {
      return this.IsModAlreadyInList(inListToCheck, (ulong) inSteamMod.modDetails.m_nPublishedFileId);
    }

    public SteamMod GetSubscribedMod(ulong inPublishedID)
    {
      return this.GetMod(this.mAllUserSubscribedMods, inPublishedID);
    }

    public SteamMod GetPublishedMod(ulong inPublishedID)
    {
      return this.GetMod(this.mUserPublishedMods, inPublishedID);
    }

    public SteamMod GetWorkshopMod(ulong inPublishedID)
    {
      return this.GetMod(this.mAllWorkshopItems, inPublishedID);
    }

    private SteamMod GetMod(List<SteamMod> inModList, ulong inPublishedID)
    {
      SteamMod steamMod = (SteamMod) null;
      for (int index = 0; index < inModList.Count; ++index)
      {
        if ((long) inModList[index].modDetails.m_nPublishedFileId.m_PublishedFileId == (long) inPublishedID)
        {
          steamMod = inModList[index];
          break;
        }
      }
      return steamMod;
    }

    public bool IsAssetLoadRequiredForActiveMods()
    {
      if (this.modPublisher.isStaging)
        return this.modPublisher.stagingMod.IsAssetLoadRequired();
      List<SteamMod> userSubscribedMods = this.allUserSubscribedMods;
      for (int index = 0; index < userSubscribedMods.Count; ++index)
      {
        if (userSubscribedMods[index].isActive && (userSubscribedMods[index].IsAssetLoadRequired() || !userSubscribedMods[index].isInstalled))
          return true;
      }
      return false;
    }

    public int GetNewGameModsCount(bool inCountActiveModsOnly)
    {
      int num = 0;
      for (int index = 0; index < this.allUserSubscribedMods.Count; ++index)
      {
        if (this.allUserSubscribedMods[index].HasTag(StagingMod.newGameRequiredTag))
        {
          if (inCountActiveModsOnly && this.allUserSubscribedMods[index].isActive)
            ++num;
          else if (!inCountActiveModsOnly)
            ++num;
        }
      }
      return num;
    }
  }
}
