// Decompiled with JetBrains decompiler
// Type: ModdingSystem.SteamModFetcher
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using Steamworks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModdingSystem
{
  public class SteamModFetcher
  {
    private CallResult<SteamUGCQueryCompleted_t> mQueryAllSubscribedCompleted = new CallResult<SteamUGCQueryCompleted_t>((CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate) null);
    private CallResult<SteamUGCQueryCompleted_t> mQueryPublishedCompleted = new CallResult<SteamUGCQueryCompleted_t>((CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate) null);
    private CallResult<SteamUGCQueryCompleted_t> mQueryAllWorkshopItemsCompleted = new CallResult<SteamUGCQueryCompleted_t>((CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate) null);
    private CallResult<SteamUGCQueryCompleted_t> mQuerySubscribedItemsCompleted = new CallResult<SteamUGCQueryCompleted_t>((CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate) null);
    private CallResult<SteamUGCQueryCompleted_t> mQueryGetItemsDetailsCompleted = new CallResult<SteamUGCQueryCompleted_t>((CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate) null);
    public Action<List<SteamMod>, uint> OnFetchSubscribedSuccess;
    public Action<string> OnFetchSubscribedFailure;
    public Action<List<SteamMod>, uint> OnFetchPublishedSuccess;
    public Action<string> OnFetchPublishedFailure;
    public Action<List<SteamMod>, uint> OnFetchAllWorkshopItemsSuccess;
    public Action<string> OnFetchAllWorkshopItemsFailure;
    public Action<List<SteamMod>, uint> OnFetchWorkshopItemsDetailsSuccess;
    public Action<string> OnFetchWorkshopItemsDetailsFailure;
    public Action<ulong> OnFetchSteamModAuthor;
    private Callback<PersonaStateChange_t> mPersonStateChange;
    private uint mAllSubscribedItemsCount;
    private uint mPublishedItemsCount;
    private uint mAllWorkshopItemsCount;
    private uint mSubscribedItemsCount;

    public uint publishedItemsCount
    {
      get
      {
        return this.mPublishedItemsCount;
      }
    }

    public uint subscribedItemsCount
    {
      get
      {
        return this.mSubscribedItemsCount;
      }
    }

    public uint allWorkshopItemsCount
    {
      get
      {
        return this.mAllWorkshopItemsCount;
      }
    }

    public void Initialise()
    {
      this.mPersonStateChange = Callback<PersonaStateChange_t>.Create(new Callback<PersonaStateChange_t>.DispatchDelegate(this.OnPersonaStateChange));
    }

    public bool FetchAuthorName(SteamMod inSteamMod, out string inAuthorName)
    {
      inAuthorName = string.Empty;
      CSteamID ulSteamIdOwner = (CSteamID) inSteamMod.modDetails.m_ulSteamIDOwner;
      if (!SteamFriends.RequestUserInformation(ulSteamIdOwner, false))
        inAuthorName = SteamFriends.GetFriendPersonaName(ulSteamIdOwner);
      return !string.IsNullOrEmpty(inAuthorName);
    }

    private void OnPersonaStateChange(PersonaStateChange_t inPersonStateChangeCallback)
    {
      if (!SteamMod.HasPersonChangeStateFlag(inPersonStateChangeCallback.m_nChangeFlags, 1) || this.OnFetchSteamModAuthor == null)
        return;
      this.OnFetchSteamModAuthor(inPersonStateChangeCallback.m_ulSteamID);
    }

    private CallResult<SteamUGCQueryCompleted_t> QueryUserUGCRequest(EUserUGCList inListType, CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate dispatchDelegate, string[] includeTags, string[] excludeTags, uint inPaging = 1)
    {
      if (!SteamManager.Initialized)
        return (CallResult<SteamUGCQueryCompleted_t>) null;
      AccountID_t accountId = SteamUser.GetSteamID().GetAccountID();
      CallResult<SteamUGCQueryCompleted_t> callResult = CallResult<SteamUGCQueryCompleted_t>.Create(dispatchDelegate);
      UGCQueryHandle_t queryUserUgcRequest = SteamUGC.CreateQueryUserUGCRequest(accountId, inListType, EUGCMatchingUGCType.k_EUGCMatchingUGCType_All, EUserUGCListSortOrder.k_EUserUGCListSortOrder_SubscriptionDateDesc, SteamManager.MM2AppID, SteamManager.MM2AppID, inPaging);
      SteamUGC.SetReturnMetadata(queryUserUgcRequest, true);
      SteamUGC.SetReturnLongDescription(queryUserUgcRequest, true);
      this.SetTags(queryUserUgcRequest, includeTags, excludeTags);
      SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(queryUserUgcRequest);
      callResult.Set(hAPICall, (CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate) null);
      return callResult;
    }

    private CallResult<SteamUGCQueryCompleted_t> QueryAllUGCRequest(EUGCMatchingUGCType inListType, CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate dispatchDelegate, string[] includeTags, string[] excludeTags, uint inPaging = 1)
    {
      if (!SteamManager.Initialized)
        return (CallResult<SteamUGCQueryCompleted_t>) null;
      CallResult<SteamUGCQueryCompleted_t> callResult = CallResult<SteamUGCQueryCompleted_t>.Create(dispatchDelegate);
      UGCQueryHandle_t queryAllUgcRequest = SteamUGC.CreateQueryAllUGCRequest(EUGCQuery.k_EUGCQuery_RankedByTrend, inListType, SteamManager.MM2AppID, SteamManager.MM2AppID, inPaging);
      SteamUGC.SetReturnMetadata(queryAllUgcRequest, true);
      SteamUGC.SetReturnLongDescription(queryAllUgcRequest, true);
      this.SetTags(queryAllUgcRequest, includeTags, excludeTags);
      SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(queryAllUgcRequest);
      callResult.Set(hAPICall, (CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate) null);
      return callResult;
    }

    private CallResult<SteamUGCQueryCompleted_t> QueryUGCDetailsRequest(PublishedFileId_t[] inPublishedFileIDs, uint inNumberOfPublishedIDs, CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate dispatchDelegate)
    {
      if (!SteamManager.Initialized)
        return (CallResult<SteamUGCQueryCompleted_t>) null;
      CallResult<SteamUGCQueryCompleted_t> callResult = CallResult<SteamUGCQueryCompleted_t>.Create(dispatchDelegate);
      UGCQueryHandle_t ugcDetailsRequest = SteamUGC.CreateQueryUGCDetailsRequest(inPublishedFileIDs, inNumberOfPublishedIDs);
      SteamUGC.SetReturnMetadata(ugcDetailsRequest, true);
      SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(ugcDetailsRequest);
      callResult.Set(hAPICall, (CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate) null);
      return callResult;
    }

    private void SetTags(UGCQueryHandle_t inHandle, string[] includeTags, string[] excludeTags)
    {
      if (includeTags != null)
      {
        for (int index = 0; index < includeTags.Length; ++index)
          SteamUGC.AddRequiredTag(inHandle, includeTags[index]);
      }
      if (excludeTags == null)
        return;
      for (int index = 0; index < excludeTags.Length; ++index)
        SteamUGC.AddExcludedTag(inHandle, excludeTags[index]);
    }

    public void GetAllUserSubscribedMods(uint inPaging = 1)
    {
      this.mQueryAllSubscribedCompleted = this.MakeUserUGCRequest(EUserUGCList.k_EUserUGCList_Subscribed, new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(this.OnFetchAllSubscribedMods), (string[]) null, (string[]) null, inPaging);
    }

    public void QueryUserSubscribedMods(string[] includeTags, string[] excludeTags, uint inPaging = 1)
    {
      this.mQuerySubscribedItemsCompleted = this.MakeUserUGCRequest(EUserUGCList.k_EUserUGCList_Subscribed, new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(this.OnFetchSubscribedMods), includeTags, excludeTags, inPaging);
    }

    public void GetUserPublishedMods(string[] includeTags, string[] excludeTags, uint inPaging = 1)
    {
      this.mQueryPublishedCompleted = this.MakeUserUGCRequest(EUserUGCList.k_EUserUGCList_Published, new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(this.OnFetchPublishedMods), includeTags, excludeTags, inPaging);
    }

    public CallResult<SteamUGCQueryCompleted_t> MakeUserUGCRequest(EUserUGCList inUGCListType, CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate inDelegate, string[] includeTags, string[] excludeTags, uint inPaging = 1)
    {
      CallResult<SteamUGCQueryCompleted_t> callResult = this.QueryUserUGCRequest(inUGCListType, inDelegate, includeTags, excludeTags, inPaging);
      if (callResult == null)
      {
        Debug.Log((object) "ModdingSystem::SteamModFetcher - Fail to make UGC request for user, Steam Manager not initialized.", (UnityEngine.Object) null);
        if (this.OnFetchSubscribedFailure != null)
          this.OnFetchSubscribedFailure("Steam Manager not initialized. Make sure to have steam running and MotorsportManager installed.");
      }
      return callResult;
    }

    public void GetAllWorkshopItems(string[] includeTags, string[] excludeTags, uint inPaging = 1)
    {
      this.mQueryAllWorkshopItemsCompleted = this.QueryAllUGCRequest(EUGCMatchingUGCType.k_EUGCMatchingUGCType_All, new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(this.OnFetchAllWorkshopItems), includeTags, excludeTags, inPaging);
      if (this.mQueryAllWorkshopItemsCompleted != null)
        return;
      Debug.Log((object) "ModdingSystem::SteamModFetcher - Fail to retrieve all steam workshop items UGC for user, Steam Manager not initialized.", (UnityEngine.Object) null);
      if (this.OnFetchAllWorkshopItemsFailure == null)
        return;
      this.OnFetchAllWorkshopItemsFailure("Steam Manager not initialized. Make sure to have steam running and MotorsportManager installed.");
    }

    public void GetWorkshopItemsDetails(PublishedFileId_t[] inPublishedFileIDs, uint inNumberOfPublishedIDs)
    {
      this.mQueryGetItemsDetailsCompleted = this.QueryUGCDetailsRequest(inPublishedFileIDs, inNumberOfPublishedIDs, new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(this.OnFetchWorkshopItemsDetails));
      if (this.mQueryGetItemsDetailsCompleted != null)
        return;
      Debug.Log((object) "ModdingSystem::SteamModFetcher - Fail to retrieve steam workshop items details, Steam Manager not initialized.", (UnityEngine.Object) null);
      if (this.OnFetchWorkshopItemsDetailsFailure == null)
        return;
      this.OnFetchWorkshopItemsDetailsFailure("Steam Manager not initialized. Make sure to have steam running and MotorsportManager installed.");
    }

    private void OnFetchAllSubscribedMods(SteamUGCQueryCompleted_t callback, bool IOFailure)
    {
      if (callback.m_eResult != EResult.k_EResultOK || IOFailure)
      {
        Debug.Log((object) "ModdingSystem::SteamModFetcher - Fail to retrieve subscribed UGC for user", (UnityEngine.Object) null);
        if (this.OnFetchSubscribedFailure != null)
          this.OnFetchSubscribedFailure(callback.m_eResult.ToString());
      }
      else
      {
        ModManager modManager = App.instance.modManager;
        this.mAllSubscribedItemsCount = callback.m_unTotalMatchingResults;
        List<SteamMod> steamModList = this.ProcessSuccessfulQueryResult(callback);
        modManager.allUserSubscribedMods.AddRange((IEnumerable<SteamMod>) steamModList);
        if ((long) this.mAllSubscribedItemsCount <= (long) modManager.allUserSubscribedMods.Count)
        {
          this.LoadSubscribedModsInfo();
          if (this.OnFetchSubscribedSuccess != null)
            this.OnFetchSubscribedSuccess(steamModList, callback.m_unTotalMatchingResults);
        }
        else
          this.GetAllUserSubscribedMods((uint) Mathf.CeilToInt((float) ((long) modManager.allUserSubscribedMods.Count / (long) this.mAllSubscribedItemsCount)));
      }
      SteamUGC.ReleaseQueryUGCRequest(callback.m_handle);
    }

    private void OnFetchSubscribedMods(SteamUGCQueryCompleted_t callback, bool IOFailure)
    {
      if (callback.m_eResult != EResult.k_EResultOK || IOFailure)
      {
        Debug.Log((object) "ModdingSystem::SteamModFetcher - Fail to retrieve subscribed UGC for user", (UnityEngine.Object) null);
        if (this.OnFetchSubscribedFailure != null)
          this.OnFetchSubscribedFailure(callback.m_eResult.ToString());
      }
      else
      {
        ModManager modManager = App.instance.modManager;
        this.mSubscribedItemsCount = callback.m_unTotalMatchingResults;
        List<SteamMod> steamModList = this.ProcessSuccessfulQueryResult(callback);
        modManager.subscribedWorkshopItems.Clear();
        modManager.subscribedWorkshopItems.AddRange((IEnumerable<SteamMod>) steamModList);
        if (this.OnFetchSubscribedSuccess != null)
          this.OnFetchSubscribedSuccess(steamModList, callback.m_unTotalMatchingResults);
      }
      SteamUGC.ReleaseQueryUGCRequest(callback.m_handle);
    }

    private void OnFetchPublishedMods(SteamUGCQueryCompleted_t callback, bool IOFailure)
    {
      if (callback.m_eResult != EResult.k_EResultOK || IOFailure)
      {
        Debug.Log((object) "ModdingSystem::SteamModFetcher - Fail to retrieve published UGC for user", (UnityEngine.Object) null);
        if (this.OnFetchPublishedFailure != null)
          this.OnFetchPublishedFailure(callback.m_eResult.ToString());
      }
      else
      {
        ModManager modManager = App.instance.modManager;
        this.mPublishedItemsCount = callback.m_unTotalMatchingResults;
        List<SteamMod> steamModList = this.ProcessSuccessfulQueryResult(callback);
        modManager.userPublishedMods.Clear();
        modManager.userPublishedMods.AddRange((IEnumerable<SteamMod>) steamModList);
        if (this.OnFetchPublishedSuccess != null)
          this.OnFetchPublishedSuccess(steamModList, callback.m_unTotalMatchingResults);
      }
      SteamUGC.ReleaseQueryUGCRequest(callback.m_handle);
    }

    private void OnFetchWorkshopItemsDetails(SteamUGCQueryCompleted_t callback, bool IOFailure)
    {
      if (callback.m_eResult != EResult.k_EResultOK || IOFailure)
      {
        Debug.Log((object) "ModdingSystem::SteamModFetcher - Fail to retrieve all steam workshop items UGC for user", (UnityEngine.Object) null);
        if (this.OnFetchWorkshopItemsDetailsFailure != null)
          this.OnFetchWorkshopItemsDetailsFailure(callback.m_eResult.ToString());
      }
      else
      {
        ModManager modManager = App.instance.modManager;
        List<SteamMod> steamModList = this.ProcessSuccessfulQueryResult(callback);
        modManager.allUserSubscribedMods.AddRange((IEnumerable<SteamMod>) steamModList);
        if (this.OnFetchAllWorkshopItemsSuccess != null)
          this.OnFetchAllWorkshopItemsSuccess(steamModList, callback.m_unTotalMatchingResults);
      }
      SteamUGC.ReleaseQueryUGCRequest(callback.m_handle);
    }

    private void OnFetchAllWorkshopItems(SteamUGCQueryCompleted_t callback, bool IOFailure)
    {
      if (callback.m_eResult != EResult.k_EResultOK || IOFailure)
      {
        Debug.Log((object) "ModdingSystem::SteamModFetcher - Fail to retrieve all steam workshop items UGC for user", (UnityEngine.Object) null);
        if (this.OnFetchAllWorkshopItemsFailure != null)
          this.OnFetchAllWorkshopItemsFailure(callback.m_eResult.ToString());
      }
      else
      {
        ModManager modManager = App.instance.modManager;
        this.mAllWorkshopItemsCount = callback.m_unTotalMatchingResults;
        List<SteamMod> steamModList = this.ProcessSuccessfulQueryResult(callback);
        modManager.allWorkshopItems.Clear();
        modManager.allWorkshopItems.AddRange((IEnumerable<SteamMod>) steamModList);
        if (this.OnFetchAllWorkshopItemsSuccess != null)
          this.OnFetchAllWorkshopItemsSuccess(steamModList, callback.m_unTotalMatchingResults);
      }
      SteamUGC.ReleaseQueryUGCRequest(callback.m_handle);
    }

    private List<SteamMod> ProcessSuccessfulQueryResult(SteamUGCQueryCompleted_t callback)
    {
      List<SteamMod> steamModList = new List<SteamMod>();
      uint numResultsReturned = callback.m_unNumResultsReturned;
      for (uint index = 0; index < numResultsReturned; ++index)
      {
        SteamUGCDetails_t pDetails = new SteamUGCDetails_t();
        if (SteamUGC.GetQueryUGCResult(callback.m_handle, index, out pDetails))
        {
          SteamMod steamMod = new SteamMod(pDetails);
          steamMod.Initialise();
          string pchMetadata = (string) null;
          if (SteamUGC.GetQueryUGCMetadata(callback.m_handle, index, out pchMetadata, 5000U))
            steamMod.SetMetadata(pchMetadata);
          string pchURL = (string) null;
          uint cchURLSize = 1000000;
          if (SteamUGC.GetQueryUGCPreviewURL(callback.m_handle, index, out pchURL, cchURLSize))
            steamMod.SetPreviewURL(pchURL);
          steamModList.Add(steamMod);
        }
      }
      return steamModList;
    }

    public float GetDownloadProgress(SteamMod inSteamMod)
    {
      if (SteamManager.Initialized && inSteamMod.isDownloading)
      {
        ulong punBytesDownloaded = 0;
        ulong punBytesTotal = 0;
        if (SteamUGC.GetItemDownloadInfo(inSteamMod.modDetails.m_nPublishedFileId, out punBytesDownloaded, out punBytesTotal) && punBytesTotal > 0UL)
          return (float) (punBytesDownloaded / punBytesTotal);
      }
      return 0.0f;
    }

    public bool DownloadMod(SteamMod inSteamMod)
    {
      if (SteamManager.Initialized)
        return SteamUGC.DownloadItem(inSteamMod.modDetails.m_nPublishedFileId, true);
      return false;
    }

    private void LoadSubscribedModsInfo()
    {
      List<SteamMod> userSubscribedMods = App.instance.modManager.allUserSubscribedMods;
      for (int index = 0; index < userSubscribedMods.Count; ++index)
      {
        userSubscribedMods[index].LoadModFolderInfo();
        userSubscribedMods[index].LoadMetadataForFiles();
      }
    }
  }
}
