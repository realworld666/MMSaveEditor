// Decompiled with JetBrains decompiler
// Type: PrefVideoUnityQuality
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class PrefVideoUnityQuality
{
  public static PrefVideoPreset.Type ConvertUnityQualityToPreset(PrefVideoUnityQuality.Type inType)
  {
    switch (inType)
    {
      case PrefVideoUnityQuality.Type.High:
        return PrefVideoPreset.Type.High;
      case PrefVideoUnityQuality.Type.MediumHigh:
        return PrefVideoPreset.Type.MediumHigh;
      case PrefVideoUnityQuality.Type.MediumLow:
        return PrefVideoPreset.Type.MediumLow;
      default:
        return PrefVideoPreset.Type.Low;
    }
  }

  public enum Type
  {
    [LocalisationID("PSG_10012167")] High,
    [LocalisationID("PSG_10012166")] MediumHigh,
    [LocalisationID("PSG_10012165")] MediumLow,
    [LocalisationID("PSG_10012164")] Low,
  }
}
