// Decompiled with JetBrains decompiler
// Type: TyrePerformanceDesignData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using System.Xml.Serialization;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TyrePerformanceDesignData
{
  [XmlElement("DriverSmoothnessLapCountIncrease")]
  public int driverSmoothnessLapCountIncrease = 1;
  [XmlElement("DriverSmoothnessLapCountDecrease")]
  public int driverSmoothnessLapCountDecrease = 1;
  [XmlElement("CircuitHighTyreWearLapCountDecrease")]
  public int circuitHighTyreWearLapCountDecrease = 1;
  [XmlElement("CircuitLowTyreWearLapCountIncrease")]
  public int circuitLowTyreWearLapCountIncrease = 1;
  [XmlElement("HighPerformanceLapCount")]
  public int highPerformanceLapCount;
  [XmlElement("HighPerformanceTimeCost")]
  public float highPerformanceTimeCost;
  [XmlElement("MediumPerformanceLapCount")]
  public int mediumPerformanceLapCount;
  [XmlElement("MediumPerformanceTimeCost")]
  public float mediumPerformanceTimeCost;
  [XmlElement("LowPerformanceLapCount")]
  public int lowPerformanceLapCount;
  [XmlElement("LowPerformanceTimeCost")]
  public float lowPerformanceTimeCost;
}
