// Decompiled with JetBrains decompiler
// Type: PartsDatabase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class PartsDatabase
{
  public static void SetCarPartsFromDatabase(Database database)
  {
    List<DatabaseEntry> carPartsData = database.carPartsData;
    TeamManager teamManager = Game.instance.teamManager;
    for (int index1 = 0; index1 < carPartsData.Count; ++index1)
    {
      DatabaseEntry inData = carPartsData[index1];
      string[] strArray = inData.GetStringValue("Team").Split(';');
      for (int index2 = 0; index2 < strArray.Length; ++index2)
      {
        strArray[index2] = strArray[index2].Trim();
        if (!(strArray[index2] == string.Empty))
        {
          Team entity = teamManager.GetEntity(int.Parse(strArray[index2]) - 2);
          CarManager carManager = entity.carManager;
          CarPart.PartType[] partType1 = CarPart.GetPartType(entity.championship.series, false);
          CarPart.PartType partType2 = PartsDatabase.GetPartType(inData.GetStringValue("Part Type"));
          bool flag = true;
          for (int index3 = 0; index3 < partType1.Length; ++index3)
          {
            if (partType1[index3] == partType2)
              flag = false;
          }
          if (!flag && !entity.championship.rules.specParts.Contains(partType2))
            PartsDatabase.LoadPartData(carManager, partType2, entity.championship, inData);
        }
      }
    }
    for (int inIndex = 0; inIndex < teamManager.count; ++inIndex)
    {
      Team entity = teamManager.GetEntity(inIndex);
      entity.carManager.AutoFit(entity.carManager.GetCar(0), CarManager.AutofitOptions.Performance, CarManager.AutofitAvailabilityOption.AllParts);
      entity.carManager.AutoFit(entity.carManager.GetCar(1), CarManager.AutofitOptions.Performance, CarManager.AutofitAvailabilityOption.UnfitedParts);
      entity.carManager.carPartDesign.SetSeasonStartingStats();
    }
  }

  private static void LoadPartData(CarManager inCarManager, CarPart.PartType inType, Championship inChampionship, DatabaseEntry inData)
  {
    CarPart partEntity = CarPart.CreatePartEntity(inType, inCarManager.team.championship);
    float inValue = 0.0f;
    switch (partEntity.stats.statType)
    {
      case CarStats.StatType.TopSpeed:
        inValue = inData.GetFloatValue("TS");
        break;
      case CarStats.StatType.Acceleration:
        inValue = inData.GetFloatValue("ACC");
        break;
      case CarStats.StatType.Braking:
        inValue = inData.GetFloatValue("DEC");
        break;
      case CarStats.StatType.LowSpeedCorners:
        inValue = inData.GetFloatValue("LSC");
        break;
      case CarStats.StatType.MediumSpeedCorners:
        inValue = inData.GetFloatValue("MSC");
        break;
      case CarStats.StatType.HighSpeedCorners:
        inValue = inData.GetFloatValue("HSC");
        break;
    }
    partEntity.stats.SetStat(CarPartStats.CarPartStat.MainStat, inValue);
    partEntity.stats.SetStat(CarPartStats.CarPartStat.Reliability, inData.GetFloatValue("Reliability") / 100f);
    partEntity.stats.partCondition.SetCondition(inData.GetFloatValue("Condition") / 100f);
    partEntity.stats.partCondition.redZone = GameStatsConstants.initialRedZone;
    partEntity.stats.maxReliability = Mathf.Max(GameStatsConstants.initialMaxReliabilityValue, partEntity.stats.partCondition.condition);
    partEntity.stats.maxPerformance = (float) (inData.GetIntValue("Performance Improvability") * 3);
    partEntity.buildDate = Game.instance.time.now.AddDays((double) -inData.GetIntValue("Age"));
    partEntity.PostStatsSetup(inChampionship);
    inCarManager.partInventory.AddPart(partEntity);
  }

  public static CarPart.PartType GetPartType(string inString)
  {
    string key = inString;
    if (key != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (PartsDatabase.\u003C\u003Ef__switch\u0024mapF == null)
      {
        // ISSUE: reference to a compiler-generated field
        PartsDatabase.\u003C\u003Ef__switch\u0024mapF = new Dictionary<string, int>(11)
        {
          {
            "Engine",
            0
          },
          {
            "Gearbox",
            1
          },
          {
            "Brakes",
            2
          },
          {
            "Suspension",
            3
          },
          {
            "Front Wing",
            4
          },
          {
            "Rear Wing",
            5
          },
          {
            "Rear Wing GT",
            6
          },
          {
            "Engine GT",
            7
          },
          {
            "Brakes GT",
            8
          },
          {
            "Gearbox GT",
            9
          },
          {
            "Suspension GT",
            10
          }
        };
      }
      int num;
      // ISSUE: reference to a compiler-generated field
      if (PartsDatabase.\u003C\u003Ef__switch\u0024mapF.TryGetValue(key, out num))
      {
        switch (num)
        {
          case 0:
            return CarPart.PartType.Engine;
          case 1:
            return CarPart.PartType.Gearbox;
          case 2:
            return CarPart.PartType.Brakes;
          case 3:
            return CarPart.PartType.Suspension;
          case 4:
            return CarPart.PartType.FrontWing;
          case 5:
            return CarPart.PartType.RearWing;
          case 6:
            return CarPart.PartType.RearWingGT;
          case 7:
            return CarPart.PartType.EngineGT;
          case 8:
            return CarPart.PartType.BrakesGT;
          case 9:
            return CarPart.PartType.GearboxGT;
          case 10:
            return CarPart.PartType.SuspensionGT;
        }
      }
    }
    return CarPart.PartType.None;
  }

  private static List<CarPartComponent> GetComponentsFromIDs(string inData)
  {
    List<CarPartComponent> carPartComponentList = new List<CarPartComponent>();
    string[] strArray = inData.Split(';');
    for (int index = 0; index < strArray.Length; ++index)
    {
      strArray[index] = strArray[index].Trim();
      if (!(strArray[index] == string.Empty))
      {
        int key = int.Parse(strArray[index]);
        if (App.instance.componentsManager.components.ContainsKey(key))
          carPartComponentList.Add(App.instance.componentsManager.components[key]);
      }
    }
    return carPartComponentList;
  }
}
