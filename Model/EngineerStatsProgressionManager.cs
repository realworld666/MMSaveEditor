using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class EngineerStatsProgressionManager
{
  private Dictionary<string, EngineerStatsProgression> statsProgressionDictionary;
}
