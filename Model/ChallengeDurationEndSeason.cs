
using FullSerializer;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChallengeDurationEndSeason : ChallengeDuration
{
    private int mChampionshipID = -1;
    private int mEventNumber = -1;
    private int mEventCount = -1;
    private int mSeasonsLeft;

}
