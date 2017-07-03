
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class NextYearCarDesign
{
  public NextYearCarDesign.State state = NextYearCarDesign.State.WaitingForDesign;
  public DateTime designStartDate = new DateTime();
  public DateTime designEndDate = new DateTime();
  private Team mTeam;
  private CarChassisStats mChassisStats;
  private Notification mNotification;
  private int mEngineModifier;
    

  public enum State
  {
    Designing,
    WaitingForDesign,
    Complete,
  }
}
