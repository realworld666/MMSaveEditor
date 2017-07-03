// Decompiled with JetBrains decompiler
// Type: UICompareStaffEngineerStatsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UICompareStaffEngineerStatsWidget : MonoBehaviour
{
  public UIStaffDetailsScreenEngineerComponentsWidget components;
  private Engineer mEngineer;

  public void Setup(Engineer inEngineer)
  {
    if (inEngineer == null)
      return;
    this.mEngineer = inEngineer;
    this.SetComponents();
  }

  private void SetComponents()
  {
    this.components.Setup(this.mEngineer);
  }
}
