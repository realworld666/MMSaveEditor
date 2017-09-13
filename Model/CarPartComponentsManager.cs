using FullSerializer;
using MM2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarPartComponentsManager
{
    public static CarPartComponentsManager Instance = new CarPartComponentsManager();

    public Dictionary<int, CarPartComponent> components = new Dictionary<int, CarPartComponent>();

    public CarPartComponentsManager()
    {
        Instance = this;

        try
        {
            var uri = new Uri("pack://application:,,,/Assets/Part Components.txt");
            System.Windows.Resources.StreamResourceInfo resourceStream = Application.GetResourceStream(uri);

            using (var reader = new StreamReader(resourceStream.Stream))
            {
                string traitsText = reader.ReadToEnd();
                List<DatabaseEntry> result = DatabaseReader.LoadFromText(traitsText);

                LoadComponentsData(result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            //throw;
        }
    }

    private void LoadComponentsData(List<DatabaseEntry> inComponentsData)
    {
        this.components.Clear();
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
        inComponent.componentType = (CarPartComponent.ComponentType)Enum.Parse(typeof(CarPartComponent.ComponentType), data.GetStringValue("Type"));
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

    private void AddBonus(CarPartComponent inComponent, DatabaseEntry inData)
    {
        inComponent.bonuses.Clear();
        string stringValue = inData.GetStringValue("Bonus Type");
        string text = stringValue;
        switch (text)
        {
            case "NoConditionLossFirstRace":
                inComponent.AddBonuses(new BonusNoConditionLossFirstSession
                {
                    session = SessionDetails.SessionType.Race,
                    name = stringValue
                });
                break;
            case "SecondPartFree":
                inComponent.AddBonuses(new BonusCreateTwoParts
                {
                    name = stringValue
                });
                break;
            case "NoConditionLossEver":
                inComponent.AddBonuses(new BonusNoConditionLoss
                {
                    name = stringValue
                });
                break;
            case "TimeReductionPerMillionSpent":
                inComponent.AddBonuses(new BonusTimeReductionPerMillionSpent
                {
                    bonusValue = inData.GetFloatValue("Bonus"),
                    name = stringValue
                });
                break;
            case "SlotsLevelAddNoDays":
                inComponent.AddBonuses(new BonusSpecificLevelSlotAddNoDays
                {
                    bonusValue = inData.GetFloatValue("Bonus"),
                    name = stringValue
                });
                break;
            case "AddRandomLevelComponent":
                inComponent.AddBonuses(new BonusAddRandomLevelComponent
                {
                    bonusValue = inData.GetFloatValue("Bonus"),
                    name = stringValue
                });
                break;
            case "ReliabilityPerDayInProduction":
                inComponent.AddBonuses(new BonusExtraReliabilityPerDayInProduction
                {
                    bonusValue = inData.GetFloatValue("Bonus"),
                    name = stringValue
                });
                break;
            case "UnlockExtraSlotOfLevel":
                inComponent.AddBonuses(new BonusUnlockExtraSlot
                {
                    bonusValue = inData.GetFloatValue("Bonus"),
                    name = stringValue
                });
                break;
            case "PerformanceAutoImproved":
                inComponent.AddBonuses(new BonusPerformanceAutoImproved
                {
                    name = stringValue
                });
                break;
            case "ComponentLevelAddNoDays":
                inComponent.AddBonuses(new BonusSpecificLevelComponentAddNoDays
                {
                    name = stringValue,
                    bonusValue = inData.GetFloatValue("Bonus")
                });
                break;
            case "ReliabilityBonusPerComponentOfLevel":
                {
                    BonusPerSpecificLevelComponentUsed bonusPerSpecificLevelComponentUsed = new BonusPerSpecificLevelComponentUsed();
                    bonusPerSpecificLevelComponentUsed.stat = CarPartStats.CarPartStat.Reliability;
                    bonusPerSpecificLevelComponentUsed.statBoost = inComponent.reliabilityBoost;
                    bonusPerSpecificLevelComponentUsed.bonusValue = inData.GetFloatValue("Bonus");
                    bonusPerSpecificLevelComponentUsed.name = stringValue;
                    inComponent.reliabilityBoost = 0f;
                    inComponent.AddBonuses(bonusPerSpecificLevelComponentUsed);
                    break;
                }
        }
    }

    private void AddRequirements(CarPartComponent inComponent, string inRequirement)
    {
        inComponent.activationRequirements.Clear();
        switch (inRequirement)
        {
            case "TyreCompoundHard":
                {
                    RequirementTyreCompound requirementTyreCompound = new RequirementTyreCompound();
                    requirementTyreCompound.compound = TyreSet.Compound.Hard;
                    inComponent.activationRequirements.Add(requirementTyreCompound);
                    break;
                }
            case "TyreCompoundMedium":
                {
                    RequirementTyreCompound requirementTyreCompound2 = new RequirementTyreCompound();
                    requirementTyreCompound2.compound = TyreSet.Compound.Medium;
                    inComponent.activationRequirements.Add(requirementTyreCompound2);
                    break;
                }
            case "TyreCompoundIntermediate":
                {
                    RequirementTyreCompound requirementTyreCompound3 = new RequirementTyreCompound();
                    requirementTyreCompound3.compound = TyreSet.Compound.Intermediate;
                    inComponent.activationRequirements.Add(requirementTyreCompound3);
                    break;
                }
            case "TyreCompoundSoft":
                {
                    RequirementTyreCompound requirementTyreCompound4 = new RequirementTyreCompound();
                    requirementTyreCompound4.compound = TyreSet.Compound.Soft;
                    inComponent.activationRequirements.Add(requirementTyreCompound4);
                    break;
                }
            case "TyreCompoundSuperSoft":
                {
                    RequirementTyreCompound requirementTyreCompound5 = new RequirementTyreCompound();
                    requirementTyreCompound5.compound = TyreSet.Compound.SuperSoft;
                    inComponent.activationRequirements.Add(requirementTyreCompound5);
                    break;
                }
            case "TyreCompoundWet":
                {
                    RequirementTyreCompound requirementTyreCompound6 = new RequirementTyreCompound();
                    requirementTyreCompound6.compound = TyreSet.Compound.Wet;
                    inComponent.activationRequirements.Add(requirementTyreCompound6);
                    break;
                }
            case "Race":
                {
                    RequirementCurrentSession requirementCurrentSession = new RequirementCurrentSession();
                    requirementCurrentSession.session = SessionDetails.SessionType.Race;
                    inComponent.activationRequirements.Add(requirementCurrentSession);
                    break;
                }
            case "Qualifying":
                {
                    RequirementCurrentSession requirementCurrentSession2 = new RequirementCurrentSession();
                    requirementCurrentSession2.session = SessionDetails.SessionType.Qualifying;
                    inComponent.activationRequirements.Add(requirementCurrentSession2);
                    break;
                }
            case "Practice":
                {
                    RequirementCurrentSession requirementCurrentSession3 = new RequirementCurrentSession();
                    requirementCurrentSession3.session = SessionDetails.SessionType.Practice;
                    inComponent.activationRequirements.Add(requirementCurrentSession3);
                    break;
                }
        }
    }


    private void AddPartAvailability(CarPartComponent inComponent, string inPartData)
    {
        inComponent.partsAvailableTo.Clear();
        string[] array = inPartData.Split(new char[]
        {
        ';'
        });
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = array[i].Trim();
            string text = array[i];
            switch (text)
            {
                case "Rear Wing GT":
                    inComponent.partsAvailableTo.Add(CarPart.PartType.RearWingGT);
                    break;
                case "Engine GT":
                    inComponent.partsAvailableTo.Add(CarPart.PartType.EngineGT);
                    break;
                case "Brakes GT":
                    inComponent.partsAvailableTo.Add(CarPart.PartType.BrakesGT);
                    break;
                case "Gearbox GT":
                    inComponent.partsAvailableTo.Add(CarPart.PartType.GearboxGT);
                    break;
                case "Suspension GT":
                    inComponent.partsAvailableTo.Add(CarPart.PartType.SuspensionGT);
                    break;
                case "Rear Wing":
                    inComponent.partsAvailableTo.Add(CarPart.PartType.RearWing);
                    break;
                case "Front Wing":
                    inComponent.partsAvailableTo.Add(CarPart.PartType.FrontWing);
                    break;
                case "Brakes":
                    inComponent.partsAvailableTo.Add(CarPart.PartType.Brakes);
                    break;
                case "Engine":
                    inComponent.partsAvailableTo.Add(CarPart.PartType.Engine);
                    break;
                case "Suspension":
                    inComponent.partsAvailableTo.Add(CarPart.PartType.Suspension);
                    break;
                case "Gearbox":
                    inComponent.partsAvailableTo.Add(CarPart.PartType.Gearbox);
                    break;
                case "All":
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
                    break;
            }
        }
    }

    public List<CarPartComponent> GetAllComponents(CarPartComponent.ComponentType inComponentType, CarPart.PartType inPartType)
    {
        List<CarPartComponent> inList = new List<CarPartComponent>();
        inList.AddRange((IEnumerable<CarPartComponent>)this.components.Values);
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
        List<CarPartComponent.ComponentType> componentTypeList = new List<CarPartComponent.ComponentType>((IEnumerable<CarPartComponent.ComponentType>)inComponentType);
        carPartComponentList1.AddRange((IEnumerable<CarPartComponent>)this.components.Values);
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
        List<CarPartComponent.ComponentType> componentTypeList = new List<CarPartComponent.ComponentType>((IEnumerable<CarPartComponent.ComponentType>)inComponentType);
        carPartComponentList1.AddRange((IEnumerable<CarPartComponent>)this.components.Values);
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
