// Decompiled with JetBrains decompiler
// Type: UICompareStaffMechanicStatsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UICompareStaffMechanicStatsWidget : MonoBehaviour
{
  public UIStaffDetailsScreenMechanicBonusesWidget bonuses;
  private Mechanic mMechanic;

  public void Setup(Mechanic inMechanic)
  {
    if (inMechanic == null)
      return;
    this.mMechanic = inMechanic;
    this.SetBonuses();
  }

  private void SetBonuses()
  {
    this.bonuses.Setup(this.mMechanic);
  }
}
