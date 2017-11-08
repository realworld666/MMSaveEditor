
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SessionPitstopMistake
{
    private SessionSetupChangeEntry.Target mTaskType = SessionSetupChangeEntry.Target.Count;
    private CarPart.PartType mPartTypeFixed = CarPart.PartType.None;
    private SessionSetupChangeEntry.TyreSlot mTargetTyreSlot = SessionSetupChangeEntry.TyreSlot.BackLeft;
    private TyreSet.Compound mWrongFittedCompound = TyreSet.Compound.Hard;
    private SessionPitstopMistake.MistakeType mMistakeType;
    private float mMistakeTime;
    private PitCrewMember mPitCrewMember;
    private RacingVehicle mVehicleWorkedOn;


    public enum MistakeType
    {
        TimeCost,
        JobSpecific,
        CatastrophicFire,
        CatastrophicLoseWheel,
    }
}
