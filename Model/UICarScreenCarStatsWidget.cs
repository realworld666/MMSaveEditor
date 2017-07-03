// Decompiled with JetBrains decompiler
// Type: UICarScreenCarStatsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICarScreenCarStatsWidget : MonoBehaviour
{
  public UISimulationStatEntry[] statEntries;
  public Transform[] statIcons;
  public UIAbilityStars tyreWearStars;
  public UIAbilityStars fuelStars;
  public UIAbilityStars tyreHeatingStars;
  public UIAbilityStars improvabilityStars;
  public Slider overallAverageSlider;
  public Image overallImage;
  public Button changeLiveryButton;
  public bool flipRollover;
  private ChampionshipStatistics.StatisticsReport mReport;

  private void Awake()
  {
    this.changeLiveryButton.onClick.AddListener(new UnityAction(this.GoToLiveryEditScreen));
  }

  private void GoToLiveryEditScreen()
  {
    UIManager.instance.ChangeScreen("CarLiveryScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  public void Setup()
  {
    CarChassisStats chassisStats = Game.instance.player.team.carManager.GetCar(0).chassisStats;
    if (App.instance.gameStateManager.currentState is PreSeasonState && (App.instance.gameStateManager.currentState as PreSeasonState).stage >= PreSeasonState.PreSeasonStage.DesigningCar)
      chassisStats = Game.instance.player.team.carManager.nextYearCarDesign.chassisStats;
    CarPart.PartType[] partType1 = CarPart.GetPartType(Game.instance.player.team.championship.series, true);
    for (int index = 0; index < this.statEntries.Length; ++index)
    {
      UISimulationStatEntry statEntry = this.statEntries[index];
      if (index < partType1.Length)
      {
        CarPart.PartType partType2 = partType1[index];
        CarStats.StatType statForPartType = CarPart.GetStatForPartType(partType2);
        statEntry.Setup(statForPartType);
        this.SetIcon(this.statIcons[index], partType2);
      }
      GameUtility.SetActive(statEntry.gameObject, index < partType1.Length);
    }
    if (chassisStats != null)
    {
      this.improvabilityStars.SetStarsValue((float) ((double) chassisStats.improvability / (double) GameStatsConstants.chassisStatMax * 5.0), 0.0f);
      this.fuelStars.SetStarsValue((float) ((double) chassisStats.fuelEfficiency / (double) GameStatsConstants.chassisStatMax * 5.0), 0.0f);
      this.tyreWearStars.SetStarsValue((float) ((double) chassisStats.tyreWear / (double) GameStatsConstants.chassisStatMax * 5.0), 0.0f);
      this.tyreHeatingStars.SetStarsValue((float) ((double) chassisStats.tyreHeating / (double) GameStatsConstants.chassisStatMax * 5.0), 0.0f);
    }
    else
    {
      this.improvabilityStars.SetStarsValue(0.0f, 0.0f);
      this.fuelStars.SetStarsValue(0.0f, 0.0f);
      this.tyreWearStars.SetStarsValue(0.0f, 0.0f);
      this.tyreHeatingStars.SetStarsValue(0.0f, 0.0f);
    }
    this.SetOverallGraphBarData();
  }

  private void SetOverallGraphBarData()
  {
    this.mReport = ChampionshipStatistics.GenerateReport(Game.instance.player.team, TeamStatistics.Stat.Car);
    GameUtility.SetSliderAmountIfDifferent(this.overallAverageSlider, this.mReport.averageNormalized, 1000f);
    GameUtility.SetImageFillAmountIfDifferent(this.overallImage, (float) (0.0500000007450581 + (double) Mathf.Clamp01(this.mReport.statNormalized) * 0.949999988079071), 1f / 512f);
    this.overallImage.color = UIConstants.GetBarColor(this.overallImage.fillAmount);
  }

  private void SetIcon(Transform inTransform, CarPart.PartType inType)
  {
    for (int index = 0; index < inTransform.childCount; ++index)
      GameUtility.SetActive(inTransform.GetChild(index).gameObject, inType == (CarPart.PartType) index);
  }

  private void SetStars(GameObject[] inStars, float inValue)
  {
    for (int index = 0; index < inStars.Length; ++index)
      inStars[index].GetComponent<Image>().enabled = (double) index < (double) inValue;
  }

  public void ShowRollover()
  {
    if (this.mReport == null)
      return;
    TeamStatsRollover.ShowRollover(this.mReport, this.flipRollover);
  }

  public void HideRollover()
  {
    TeamStatsRollover.HideRollover();
  }
}
