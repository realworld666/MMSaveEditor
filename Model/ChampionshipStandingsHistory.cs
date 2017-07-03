using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChampionshipStandingsHistory
{
  public List<ChampionshipStandingsHistoryEntry> history = new List<ChampionshipStandingsHistoryEntry>();
  private Championship mChampionship;
}
