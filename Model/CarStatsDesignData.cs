// Decompiled with JetBrains decompiler
// Type: CarStatsDesignData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Xml.Serialization;

public class CarStatsDesignData
{
  [XmlAttribute("isActive")]
  public bool isActive = true;
  [XmlElement("TopSpeedMph")]
  public float topSpeedMph = 250f;
  [XmlElement("Acceleration")]
  public float acceleration = 15f;
  [XmlElement("Braking")]
  public float braking = 40f;
  [XmlElement("LowSpeedCorners")]
  public float lowSpeedCorners = 250f;
  [XmlElement("MediumSpeedCorners")]
  public float mediumSpeedCorners = 15f;
  [XmlElement("HighSpeedCorners")]
  public float highSpeedCorners = 40f;
  [XmlElement("TopSpeedPerformanceScalar")]
  public float topSpeedPerformanceScalar = 1f;
  [XmlElement("AccelerationPerformanceScalar")]
  public float accelerationPerformanceScalar = 1f;
  [XmlElement("BrakingPerformanceScalar")]
  public float brakingPerformanceScalar = 1f;
  [XmlElement("CorneringPerformanceScalar")]
  public float corneringPerformanceScalar = 1f;
  [XmlElement("MinPerformanceTimeCost")]
  public float minPerformanceTimeCost;
  [XmlElement("Tier2TimeCost")]
  public float tier2TimeCost;
  [XmlElement("Tier3TimeCost")]
  public float tier3TimeCost;
}
