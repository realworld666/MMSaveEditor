// Decompiled with JetBrains decompiler
// Type: ModdingSystem.SteamMod
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace ModdingSystem
{
  public class SteamMod : BasicMod
  {
    private static List<Texture2D> downloadedTextures = new List<Texture2D>();
    public ModMetadata modMetadata = new ModMetadata();
    private SteamUGCDetails_t mModDetails = new SteamUGCDetails_t();
    public Action onFetchPreview;
    public Texture2D previewTexture;
    private string mPreviewImageURL;
    private string[] mTags;
    private bool mIsActive;

    public bool isActive
    {
      get
      {
        return this.mIsActive;
      }
    }

    public bool isInstalled
    {
      get
      {
        return SteamMod.HasFlagState(this.modDetails.m_nPublishedFileId, 4U);
      }
    }

    public bool isSubscribed
    {
      get
      {
        return SteamMod.HasFlagState(this.modDetails.m_nPublishedFileId, 1U);
      }
    }

    public bool needsUpdate
    {
      get
      {
        return SteamMod.HasFlagState(this.modDetails.m_nPublishedFileId, 8U);
      }
    }

    public bool isDownloading
    {
      get
      {
        if (!SteamMod.HasFlagState(this.modDetails.m_nPublishedFileId, 16U))
          return SteamMod.HasFlagState(this.modDetails.m_nPublishedFileId, 32U);
        return true;
      }
    }

    public SteamUGCDetails_t modDetails
    {
      get
      {
        return this.mModDetails;
      }
    }

    public ulong modID
    {
      get
      {
        return this.mModDetails.m_nPublishedFileId.m_PublishedFileId;
      }
    }

    public string authorName
    {
      get
      {
        return SteamFriends.GetFriendPersonaName((CSteamID) this.mModDetails.m_ulSteamIDOwner);
      }
    }

    public string[] tags
    {
      get
      {
        this.LoadTags();
        return this.mTags;
      }
    }

    public float score
    {
      get
      {
        return this.mModDetails.m_flScore;
      }
    }

    public uint totalVotes
    {
      get
      {
        return this.mModDetails.m_unVotesUp + this.mModDetails.m_unVotesDown;
      }
    }

    public int stars
    {
      get
      {
        return Mathf.CeilToInt(5f * this.score);
      }
    }

    public SteamMod(SteamUGCDetails_t inModDetails)
    {
      this.mModDetails = inModDetails;
    }

    public static void UnloadDownloadedTextures()
    {
      for (int index = 0; index < SteamMod.downloadedTextures.Count; ++index)
        UnityEngine.Object.DestroyImmediate((UnityEngine.Object) SteamMod.downloadedTextures[index]);
      SteamMod.downloadedTextures.Clear();
    }

    public override void Initialise()
    {
      base.Initialise();
      this.modMetadata.Clear();
    }

    public override void LoadModFolderInfo()
    {
      ulong punSizeOnDisk = 0;
      string pchFolder = (string) null;
      uint punTimeStamp = 0;
      if (!SteamUGC.GetItemInstallInfo(this.mModDetails.m_nPublishedFileId, out punSizeOnDisk, out pchFolder, 260U, out punTimeStamp))
        return;
      this.mModFolder = pchFolder;
      base.LoadModFolderInfo();
    }

    private void LoadTags()
    {
      if (this.mTags != null)
        return;
      if (!string.IsNullOrEmpty(this.mModDetails.m_rgchTags))
        this.mTags = this.mModDetails.m_rgchTags.Split(',');
      else
        this.mTags = new string[0];
    }

    public bool HasTag(string tag)
    {
      this.LoadTags();
      if (!this.mModDetails.m_bTagsTruncated)
        ;
      for (int index = 0; index < this.mTags.Length; ++index)
      {
        if (this.mTags[index] == tag)
          return true;
      }
      return false;
    }

    public void SetActive(bool inActive)
    {
      this.mIsActive = inActive;
    }

    public void SetMetadata(string inMetadata)
    {
      this.modMetadata = new ModMetadata();
      this.modMetadata.LoadMetadata(inMetadata);
    }

    public void LoadMetadataForFiles()
    {
      for (int index = 0; index < this.modFiles.Count; ++index)
        this.modFiles[index].LoadMetadata(this.modMetadata);
    }

    public static bool HasFlagState(PublishedFileId_t publishedID, uint inItemState)
    {
      return (int) (SteamUGC.GetItemState(publishedID) & inItemState) == (int) inItemState;
    }

    public static bool HasPersonChangeStateFlag(EPersonaChange inPersonaChange, int inPersonaChangeFlag)
    {
      return (inPersonaChange & (EPersonaChange) inPersonaChangeFlag) == (EPersonaChange) inPersonaChangeFlag;
    }

    public void SetPreviewURL(string inURL)
    {
      this.mPreviewImageURL = inURL;
    }

    [DebuggerHidden]
    public IEnumerator DownloadPreviewTexture()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SteamMod.\u003CDownloadPreviewTexture\u003Ec__Iterator11() { \u003C\u003Ef__this = this };
    }
  }
}
