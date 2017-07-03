// Decompiled with JetBrains decompiler
// Type: FuelDesignData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using System.Xml.Serialization;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class FuelDesignData : PerformanceDesignData
{
  [XmlElement("MaxEngineModeTimeCost")]
  public float maxEngineModeTimeCost = 1f;
  [XmlElement("OutOfFuelTimeCost")]
  public float outOfFuelTimeCost = 30f;
  [XmlElement("TimeCostPerLapOfFuel")]
  public float timeCostPerLapOfFuel = 0.3f;
  [XmlElement("SuperOvertakePerformance")]
  public float superOvertakePerformance = 1f;
  [XmlElement("SuperOvertakeBurnRate")]
  public float superOvertakeBurnRate = 1.2f;
  [XmlElement("OvertakeBurnRate")]
  public float overtakeBurnRate = 1.5f;
  [XmlElement("OvertakePerformance")]
  public float overtakePerformance = 1f;
  [XmlElement("HighBurnRate")]
  public float highBurnRate = 1.25f;
  [XmlElement("HighPerformance")]
  public float highPerformance = 0.75f;
  [XmlElement("MediumBurnRate")]
  public float mediumBurnRate = 0.85f;
  [XmlElement("MediumPerformance")]
  public float mediumPerformance = 0.5f;
  [XmlElement("LowBurnRate")]
  public float lowBurnRate = 0.5f;
  [XmlElement("LowPerformance")]
  public float lowPerformance;
}
