using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Car
{
    public string name = string.Empty;
    public int identifier;
    private CarChassisStats chassisStats = new CarChassisStats();

    private const float totalTimeToRepairCondition = 345600f;
    private const float workRatePerformance = 345600f;
    private const float workRateReliability = 1728000f;
    private const float workRateCondition = 1728000f;

    private bool mRefreshDriverOpinion;
    private float mCarConditionAfterEvent;
    private CarManager mCarManager;
    private CarPart[] mCurrentPart = new CarPart[17];
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

    public CarPart[] seriesCurrentParts
    {
        get
        {
            if (this.mPartsFittedToCar.Length == 0)
                this.UpdatePartsFittedToCarArray();
            return this.mPartsFittedToCar;
        }
    }

    public CarChassisStats ChassisStats
    {
        get { return chassisStats; }
        set { chassisStats = value; }
    }

    private void UpdatePartsFittedToCarArray()
    {
        CarPart.PartType[] partType1 = CarPart.GetPartType(this.mCarManager.team.championship.series, false);
        this.mPartsFittedToCar = new CarPart[partType1.Length];
        for (int index = 0; index < partType1.Length; ++index)
        {
            CarPart.PartType partType2 = partType1[index];
            this.mPartsFittedToCar[index] = this.mCurrentPart[(int)partType2];
        }
    }

    public void UnfitPart(CarPart inPart)
    {
        for (int index = 0; index < this.mCurrentPart.Length; ++index)
        {
            if (this.mCurrentPart[index] != null && this.mCurrentPart[index] == inPart)
            {
                if (this.carManager.team.GetDriver(this.identifier) != null)
                    this.mRefreshDriverOpinion = true;
                this.mCurrentPart[index].isFitted = false;
                this.mCurrentPart[index].fittedCar = (Car)null;
                this.mCurrentPart[index] = (CarPart)null;
            }
        }
        this.UpdatePartsFittedToCarArray();
    }

    public bool FitPart(CarPart inPart)
    {
        if (inPart.IsBanned)
            return false;
        for (int inIndex = 0; inIndex < CarManager.carCount; ++inIndex)
            this.carManager.GetCar(inIndex).UnfitPart(inPart);
        if (this.mCurrentPart[(int)inPart.GetPartType()] != null)
        {
            this.mCurrentPart[(int)inPart.GetPartType()].isFitted = false;
            this.mCurrentPart[(int)inPart.GetPartType()].fittedCar = (Car)null;
        }
        this.mCurrentPart[(int)inPart.GetPartType()] = inPart;
        inPart.isFitted = true;
        inPart.fittedCar = this;
        this.mCarManager.FitPartToFrontendCar(inPart);
        if (this.carManager.team.GetDriver(this.identifier) != null)
            this.mRefreshDriverOpinion = true;
        this.UpdatePartsFittedToCarArray();
        this.UpdatePartFittedAchievments();
        return true;
    }

    private void UpdatePartFittedAchievments()
    {
        //throw new NotImplementedException();
    }
}
