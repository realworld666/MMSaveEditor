// Decompiled with JetBrains decompiler
// Type: UISimulationStatEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISimulationStatEntry : MonoBehaviour
{
  private CarStats.StatType mStat = CarStats.StatType.Acceleration;
  private ChampionshipStatistics.StatisticsReport mReport = new ChampionshipStatistics.StatisticsReport();
  private List<Team> mTeamsIgnore = new List<Team>();
  public TextMeshProUGUI statName;
  public Image car1statBar;
  public Image car1GreenStatBar;
  public Image car1RedStatBar;
  public GameObject specPartContainer;
  public Slider gridAverageSlider;
  public CanvasGroup canvasGroup;
  public bool flipRollover;
  public bool statsRollover;

  public CarStats.StatType stat
  {
    get
    {
      return this.mStat;
    }
  }

  public void Setup(CarStats.StatType inStat)
  {
    GameUtility.SetActive(this.car1GreenStatBar.gameObject, false);
    GameUtility.SetActive(this.car1RedStatBar.gameObject, false);
    this.mStat = inStat;
    this.statName.text = Localisation.LocaliseEnum((Enum) (CarStats.CarStatShortName) this.mStat);
    CarManager carManager = Game.instance.player.team.carManager;
    float highestStatOfType = carManager.partInventory.GetHighestStatOfType(CarPart.GetPartForStatType(this.mStat, Game.instance.player.team.championship.series), CarPartStats.CarPartStat.MainStat);
    this.mReport.carStat = this.mStat;
    if (Game.instance.player.team.championship.rules.specParts.Contains(CarPart.GetPartForStatType(this.mStat, Game.instance.player.team.championship.series)))
    {
      this.car1statBar.fillAmount = 0.0f;
      GameUtility.SetActive(this.gridAverageSlider.gameObject, false);
      GameUtility.SetActive(this.specPartContainer, true);
    }
    else
    {
      float carStatValueOnGrid1 = carManager.GetCarStatValueOnGrid(this.mStat, CarManager.MedianTypes.Lowest);
      float carStatValueOnGrid2 = carManager.GetCarStatValueOnGrid(this.mStat, CarManager.MedianTypes.Highest);
      float carStatValueOnGrid3 = carManager.GetCarStatValueOnGrid(this.mStat, CarManager.MedianTypes.Average);
      float num = !Mathf.Approximately(carStatValueOnGrid2, carStatValueOnGrid1) ? (float) (((double) carStatValueOnGrid3 - (double) carStatValueOnGrid1) / ((double) carStatValueOnGrid2 - (double) carStatValueOnGrid1)) : 0.0f;
      GameUtility.SetImageFillAmountIfDifferent(this.car1statBar, (float) (0.0500000007450581 + (double) Mathf.Clamp01(!Mathf.Approximately(carStatValueOnGrid2, carStatValueOnGrid1) ? (float) (((double) highestStatOfType - (double) carStatValueOnGrid1) / ((double) carStatValueOnGrid2 - (double) carStatValueOnGrid1)) : 0.0f) * 0.949999988079071), 1f / 512f);
      this.car1statBar.color = UIConstants.GetBarColor(this.car1statBar.fillAmount);
      GameUtility.SetSliderAmountIfDifferent(this.gridAverageSlider, Mathf.Clamp01(num), 1000f);
      GameUtility.SetActive(this.gridAverageSlider.gameObject, true);
      GameUtility.SetActive(this.specPartContainer, false);
      Team team1 = Game.instance.player.team;
      Car carOfBestStat = carManager.GetCarOfBestStat(inStat);
      List<Car> carStandingsOnStat = CarManager.GetCarStandingsOnStat(this.mStat, team1.championship);
      int count = carStandingsOnStat.Count;
      this.mReport.teamsOrdered.Clear();
      this.mTeamsIgnore.Clear();
      for (int index = 0; index < count; ++index)
      {
        Team team2 = carStandingsOnStat[index].carManager.team;
        if (!this.mTeamsIgnore.Contains(team2))
        {
          this.mReport.teamsOrdered.Add(team2);
          this.mTeamsIgnore.Add(team2);
          if (team2.IsPlayersTeam())
            this.mReport.teamPosition = this.mReport.teamsOrdered.Count;
        }
      }
      this.mReport.championship = Game.instance.player.team.championship;
      this.mReport.bestTeam = carStandingsOnStat[0].carManager.team;
      this.mReport.worstTeam = carStandingsOnStat[carStandingsOnStat.Count - 1].carManager.team;
      this.mReport.bestDriver = team1.GetDriver(carOfBestStat.identifier);
    }
  }

  public static float GetNormalizedValueForStat(CarManager inCarManager, CarStats.StatType inStat)
  {
    float highestStatOfType = inCarManager.partInventory.GetHighestStatOfType(CarPart.GetPartForStatType(inStat, inCarManager.team.championship.series), CarPartStats.CarPartStat.MainStat);
    float carStatValueOnGrid1 = inCarManager.GetCarStatValueOnGrid(inStat, CarManager.MedianTypes.Lowest);
    float carStatValueOnGrid2 = inCarManager.GetCarStatValueOnGrid(inStat, CarManager.MedianTypes.Highest);
    if (Mathf.Approximately(carStatValueOnGrid2, carStatValueOnGrid1))
      return 0.0f;
    return (float) (((double) highestStatOfType - (double) carStatValueOnGrid1) / ((double) carStatValueOnGrid2 - (double) carStatValueOnGrid1));
  }

  public void HideAverageLine(bool inValue)
  {
    GameUtility.SetActive(this.gridAverageSlider.gameObject, inValue);
  }

  public void ShowRollover()
  {
    if (!this.statsRollover || this.mReport == null)
      return;
    TeamStatsRollover.ShowRolloverCar(this.mReport, this.flipRollover);
  }

  public void HideRollover()
  {
    TeamStatsRollover.HideRollover();
  }
}
