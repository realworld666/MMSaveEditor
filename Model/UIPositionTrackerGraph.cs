// Decompiled with JetBrains decompiler
// Type: UIPositionTrackerGraph
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPositionTrackerGraph : MonoBehaviour
{
  public int numberOfLapsDisplayed = 11;
  public int currentLapIndex = 8;
  private Dictionary<Driver, UIPositionTrackerGraph.TrackerData> mData = new Dictionary<Driver, UIPositionTrackerGraph.TrackerData>();
  private UIPositionTrackerGraph.TrackerData[] mTrackData = new UIPositionTrackerGraph.TrackerData[20];
  public UIPositionTrackerDriver[] drivers;
  public UIPositionTrackerLap[] laps;
  public UIPositionTrackerLine[] lines;
  public Button leftButton;
  public Button rightButton;
  public PositionTrackerScreen screen;
  private int mCurrentLap;
  private int mMaxLap;
  private int mFirstLap;
  private int mLastLap;
  private int mTotalLaps;

  public UIPositionTrackerGraph.TrackerData[] trackerData
  {
    get
    {
      return this.mTrackData;
    }
    set
    {
      this.mTrackData = value;
    }
  }

  public int currentLap
  {
    get
    {
      return this.mCurrentLap;
    }
  }

  public void OnStart()
  {
    for (int index = 0; index < this.mTrackData.Length; ++index)
      this.mTrackData[index] = new UIPositionTrackerGraph.TrackerData();
    this.leftButton.onClick.AddListener(new UnityAction(this.OnLeftButton));
    this.rightButton.onClick.AddListener(new UnityAction(this.OnRightButton));
  }

  public void Setup()
  {
    this.mData.Clear();
    this.mCurrentLap = 0;
    this.mMaxLap = 0;
    this.mTotalLaps = Game.instance.sessionManager.lapCount + 1;
    if (this.numberOfLapsDisplayed > this.mTotalLaps)
      this.numberOfLapsDisplayed = this.mTotalLaps;
    if (this.currentLapIndex > this.numberOfLapsDisplayed)
      this.currentLapIndex = Mathf.Max(this.numberOfLapsDisplayed - 2, 0);
    for (int index = 0; index < this.mTrackData.Length; ++index)
      this.mTrackData[index].Reset();
    this.LoadTrackerData();
    this.mMaxLap = Game.instance.sessionManager.hasSessionEnded ? Game.instance.sessionManager.lap : Game.instance.sessionManager.lap - 1;
    this.mCurrentLap = Mathf.Max(this.currentLapIndex, this.mMaxLap);
    this.UpdateGraphics();
  }

  public void UpdateGraphics()
  {
    this.mFirstLap = Mathf.Clamp(this.mCurrentLap - this.currentLapIndex, 0, this.mTotalLaps - this.numberOfLapsDisplayed);
    this.mLastLap = Mathf.Clamp(this.mFirstLap + this.numberOfLapsDisplayed, this.numberOfLapsDisplayed, this.mTotalLaps);
    for (int index = 0; index < this.laps.Length; ++index)
    {
      GameUtility.SetActive(this.laps[index].gameObject, index < this.numberOfLapsDisplayed);
      this.laps[index].Setup(this.mFirstLap + index, this.mMaxLap, this.mTotalLaps - 1);
    }
    for (int index1 = 0; index1 < this.lines.Length; ++index1)
    {
      this.lines[index1].Setup(this, this.mTrackData[index1], this.mFirstLap, this.mLastLap);
      UIPositionTrackerGraph.TrackerData trackerData = this.mTrackData[index1];
      int index2 = this.mFirstLap >= trackerData.lapsData.Count ? trackerData.lapsData.Count - 1 : this.mFirstLap;
      UIPositionTrackerGraph.LapData lapData = trackerData.lapsData[index2];
      int index3 = !lapData.isOutOfRace ? lapData.position : lapData.retiredPosition;
      if (index3 >= 0 && index3 < this.drivers.Length)
        this.drivers[index3].Setup(trackerData.driver);
    }
    this.UpdateButtonsState();
  }

  public void UpdateLine(UIPositionTrackerGraph.TrackerData inData)
  {
    this.mFirstLap = Mathf.Clamp(this.mCurrentLap - this.currentLapIndex, 0, this.mTotalLaps - this.numberOfLapsDisplayed);
    this.mLastLap = Mathf.Clamp(this.mFirstLap + this.numberOfLapsDisplayed, this.numberOfLapsDisplayed, this.mTotalLaps);
    for (int index = 0; index < this.mTrackData.Length; ++index)
    {
      if (this.mTrackData[index] == inData)
        this.lines[index].Setup(this, inData, this.mFirstLap, this.mLastLap);
    }
  }

  public void LoadTrackerData()
  {
    int count1 = Game.instance.sessionManager.standings.Count;
    for (int index1 = 0; index1 < count1; ++index1)
    {
      RacingVehicle standing = Game.instance.sessionManager.standings[index1];
      int index2 = standing.stats.qualifyingPosition - 1;
      if (index2 >= 0 && index2 < this.mTrackData.Length)
      {
        this.mTrackData[index2].vehicle = standing;
        this.mTrackData[index2].driver = standing.driver;
        this.mTrackData[index2].lapsData.Add(new UIPositionTrackerGraph.LapData()
        {
          position = index2,
          lapTime = 0.0f,
          totalTime = 0.0f
        });
        this.mData.Add(this.mTrackData[index2].driver, this.mTrackData[index2]);
      }
    }
    GateInfo startFinishGate = Game.instance.sessionManager.GetStartFinishGate();
    int count2 = startFinishGate.lapData.Count;
    for (int index1 = 0; index1 < count2; ++index1)
    {
      GateInfo.LapData lapData1 = startFinishGate.lapData[index1];
      for (int index2 = 0; index2 < lapData1.entries.Count; ++index2)
      {
        UIPositionTrackerGraph.TrackerData trackerData = this.GetTrackerData(lapData1.entries[index2].vehicle.driver);
        UIPositionTrackerGraph.LapData lapData2 = new UIPositionTrackerGraph.LapData();
        lapData2.lapTime = index1 >= trackerData.vehicle.timer.lapData.Count ? 0.0f : trackerData.vehicle.timer.lapData[index1].time;
        trackerData.totalTime += lapData2.lapTime;
        lapData2.totalTime = trackerData.totalTime;
        lapData2.position = index2;
        trackerData.lapsData.Add(lapData2);
        if (index2 == count1 - 1)
          this.mMaxLap = index1;
      }
    }
    for (int index1 = 0; index1 < this.mTrackData.Length; ++index1)
    {
      UIPositionTrackerGraph.TrackerData trackerData = this.mTrackData[index1];
      RacingVehicle vehicle = this.mTrackData[index1].vehicle;
      if (vehicle != null)
      {
        if (vehicle.behaviourManager.isCrashed)
        {
          trackerData.lapsData[trackerData.lapsData.Count - 1].crashed = true;
          trackerData.lapsData[trackerData.lapsData.Count - 1].retiredPosition = vehicle.standingsPosition - 1;
        }
        else if (vehicle.behaviourManager.isRetired)
        {
          trackerData.lapsData[trackerData.lapsData.Count - 1].retired = true;
          trackerData.lapsData[trackerData.lapsData.Count - 1].retiredPosition = vehicle.standingsPosition - 1;
        }
        int count3 = vehicle.timer.pitstopData.Count;
        for (int index2 = 0; index2 < count3; ++index2)
        {
          SessionTimer.PitstopData pitstopData = vehicle.timer.pitstopData[index2];
          if (pitstopData.lapNumber < trackerData.lapsData.Count)
          {
            UIPositionTrackerGraph.LapData lapData = trackerData.lapsData[pitstopData.lapNumber];
            lapData.pitted = true;
            lapData.pitlaneTime = pitstopData.pitlaneTime;
            lapData.stopTime = pitstopData.stopTime;
            lapData.estimatedPitStopTime = pitstopData.estimatedPitStopTime;
            lapData.entrancePosition = pitstopData.entrancePosition;
            lapData.exitPosition = pitstopData.exitPosition;
          }
        }
      }
    }
  }

  public UIPositionTrackerGraph.TrackerData GetTrackerData(Driver inDriver)
  {
    if (this.mData.ContainsKey(inDriver))
      return this.mData[inDriver];
    return (UIPositionTrackerGraph.TrackerData) null;
  }

  private void OnLeftButton()
  {
    this.mCurrentLap = Mathf.Clamp(MathsUtility.PreviousMultipleOf(this.mFirstLap + this.currentLapIndex, 2), this.currentLapIndex, this.mTotalLaps - (this.numberOfLapsDisplayed - this.currentLapIndex));
    this.UpdateGraphics();
  }

  private void OnRightButton()
  {
    this.mCurrentLap = Mathf.Clamp(MathsUtility.NextMultipleOf(this.mLastLap - (this.numberOfLapsDisplayed - this.currentLapIndex), 2), this.currentLapIndex, this.mTotalLaps - (this.numberOfLapsDisplayed - this.currentLapIndex));
    this.UpdateGraphics();
  }

  private void UpdateButtonsState()
  {
    this.leftButton.interactable = this.mCurrentLap > this.currentLapIndex;
    this.rightButton.interactable = this.mCurrentLap < this.mTotalLaps - (this.numberOfLapsDisplayed - this.currentLapIndex);
  }

  public class LapData
  {
    public int position;
    public float lapTime;
    public float totalTime;
    public float estimatedPitStopTime;
    public float pitlaneTime;
    public float stopTime;
    public int entrancePosition;
    public int exitPosition;
    public int retiredPosition;
    public bool retired;
    public bool crashed;
    public bool pitted;

    public bool isOutOfRace
    {
      get
      {
        if (!this.retired)
          return this.crashed;
        return true;
      }
    }
  }

  public class TrackerData
  {
    public List<UIPositionTrackerGraph.LapData> lapsData = new List<UIPositionTrackerGraph.LapData>();
    public RacingVehicle vehicle;
    public Driver driver;
    public float totalTime;
    public bool isSelected;

    public void Reset()
    {
      this.vehicle = (RacingVehicle) null;
      this.driver = (Driver) null;
      this.totalTime = 0.0f;
      this.lapsData.Clear();
    }
  }
}
