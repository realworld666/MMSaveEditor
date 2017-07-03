// Decompiled with JetBrains decompiler
// Type: UITeamScreenHistoryWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UITeamScreenHistoryWidget : MonoBehaviour
{
  public UITeamStatsBarEntry carStat;
  public UITeamStatsBarEntry driversStat;
  public UITeamStatsBarEntry headquartersStat;
  public UITeamStatsBarEntry staffStat;
  public UITeamStatsBarEntry sponsorsStat;
  public UIAbilityStars tyreWearStars;
  public UIAbilityStars fuelStars;
  public UIAbilityStars tyreHeatingStars;
  public UIAbilityStars improvabilityStars;

  public void Setup(Team inTeam)
  {
    this.carStat.Setup(inTeam);
    this.driversStat.Setup(inTeam);
    this.headquartersStat.Setup(inTeam);
    this.staffStat.Setup(inTeam);
    this.sponsorsStat.Setup(inTeam);
    CarChassisStats carChassisStats1 = inTeam.carManager.GetCar(0).chassisStats;
    CarChassisStats carChassisStats2 = (CarChassisStats) null;
    if (App.instance.gameStateManager.currentState is PreSeasonState && inTeam.IsPlayersTeam() && (App.instance.gameStateManager.currentState as PreSeasonState).stage >= PreSeasonState.PreSeasonStage.DesigningCar)
      carChassisStats2 = inTeam.carManager.nextYearCarDesign.chassisStats;
    if (carChassisStats2 != null)
      carChassisStats1 = carChassisStats2;
    this.improvabilityStars.SetStarsValue((float) ((double) carChassisStats1.improvability / (double) GameStatsConstants.chassisStatMax * 5.0), 0.0f);
    this.fuelStars.SetStarsValue((float) ((double) carChassisStats1.fuelEfficiency / (double) GameStatsConstants.chassisStatMax * 5.0), 0.0f);
    this.tyreWearStars.SetStarsValue((float) ((double) carChassisStats1.tyreWear / (double) GameStatsConstants.chassisStatMax * 5.0), 0.0f);
    this.tyreHeatingStars.SetStarsValue((float) ((double) carChassisStats1.tyreHeating / (double) GameStatsConstants.chassisStatMax * 5.0), 0.0f);
  }
}
