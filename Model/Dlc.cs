// Decompiled with JetBrains decompiler
// Type: Dlc
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class Dlc
{
  private int mDlcId;
  private uint mAppId;
  private uint mPackageId;
  private string mAssetBundleName;
  private string mFriendlyName;
  private string mLocalisationID;
  private bool mDlcOwned;

  public string localisationID
  {
    get
    {
      return this.mLocalisationID;
    }
  }

  public string assetBundleName
  {
    get
    {
      return this.mAssetBundleName;
    }
  }

  public string friendlyName
  {
    get
    {
      return this.mFriendlyName;
    }
  }

  public int dlcId
  {
    get
    {
      return this.mDlcId;
    }
  }

  public uint packageId
  {
    get
    {
      return this.mPackageId;
    }
  }

  public uint appId
  {
    get
    {
      return this.mAppId;
    }
  }

  public bool isOwned
  {
    get
    {
      return this.mDlcOwned;
    }
  }

  public Dlc(int inDlcId, uint inAppId, uint inPackageId, string inFriendlyName, string inLocalisationID, string inAssetBundleName, bool inIsDlcOwned = false)
  {
    this.mDlcId = inDlcId;
    this.mAppId = inAppId;
    this.mPackageId = inPackageId;
    this.mAssetBundleName = inAssetBundleName;
    this.mFriendlyName = inFriendlyName;
    this.mLocalisationID = inLocalisationID;
    this.mDlcOwned = inIsDlcOwned;
  }

  public AssetBundle GetAssetBundle()
  {
    if (this.mAssetBundleName != null && this.mAssetBundleName != string.Empty)
      return App.instance.assetBundleManager.GetAssetBundle(this.mAssetBundleName);
    return (AssetBundle) null;
  }

  public void SetOwned(bool inIsOwned)
  {
    this.mDlcOwned = inIsOwned;
  }

  public bool IsBaseGame()
  {
    return (int) this.mAppId == (int) SteamManager.MM2AppID.m_AppId;
  }
}
