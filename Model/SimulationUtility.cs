using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SimulationUtility
{
    private List<SimulationUtility.DriverData> mDriverData = new List<SimulationUtility.DriverData>();
    private List<RaceEventResults.ResultData> mPreviousSessionResultData = new List<RaceEventResults.ResultData>();
    private const float driverContributionWithAids = 0.05f;
    private const float driverContributionWithoutAids = 0.1f;
    private const float carTotalStatsContribution = 0.75f;
    private const float carTrackSpecificStatsContribution = 0.25f;
    private const float timeOnTrackModifier = 0.8f;
    private const int minPitStops = 2;
    private const int maxPitStops = 4;

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class DriverData
    {
        public SimulationUtility.DriverData.DriverForm driverForm = SimulationUtility.DriverData.DriverForm.Neutral;
        public Driver driver;
        public float driverQuality01;
        public Car car;
        public float carQuality01;
        public float averageLap;
        private float mOverallQuality01;



        public enum DriverForm
        {
            Good,
            Neutral,
            Bad,
        }
    }

    public enum ReorderType
    {
        LapTime,
        SessionTime,
    }
}
