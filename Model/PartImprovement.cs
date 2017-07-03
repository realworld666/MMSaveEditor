
using FullSerializer;
using System;
using System.Collections.Generic;


[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PartImprovement
{
  public static CarPartStats.CarPartStat[] playerAvailableImprovementTypes = new CarPartStats.CarPartStat[2]{ CarPartStats.CarPartStat.Reliability, CarPartStats.CarPartStat.Performance };
  public static CarPartStats.CarPartStat[] allImprovementTypes = new CarPartStats.CarPartStat[2]{ CarPartStats.CarPartStat.Reliability, CarPartStats.CarPartStat.Performance };
  public Dictionary<int, List<CarPart>> partsToImprove = new Dictionary<int, List<CarPart>>();
  public Dictionary<int, DateTime> partWorkStartDate = new Dictionary<int, DateTime>();
  public Dictionary<int, DateTime> partWorkEndDate = new Dictionary<int, DateTime>();
  public Dictionary<int, CalendarEvent_v1> endDateCalendarEvents = new Dictionary<int, CalendarEvent_v1>();
  public Dictionary<int, int> mechanics = new Dictionary<int, int>();
  private float mPlayerMechanicsPreference = 0.5f;
  private float mNormalizedMechanicDistribution = 0.5f;
  [NonSerialized]
  private List<CarPart> partsDoneCache = new List<CarPart>();
  private DateTime mConditionWorkStartDate = new DateTime();
  private DateTime mConditionWorkEndDate = new DateTime();
  public const int workSeconds = 32400;
  public Action OnItemListsChangedForUI;
  public Person mechanicOnPerformance;
  public Person mechanicOnReliability;
  private bool mIsFixingCondition;
  private Team mTeam;
  private HQsBuilding_v1 mFactory;
  private bool mRefreshEndDate;
  private CalendarEvent_v1 mConditionCalendarEvent;
    
}
