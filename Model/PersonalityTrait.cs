
using System;
using FullSerializer;

[fsObject("v1", new System.Type[] { }, MemberSerialization = fsMemberSerialization.OptOut)]
public class PersonalityTrait
{
    private PersonalityTraitData data;
    private DateTime mTraitStartDate = DateTime.MaxValue;
    private DateTime mTraitEndTime = new DateTime();
    private DriverStats mDriverStats = new DriverStats();
    private PersonalityTraitSpecialCaseBehaviour specialCaseBehaviour = new PersonalityTraitSpecialCaseBehaviour();
    private Driver mDriver;

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
            return data;
        }

        set
        {
            data = value;
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
}
