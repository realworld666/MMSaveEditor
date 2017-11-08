
using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class VehicleManager
{
    public static int maxVehicleCount;

    private List<RacingVehicle> mVehicles = new List<RacingVehicle>();
    private SafetyVehicle mSafetyVehicle;
    private RacingVehicle[] mPlayerVehicles;


    public enum Visuals
    {
        Create,
        DontCreate,
    }
}
