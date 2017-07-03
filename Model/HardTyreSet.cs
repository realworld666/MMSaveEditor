// Decompiled with JetBrains decompiler
// Type: HardTyreSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class HardTyreSet : TyreSet
{
  public override Color GetColor()
  {
    return GameUtility.ColorFromInts(238, 102, 56);
  }

  public override TyreSet.Compound GetCompound()
  {
    return TyreSet.Compound.Hard;
  }

  public override TyreSet.Tread GetTread()
  {
    return TyreSet.Tread.Slick;
  }

  public override string GetName()
  {
    return Localisation.LocaliseID("PSG_10000468", (GameObject) null);
  }

  public override string GetShortName()
  {
    return "H";
  }

  public override float GetDurabilityForUI()
  {
    return 0.8f;
  }
}
