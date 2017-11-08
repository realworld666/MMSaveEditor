
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PitstopDirector
{
    private static bool DEBUG_mCatastrophicMistakeActive;
    private bool mHasCatastrophicMistakeHappened;
    private PitstopDirector.CatastrophicMistakeType mCatastrophicMistakeType;
    private RacingVehicle mVehicleWithCatastrophicMistake;



    public enum CatastrophicMistakeType
    {
        PitStopFire,
        LoseWheel,
    }
}
