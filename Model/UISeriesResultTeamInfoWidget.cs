// Decompiled with JetBrains decompiler
// Type: UISeriesResultTeamInfoWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISeriesResultTeamInfoWidget : MonoBehaviour
{
  public UITeamLogo[] teamLogo = new UITeamLogo[0];
  public TextMeshProUGUI[] teamName = new TextMeshProUGUI[0];
  public Image backing;
  public UIChampionshipLogo championshipLogo;
  public TextMeshProUGUI yearLabel;
  public GameObject driverContainer;
  public UICharacterPortrait[] driverPortrait;
  public Flag[] driverFlag;
  public TextMeshProUGUI[] driverNameLabel;
  public TextMeshProUGUI[] driverPointsLabel;
  public TextMeshProUGUI[] driverPositionLabel;
  public UIAbilityStars[] abilityStars;
  public UICharacterPortrait teamPrincipalPortrait;
  public Flag teamPrincipalFlag;
  public TextMeshProUGUI teamPrincipalName;
  public TextMeshProUGUI pointsTotal;
  public TextMeshProUGUI prizeMoney;

  public void Setup(ChampionshipWinnersEntry inEntry, Championship inChampionship)
  {
    ChampionshipWinnersEntry championshipWinnersEntry = inEntry;
    Team teamChampion = championshipWinnersEntry.teamChampion;
    GameUtility.SetActive(this.driverContainer, true);
    this.championshipLogo.SetChampionship(inChampionship);
    this.backing.color = teamChampion.GetTeamColor().primaryUIColour.normal;
    StringVariableParser.intValue1 = championshipWinnersEntry.year;
    this.yearLabel.text = Localisation.LocaliseID("PSG_10011110", (GameObject) null);
    for (int index = 0; index < inEntry.teamsDrivers.Length; ++index)
    {
      Person teamsDriver = inEntry.teamsDrivers[index];
      this.abilityStars[index].SetAbilityStarsData(teamsDriver);
      this.driverPortrait[index].SetPortrait(teamsDriver);
      this.driverFlag[index].SetNationality(teamsDriver.nationality);
      this.driverNameLabel[index].text = teamsDriver.name;
      this.driverPositionLabel[index].text = GameUtility.FormatForPosition(championshipWinnersEntry.teamsDriversPosition[index], (string) null);
      this.driverPointsLabel[index].text = GameUtility.FormatChampionshipPoints(championshipWinnersEntry.teamsDriversPoints[index]);
    }
    for (int index = 0; index < this.teamLogo.Length; ++index)
    {
      this.teamLogo[index].SetTeam(teamChampion);
      this.teamName[index].text = teamChampion.name;
    }
    Person teamsTeamPrincipal = championshipWinnersEntry.teamsTeamPrincipal;
    this.teamPrincipalPortrait.SetPortrait(teamsTeamPrincipal);
    this.teamPrincipalFlag.SetNationality(teamsTeamPrincipal.nationality);
    this.teamPrincipalName.text = teamsTeamPrincipal.name;
    this.pointsTotal.text = GameUtility.FormatChampionshipPoints(championshipWinnersEntry.teamPoints);
    this.prizeMoney.text = GameUtility.GetCurrencyString(GameUtility.RoundCurrency(championshipWinnersEntry.teamPrizeMoney), 0);
  }

  public void Setup(Trophy inTrophy)
  {
    GameUtility.SetActive(this.driverContainer, false);
    Team team = inTrophy.team;
    this.championshipLogo.SetChampionship(inTrophy.championship);
    this.backing.color = team.GetTeamColor().primaryUIColour.normal;
    StringVariableParser.intValue1 = inTrophy.yearWon;
    this.yearLabel.text = Localisation.LocaliseID("PSG_10011110", (GameObject) null);
    for (int index = 0; index < this.teamLogo.Length; ++index)
    {
      this.teamLogo[index].SetTeam(team);
      this.teamName[index].text = team.name;
    }
    Person player = (Person) Game.instance.player;
    this.teamPrincipalPortrait.SetPortrait(player);
    this.teamPrincipalFlag.SetNationality(player.nationality);
    this.teamPrincipalName.text = player.name;
    this.pointsTotal.text = GameUtility.FormatChampionshipPoints(inTrophy.pointsTotal);
    this.prizeMoney.text = GameUtility.GetCurrencyString(GameUtility.RoundCurrency(inTrophy.prizeMoney), 0);
  }
}
