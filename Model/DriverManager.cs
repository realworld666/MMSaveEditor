using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class DriverManager : PersonManager<Driver>
{
  public NullDriver nullDriver = new NullDriver();
  public DriverStatsProgression ageDriverStatProgression;
  public DriverStatsProgression maxDriverStatProgressionPerDay;
  public DriverStatsProgression raceDriverStatProgression;
  public DriverStatsProgression qualifyingDriverStatProgression;
  public DriverStatsProgression practiceDriverStatProgression;
  private static IEnumerable<Driver> mDriversCache;
    
}
