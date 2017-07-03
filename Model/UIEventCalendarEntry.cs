// Decompiled with JetBrains decompiler
// Type: UIEventCalendarEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIEventCalendarEntry : MonoBehaviour
{
  public Toggle toggle;
  public Flag raceFlag;
  public RoundInfoPip infoPip;
  public TextMeshProUGUI raceNumber;
  public TextMeshProUGUI raceDate;
  public TextMeshProUGUI raceLocation;
  public TextMeshProUGUI[] standings;
  public EventCalendarScreen screen;
  private RaceEventDetails mRaceEvent;
  private int mEventNumber;
  private bool mFirstToggleSoundBlocked;

  public void OnStart()
  {
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle()));
  }

  public void Setup(RaceEventDetails inRaceEvent, int inEventNumber)
  {
    if (inRaceEvent == null)
      return;
    this.mRaceEvent = inRaceEvent;
    this.mEventNumber = inEventNumber;
    this.SetDetails();
    this.SetDrivers();
  }

  private void SetDetails()
  {
    scSoundManager.BlockSoundEvents = true;
    this.raceNumber.text = "Race " + (this.mEventNumber + 1).ToString();
    this.raceDate.text = GameUtility.FormatDateTimeToDateNoYear(this.mRaceEvent.eventDate, string.Empty);
    this.raceLocation.text = Localisation.LocaliseID(this.mRaceEvent.circuit.locationNameID, (GameObject) null);
    this.raceFlag.SetNationality(this.mRaceEvent.circuit.nationality);
    this.infoPip.SetRaceEventDetails(this.screen.championship, this.mRaceEvent);
    scSoundManager.BlockSoundEvents = false;
  }

  private void SetDrivers()
  {
    List<RaceEventResults.ResultData> resultDataList = new List<RaceEventResults.ResultData>();
    if (this.mRaceEvent.results.GetAllResultsForSession(SessionDetails.SessionType.Race).Count > 0)
      resultDataList = this.mRaceEvent.results.GetResultsForSession(SessionDetails.SessionType.Race).resultData;
    int count = resultDataList.Count;
    for (int index = 0; index < this.standings.Length; ++index)
    {
      string str = (index + 1).ToString() + ". ";
      if (index < count)
      {
        RaceEventResults.ResultData resultData = resultDataList[index];
        this.standings[index].text = str + resultData.driver.lastName;
      }
      else
        this.standings[index].text = str + "-";
    }
  }

  public void OnToggle()
  {
    if (!this.mFirstToggleSoundBlocked)
      this.mFirstToggleSoundBlocked = true;
    else
      scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!this.toggle.isOn)
      return;
    this.screen.SelectCircuit(this.mRaceEvent, this.mEventNumber);
  }

  public void OnEnable()
  {
    this.mFirstToggleSoundBlocked = false;
  }
}
