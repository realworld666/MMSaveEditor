// Decompiled with JetBrains decompiler
// Type: HomeScreenTeamStatsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class HomeScreenTeamStatsWidget : MonoBehaviour
{
  public UITeamStatsBarEntry carStat;
  public UITeamStatsBarEntry driversStat;
  public UITeamStatsBarEntry headquartersStat;
  public UITeamStatsBarEntry staffStat;
  public UITeamStatsBarEntry sponsorsStat;
  public HomeScreenStatsInfoWidget infoWidget;

  public void Setup()
  {
    Team team = Game.instance.player.team;
    this.carStat.Setup(team);
    this.driversStat.Setup(team);
    this.headquartersStat.Setup(team);
    this.staffStat.Setup(team);
    this.sponsorsStat.Setup(team);
    this.ShowInfoPanel();
  }

  private void ShowInfoPanel()
  {
    this.infoWidget.panels[0].barValue = this.carStat.teamStat;
    this.infoWidget.panels[1].barValue = this.driversStat.teamStat;
    this.infoWidget.panels[2].barValue = this.headquartersStat.teamStat;
    this.infoWidget.panels[3].barValue = this.staffStat.teamStat;
    this.infoWidget.panels[4].barValue = this.sponsorsStat.teamStat;
    this.infoWidget.AutoSelect();
  }
}
