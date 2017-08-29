
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class DilemmaSystem
{
    private readonly float mTransactionScalar = 1000000f;
    public List<CarPart.PartType> carPartsLeveledUp = new List<CarPart.PartType>();
    private readonly int mMaxDaysRange = 50;
    private DateTime mTriggerDilemmaDate = new DateTime();
    private float[] mDilemmaProbability = new float[6];
    private readonly float mDilemmaProbabilityIncrease = 0.05f;
    private readonly int mMaxPreSeasonDaysRange = 30;
    private DateTime mTriggerPreSeasonDilemmaDate = new DateTime();
    public DilemmaSystem.BribedOption currentBribe;
    private int mLastRandomDayValue;
    private int mLastRandomPreSeasonDayValue;
    private int mLastBirthdayDilemmaYear;
    private float mPromiseDilemmaProbability;
    private bool mWasPreSeason;
    private bool mIsDilemmaFiring;
    private bool mGotFineTransaction;
    private DilemmaChallengeSystem mDilemmaChallengeSystem;

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class DilemmaMessageData
    {
        public CarPart.PartType carPartTypeSubject = CarPart.PartType.None;
        public CarPart.PartType carPartNotMaxPerformaceSubject = CarPart.PartType.None;
        public CarPart.PartType carPartTypeNotOriginalSubject = CarPart.PartType.None;
        public Person stringVariableParserSubject;
        public Driver dilemmaBirthdayDriver;
        public Mechanic dilemmaRandomMechanic;
        public Driver dilemmaDriverPromisedTo;
        public CarPart partAffectedDilemma;
        public Team randomTeam;
    }

    public enum BribedOption
    {
        None,
        InFavor,
        Against,
        Abstain,
    }

    public enum DilemmaType
    {
        EngineCoolingSystem,
        NonSpecParts,
        NonOriginalParts,
        NoMaxPerformanceParts,
        Bribe,
        FireRandomMechanic,
        Count,
    }
}
