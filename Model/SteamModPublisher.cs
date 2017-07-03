// Decompiled with JetBrains decompiler
// Type: ModdingSystem.SteamModPublisher
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using Steamworks;
using System;
using System.Collections.Generic;

namespace ModdingSystem
{
  public class SteamModPublisher
  {
    private CallResult<CreateItemResult_t> mCreateItemResult = new CallResult<CreateItemResult_t>((CallResult<CreateItemResult_t>.APIDispatchDelegate) null);
    private CallResult<SubmitItemUpdateResult_t> mSubmitItemResult = new CallResult<SubmitItemUpdateResult_t>((CallResult<SubmitItemUpdateResult_t>.APIDispatchDelegate) null);
    private CallResult<SubmitItemUpdateResult_t> mUpdateItemResult = new CallResult<SubmitItemUpdateResult_t>((CallResult<SubmitItemUpdateResult_t>.APIDispatchDelegate) null);
    private List<PublishedFileId_t> filesPublished = new List<PublishedFileId_t>();
    private PublishedFileId_t mCurrentItemPublished = new PublishedFileId_t();
    private PublishedFileId_t mUpdateItemPublished = new PublishedFileId_t();
    private UGCUpdateHandle_t mUploadHandle = new UGCUpdateHandle_t();
    public Action OnUploadComplete;
    public Action<string> OnUploadFail;
    private string mItemTitle;
    private string mItemDescription;
    private string mItemContentFolder;
    private StagingMod mStagingMod;

    public float uploadCompletion
    {
      get
      {
        if (!SteamManager.Initialized)
          return 0.0f;
        ulong punBytesProcessed = 0;
        ulong punBytesTotal = 0;
        int itemUpdateProgress = (int) SteamUGC.GetItemUpdateProgress(this.mUploadHandle, out punBytesProcessed, out punBytesTotal);
        if (punBytesTotal > 0UL)
          return (float) punBytesProcessed / (float) punBytesTotal;
        return 0.0f;
      }
    }

    public bool isStaging
    {
      get
      {
        return this.mStagingMod.isStaging;
      }
    }

    public StagingMod stagingMod
    {
      get
      {
        return this.mStagingMod;
      }
    }

    public void Initialise()
    {
      this.mStagingMod = new StagingMod();
      this.mStagingMod.Initialise();
    }

    public void LoadStagingModFolderInfo()
    {
      this.mStagingMod.LoadModFolderInfo();
    }

    public void PublishMod(string inItemTitle, string inItemDescription)
    {
      if (SteamManager.Initialized)
      {
        this.mItemTitle = inItemTitle;
        this.mItemDescription = inItemDescription;
        this.mItemContentFolder = this.mStagingMod.modStagingFolder;
        this.mCreateItemResult = CallResult<CreateItemResult_t>.Create((CallResult<CreateItemResult_t>.APIDispatchDelegate) null);
        this.mCreateItemResult.Set(SteamUGC.CreateItem(SteamManager.MM2AppID, EWorkshopFileType.k_EWorkshopFileTypeFirst), new CallResult<CreateItemResult_t>.APIDispatchDelegate(this.OnCreateItem));
      }
      else
      {
        Debug.Log((object) "ModdingSystem::PublishMod - Steam is not initialized!", (UnityEngine.Object) null);
        if (this.OnUploadFail == null)
          return;
        this.OnUploadFail("ModdingSystem::PublishMod - Steam is not initialized!");
      }
    }

    private void OnCreateItem(CreateItemResult_t callback, bool IOFailure)
    {
      if (callback.m_eResult != EResult.k_EResultOK || IOFailure)
      {
        Debug.Log((object) "ModdingSystem::SteamModUploader -  Fail creating new item", (UnityEngine.Object) null);
        if (this.OnUploadFail == null)
          return;
        this.OnUploadFail(callback.m_eResult.ToString());
      }
      else
      {
        PublishedFileId_t nPublishedFileId = callback.m_nPublishedFileId;
        this.filesPublished.Add(nPublishedFileId);
        this.mCurrentItemPublished = nPublishedFileId;
        if (callback.m_bUserNeedsToAcceptWorkshopLegalAgreement)
          App.instance.modManager.OpenItemWebpageOnOverlay(this.mCurrentItemPublished);
        this.mSubmitItemResult = CallResult<SubmitItemUpdateResult_t>.Create(new CallResult<SubmitItemUpdateResult_t>.APIDispatchDelegate(this.OnSubmit));
        this.mUploadHandle = SteamUGC.StartItemUpdate(SteamManager.MM2AppID, nPublishedFileId);
        SteamUGC.SetItemTitle(this.mUploadHandle, this.mItemTitle);
        SteamUGC.SetItemDescription(this.mUploadHandle, this.mItemDescription);
        SteamUGC.SetItemContent(this.mUploadHandle, System.IO.Path.GetFullPath(this.mItemContentFolder));
        this.SetItemMetadata(this.mUploadHandle);
        this.SetItemPreview(this.mUploadHandle);
        this.SetItemTags(this.mUploadHandle);
        this.mSubmitItemResult.Set(SteamUGC.SubmitItemUpdate(this.mUploadHandle, string.Empty), (CallResult<SubmitItemUpdateResult_t>.APIDispatchDelegate) null);
      }
    }

    public void UpdateExistingMod(PublishedFileId_t inPublishedID, string inNewTitle, string inNewDescription)
    {
      if (SteamManager.Initialized)
      {
        this.mItemContentFolder = this.mStagingMod.modStagingFolder;
        this.mUpdateItemPublished = inPublishedID;
        this.mUpdateItemResult = CallResult<SubmitItemUpdateResult_t>.Create(new CallResult<SubmitItemUpdateResult_t>.APIDispatchDelegate(this.OnUpdate));
        this.mUploadHandle = SteamUGC.StartItemUpdate(SteamManager.MM2AppID, inPublishedID);
        SteamUGC.SetItemTitle(this.mUploadHandle, inNewTitle);
        SteamUGC.SetItemDescription(this.mUploadHandle, inNewDescription);
        SteamUGC.SetItemContent(this.mUploadHandle, System.IO.Path.GetFullPath(this.mItemContentFolder));
        this.SetItemMetadata(this.mUploadHandle);
        this.SetItemPreview(this.mUploadHandle);
        this.SetItemTags(this.mUploadHandle);
        this.mUpdateItemResult.Set(SteamUGC.SubmitItemUpdate(this.mUploadHandle, string.Empty), (CallResult<SubmitItemUpdateResult_t>.APIDispatchDelegate) null);
      }
      else
      {
        Debug.Log((object) "ModdingSystem::UpdateExistingMod - Steam is not initialized!", (UnityEngine.Object) null);
        if (this.OnUploadFail == null)
          return;
        this.OnUploadFail("ModdingSystem::UpdateExistingMod - Steam is not initialized!");
      }
    }

    private void SetItemMetadata(UGCUpdateHandle_t inHandle)
    {
      string metadata = ModMetadata.CreateMetadata((BasicMod) this.mStagingMod);
      SteamUGC.SetItemMetadata(inHandle, metadata);
    }

    private void SetItemPreview(UGCUpdateHandle_t inHandle)
    {
      string previewImagePath = this.mStagingMod.GetPreviewImagePath();
      if (string.IsNullOrEmpty(previewImagePath))
        return;
      SteamUGC.SetItemPreview(inHandle, previewImagePath);
    }

    private void SetItemTags(UGCUpdateHandle_t inHandle)
    {
      List<string> tags = this.mStagingMod.GetTags();
      SteamUGC.SetItemTags(inHandle, (IList<string>) tags);
    }

    private void OnSubmit(SubmitItemUpdateResult_t callback, bool IOFailure)
    {
      if (callback.m_eResult != EResult.k_EResultOK || IOFailure)
      {
        Debug.Log((object) ("ModdingSystem::SteamModUploader - Fail to submit item, Steam returned error: " + callback.m_eResult.ToString()), (UnityEngine.Object) null);
        if (this.OnUploadFail == null)
          return;
        this.OnUploadFail(callback.m_eResult.ToString());
      }
      else
      {
        App.instance.modManager.OpenItemWebpageOnOverlay(this.mCurrentItemPublished);
        Debug.Log((object) "ModdingSystem::SteamModUploader - Success submitting item", (UnityEngine.Object) null);
        if (this.OnUploadComplete == null)
          return;
        this.OnUploadComplete();
      }
    }

    private void OnUpdate(SubmitItemUpdateResult_t callback, bool IOFailure)
    {
      if (callback.m_eResult != EResult.k_EResultOK || IOFailure)
      {
        Debug.Log((object) ("ModdingSystem::SteamModUploader - Fail to update item, Steam returned error: " + callback.m_eResult.ToString()), (UnityEngine.Object) null);
        if (this.OnUploadFail == null)
          return;
        this.OnUploadFail(callback.m_eResult.ToString());
      }
      else
      {
        App.instance.modManager.OpenItemWebpageOnOverlay(this.mUpdateItemPublished);
        Debug.Log((object) "ModdingSystem::SteamModUploader - Success updating item", (UnityEngine.Object) null);
        if (this.OnUploadComplete == null)
          return;
        this.OnUploadComplete();
      }
    }
  }
}
