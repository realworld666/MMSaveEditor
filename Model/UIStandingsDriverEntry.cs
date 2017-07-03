// Decompiled with JetBrains decompiler
// Type: UIStandingsDriverEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIStandingsDriverEntry : MonoBehaviour
{
  public UIStandingsDriverEntry.BarType barType;
  public GameObject[] bars;
  public Button button;
  public Flag driverFlag;
  public Image teamStrip;
  public GameObject changeUp;
  public GameObject changeDown;
  public TextMeshProUGUI positionNumber;
  public TextMeshProUGUI driverName;
  public TextMeshProUGUI teamName;
  public TextMeshProUGUI wins;
  public TextMeshProUGUI points;
  public TextMeshProUGUI diff;
  public UIStandingsDriversTableWidget widget;
  private Driver mDriver;
  private Team mTeam;
  private ChampionshipEntry_v1 mChampionshipEntry;
  private ChampionshipStandingsHistoryEntry mHistoryEntry;
  private bool mCurrentStandings;

  public void OnStart()
  {
    this.button.onClick.AddListener(new UnityAction(this.OnButton));
  }

  public void Setup(UIStandingsDriversTableWidget inWidget, ChampionshipEntry_v1 inEntry, bool inCurrentStandings, ChampionshipStandingsHistoryEntry inHistoryEntry)
  {
    if (inEntry == null)
      return;
    this.widget = inWidget;
    this.mChampionshipEntry = inEntry;
    this.mDriver = this.mChampionshipEntry.GetEntity<Driver>();
    this.mTeam = this.mDriver.contract.GetTeam();
    this.mCurrentStandings = inCurrentStandings;
    this.mHistoryEntry = inHistoryEntry;
    this.SetDetails();
  }

  private void SetDetails()
  {
    this.driverFlag.SetNationality(this.mDriver.nationality);
    this.teamStrip.color = this.mTeam.GetTeamColor().primaryUIColour.normal;
    int championshipPosition = this.mChampionshipEntry.GetCurrentChampionshipPosition();
    this.positionNumber.text = championshipPosition.ToString();
    this.driverName.text = this.mDriver.shortName;
    StringVariableParser.subject = (Person) this.mDriver;
    if (this.mCurrentStandings)
    {
      this.teamName.text = !this.mDriver.IsFreeAgent() ? this.mTeam.name : Localisation.LocaliseID("PSG_10011118", (GameObject) null);
    }
    else
    {
      CareerHistoryEntry careerYear = this.mDriver.careerHistory.GetCareerYear(this.mHistoryEntry.year);
      this.teamName.text = careerYear == null || careerYear.team == null || careerYear.team is NullTeam ? Localisation.LocaliseID("PSG_10011118", (GameObject) null) : careerYear.team.name;
    }
    StringVariableParser.subject = (Person) null;
    this.wins.text = this.mChampionshipEntry.wins.ToString();
    int num1 = !this.mCurrentStandings ? this.mChampionshipEntry.GetPointsForEvent(this.mChampionshipEntry.pointsEntryCount - 1) : this.mChampionshipEntry.GetCurrentPoints();
    this.points.text = num1.ToString();
    int num2 = num1 - (!this.mCurrentStandings ? this.widget.firstPlace.GetPointsForEvent(this.widget.firstPlace.pointsEntryCount - 1) : this.widget.firstPlace.GetCurrentPoints());
    this.diff.text = num2 == 0 ? "-" : num2.ToString();
    int eventNumber = this.mChampionshipEntry.championship.eventNumber;
    int num3 = championshipPosition;
    if (eventNumber > 1 && eventNumber - 2 >= 0)
      num3 = this.mChampionshipEntry.GetChampionshipPositionForEvent(eventNumber - 2);
    int num4 = championshipPosition - num3;
    GameUtility.SetActive(this.changeUp, this.mCurrentStandings && num4 < 0);
    GameUtility.SetActive(this.changeDown, this.mCurrentStandings && num4 > 0);
    if (this.mDriver.IsPlayersDriver())
      this.barType = UIStandingsDriverEntry.BarType.PlayerOwned;
    this.SetBarType();
  }

  public void SetBarType()
  {
    for (int index = 0; index < this.bars.Length; ++index)
      GameUtility.SetActive(this.bars[index], (UIStandingsDriverEntry.BarType) index == this.barType);
  }

  private void OnButton()
  {
    if (this.mDriver == null || !this.mCurrentStandings)
      return;
    UIManager.instance.ChangeScreen("DriverScreen", (Entity) this.mDriver, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }

  public enum BarType
  {
    Lighter,
    Darker,
    PlayerOwned,
  }
}
