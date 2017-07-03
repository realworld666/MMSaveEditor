// Decompiled with JetBrains decompiler
// Type: UICompareStaffEngineerWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UICompareStaffEngineerWidget : MonoBehaviour
{
  public UICompareStaffEngineerStatsWidget statsWidget;
  public CompareStaffScreen screen;
  private Engineer mEngineer;

  public Engineer engineer
  {
    get
    {
      return this.mEngineer;
    }
  }

  public void OnStart()
  {
  }

  public void Setup(Engineer inEngineer)
  {
    if (inEngineer == null)
      return;
    this.mEngineer = inEngineer;
    this.statsWidget.Setup(inEngineer);
  }
}
