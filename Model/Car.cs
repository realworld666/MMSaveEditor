using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Car
{
    public string name = string.Empty;
    public CarChassisStats chassisStats = new CarChassisStats();
    private CarPart[] mCurrentPart = new CarPart[11];
    private CarPart[] mPartsFittedToCar = new CarPart[0];
    private CarStats mCachedStats = new CarStats();
    private const float totalTimeToRepairCondition = 345600f;
    private const float workRatePerformance = 345600f;
    private const float workRateReliability = 1728000f;
    private const float workRateCondition = 1728000f;
    public int identifier;
    private bool mRefreshDriverOpinion;
    private float mCarConditionAfterEvent;
    private CarManager mCarManager;
    private FrontendCar mFrontendCar;

    public FrontendCarData GetDataForCar(int index)
    {
        if (index == 0)
            return this.mCarManager.frontendCar.data;
        return this.mCarManager.nextFrontendCar.data;
    }

    public CarManager carManager
    {
        get
        {
            return this.mCarManager;
        }
    }
}
