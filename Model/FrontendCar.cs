using FullSerializer;
using System;
using System.Collections.Generic;
using System.Diagnostics;

[fsObject(MemberSerialization = fsMemberSerialization.OptIn)]
public class FrontendCar
{
    private static readonly int numSponsors = 6;
    private static readonly string liveryShaderName = "Custom/LiveryShader";
    private static readonly string sponsorShaderName = "Custom/Car_Sponsors";
    public static readonly string frontWingSocketName = "Front_Wing";
    public static readonly string rearWingSocketName = "Rear_Wing";
    public static readonly string seatSocketName = "Seat";
    public static readonly string steeringWheelSocketName = "Steering_Wheel";
    public static readonly string[] wheelSocketNames = new string[4] { "FL_Wheel", "FR_Wheel", "RL_Wheel", "RR_Wheel" };
    [fsProperty]
    private FrontendCarData mData = new FrontendCarData();
    private int[] blendShapeIndexMapping = new int[5] { -1, -1, -1, -1, -1 };
    private CarPart[] mCurrentPart = new CarPart[11];
    private int mChampionshipID = -1;
    private int mTeamID = -1;
    private FrontendCarManager mManager;


    public FrontendCarData data
    {
        get
        {
            return this.mData;
        }
    }
}
