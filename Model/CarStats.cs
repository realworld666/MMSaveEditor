using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarStats
{
  public float topSpeed;
  public float acceleration;
  public float braking;
  public float lowSpeedCorners;
  public float mediumSpeedCorners;
  public float highSpeedCorners;

  public enum RelevantToCircuit
  {
    [LocalisationID("PSG_10003785")] No,
    [LocalisationID("PSG_10003786")] Useful,
    [LocalisationID("PSG_10003787")] VeryUseful,
    [LocalisationID("PSG_10003788")] VeryImportant,
  }

  public enum StatType
  {
    [LocalisationID("PSG_10004158")] TopSpeed,
    [LocalisationID("PSG_10004159")] Acceleration,
    [LocalisationID("PSG_10004160")] Braking,
    [LocalisationID("PSG_10004161")] LowSpeedCorners,
    [LocalisationID("PSG_10004162")] MediumSpeedCorners,
    [LocalisationID("PSG_10004163")] HighSpeedCorners,
    Count,
  }

  public enum CarStatShortName
  {
    [LocalisationID("PSG_10004164")] TS,
    [LocalisationID("PSG_10004165")] ACC,
    [LocalisationID("PSG_10004166")] DEC,
    [LocalisationID("PSG_10004167")] LSC,
    [LocalisationID("PSG_10004168")] MSC,
    [LocalisationID("PSG_10004169")] HSC,
  }
}
