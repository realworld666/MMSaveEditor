using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PitStopRaceLog
{
    private float mFastestPitStop = float.MaxValue;
    private List<float> mPitstopTimes = new List<float>();
    private int mNumberOfPitstops;
    private int mNumberOfMistakes;
    private int mNumberOfCatastrophicMistakes;
    private bool mCatastrophicFireHappened;

}
