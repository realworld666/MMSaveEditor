// Decompiled with JetBrains decompiler
// Type: ModdingSystem.ModLogoFileInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.IO;
using UnityEngine;

namespace ModdingSystem
{
  public class ModLogoFileInfo : ModFileInfo
  {
    public static readonly string[] logoTags = new string[2]{ "Logos", "Custom Portraits" };
    public static readonly string[] logoTagsID = new string[2]{ "PSG_10012018", "PSG_10012158" };
    private AtlasManager.Atlas mAtlasType = AtlasManager.Atlas.Count;

    public override ModFileInfo.ModFileInfoType fileInfoType
    {
      get
      {
        return ModFileInfo.ModFileInfoType.Logo;
      }
    }

    public AtlasManager.Atlas atlasType
    {
      get
      {
        return this.mAtlasType;
      }
    }

    public override void LoadDataFromFileInfo(FileInfo inFileInfo)
    {
      base.LoadDataFromFileInfo(inFileInfo);
      if (inFileInfo.Name.ToLowerInvariant() == "logos1")
        this.mAtlasType = AtlasManager.Atlas.Logos1;
      else if (inFileInfo.Name.ToLowerInvariant() == "hqicons")
        this.mAtlasType = AtlasManager.Atlas.HQIcons;
      else if (inFileInfo.Name.ToLowerInvariant() == "characterportraits1")
        this.mAtlasType = AtlasManager.Atlas.CharacterPortraits1;
      this.CacheAssets();
    }

    public override string GetModFileTag()
    {
      switch (this.mAtlasType)
      {
        case AtlasManager.Atlas.Logos1:
        case AtlasManager.Atlas.HQIcons:
          return ModLogoFileInfo.logoTags[0];
        case AtlasManager.Atlas.CharacterPortraits1:
          return ModLogoFileInfo.logoTags[1];
        default:
          return (string) null;
      }
    }

    public Texture2D GetAtlasTexture(AtlasManager.Atlas inAtlasType)
    {
      return this.GetAsset<Texture2D>(inAtlasType.ToString());
    }
  }
}
