// Decompiled with JetBrains decompiler
// Type: MiniDriverStandingsEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MiniDriverStandingsEntry : MonoBehaviour
{
  private MiniDriverStandingsEntry.BarType mBarType = MiniDriverStandingsEntry.BarType.Darker;
  public Button button;
  public Image colorStripe;
  public Flag flag;
  public TextMeshProUGUI teamNameLabel;
  public TextMeshProUGUI positionLabel;
  public TextMeshProUGUI nameLabel;
  public TextMeshProUGUI pointsLabel;
  public Image[] backing;
  public UICar uiCar;
  public Image sessionObjectiveLine;
  private Driver mDriver;

  public Driver driver
  {
    get
    {
      return this.mDriver;
    }
  }

  private void Awake()
  {
    this.button.onClick.AddListener(new UnityAction(this.OnButtonPressed));
  }

  public void SetChampionshipEntry(ChampionshipEntry_v1 inChampionshipEntry)
  {
    this.mDriver = inChampionshipEntry.GetEntity<Driver>();
    int championshipPosition = inChampionshipEntry.GetCurrentChampionshipPosition();
    this.uiCar.SetTeamColor(this.mDriver.contract.GetTeam().GetTeamColor().carColor);
    this.positionLabel.text = championshipPosition.ToString();
    this.nameLabel.text = this.mDriver.shortName;
    StringVariableParser.subject = (Person) this.mDriver;
    this.pointsLabel.text = inChampionshipEntry.GetCurrentPoints().ToString();
    this.teamNameLabel.text = !this.mDriver.IsFreeAgent() ? this.mDriver.contract.GetTeam().name : Localisation.LocaliseID("PSG_10011118", (GameObject) null);
    this.colorStripe.color = this.mDriver.GetTeamColor().primaryUIColour.normal;
    StringVariableParser.subject = (Person) null;
    this.flag.SetNationality(this.mDriver.nationality);
    if (this.mDriver.contract.GetTeam() == Game.instance.player.team)
      this.mBarType = MiniDriverStandingsEntry.BarType.PlayerOwned;
    this.UpdateBarBacking();
  }

  public void SetBarType(MiniDriverStandingsEntry.BarType inBarType)
  {
    this.mBarType = inBarType;
  }

  private void UpdateBarBacking()
  {
    for (int index = 0; index < this.backing.Length; ++index)
    {
      if ((MiniDriverStandingsEntry.BarType) index == this.mBarType)
        this.backing[index].gameObject.SetActive(true);
      else
        this.backing[index].gameObject.SetActive(false);
    }
  }

  public void OnButtonPressed()
  {
    UIManager.instance.ChangeScreen("DriverScreen", (Entity) this.mDriver, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
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
