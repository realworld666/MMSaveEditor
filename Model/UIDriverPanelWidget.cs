// Decompiled with JetBrains decompiler
// Type: UIDriverPanelWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIDriverPanelWidget : MonoBehaviour
{
  public UIDriverEntry currentDriver;
  public UIDriverPerformanceWidget performanceWidget;
  public UIContractInformationEntry contractInformationPanel;

  public void Setup(Driver inCurrentDriver, Driver inNextYearDriver)
  {
    this.currentDriver.Setup(inCurrentDriver);
    if ((Object) this.performanceWidget != (Object) null)
    {
      int driverIndex = Game.instance.player.team.GetDriverIndex(inCurrentDriver);
      Driver driver = Game.instance.player.team.GetDriver(driverIndex != 0 ? 0 : 1);
      this.performanceWidget.Setup(inCurrentDriver, driver);
    }
    if (inCurrentDriver == null || inNextYearDriver == null || inCurrentDriver != inNextYearDriver)
    {
      if (inNextYearDriver != null)
        this.contractInformationPanel.renew.interactable = false;
      else if (inCurrentDriver != null)
        this.contractInformationPanel.renew.interactable = true;
    }
    this.contractInformationPanel.Setup(inCurrentDriver);
  }
}
