// Decompiled with JetBrains decompiler
// Type: UISessionHUBStandingsEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISessionHUBStandingsEntry : MonoBehaviour
{
  public UISessionHUBStandingsEntry.BarType barType = UISessionHUBStandingsEntry.BarType.Lighter;
  public TextMeshProUGUI racePosition;
  public TextMeshProUGUI teamName;
  public TextMeshProUGUI change;
  public TextMeshProUGUI points;
  public TextMeshProUGUI pointsDiff;
  public Button button;
  public Image teamStripe;
  public Image changeUp;
  public Image changeDown;
  public GameObject[] backing;
  public Vehicle vehicle;
  public int position;
  public int eventNumber;
  private int mDiffPosition;
  private int mDiffPoints;
  private Team mTeam;

  public virtual void OnStart()
  {
  }

  public virtual void Setup(SessionHUBStandingsScreen.HUBStanding inStanding)
  {
    if (inStanding != null)
    {
      if (this.vehicle != inStanding.vehicle)
        this.vehicle = (Vehicle) inStanding.vehicle;
      if (this.mTeam != inStanding.team)
      {
        this.mTeam = inStanding.team;
        StringVariableParser.subject = (Person) inStanding.driver;
        this.teamName.text = !(this.mTeam is NullTeam) ? this.mTeam.name : Localisation.LocaliseID("PSG_10011118", (GameObject) null);
        StringVariableParser.subject = (Person) null;
        this.teamStripe.color = this.mTeam.GetTeamColor().primaryUIColour.normal;
      }
      this.mDiffPosition = inStanding.championshipPosition - inStanding.predictedPosition;
      this.mDiffPoints = inStanding.predictedPoints - inStanding.championshipPoints;
      this.SetPoints(inStanding);
      this.SetPositionChange(inStanding);
      this.SetPointsChange(inStanding);
    }
    this.SetBarType();
  }

  private void SetBarType()
  {
    for (int index = 0; index < this.backing.Length; ++index)
      GameUtility.SetActive(this.backing[index], (UISessionHUBStandingsEntry.BarType) index == this.barType);
  }

  private void SetPoints(SessionHUBStandingsScreen.HUBStanding inStanding)
  {
    this.eventNumber = inStanding.eventNumber;
    if (this.eventNumber <= 0)
    {
      if (inStanding.sessionType == SessionDetails.SessionType.Practice)
      {
        this.points.text = "-";
        this.points.color = Color.white;
      }
      else if (inStanding.sessionType == SessionDetails.SessionType.Qualifying)
      {
        this.points.text = this.mDiffPoints <= 0 ? string.Empty : this.mDiffPoints.ToString();
        this.points.color = Color.white;
      }
      else
      {
        this.points.text = inStanding.predictedPoints.ToString();
        this.points.color = Color.white;
      }
    }
    else
    {
      this.points.text = inStanding.predictedPoints.ToString();
      this.points.color = Color.white;
    }
  }

  private void SetPointsChange(SessionHUBStandingsScreen.HUBStanding inStanding)
  {
    GameUtility.SetActive(this.pointsDiff.gameObject, this.mDiffPoints > 0 && (inStanding.sessionType == SessionDetails.SessionType.Qualifying || inStanding.sessionType == SessionDetails.SessionType.Race && !inStanding.isSessionEnd));
    if (!this.pointsDiff.gameObject.activeSelf)
      return;
    this.pointsDiff.text = string.Format("{{+{0}}}", (object) this.mDiffPoints.ToString());
    this.pointsDiff.color = UIConstants.positiveColor;
  }

  private void SetPositionChange(SessionHUBStandingsScreen.HUBStanding inStanding)
  {
    GameUtility.SetActive(this.change.gameObject, this.mDiffPosition != 0 && this.eventNumber > 0 && inStanding.sessionType == SessionDetails.SessionType.Race);
    if (!this.change.gameObject.activeSelf)
      return;
    this.changeUp.gameObject.SetActive(this.mDiffPosition > 0);
    this.changeDown.gameObject.SetActive(this.mDiffPosition < 0);
    this.change.text = Mathf.Abs(this.mDiffPosition).ToString();
  }

  public enum BarType
  {
    PlayerOwned,
    Lighter,
    Darker,
  }
}
