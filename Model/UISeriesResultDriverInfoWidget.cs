// Decompiled with JetBrains decompiler
// Type: UISeriesResultDriverInfoWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISeriesResultDriverInfoWidget : MonoBehaviour
{
  public UITeamLogo[] teamLogo = new UITeamLogo[0];
  public TextMeshProUGUI[] teamName = new TextMeshProUGUI[0];
  public Image backing;
  public UIChampionshipLogo championshipLogo;
  public TextMeshProUGUI yearLabel;
  public UICharacterPortrait driverPortrait;
  public Flag driverFlag;
  public TextMeshProUGUI driverNameLabel;
  public TextMeshProUGUI driverPointsLabel;
  public TextMeshProUGUI driverAgeLabel;
  public TextMeshProUGUI driverRaceWinsLabel;
  public TextMeshProUGUI driverPodiumsLabel;
  public TextMeshProUGUI driverDNFLabel;
  public UIAbilityStars abilityStars;
  public GameObject representativeContainer;
  public UICharacterPortrait teamPrincipalPortrait;
  public Flag teamPrincipalFlag;
  public TextMeshProUGUI teamPrincipalName;
  public UIDriverHelmet driverHelmet;
  public Toggle favouriteToggle;

  public void Setup(ChampionshipWinnersEntry inEntry, Championship inChampionship)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UISeriesResultDriverInfoWidget.\u003CSetup\u003Ec__AnonStoreyAD setupCAnonStoreyAd = new UISeriesResultDriverInfoWidget.\u003CSetup\u003Ec__AnonStoreyAD();
    ChampionshipWinnersEntry championshipWinnersEntry = inEntry;
    // ISSUE: reference to a compiler-generated field
    setupCAnonStoreyAd.driver = championshipWinnersEntry.driverChampion;
    Team driversTeam = championshipWinnersEntry.driversTeam;
    this.championshipLogo.SetChampionship(inChampionship);
    this.backing.color = driversTeam.GetTeamColor().primaryUIColour.normal;
    StringVariableParser.intValue1 = championshipWinnersEntry.year;
    this.yearLabel.text = Localisation.LocaliseID("PSG_10011110", (GameObject) null);
    // ISSUE: reference to a compiler-generated field
    this.abilityStars.SetAbilityStarsData(setupCAnonStoreyAd.driver);
    // ISSUE: reference to a compiler-generated field
    this.driverPortrait.SetPortrait((Person) setupCAnonStoreyAd.driver);
    // ISSUE: reference to a compiler-generated field
    this.driverFlag.SetNationality(setupCAnonStoreyAd.driver.nationality);
    // ISSUE: reference to a compiler-generated field
    this.driverNameLabel.text = setupCAnonStoreyAd.driver.name;
    // ISSUE: reference to a compiler-generated field
    StringVariableParser.intValue1 = setupCAnonStoreyAd.driver.GetAge();
    this.driverAgeLabel.text = Localisation.LocaliseID("PSG_10010748", (GameObject) null);
    this.driverRaceWinsLabel.text = championshipWinnersEntry.driverWins.ToString();
    this.driverPodiumsLabel.text = championshipWinnersEntry.driverPodiums.ToString();
    this.driverDNFLabel.text = championshipWinnersEntry.driverDNFs.ToString();
    this.driverPointsLabel.text = GameUtility.FormatChampionshipPoints(championshipWinnersEntry.driverPoints);
    // ISSUE: reference to a compiler-generated field
    this.driverHelmet.SetHelmet(setupCAnonStoreyAd.driver);
    for (int index = 0; index < this.teamLogo.Length; ++index)
    {
      this.teamLogo[index].SetTeam(driversTeam);
      this.teamName[index].text = driversTeam.name;
    }
    // ISSUE: reference to a compiler-generated field
    GameUtility.SetActive(this.representativeContainer, !setupCAnonStoreyAd.driver.IsFreeAgent());
    if (this.representativeContainer.activeSelf)
    {
      Person driversTeamPrincipal = championshipWinnersEntry.driversTeamPrincipal;
      this.teamPrincipalPortrait.SetPortrait(driversTeamPrincipal);
      this.teamPrincipalFlag.SetNationality(driversTeamPrincipal.nationality);
      this.teamPrincipalName.text = driversTeamPrincipal.name;
    }
    this.favouriteToggle.onValueChanged.RemoveAllListeners();
    // ISSUE: reference to a compiler-generated field
    this.favouriteToggle.isOn = setupCAnonStoreyAd.driver.isShortlisted;
    // ISSUE: reference to a compiler-generated method
    this.favouriteToggle.onValueChanged.AddListener(new UnityAction<bool>(setupCAnonStoreyAd.\u003C\u003Em__253));
  }
}
