// Decompiled with JetBrains decompiler
// Type: UITeamStatsBarEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UITeamStatsBarEntry : MonoBehaviour
{
  public TeamStatistics.Stat stat = TeamStatistics.Stat.Drivers;
  public bool changeBarColor = true;
  public bool flipRollover = true;
  protected AnimatedFloat mTeamStat = new AnimatedFloat();
  protected Color mColor = Color.black;
  public Slider average;
  public Slider team;
  public Image fill;
  public GameObject gtSeriesImage;
  public GameObject singleSeaterImage;
  protected ChampionshipStatistics.StatisticsReport mReport;

  public float teamStat
  {
    get
    {
      return this.mTeamStat.targetValue;
    }
  }

  public virtual void Setup(Team inTeam)
  {
    this.mReport = ChampionshipStatistics.GenerateReport(inTeam, this.stat);
    this.SetStat(this.mReport.averageNormalized, this.mReport.statNormalized);
    if ((Object) this.gtSeriesImage != (Object) null)
      GameUtility.SetActive(this.gtSeriesImage, inTeam.championship.series == Championship.Series.GTSeries);
    if (!((Object) this.singleSeaterImage != (Object) null))
      return;
    GameUtility.SetActive(this.singleSeaterImage, inTeam.championship.series == Championship.Series.SingleSeaterSeries);
  }

  public virtual void SetStat(float inAverageStat, float inTeamStat)
  {
    this.mTeamStat.SetValue((float) (0.0500000007450581 + (double) Mathf.Clamp01(inTeamStat) * 0.949999988079071), AnimatedFloat.Animation.Animate, 0.0f, 0.8f, EasingUtility.Easing.InOutCubic);
    if ((Object) this.average != (Object) null)
      GameUtility.SetSliderAmountIfDifferent(this.average, Mathf.Clamp01(inAverageStat), 1000f);
    this.UpdateBar();
  }

  private void Update()
  {
    this.mTeamStat.Update();
    this.UpdateBar();
  }

  private void UpdateBar()
  {
    if ((Object) this.team != (Object) null)
      GameUtility.SetSliderAmountIfDifferent(this.team, this.mTeamStat.value, 1000f);
    else if ((Object) this.fill != (Object) null)
      GameUtility.SetImageFillAmountIfDifferent(this.fill, this.mTeamStat.value, 1f / 512f);
    this.mColor = UIConstants.GetBarColor(this.mTeamStat.value);
    if (!((Object) this.fill != (Object) null) || !this.changeBarColor || !(this.fill.color != this.mColor))
      return;
    this.fill.color = this.mColor;
  }

  public virtual void ShowRollover()
  {
    if (this.mReport == null)
      return;
    TeamStatsRollover.ShowRollover(this.mReport, this.flipRollover);
  }

  public virtual void HideRollover()
  {
    TeamStatsRollover.HideRollover();
  }
}
