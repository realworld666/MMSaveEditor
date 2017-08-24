
using FullSerializer;
using System;
using System.Collections.Generic;


[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PartImprovement
{
    public static CarPartStats.CarPartStat[] playerAvailableImprovementTypes = new CarPartStats.CarPartStat[2] { CarPartStats.CarPartStat.Reliability, CarPartStats.CarPartStat.Performance };
    public static CarPartStats.CarPartStat[] allImprovementTypes = new CarPartStats.CarPartStat[2] { CarPartStats.CarPartStat.Reliability, CarPartStats.CarPartStat.Performance };
    public Dictionary<int, List<CarPart>> partsToImprove = new Dictionary<int, List<CarPart>>();
    public Dictionary<int, DateTime> partWorkStartDate = new Dictionary<int, DateTime>();
    public Dictionary<int, DateTime> partWorkEndDate = new Dictionary<int, DateTime>();
    public Dictionary<int, CalendarEvent_v1> endDateCalendarEvents = new Dictionary<int, CalendarEvent_v1>();
    public Dictionary<int, int> mechanics = new Dictionary<int, int>();
    public Person mechanicOnPerformance;
    public Person mechanicOnReliability;
    private bool mIsFixingCondition;
    private Team mTeam;
    private HQsBuilding_v1 mFactory;
    private float mPlayerMechanicsPreference = 0.5f;
    private float mNormalizedMechanicDistribution = 0.5f;
    private bool mRefreshEndDate;
    private CalendarEvent_v1 mConditionCalendarEvent;
    [NonSerialized]
    private List<CarPart> partsDoneCache = new List<CarPart>();
    private DateTime mConditionWorkStartDate = new DateTime();
    private DateTime mConditionWorkEndDate = new DateTime();
    public const int workSeconds = 32400;
    public Action OnItemListsChangedForUI;

    public void AssignChiefMechanics()
    {
        List<Person> allPeopleOnJob = this.mTeam.contractManager.GetAllPeopleOnJob(Contract.Job.Mechanic);
        if (this.mechanicOnPerformance == null || this.mechanicOnPerformance.Contract.Job1 == Contract.Job.Unemployed)
        {
            for (int index = 0; index < allPeopleOnJob.Count; ++index)
            {
                if (this.mechanicOnReliability == null || allPeopleOnJob[index] != this.mechanicOnReliability)
                    this.mechanicOnPerformance = allPeopleOnJob[index];
            }
        }
        if (this.mechanicOnReliability == null || this.mechanicOnReliability.Contract.Job1 == Contract.Job.Unemployed)
        {
            for (int index = 0; index < allPeopleOnJob.Count; ++index)
            {
                if (this.mechanicOnPerformance == null || allPeopleOnJob[index] != this.mechanicOnPerformance)
                    this.mechanicOnReliability = allPeopleOnJob[index];
            }
        }
    }
}
