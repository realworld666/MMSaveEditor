using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarManager
{
    public static int carCount = 2;
    private static List<Car> mResultCache = new List<Car>();
    private static List<Car> mAllCarsCache = new List<Car>();
    public CarManagerAIController developmentAIController = new CarManagerAIController();
    public NextYearCarDesign nextYearCarDesign = new NextYearCarDesign();
    public CarPartDesign carPartDesign = new CarPartDesign();
    public CarPartInventory partInventory = new CarPartInventory();
    public PartImprovement partImprovement = new PartImprovement();
    public float designStaffAllocation = 0.5f;
    public float factoryStaffAllocation = 0.5f;
    public WorkingHours designStaffWorkingHours = WorkingHours.Average;
    public WorkingHours factoryStaffWorkingHours = WorkingHours.Average;
    private Team mTeam;
    private Car[] mCar = new Car[CarManager.carCount];
    private Car[] mNextCar = new Car[CarManager.carCount];
    private FrontendCar mFrontendCarThisYear;
    private FrontendCar mFrontendCarNextYear;

    public FrontendCar frontendCar
    {
        get
        {
            //if (this.mFrontendCarThisYear == null)
            //    this.CreateThisYearFrontendCar();
            return this.mFrontendCarThisYear;
        }
    }

    public FrontendCar nextFrontendCar
    {
        get
        {
            //if (this.mFrontendCarNextYear == null)
            //    this.CreateNextYearFrontendCar();
            return this.mFrontendCarNextYear;
        }
    }

    public Team team
    {
        get
        {
            return this.mTeam;
        }
    }

    public Car GetCar(int inIndex)
    {
        return this.mCar[inIndex];
    }

    public enum AutofitAvailabilityOption
    {
        AllParts,
        UnfitedParts,
    }

    public enum AutofitOptions
    {
        Performance,
        Reliability,
    }

    public enum MedianTypes
    {
        Highest,
        Average,
        Lowest,
    }

    public void UnfitAllParts(Car inCar)
    {
        for (int index = 0; index < inCar.seriesCurrentParts.Length; ++index)
        {
            if (inCar.seriesCurrentParts[index] != null)
                inCar.UnfitPart(inCar.seriesCurrentParts[index]);
        }
    }

    public void AutoFit(Car inCar, CarManager.AutofitOptions inOption, CarManager.AutofitAvailabilityOption inAvailabilityOption)
    {
        this.UnfitAllParts(inCar);
        bool flag1 = false;
        switch (inAvailabilityOption)
        {
            case CarManager.AutofitAvailabilityOption.AllParts:
                flag1 = false;
                break;
            case CarManager.AutofitAvailabilityOption.UnfitedParts:
                flag1 = true;
                break;
        }
        CarPartStats.CarPartStat inStat1 = CarPartStats.CarPartStat.MainStat;
        CarPartStats.CarPartStat inStat2 = CarPartStats.CarPartStat.Reliability;
        switch (inOption)
        {
            case CarManager.AutofitOptions.Performance:
                inStat1 = CarPartStats.CarPartStat.MainStat;
                inStat2 = CarPartStats.CarPartStat.Reliability;
                break;
            case CarManager.AutofitOptions.Reliability:
                inStat1 = CarPartStats.CarPartStat.Reliability;
                inStat2 = CarPartStats.CarPartStat.MainStat;
                break;
        }
        CarPart.PartType[] partType = CarPart.GetPartType(this.mTeam.championship.series, false);
        for (int index1 = 0; index1 < partType.Length; ++index1)
        {
            List<CarPart> partInventory = this.partInventory.GetPartInventory(partType[index1]);
            CarPart seriesCurrentPart = inCar.seriesCurrentParts[index1];
            for (int index2 = 0; index2 < partInventory.Count; ++index2)
            {
                if (!flag1 || !partInventory[index2].isFitted)
                {
                    bool flag2 = false;
                    bool flag3 = false;
                    if (seriesCurrentPart != null)
                    {
                        switch (inStat1)
                        {
                            case CarPartStats.CarPartStat.MainStat:
                                flag2 = (double)seriesCurrentPart.stats.statWithPerformance < (double)partInventory[index2].stats.statWithPerformance;
                                flag3 = (double)seriesCurrentPart.stats.statWithPerformance == (double)partInventory[index2].stats.statWithPerformance && (double)seriesCurrentPart.stats.GetStat(inStat2) < (double)partInventory[index2].stats.GetStat(inStat2);
                                break;
                            case CarPartStats.CarPartStat.Reliability:
                                flag2 = (double)seriesCurrentPart.stats.GetStat(inStat1) < (double)partInventory[index2].stats.GetStat(inStat1);
                                flag3 = (double)seriesCurrentPart.stats.GetStat(inStat1) == (double)partInventory[index2].stats.GetStat(inStat1) && (double)seriesCurrentPart.stats.statWithPerformance < (double)partInventory[index2].stats.statWithPerformance;
                                break;
                        }
                    }
                    if ((seriesCurrentPart == null || flag2 || flag3) && inCar.FitPart(partInventory[index2]))
                        seriesCurrentPart = partInventory[index2];
                }
            }
        }
    }

    public void FitPartToFrontendCar(CarPart inPart)
    {
#if false
        if (!this.mTeam.IsPlayersTeam())
            return;
        CarPart highestStatPartOfType = this.partInventory.GetHighestStatPartOfType(inPart.GetPartType());
        if (this.mFrontendCarThisYear == null || this.mFrontendCarNextYear == null /*|| !App.instance.carPartModelDatabase.IsPartValidInChampionship(highestStatPartOfType.modelId, this.mTeam.championship.championshipID)*/)
            return;
        this.mFrontendCarThisYear.FitPart(highestStatPartOfType, App.instance.carPartModelDatabase, false);
        this.mFrontendCarNextYear.FitPart(highestStatPartOfType, App.instance.carPartModelDatabase, false);
        this.mFrontendCarNextYear.SetupBasedOnCurrentData();
#endif
    }
}
