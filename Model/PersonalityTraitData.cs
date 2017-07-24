using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PersonalityTraitData
{
    public List<DialogCriteria> triggerCriteria = new List<DialogCriteria>();
    public DriverStats driverStatsModifier = new DriverStats();
    public List<PersonalityTrait.SpecialCaseType> specialCases = new List<PersonalityTrait.SpecialCaseType>();
    private string mCustomTraitName = string.Empty;
    private string mCustomTraitDescription = string.Empty;
    public int ID;
    public PersonalityTraitData.TraitType type;
    public int[] possibleLength;
    public List<DialogCriteria> requirements;
    private float probability;
    public int[] evolvesInto;
    public int[] opposites;
    public int[] removesTraits;
    public PersonalityTraitData.TriggerShownType shownType;
    public PersonalityTraitData.EventTriggerType eventTriggerType;
    public int allStats;
    public float moraleModifier;
    public int mechanicModifier;
    public float teammateModifier;
    public int chairmanModifier;
    public float improveabilityModifier;
    public int potentialModifier;
    public int desiredWinsModifier;
    public long desiredEarningsModifier;
    public string specialCaseDescriptionID;
    public bool isRepeatable;
    private string nameID;
    private string descriptionID;

    public float Probability
    {
        get
        {
            return probability;
        }

        set
        {
            probability = value;
        }
    }

    public enum TraitType
    {
        Permanent,
        Temporary,
    }

    public enum TriggerShownType
    {
        AllDrivers,
        PlayerDriverOnly,
    }

    public enum EventTriggerType
    {
        None,
        PostRace,
    }
}
