// Decompiled with JetBrains decompiler
// Type: DLCManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using Steamworks;
using System;
using System.Collections.Generic;

public class DLCManager
{
  public static readonly List<Dlc> DlcList = new List<Dlc>()
  {
    new Dlc(0, SteamManager.MM2AppID.m_AppId, 83527U, "Base Game", "BaseGameLocID", string.Empty, true),
    new Dlc(1, 551810U, 136963U, "Livery Pack", "LiveryPackLocID", "livery_pack_dlc", false),
    new Dlc(2, 559430U, 140164U, "New Championship", "NewChampLocID", "new_championship_dlc", false),
    new Dlc(3, 568380U, 143796U, "Create Team", "CreateTeamLocId", "create_team_dlc", false)
  };
  public Action OnOwnedDlcChanged;
  private Callback<DlcInstalled_t> _DlcInstalled;

  public int dlcCount
  {
    get
    {
      return DLCManager.DlcList.Count;
    }
  }

  public List<Dlc> allDlc
  {
    get
    {
      return DLCManager.DlcList;
    }
  }

  public void Start()
  {
    if (!SteamManager.Initialized)
    {
      Debug.Log((object) "Cannot detect Steam instance. No DLC will be usable.", (UnityEngine.Object) null);
    }
    else
    {
      this.InitialiseSteamCallbacks();
      this.InitialiseOwnedDlc();
    }
  }

  public bool IsDlcInstalled(uint inAppId)
  {
    return DLCManager.GetDlcByAppId(inAppId).isOwned;
  }

  public bool IsDlcKnown(uint inAppId)
  {
    for (int index = 0; index < DLCManager.DlcList.Count; ++index)
    {
      if ((int) DLCManager.DlcList[index].appId == (int) inAppId)
        return true;
    }
    return false;
  }

  public bool IsDlcWithIdInstalled(int inDlcId)
  {
    for (int index = 0; index < DLCManager.DlcList.Count; ++index)
    {
      if (DLCManager.DlcList[index].dlcId == inDlcId)
        return DLCManager.DlcList[index].isOwned;
    }
    return false;
  }

  public void InitialiseOwnedDlc()
  {
    for (int index = 0; index < DLCManager.DlcList.Count; ++index)
    {
      Dlc dlc = DLCManager.DlcList[index];
      if (!dlc.IsBaseGame())
        this.SetDlcOwned(dlc, SteamApps.BIsDlcInstalled(new AppId_t(dlc.appId)));
    }
    this.RefreshLoadedDLCAssetBundles();
  }

  private void InitialiseSteamCallbacks()
  {
    this._DlcInstalled = Callback<DlcInstalled_t>.Create(new Callback<DlcInstalled_t>.DispatchDelegate(this.OnDlcPurchased));
    if (this._DlcInstalled != null)
      return;
    UnityEngine.Debug.LogWarning((object) "Failed to create DLC Purchase Monitor callback");
  }

  private void OnDlcPurchased(DlcInstalled_t inResult)
  {
    if (inResult.m_nAppID == AppId_t.Invalid)
    {
      UnityEngine.Debug.LogWarning((object) "OnDLCPurchased received invalid appID for purchased DLC.");
    }
    else
    {
      this.SetDlcOwned(inResult.m_nAppID.m_AppId, true);
      this.RefreshLoadedDLCAssetBundles();
    }
  }

  public void SetDlcOwned(Dlc inDlc, bool inSetOwned = true)
  {
    if (inDlc.IsBaseGame())
    {
      Debug.LogWarningFormat("Tried to add DLC {0} which was not recognised in the list of known DLC. Use DisplayAllDLC debug to see known DLC.", (object) inDlc.friendlyName);
    }
    else
    {
      if (inSetOwned == inDlc.isOwned)
        return;
      inDlc.SetOwned(inSetOwned);
      if (this.OnOwnedDlcChanged == null)
        return;
      this.OnOwnedDlcChanged();
    }
  }

  public void SetDlcOwned(int inDlcIndex, bool inSetOwned = true)
  {
    if (inDlcIndex >= DLCManager.DlcList.Count)
      Debug.LogWarningFormat("Tried to set dlc at index {0} as owned whereas there are only {1} dlc objects known to the DLC manager", new object[2]
      {
        (object) inDlcIndex,
        (object) DLCManager.DlcList.Count
      });
    this.SetDlcOwned(DLCManager.DlcList[inDlcIndex], inSetOwned);
  }

  private void SetDlcOwned(uint inAppID, bool inSetOwned = true)
  {
    for (int index = 0; index < DLCManager.DlcList.Count; ++index)
    {
      if ((int) DLCManager.DlcList[index].appId == (int) inAppID)
      {
        this.SetDlcOwned(DLCManager.DlcList[index], inSetOwned);
        return;
      }
    }
    Debug.LogWarningFormat("Player Unlocked DLC App ID {0} which was not found in the listed DLC types! Could not unlock DLC", (object) inAppID);
  }

  public void RefreshLoadedDLCAssetBundles()
  {
    for (int index = 0; index < DLCManager.DlcList.Count; ++index)
    {
      if (DLCManager.DlcList[index].isOwned)
        this.EnsureAssetsLoaded(DLCManager.DlcList[index]);
    }
  }

  public static Dlc GetDlcById(int inDlcId)
  {
    for (int index = 0; index < DLCManager.DlcList.Count; ++index)
    {
      if (DLCManager.DlcList[index].dlcId == inDlcId)
        return DLCManager.DlcList[index];
    }
    return (Dlc) null;
  }

  public static Dlc GetDlcByAppId(uint inAppId)
  {
    for (int index = 0; index < DLCManager.DlcList.Count; ++index)
    {
      if ((int) DLCManager.DlcList[index].appId == (int) inAppId)
        return DLCManager.DlcList[index];
    }
    return (Dlc) null;
  }

  public static Dlc GetDlcByName(string inFriendlyName)
  {
    for (int index = 0; index < DLCManager.DlcList.Count; ++index)
    {
      if (DLCManager.DlcList[index].friendlyName.Equals(inFriendlyName, StringComparison.OrdinalIgnoreCase))
        return DLCManager.DlcList[index];
    }
    return (Dlc) null;
  }

  public static void HandleGetDlcButton(uint inAppId)
  {
    SteamFriends.ActivateGameOverlayToStore(new AppId_t(inAppId), EOverlayToStoreFlag.k_EOverlayToStoreFlag_None);
  }

  private void EnsureAssetsLoaded(Dlc inDlc)
  {
    App.instance.assetBundleManager.CheckLoadAssetBundle(inDlc.assetBundleName);
  }

  public bool IsSeriesAvailable(Championship.Series inSeries)
  {
    switch (inSeries)
    {
      case Championship.Series.GTSeries:
        return App.instance.dlcManager.IsDlcInstalled(DLCManager.GetDlcByName("New Championship").appId);
      default:
        return true;
    }
  }
}
