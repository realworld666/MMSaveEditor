// Decompiled with JetBrains decompiler
// Type: ModdingSystem.ModLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace ModdingSystem
{
  public class ModLoader
  {
    private int mLastTeamModded = -1;
    private int mLastSeriesTypeID = -1;
    private GameObject mDefaultCarChassis;
    private GameObject mDefaultCarFrontWing;
    private GameObject mDefaultCarRearWing;
    private GameObject mDefaultCarWheel;
    private GameObject mRaceModCarCache;
    private ActiveModsController mActiveModsController;
    private bool mAllowFrontendCarModding;

    public ActiveModsController activeModsController
    {
      get
      {
        return this.mActiveModsController;
      }
    }

    public bool allowFrontendCarModding
    {
      get
      {
        return this.mAllowFrontendCarModding;
      }
      set
      {
        this.mAllowFrontendCarModding = value;
      }
    }

    public ModLoader()
    {
      this.mDefaultCarChassis = Resources.Load("Prefabs/Simulation/WGP1/Chassis") as GameObject;
      this.mDefaultCarFrontWing = Resources.Load("Prefabs/Simulation/WGP1/FrontWing") as GameObject;
      this.mDefaultCarRearWing = Resources.Load("Prefabs/Simulation/WGP1/RearWing") as GameObject;
      this.mDefaultCarWheel = Resources.Load("Prefabs/Simulation/WGP1/Wheel") as GameObject;
      this.mActiveModsController = new ActiveModsController(App.instance.modManager);
    }

    public void LoadAssetsAndDatabasesWithMods()
    {
      App.instance.database.ReadDatabaseMods();
    }

    public void ReloadAtlases()
    {
      App.instance.atlasManager.UpdateAtlasesWithMods();
    }

    public bool ReadDatabase(ModDatabaseFileInfo.DatabaseType inDatabaseType, out List<DatabaseEntry> inDatabase)
    {
      inDatabase = (List<DatabaseEntry>) null;
      if (!SteamManager.Initialized)
        return false;
      ModManager modManager = App.instance.modManager;
      if (modManager.modPublisher.isStaging)
      {
        inDatabase = modManager.modPublisher.stagingMod.ReadDatabaseOfType(inDatabaseType);
      }
      else
      {
        List<SteamMod> userSubscribedMods = modManager.allUserSubscribedMods;
        for (int index = 0; index < userSubscribedMods.Count; ++index)
        {
          if (userSubscribedMods[index].isActive && userSubscribedMods[index].modDatabases.Count > 0)
            inDatabase = userSubscribedMods[index].ReadDatabaseOfType(inDatabaseType);
        }
      }
      return inDatabase != null;
    }

    public bool GetMesh(ModModelFileInfo.ModelType inModelType, int inChampionshipID, int inTeamIndex, int inBuildingLevel, string inModelName, out GameObject inMesh)
    {
      inMesh = (GameObject) null;
      if (!SteamManager.Initialized)
        return false;
      ModManager modManager = App.instance.modManager;
      if (modManager.modPublisher.isStaging)
      {
        inMesh = modManager.modPublisher.stagingMod.GetMesh(inModelType, inChampionshipID, inTeamIndex, inBuildingLevel, inModelName);
      }
      else
      {
        List<SteamMod> userSubscribedMods = modManager.allUserSubscribedMods;
        for (int index = 0; index < userSubscribedMods.Count; ++index)
        {
          SteamMod steamMod = userSubscribedMods[index];
          if (steamMod.isActive && steamMod.modModels.Count > 0)
          {
            GameObject mesh = userSubscribedMods[index].GetMesh(inModelType, inChampionshipID, inTeamIndex, inBuildingLevel, inModelName);
            if ((Object) mesh != (Object) null)
              inMesh = mesh;
          }
        }
      }
      return (Object) inMesh != (Object) null;
    }

    public bool GetAtlasTexture(AtlasManager.Atlas inAtlasType, out Texture2D inTexture)
    {
      inTexture = (Texture2D) null;
      if (!SteamManager.Initialized)
        return false;
      ModManager modManager = App.instance.modManager;
      if (modManager.modPublisher.isStaging)
      {
        inTexture = modManager.modPublisher.stagingMod.GetAtlasTexture(inAtlasType);
      }
      else
      {
        List<SteamMod> userSubscribedMods = modManager.allUserSubscribedMods;
        for (int index = 0; index < userSubscribedMods.Count; ++index)
        {
          SteamMod steamMod = userSubscribedMods[index];
          if (steamMod.isActive && steamMod.modLogos.Count > 0)
            inTexture = steamMod.GetAtlasTexture(inAtlasType);
        }
      }
      return (Object) inTexture != (Object) null;
    }

    public bool GetTextureImage(ModImageFileInfo.ImageType inImageType, string inImageName, string inAssetBundleName, int inID, out Texture inTexture)
    {
      inTexture = (Texture) null;
      if (!SteamManager.Initialized || inImageType == ModImageFileInfo.ImageType.Liveries && !this.mAllowFrontendCarModding)
        return false;
      ModManager modManager = App.instance.modManager;
      if (modManager.modPublisher.isStaging)
      {
        inTexture = modManager.modPublisher.stagingMod.GetTextureImage(inImageType, inImageName, inAssetBundleName, inID);
      }
      else
      {
        List<SteamMod> userSubscribedMods = modManager.allUserSubscribedMods;
        for (int index = 0; index < userSubscribedMods.Count; ++index)
        {
          SteamMod steamMod = userSubscribedMods[index];
          if (steamMod.isActive && steamMod.modImages.Count > 0)
          {
            Texture textureImage = steamMod.GetTextureImage(inImageType, inImageName, inAssetBundleName, inID);
            if ((Object) textureImage != (Object) null)
              inTexture = textureImage;
          }
        }
      }
      return (Object) inTexture != (Object) null;
    }

    public bool GetMovieFullFilePath(string inVideoName, out string inMovieFullFilePath)
    {
      inMovieFullFilePath = (string) null;
      if (!SteamManager.Initialized)
        return false;
      ModManager modManager = App.instance.modManager;
      if (modManager.modPublisher.isStaging)
      {
        inMovieFullFilePath = modManager.modPublisher.stagingMod.GetMovieFilePath(inVideoName);
      }
      else
      {
        List<SteamMod> userSubscribedMods = modManager.allUserSubscribedMods;
        for (int index = 0; index < userSubscribedMods.Count; ++index)
        {
          SteamMod steamMod = userSubscribedMods[index];
          if (steamMod.isActive && steamMod.modVideos.Count > 0)
            inMovieFullFilePath = steamMod.GetMovieFilePath(inVideoName);
        }
      }
      return inMovieFullFilePath != null;
    }

    public bool GetModLiveryPack(out List<LiveryData> inLiveryPack)
    {
      inLiveryPack = new List<LiveryData>();
      if (!SteamManager.Initialized)
        return false;
      ModManager modManager = App.instance.modManager;
      if (modManager.modPublisher.isStaging)
      {
        inLiveryPack = modManager.modPublisher.stagingMod.GetLiveryPack();
      }
      else
      {
        List<SteamMod> userSubscribedMods = modManager.allUserSubscribedMods;
        for (int index = 0; index < userSubscribedMods.Count; ++index)
        {
          SteamMod steamMod = userSubscribedMods[index];
          if (steamMod.isActive && steamMod.modImages.Count > 0)
          {
            List<LiveryData> liveryPack = steamMod.GetLiveryPack();
            if (liveryPack.Count > 0)
              inLiveryPack.AddRange((IEnumerable<LiveryData>) liveryPack);
          }
        }
      }
      return inLiveryPack != null && inLiveryPack.Count > 0;
    }

    public void ResetRaceModCar()
    {
      Object.Destroy((Object) this.mRaceModCarCache);
      this.mRaceModCarCache = (GameObject) null;
    }

    public void ResetRaceModCarVariables()
    {
      this.mLastTeamModded = -1;
      this.mLastSeriesTypeID = -1;
    }

    public bool GetRaceModCar(int inChampionshipID, int inTeamIndex, out GameObject inRaceModCar)
    {
      inRaceModCar = (GameObject) null;
      if (!SteamManager.Initialized)
        return false;
      ModManager modManager = App.instance.modManager;
      bool flag = false;
      if (modManager.modPublisher.isStaging)
      {
        flag = modManager.modPublisher.stagingMod.IsModdingRaceCar(inChampionshipID, inTeamIndex);
      }
      else
      {
        List<SteamMod> userSubscribedMods = modManager.allUserSubscribedMods;
        for (int index = 0; index < userSubscribedMods.Count; ++index)
        {
          if (userSubscribedMods[index].isActive)
            flag |= userSubscribedMods[index].IsModdingRaceCar(inChampionshipID, inTeamIndex);
        }
      }
      if (flag)
      {
        if (this.mLastTeamModded != inTeamIndex || this.mLastSeriesTypeID != inChampionshipID)
        {
          this.ResetRaceModCar();
          this.mLastTeamModded = inTeamIndex;
          this.mLastSeriesTypeID = inChampionshipID;
        }
        if ((Object) this.mRaceModCarCache == (Object) null)
        {
          this.mRaceModCarCache = this.BuildRaceModCar(inChampionshipID, inTeamIndex);
          GameUtility.SetActive(this.mRaceModCarCache, false);
        }
        inRaceModCar = this.mRaceModCarCache;
      }
      return (Object) inRaceModCar != (Object) null;
    }

    private GameObject BuildRaceModCar(int inChampionshipID, int inTeamIndex)
    {
      GameObject inMesh1 = (GameObject) null;
      GameObject gameObject = !this.GetMesh(ModModelFileInfo.ModelType.Vehicle, inChampionshipID, inTeamIndex, -1, string.Format("{0}{1}", (object) ModModelFileInfo.chassisModelName, (object) ModModelFileInfo.raceSimulationSuffix), out inMesh1) ? Object.Instantiate<GameObject>(this.mDefaultCarChassis) : Object.Instantiate<GameObject>(inMesh1);
      Transform inAllDescendents1 = GameUtility.FindInAllDescendents(gameObject.transform, FrontendCar.frontWingSocketName);
      if ((Object) inAllDescendents1 != (Object) null)
      {
        GameObject inMesh2 = (GameObject) null;
        (!this.GetMesh(ModModelFileInfo.ModelType.Vehicle, inChampionshipID, inTeamIndex, -1, string.Format("{0}{1}", (object) ModModelFileInfo.frontWingName, (object) ModModelFileInfo.raceSimulationSuffix), out inMesh2) ? Object.Instantiate<GameObject>(this.mDefaultCarFrontWing) : Object.Instantiate<GameObject>(inMesh2)).transform.SetParent(inAllDescendents1, false);
      }
      Transform inAllDescendents2 = GameUtility.FindInAllDescendents(gameObject.transform, FrontendCar.rearWingSocketName);
      if ((Object) inAllDescendents2 != (Object) null)
      {
        GameObject inMesh2 = (GameObject) null;
        (!this.GetMesh(ModModelFileInfo.ModelType.Vehicle, inChampionshipID, inTeamIndex, -1, string.Format("{0}{1}", (object) ModModelFileInfo.rearWingName, (object) ModModelFileInfo.raceSimulationSuffix), out inMesh2) ? Object.Instantiate<GameObject>(this.mDefaultCarRearWing) : Object.Instantiate<GameObject>(inMesh2)).transform.SetParent(inAllDescendents2, false);
      }
      for (int index = 0; index < 4; ++index)
      {
        Transform inAllDescendents3 = GameUtility.FindInAllDescendents(gameObject.transform, FrontendCar.wheelSocketNames[index]);
        if ((Object) inAllDescendents3 != (Object) null)
        {
          GameObject inMesh2 = (GameObject) null;
          (!this.GetMesh(ModModelFileInfo.ModelType.Vehicle, inChampionshipID, inTeamIndex, -1, string.Format("{0}{1}", (object) ModModelFileInfo.wheelModelName, (object) ModModelFileInfo.raceSimulationSuffix), out inMesh2) ? Object.Instantiate<GameObject>(this.mDefaultCarWheel) : Object.Instantiate<GameObject>(inMesh2)).transform.SetParent(inAllDescendents3, false);
        }
      }
      return gameObject;
    }

    public bool IsModdingFrontendCarModel(string inPartName, int championshipID, int inTeamID)
    {
      if (!SteamManager.Initialized || !this.mAllowFrontendCarModding)
        return false;
      ModManager modManager = App.instance.modManager;
      bool flag = false;
      if (modManager.modPublisher.isStaging)
      {
        flag = modManager.modPublisher.stagingMod.IsModdingFrontendCarModel(inPartName, championshipID, inTeamID);
      }
      else
      {
        List<SteamMod> userSubscribedMods = modManager.allUserSubscribedMods;
        for (int index = 0; index < userSubscribedMods.Count; ++index)
        {
          if (userSubscribedMods[index].isActive)
            flag |= userSubscribedMods[index].IsModdingFrontendCarModel(inPartName, championshipID, inTeamID);
        }
      }
      return flag;
    }
  }
}
