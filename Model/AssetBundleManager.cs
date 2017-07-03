// Decompiled with JetBrains decompiler
// Type: AssetBundleManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class AssetBundleManager : IDisposable
{
  public static readonly string assetNamePrefix = "Assets/Editor/AssetBundleResources/";
  private Dictionary<string, AssetBundle> mAssetBundles = new Dictionary<string, AssetBundle>();
  private Dictionary<string, AssetBundleManifest> mAssetBundleManifests = new Dictionary<string, AssetBundleManifest>();
  private readonly string mAssetBundleBasePath = Application.streamingAssetsPath + "/AssetBundles/";
  private AssetBundleManifest mHeadManifest;
  private List<string> mBundlesInBuild;

  public void Start()
  {
    this.mHeadManifest = new AssetBundleManifest();
    this.mHeadManifest = (AssetBundleManifest) AssetBundle.LoadFromFile(this.mAssetBundleBasePath + "AssetBundles").LoadAsset("AssetBundleManifest", typeof (AssetBundleManifest));
    this.mBundlesInBuild = ((IEnumerable<string>) this.mHeadManifest.GetAllAssetBundles()).ToList<string>();
  }

  private void OnDestroy()
  {
    for (int count = this.mAssetBundles.Count; count >= 0; --count)
      this.UnLoadAssetBundle(this.mAssetBundles.ElementAt<KeyValuePair<string, AssetBundle>>(count).Key, true);
  }

  public void Dispose()
  {
    this.OnDestroy();
  }

  public void AddAssetBundle(string inPath, string inName)
  {
    if (!this.ShouldLoadBundle(inName))
      return;
    AssetBundle assetBundle = AssetBundle.LoadFromFile(inPath);
    this.mAssetBundles.Add(inName, assetBundle);
    this.mBundlesInBuild.Add(inName);
  }

  public void CheckLoadAssetBundle(string inBundle)
  {
    if (!this.ShouldLoadBundle(inBundle))
      return;
    AssetBundle assetBundle = AssetBundle.LoadFromFile(this.mAssetBundleBasePath + inBundle);
    if ((UnityEngine.Object) assetBundle == (UnityEngine.Object) null)
    {
      Debug.LogErrorFormat("Could not load asset bundle {0}", (object) inBundle);
    }
    else
    {
      AssetBundleManifest assetBundleManifest = (AssetBundleManifest) assetBundle.LoadAsset("AssetBundleManifest");
      this.mAssetBundleManifests.Add(inBundle, assetBundleManifest);
      this.mAssetBundles.Add(inBundle, assetBundle);
    }
  }

  private bool ShouldLoadBundle(string inBundle)
  {
    return inBundle != null && !(inBundle == string.Empty) && (!this.mAssetBundles.ContainsKey(inBundle) && File.Exists(this.mAssetBundleBasePath + inBundle));
  }

  public AssetBundle GetAssetBundle(string inBundle)
  {
    this.CheckLoadAssetBundle(inBundle);
    AssetBundle assetBundle = (AssetBundle) null;
    if (this.mAssetBundles.ContainsKey(inBundle))
      this.mAssetBundles.TryGetValue(inBundle, out assetBundle);
    else
      Debug.LogWarningFormat("Missing requested asset bundle {0} for DLC", (object) inBundle);
    return assetBundle;
  }

  public object GetAssetFromBundle(string inAssetName, string inBundle)
  {
    AssetBundle assetBundle = this.GetAssetBundle(inBundle);
    string lower = (AssetBundleManager.assetNamePrefix + inBundle + "/" + inAssetName).ToLower();
    object obj = (object) null;
    if ((UnityEngine.Object) assetBundle != (UnityEngine.Object) null)
      obj = (object) assetBundle.LoadAsset(lower);
    if (obj == null)
    {
      if ((UnityEngine.Object) assetBundle != (UnityEngine.Object) null)
        Debug.LogWarningFormat("Could not get the requested asset:{0}, from the asset bundle loaded as {1}. The asset bundle contains {2}", (object) lower, (object) inBundle, (object) assetBundle.GetAllAssetNames());
      else
        Debug.LogWarningFormat("Could not get the requested asset:{0}, from the asset bundle {1}. Could not load bundle.", new object[2]
        {
          (object) lower,
          (object) inBundle
        });
    }
    return obj;
  }

  private void UnLoadAssetBundle(string inBundle, bool inKeepAssetsInUse = true)
  {
    AssetBundle assetBundle;
    this.mAssetBundles.TryGetValue(inBundle, out assetBundle);
    assetBundle.Unload(!inKeepAssetsInUse);
    this.mAssetBundles.Remove(inBundle);
  }
}
