// Decompiled with JetBrains decompiler
// Type: ERSDesignData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using System.Xml.Serialization;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ERSDesignData
{
  [XmlElement("SmallBatterySize")]
  public float smallBatterySize;
  [XmlElement("LargeBatterySize")]
  public float largeBatterySize;
  [XmlElement("HarvestModeRate")]
  public float harvestModeRate;
  [XmlElement("PowerModeRate")]
  public float powerModeRate;
  [XmlElement("PowerModeTimeCostGain")]
  public float powerModeTimeCostGain;
  [XmlElement("HybridModeFuelSave")]
  public float hybridModeFuelSave;
  [XmlElement("HybridModeRate")]
  public float hybridModeRate;
  [XmlElement("PowerModeChangeCooldown")]
  public float powerModeChangeCooldown;
  [XmlElement("HybridModeChangeCooldown")]
  public float hybridModeChangeCooldown;
  [XmlElement("HarvestModeChangeCooldown")]
  public float harvestModeChangeCooldown;
}
