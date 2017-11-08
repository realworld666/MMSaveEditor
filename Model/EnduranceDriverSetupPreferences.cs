
using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class EnduranceDriverSetupPreferences
{
    private Team mTeam;
    private Driver[] mCar1DriverList;
    private float[] mCar1DriverTimeCost;
    private Driver[] mCar2DriverList;
    private float[] mCar2DriverTimeCost;
    private float mCar1SetupPosition01 = 0.5f;
    private float mCar2SetupPosition01 = 0.5f;
    private bool mHasSeenNewDriverOrder;


}
