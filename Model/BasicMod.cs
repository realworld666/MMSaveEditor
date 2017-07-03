// Decompiled with JetBrains decompiler
// Type: ModdingSystem.BasicMod
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ModdingSystem
{
  public class BasicMod
  {
    public List<ModModelFileInfo> modModels = new List<ModModelFileInfo>();
    public List<ModDatabaseFileInfo> modDatabases = new List<ModDatabaseFileInfo>();
    public List<ModImageFileInfo> modImages = new List<ModImageFileInfo>();
    public List<ModLogoFileInfo> modLogos = new List<ModLogoFileInfo>();
    public List<ModVideoFileInfo> modVideos = new List<ModVideoFileInfo>();
    private List<ModFileInfo> mModFiles = new List<ModFileInfo>();
    protected const string mModelsFolderName = "Models";
    protected const string mDatabasesFolderName = "Databases";
    protected const string mLogosFolderName = "Logos";
    protected const string mImagesFolderName = "Images";
    protected const string mVideosFolderName = "Videos";
    protected const string mVehicleSubfolderName = "Vehicle";
    protected const string mHQsSubfolderName = "HQs";
    protected const string mTeamMoviesSubfolder = "TeamIntroMovies";
    protected const string mTrackMoviesSubfolder = "TrackMovies";
    protected const string mChampionshipMoviesSubfolder = "ChampionshipMovies";
    protected string mModFolder;

    public List<ModFileInfo> modFiles
    {
      get
      {
        return this.mModFiles;
      }
    }

    public virtual void Initialise()
    {
      this.ClearModFileInfos();
    }

    protected void ClearModFileInfos()
    {
      this.modModels.Clear();
      this.modDatabases.Clear();
      this.modImages.Clear();
      this.modLogos.Clear();
      this.modVideos.Clear();
      this.mModFiles.Clear();
    }

    public void UnloadCachedAssets()
    {
      bool flag = false;
      for (int index = 0; index < this.mModFiles.Count; ++index)
      {
        if (this.mModFiles[index].HasCachedAssets())
        {
          this.mModFiles[index].UnloadAssets();
          flag = true;
        }
      }
      if (!flag)
        return;
      Resources.UnloadUnusedAssets();
    }

    public virtual void LoadModFolderInfo()
    {
      DirectoryInfo directoryInfo = new DirectoryInfo(this.mModFolder);
      try
      {
        DirectoryInfo[] directories = directoryInfo.GetDirectories();
        for (int index = 0; index < directories.Length; ++index)
        {
          string name = directories[index].Name;
          if (name != null)
          {
            // ISSUE: reference to a compiler-generated field
            if (BasicMod.\u003C\u003Ef__switch\u0024map36 == null)
            {
              // ISSUE: reference to a compiler-generated field
              BasicMod.\u003C\u003Ef__switch\u0024map36 = new Dictionary<string, int>(5)
              {
                {
                  "Models",
                  0
                },
                {
                  "Databases",
                  1
                },
                {
                  "Logos",
                  2
                },
                {
                  "Images",
                  3
                },
                {
                  "Videos",
                  4
                }
              };
            }
            int num;
            // ISSUE: reference to a compiler-generated field
            if (BasicMod.\u003C\u003Ef__switch\u0024map36.TryGetValue(name, out num))
            {
              switch (num)
              {
                case 0:
                  this.LoadModelsInfo(directories[index]);
                  continue;
                case 1:
                  this.LoadDatabasesInfo(directories[index]);
                  continue;
                case 2:
                  this.LoadLogosInfo(directories[index]);
                  continue;
                case 3:
                  this.LoadImagesInfo(directories[index]);
                  continue;
                case 4:
                  this.LoadVideosInfo(directories[index]);
                  continue;
              }
            }
          }
          Debug.Log((object) ("ModdingSystem::BasicMod - There is no support for folder \"" + directories[index].FullName + "\""), (Object) null);
        }
      }
      catch (DirectoryNotFoundException ex)
      {
      }
    }

    private void LoadModelsInfo(DirectoryInfo inDirectory)
    {
      DirectoryInfo[] directories = inDirectory.GetDirectories();
      for (int index1 = 0; index1 < directories.Length; ++index1)
      {
        FileInfo[] files = directories[index1].GetFiles();
        for (int index2 = 0; index2 < files.Length; ++index2)
        {
          if (string.IsNullOrEmpty(files[index2].Extension))
          {
            ModModelFileInfo modModelFileInfo = new ModModelFileInfo();
            modModelFileInfo.LoadDataFromFileInfo(files[index2]);
            string name = directories[index1].Name;
            if (name != null)
            {
              // ISSUE: reference to a compiler-generated field
              if (BasicMod.\u003C\u003Ef__switch\u0024map37 == null)
              {
                // ISSUE: reference to a compiler-generated field
                BasicMod.\u003C\u003Ef__switch\u0024map37 = new Dictionary<string, int>(2)
                {
                  {
                    "Vehicle",
                    0
                  },
                  {
                    "HQs",
                    1
                  }
                };
              }
              int num;
              // ISSUE: reference to a compiler-generated field
              if (BasicMod.\u003C\u003Ef__switch\u0024map37.TryGetValue(name, out num))
              {
                if (num != 0)
                {
                  if (num == 1)
                    modModelFileInfo.modelType = ModModelFileInfo.ModelType.HQBuilding;
                }
                else
                  modModelFileInfo.modelType = ModModelFileInfo.ModelType.Vehicle;
              }
            }
            if (modModelFileInfo.modelType != ModModelFileInfo.ModelType.None)
            {
              this.modModels.Add(modModelFileInfo);
              this.mModFiles.Add((ModFileInfo) modModelFileInfo);
            }
          }
        }
      }
    }

    private void LoadDatabasesInfo(DirectoryInfo directory)
    {
      foreach (FileInfo file in directory.GetFiles("*.txt"))
      {
        ModDatabaseFileInfo databaseFileInfo = new ModDatabaseFileInfo();
        databaseFileInfo.LoadDataFromFileInfo(file);
        if (databaseFileInfo.databaseType != ModDatabaseFileInfo.DatabaseType.None)
        {
          this.modDatabases.Add(databaseFileInfo);
          this.mModFiles.Add((ModFileInfo) databaseFileInfo);
        }
      }
    }

    private void LoadLogosInfo(DirectoryInfo inDirectory)
    {
      FileInfo[] files = inDirectory.GetFiles();
      for (int index = 0; index < files.Length; ++index)
      {
        if (string.IsNullOrEmpty(files[index].Extension))
        {
          ModLogoFileInfo modLogoFileInfo = new ModLogoFileInfo();
          modLogoFileInfo.LoadDataFromFileInfo(files[index]);
          if (modLogoFileInfo.atlasType != AtlasManager.Atlas.Count)
          {
            this.modLogos.Add(modLogoFileInfo);
            this.mModFiles.Add((ModFileInfo) modLogoFileInfo);
          }
        }
      }
    }

    private void LoadImagesInfo(DirectoryInfo directory)
    {
      FileInfo[] files = directory.GetFiles();
      for (int index = 0; index < files.Length; ++index)
      {
        if (string.IsNullOrEmpty(files[index].Extension))
        {
          ModImageFileInfo modImageFileInfo = new ModImageFileInfo();
          modImageFileInfo.LoadDataFromFileInfo(files[index]);
          if (modImageFileInfo.imageType != ModImageFileInfo.ImageType.None)
          {
            this.modImages.Add(modImageFileInfo);
            this.mModFiles.Add((ModFileInfo) modImageFileInfo);
          }
        }
      }
    }

    private void LoadVideosInfo(DirectoryInfo inDirectory)
    {
      DirectoryInfo[] directories = inDirectory.GetDirectories();
      for (int index = 0; index < directories.Length; ++index)
      {
        foreach (FileInfo file in directories[index].GetFiles("*.bik"))
        {
          ModVideoFileInfo modVideoFileInfo = new ModVideoFileInfo();
          modVideoFileInfo.LoadDataFromFileInfo(file);
          if (directories[index].Name == "TeamIntroMovies")
            modVideoFileInfo.videoType = ModVideoFileInfo.VideoType.Team;
          else if (directories[index].Name == "TrackMovies")
            modVideoFileInfo.videoType = ModVideoFileInfo.VideoType.Track;
          else if (directories[index].Name == "ChampionshipMovies")
            modVideoFileInfo.videoType = ModVideoFileInfo.VideoType.Championship;
          if (modVideoFileInfo.videoType != ModVideoFileInfo.VideoType.None)
          {
            this.modVideos.Add(modVideoFileInfo);
            this.mModFiles.Add((ModFileInfo) modVideoFileInfo);
          }
        }
      }
      FileInfo[] files = inDirectory.GetFiles("*.bik");
      for (int index = 0; index < files.Length; ++index)
      {
        ModVideoFileInfo modVideoFileInfo = new ModVideoFileInfo();
        modVideoFileInfo.LoadDataFromFileInfo(files[index]);
        if (files[index].Name.Contains(ModVideoFileInfo.backgroundVideosName))
          modVideoFileInfo.videoType = ModVideoFileInfo.VideoType.Background;
        this.modVideos.Add(modVideoFileInfo);
        this.mModFiles.Add((ModFileInfo) modVideoFileInfo);
      }
    }

    public List<DatabaseEntry> ReadDatabaseOfType(ModDatabaseFileInfo.DatabaseType inDatabaseType)
    {
      for (int index = 0; index < this.modDatabases.Count; ++index)
      {
        if (this.modDatabases[index].databaseType == inDatabaseType)
          return this.modDatabases[index].ReadDatabase();
      }
      return (List<DatabaseEntry>) null;
    }

    public GameObject GetMesh(ModModelFileInfo.ModelType inModelType, int inChampioshipID, int inTeamIndex, int inBuildingLevel, string inModelName)
    {
      GameObject gameObject = (GameObject) null;
      for (int index = 0; index < this.modModels.Count; ++index)
      {
        if (this.modModels[index].modelType == inModelType)
        {
          switch (inModelType)
          {
            case ModModelFileInfo.ModelType.Vehicle:
              GameObject mesh1 = this.modModels[index].GetMesh(inModelName + ModModelFileInfo.teamModelSuffix + (object) inTeamIndex);
              if ((Object) mesh1 == (Object) null)
                mesh1 = this.modModels[index].GetMesh(inModelName + ModModelFileInfo.championshipSuffix + (object) inChampioshipID);
              if ((Object) mesh1 == (Object) null)
                mesh1 = this.modModels[index].GetMesh(inModelName);
              if ((Object) mesh1 != (Object) null)
              {
                gameObject = mesh1;
                continue;
              }
              continue;
            case ModModelFileInfo.ModelType.HQBuilding:
              GameObject mesh2 = this.modModels[index].GetMesh(inModelName + ModModelFileInfo.levelModelSuffix + (object) inBuildingLevel);
              if ((Object) mesh2 == (Object) null)
                mesh2 = this.modModels[index].GetMesh(inModelName);
              if ((Object) mesh2 != (Object) null)
              {
                gameObject = mesh2;
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
      return gameObject;
    }

    public Texture2D GetAtlasTexture(AtlasManager.Atlas inAtlasType)
    {
      Texture2D texture2D = (Texture2D) null;
      for (int index = 0; index < this.modLogos.Count; ++index)
      {
        Texture2D atlasTexture = this.modLogos[index].GetAtlasTexture(inAtlasType);
        if ((Object) atlasTexture != (Object) null)
          texture2D = atlasTexture;
      }
      return texture2D;
    }

    public Texture GetTextureImage(ModImageFileInfo.ImageType inImageType, string inImageName, string inAssetBundleName, int inID)
    {
      Texture texture1 = (Texture) null;
      for (int index = 0; index < this.modImages.Count; ++index)
      {
        if (this.modImages[index].imageType == inImageType)
        {
          switch (inImageType)
          {
            case ModImageFileInfo.ImageType.Liveries:
              if (inID != -1)
              {
                string inTextureName = inImageName + "_" + (object) (inID - ModImageFileInfo.liveryIdOffset);
                Texture texture2 = this.modImages[index].GetTexture(inTextureName);
                if ((Object) texture2 != (Object) null)
                {
                  texture1 = texture2;
                  continue;
                }
                continue;
              }
              continue;
            case ModImageFileInfo.ImageType.Portraits:
              if (inImageName.ToLowerInvariant() == ModImageFileInfo.playerPortraitName.ToLowerInvariant() || inImageName.ToLowerInvariant() == ModImageFileInfo.presidentPortraitName.ToLowerInvariant())
              {
                Texture texture2 = this.modImages[index].GetTexture(inImageName);
                if ((Object) texture2 != (Object) null)
                {
                  texture1 = texture2;
                  continue;
                }
                continue;
              }
              if (inID != -1)
              {
                string inTextureName = inImageName + "_" + (object) inID;
                Texture texture2 = this.modImages[index].GetTexture(inTextureName);
                if ((Object) texture2 != (Object) null)
                {
                  texture1 = texture2;
                  continue;
                }
                Texture texture3 = this.modImages[index].GetTexture(inImageName);
                if ((Object) texture3 != (Object) null)
                {
                  texture1 = texture3;
                  continue;
                }
                continue;
              }
              Texture texture4 = this.modImages[index].GetTexture(inImageName);
              if ((Object) texture4 != (Object) null)
              {
                texture1 = texture4;
                continue;
              }
              continue;
            case ModImageFileInfo.ImageType.LiveryPack:
              if (this.modImages[index].fileInfo.FullName == inAssetBundleName)
              {
                Texture texture2 = this.modImages[index].GetTexture(inImageName);
                if ((Object) texture2 != (Object) null)
                {
                  texture1 = texture2;
                  continue;
                }
                continue;
              }
              continue;
            case ModImageFileInfo.ImageType.TeamLogos:
            case ModImageFileInfo.ImageType.SponsorLogos:
            case ModImageFileInfo.ImageType.ChampionshipLogos:
            case ModImageFileInfo.ImageType.MediaLogos:
              string inTextureName1 = inImageName + "_" + (object) inID;
              Texture texture5 = this.modImages[index].GetTexture(inTextureName1);
              if ((Object) texture5 != (Object) null)
              {
                texture1 = texture5;
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
      return texture1;
    }

    public string GetMovieFilePath(string inVideoName)
    {
      string str = (string) null;
      for (int index = 0; index < this.modVideos.Count; ++index)
      {
        if (this.modVideos[index].IsReferringToVideo(inVideoName))
          str = this.modVideos[index].fileInfo.FullName;
      }
      return str;
    }

    public List<LiveryData> GetLiveryPack()
    {
      List<LiveryData> liveryDataList = new List<LiveryData>();
      for (int index1 = 0; index1 < this.modImages.Count; ++index1)
      {
        if (this.modImages[index1].imageType == ModImageFileInfo.ImageType.LiveryPack)
        {
          int num1 = this.modImages[index1].GetTextureNames().Length / 2;
          for (int index2 = 0; index2 < num1; ++index2)
          {
            string inAssetName1 = ModImageFileInfo.liveryBaseName + "_" + (object) (index2 + 1);
            string inAssetName2 = ModImageFileInfo.liveryDetailName + "_" + (object) (index2 + 1);
            bool flag = false;
            int[] numArray = new int[5]{ 0, 1, 2, 3, 4 };
            int num2 = -1;
            for (int index3 = 0; index3 < numArray.Length; ++index3)
            {
              if (this.modImages[index1].ContainsAsset(inAssetName1 + ModImageFileInfo.championshipSuffix + (object) numArray[index3]) && this.modImages[index1].ContainsAsset(inAssetName2 + ModImageFileInfo.championshipSuffix + (object) numArray[index3]))
              {
                flag = true;
                num2 = index3;
                break;
              }
            }
            if (!flag)
              flag = this.modImages[index1].ContainsAsset(inAssetName1) && this.modImages[index1].ContainsAsset(inAssetName2);
            if (flag)
            {
              LiveryData liveryData = LiveryData.defaultLivery.Clone();
              liveryData.chassis.customAssetBundleName = this.modImages[index1].fileInfo.FullName;
              if (num2 == -1)
              {
                liveryData.chassis.detailLiveryTexture = inAssetName2;
                liveryData.chassis.baseLiveryTexture = inAssetName1;
                liveryData.championshipID = numArray;
              }
              else
              {
                liveryData.chassis.detailLiveryTexture = inAssetName2 + ModImageFileInfo.championshipSuffix + (object) num2;
                liveryData.chassis.baseLiveryTexture = inAssetName1 + ModImageFileInfo.championshipSuffix + (object) num2;
                liveryData.championshipID = new int[1]
                {
                  num2
                };
              }
              liveryDataList.Add(liveryData);
            }
          }
        }
      }
      return liveryDataList;
    }

    public bool IsModdingRaceCar(int inChampionshipID, int inTeamIndex)
    {
      for (int index = 0; index < this.modModels.Count; ++index)
      {
        if (this.modModels[index].modelType == ModModelFileInfo.ModelType.Vehicle && this.modModels[index].ContainsInRaceSpecificModel(inChampionshipID, inTeamIndex))
          return true;
      }
      return false;
    }

    public bool IsModdingFrontendCarModel(string inPartName, int inChampionshipID, int inTeamIndex)
    {
      for (int index = 0; index < this.modModels.Count; ++index)
      {
        if (this.modModels[index].modelType == ModModelFileInfo.ModelType.Vehicle && this.modModels[index].ContainsFrontedCarModel(inPartName, inChampionshipID, inTeamIndex))
          return true;
      }
      return false;
    }

    public bool IsAssetLoadRequired()
    {
      return this.modDatabases.Count > 0 || this.modImages.Count > 0 || (this.modLogos.Count > 0 || this.modVideos.Count > 0);
    }
  }
}
