
using System;
using System.Collections.Generic;
using FullSerializer;

[fsObject("v2", new System.Type[] { typeof(PersonalityTraitController) }, MemberSerialization = fsMemberSerialization.OptOut)]
public class PersonalityTraitController_v2
{
    public List<PersonalityTrait> permanentPersonalityTraits = new List<PersonalityTrait>();
    public List<PersonalityTrait> temporaryPersonalityTraits = new List<PersonalityTrait>();
    public List<PersonalityTrait> allTraits = new List<PersonalityTrait>();
    private List<int> mTraitHistory = new List<int>();
    private readonly int mMaxCooldownDaysRange = 180;
    private DateTime cooldownPeriodEnd = new DateTime();
    private DriverStats mDriverStats = new DriverStats();
    private int mLastRandomCooldownDayValue;
    private Driver mDriver;

    public PersonalityTraitController_v2(PersonalityTraitController inOldController)
    {
        this.permanentPersonalityTraits = inOldController.permanentPersonalityTraits;
        this.temporaryPersonalityTraits = inOldController.temporaryPersonalityTraits;
        this.mTraitHistory = inOldController.mTraitHistory;
        this.mMaxCooldownDaysRange = inOldController.mMaxCooldownDaysRange;
        this.cooldownPeriodEnd = inOldController.cooldownPeriodEnd;
        this.mLastRandomCooldownDayValue = inOldController.mLastRandomCooldownDayValue;
        this.mDriver = inOldController.mDriver;
        this.mDriverStats = inOldController.mDriverStats;
        this.allTraits = new List<PersonalityTrait>();
    }

    public PersonalityTraitController_v2(Driver inDriver)
    {
        this.mDriver = inDriver;
    }

    public PersonalityTraitController_v2()
    {
        this.allTraits = new List<PersonalityTrait>();
    }
}
