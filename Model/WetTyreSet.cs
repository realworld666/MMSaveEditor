// Decompiled with JetBrains decompiler
// Type: WetTyreSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class WetTyreSet : TyreSet
{
  public override Color GetColor()
  {
    return GameUtility.ColorFromInts(78, 147, 208);
  }

  public override TyreSet.Compound GetCompound()
  {
    return TyreSet.Compound.Wet;
  }

  public override TyreSet.Tread GetTread()
  {
    return TyreSet.Tread.HeavyTread;
  }

  public override string GetName()
  {
    return Localisation.LocaliseID("PSG_10000470", (GameObject) null);
  }

  public override string GetShortName()
  {
    return "W";
  }

  public override float GetDurabilityForUI()
  {
    return 0.8f;
  }

  public override string GetPreferedConditionsText()
  {
    return "Wet Track";
  }
}
