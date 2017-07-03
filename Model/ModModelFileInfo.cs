// Decompiled with JetBrains decompiler
// Type: ModdingSystem.ModModelFileInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;
using UnityEngine;

namespace ModdingSystem
{
  public class ModModelFileInfo : ModFileInfo
  {
    [NonSerialized]
    public static readonly string chassisModelName = "Chassis";
    [NonSerialized]
    public static readonly string wheelModelName = "Wheel";
    [NonSerialized]
    public static readonly string brakeModelName = "Brake";
    [NonSerialized]
    public static readonly string frontWingName = "FrontWing";
    [NonSerialized]
    public static readonly string rearWingName = "RearWing";
    [NonSerialized]
    public static readonly string seatModelName = "Seat";
    [NonSerialized]
    public static readonly string steeringWheelModelName = "SteeringWheel";
    [NonSerialized]
    public static readonly string raceSimulationSuffix = "_RaceSim";
    [NonSerialized]
    public static readonly string championshipSuffix = "_Championship";
    [NonSerialized]
    public static readonly string buildingUpgradesSuffix = "_Upgrade";
    [NonSerialized]
    public static readonly string levelModelSuffix = "_Level";
    [NonSerialized]
    public static readonly string teamModelSuffix = "_Team";
    public static readonly string[] modelTags = new string[2]{ "Headquarter Buildings", "Cars" };
    public static readonly string[] modelTagsIDs = new string[2]{ "PSG_10002250", "PSG_10001619" };
    private ModModelFileInfo.ModelType mModelType;

    public override ModFileInfo.ModFileInfoType fileInfoType
    {
      get
      {
        return ModFileInfo.ModFileInfoType.Model;
      }
    }

    public ModModelFileInfo.ModelType modelType
    {
      get
      {
        return this.mModelType;
      }
      set
      {
        this.mModelType = value;
      }
    }

    public override void LoadDataFromFileInfo(FileInfo inFileInfo)
    {
      base.LoadDataFromFileInfo(inFileInfo);
      this.CacheAssets();
    }

    public override void LoadMetadata(ModMetadata inModMetadata)
    {
      string metadataForFile = inModMetadata.GetMetadataForFile(this.fileInfo.Name);
      if (metadataForFile == null)
        return;
      if (Enum.IsDefined(typeof (ModModelFileInfo.ModelType), (object) metadataForFile))
        this.mModelType = (ModModelFileInfo.ModelType) Enum.Parse(typeof (ModModelFileInfo.ModelType), metadataForFile);
      else
        this.mModelType = ModModelFileInfo.ModelType.None;
    }

    public override string GetModFileTag()
    {
      switch (this.modelType)
      {
        case ModModelFileInfo.ModelType.Vehicle:
          return ModModelFileInfo.modelTags[1];
        case ModModelFileInfo.ModelType.HQBuilding:
          return ModModelFileInfo.modelTags[0];
        default:
          return (string) null;
      }
    }

    public GameObject GetMesh(string inModelName)
    {
      return this.GetAsset<GameObject>(inModelName);
    }

    public bool ContainsInRaceSpecificModel(int inChampionshipID, int inTeamIndex)
    {
      bool flag = false;
      string str1 = ModModelFileInfo.teamModelSuffix + (object) inTeamIndex;
      string str2 = ModModelFileInfo.championshipSuffix + (object) inChampionshipID;
      return flag | this.mCachedAssets.ContainsKey(ModModelFileInfo.rearWingName + ModModelFileInfo.raceSimulationSuffix) | this.mCachedAssets.ContainsKey(ModModelFileInfo.frontWingName + ModModelFileInfo.raceSimulationSuffix) | this.mCachedAssets.ContainsKey(ModModelFileInfo.wheelModelName + ModModelFileInfo.raceSimulationSuffix) | this.mCachedAssets.ContainsKey(ModModelFileInfo.chassisModelName + ModModelFileInfo.raceSimulationSuffix) | this.mCachedAssets.ContainsKey(ModModelFileInfo.rearWingName + ModModelFileInfo.raceSimulationSuffix + str1) | this.mCachedAssets.ContainsKey(ModModelFileInfo.frontWingName + ModModelFileInfo.raceSimulationSuffix + str1) | this.mCachedAssets.ContainsKey(ModModelFileInfo.wheelModelName + ModModelFileInfo.raceSimulationSuffix + str1) | this.mCachedAssets.ContainsKey(ModModelFileInfo.chassisModelName + ModModelFileInfo.raceSimulationSuffix + str1) | this.mCachedAssets.ContainsKey(ModModelFileInfo.rearWingName + ModModelFileInfo.raceSimulationSuffix + str2) | this.mCachedAssets.ContainsKey(ModModelFileInfo.frontWingName + ModModelFileInfo.raceSimulationSuffix + str2) | this.mCachedAssets.ContainsKey(ModModelFileInfo.wheelModelName + ModModelFileInfo.raceSimulationSuffix + str2) | this.mCachedAssets.ContainsKey(ModModelFileInfo.chassisModelName + ModModelFileInfo.raceSimulationSuffix + str2);
    }

    public bool ContainsFrontedCarModel(string inPartName, int inChampionshipID, int inTeamIndex)
    {
      bool flag = false;
      string str1 = inTeamIndex != -1 ? ModModelFileInfo.teamModelSuffix + (object) inTeamIndex : string.Empty;
      string str2 = ModModelFileInfo.championshipSuffix + (object) inChampionshipID;
      return flag | this.mCachedAssets.ContainsKey(inPartName) | this.mCachedAssets.ContainsKey(inPartName + str1) | this.mCachedAssets.ContainsKey(inPartName + str2);
    }

    public enum ModelType
    {
      None,
      Vehicle,
      HQBuilding,
    }
  }
}
