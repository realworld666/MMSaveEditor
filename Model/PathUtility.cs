// Decompiled with JetBrains decompiler
// Type: PathUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class PathUtility
{
  public static int ClampIndex(int idx, int len)
  {
    if (idx < 0)
      idx = 0;
    else if (idx > len - 1)
      idx = len - 1;
    return idx;
  }

  public static int WrapIndex(int idx, int len)
  {
    if (idx < 0)
      idx = len + idx % len;
    else if (idx >= len - 1)
      idx %= len;
    return idx;
  }

  public static int CalculatePathIndex(int idx, int len, PathData.Type pathtype)
  {
    if (pathtype == PathData.Type.Loop)
      return PathUtility.WrapIndex(idx, len);
    return PathUtility.ClampIndex(idx, len);
  }

  public static float WrapPosition(PathData.Type wrapmode, float pos, float len)
  {
    switch (wrapmode)
    {
      case PathData.Type.Loop:
        if ((double) pos < 0.0)
        {
          int num = (int) (-(double) pos / (double) len) + 1;
          pos += (float) num * len;
          break;
        }
        if ((double) pos >= (double) len)
        {
          int num = (int) ((double) pos / (double) len);
          pos -= (float) num * len;
          break;
        }
        break;
      case PathData.Type.Line:
        if ((double) pos < 0.0)
        {
          pos = 0.0f;
          break;
        }
        if ((double) pos > (double) len)
        {
          pos = len;
          break;
        }
        break;
    }
    return pos;
  }

  public static int GetFirstGateIDFurtherAwayThanWidth(PathData inPath, PathController.Path inTrackPath)
  {
    float num = inTrackPath.data.width * 1.5f;
    for (int index = 0; index < inPath.gates.Count; ++index)
    {
      PathData.Gate gate1 = inPath.gates[index];
      int closestTrackGateId = PathUtility.GetClosestTrackGateID(gate1.position, inTrackPath, 0);
      PathData.Gate gate2 = inTrackPath.data.gates[closestTrackGateId];
      if ((double) Vector3.Distance(gate1.position, gate2.position) >= (double) num)
        return index;
    }
    return 1;
  }

  public static int GetClosestTrackGateID(PathController.Path inPath, PathController.Path inTrackPath, bool inGetClosestToExit = false)
  {
    int index = 0;
    if (inGetClosestToExit)
      index = inPath.data.gates.Count - 1;
    return PathUtility.GetClosestTrackGateID(inPath.data.gates[index], inTrackPath);
  }

  public static int GetClosestTrackGateID(PathData.Gate inGate, PathController.Path inTrackPath)
  {
    return PathUtility.GetClosestTrackGateID(inGate.position, inTrackPath, 1);
  }

  public static int GetClosestTrackGateID(Vector3 inPosition, PathController.Path inTrackPath, int offset = 1)
  {
    int num1 = 0;
    float num2 = float.MaxValue;
    for (int index = 0; index < inTrackPath.data.gates.Count; ++index)
    {
      PathData.Gate gate = inTrackPath.data.gates[index];
      float num3 = Vector3.Distance(gate.position, inPosition);
      if ((double) num3 < (double) num2)
      {
        num2 = num3;
        num1 = gate.id;
      }
    }
    return PathUtility.WrapIndex(num1 - offset, inTrackPath.data.gates.Count);
  }

  public static float GetDistanceAtCorneringSpeed(PathData inPathData, PathData.Corner inCorner)
  {
    int transitionGateId1 = PathUtility.GetCornerTransitionGateID(inPathData, inCorner, PathUtility.ZoneType.Braking);
    int transitionGateId2 = PathUtility.GetCornerTransitionGateID(inPathData, inCorner, PathUtility.ZoneType.Accelerating);
    return PathUtility.GetDistanceBetweenGates(inPathData, transitionGateId1, transitionGateId2);
  }

  public static int GetCornerTransitionGateID(PathData inPathData, PathData.Corner inCorner, PathUtility.ZoneType inZoneType)
  {
    List<PathData.Gate> gates = inPathData.gates;
    int index = inCorner.startGateID != 0 ? inCorner.startGateID - 1 : inPathData.gates.Count - 1;
    switch (inZoneType)
    {
      case PathUtility.ZoneType.Braking:
        return inPathData.FindNextGateOfType(gates[index], PathData.GateType.BrakingZone).id;
      case PathUtility.ZoneType.Accelerating:
        return inPathData.FindNextGateOfType(gates[index], PathData.GateType.AccelerationZone).id;
      default:
        return -1;
    }
  }

  public static float GetDistanceBetweenGates(PathData inPathData, int inStartID, int inEndID)
  {
    float num = 0.0f;
    for (int index = inStartID; index != inEndID; index = PathUtility.CalculatePathIndex(index + 1, inPathData.gates.Count, PathData.Type.Loop))
      num += inPathData.gates[index].distance;
    return num;
  }

  public static bool IsGateBeforeInclusive(int inEarlyGateID, int inLaterGateID, List<PathData.Gate> inGates)
  {
    int num1 = inEarlyGateID;
    int num2 = inLaterGateID;
    if ((double) Mathf.Abs(num1 - num2) >= (double) inGates.Count / 2.0)
    {
      num1 = (double) num1 >= (double) inGates.Count / 2.0 ? num1 : num1 + inGates.Count;
      num2 = (double) num2 >= (double) inGates.Count / 2.0 ? num2 : num2 + inGates.Count;
    }
    Debug.Assert((double) Mathf.Abs(num1 - num2) < (double) inGates.Count / 2.0, "Jason - Code to account for wrap around in gate numbers is not doing its job!");
    return num2 - num1 >= 0;
  }

  public static PathData.Gate GetGateWithID(int inGateID, PathData inPathData)
  {
    for (int index = 0; index < inPathData.gates.Count; ++index)
    {
      if (inPathData.gates[index].id == inGateID)
        return inPathData.gates[index];
    }
    return (PathData.Gate) null;
  }

  public static int GetIndexOfGate(int inGateID, PathData inPathData)
  {
    for (int index = 0; index < inPathData.gates.Count; ++index)
    {
      if (inPathData.gates[index].id == inGateID)
        return index;
    }
    return -1;
  }

  public static bool IsGateInBetweenOthersInclusive(int inStartGateID, int inEndGateID, int inQuestionGateID)
  {
    if (inStartGateID <= inEndGateID)
    {
      if (inStartGateID <= inQuestionGateID)
        return inQuestionGateID <= inEndGateID;
      return false;
    }
    if (inQuestionGateID >= inStartGateID)
      return true;
    return inQuestionGateID <= inEndGateID;
  }

  public static PathUtility.ZoneType GetCornerGateZoneRegion(PathData.Gate inGate, PathData inPathData)
  {
    if (inGate.corner == null)
      Debug.LogError((object) "Tried to classify a corner gate with no corner!!", (Object) null);
    PathData.Gate nextKeyGate = PathUtility.GetNextKeyGate(inGate, inPathData);
    if (nextKeyGate.corner != inGate.corner)
      return PathUtility.ZoneType.Accelerating;
    if (nextKeyGate.gateType == PathData.GateType.AccelerationZone)
      return PathUtility.ZoneType.Constant;
    if (nextKeyGate.gateType == PathData.GateType.BrakingZone)
      return PathUtility.ZoneType.Braking;
    Debug.LogError((object) "Could not get corner zone region!", (Object) null);
    return PathUtility.ZoneType.Constant;
  }

  public static PathData.Gate GetNextKeyGate(PathData.Gate inGate, PathData inPathData)
  {
    int indexOfGate = PathUtility.GetIndexOfGate(inGate.id, inPathData);
    List<PathData.Gate> gates = inPathData.gates;
    int pathIndex;
    for (int idx = indexOfGate + 1; idx != indexOfGate; idx = pathIndex + 1)
    {
      pathIndex = PathUtility.CalculatePathIndex(idx, inPathData.gates.Count, PathData.Type.Loop);
      if (gates[pathIndex].gateType == PathData.GateType.AccelerationZone || gates[pathIndex].gateType == PathData.GateType.BrakingZone)
        return gates[pathIndex];
    }
    return (PathData.Gate) null;
  }

  public enum ZoneType
  {
    Braking,
    Accelerating,
    Constant,
  }
}
