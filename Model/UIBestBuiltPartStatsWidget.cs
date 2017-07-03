// Decompiled with JetBrains decompiler
// Type: UIBestBuiltPartStatsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UIBestBuiltPartStatsWidget : MonoBehaviour
{
  public UISimulationStatEntry[] statEntries;
  public Transform[] statIcons;
  public Slider overallAverageSlider;
  public Image overallImage;
  public bool flipRollover;
  private ChampionshipStatistics.StatisticsReport mReport;

  public void Setup()
  {
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
