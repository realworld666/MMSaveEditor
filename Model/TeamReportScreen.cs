// Decompiled with JetBrains decompiler
// Type: TeamReportScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

public class TeamReportScreen : UIScreen
{
  public List<UITeamReportScreenDriverWidget> driverPanels = new List<UITeamReportScreenDriverWidget>();
  public UITeamReportScreenChairmanWidget chairmanWidget;
  private TeamReportScreen.State mState;
  private float mTimer;

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.mState = TeamReportScreen.State.None;
    this.mTimer = 0.0f;
    this.SetupDrivers();
    this.chairmanWidget.Setup(Game.instance.player.team.chairman);
    this.SetState(TeamReportScreen.State.WaitForSave);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionTeam, 0.0f);
  }

  private void SetupDrivers()
  {
    Driver driver1 = Game.instance.player.team.GetDriver(0);
    Driver driver2 = Game.instance.player.team.GetDriver(1);
    Driver driver3 = Game.instance.player.team.GetDriver(2);
    this.driverPanels[0].Setup(driver1);
    this.driverPanels[1].Setup(driver2);
    this.driverPanels[2].Setup(driver3);
  }

  private void SetState(TeamReportScreen.State inState)
  {
    if (this.mState == inState)
      return;
    switch (inState)
    {
      case TeamReportScreen.State.AnimateDriver1:
        this.driverPanels[0].StartAnimating();
        break;
      case TeamReportScreen.State.AnimateDriver2:
        this.driverPanels[1].StartAnimating();
        break;
      case TeamReportScreen.State.AnimateReserveDriver:
        this.driverPanels[2].StartAnimating();
        break;
      case TeamReportScreen.State.AnimateChairman:
        this.chairmanWidget.StartAnimating();
        break;
    }
    this.mState = inState;
    this.mTimer = 0.0f;
  }

  private void Update()
  {
    this.mTimer += GameTimer.deltaTime;
    if ((double) this.mTimer < 1.0)
      return;
    switch (this.mState)
    {
      case TeamReportScreen.State.WaitForSave:
        if (App.instance.saveSystem.status != SaveSystem.Status.InActive)
          break;
        this.SetState(TeamReportScreen.State.AnimateDriver1);
        break;
      case TeamReportScreen.State.AnimateDriver1:
        this.SetState(TeamReportScreen.State.AnimateDriver2);
        break;
      case TeamReportScreen.State.AnimateDriver2:
        this.SetState(TeamReportScreen.State.AnimateReserveDriver);
        break;
      case TeamReportScreen.State.AnimateReserveDriver:
        this.SetState(TeamReportScreen.State.AnimateChairman);
        break;
      case TeamReportScreen.State.AnimateChairman:
        this.SetState(TeamReportScreen.State.End);
        break;
    }
  }

  private enum State
  {
    None,
    WaitForSave,
    AnimateDriver1,
    AnimateDriver2,
    AnimateReserveDriver,
    AnimateChairman,
    End,
  }
}
