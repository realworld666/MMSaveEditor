// Decompiled with JetBrains decompiler
// Type: UIStandingsTeamEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIStandingsTeamEntry : MonoBehaviour
{
  public UIStandingsTeamEntry.BarType barType;
  public GameObject[] bars;
  public Button button;
  public Flag teamFlag;
  public Image teamStrip;
  public GameObject changeUp;
  public GameObject changeDown;
  public TextMeshProUGUI positionNumber;
  public TextMeshProUGUI teamName;
  public TextMeshProUGUI wins;
  public TextMeshProUGUI points;
  public TextMeshProUGUI diff;
  public UIStandingsTeamsTableWidget widget;
  private Team mTeam;
  private ChampionshipEntry_v1 mChampionshipEntry;
  private bool mCurrentStandings;

  public void OnStart()
  {
    this.button.onClick.AddListener(new UnityAction(this.OnButton));
  }

  public void Setup(UIStandingsTeamsTableWidget inWidget, ChampionshipEntry_v1 inEntry, bool inCurrentStandings)
  {
    if (inEntry == null)
      return;
    this.widget = inWidget;
    this.mChampionshipEntry = inEntry;
    this.mTeam = this.mChampionshipEntry.GetEntity<Team>();
    this.mCurrentStandings = inCurrentStandings;
    this.SetDetails();
  }

  private void SetDetails()
  {
    this.teamFlag.SetNationality(this.mTeam.nationality);
    this.teamStrip.color = this.mTeam.GetTeamColor().primaryUIColour.normal;
    int championshipPosition = this.mChampionshipEntry.GetCurrentChampionshipPosition();
    this.positionNumber.text = championshipPosition.ToString();
    this.teamName.text = this.mTeam.name;
    this.wins.text = this.mChampionshipEntry.wins.ToString();
    int num1 = !this.mCurrentStandings ? this.mChampionshipEntry.GetPointsForEvent(this.mChampionshipEntry.pointsEntryCount - 1) : this.mChampionshipEntry.GetCurrentPoints();
    this.points.text = num1.ToString();
    int num2 = num1 - (!this.mCurrentStandings ? this.widget.firstPlace.GetPointsForEvent(this.widget.firstPlace.pointsEntryCount - 1) : this.widget.firstPlace.GetCurrentPoints());
    this.diff.text = num2 == 0 ? "-" : num2.ToString();
    int eventNumber = this.mChampionshipEntry.championship.eventNumber;
    int num3 = championshipPosition;
    if (eventNumber > 1 && eventNumber - 1 >= 0)
      num3 = this.mChampionshipEntry.GetChampionshipPositionForEvent(eventNumber - 1);
    int num4 = championshipPosition - num3;
    GameUtility.SetActive(this.changeUp, this.mCurrentStandings && num4 < 0);
    GameUtility.SetActive(this.changeDown, this.mCurrentStandings && num4 > 0);
    if (this.mTeam == Game.instance.player.team)
      this.barType = UIStandingsTeamEntry.BarType.PlayerOwned;
    this.SetBarType();
  }

  public void SetBarType()
  {
    for (int index = 0; index < this.bars.Length; ++index)
      GameUtility.SetActive(this.bars[index], (UIStandingsTeamEntry.BarType) index == this.barType);
  }

  private void OnButton()
  {
    if (this.mTeam == null || !this.mCurrentStandings)
      return;
    UIManager.instance.ChangeScreen("TeamScreen", (Entity) this.mTeam, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }

  public enum BarType
  {
    Lighter,
    Darker,
    PlayerOwned,
  }
}
