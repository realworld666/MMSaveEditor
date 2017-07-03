// Decompiled with JetBrains decompiler
// Type: UIChooseSeriesOverview
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIChooseSeriesOverview : MonoBehaviour
{
  public ActivateForSeries.GameObjectData[] seriesSpecificData = new ActivateForSeries.GameObjectData[0];
  public UIChampionshipLogo seriesLogo;
  public Image seriesBacking;
  public Image seriesStripBar;
  public Image seriesLocationBacking;
  public TextMeshProUGUI seriesNameLabel;
  public TextMeshProUGUI descriptionLabel;
  public TextMeshProUGUI eventsLabel;
  public TextMeshProUGUI locationsLabel;
  public GameObject eventsTextGameObject;
  public GameObject[] trophies;
  public GameObject[] carImages;
  public TextMeshProUGUI prizeFund;
  public TextMeshProUGUI audience;
  public TextMeshProUGUI races;
  public TextMeshProUGUI teams;
  public UITeamStatsBarEntry[] bars;
  public TextMeshProUGUI carModelLabel;
  public TextMeshProUGUI carWeightLabel;
  public TextMeshProUGUI carTopSpeedLabel;
  public TextMeshProUGUI carAccelerationTitle;
  public TextMeshProUGUI carAccelerationLabel;
  public TextMeshProUGUI carPartManufacturingLabel;
  public Button rulesButton;
  private Championship mChampionship;

  public void Setup(Championship inChampionship)
  {
    this.mChampionship = inChampionship;
    GameUtility.SetActiveForSeries(inChampionship, this.seriesSpecificData);
    this.seriesLogo.SetChampionship(this.mChampionship);
    this.seriesBacking.color = this.mChampionship.uiColor;
    this.seriesStripBar.color = this.mChampionship.uiColor;
    this.seriesLocationBacking.color = this.mChampionship.uiColor;
    this.seriesNameLabel.text = this.mChampionship.GetChampionshipName(false);
    this.descriptionLabel.text = this.mChampionship.GetChampionshipDescription();
    GameUtility.SetActive(this.eventsTextGameObject, this.mChampionship.eventCount != 1);
    StringVariableParser.intValue1 = this.mChampionship.eventCount;
    this.eventsLabel.text = Localisation.LocaliseID("PSG_10010445", (GameObject) null);
    this.rulesButton.onClick.RemoveAllListeners();
    this.rulesButton.onClick.AddListener(new UnityAction(this.ShowRules));
    StringVariableParser.intValue1 = this.mChampionship.eventLocations;
    this.locationsLabel.text = Localisation.LocaliseID("PSG_10010446", (GameObject) null);
    this.prizeFund.text = GameUtility.GetCurrencyString((long) this.mChampionship.prizeFund, 0);
    this.audience.text = GameUtility.FormatNumberString(this.mChampionship.tvAudience);
    this.teams.text = this.mChampionship.standings.teamEntryCount.ToString();
    this.races.text = this.mChampionship.eventCount.ToString();
    this.carModelLabel.text = Localisation.LocaliseID(this.mChampionship.modelID, (GameObject) null);
    this.carWeightLabel.text = GameUtility.GetWeightText((float) this.mChampionship.weightKG, 2);
    this.carTopSpeedLabel.text = GameUtility.GetSpeedText(GameUtility.MilesPerHourToMetersPerSecond((float) this.mChampionship.topSpeedMPH), 1f);
    this.carAccelerationTitle.text = GameUtility.GetAccelerationSpeedRangeText();
    this.carAccelerationLabel.text = Localisation.LocaliseID(this.mChampionship.accelerationID, (GameObject) null);
    this.carPartManufacturingLabel.text = Localisation.LocaliseID(this.mChampionship.partManufacturingID, (GameObject) null);
    this.SetTrophy();
    this.SetBars();
    this.SetCarImage();
  }

  private void SetTrophy()
  {
    for (int index = 0; index < this.trophies.Length; ++index)
      GameUtility.SetActive(this.trophies[index], index == this.mChampionship.championshipID);
  }

  private void SetBars()
  {
    this.bars[0].SetStat(0.0f, Mathf.Clamp01((float) this.mChampionship.qualityTeamAverage / 100f));
    this.bars[0].fill.color = this.mChampionship.uiColor;
    this.bars[1].SetStat(0.0f, Mathf.Clamp01((float) this.mChampionship.qualityCars / 100f));
    this.bars[1].fill.color = this.mChampionship.uiColor;
    this.bars[2].SetStat(0.0f, Mathf.Clamp01((float) this.mChampionship.qualityDrivers / 100f));
    this.bars[2].fill.color = this.mChampionship.uiColor;
    this.bars[3].SetStat(0.0f, Mathf.Clamp01((float) this.mChampionship.qualityHQ / 100f));
    this.bars[3].fill.color = this.mChampionship.uiColor;
    this.bars[4].SetStat(0.0f, Mathf.Clamp01((float) this.mChampionship.qualityStaff / 100f));
    this.bars[4].fill.color = this.mChampionship.uiColor;
    this.bars[5].SetStat(0.0f, Mathf.Clamp01((float) this.mChampionship.qualityFinances / 100f));
    this.bars[5].fill.color = this.mChampionship.uiColor;
  }

  private void SetCarImage()
  {
    for (int index = 0; index < this.carImages.Length; ++index)
      GameUtility.SetActive(this.carImages[index], index == this.mChampionship.championshipID);
  }

  private void ShowRules()
  {
    UIChampionshipRulesDialog.ShowRollover(this.mChampionship, UIChampionshipRulesDialog.Mode.CurrentRules, true, (Action) null);
  }
}
