
using FullSerializer;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SessionSimSpeedDirector
{
    public static float fastSimSpeed = 11f;
    private static float speedLerpDuration = 0.5f;
    private SessionSimSpeedDirector.State mState = SessionSimSpeedDirector.State.Inactive;
    private SessionSimSpeedDirector.SlowdownEvents mCurrentSlowDownEvent = SessionSimSpeedDirector.SlowdownEvents.Count;
    private float mSpeedLerpDuration;
    private float mSpeedChangeDuration;
    [NonSerialized]
    private Vehicle mPreviousTargetVehicle;
    private RacingVehicle mVehicle;
    private bool mIsSlowDownFromActive;



    public enum State
    {
        Active,
        Inactive,
        SlowDownForPlayer,
        SpeedUpForPlayer,
    }

    public enum SlowdownEvents
    {
        [LocalisationID("PSG_10013198")] NewKnowledgeLevelUnlocked,
        [LocalisationID("PSG_10013199")] NewSetupLevelUnlocked,
        [LocalisationID("PSG_10013200")] FinalMinuteLapOfSession,
        [LocalisationID("PSG_10013201")] PlayerDriverCollision,
        [LocalisationID("PSG_10013202")] DriveThroughPenalty,
        [LocalisationID("PSG_10013203")] PlayerDriverCrashes,
        [LocalisationID("PSG_10013203")] AnyDriverCrashes,
        [LocalisationID("PSG_10013205")] PlayerDriverMistakes,
        [LocalisationID("PSG_10013206")] SafetyCarPeriodStarts,
        [LocalisationID("PSG_10013207")] SafetyCarPeriodEnds,
        [LocalisationID("PSG_10013208")] PlayerDriverRetires,
        [LocalisationID("PSG_10013208")] AnyDriverRetires,
        [LocalisationID("PSG_10013210")] TyreWearLow,
        [LocalisationID("PSG_10013211")] FuelLowRefuelling,
        [LocalisationID("PSG_10013212")] FuelDeltaLow,
        [LocalisationID("PSG_10013213")] FuelDeltaHigh,
        [LocalisationID("PSG_10013214")] PartConditionLow,
        [LocalisationID("PSG_10013215")] PositionGainedOvertake,
        [LocalisationID("PSG_10013216")] PositionLostOvertaken,
        [LocalisationID("PSG_10013217")] PitEntry,
        [LocalisationID("PSG_10013218")] PitStop,
        [LocalisationID("PSG_10013219")] PitExit,
        [LocalisationID("PSG_10013220")] TyresOverheating,
        [LocalisationID("PSG_10013221")] TyresUnderheating,
        [LocalisationID("PSG_10013222")] ERSFullyCharged,
        [LocalisationID("PSG_10013223")] DriverStaminaLow,
        [LocalisationID("PSG_10013224")] DrivingTime,
        [LocalisationID("PSG_10013225")] ReturnToFastSpeed,
        [LocalisationID("PSG_10013669")] CatastrophicPitFire,
        [LocalisationID("PSG_10013670")] CatastrophicLooseWheel,
        Count,
    }
}
