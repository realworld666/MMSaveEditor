// Decompiled with JetBrains decompiler
// Type: SimulationLapTimeModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class SimulationLapTimeModel
{
  private static float bodgeFactor = 1.111111f;

  public static TimeSpan EstimateLapTime(CarStats inStats, CircuitScene inCircuit)
  {
    List<PathData.Gate> gates = inCircuit.GetTrackPath().data.gates;
    Debug.AssertFormat((gates.Count > 0 ? 1 : 0) != 0, "No Gates found for track: {0}!", (object) inCircuit.name);
    return SimulationLapTimeModel.CalculateTimeBetweenGates(gates[0], gates[gates.Count - 1], inCircuit, inStats, true);
  }

  public static TimeSpan EstimateDriveThroughPenaltyTime(CircuitScene inCircuit, float inPitLaneSpeed, CarStats inChasingCar, CarStats inPittingCar = null)
  {
    SimulationLapTimeModel.StraightZoneData straightZoneData = new SimulationLapTimeModel.StraightZoneData();
    TimeSpan timeSpan1 = new TimeSpan();
    if (inPittingCar == null)
      inPittingCar = inChasingCar;
    PathData data = inCircuit.GetTrackPath().data;
    PathData.Gate gateWithId1 = PathUtility.GetGateWithID(inCircuit.pitlaneEntryTrackPathID, data);
    PathData.Gate gateWithId2 = PathUtility.GetGateWithID(inCircuit.pitlaneExitTrackPathID, data);
    TimeSpan timeBetweenGates = SimulationLapTimeModel.CalculateTimeBetweenGates(gateWithId1, gateWithId2, inCircuit, inChasingCar, false);
    float inTotalDistance1 = inCircuit.pitlaneEntryPath.data.length / 2f;
    float inTotalDistance2 = inCircuit.pitlaneExitPath.data.length / 2f;
    straightZoneData.SetupStraightZoneData(SimulationLapTimeModel.EstimateSpeedIntoGate(gateWithId1, data, inPittingCar), inTotalDistance1, inPittingCar, inPitLaneSpeed);
    TimeSpan timeSpan2 = timeSpan1 + straightZoneData.GetTotalStraightTime();
    straightZoneData.SetupStraightZoneData(inPitLaneSpeed, inCircuit.pitlanePath.data.length + inTotalDistance1 + inTotalDistance2);
    TimeSpan timeSpan3 = timeSpan2 + straightZoneData.GetTotalStraightTime();
    straightZoneData.SetupStraightZoneData(inPitLaneSpeed, inTotalDistance2, inPittingCar, inPittingCar.topSpeed);
    TimeSpan timeSpan4 = timeSpan3 + straightZoneData.GetTotalStraightTime();
    if (timeBetweenGates > timeSpan4)
      Debug.LogWarningFormat("Calculated the pit lane time as: {0}s, track time as {1}s on track {2}", (object) timeSpan4.TotalSeconds, (object) timeBetweenGates, (object) inCircuit.name);
    Debug.LogFormat("Total time to travel down pit lane: {0}s, total time to travel down main straight:{1}s", new object[2]
    {
      (object) timeSpan4.TotalSeconds,
      (object) timeBetweenGates
    });
    return timeSpan4 - timeBetweenGates;
  }

  public float CalculatePitLaneSpeedLimit(CircuitScene inCircuit, CarStats inAverageStats, float inPitLaneTimeCostSeconds)
  {
    float num1 = 1f;
    float num2 = 0.5f;
    float f = num2;
    float inPitLaneSpeed = 50f;
    TimeSpan timeSpan = new TimeSpan();
    int num3 = 0;
    while ((double) Mathf.Abs(f) >= (double) num2)
    {
      timeSpan = SimulationLapTimeModel.EstimateDriveThroughPenaltyTime(inCircuit, inPitLaneSpeed, inAverageStats, (CarStats) null);
      f = Mathf.Abs(inPitLaneTimeCostSeconds - (float) timeSpan.TotalSeconds);
      if ((double) f > 0.0)
      {
        inPitLaneSpeed += num1;
        if (num3 == -1)
        {
          num1 /= 2f;
          num3 = 0;
        }
        else
          num3 = 1;
      }
      else
      {
        inPitLaneSpeed -= num1;
        if (num3 == 1)
        {
          num1 /= 2f;
          num3 = 0;
        }
        else
          num3 = -1;
      }
    }
    Debug.LogFormat("Speed Limit For {0} Pit Lane Found at: {1}kmph for a pit lane drive through cost of {2}s.", (object) inCircuit.name, (object) inPitLaneSpeed, (object) timeSpan.TotalSeconds);
    return inPitLaneSpeed;
  }

  public static string DebugEstimateLapTimes()
  {
    if (Game.instance == null || Game.instance.sessionManager == null || ((UnityEngine.Object) Game.instance.sessionManager.circuit == (UnityEngine.Object) null || Game.instance.sessionManager.standings.Count == 0))
      return (string) null;
    List<RacingVehicle> standings = Game.instance.sessionManager.standings;
    TimeSpan timeSpan1 = new TimeSpan(100, 0, 0);
    TimeSpan timeSpan2 = new TimeSpan(0, 0, 0);
    TimeSpan timeSpan3 = new TimeSpan(0, 0, 0);
    for (int index = 0; index < standings.Count; ++index)
    {
      TimeSpan timeSpan4 = SimulationLapTimeModel.EstimateLapTime(standings[index].performance.GetRacingPerformance(), Game.instance.sessionManager.circuit);
      timeSpan1 = !(timeSpan1 > timeSpan4) ? timeSpan1 : timeSpan4;
      timeSpan2 = !(timeSpan2 < timeSpan4) ? timeSpan2 : timeSpan4;
      timeSpan3 += timeSpan4;
    }
    TimeSpan timeSpan5 = TimeSpan.FromMilliseconds(timeSpan3.TotalMilliseconds / (double) standings.Count);
    return "Fastest Lap Time: " + (object) timeSpan1.Hours + ":" + (object) timeSpan1.Minutes + ":" + (object) timeSpan1.Seconds + "\n Average lap time: " + (object) timeSpan5.Hours + ":" + (object) timeSpan5.Minutes + ":" + (object) timeSpan5.Seconds + "\n Slowest Lap Time: " + (object) timeSpan2.Hours + ":" + (object) timeSpan2.Minutes + ":" + (object) timeSpan2.Seconds;
  }

  public static TimeSpan CalculateTimeBetweenGates(PathData.Gate inStartGate, PathData.Gate inEndGate, CircuitScene inCircuit, CarStats inStats, bool inclusiveOfLastGate = false)
  {
    PathData data = inCircuit.GetTrackPath().data;
    SimulationLapTimeModel.StraightZoneData straightZoneData = new SimulationLapTimeModel.StraightZoneData();
    if (data.gates.Count == 0)
      Debug.LogError((object) "No gates on track!", (UnityEngine.Object) null);
    if (data.corners.Count == 0)
      return TimeSpan.FromSeconds((double) PhysicsUtility.GetTimeToTravelDistance(PathUtility.GetDistanceBetweenGates(data, inStartGate.id, inEndGate.id), inStats.topSpeed));
    TimeSpan timeSpan1 = new TimeSpan(0, 0, 0);
    float inStartingSpeed = SimulationLapTimeModel.EstimateSpeedIntoGate(inStartGate, data, inStats);
    float num = 0.0f;
    PathData.Gate gate1;
    float outCurrentSpeed;
    SimulationLapTimeModel.CalculateTimeFromGateToNextKeyGate(inStartGate, inStartingSpeed, inEndGate, data, inStats, ref straightZoneData, out gate1, out num, out outCurrentSpeed);
    TimeSpan timeSpan2 = timeSpan1 + TimeSpan.FromSeconds((double) num);
    if (gate1 == inEndGate)
      return timeSpan2;
    PathData.Gate gate2 = gate1;
    SimulationLapTimeModel.CalculateTimeBetweenKeyGates(gate2, data, inStats, ref outCurrentSpeed, ref straightZoneData, out gate1, out num);
    while (!PathUtility.IsGateInBetweenOthersInclusive(gate2.id, gate1.id, inEndGate.id))
    {
      timeSpan2 += TimeSpan.FromSeconds((double) num);
      gate2 = gate1;
      SimulationLapTimeModel.CalculateTimeBetweenKeyGates(gate2, data, inStats, ref outCurrentSpeed, ref straightZoneData, out gate1, out num);
    }
    return TimeSpan.FromMilliseconds((gate1.id != inEndGate.id ? timeSpan2 + SimulationLapTimeModel.CalculateTimeBetweenKeyGateAndEndGate(gate2, inEndGate, data, inStats, ref outCurrentSpeed, ref straightZoneData) : timeSpan2 + TimeSpan.FromSeconds((double) num)).TotalMilliseconds * (double) SimulationLapTimeModel.bodgeFactor);
  }

  private static void CalculateTimeBetweenKeyGates(PathData.Gate inStartingKeyGate, PathData inPathData, CarStats inStats, ref float currentSpeed, ref SimulationLapTimeModel.StraightZoneData straightZoneData, out PathData.Gate nextKeyGate, out float sectionTime)
  {
    if (inStartingKeyGate.corner == null)
      Debug.LogWarning((object) "Cannot process track section which does not start on a corner", (UnityEngine.Object) null);
    PathData.Corner corner = inStartingKeyGate.corner;
    straightZoneData.Clear();
    int transitionGateId1 = PathUtility.GetCornerTransitionGateID(inPathData, inStartingKeyGate.corner, PathUtility.ZoneType.Accelerating);
    int transitionGateId2 = PathUtility.GetCornerTransitionGateID(inPathData, inStartingKeyGate.corner, PathUtility.ZoneType.Braking);
    if (transitionGateId1 == inStartingKeyGate.id)
    {
      PathData.Corner inCorner;
      float inTotalDistance;
      if (corner.nextStraight != null)
      {
        inCorner = corner.nextStraight.nextCorner;
        inTotalDistance = corner.nextStraight.length + SimulationLapTimeModel.GetCornerSpeedChangeDistance(inPathData, corner, PathUtility.ZoneType.Accelerating, -1) + SimulationLapTimeModel.GetCornerSpeedChangeDistance(inPathData, inCorner, PathUtility.ZoneType.Braking, -1);
      }
      else
      {
        inCorner = inPathData.corners[PathUtility.WrapIndex(corner.id + 1, inPathData.corners.Count)];
        inTotalDistance = SimulationLapTimeModel.GetCornerSpeedChangeDistance(inPathData, corner, PathUtility.ZoneType.Accelerating, -1) + SimulationLapTimeModel.GetCornerSpeedChangeDistance(inPathData, inCorner, PathUtility.ZoneType.Braking, -1);
      }
      straightZoneData.SetupStraightZoneData(currentSpeed, SimulationLapTimeModel.GetClampedCorneringSpeed(inStats, inCorner), inTotalDistance, inStats);
      sectionTime = (float) straightZoneData.GetTotalStraightTime().TotalSeconds;
      currentSpeed = straightZoneData.cornerEntrySpeed;
      nextKeyGate = PathUtility.GetGateWithID(PathUtility.GetCornerTransitionGateID(inPathData, inCorner, PathUtility.ZoneType.Braking), inPathData);
    }
    else if (transitionGateId2 == inStartingKeyGate.id)
    {
      nextKeyGate = PathUtility.GetGateWithID(PathUtility.GetCornerTransitionGateID(inPathData, inStartingKeyGate.corner, PathUtility.ZoneType.Accelerating), inPathData);
      straightZoneData.SetupStraightZoneData(currentSpeed, PathUtility.GetDistanceBetweenGates(inPathData, inStartingKeyGate.id, nextKeyGate.id), inStats, SimulationLapTimeModel.GetClampedCorneringSpeed(inStats, corner));
      sectionTime = (float) straightZoneData.GetTotalStraightTime().TotalSeconds;
      currentSpeed = straightZoneData.cornerEntrySpeed;
    }
    else
    {
      Debug.LogWarning((object) "Could not process a lap segement", (UnityEngine.Object) null);
      nextKeyGate = (PathData.Gate) null;
      sectionTime = 0.0f;
    }
  }

  private static void CalculateTimeFromGateToNextKeyGate(PathData.Gate inGate, float inStartingSpeed, PathData.Gate inLimitGate, PathData inPathData, CarStats inCarStats, ref SimulationLapTimeModel.StraightZoneData straightZoneData, out PathData.Gate outGateReached, out float outTimeSeconds, out float outCurrentSpeed)
  {
    PathData.Gate nextKeyGate = PathUtility.GetNextKeyGate(inGate, inPathData);
    bool flag = !PathUtility.IsGateInBetweenOthersInclusive(inGate.id, nextKeyGate.id, inLimitGate.id);
    straightZoneData.Clear();
    float distanceBetweenGates;
    if (flag)
    {
      distanceBetweenGates = PathUtility.GetDistanceBetweenGates(inPathData, inGate.id, nextKeyGate.id);
      outGateReached = nextKeyGate;
    }
    else
    {
      distanceBetweenGates = PathUtility.GetDistanceBetweenGates(inPathData, inGate.id, inLimitGate.id);
      outGateReached = inLimitGate;
    }
    if (inGate.corner != null)
    {
      switch (PathUtility.GetCornerGateZoneRegion(inGate, inPathData))
      {
        case PathUtility.ZoneType.Accelerating:
          straightZoneData.SetupStraightZoneData(inStartingSpeed, distanceBetweenGates, inCarStats);
          break;
        case PathUtility.ZoneType.Constant:
          Debug.AssertFormat((Mathf.Approximately(inStartingSpeed, SimulationLapTimeModel.GetClampedCorneringSpeed(inCarStats, inGate.corner)) ? 1 : 0) != 0, "Starting on a corner with speed of {0}! Not at the cornering speed of {1}!", (object) inStartingSpeed, (object) SimulationLapTimeModel.GetClampedCorneringSpeed(inCarStats, inGate.corner));
          straightZoneData.SetupStraightZoneData(inStartingSpeed, distanceBetweenGates);
          break;
        default:
          straightZoneData.SetupStraightZoneData(inStartingSpeed, SimulationLapTimeModel.GetClampedCorneringSpeed(inCarStats, inGate.corner), distanceBetweenGates, inCarStats);
          break;
      }
    }
    else if (inGate.straight != null)
    {
      if (flag)
        straightZoneData.SetupStraightZoneData(inStartingSpeed, SimulationLapTimeModel.GetClampedCorneringSpeed(inCarStats, inGate.straight.nextCorner), distanceBetweenGates, inCarStats);
      else
        straightZoneData.SetupStraightZoneData(inStartingSpeed, SimulationLapTimeModel.EstimateSpeedIntoGate(inLimitGate, inPathData, inCarStats), distanceBetweenGates, inCarStats);
    }
    outTimeSeconds = (float) straightZoneData.GetTotalStraightTime().TotalSeconds;
    outCurrentSpeed = straightZoneData.cornerEntrySpeed;
  }

  private static TimeSpan CalculateTimeBetweenKeyGateAndEndGate(PathData.Gate inKeyGate, PathData.Gate inEndGate, PathData inPathData, CarStats inCarStats, ref float currentSpeed, ref SimulationLapTimeModel.StraightZoneData straightZoneData)
  {
    Debug.Assert(PathUtility.IsGateInBetweenOthersInclusive(inKeyGate.id, PathUtility.GetNextKeyGate(inKeyGate, inPathData).id, inEndGate.id), "Asked to find time to end gate when another key gate is in between!");
    straightZoneData.Clear();
    if (inEndGate.corner == null && inKeyGate.corner.nextStraight != inEndGate.straight || inKeyGate.corner.nextStraight == null && inEndGate.corner == null)
      Debug.LogError((object) "Cannot estimate travel time as end gate does not lie adjacent to the key gate corner", (UnityEngine.Object) null);
    float distanceBetweenGates = PathUtility.GetDistanceBetweenGates(inPathData, inKeyGate.id, inEndGate.id);
    if (inKeyGate.corner == inEndGate.corner)
    {
      switch (inKeyGate.gateType)
      {
        case PathData.GateType.BrakingZone:
          float clampedCorneringSpeed = SimulationLapTimeModel.GetClampedCorneringSpeed(inCarStats, inKeyGate.corner);
          straightZoneData.SetupStraightZoneData(currentSpeed, distanceBetweenGates, inCarStats, clampedCorneringSpeed);
          break;
        case PathData.GateType.AccelerationZone:
          straightZoneData.SetupStraightZoneData(currentSpeed, distanceBetweenGates, inCarStats);
          break;
      }
      return straightZoneData.GetTotalStraightTime();
    }
    if (inEndGate.corner != null && PathUtility.GetNextKeyGate(inEndGate, inPathData).gateType != PathData.GateType.BrakingZone)
    {
      Debug.LogWarning((object) "Cannot estimate travel time as end gate does not lie adjacent to the key gate corner", (UnityEngine.Object) null);
      return new TimeSpan();
    }
    straightZoneData.SetupStraightZoneData(currentSpeed, SimulationLapTimeModel.GetClampedCorneringSpeed(inCarStats, PathUtility.GetNextKeyGate(inKeyGate, inPathData).corner), PathUtility.GetDistanceBetweenGates(inPathData, inKeyGate.id, PathUtility.GetNextKeyGate(inKeyGate, inPathData).id), inCarStats);
    return straightZoneData.CalculateTimeAtDistance(distanceBetweenGates);
  }

  private static float EstimateSpeedIntoGate(PathData.Gate inStartGate, PathData inPathData, CarStats inStats)
  {
    List<PathData.Corner> corners = inPathData.corners;
    if (corners.Count == 0)
      return inStats.topSpeed;
    float num;
    if (inStartGate.straight != null)
    {
      PathData.Corner inCorner = (PathData.Corner) null;
      for (int index = 0; index < corners.Count; ++index)
      {
        if (corners[index].nextStraight == inStartGate.straight)
        {
          inCorner = corners[index];
          break;
        }
      }
      if (inCorner == null)
      {
        num = inStats.topSpeed;
      }
      else
      {
        PathData.Corner nextCorner = inStartGate.straight.nextCorner;
        SimulationLapTimeModel.StraightZoneData straightZoneData = new SimulationLapTimeModel.StraightZoneData();
        straightZoneData.SetupStraightZoneData(SimulationLapTimeModel.GetClampedCorneringSpeed(inStats, inCorner), SimulationLapTimeModel.GetClampedCorneringSpeed(inStats, nextCorner), inStartGate.straight.length + SimulationLapTimeModel.GetCornerSpeedChangeDistance(inPathData, inCorner, PathUtility.ZoneType.Accelerating, -1) + SimulationLapTimeModel.GetCornerSpeedChangeDistance(inPathData, nextCorner, PathUtility.ZoneType.Braking, -1), inStats);
        num = straightZoneData.CalculateSpeedAtDistance(PathUtility.GetDistanceBetweenGates(inPathData, PathUtility.GetCornerTransitionGateID(inPathData, inCorner, PathUtility.ZoneType.Accelerating), inStartGate.id));
      }
    }
    else
    {
      int transitionGateId1 = PathUtility.GetCornerTransitionGateID(inPathData, inStartGate.corner, PathUtility.ZoneType.Braking);
      int transitionGateId2 = PathUtility.GetCornerTransitionGateID(inPathData, inStartGate.corner, PathUtility.ZoneType.Accelerating);
      num = transitionGateId1 == inStartGate.id || PathUtility.IsGateBeforeInclusive(transitionGateId1, inStartGate.id, inPathData.gates) ? (PathUtility.IsGateBeforeInclusive(inStartGate.id, transitionGateId2, inPathData.gates) || inStartGate.id == transitionGateId2 ? SimulationLapTimeModel.GetClampedCorneringSpeed(inStats, inStartGate.corner) : PhysicsUtility.GetCappedSpeedReachedOverDistance(PathUtility.GetDistanceBetweenGates(inPathData, transitionGateId2, inStartGate.id), inStats.acceleration, SimulationLapTimeModel.GetClampedCorneringSpeed(inStats, inStartGate.corner), inStats.topSpeed, 0.0f)) : PhysicsUtility.GetCappedSpeedReachedOverDistance(PathUtility.GetDistanceBetweenGates(inPathData, inStartGate.id, transitionGateId1), inStats.braking, SimulationLapTimeModel.GetClampedCorneringSpeed(inStats, inStartGate.corner), inStats.topSpeed, 0.0f);
    }
    return num;
  }

  private static float GetCornerSpeedChangeDistance(PathData inPathData, PathData.Corner inCorner, PathUtility.ZoneType inZoneType, int endCapGateID = -1)
  {
    List<PathData.Gate> gates = inPathData.gates;
    int inStartID = 0;
    int num = 0;
    switch (inZoneType)
    {
      case PathUtility.ZoneType.Braking:
        inStartID = inCorner.startGateID;
        num = PathUtility.GetCornerTransitionGateID(inPathData, inCorner, inZoneType);
        if (endCapGateID >= 0 && PathUtility.IsGateBeforeInclusive(endCapGateID, num, inPathData.gates))
        {
          num = endCapGateID;
          break;
        }
        break;
      case PathUtility.ZoneType.Accelerating:
        inStartID = PathUtility.GetCornerTransitionGateID(inPathData, inCorner, inZoneType);
        num = inCorner.endGateID + 1 != gates.Count ? inCorner.endGateID + 1 : 0;
        if (endCapGateID >= 0 && PathUtility.IsGateBeforeInclusive(endCapGateID, num, inPathData.gates))
        {
          num = endCapGateID + 1 != gates.Count ? endCapGateID + 1 : 0;
          break;
        }
        break;
    }
    return PathUtility.GetDistanceBetweenGates(inPathData, inStartID, num);
  }

  private static float GetClampedCorneringSpeed(CarStats inStats, PathData.Corner inCorner)
  {
    float num = Mathf.Clamp(SessionPerformance.GetSpeedForCorner(inStats, inCorner), 0.0f, inStats.topSpeed);
    if ((double) num == 0.0)
    {
      num = 1f;
      Debug.LogWarningFormat("Cannot corner at zero speed!! CornerLength: {0}, CornerType: {1}, LSC {2}, MSC {3}, HSC {4}", (object) inCorner.length, (object) inCorner.type, (object) inStats.lowSpeedCorners, (object) inStats.mediumSpeedCorners, (object) inStats.highSpeedCorners);
    }
    return num;
  }

  public static TimeSpan EstimateDriveThroughPenaltyTimeDeprecated(CircuitScene inCircuit, float inPitLaneSpeed, CarStats inAverageStats)
  {
    List<PathData.Gate> gates = inCircuit.GetTrackPath().data.gates;
    int index = inCircuit.pitlaneEntryTrackPathID;
    int pitlaneExitTrackPathId = inCircuit.pitlaneExitTrackPathID;
    float num1 = 0.0f;
    while (index != pitlaneExitTrackPathId)
    {
      if (gates[index].straight != null)
      {
        num1 += PhysicsUtility.GetTimeToTravelDistance(gates[index].distance, inAverageStats.topSpeed);
      }
      else
      {
        Debug.Assert(gates[index].corner != null, "Gate is not on a corner on a straight");
        num1 += PhysicsUtility.GetTimeToTravelDistance(gates[index].distance, SimulationLapTimeModel.GetClampedCorneringSpeed(inAverageStats, gates[index].corner));
      }
      if (index == gates.Count - 1)
        index = 0;
      else
        ++index;
    }
    float num2 = inCircuit.pitlaneEntryPath.data.length / 2f;
    float num3 = inCircuit.pitlaneExitPath.data.length / 2f;
    SimulationLapTimeModel.StraightZoneData straightZoneData1 = new SimulationLapTimeModel.StraightZoneData();
    straightZoneData1.SetupStraightZoneData(inPitLaneSpeed, PhysicsUtility.GetCappedSpeedReachedOverDistance(num2, inAverageStats.braking, inPitLaneSpeed, inAverageStats.topSpeed, inPitLaneSpeed), num2, inAverageStats);
    SimulationLapTimeModel.StraightZoneData straightZoneData2 = new SimulationLapTimeModel.StraightZoneData();
    straightZoneData2.SetupStraightZoneData(inPitLaneSpeed, PhysicsUtility.GetCappedSpeedReachedOverDistance(num3, inAverageStats.braking, inPitLaneSpeed, inAverageStats.topSpeed, inPitLaneSpeed), num3, inAverageStats);
    SimulationLapTimeModel.StraightZoneData straightZoneData3 = new SimulationLapTimeModel.StraightZoneData();
    straightZoneData3.SetupStraightZoneData(inPitLaneSpeed, num2 + inCircuit.pitlanePath.data.length + num3);
    TimeSpan timeSpan1 = straightZoneData3.GetTotalStraightTime() + straightZoneData2.GetTotalStraightTime() + straightZoneData1.GetTotalStraightTime();
    if ((double) num1 > timeSpan1.TotalSeconds)
      Debug.LogWarningFormat("Calculated the pit lane time as: {0}s, track time as {1}s on track {2}", (object) straightZoneData3.GetTotalStraightTime().TotalSeconds, (object) num1, (object) inCircuit.name);
    TimeSpan timeSpan2 = timeSpan1 - TimeSpan.FromSeconds((double) num1);
    Debug.LogFormat("Calculated the duration of a pit drivethrough penalty as approximately {0} seconds.", (object) timeSpan2.TotalSeconds);
    return timeSpan2;
  }

  public static TimeSpan EstimateLapTimeDeprecated(CarStats inStats, CircuitScene inCircuit)
  {
    TimeSpan timeSpan = new TimeSpan(0, 0, 0);
    PathData data = inCircuit.GetTrackPath().data;
    if (data.gates.Count == 0)
      Debug.LogError((object) "No gates on track!", (UnityEngine.Object) null);
    List<PathData.Straight> straights = data.straights;
    List<PathData.Corner> corners = data.corners;
    bool[] inStraightsUsed = new bool[straights.Count];
    bool[] inCornersUsed = new bool[corners.Count];
    float[] numArray = new float[corners.Count];
    if ((double) inStats.topSpeed <= 0.0 || (double) inStats.braking <= 0.0 || (double) inStats.acceleration <= 0.0)
    {
      Debug.LogWarningFormat("Could not estimate lap time for car with stats: Braking: {0}, Acceleration: {1}, Top Speed: {2}", (object) inStats.braking, (object) inStats.topSpeed, (object) inStats.acceleration);
      return timeSpan;
    }
    for (int index = 0; index < corners.Count; ++index)
    {
      if (index != corners[index].id)
        Debug.LogWarningFormat("Lap time estimate: Corner at index {0} has ID {1}. This algorithm assumes corner Index == corner ID!", new object[2]
        {
          (object) index,
          (object) corners[index].id
        });
      inCornersUsed[index] = false;
      numArray[index] = 0.0f;
    }
    for (int index = 0; index < straights.Count; ++index)
    {
      if (index != straights[index].id)
        Debug.LogWarningFormat("Lap time estimate: Straight at index {0} has ID {1}. This algorithm assumes Straight Index == Straight ID!", new object[2]
        {
          (object) index,
          (object) straights[index].id
        });
      inStraightsUsed[index] = false;
    }
    List<SimulationLapTimeModel.StraightZoneData> straightZoneDataList = new List<SimulationLapTimeModel.StraightZoneData>();
    PathData.Corner inCorner = (PathData.Corner) null;
    for (int index = 0; index < corners.Count; ++index)
    {
      if (corners[index].nextStraight != null && corners[index].nextStraight.id == straights[0].id)
      {
        inCorner = corners[index];
        inCornersUsed[inCorner.id] = true;
        break;
      }
    }
    if (corners.Count == 0)
      return TimeSpan.FromSeconds((double) data.length / (double) inStats.topSpeed);
    if (inCorner == null || inCorner.nextStraight != straights[0])
      Debug.LogError((object) "The straight after the last corner is not the first straight! Something is wrong!", (UnityEngine.Object) null);
    PathData.Straight nextStraight1 = inCorner.nextStraight;
    bool flag = false;
    while (!flag)
    {
      numArray[inCorner.id] += inCorner.length;
      PathData.Straight nextStraight2 = inCorner.nextStraight;
      PathData.Corner nextCorner;
      float num;
      if (nextStraight2 != null)
      {
        nextCorner = nextStraight2.nextCorner;
        num = nextStraight2.length;
        inStraightsUsed[nextStraight2.id] = true;
      }
      else
      {
        int index = inCorner.id < corners.Count - 1 ? inCorner.id + 1 : 0;
        nextCorner = corners[index];
        num = 0.0f;
      }
      float speedChangeDistance1 = SimulationLapTimeModel.GetCornerSpeedChangeDistance(data, inCorner, PathUtility.ZoneType.Accelerating, -1);
      float speedChangeDistance2 = SimulationLapTimeModel.GetCornerSpeedChangeDistance(data, nextCorner, PathUtility.ZoneType.Braking, -1);
      numArray[inCorner.id] -= speedChangeDistance1;
      numArray[nextCorner.id] -= speedChangeDistance2;
      SimulationLapTimeModel.StraightZoneData straightZoneData = new SimulationLapTimeModel.StraightZoneData();
      straightZoneData.SetupStraightZoneData(SimulationLapTimeModel.GetClampedCorneringSpeed(inStats, inCorner), SimulationLapTimeModel.GetClampedCorneringSpeed(inStats, nextCorner), num + speedChangeDistance1 + speedChangeDistance2, inStats);
      straightZoneDataList.Add(straightZoneData);
      inCorner = nextCorner;
      inCornersUsed[inCorner.id] = true;
      if (inCorner.nextStraight == nextStraight1)
        flag = true;
    }
    if (SimulationLapTimeModel.WholeTrackUsedDeprecated(inCornersUsed, inStraightsUsed))
    {
      for (int index = 0; index < straightZoneDataList.Count; ++index)
        timeSpan += straightZoneDataList[index].GetTotalStraightTime();
      for (int index = 0; index < corners.Count; ++index)
      {
        if ((double) numArray[index] < 0.0)
        {
          int transitionGateId1 = PathUtility.GetCornerTransitionGateID(data, corners[index], PathUtility.ZoneType.Braking);
          int transitionGateId2 = PathUtility.GetCornerTransitionGateID(data, corners[index], PathUtility.ZoneType.Accelerating);
          float speedChangeDistance1 = SimulationLapTimeModel.GetCornerSpeedChangeDistance(data, corners[index], PathUtility.ZoneType.Braking, -1);
          float speedChangeDistance2 = SimulationLapTimeModel.GetCornerSpeedChangeDistance(data, corners[index], PathUtility.ZoneType.Accelerating, -1);
          float atCorneringSpeed = PathUtility.GetDistanceAtCorneringSpeed(data, corners[index]);
          float num = speedChangeDistance1 + speedChangeDistance2 + atCorneringSpeed;
          Debug.LogFormat("Corner braking gate {0}, accel gate {1}, braking dist {2}, constant distance {3}, accel distance{4}, total {5}, actual total {6}", (object) transitionGateId1, (object) transitionGateId2, (object) speedChangeDistance1, (object) atCorneringSpeed, (object) speedChangeDistance2, (object) num, (object) corners[index].length);
        }
        Debug.AssertFormat(((double) numArray[index] >= 0.0 ? 1 : 0) != 0, "Negative corner length of {0} recovered in lap time estimation! Something is very wrong!", (object) numArray[index]);
        float clampedCorneringSpeed = SimulationLapTimeModel.GetClampedCorneringSpeed(inStats, corners[index]);
        float toTravelDistance = PhysicsUtility.GetTimeToTravelDistance(numArray[index], clampedCorneringSpeed);
        timeSpan += TimeSpan.FromSeconds((double) toTravelDistance);
      }
    }
    else
      Debug.LogError((object) "Did not use all of the corners and straights to estimate lap time!", (UnityEngine.Object) null);
    return timeSpan;
  }

  private static bool WholeTrackUsedDeprecated(bool[] inCornersUsed, bool[] inStraightsUsed)
  {
    for (int index = 0; index < inCornersUsed.Length; ++index)
    {
      if (!inCornersUsed[index])
        return false;
    }
    for (int index = 0; index < inStraightsUsed.Length; ++index)
    {
      if (!inStraightsUsed[index])
        return false;
    }
    return true;
  }

  public class StraightZoneData
  {
    private float mStartSpeed;
    private float mTopSpeed;
    private float mEndSpeed;
    private float mTotalDistance;
    private float mAcceleration;
    private float mBraking;
    private SimulationLapTimeModel.ZoneData mAccelerationZone;
    private SimulationLapTimeModel.ZoneData mConstSpeedZone;
    private SimulationLapTimeModel.ZoneData mBrakingZone;

    public float distance
    {
      get
      {
        return this.mAccelerationZone.distance + this.mConstSpeedZone.distance + this.mBrakingZone.distance;
      }
    }

    public float cornerEntrySpeed
    {
      get
      {
        return this.mEndSpeed;
      }
    }

    public StraightZoneData()
    {
      this.mAccelerationZone = new SimulationLapTimeModel.ZoneData();
      this.mConstSpeedZone = new SimulationLapTimeModel.ZoneData();
      this.mBrakingZone = new SimulationLapTimeModel.ZoneData();
    }

    public void SetupStraightZoneData(float inStartingSpeed, float inEndSpeed, float inTotalDistance, CarStats inCarStats)
    {
      this.mStartSpeed = inStartingSpeed;
      this.mTotalDistance = inTotalDistance;
      this.mEndSpeed = inEndSpeed;
      this.mTopSpeed = inCarStats.topSpeed;
      this.mAcceleration = inCarStats.acceleration;
      this.mBraking = -inCarStats.braking;
      this.AllocateZones();
    }

    public void SetupStraightZoneData(float inStartingSpeed, float inTotalDistance, CarStats inCarStats)
    {
      this.mStartSpeed = inStartingSpeed;
      this.mTotalDistance = inTotalDistance;
      this.mEndSpeed = Mathf.Min(inCarStats.topSpeed, PhysicsUtility.GetCappedSpeedReachedOverDistance(inTotalDistance, inCarStats.acceleration, inStartingSpeed, inCarStats.topSpeed, 0.0f));
      this.mTopSpeed = inCarStats.topSpeed;
      this.mAcceleration = inCarStats.acceleration;
      this.mBraking = -inCarStats.braking;
      this.AllocateZones();
    }

    public void SetupStraightZoneData(float inStartingSpeed, float inTotalDistance, CarStats inCarStats, float limitedSpeed)
    {
      this.mStartSpeed = inStartingSpeed;
      this.mTotalDistance = inTotalDistance;
      if ((double) inStartingSpeed < (double) limitedSpeed)
      {
        this.mTopSpeed = Mathf.Min(limitedSpeed, inCarStats.topSpeed);
        this.mEndSpeed = PhysicsUtility.GetCappedSpeedReachedOverDistance(inTotalDistance, inCarStats.acceleration, inStartingSpeed, this.mTopSpeed, 0.0f);
      }
      else
      {
        this.mEndSpeed = PhysicsUtility.GetCappedSpeedReachedOverDistance(inTotalDistance, inCarStats.braking, inStartingSpeed, inCarStats.topSpeed, limitedSpeed);
        this.mTopSpeed = inCarStats.topSpeed;
      }
      this.mAcceleration = inCarStats.acceleration;
      this.mBraking = -inCarStats.braking;
      this.AllocateZones();
    }

    public void SetupStraightZoneData(float inConstantSpeed, float inTotalDistance)
    {
      this.mStartSpeed = inConstantSpeed;
      this.mTotalDistance = inTotalDistance;
      this.mEndSpeed = inConstantSpeed;
      this.mTopSpeed = inConstantSpeed;
      this.mAcceleration = 0.0f;
      this.mBraking = 0.0f;
      this.mAccelerationZone.SetZoneData(0.0f, this.mStartSpeed, this.mStartSpeed);
      this.mConstSpeedZone.SetZoneData(this.mTotalDistance, this.mStartSpeed, this.mStartSpeed);
      this.mBrakingZone.SetZoneData(0.0f, this.mStartSpeed, this.mStartSpeed);
    }

    public void Clear()
    {
      this.mStartSpeed = 0.0f;
      this.mTotalDistance = 0.0f;
      this.mEndSpeed = 0.0f;
      this.mTopSpeed = 0.0f;
      this.mAcceleration = 0.0f;
      this.mBraking = 0.0f;
      this.mAccelerationZone.SetZoneData(0.0f, 0.0f, 0.0f);
      this.mConstSpeedZone.SetZoneData(0.0f, 0.0f, 0.0f);
      this.mBrakingZone.SetZoneData(0.0f, 0.0f, 0.0f);
    }

    private void AllocateZones()
    {
      this.mAccelerationZone.SetZoneData(0.0f, 0.0f, 0.0f);
      this.mConstSpeedZone.SetZoneData(0.0f, 0.0f, 0.0f);
      this.mBrakingZone.SetZoneData(0.0f, 0.0f, 0.0f);
      if ((double) this.mEndSpeed > (double) this.mTopSpeed)
      {
        if (MathsUtility.Approximately(this.mEndSpeed, this.mTopSpeed, 0.01f))
          this.mEndSpeed = this.mTopSpeed;
        else
          Debug.LogWarningFormat("Cannot allocate a zone with start speed={0}, end speed ={1} and a top speed of {2}", (object) this.mStartSpeed, (object) this.mEndSpeed, (object) this.mTopSpeed);
      }
      if ((double) this.mStartSpeed > (double) this.mTopSpeed)
      {
        if (MathsUtility.Approximately(this.mStartSpeed, this.mTopSpeed, 0.01f))
          this.mStartSpeed = this.mTopSpeed;
        else if (!MathsUtility.Approximately(this.mEndSpeed, this.mTopSpeed, 0.01f))
          Debug.LogWarningFormat("Cannot allocate a zone with start speed={0}, end speed ={1} and a top speed of {2}", (object) this.mStartSpeed, (object) this.mEndSpeed, (object) this.mTopSpeed);
      }
      if ((double) this.mEndSpeed > (double) this.mStartSpeed)
        this.mEndSpeed = Mathf.Min(this.mEndSpeed, PhysicsUtility.GetCappedSpeedReachedOverDistance(this.mTotalDistance, this.mAcceleration, this.mStartSpeed, 1000000f, 0.0f));
      this.mEndSpeed = Mathf.Max(this.mEndSpeed, PhysicsUtility.GetCappedSpeedReachedOverDistance(this.mTotalDistance, this.mBraking, this.mStartSpeed, 1000000f, 0.0f));
      if ((double) this.mStartSpeed > (double) this.mTopSpeed)
      {
        float whilstChangingSpeed1 = PhysicsUtility.GetDistanceWhilstChangingSpeed(this.mStartSpeed, this.mTopSpeed, this.mBraking);
        float whilstChangingSpeed2 = PhysicsUtility.GetDistanceWhilstChangingSpeed(this.mTopSpeed, this.mEndSpeed, this.mBraking);
        float inZoneDistance = Mathf.Max(this.mTotalDistance - (whilstChangingSpeed1 + whilstChangingSpeed2), 0.0f);
        this.mAccelerationZone.SetZoneData(whilstChangingSpeed1, this.mStartSpeed, this.mTopSpeed);
        this.mConstSpeedZone.SetZoneData(inZoneDistance, this.mTopSpeed, this.mTopSpeed);
        this.mBrakingZone.SetZoneData(whilstChangingSpeed2, this.mTopSpeed, this.mEndSpeed);
      }
      else
      {
        float num = Mathf.Min(this.mTopSpeed, PhysicsUtility.GetPeakSpeedOverDistanceGivenBoundryConditions(this.mTotalDistance, this.mAcceleration, this.mBraking, this.mStartSpeed, this.mEndSpeed));
        this.mEndSpeed = Mathf.Min(this.mTopSpeed, this.mEndSpeed);
        float whilstChangingSpeed1 = PhysicsUtility.GetDistanceWhilstChangingSpeed(this.mStartSpeed, num, this.mAcceleration);
        float whilstChangingSpeed2 = PhysicsUtility.GetDistanceWhilstChangingSpeed(num, this.mEndSpeed, this.mBraking);
        float inZoneDistance = this.mTotalDistance - (whilstChangingSpeed1 + whilstChangingSpeed2);
        if ((double) whilstChangingSpeed2 < -1.0 || (double) whilstChangingSpeed1 < -1.0 || (double) inZoneDistance < -1.0)
          Debug.LogErrorFormat("Managed to generate a negative distance. Braking distance: {0}, top speed distance {1}, accel distance {2}", (object) whilstChangingSpeed2, (object) inZoneDistance, (object) whilstChangingSpeed1);
        if ((double) whilstChangingSpeed1 > 0.0)
          this.mAccelerationZone.SetZoneData(whilstChangingSpeed1, this.mStartSpeed, num);
        if ((double) inZoneDistance > 0.0)
          this.mConstSpeedZone.SetZoneData(inZoneDistance, num, num);
        if ((double) whilstChangingSpeed2 <= 0.0)
          return;
        this.mBrakingZone.SetZoneData(whilstChangingSpeed2, num, this.mEndSpeed);
      }
    }

    public TimeSpan GetTotalStraightTime()
    {
      TimeSpan timeSpan = new TimeSpan();
      if (this.mAccelerationZone != null)
        timeSpan += this.mAccelerationZone.GetZoneTime();
      if (this.mConstSpeedZone != null)
        timeSpan += this.mConstSpeedZone.GetZoneTime();
      if (this.mBrakingZone != null)
        timeSpan += this.mBrakingZone.GetZoneTime();
      return timeSpan;
    }

    public float CalculateSpeedAtDistance(float inDistance)
    {
      if ((double) inDistance > (double) this.distance || (double) inDistance < 0.0)
        Debug.LogWarningFormat("Cannot find speed at a distance outsize the StraightZoneData. Distance into Zone requested={0}. Overall length={1}", new object[2]
        {
          (object) inDistance,
          (object) this.distance
        });
      if ((double) this.mAccelerationZone.distance > (double) inDistance)
        return this.mAccelerationZone.CalculateSpeedAtDistance(inDistance);
      if ((double) this.mAccelerationZone.distance + (double) this.mConstSpeedZone.distance > (double) inDistance)
        return this.mConstSpeedZone.CalculateSpeedAtDistance(inDistance - this.mAccelerationZone.distance);
      return this.mBrakingZone.CalculateSpeedAtDistance(inDistance - (this.mAccelerationZone.distance + this.mConstSpeedZone.distance));
    }

    public TimeSpan CalculateTimeAtDistance(float inDistance)
    {
      if ((double) inDistance > (double) this.distance || (double) inDistance < 0.0)
        Debug.LogWarningFormat("Cannot find speed at a distance outsize the StraightZoneData. Distance into Zone requested={0}. Overall length={1}", new object[2]
        {
          (object) inDistance,
          (object) this.distance
        });
      if ((double) this.mAccelerationZone.distance > (double) inDistance)
        return this.mAccelerationZone.CalculateTimeAtDistance(inDistance);
      if ((double) this.mAccelerationZone.distance + (double) this.mConstSpeedZone.distance > (double) inDistance)
        return this.mAccelerationZone.GetZoneTime() + this.mConstSpeedZone.CalculateTimeAtDistance(inDistance - this.mAccelerationZone.distance);
      return this.mAccelerationZone.GetZoneTime() + this.mConstSpeedZone.GetZoneTime() + this.mBrakingZone.CalculateTimeAtDistance(inDistance - (this.mAccelerationZone.distance + this.mConstSpeedZone.distance));
    }

    public enum ChangeSpeedType
    {
      accelerating,
      decelerating,
    }
  }

  public class ZoneData
  {
    private float mZoneDistance;
    private float mZoneInSpeed;
    private float mZoneOutSpeed;

    public float distance
    {
      get
      {
        return this.mZoneDistance;
      }
    }

    public void SetZoneData(float inZoneDistance, float inZoneInSpeed, float inZoneOutSpeed)
    {
      if ((double) inZoneDistance < 0.0)
        Debug.LogWarning((object) "Tried to create a zone with negative distance! Something is very wrong!", (UnityEngine.Object) null);
      this.mZoneDistance = inZoneDistance;
      this.mZoneInSpeed = inZoneInSpeed;
      this.mZoneOutSpeed = inZoneOutSpeed;
    }

    public TimeSpan GetZoneTime()
    {
      return TimeSpan.FromSeconds((double) PhysicsUtility.GetTimeToTravelDistance(this.mZoneDistance, (float) (((double) this.mZoneInSpeed + (double) this.mZoneOutSpeed) / 2.0)));
    }

    public float CalculateSpeedAtDistance(float inDistance)
    {
      if ((double) inDistance > (double) this.mZoneDistance || (double) inDistance < 0.0)
        Debug.LogWarningFormat("Cannot find speed at a distance outsize the ZoneData. Distance into Zone requested={0}. Zone length={1}", new object[2]
        {
          (object) inDistance,
          (object) this.mZoneDistance
        });
      float reachTargetSpeed = PhysicsUtility.GetAccelerationNeededToReachTargetSpeed(this.mZoneInSpeed, this.mZoneOutSpeed, this.mZoneDistance);
      return PhysicsUtility.GetCappedSpeedReachedOverDistance(inDistance, reachTargetSpeed, this.mZoneInSpeed, 1000000f, 0.0f);
    }

    public TimeSpan CalculateTimeAtDistance(float inDistance)
    {
      Debug.Assert((double) inDistance < (double) this.mZoneDistance && (double) inDistance > 0.0, "Cannot find speed at a distance outsize the ZoneData");
      float reachTargetSpeed = PhysicsUtility.GetAccelerationNeededToReachTargetSpeed(this.mZoneInSpeed, this.mZoneOutSpeed, this.mZoneDistance);
      return PhysicsUtility.GetTimeToTravelDistance(inDistance, this.mZoneInSpeed, reachTargetSpeed);
    }
  }
}
