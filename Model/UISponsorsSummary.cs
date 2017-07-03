// Decompiled with JetBrains decompiler
// Type: UISponsorsSummary
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using MM2;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISponsorsSummary : MonoBehaviour
{
  public TextMeshProUGUI driver1Name;
  public TextMeshProUGUI driver2Name;
  public TextMeshProUGUI driver1Marketability;
  public TextMeshProUGUI driver2Marketability;
  public TextMeshProUGUI teamMarketability;
  public TextMeshProUGUI totalMarketability;
  public Slider marketabilitySlider;
  public GameObject appealLock;
  public GameObject[] appealStars;
  public SponsorsScreen screen;
  private Team mTeam;

  public void OnStart()
  {
  }

  public void Setup()
  {
    this.mTeam = Game.instance.player.team;
    Driver driver1 = this.mTeam.GetDriver(0);
    Driver driver2 = this.mTeam.GetDriver(1);
    this.driver1Name.text = string.Format("{0}:", (object) driver1.name);
    this.driver2Name.text = string.Format("{0}:", (object) driver2.name);
    float marketability1 = driver1.GetDriverStats().marketability;
    float marketability2 = driver2.GetDriverStats().marketability;
    float marketability3 = this.mTeam.GetMarketability();
    this.driver1Marketability.text = marketability1.ToString("P0", (IFormatProvider) Localisation.numberFormatter);
    this.driver2Marketability.text = marketability2.ToString("P0", (IFormatProvider) Localisation.numberFormatter);
    this.teamMarketability.text = marketability3.ToString("P0", (IFormatProvider) Localisation.numberFormatter);
    float inNormValue = (float) (((double) marketability1 + (double) marketability2 + (double) marketability3) / 3.0);
    this.UpdateMarketabilityAchievements(inNormValue * 100f);
    this.totalMarketability.text = GameUtility.GetPercentageText(inNormValue, 1f, false, false);
    this.marketabilitySlider.value = this.mTeam.GetTeamTotalMarketability();
    this.SetAppealStars(this.mTeam.sponsorAppeal);
    GameUtility.SetActive(this.appealLock, !this.mTeam.perksManager.CheckPerkUnlocked(TeamPerk.Type.SponsorsLevel5));
  }

  private void SetAppealStars(int inTotal)
  {
    for (int index = 0; index < this.appealStars.Length; ++index)
      GameUtility.SetActive(this.appealStars[index], index < inTotal);
  }

  private void UpdateMarketabilityAchievements(float inMarketability)
  {
    if (!MathsUtility.Approximately(inMarketability, 100f, 0.05f))
      return;
    App.instance.steamAchievementsManager.UnlockAchievement(Achievements.AchievementEnum.Reach_Max_Marketability);
  }
}
