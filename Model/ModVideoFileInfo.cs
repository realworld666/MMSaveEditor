// Decompiled with JetBrains decompiler
// Type: ModdingSystem.ModVideoFileInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;

namespace ModdingSystem
{
  public class ModVideoFileInfo : ModFileInfo
  {
    [NonSerialized]
    public static readonly string teamVideosName = "Teams";
    [NonSerialized]
    public static readonly string championshipVideosName = "Champ";
    [NonSerialized]
    public static readonly string backgroundVideosName = "Backgound";
    public static readonly string[] videoTags = new string[1]{ "Videos" };
    public static readonly string[] videoTagsIDs = new string[1]{ "PSG_10012022" };
    private ModVideoFileInfo.VideoType mVideoType = ModVideoFileInfo.VideoType.None;

    public override ModFileInfo.ModFileInfoType fileInfoType
    {
      get
      {
        return ModFileInfo.ModFileInfoType.Video;
      }
    }

    public ModVideoFileInfo.VideoType videoType
    {
      get
      {
        return this.mVideoType;
      }
      set
      {
        this.mVideoType = value;
      }
    }

    public override string GetModFileTag()
    {
      switch (this.mVideoType)
      {
        case ModVideoFileInfo.VideoType.Team:
        case ModVideoFileInfo.VideoType.Championship:
        case ModVideoFileInfo.VideoType.Background:
        case ModVideoFileInfo.VideoType.Track:
          return ModVideoFileInfo.videoTags[0];
        default:
          return (string) null;
      }
    }

    public bool IsReferringToVideo(string inVideoName)
    {
      return this.fileInfo.FullName.Replace('\\', '/').Contains(inVideoName);
    }

    public enum VideoType
    {
      Team,
      Championship,
      Background,
      Track,
      None,
    }
  }
}
