// Decompiled with JetBrains decompiler
// Type: ModdingSystem.ModImageFileInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ModdingSystem
{
  public class ModImageFileInfo : ModFileInfo
  {
    public static readonly string[] imageTags = new string[4]{ "Custom Portraits", "Liveries", "Livery Pack", "Logos" };
    public static readonly string[] imageTagsIDs = new string[4]{ "PSG_10012158", "PSG_10012160", "PSG_10011288", "PSG_10012018" };
    [NonSerialized]
    public static readonly string liveryBaseName = "LiveryBase";
    [NonSerialized]
    public static readonly string liveryDetailName = "LiveryDetail";
    [NonSerialized]
    public static readonly int liveryIdOffset = 7;
    [NonSerialized]
    public static readonly string presidentPortraitName = "President";
    [NonSerialized]
    public static readonly string playerPortraitName = "Player";
    [NonSerialized]
    public static readonly string championshipSuffix = "_Championship";
    [NonSerialized]
    public static readonly string championshipImagePrefix = "Championship";
    [NonSerialized]
    public static readonly string championshipImagePrefixBW = "ChampionshipBW";
    [NonSerialized]
    public static readonly string teamImagePrefix = "Team";
    [NonSerialized]
    public static readonly string teamImagePrefixBW = "TeamBW";
    [NonSerialized]
    public static readonly string teamImagePrefixSmall = "TeamSmall";
    [NonSerialized]
    public static readonly string teamImagePrefixHat = "TeamHat";
    [NonSerialized]
    public static readonly string teamImagePrefixBody = "TeamBody";
    [NonSerialized]
    public static readonly string sponsorImagePrefix = "Sponsor";
    [NonSerialized]
    public static readonly string sponsorImagePrefixCar = "SponsorCar";
    [NonSerialized]
    public static readonly string mediaImagePrefix = "Media";
    private ModImageFileInfo.ImageType mImageType;

    public override ModFileInfo.ModFileInfoType fileInfoType
    {
      get
      {
        return ModFileInfo.ModFileInfoType.Image;
      }
    }

    public ModImageFileInfo.ImageType imageType
    {
      get
      {
        return this.mImageType;
      }
    }

    public override void LoadDataFromFileInfo(FileInfo inFileInfo)
    {
      base.LoadDataFromFileInfo(inFileInfo);
      string lowerInvariant = inFileInfo.Name.ToLowerInvariant();
      if (lowerInvariant != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (ModImageFileInfo.\u003C\u003Ef__switch\u0024map39 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ModImageFileInfo.\u003C\u003Ef__switch\u0024map39 = new Dictionary<string, int>(7)
          {
            {
              "liveries",
              0
            },
            {
              "portraits",
              1
            },
            {
              "liverypack",
              2
            },
            {
              "teamlogos",
              3
            },
            {
              "championshiplogos",
              4
            },
            {
              "sponsorlogos",
              5
            },
            {
              "medialogos",
              6
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (ModImageFileInfo.\u003C\u003Ef__switch\u0024map39.TryGetValue(lowerInvariant, out num))
        {
          switch (num)
          {
            case 0:
              this.mImageType = ModImageFileInfo.ImageType.Liveries;
              break;
            case 1:
              this.mImageType = ModImageFileInfo.ImageType.Portraits;
              break;
            case 2:
              this.mImageType = ModImageFileInfo.ImageType.LiveryPack;
              break;
            case 3:
              this.mImageType = ModImageFileInfo.ImageType.TeamLogos;
              break;
            case 4:
              this.mImageType = ModImageFileInfo.ImageType.ChampionshipLogos;
              break;
            case 5:
              this.mImageType = ModImageFileInfo.ImageType.SponsorLogos;
              break;
            case 6:
              this.mImageType = ModImageFileInfo.ImageType.MediaLogos;
              break;
          }
        }
      }
      this.CacheAssets();
    }

    public Texture GetTexture(string inTextureName)
    {
      return this.GetAsset<Texture>(inTextureName);
    }

    public string[] GetTextureNames()
    {
      Dictionary<string, object>.Enumerator enumerator = this.mCachedAssets.GetEnumerator();
      List<string> stringList = new List<string>();
      while (enumerator.MoveNext())
        stringList.Add(enumerator.Current.Key);
      return stringList.ToArray();
    }

    public override string GetModFileTag()
    {
      switch (this.mImageType)
      {
        case ModImageFileInfo.ImageType.Liveries:
          return ModImageFileInfo.imageTags[1];
        case ModImageFileInfo.ImageType.Portraits:
          return ModImageFileInfo.imageTags[0];
        case ModImageFileInfo.ImageType.LiveryPack:
          return ModImageFileInfo.imageTags[2];
        case ModImageFileInfo.ImageType.TeamLogos:
        case ModImageFileInfo.ImageType.SponsorLogos:
        case ModImageFileInfo.ImageType.ChampionshipLogos:
        case ModImageFileInfo.ImageType.MediaLogos:
          return ModImageFileInfo.imageTags[3];
        default:
          return (string) null;
      }
    }

    public enum ImageType
    {
      None,
      Liveries,
      Portraits,
      LiveryPack,
      TeamLogos,
      SponsorLogos,
      ChampionshipLogos,
      MediaLogos,
    }
  }
}
