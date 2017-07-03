
using FullSerializer;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarPartStats
{
  public CarStats.StatType statType = CarStats.StatType.Acceleration;
  public float maxReliability = 0.8f;
  public float maxPerformance = 20f;
  public CarPartCondition partCondition = new CarPartCondition();
  public const int maxLevelMultiplier = 3;
  public const int maxPerformanceConstant = 21;
  public int level;
  public float rulesRisk;
  private float mPerformance;
  private float mReliability;
  private float mStat;

  public enum CarPartStat
  {
    MainStat,
    [LocalisationID("PSG_10002078")] Reliability,
    [LocalisationID("PSG_10004387")] Condition,
    [LocalisationID("PSG_10004388")] Performance,
    Count,
  }

  public enum RulesRisk
  {
    [LocalisationID("PSG_10005815")] None,
    [LocalisationID("PSG_10001437")] Low,
    [LocalisationID("PSG_10001438")] Medium,
    [LocalisationID("PSG_10001439")] High,
  }
}
