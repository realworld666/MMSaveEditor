// Decompiled with JetBrains decompiler
// Type: SponsorsScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class SponsorsScreen : UIScreen
{
  public UISponsorsPanel panelWidget;
  public UISponsorsSummary summaryWidget;
  private StudioScene mStudioScene;

  public StudioScene studioScene
  {
    get
    {
      return this.mStudioScene;
    }
  }

  public override void OnStart()
  {
    base.OnStart();
    this.summaryWidget.OnStart();
    UIManager.instance.dialogBoxManager.GetDialog<UISponsorsOverview>().OnStart();
    UIManager.instance.dialogBoxManager.GetDialog<UISponsorsOffers>().OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.LoadScene();
    this.Refresh();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
  }

  public override void OnExit()
  {
    base.OnExit();
    this.panelWidget.CloseRollover();
    SceneManager.instance.LeaveCurrentScene();
  }

  private void LoadScene()
  {
    SceneManager.instance.SwitchScene("TrackFrontEnd");
    GameObject sceneGameObject = SceneManager.instance.GetSceneGameObject("TrackFrontEnd");
    if (!((UnityEngine.Object) sceneGameObject != (UnityEngine.Object) null) || this.screenMode != UIScreen.ScreenMode.Mode3D)
      return;
    this.mStudioScene = sceneGameObject.GetComponent<StudioScene>();
    if (!Game.instance.player.IsUnemployed())
      this.mStudioScene.SetSeries(Game.instance.player.team.championship.series);
    this.mStudioScene.EnableCamera("SponsorSlotsCamera");
    if (this.UseNextYearsCar())
    {
      this.mStudioScene.SetCarType(StudioScene.Car.NextYearCar);
      this.RefreshCar();
    }
    else
      this.mStudioScene.SetCarType(StudioScene.Car.CurrentCar);
    this.mStudioScene.SetCarVisualsToCurrentGame();
    this.mStudioScene.studioSponsorCameraController.Setup(Game.instance.player.team.championship);
    this.mStudioScene.studioSponsorCameraController.SetStartCamera(0);
  }

  public void RefreshCar()
  {
    if (!((UnityEngine.Object) this.mStudioScene != (UnityEngine.Object) null))
      return;
    this.mStudioScene.SetCarVisualsToCurrentGame();
    Team team = Game.instance.player.team;
    team.carManager.frontendCar.RefreshSponsorData(team);
    if (!this.UseNextYearsCar())
      return;
    team.carManager.nextFrontendCar.RefreshSponsorData(team);
  }

  public void Refresh()
  {
    UIManager.instance.dialogBoxManager.GetDialog<UISponsorsOverview>().Close();
    UIManager.instance.dialogBoxManager.GetDialog<UISponsorsOffers>().Hide();
    this.panelWidget.Setup();
    this.summaryWidget.Setup();
  }

  public void ProcessTransaction(ContractSponsor inOffer, Action inActionSuccess)
  {
    if (inOffer == null)
      return;
    if (inOffer.upfrontValue > 0)
    {
      StringVariableParser.sponsor = inOffer.sponsor;
      StringVariableParser.intValue1 = inOffer.contractTotalRaces;
      string inName = inOffer.contractTotalRaces <= 1 ? Localisation.LocaliseID("PSG_10010954", (GameObject) null) : Localisation.LocaliseID("PSG_10010579", (GameObject) null);
      Transaction transaction = new Transaction(Transaction.Group.Sponsor, Transaction.Type.Credit, (long) inOffer.upfrontValue, inName);
      Game.instance.player.team.financeController.finance.ProcessTransactions(inActionSuccess, (Action) null, true, transaction);
    }
    else
      inActionSuccess();
  }

  private bool UseNextYearsCar()
  {
    if (!(App.instance.gameStateManager.currentState is PreSeasonState))
      return false;
    PreSeasonState currentState = (PreSeasonState) App.instance.gameStateManager.currentState;
    if (currentState.stage > PreSeasonState.PreSeasonStage.DesigningCar)
      return currentState.stage < PreSeasonState.PreSeasonStage.ChooseLivery;
    return false;
  }
}
