// Decompiled with JetBrains decompiler
// Type: UIScoutingStaffEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIScoutingStaffEntry : MonoBehaviour
{
  public Flag flag;
  public Toggle shortlistButton;
  public TextMeshProUGUI staffName;
  public TextMeshProUGUI age;
  public TextMeshProUGUI teamName;
  public TextMeshProUGUI racingSeries;
  public TextMeshProUGUI monthlyWage;
  public TextMeshProUGUI breakClause;
  public TextMeshProUGUI contractEnds;
  public UIScoutDriverWidget scoutWidget;
  public UIAbilityStars abilityStars;
  public GameObject driverSeriesIconsContainer;
  public UIDriverSeriesIcons driverSeriesIcon;
  public Button EntryScreenButton;
  public ScoutingScreen screen;
  public Person person;

  public void SetupEntry(Person inStaff)
  {
    this.person = inStaff;
    this.flag.SetNationality(inStaff.nationality);
    this.shortlistButton.onValueChanged.RemoveAllListeners();
    this.shortlistButton.isOn = inStaff.isShortlisted;
    GameUtility.SetActive(this.shortlistButton.graphic.gameObject, this.shortlistButton.isOn);
    this.shortlistButton.onValueChanged.AddListener(new UnityAction<bool>(this.OnShortListToggle));
    this.staffName.text = inStaff.name;
    this.age.text = inStaff.GetAge().ToString();
    GameUtility.SetActive(this.driverSeriesIconsContainer.gameObject, this.person is Driver && Game.instance.championshipManager.isGTSeriesActive);
    if (this.person is Driver)
      this.driverSeriesIcon.Setup(this.person as Driver);
    if (!this.person.IsFreeAgent())
    {
      this.teamName.text = inStaff.contract.GetTeam().name;
      this.racingSeries.text = inStaff.contract.GetTeam().championship.GetChampionshipName(false);
      this.monthlyWage.text = GameUtility.GetCurrencyString(inStaff.contract.perRaceCost, 0);
      this.breakClause.text = GameUtility.GetCurrencyString((long) inStaff.contract.GetContractTerminationCost(), 0);
      this.contractEnds.text = inStaff.contract.endDate.Year.ToString();
    }
    else
    {
      this.teamName.text = "-";
      this.racingSeries.text = "-";
      this.monthlyWage.text = "-";
      this.breakClause.text = "-";
      this.contractEnds.text = "-";
    }
    this.UpdateAbilityScoutingState();
    this.EntryScreenButton.onClick.RemoveAllListeners();
    this.EntryScreenButton.onClick.AddListener(new UnityAction(this.OnButton));
  }

  public void UpdateEntry()
  {
    if (this.person.isShortlisted != this.shortlistButton.isOn)
    {
      this.shortlistButton.onValueChanged.RemoveAllListeners();
      this.shortlistButton.isOn = this.person.isShortlisted;
      GameUtility.SetActive(this.shortlistButton.graphic.gameObject, this.shortlistButton.isOn);
      this.shortlistButton.onValueChanged.AddListener(new UnityAction<bool>(this.OnShortListToggle));
    }
    this.UpdateAbilityScoutingState();
  }

  public void UpdateAbilityScoutingState()
  {
    if (this.person is Driver)
    {
      Driver person = (Driver) this.person;
      if (person.CanShowStats())
      {
        GameUtility.SetActive(this.scoutWidget.gameObject, false);
        GameUtility.SetActive(this.abilityStars.gameObject, true);
        this.abilityStars.SetAbilityStarsData(person);
      }
      else
      {
        GameUtility.SetActive(this.scoutWidget.gameObject, true);
        GameUtility.SetActive(this.abilityStars.gameObject, false);
        this.scoutWidget.SetupScoutDriverWidget(person);
      }
    }
    else
    {
      GameUtility.SetActive(this.scoutWidget.gameObject, false);
      GameUtility.SetActive(this.abilityStars.gameObject, true);
      this.abilityStars.SetAbilityStarsData(this.person);
    }
  }

  private void OnButton()
  {
    if (this.person == null)
      return;
    if (this.person is Driver)
      UIManager.instance.ChangeScreen("DriverScreen", (Entity) (this.person as Driver), UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
    else if (this.person is Mechanic)
    {
      UIManager.instance.ChangeScreen("StaffDetailsScreen", (Entity) (this.person as Mechanic), UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
    }
    else
    {
      if (!(this.person is Engineer))
        return;
      UIManager.instance.ChangeScreen("StaffDetailsScreen", (Entity) (this.person as Engineer), UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
    }
  }

  private void OnShortListToggle(bool inValue)
  {
    this.person.ToggleShortlisted(inValue);
    this.screen.searchWidget.UpdateFavouriteNotification(inValue);
    GameUtility.SetActive(this.shortlistButton.graphic.gameObject, this.shortlistButton.isOn);
  }
}
