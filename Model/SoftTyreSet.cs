// Decompiled with JetBrains decompiler
// Type: SoftTyreSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SoftTyreSet : TyreSet
{
  public override Color GetColor()
  {
    return GameUtility.ColorFromInts(234, 214, 65);
  }

  public override TyreSet.Compound GetCompound()
  {
    return TyreSet.Compound.Soft;
  }

  public override TyreSet.Tread GetTread()
  {
    return TyreSet.Tread.Slick;
  }

  public override string GetName()
  {
    return Localisation.LocaliseID("PSG_10000473", (GameObject) null);
  }

  public override string GetShortName()
  {
    return "S";
  }

  public override float GetDurabilityForUI()
  {
    return 0.48f;
  }
}
