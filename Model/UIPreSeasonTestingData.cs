// Decompiled with JetBrains decompiler
// Type: UIPreSeasonTestingData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPreSeasonTestingData : MonoBehaviour
{
  private readonly float mUpdateRate = 1f;
  private float mCurrentTimer = 1f;
  public Image colorStripe;
  public Flag nationality;
  public TextMeshProUGUI driverNameLabel;
  public TextMeshProUGUI teamNameLabel;
  public TextMeshProUGUI lapsLabel;
  public TextMeshProUGUI fastestLapsLabel;
  public TextMeshProUGUI topSpeedLabel;
  public TextMeshProUGUI averageSpeedLabel;
  public TextMeshProUGUI timeOnTrackLabel;
  public TextMeshProUGUI notesLabel;
  public TextMeshProUGUI positionLabel;
  public GameObject playerBacking;
  private PreSeasonTestingData mPreSeasonTestingData;
  private int mCurrentPosition;

  public PreSeasonTestingData data
  {
    get
    {
      return this.mPreSeasonTestingData;
    }
  }

  public void SetData(PreSeasonTestingData inData)
  {
    this.mPreSeasonTestingData = inData;
    this.mCurrentPosition = this.mPreSeasonTestingData.resultData.position;
  }

  private void Update()
  {
    this.mCurrentTimer -= GameTimer.deltaTime;
    if ((double) this.mCurrentTimer <= 0.0)
    {
      this.SetTestingData();
      this.mCurrentTimer = this.mUpdateRate;
    }
    int position = this.mPreSeasonTestingData.resultData.position;
    if (this.mCurrentPosition == position)
      return;
    this.positionLabel.text = GameUtility.FormatForPosition(position, (string) null);
    this.mCurrentPosition = position;
  }

  private void SetTestingData()
  {
    Driver driver = this.mPreSeasonTestingData.driver;
    Team team = this.mPreSeasonTestingData.driver.contract.GetTeam();
    this.colorStripe.color = driver.GetTeamColor().primaryUIColour.normal;
    this.nationality.SetNationality(driver.nationality);
    this.driverNameLabel.text = driver.shortName;
    this.teamNameLabel.text = team.name;
    this.lapsLabel.text = this.mPreSeasonTestingData.laps.ToString();
    this.fastestLapsLabel.text = GameUtility.GetLapTimeText(this.mPreSeasonTestingData.fastestLap, false);
    this.topSpeedLabel.text = GameUtility.GetSpeedText(GameUtility.MilesPerHourToMetersPerSecond(this.mPreSeasonTestingData.topSpeed), 1f);
    this.averageSpeedLabel.text = GameUtility.GetSpeedText(GameUtility.MilesPerHourToMetersPerSecond(this.mPreSeasonTestingData.averageSpeed), 1f);
    this.timeOnTrackLabel.text = GameUtility.FormatTimeSpan(new TimeSpan(0, (int) GameUtility.SecondsToMinutes(this.mPreSeasonTestingData.timeOnTrack), 0));
    this.notesLabel.text = this.mPreSeasonTestingData.notes;
    this.positionLabel.text = GameUtility.FormatForPosition(this.mPreSeasonTestingData.resultData.position, (string) null);
    GameUtility.SetActive(this.playerBacking.gameObject, driver.IsPlayersDriver());
  }

  private void OnEnable()
  {
    if (this.mPreSeasonTestingData == null)
      return;
    this.SetTestingData();
  }
}
