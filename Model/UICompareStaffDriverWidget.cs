// Decompiled with JetBrains decompiler
// Type: UICompareStaffDriverWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UICompareStaffDriverWidget : MonoBehaviour
{
  public UICompareStaffDriverStatsWidget statsWidget;
  public CompareStaffScreen screen;
  private Driver mDriver;

  public Driver driver
  {
    get
    {
      return this.mDriver;
    }
  }

  public void OnStart()
  {
  }

  public void Setup(Driver inDriver)
  {
    if (inDriver == null)
      return;
    this.mDriver = inDriver;
    this.statsWidget.Setup(this.mDriver);
  }
}
