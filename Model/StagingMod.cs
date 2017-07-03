// Decompiled with JetBrains decompiler
// Type: ModdingSystem.StagingMod
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ModdingSystem
{
  public class StagingMod : BasicMod
  {
    public static readonly string newGameRequiredTag = "New Game Mod";
    public static readonly string mModStagingRoot = "Modding";
    public static readonly string imagePath = "Preview.png";
    private bool mIsStaging;
    private bool mContainsFiles;
    private FileInfo mPreviewFileInfo;

    public string modStagingFolder
    {
      get
      {
        return Path.Combine(Application.dataPath, StagingMod.mModStagingRoot);
      }
    }

    public bool isStaging
    {
      get
      {
        return this.mIsStaging;
      }
      set
      {
        this.mIsStaging = value;
      }
    }

    public bool containsFiles
    {
      get
      {
        return this.mContainsFiles;
      }
    }

    public override void Initialise()
    {
      this.CreateFoldersIfNeeded();
      base.Initialise();
      this.mModFolder = this.modStagingFolder;
      this.LoadModFolderInfo();
      this.UpdateModStagingStatus();
    }

    private void CreateFoldersIfNeeded()
    {
      if (!Directory.Exists(this.modStagingFolder))
        Directory.CreateDirectory(this.modStagingFolder);
      string str1 = Path.Combine(this.modStagingFolder, "Models");
      if (!Directory.Exists(str1))
        Directory.CreateDirectory(str1);
      string path1 = Path.Combine(str1, "Vehicle");
      if (!Directory.Exists(path1))
        Directory.CreateDirectory(path1);
      string path2 = Path.Combine(str1, "HQs");
      if (!Directory.Exists(path2))
        Directory.CreateDirectory(path2);
      string path3 = Path.Combine(this.modStagingFolder, "Databases");
      if (!Directory.Exists(path3))
        Directory.CreateDirectory(path3);
      string path4 = Path.Combine(this.modStagingFolder, "Logos");
      if (!Directory.Exists(path4))
        Directory.CreateDirectory(path4);
      string path5 = Path.Combine(this.modStagingFolder, "Images");
      if (!Directory.Exists(path5))
        Directory.CreateDirectory(path5);
      string str2 = Path.Combine(this.modStagingFolder, "Videos");
      if (!Directory.Exists(str2))
        Directory.CreateDirectory(str2);
      string path6 = Path.Combine(str2, "TeamIntroMovies");
      if (!Directory.Exists(path6))
        Directory.CreateDirectory(path6);
      string path7 = Path.Combine(str2, "TrackMovies");
      if (!Directory.Exists(path7))
        Directory.CreateDirectory(path7);
      string path8 = Path.Combine(str2, "ChampionshipMovies");
      if (Directory.Exists(path8))
        return;
      Directory.CreateDirectory(path8);
    }

    public void UpdateModStagingStatus()
    {
      if (Directory.Exists(this.mModFolder))
      {
        DirectoryInfo[] directories = new DirectoryInfo(this.mModFolder).GetDirectories();
        if (directories.Length > 0)
        {
          for (int index = 0; index < directories.Length; ++index)
          {
            if (directories[index].Name == "Databases" || directories[index].Name == "Logos" || directories[index].Name == "Images")
            {
              if (directories[index].GetFiles().Length > 0)
              {
                this.mContainsFiles = true;
                return;
              }
            }
            else
            {
              foreach (DirectoryInfo directory in directories[index].GetDirectories())
              {
                if (directory.GetFiles().Length > 0)
                {
                  this.mContainsFiles = true;
                  return;
                }
              }
            }
          }
        }
      }
      this.mContainsFiles = false;
    }

    public override void LoadModFolderInfo()
    {
      this.ClearModFileInfos();
      string str = Path.Combine(this.mModFolder, StagingMod.imagePath).Replace("\\", "/");
      this.mPreviewFileInfo = !File.Exists(str) ? (FileInfo) null : new FileInfo(str);
      base.LoadModFolderInfo();
    }

    public Texture2D GetPreviewImage()
    {
      Texture2D texture2D = (Texture2D) null;
      if (this.mPreviewFileInfo != null)
      {
        byte[] data = File.ReadAllBytes(this.mPreviewFileInfo.FullName);
        texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(data);
      }
      return texture2D;
    }

    public List<string> GetTags()
    {
      List<string> stringList = new List<string>();
      for (int index = 0; index < this.modFiles.Count; ++index)
      {
        string modFileTag = this.modFiles[index].GetModFileTag();
        if (!string.IsNullOrEmpty(modFileTag) && !stringList.Contains(modFileTag))
          stringList.Add(modFileTag);
      }
      if (this.modDatabases.Count > 0)
        stringList.Add(StagingMod.newGameRequiredTag);
      return stringList;
    }

    public string GetPreviewImagePath()
    {
      string str = (string) null;
      if (this.mPreviewFileInfo != null && this.IsPreviewImageCorrectSize())
        str = this.mPreviewFileInfo.FullName;
      return str;
    }

    public bool IsPreviewImageCorrectSize()
    {
      if (this.mPreviewFileInfo != null)
        return this.mPreviewFileInfo.Length <= 1024000L;
      return true;
    }
  }
}
