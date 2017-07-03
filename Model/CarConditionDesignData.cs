// Decompiled with JetBrains decompiler
// Type: CarConditionDesignData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using System.Xml.Serialization;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarConditionDesignData : PerformanceDesignData
{
  [XmlElement("BrakeFailureTimeCost")]
  public float brakeFailureTimeCost;
  [XmlElement("EngineFailureTimeCost")]
  public float engineFailureTimeCost;
  [XmlElement("FrontWingFailureTimeCost")]
  public float frontWingFailureTimeCost;
  [XmlElement("GearboxFailureTimeCost")]
  public float gearboxFailureTimeCost;
  [XmlElement("RearWingFailureTimeCost")]
  public float rearWingFailureTimeCost;
  [XmlElement("SuspensionFailureTimeCost")]
  public float suspensionFailureTimeCost;
}
