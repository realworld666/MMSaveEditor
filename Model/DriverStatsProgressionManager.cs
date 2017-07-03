using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class DriverStatsProgressionManager
{
  private Dictionary<string, DriverStatsProgression> statsProgressionDictionary;

}
