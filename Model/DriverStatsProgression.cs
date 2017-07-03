using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class DriverStatsProgression
{
  public string progressionType;
  public float braking;
  public float cornering;
  public float smoothness;
  public float overtaking;
  public float consistency;
  public float adaptability;
  public float fitness;
  public float feedback;
  public float focus;
    
}
