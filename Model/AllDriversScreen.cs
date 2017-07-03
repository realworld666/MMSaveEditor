// Decompiled with JetBrains decompiler
// Type: AllDriversScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

public class AllDriversScreen : UIScreen
{
  public List<UIDriverPanelWidget> driverPanels = new List<UIDriverPanelWidget>();
  public UIContractNegotiationsWidget contractNegotiations;

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.SetupDrivers();
    this.contractNegotiations.Setup(new List<Contract.Job>()
    {
      Contract.Job.Driver
    });
    Game.instance.notificationManager.GetNotification("DriverContractConsidered").ResetCount();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
  }

  private void SetupDrivers()
  {
    Driver driver1 = Game.instance.player.team.GetDriver(0);
    Driver driver2 = Game.instance.player.team.GetDriver(1);
    Driver driver3 = Game.instance.player.team.GetDriver(2);
    Driver nextYearDriver1 = Game.instance.player.team.GetNextYearDriver(0);
    Driver nextYearDriver2 = Game.instance.player.team.GetNextYearDriver(1);
    Driver nextYearDriver3 = Game.instance.player.team.GetNextYearDriver(2);
    this.driverPanels[0].Setup(driver1, nextYearDriver1);
    this.driverPanels[1].Setup(driver2, nextYearDriver2);
    this.driverPanels[2].Setup(driver3, nextYearDriver3);
  }
}
