using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class MechanicManager : PersonManager<Mechanic>
{
  public MechanicStatsProgression ageMechanicStatProgression;
  public MechanicStatsProgression maxMechanicStatProgressionPerDay;

}
