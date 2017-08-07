
using System;
using System.Collections.Generic;
using FullSerializer;

[fsObject("v1", new System.Type[] { }, MemberSerialization = fsMemberSerialization.OptOut)]
public class PersonalityTraitSpecialCaseBehaviour
{
    private List<PersonalityTrait.SpecialCaseType> mSpecialCases = new List<PersonalityTrait.SpecialCaseType>();
    private Driver mDriver;
    private string mPersonalityTraitName;
    private Circuit mCircuit;
    private int[] mTieredPayDriversAmount = new int[5] { 900000, 500000, 250000, 500000, 250000 };
    private float mTeamDailyImprovementModifier;
    private Driver mFightTeammateDriver;
    private readonly int fightWithTeammateTraitID = 171;

    public List<PersonalityTrait.SpecialCaseType> specialCases
    {
        set
        {
            this.mSpecialCases = value;
        }
    }

    public Driver driver
    {
        set
        {
            this.mDriver = value;
        }
    }

    public string personalityTraitName
    {
        set
        {
            this.mPersonalityTraitName = value;
        }
    }

    public bool hasSpecialCase
    {
        get
        {
            return this.mSpecialCases.Count > 0;
        }
    }

    public int tieredPayDriverAmount
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public int tieredPayDriverAmountForPlayer
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public float teamDailyImprovementModifier
    {
        get
        {
            return this.mTeamDailyImprovementModifier;
        }
    }

    public bool HasSpecialCase(PersonalityTrait.SpecialCaseType inSpecialCaseType)
    {
        if (this.hasSpecialCase)
        {
            for (int index = 0; index < this.mSpecialCases.Count; ++index)
            {
                if (this.mSpecialCases[index] == inSpecialCaseType)
                    return true;
            }
        }
        return false;
    }
}
