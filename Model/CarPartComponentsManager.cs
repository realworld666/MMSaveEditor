// Decompiled with JetBrains decompiler
// Type: CarPartComponentsManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using MM2;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarPartComponentsManager
{
  public Dictionary<int, CarPartComponent> components = new Dictionary<int, CarPartComponent>();

  public void LoadComponentsFromDatabase(Database inDatabase)
  {
    this.components.Clear();
    this.LoadComponentsData(inDatabase.partComponentsData);
  }

  private void LoadComponentsData(List<DatabaseEntry> inComponentsData)
  {
    for (int index = 0; index < inComponentsData.Count; ++index)
    {
      DatabaseEntry data = inComponentsData[index];
      CarPartComponent inComponent = new CarPartComponent();
      this.SetComponentData(inComponent, data);
      this.components.Add(data.GetIntValue("ID"), inComponent);
    }
  }

  private void SetComponentData(CarPartComponent inComponent, DatabaseEntry data)
  {
    inComponent.nameID = data.GetStringValue("Name ID");
    inComponent.iconID = data.GetIntValue("Icon");
    inComponent.iconPath = data.GetStringValue("Icon Path");
    inComponent.agressiveTeamWeightings = data.GetFloatValue("AggressiveTeamWeightings");
    inComponent.nonAgressiveTeamWeightings = data.GetFloatValue("PassiveTeamWeightings");
    inComponent.componentType = (CarPartComponent.ComponentType) Enum.Parse(typeof (CarPartComponent.ComponentType), data.GetStringValue("Type"));
    inComponent.riskLevel = data.GetFloatValue("Risk Level");
    inComponent.statBoost = data.GetFloatValue("Stat Boost");
    inComponent.maxStatBoost = data.GetFloatValue("Max Stat Boost");
    inComponent.reliabilityBoost = data.GetFloatValue("Reliability Boost") / 100f;
    inComponent.maxReliabilityBoost = data.GetFloatValue("Max Reliability Boost") / 100f;
    inComponent.productionTime = data.GetFloatValue("Production Time");
    inComponent.cost = data.GetFloatValue("Cost");
    inComponent.redZone = data.GetFloatValue("Red Zone") / 100f;
    inComponent.customComponentName = data.GetStringValue("Custom Trait Name");
    this.AddPartAvailability(inComponent, data.GetStringValue("Part"));
    this.AddRequirements(inComponent, data.GetStringValue("Activation Requirement"));
    this.AddBonus(inComponent, data);
    inComponent.level = data.GetIntValue("Level");
    inComponent.id = data.GetIntValue("ID");
  }

  public void ValidateData(CarPartComponent component)
  {
    if (App.instance.saveSystem.mostRecentlyManuallySavedOrLoadedFile.saveInfo.version == GameVersionNumber.version)
      return;
    List<DatabaseEntry> partComponentsData = App.instance.database.partComponentsData;
    for (int index = 0; index < partComponentsData.Count; ++index)
    {
      DatabaseEntry data = partComponentsData[index];
      if (data.GetStringValue("Name ID") == component.nameID || data.GetIntValue("ID") == component.id)
        this.SetComponentData(component, data);
    }
  }

  private void AddBonus(CarPartComponent inComponent, DatabaseEntry inData)
  {
    inComponent.bonuses.Clear();
    string stringValue = inData.GetStringValue("Bonus Type");
    string key = stringValue;
    if (key == null)
      return;
    // ISSUE: reference to a compiler-generated field
    if (CarPartComponentsManager.\u003C\u003Ef__switch\u0024map8 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CarPartComponentsManager.\u003C\u003Ef__switch\u0024map8 = new Dictionary<string, int>(11)
      {
        {
          "NoConditionLossFirstRace",
          0
        },
        {
          "SecondPartFree",
          1
        },
        {
          "NoConditionLossEver",
          2
        },
        {
          "TimeReductionPerMillionSpent",
          3
        },
        {
          "SlotsLevelAddNoDays",
          4
        },
        {
          "AddRandomLevelComponent",
          5
        },
        {
          "ReliabilityPerDayInProduction",
          6
        },
        {
          "UnlockExtraSlotOfLevel",
          7
        },
        {
          "PerformanceAutoImproved",
          8
        },
        {
          "ComponentLevelAddNoDays",
          9
        },
        {
          "ReliabilityBonusPerComponentOfLevel",
          10
        }
      };
    }
    int num;
    // ISSUE: reference to a compiler-generated field
    if (!CarPartComponentsManager.\u003C\u003Ef__switch\u0024map8.TryGetValue(key, out num))
      return;
    switch (num)
    {
      case 0:
        BonusNoConditionLossFirstSession lossFirstSession = new BonusNoConditionLossFirstSession();
        lossFirstSession.session = SessionDetails.SessionType.Race;
        lossFirstSession.name = stringValue;
        inComponent.AddBonuses((CarPartComponentBonus) lossFirstSession);
        break;
      case 1:
        BonusCreateTwoParts bonusCreateTwoParts = new BonusCreateTwoParts();
        bonusCreateTwoParts.name = stringValue;
        inComponent.AddBonuses((CarPartComponentBonus) bonusCreateTwoParts);
        break;
      case 2:
        BonusNoConditionLoss bonusNoConditionLoss = new BonusNoConditionLoss();
        bonusNoConditionLoss.name = stringValue;
        inComponent.AddBonuses((CarPartComponentBonus) bonusNoConditionLoss);
        break;
      case 3:
        BonusTimeReductionPerMillionSpent reductionPerMillionSpent = new BonusTimeReductionPerMillionSpent();
        reductionPerMillionSpent.bonusValue = inData.GetFloatValue("Bonus");
        reductionPerMillionSpent.name = stringValue;
        inComponent.AddBonuses((CarPartComponentBonus) reductionPerMillionSpent);
        break;
      case 4:
        BonusSpecificLevelSlotAddNoDays levelSlotAddNoDays = new BonusSpecificLevelSlotAddNoDays();
        levelSlotAddNoDays.bonusValue = inData.GetFloatValue("Bonus");
        levelSlotAddNoDays.name = stringValue;
        inComponent.AddBonuses((CarPartComponentBonus) levelSlotAddNoDays);
        break;
      case 5:
        BonusAddRandomLevelComponent randomLevelComponent = new BonusAddRandomLevelComponent();
        randomLevelComponent.bonusValue = inData.GetFloatValue("Bonus");
        randomLevelComponent.name = stringValue;
        inComponent.AddBonuses((CarPartComponentBonus) randomLevelComponent);
        break;
      case 6:
        BonusExtraReliabilityPerDayInProduction perDayInProduction = new BonusExtraReliabilityPerDayInProduction();
        perDayInProduction.bonusValue = inData.GetFloatValue("Bonus");
        perDayInProduction.name = stringValue;
        inComponent.AddBonuses((CarPartComponentBonus) perDayInProduction);
        break;
      case 7:
        BonusUnlockExtraSlot bonusUnlockExtraSlot = new BonusUnlockExtraSlot();
        bonusUnlockExtraSlot.bonusValue = inData.GetFloatValue("Bonus");
        bonusUnlockExtraSlot.name = stringValue;
        inComponent.AddBonuses((CarPartComponentBonus) bonusUnlockExtraSlot);
        break;
      case 8:
        BonusPerformanceAutoImproved performanceAutoImproved = new BonusPerformanceAutoImproved();
        performanceAutoImproved.name = stringValue;
        inComponent.AddBonuses((CarPartComponentBonus) performanceAutoImproved);
        break;
      case 9:
        BonusSpecificLevelComponentAddNoDays componentAddNoDays = new BonusSpecificLevelComponentAddNoDays();
        componentAddNoDays.name = stringValue;
        componentAddNoDays.bonusValue = inData.GetFloatValue("Bonus");
        inComponent.AddBonuses((CarPartComponentBonus) componentAddNoDays);
        break;
      case 10:
        BonusPerSpecificLevelComponentUsed levelComponentUsed = new BonusPerSpecificLevelComponentUsed();
        levelComponentUsed.stat = CarPartStats.CarPartStat.Reliability;
        levelComponentUsed.statBoost = inComponent.reliabilityBoost;
        levelComponentUsed.bonusValue = inData.GetFloatValue("Bonus");
        levelComponentUsed.name = stringValue;
        inComponent.reliabilityBoost = 0.0f;
        inComponent.AddBonuses((CarPartComponentBonus) levelComponentUsed);
        break;
    }
  }

  private void AddRequirements(CarPartComponent inComponent, string inRequirement)
  {
    inComponent.activationRequirements.Clear();
    string key = inRequirement;
    if (key == null)
      return;
    // ISSUE: reference to a compiler-generated field
    if (CarPartComponentsManager.\u003C\u003Ef__switch\u0024map9 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CarPartComponentsManager.\u003C\u003Ef__switch\u0024map9 = new Dictionary<string, int>(9)
      {
        {
          "TyreCompoundHard",
          0
        },
        {
          "TyreCompoundMedium",
          1
        },
        {
          "TyreCompoundIntermediate",
          2
        },
        {
          "TyreCompoundSoft",
          3
        },
        {
          "TyreCompoundSuperSoft",
          4
        },
        {
          "TyreCompoundWet",
          5
        },
        {
          "Race",
          6
        },
        {
          "Qualifying",
          7
        },
        {
          "Practice",
          8
        }
      };
    }
    int num;
    // ISSUE: reference to a compiler-generated field
    if (!CarPartComponentsManager.\u003C\u003Ef__switch\u0024map9.TryGetValue(key, out num))
      return;
    switch (num)
    {
      case 0:
        inComponent.activationRequirements.Add((CarPartComponentRequirement) new RequirementTyreCompound()
        {
          compound = TyreSet.Compound.Hard
        });
        break;
      case 1:
        inComponent.activationRequirements.Add((CarPartComponentRequirement) new RequirementTyreCompound()
        {
          compound = TyreSet.Compound.Medium
        });
        break;
      case 2:
        inComponent.activationRequirements.Add((CarPartComponentRequirement) new RequirementTyreCompound()
        {
          compound = TyreSet.Compound.Intermediate
        });
        break;
      case 3:
        inComponent.activationRequirements.Add((CarPartComponentRequirement) new RequirementTyreCompound()
        {
          compound = TyreSet.Compound.Soft
        });
        break;
      case 4:
        inComponent.activationRequirements.Add((CarPartComponentRequirement) new RequirementTyreCompound()
        {
          compound = TyreSet.Compound.SuperSoft
        });
        break;
      case 5:
        inComponent.activationRequirements.Add((CarPartComponentRequirement) new RequirementTyreCompound()
        {
          compound = TyreSet.Compound.Wet
        });
        break;
      case 6:
        inComponent.activationRequirements.Add((CarPartComponentRequirement) new RequirementCurrentSession()
        {
          session = SessionDetails.SessionType.Race
        });
        break;
      case 7:
        inComponent.activationRequirements.Add((CarPartComponentRequirement) new RequirementCurrentSession()
        {
          session = SessionDetails.SessionType.Qualifying
        });
        break;
      case 8:
        inComponent.activationRequirements.Add((CarPartComponentRequirement) new RequirementCurrentSession()
        {
          session = SessionDetails.SessionType.Practice
        });
        break;
    }
  }

  private void AddPartAvailability(CarPartComponent inComponent, string inPartData)
  {
    inComponent.partsAvailableTo.Clear();
    string[] strArray = inPartData.Split(';');
    for (int index = 0; index < strArray.Length; ++index)
    {
      strArray[index] = strArray[index].Trim();
      string key = strArray[index];
      if (key != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (CarPartComponentsManager.\u003C\u003Ef__switch\u0024mapA == null)
        {
          // ISSUE: reference to a compiler-generated field
          CarPartComponentsManager.\u003C\u003Ef__switch\u0024mapA = new Dictionary<string, int>(12)
          {
            {
              "Rear Wing GT",
              0
            },
            {
              "Engine GT",
              1
            },
            {
              "Brakes GT",
              2
            },
            {
              "Gearbox GT",
              3
            },
            {
              "Suspension GT",
              4
            },
            {
              "Rear Wing",
              5
            },
            {
              "Front Wing",
              6
            },
            {
              "Brakes",
              7
            },
            {
              "Engine",
              8
            },
            {
              "Suspension",
              9
            },
            {
              "Gearbox",
              10
            },
            {
              "All",
              11
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (CarPartComponentsManager.\u003C\u003Ef__switch\u0024mapA.TryGetValue(key, out num))
        {
          switch (num)
          {
            case 0:
              inComponent.partsAvailableTo.Add(CarPart.PartType.RearWingGT);
              continue;
            case 1:
              inComponent.partsAvailableTo.Add(CarPart.PartType.EngineGT);
              continue;
            case 2:
              inComponent.partsAvailableTo.Add(CarPart.PartType.BrakesGT);
              continue;
            case 3:
              inComponent.partsAvailableTo.Add(CarPart.PartType.GearboxGT);
              continue;
            case 4:
              inComponent.partsAvailableTo.Add(CarPart.PartType.SuspensionGT);
              continue;
            case 5:
              inComponent.partsAvailableTo.Add(CarPart.PartType.RearWing);
              continue;
            case 6:
              inComponent.partsAvailableTo.Add(CarPart.PartType.FrontWing);
              continue;
            case 7:
              inComponent.partsAvailableTo.Add(CarPart.PartType.Brakes);
              continue;
            case 8:
              inComponent.partsAvailableTo.Add(CarPart.PartType.Engine);
              continue;
            case 9:
              inComponent.partsAvailableTo.Add(CarPart.PartType.Suspension);
              continue;
            case 10:
              inComponent.partsAvailableTo.Add(CarPart.PartType.Gearbox);
              continue;
            case 11:
              inComponent.partsAvailableTo.Add(CarPart.PartType.RearWing);
              inComponent.partsAvailableTo.Add(CarPart.PartType.FrontWing);
              inComponent.partsAvailableTo.Add(CarPart.PartType.Brakes);
              inComponent.partsAvailableTo.Add(CarPart.PartType.Engine);
              inComponent.partsAvailableTo.Add(CarPart.PartType.Suspension);
              inComponent.partsAvailableTo.Add(CarPart.PartType.Gearbox);
              inComponent.partsAvailableTo.Add(CarPart.PartType.RearWingGT);
              inComponent.partsAvailableTo.Add(CarPart.PartType.BrakesGT);
              inComponent.partsAvailableTo.Add(CarPart.PartType.EngineGT);
              inComponent.partsAvailableTo.Add(CarPart.PartType.GearboxGT);
              inComponent.partsAvailableTo.Add(CarPart.PartType.SuspensionGT);
              continue;
            default:
              continue;
          }
        }
      }
    }
  }

  public List<CarPartComponent> GetAllComponents(CarPartComponent.ComponentType inComponentType, CarPart.PartType inPartType)
  {
    List<CarPartComponent> inList = new List<CarPartComponent>();
    inList.AddRange((IEnumerable<CarPartComponent>) this.components.Values);
    return CarPartComponentsManager.GetAllComponents(inList, inComponentType, inPartType);
  }

  public static List<CarPartComponent> GetAllComponents(List<CarPartComponent> inList, CarPartComponent.ComponentType inComponentType, CarPart.PartType inPartType)
  {
    List<CarPartComponent> carPartComponentList = new List<CarPartComponent>();
    for (int index = 0; index < inList.Count; ++index)
    {
      CarPartComponent carPartComponent = inList[index];
      if (carPartComponent.componentType == inComponentType && carPartComponent.partsAvailableTo.Contains(inPartType))
        carPartComponentList.Add(carPartComponent);
    }
    return carPartComponentList;
  }

  public List<CarPartComponent> GetComponentsOfLevel(CarPart.PartType inPartType, int inLevel, params CarPartComponent.ComponentType[] inComponentType)
  {
    List<CarPartComponent> carPartComponentList1 = new List<CarPartComponent>();
    List<CarPartComponent.ComponentType> componentTypeList = new List<CarPartComponent.ComponentType>((IEnumerable<CarPartComponent.ComponentType>) inComponentType);
    carPartComponentList1.AddRange((IEnumerable<CarPartComponent>) this.components.Values);
    List<CarPartComponent> carPartComponentList2 = new List<CarPartComponent>();
    for (int index = 0; index < carPartComponentList1.Count; ++index)
    {
      CarPartComponent carPartComponent = carPartComponentList1[index];
      if (carPartComponent.level == inLevel && componentTypeList.Contains(carPartComponent.componentType) && carPartComponent.partsAvailableTo.Contains(inPartType))
        carPartComponentList2.Add(carPartComponent);
    }
    return carPartComponentList2;
  }

  public List<CarPartComponent> GetComponentsOfLevel(int inLevel, params CarPartComponent.ComponentType[] inComponentType)
  {
    List<CarPartComponent> carPartComponentList1 = new List<CarPartComponent>();
    List<CarPartComponent.ComponentType> componentTypeList = new List<CarPartComponent.ComponentType>((IEnumerable<CarPartComponent.ComponentType>) inComponentType);
    carPartComponentList1.AddRange((IEnumerable<CarPartComponent>) this.components.Values);
    List<CarPartComponent> carPartComponentList2 = new List<CarPartComponent>();
    for (int index = 0; index < carPartComponentList1.Count; ++index)
    {
      CarPartComponent carPartComponent = carPartComponentList1[index];
      if (carPartComponent.level == inLevel && componentTypeList.Contains(carPartComponent.componentType))
        carPartComponentList2.Add(carPartComponent);
    }
    return carPartComponentList2;
  }
}
