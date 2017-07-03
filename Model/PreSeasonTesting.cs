using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PreSeasonTesting : Entity
{
  [NonSerialized]
  private readonly float mTestingDuration = 1.5f;
  [NonSerialized]
  private readonly string mTestingCircuit = "Tondela";
  [NonSerialized]
  private readonly float mSimulationSpeed = 300f;
  [NonSerialized]
  private readonly float mSimulationUpdateRate = 1f;
  [NonSerialized]
  private readonly float mMoralePositionRangeDifference = 10f;
  [NonSerialized]
  private readonly float mMoraleEndTestingIncreaseRate = 0.2f;
  private float[] mDriverConfidenceWithCar = new float[Team.mainDriverCount];
  private List<PreSeasonTestingData> mTestingData = new List<PreSeasonTestingData>();
  public Championship championship;
  private PreSeasonTesting.State mState;
  private DateTime mTestingDate;
  private float mSessionTime;
  private float mInititalSessionTime;
  private float mNextDataUpdateTime;
  private float mTimeSinceLastDataUpdate;
  private float mStaffMorale;
  public enum State
  {
    NotStarted,
    Running,
    Finished,
  }
}
