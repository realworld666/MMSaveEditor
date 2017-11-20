
using System;
using FullSerializer;

[fsObject("v1", new System.Type[] { }, MemberSerialization = fsMemberSerialization.OptOut)]
public class PersonalityTrait
{
    public PersonalityTraitData data;
    private Driver mDriver;
    private DateTime mTraitStartDate = DateTime.MaxValue;
    private DateTime mTraitEndTime = new DateTime();
    private DriverStats mDriverStats = new DriverStats();
    private PersonalityTraitSpecialCaseBehaviour specialCaseBehaviour = new PersonalityTraitSpecialCaseBehaviour();

    public DateTime TraitStartDate
    {
        get
        {
            return mTraitStartDate;
        }

        set
        {
            mTraitStartDate = value;
        }
    }

    public DateTime TraitEndTime
    {
        get
        {
            return mTraitEndTime;
        }

        set
        {
            mTraitEndTime = value;
        }
    }

    public PersonalityTraitData Data
    {
        get
        {
            return Game.instance.personalityTraitManager?.personalityTraits[data.ID];
        }
    }

    public string TraitName
    {
        get
        {
            return Data.NameID;
        }
    }

    public PersonalityTrait()
    {

    }

    public PersonalityTrait(PersonalityTraitData inPersonalityTraitData, Driver inDriver)
    {
        this.data = inPersonalityTraitData;
        this.mDriver = inDriver;
        this.specialCaseBehaviour.driver = this.mDriver;
        this.specialCaseBehaviour.specialCases = this.data.specialCases;
        this.mTraitStartDate = Game.instance.time.now;
    }

    public void SetupTraitEndTime()
    {
        if (this.HasSpecialCase(PersonalityTrait.SpecialCaseType.CarPartPromise) && !this.mDriver.IsFreeAgent())
            this.mTraitEndTime = this.mDriver.Contract.GetTeam().championship.GetCurrentEventDetails().raceSessions[0].sessionDateTime;
        else if (this.HasSpecialCase(PersonalityTrait.SpecialCaseType.ChampionshipPositionPromise) && !this.mDriver.IsFreeAgent())
            this.mTraitEndTime = this.mDriver.Contract.GetTeam().championship.currentSeasonEndDate.AddDays(-1.0);
        else if (this.data.possibleLength.Length == 1)
            this.mTraitEndTime = Game.instance.time.now.AddDays((double)(7 * this.data.possibleLength[0]));
        else if (this.data.possibleLength.Length > 1)
        {
            int randomInc = RandomUtility.GetRandomInc(this.data.possibleLength[0], this.data.possibleLength[1]);
            this.mTraitEndTime = Game.instance.time.now.AddDays((double)(7 * randomInc));
        }
        else
            this.mTraitEndTime = Game.instance.time.now.AddDays(1.0);
    }

    public bool HasSpecialCase(PersonalityTrait.SpecialCaseType inSpecialCaseType)
    {
        return this.specialCaseBehaviour.HasSpecialCase(inSpecialCaseType);
    }

    public void OnTraitStart()
    {
        //this.specialCaseBehaviour.OnTraitStart();
    }

    public bool DoesModifyStat(PersonalityTrait.StatModified inStatModified)
    {
        return (double)this.GetSingleModifierForStat(inStatModified) != 0.0;
    }

    public DriverStats GetDriverStatsModifier()
    {
        if (this.data.allStats == 0)
            return this.data.driverStatsModifier;
        this.mDriverStats.Clear();
        this.mDriverStats.Braking = (float)this.data.allStats;
        this.mDriverStats.Cornering = (float)this.data.allStats;
        this.mDriverStats.Smoothness = (float)this.data.allStats;
        this.mDriverStats.Overtaking = (float)this.data.allStats;
        this.mDriverStats.Consistency = (float)this.data.allStats;
        this.mDriverStats.Adaptability = (float)this.data.allStats;
        this.mDriverStats.Fitness = (float)this.data.allStats;
        this.mDriverStats.Feedback = (float)this.data.allStats;
        this.mDriverStats.Focus = (float)this.data.allStats;
        return this.mDriverStats;
    }

    public float GetSingleModifierForStat(PersonalityTrait.StatModified inStatModified)
    {
        switch (inStatModified)
        {
            case PersonalityTrait.StatModified.Braking:
                return this.GetDriverStatsModifier().Braking;
            case PersonalityTrait.StatModified.Cornering:
                return this.GetDriverStatsModifier().Cornering;
            case PersonalityTrait.StatModified.Smoothness:
                return this.GetDriverStatsModifier().Smoothness;
            case PersonalityTrait.StatModified.Overtakng:
                return this.GetDriverStatsModifier().Overtaking;
            case PersonalityTrait.StatModified.Consistency:
                return this.GetDriverStatsModifier().Consistency;
            case PersonalityTrait.StatModified.Adaptability:
                return this.GetDriverStatsModifier().Adaptability;
            case PersonalityTrait.StatModified.Fitness:
                return this.GetDriverStatsModifier().Fitness;
            case PersonalityTrait.StatModified.Feedback:
                return this.GetDriverStatsModifier().Feedback;
            case PersonalityTrait.StatModified.Focus:
                return this.GetDriverStatsModifier().Focus;
            case PersonalityTrait.StatModified.Marketability:
                return this.GetDriverStatsModifier().Marketability;
            case PersonalityTrait.StatModified.Morale:
                return this.data.moraleModifier;
            case PersonalityTrait.StatModified.MechanicRelationship:
                return (float)this.data.mechanicModifier;
            case PersonalityTrait.StatModified.TeammateMorale:
                return this.data.teammateModifier;
            case PersonalityTrait.StatModified.ChairmanHappiness:
                return (float)this.data.chairmanModifier;
            case PersonalityTrait.StatModified.Improveability:
                return this.data.improveabilityModifier;
            case PersonalityTrait.StatModified.Potential:
                return (float)this.data.potentialModifier;
            case PersonalityTrait.StatModified.DesiredWins:
                return (float)this.data.desiredWinsModifier;
            case PersonalityTrait.StatModified.DesiredEarnings:
                return (float)this.data.desiredEarningsModifier;
            default:
                return 0.0f;
        }
    }

    public enum StatModified
    {
        [LocalisationID("PSG_10001289")] Braking,
        [LocalisationID("PSG_10001287")] Cornering,
        [LocalisationID("PSG_10001291")] Smoothness,
        [LocalisationID("PSG_10001295")] Overtakng,
        [LocalisationID("PSG_10001293")] Consistency,
        [LocalisationID("PSG_10001297")] Adaptability,
        [LocalisationID("PSG_10001303")] Fitness,
        [LocalisationID("PSG_10001301")] Feedback,
        [LocalisationID("PSG_10001299")] Focus,
        [LocalisationID("PSG_10001533")] Marketability,
        [LocalisationID("PSG_10001625")] Morale,
        [LocalisationID("PSG_10006098")] MechanicRelationship,
        [LocalisationID("PSG_10006099")] TeammateMorale,
        [LocalisationID("PSG_10006100")] ChairmanHappiness,
        [LocalisationID("PSG_10006101")] Improveability,
        [LocalisationID("PSG_10006102")] Potential,
        [LocalisationID("PSG_10006103")] DesiredWins,
        [LocalisationID("PSG_10006104")] DesiredEarnings,
        Count,
    }

    public enum SpecialCaseType
    {
        CarHappinessBonus,
        CarHappinessNegative,
        P2InRace,
        P1InRace,
        RaceLap1,
        FuelBurnLow,
        FuelBurnHigh,
        TyreWearLow,
        TyreWearHigh,
        Race,
        Qualifying,
        Practice,
        WetSession,
        Track,
        HomeRace,
        RandomPaymentLow,
        RandomPaymentMed,
        PayDriver,
        WillNotJoinRival,
        WIllNotRenewContract,
        FightWithTeammate,
        RandomNewHairstyle,
        IntermediateTyres,
        SponsorTop3,
        OneOnOne,
        TurnOffTeamOrders,
        TurnOffStrategy,
        NotNumberOne,
        MechanicWorseThanDriver,
        TeammateBetterThanDriver,
        TeammateEarningMoreThanDriver,
        NeckInjury,
        FaceInjury,
        NeedMoreTrophiesBeforeRetiring,
        NeedLessTrophiesBeforeRetiring,
        MentorImproveabilityBoost,
        MentorImproveabilityDebuff,
        OffendedByInterview,
        ButteredUpByInterview,
        SingleSeaters,
        GTSeries,
        ShockRetirement,
        CarPartPromise,
        IncomeIncreasePromise,
        ChampionshipPositionPromise,
        HQBuildingPromise,
        FireMechanicPromise,
        FireEngineerPromise,
        FireReservePromise,
        Guildford,
        Ardennes,
        Phoenix,
        Yokohama,
        Munich,
        CapeTown,
        Doha,
        Milan,
        Singapore,
        Beijing,
        Dubai,
        Tondela,
        RioDeJaneiro,
        BlackSea,
        Sydney,
        Vancouver,
        Count,
    }

    public bool CanApplyTrait()
    {
        return this.specialCaseBehaviour.CanBeApplied();
    }
}
