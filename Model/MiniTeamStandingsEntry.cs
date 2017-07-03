// Decompiled with JetBrains decompiler
// Type: MiniTeamStandingsEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MiniTeamStandingsEntry : MonoBehaviour
{
  private MiniTeamStandingsEntry.BarType mBarType = MiniTeamStandingsEntry.BarType.Darker;
  public Button button;
  public Image colorStripe;
  public Flag flag;
  public TextMeshProUGUI positionLabel;
  public TextMeshProUGUI nameLabel;
  public TextMeshProUGUI pointsLabel;
  public Image[] backing;
  public UICar uiCar;
  public Image sessionObjectiveLine;
  private Team mTeam;

  public Team team
  {
    get
    {
      return this.mTeam;
    }
  }

  private void Awake()
  {
    this.button.onClick.AddListener(new UnityAction(this.OnButtonPressed));
  }

  public void SetChampionshipEntry(ChampionshipEntry_v1 inChampionshipEntry)
  {
    int championshipPosition = inChampionshipEntry.GetCurrentChampionshipPosition();
    this.mTeam = inChampionshipEntry.GetEntity<Team>();
    this.ShowSessionObjective(championshipPosition == 1, this.mTeam == Game.instance.player.team);
    this.uiCar.SetTeamColor(this.mTeam.GetTeamColor().carColor);
    this.colorStripe.color = this.mTeam.GetTeamColor().primaryUIColour.normal;
    this.flag.SetNationality(this.mTeam.nationality);
    this.positionLabel.text = championshipPosition.ToString();
    this.nameLabel.text = this.mTeam.name;
    this.pointsLabel.text = inChampionshipEntry.GetCurrentPoints().ToString();
    if (this.mTeam == Game.instance.player.team)
      this.mBarType = MiniTeamStandingsEntry.BarType.PlayerOwned;
    this.UpdateBarBacking();
  }

  public void SetBarType(MiniTeamStandingsEntry.BarType inBarType)
  {
    this.mBarType = inBarType;
  }

  private void UpdateBarBacking()
  {
    for (int index = 0; index < this.backing.Length; ++index)
    {
      if ((MiniTeamStandingsEntry.BarType) index == this.mBarType)
        this.backing[index].gameObject.SetActive(true);
      else
        this.backing[index].gameObject.SetActive(false);
    }
  }

  public void OnButtonPressed()
  {
    UIManager.instance.ChangeScreen("TeamScreen", (Entity) this.mTeam, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }

  public void ShowSessionObjective(bool inShow, bool inIsObjectiveBeingMet)
  {
    this.sessionObjectiveLine.transform.parent.gameObject.SetActive(inShow);
    if (inIsObjectiveBeingMet)
      this.sessionObjectiveLine.color = UIConstants.positiveColor;
    else
      this.sessionObjectiveLine.color = UIConstants.negativeColor;
  }

  public enum BarType
  {
    Lighter,
    Darker,
    PlayerOwned,
  }
}
