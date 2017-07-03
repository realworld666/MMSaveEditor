// Decompiled with JetBrains decompiler
// Type: CarChassisDesignData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using System.Xml.Serialization;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarChassisDesignData
{
  [XmlElement("TyreWearLapCountIncrease")]
  public int tyreWearLapCountIncrease = 1;
  [XmlElement("TyreWearLapCountDecrease")]
  public int tyreWearLapCountDecrease = 1;
  [XmlElement("TyreHeatingTimeBonusInMinutes")]
  public int tyreHeatingTimeBonusInMinutes = 2;
  [XmlElement("FuelEfficiencyChassisStatNegativeImpact")]
  public float fuelEfficiencyChassisStatNegativeImpact = 1.05f;
  [XmlElement("FuelEfficiencyChassisStatPositiveImpact")]
  public float fuelEfficiencyChassisStatPositiveImpact = 0.95f;
}
