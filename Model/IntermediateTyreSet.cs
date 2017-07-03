// Decompiled with JetBrains decompiler
// Type: IntermediateTyreSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class IntermediateTyreSet : TyreSet
{
  public override Color GetColor()
  {
    return GameUtility.ColorFromInts(134, 206, 71);
  }

  public override TyreSet.Compound GetCompound()
  {
    return TyreSet.Compound.Intermediate;
  }

  public override TyreSet.Tread GetTread()
  {
    return TyreSet.Tread.LightTread;
  }

  public override string GetName()
  {
    return Localisation.LocaliseID("PSG_10000471", (GameObject) null);
  }

  public override string GetShortName()
  {
    return "I";
  }

  public override float GetDurabilityForUI()
  {
    return 0.8f;
  }

  public override string GetPreferedConditionsText()
  {
    return "Damp Track";
  }
}
