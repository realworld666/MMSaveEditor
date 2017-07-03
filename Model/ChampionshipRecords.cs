using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChampionshipRecords
{
  private static int mMaxRecordCount = 25;
  private ChampionshipRecords.RecordData[] mData = new ChampionshipRecords.RecordData[12];
  private Dictionary<int, ChampionshipWinnersEntry> mWinnersData = new Dictionary<int, ChampionshipWinnersEntry>();
  
  public enum RecordType
  {
    Championships,
    Races,
    Podiums,
    Poles,
    DNFs,
    DNFsViaError,
    Points,
    FastestLaps,
    LapsLed,
    Overtakes,
    LapsCompleted,
    Penalties,
    Count,
  }

  [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
  public class RecordEntry
  {
    public Driver driver;
    public int value;
  }

  [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
  public class RecordData
  {
    public List<ChampionshipRecords.RecordEntry> entries = new List<ChampionshipRecords.RecordEntry>();
    [NonSerialized]
    public Dictionary<Driver, ChampionshipRecords.RecordEntry> dictionary = new Dictionary<Driver, ChampionshipRecords.RecordEntry>();
  }
}
