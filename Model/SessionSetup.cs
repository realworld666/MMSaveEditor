using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SessionSetup
{
    private SessionSetup.SetupOutput mAISetupOutput = new SessionSetup.SetupOutput();
    private SessionSetup.PitCrewSizeDependentSteps mCurrentLimitingStep = SessionSetup.PitCrewSizeDependentSteps.All;
    private SessionPitstop mSessionPitStop = new SessionPitstop();
    public const float rechargeStep = 0.25f;
    public const float rechargeRiskStep = 0.125f;
    private SessionSetup.State mState;
    private float mSetupTimer;
    private float mSetupDuration;
    private Driver mDriver;
    private RacingVehicle mVehicle;
    private SessionSetupChange changes;
    private List<SessionSetupRepairPart> mRepairParts;
    private SetupDetails mCurrentSetup;
    private SetupDetails mTargetSetup;
    private SetupDesignData mSetupDesignData;
    private ChampionshipRules.PitStopCrewSize mPitCrewSize;
    private int mSavedFuelLevel;
    private int mTargetFuelLevel;
    private float mTargetBatteryCharge;
    private float mBatteryChargeRisk;
    private bool mIsBatteryGoingToBreak;

    [fsObject("v0", new System.Type[] { }, MemberSerialization = fsMemberSerialization.OptOut)]
    public class SetupInput
    {
        public float tyrePressure;
        public float tyreCamber;
        public float suspensionStiffness;
        public float gearRatio;
        public float frontWingAngle;
        public float rearWingAngle;
    }

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class SetupOutput
    {
        public float aerodynamics;
        public float speedBalance;
        public float handling;
    }

    public enum State
    {
        Setup,
        ChangingSetup,
    }

    public enum Trim
    {
        [LocalisationID("PSG_10002225")] Qualifying,
        [LocalisationID("PSG_10002226")] Race,
    }

    public enum SetupOpinion
    {
        None,
        VeryPoor,
        Poor,
        OK,
        Good,
        Great,
        Excellent,
    }

    public enum PitCrewSizeDependentSteps
    {
        Condition,
        Tyres,
        Fuel,
        Setup,
        Trim,
        Battery,
        Driver,
        All,
    }
}
