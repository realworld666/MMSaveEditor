
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SimulationSettingsManager
{
  public Dictionary<ChampionshipRules.SessionLength, SimulationSettings> practiceSettings = new Dictionary<ChampionshipRules.SessionLength, SimulationSettings>();
  public Dictionary<ChampionshipRules.SessionLength, SimulationSettings> qualifyingSettings = new Dictionary<ChampionshipRules.SessionLength, SimulationSettings>();
  public Dictionary<ChampionshipRules.SessionLength, SimulationSettings> raceSettings = new Dictionary<ChampionshipRules.SessionLength, SimulationSettings>();
    
}
