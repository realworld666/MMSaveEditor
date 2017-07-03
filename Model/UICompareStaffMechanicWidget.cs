// Decompiled with JetBrains decompiler
// Type: UICompareStaffMechanicWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UICompareStaffMechanicWidget : MonoBehaviour
{
  public UICompareStaffMechanicStatsWidget statsWidget;
  public CompareStaffScreen screen;
  private Mechanic mMechanic;

  public Mechanic mechanic
  {
    get
    {
      return this.mMechanic;
    }
  }

  public void OnStart()
  {
  }

  public void Setup(Mechanic inMechanic)
  {
    if (inMechanic == null)
      return;
    this.mMechanic = inMechanic;
    this.statsWidget.Setup(inMechanic);
  }
}
