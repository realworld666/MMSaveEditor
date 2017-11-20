using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PersonalityTraitData
{

    public int ID;
    public PersonalityTraitData.TraitType type;
    public int[] possibleLength;
    public List<DialogCriteria> requirements;
    public float probability;
    public int[] evolvesInto;
    public int[] opposites;
    public int[] removesTraits;
    public List<DialogCriteria> triggerCriteria = new List<DialogCriteria>();
    public PersonalityTraitData.TriggerShownType shownType;
    public PersonalityTraitData.EventTriggerType eventTriggerType;
    public int allStats;
    public DriverStats driverStatsModifier = new DriverStats();
    public List<PersonalityTrait.SpecialCaseType> specialCases = new List<PersonalityTrait.SpecialCaseType>();
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
    private string mCustomTraitName = string.Empty;
    private string mCustomTraitDescription = string.Empty;
    public List<DialogCriteria> triggerEndCriteria = new List<DialogCriteria>();

    public bool canEvolve
    {
        get
        {
            return this.evolvesInto.Length > 0;
        }
    }

    public string customTraitName
    {
        get
        {
            return Localisation.LocaliseID(this.mCustomTraitName);
        }
        set
        {
            this.mCustomTraitName = value;
        }
    }

    public string customTraitDescription
    {
        get
        {
            return Localisation.LocaliseID(this.mCustomTraitDescription);
        }
        set
        {
            this.mCustomTraitDescription = value;
        }
    }

    public string NameID
    {
        get
        {
            return Localisation.LocaliseID(nameID);
        }

        set
        {
            nameID = value;
        }
    }

    public string DescriptionID
    {
        get
        {
            return Localisation.LocaliseID(descriptionID);
        }

        set
        {
            descriptionID = value;
        }
    }

    public bool IsTraitOpposite(PersonalityTraitData inTraitData)
    {
        for (int index = 0; index < this.opposites.Length; ++index)
        {
            if (this.opposites[index] == inTraitData.ID)
                return true;
        }
        return false;
    }

    public void SetDescriptionID(string inID)
    {
        this.descriptionID = inID;
    }

    public void SetNameID(string inID)
    {
        this.nameID = inID;
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
