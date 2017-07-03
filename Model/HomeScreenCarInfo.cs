// Decompiled with JetBrains decompiler
// Type: HomeScreenCarInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;

public class HomeScreenCarInfo : HomeScreenInfoPanel
{
  private HomeScreenCarInfo.Stage mStage = HomeScreenCarInfo.Stage.General;
  private CarManager mCarManager;
  private Car mCarOne;
  private Car mCarTwo;

  public override void OnStart()
  {
    this.button.onClick.AddListener(new UnityAction(this.OnButton));
  }

  public override void Setup()
  {
    this.mCarManager = Game.instance.player.team.carManager;
    this.mCarOne = this.mCarManager.GetCar(0);
    this.mCarTwo = this.mCarManager.GetCar(1);
    this.SelectStage();
    this.SetStage();
  }

  private void OnButton()
  {
    switch (this.mStage)
    {
      case HomeScreenCarInfo.Stage.FitParts:
        UIManager.instance.ChangeScreen("CarPartFittingScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
        break;
      case HomeScreenCarInfo.Stage.ImproveParts:
        UIManager.instance.ChangeScreen("FactoryPartDevelopmentScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
        break;
      case HomeScreenCarInfo.Stage.DesignPart:
        UIManager.instance.ChangeScreen("PartDesignScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
        break;
      case HomeScreenCarInfo.Stage.General:
        UIManager.instance.ChangeScreen("CarScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
        break;
    }
  }

  private void SelectStage()
  {
    bool flag1 = App.instance.gameStateManager.currentState is PreSeasonState;
    bool flag2 = this.mCarManager.BothCarsReadyForEvent();
    bool flag3 = !this.mCarManager.partImprovement.WorkOnStatActive(CarPartStats.CarPartStat.Performance) && (PartImprovement.AnyPartNeedsWork(CarPartStats.CarPartStat.Performance, this.mCarOne.seriesCurrentParts) || PartImprovement.AnyPartNeedsWork(CarPartStats.CarPartStat.Performance, this.mCarTwo.seriesCurrentParts));
    bool flag4 = !this.mCarManager.partImprovement.WorkOnStatActive(CarPartStats.CarPartStat.Reliability) && (PartImprovement.AnyPartNeedsWork(CarPartStats.CarPartStat.Reliability, this.mCarOne.seriesCurrentParts) || PartImprovement.AnyPartNeedsWork(CarPartStats.CarPartStat.Reliability, this.mCarTwo.seriesCurrentParts));
    bool flag5 = this.mCarManager.carPartDesign.stage == CarPartDesign.Stage.Designing;
    if (!flag1 && !flag2)
      this.mStage = HomeScreenCarInfo.Stage.FitParts;
    else if (!flag1 && !this.mCarManager.partImprovement.FixingCondition() && (flag3 && flag4))
      this.mStage = HomeScreenCarInfo.Stage.ImproveParts;
    else if (!flag1 && !flag5)
      this.mStage = HomeScreenCarInfo.Stage.DesignPart;
    else
      this.mStage = HomeScreenCarInfo.Stage.General;
  }

  private void SetStage()
  {
    switch (this.mStage)
    {
      case HomeScreenCarInfo.Stage.FitParts:
        this.titleLabel.text = Localisation.LocaliseID("PSG_10010261", (GameObject) null);
        this.descriptionLabel.text = Localisation.LocaliseID("PSG_10010262", (GameObject) null);
        this.buttonLabel.text = Localisation.LocaliseID("PSG_10008013", (GameObject) null);
        this.statusLabel.text = Localisation.LocaliseID("PSG_10010263", (GameObject) null);
        break;
      case HomeScreenCarInfo.Stage.ImproveParts:
        this.titleLabel.text = Localisation.LocaliseID("PSG_10010264", (GameObject) null);
        this.descriptionLabel.text = Localisation.LocaliseID("PSG_10010265", (GameObject) null);
        this.buttonLabel.text = Localisation.LocaliseID("PSG_10008766", (GameObject) null);
        this.statusLabel.text = Localisation.LocaliseID("PSG_10010266", (GameObject) null);
        break;
      case HomeScreenCarInfo.Stage.DesignPart:
        this.titleLabel.text = Localisation.LocaliseID("PSG_10004132", (GameObject) null);
        this.descriptionLabel.text = Localisation.LocaliseID("PSG_10010267", (GameObject) null);
        this.buttonLabel.text = Localisation.LocaliseID("PSG_10010268", (GameObject) null);
        this.statusLabel.text = Localisation.LocaliseID("PSG_10010269", (GameObject) null);
        break;
      case HomeScreenCarInfo.Stage.General:
        this.titleLabel.text = Localisation.LocaliseID("PSG_10004390", (GameObject) null);
        this.descriptionLabel.text = Localisation.LocaliseID("PSG_10010270", (GameObject) null);
        this.buttonLabel.text = Localisation.LocaliseID("PSG_10010271", (GameObject) null);
        this.statusLabel.text = Localisation.LocaliseID("PSG_10009316", (GameObject) null);
        break;
    }
  }

  public override bool isDefaultState()
  {
    return this.mStage == HomeScreenCarInfo.Stage.General;
  }

  public enum Stage
  {
    FitParts,
    ImproveParts,
    DesignPart,
    General,
  }
}
