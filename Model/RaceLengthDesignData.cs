// Decompiled with JetBrains decompiler
// Type: RaceLengthDesignData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Xml.Serialization;

public class RaceLengthDesignData
{
  [XmlElement("ShortRaceLengthInMiles")]
  public float shortRaceLength = 50f;
  [XmlElement("MediumRaceLengthInMiles")]
  public float mediumRaceLength = 80f;
  [XmlElement("LongRaceLengthInMiles")]
  public float longRaceLength = 140f;
  [XmlElement("ShortRaceLengthShortSessionRulesLapDelta")]
  public int shortRaceLengthShortSessionRulesLapDelta = -2;
  [XmlElement("ShortRaceLengthLongSessionRulesLapDelta")]
  public int shortRaceLengthLongSessionRulesLapDelta = 2;
  [XmlElement("MediumRaceLengthShortSessionRulesLapDelta")]
  public int mediumRaceLengthShortSessionRulesLapDelta = -5;
  [XmlElement("MediumRaceLengthLongSessionRulesLapDelta")]
  public int mediumRaceLengthLongSessionRulesLapDelta = 5;
  [XmlElement("LongRaceLengthShortSessionRulesLapDelta")]
  public int longRaceLengthShortSessionRulesLapDelta = -5;
  [XmlElement("LongRaceLengthLongSessionRulesLapDelta")]
  public int longRaceLengthLongSessionRulesLapDelta = 5;
}
