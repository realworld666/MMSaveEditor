// Decompiled with JetBrains decompiler
// Type: ModdingSystem.ModFileInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ModdingSystem
{
  public class ModFileInfo
  {
    protected Dictionary<string, object> mCachedAssets = new Dictionary<string, object>();
    protected FileInfo mFileInfo;

    public FileInfo fileInfo
    {
      get
      {
        return this.mFileInfo;
      }
    }

    public virtual ModFileInfo.ModFileInfoType fileInfoType
    {
      get
      {
        return ModFileInfo.ModFileInfoType.Count;
      }
    }

    public virtual void LoadDataFromFileInfo(FileInfo inFileInfo)
    {
      this.mFileInfo = inFileInfo;
    }

    public void UnloadAssets()
    {
      this.mCachedAssets.Clear();
    }

    public void CacheAssets()
    {
      this.mCachedAssets.Clear();
      AssetBundle assetBundle = AssetBundle.LoadFromFile(this.mFileInfo.FullName);
      Object[] objectArray = assetBundle.LoadAllAssets();
      for (int index = 0; index < objectArray.Length; ++index)
        this.mCachedAssets.Add(objectArray[index].name, (object) objectArray[index]);
      assetBundle.Unload(false);
    }

    public T GetAsset<T>(string inAssetName) where T : Object
    {
      if (this.mCachedAssets.ContainsKey(inAssetName))
        return (T) this.mCachedAssets[inAssetName];
      return (T) null;
    }

    public bool ContainsAsset(string inAssetName)
    {
      return this.mCachedAssets.ContainsKey(inAssetName);
    }

    public virtual string GetModFileTag()
    {
      return (string) null;
    }

    public virtual void LoadMetadata(ModMetadata inModMetadata)
    {
    }

    protected string GetFileNameWithNoExtension()
    {
      return this.fileInfo.Name.Split('.')[0];
    }

    public bool HasCachedAssets()
    {
      return this.mCachedAssets.Count > 0;
    }

    public enum ModFileInfoType
    {
      Logo,
      Image,
      Database,
      Model,
      Video,
      Count,
    }
  }
}
