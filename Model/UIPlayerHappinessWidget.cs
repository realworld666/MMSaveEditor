// Decompiled with JetBrains decompiler
// Type: UIPlayerHappinessWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIPlayerHappinessWidget : MonoBehaviour
{
  public UIGauge happiness;
  public UIJobSecurity jobSecurity;
  public TextMeshProUGUI ultimatumLabel;
  public GameObject noUltimatum;
  public GameObject ultimatum;
  public TextMeshProUGUI seasonTarget;
  public TextMeshProUGUI seasonStatus;
  public GameObject seasonObjective;
  public GameObject seasonDeciding;
  public GameObject positiveHappinessIconModifier;
  public GameObject negativeHappinessIconModifier;
  private TeamPrincipal mTeamPrincipal;
  private float mHappiness;

  public void Setup(Player inPlayer)
  {
    if (inPlayer == null)
      return;
    this.mTeamPrincipal = (TeamPrincipal) inPlayer;
    Team team = this.mTeamPrincipal.contract.GetTeam();
    this.mHappiness = team.chairman.happinessNormalized;
    this.happiness.SetValueRange(0.0f, 1f);
    this.happiness.SetValue(this.mHappiness, UIGauge.AnimationSetting.Animate);
    float happinessModifier = team.chairman.GetHappinessModifier();
    GameUtility.SetActive(this.positiveHappinessIconModifier, (double) happinessModifier > 0.0);
    GameUtility.SetActive(this.negativeHappinessIconModifier, (double) happinessModifier < 0.0);
    this.jobSecurity.SetJobSecurity(this.mTeamPrincipal.jobSecurity);
    GameUtility.SetActive(this.noUltimatum, !team.chairman.hasMadeUltimatum);
    GameUtility.SetActive(this.ultimatum, team.chairman.hasMadeUltimatum);
    if (this.ultimatum.activeSelf)
      this.ultimatumLabel.text = GameUtility.FormatForPosition(team.chairman.ultimatum.positionExpected, (string) null);
    GameUtility.SetActive(this.seasonObjective, team.chairman.hasSelectedExpectedPosition);
    GameUtility.SetActive(this.seasonDeciding, !team.chairman.hasSelectedExpectedPosition);
    if (!this.seasonObjective.activeSelf)
      return;
    bool flag1 = team.championship.eventNumber == 0;
    bool flag2 = team.championship.standings.GetEntry((Entity) team).GetCurrentChampionshipPosition() <= team.chairman.playerChosenExpectedTeamChampionshipPosition;
    this.seasonTarget.text = GameUtility.FormatForPositionOrAbove(team.chairman.playerChosenExpectedTeamChampionshipPosition, (string) null);
    this.seasonStatus.text = !flag1 ? (!flag2 ? Localisation.LocaliseID("PSG_10009313", (GameObject) null) : Localisation.LocaliseID("PSG_10009312", (GameObject) null)) : Localisation.LocaliseID("PSG_10010360", (GameObject) null);
    this.seasonStatus.color = !flag1 ? (!flag2 ? UIConstants.negativeColor : UIConstants.positiveColor) : UIConstants.whiteColor;
  }

  private void OnMouseEnter()
  {
    Chairman chairman = Game.instance.player.team.chairman;
    if (chairman.GetPersonalityTraitHappinessModifiers().Count <= 0 && chairman.happinessModificationHistory.historyEntryCount <= 0)
      return;
    UIManager.instance.dialogBoxManager.GetDialog<DriverStatsModifiersRollover>().ShowRollover(chairman);
  }

  private void OnMouseExit()
  {
    UIManager.instance.dialogBoxManager.GetDialog<DriverStatsModifiersRollover>().Hide();
  }
}
