using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Car
{
    public string name = string.Empty;
    public int identifier;
    public CarChassisStats chassisStats = new CarChassisStats();

    private const float totalTimeToRepairCondition = 345600f;
    private const float workRatePerformance = 345600f;
    private const float workRateReliability = 1728000f;
    private const float workRateCondition = 1728000f;

    private bool mRefreshDriverOpinion;
    private float mCarConditionAfterEvent;
    private CarManager mCarManager;
    private CarPart[] mCurrentPart = new CarPart[11];
    private CarPart[] mPartsFittedToCar = new CarPart[0];
    private FrontendCar mFrontendCar;
    private CarStats mCachedStats = new CarStats();

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
