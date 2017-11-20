
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RadioMessageOvertakes : RadioMessage
{
    private float[] mCooldownTimer = new float[3];
    private float mCantOvertakeCooldown = 20f;
    private int mLapWeGotStuckBehindVehicle;
    private RacingVehicle mCantOvertakeThisVehicle;

}
