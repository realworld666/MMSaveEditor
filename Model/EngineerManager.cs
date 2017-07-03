
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class EngineerManager : PersonManager<Engineer>
{
  public EngineerStatsProgression ageEngineerStatProgression;
  public EngineerStatsProgression maxEngineerStatProgressionPerDay;
    
}
