using System;
using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarPartComponent : fsISerializationCallbacks
{
    public List<CarPart.PartType> partsAvailableTo = new List<CarPart.PartType>();
    public List<CarPartUnlockRequirement> unlockRequirements = new List<CarPartUnlockRequirement>();
    public List<CarPartComponentRequirement> activationRequirements = new List<CarPartComponentRequirement>();
    private List<CarPartComponentBonus> mBonuses = new List<CarPartComponentBonus>();
    public string iconPath = string.Empty;
    public int id = -1;
    public float agressiveTeamWeightings = 1f;
    public float nonAgressiveTeamWeightings = 1f;
    private string mNameID = string.Empty;
    private string mCustomComponentName = string.Empty;
    public CarPartComponent.ComponentType componentType;
    public float riskLevel;
    public float statBoost;
    public float maxStatBoost;
    public float reliabilityBoost;
    public float maxReliabilityBoost;
    public float productionTime;
    public float cost;
    public float redZone;
    public int iconID;
    public int level;

    public enum ComponentType
    {
        Stock,
        Engineer,
        Risky,
    }

    public void OnBeforeSerialize(Type storageType)
    {
    }

    public void OnAfterSerialize(Type storageType, ref fsData data)
    {
    }

    public void OnBeforeDeserialize(Type storageType, ref fsData data)
    {
    }

    public void OnAfterDeserialize(Type storageType)
    {
    }
}
