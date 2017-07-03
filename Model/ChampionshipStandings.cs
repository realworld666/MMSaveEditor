using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChampionshipStandings : Entity
{
  private List<ChampionshipEntry_v1> mDrivers = new List<ChampionshipEntry_v1>();
  private List<ChampionshipEntry_v1> mTeams = new List<ChampionshipEntry_v1>();
  private List<ChampionshipEntry_v1> mInactiveDrivers = new List<ChampionshipEntry_v1>();
  private Championship mChampionship;
}
