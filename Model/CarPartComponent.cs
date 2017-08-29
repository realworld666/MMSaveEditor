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
    public CarPartComponent.ComponentType componentType;
    public float riskLevel;
    public float statBoost;
    public float maxStatBoost;
    public float reliabilityBoost;
    public float maxReliabilityBoost;
    public float productionTime;
    public float cost;
    public float redZone;
    public string iconPath = string.Empty;
    public int iconID;
    public int level;
    public int id = -1;
    public float agressiveTeamWeightings = 1f;
    public float nonAgressiveTeamWeightings = 1f;
    private string mNameID = string.Empty;
    private string mCustomComponentName = string.Empty;
    public bool isRandomComponent;

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

    public void ApplyStats(CarPart inPart)
    {
        if (this.HasActivationRequirement())
            return;
        inPart.stats.rulesRisk += this.riskLevel;
        inPart.stats.maxPerformance += this.maxStatBoost;
        inPart.stats.partCondition.redZone += this.redZone;
        inPart.stats.maxReliability += this.maxReliabilityBoost;
        inPart.stats.SetStat(CarPartStats.CarPartStat.MainStat, inPart.stats.stat + this.statBoost);
        inPart.stats.SetStat(CarPartStats.CarPartStat.Reliability, inPart.stats.reliability + this.reliabilityBoost);
    }

    private bool HasActivationRequirement()
    {
        return this.activationRequirements.Count != 0;
    }

    public void ApplyBonus(CarPartDesign inDesign, CarPart inPart)
    {
        if (this.IsUnlocked(inDesign.team))
        {
            this.mBonuses.ForEach(delegate (CarPartComponentBonus bonus)
            {
                bonus.ApplyBonus(inDesign, inPart);
            });
        }
    }

    private bool IsUnlocked(Team inTeam)
    {
        for (int index = 0; index < this.unlockRequirements.Count; ++index)
        {
            if (this.unlockRequirements[index].IsLocked(inTeam))
                return false;
        }
        return true;
    }

    public void OnPartBuilt(CarPartDesign inDesign, CarPart inPart)
    {
        if (this.IsUnlocked(inDesign.team))
        {
            this.mBonuses.ForEach(delegate (CarPartComponentBonus bonus)
            {
                bonus.OnPartBuilt(inDesign, inPart);
            });
        }
    }
}
