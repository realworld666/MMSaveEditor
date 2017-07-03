using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class UITutorialSequence
{


    public enum State
    {
        NotStartedYet,
        OnGoing,
        Finished,
    }

    public enum EndSequenceActionType
    {
        StartAnotherScreenTutorialSequence,
        DoNothing,
        StartRaceLights,
    }

    public enum StartSequenceEventType
    {
        FollowCurrentTutorialFlow,
        WaitAnotherScreenSequenceEnd,
        PitStopFinished,
        TyresWorn,
        Fuel,
        PitStopEnter,
        SessionCompletePercentage,
    }

    public enum GameStateTrigger
    {
        RaceEvent,
        Frontend,
        PracticeSession,
        QualifyingSession,
        RaceGrid,
        PreSessionHUB,
    }

    public enum SkipSequenceCondition
    {
        DontSkip,
        NoRefuelingAllowed,
        NoChampionshipTier1,
        NoChampionshipTier2,
        NoChampionshipTier3,
        DriverPersonalityTrait,
        NoDriverPersonalityTrait,
        RefuelingAllowed,
        in3DMode,
        in2DMode,
    }

    public enum TargetDriver
    {
        DriverOne,
        DriverTwo,
        Both,
    }
}
