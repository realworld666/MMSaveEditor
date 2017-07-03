// Decompiled with JetBrains decompiler
// Type: PlayerScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;

public class PlayerScreen : UIScreen
{
  public UIPlayerWidget playerWidget;
  public UIPlayerStatsWidget statsWidget;
  public UIPlayerNotificationsWidget notificationsWidget;
  public UIPlayerHappinessWidget chairmanWidget;
  public UIPlayerAvailableJobsWidget jobsWidget;
  public UIPlayerJobApplicationsWidget applicationsWidget;
  public PersonCareerWidget careerWidget;
  public UIPlayerTrophiesWidget trophiesWidget;
  private Player mPlayer;

  public override void OnStart()
  {
    base.OnStart();
    this.playerWidget.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    App.instance.dlcManager.OnOwnedDlcChanged += new Action(this.jobsWidget.Setup);
    this.showNavigationBars = true;
    this.mPlayer = Game.instance.player;
    bool inIsActive = !this.mPlayer.IsUnemployed();
    this.playerWidget.Setup(this.mPlayer);
    this.statsWidget.Setup(this.mPlayer);
    this.careerWidget.Setup((Person) this.mPlayer);
    this.trophiesWidget.Setup(this.mPlayer);
    this.jobsWidget.Setup();
    GameUtility.SetActive(this.notificationsWidget.gameObject, !inIsActive);
    GameUtility.SetActive(this.applicationsWidget.gameObject, this.mPlayer.jobApplications.Count > 0);
    GameUtility.SetActive(this.chairmanWidget.gameObject, inIsActive);
    if (!inIsActive)
      this.notificationsWidget.Setup();
    else
      this.chairmanWidget.Setup(this.mPlayer);
    if (this.applicationsWidget.gameObject.activeSelf)
      this.applicationsWidget.Setup();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
  }

  public override void OnExit()
  {
    base.OnExit();
    this.notificationsWidget.OnExit();
    App.instance.dlcManager.OnOwnedDlcChanged -= new Action(this.jobsWidget.Setup);
  }
}
