
using FullSerializer;
using System.Collections.Generic;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PitCrewMember : Person
{
    private PitCrewMember.PitCrewRole mPitCrewRole = PitCrewMember.PitCrewRole.None;
    private readonly float mStatsLostPerRaceAfterPeak = RandomUtility.GetRandom(0.5f, 1.5f);
    private PitCrewMemberStats mStats;
    private PitCrewMemberMistakeLog[] mMistakeLogs;
    public bool mHasCachedReplacementInfo;
    public bool mIsReplacement;
    private bool mHasPassedPeakAge;
    private bool mHasPassedPeakAgeNotification;


    public enum PitCrewRole
    {
        [LocalisationID("PSG_10013430")] FrontJack,
        [LocalisationID("PSG_10013431")] RearJack,
        [LocalisationID("PSG_10013434")] FrontLeftWheel,
        [LocalisationID("PSG_10013435")] FrontRightWheel,
        [LocalisationID("PSG_10013436")] RearLeftWheel,
        [LocalisationID("PSG_10013437")] RearRightWheel,
        [LocalisationID("PSG_10013438")] FrontLeftFixing,
        [LocalisationID("PSG_10013439")] FrontRightFixing,
        [LocalisationID("PSG_10013440")] RearLeftRefueling,
        [LocalisationID("PSG_10013441")] RearRightRefueling,
        Count,
        None,
    }
}
