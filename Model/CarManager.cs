using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarManager
{
    public static int carCount = 2;
    public CarManagerAIController developmentAIController = new CarManagerAIController();
    public NextYearCarDesign nextYearCarDesign = new NextYearCarDesign();
    public CarPartDesign carPartDesign = new CarPartDesign();
    public CarPartInventory partInventory = new CarPartInventory();
    public PartImprovement partImprovement = new PartImprovement();
    public float designStaffAllocation = 0.5f;
    public float factoryStaffAllocation = 0.5f;
    public WorkingHours designStaffWorkingHours = WorkingHours.Average;
    public WorkingHours factoryStaffWorkingHours = WorkingHours.Average;
    private Car[] mCar = new Car[CarManager.carCount];
    private Car[] mNextCar = new Car[CarManager.carCount];
    private Team mTeam;
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
}
