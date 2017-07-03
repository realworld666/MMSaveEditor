// Decompiled with JetBrains decompiler
// Type: UIEventCalendarResults
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIEventCalendarResults : MonoBehaviour
{
  public Toggle raceToggle;
  public Toggle qualifyingToggle;
  public GameObject toggleContainer;
  public GameObject noData;
  public TextMeshProUGUI noDataLabel;
  public GameObject raceTable;
  public GameObject qualifyingTable;
  public GameObject qualifyingSessionHeader;
  public UIGridList selectedGrid;
  public UIGridList raceGrid;
  public UIGridList qualifyingGrid;
  public EventCalendarScreen screen;
  private UIEventCalendarResults.Mode mMode;
  private RaceEventDetails mRaceEvent;

  public void OnStart()
  {
    this.raceToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnRaceToggle()));
    this.qualifyingToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnQualifyingToggle()));
  }

  public void Setup(RaceEventDetails inRaceEvent)
  {
    if (inRaceEvent == null)
      return;
    this.mRaceEvent = inRaceEvent;
    GameUtility.SetActive(this.toggleContainer, this.screen.championship.rules.qualifyingBasedActive);
    if (!this.toggleContainer.activeSelf)
      this.mMode = UIEventCalendarResults.Mode.Race;
    this.SetMode(this.mMode);
  }

  private void SetMode(UIEventCalendarResults.Mode inMode)
  {
    this.mMode = inMode;
    this.qualifyingToggle.onValueChanged.RemoveAllListeners();
    this.raceToggle.onValueChanged.RemoveAllListeners();
    this.qualifyingToggle.isOn = this.mMode == UIEventCalendarResults.Mode.Qualifying;
    this.raceToggle.isOn = this.mMode == UIEventCalendarResults.Mode.Race;
    this.qualifyingToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnQualifyingToggle()));
    this.raceToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnRaceToggle()));
    GameUtility.SetActive(this.qualifyingSessionHeader, this.qualifyingToggle.isOn && this.mRaceEvent.hasSeveralQualifyingSessions);
    this.SetGrid();
  }

  public void SetGrid()
  {
    List<RaceEventResults.ResultData> resultDataList = new List<RaceEventResults.ResultData>();
    this.selectedGrid = this.mMode != UIEventCalendarResults.Mode.Race ? this.qualifyingGrid : this.raceGrid;
    switch (this.mMode)
    {
      case UIEventCalendarResults.Mode.Race:
        if (this.mRaceEvent.results.GetAllResultsForSession(SessionDetails.SessionType.Race).Count > 0)
        {
          resultDataList = this.mRaceEvent.results.GetResultsForSession(SessionDetails.SessionType.Race).resultData;
          break;
        }
        break;
      case UIEventCalendarResults.Mode.Qualifying:
        if (this.mRaceEvent.results.GetAllResultsForSession(SessionDetails.SessionType.Qualifying).Count > 0)
        {
          resultDataList = this.mRaceEvent.results.GetResultsForSession(SessionDetails.SessionType.Qualifying).resultData;
          break;
        }
        break;
    }
    int count = resultDataList.Count;
    int itemCount1 = this.selectedGrid.itemCount;
    int num1 = count - itemCount1;
    GameUtility.SetActive(this.raceTable, this.mMode == UIEventCalendarResults.Mode.Race);
    GameUtility.SetActive(this.qualifyingTable, this.mMode == UIEventCalendarResults.Mode.Qualifying);
    GameUtility.SetActive(this.noData, count == 0);
    int num2 = Mathf.RoundToInt((float) (this.mRaceEvent.eventDate - Game.instance.time.now).TotalDays);
    int num3 = Mathf.RoundToInt((float) num2 / 7f);
    StringVariableParser.randomCircuit = this.mRaceEvent.circuit;
    if (num2 > 0)
    {
      if (num3 > 0)
      {
        StringVariableParser.intValue1 = num3;
        this.noDataLabel.text = Localisation.LocaliseID(num3 <= 1 ? "PSG_10010240" : "PSG_10010239", (GameObject) null);
      }
      else
      {
        StringVariableParser.intValue1 = num2;
        this.noDataLabel.text = Localisation.LocaliseID(num2 <= 1 ? "PSG_10010242" : "PSG_10010241", (GameObject) null);
      }
    }
    else
      this.noDataLabel.text = Localisation.LocaliseID("PSG_10010238", (GameObject) null);
    for (int index = 0; index < num1; ++index)
    {
      switch (this.mMode)
      {
        case UIEventCalendarResults.Mode.Race:
          this.selectedGrid.CreateListItem<UIEventCalendarRaceEntry>().OnStart();
          break;
        case UIEventCalendarResults.Mode.Qualifying:
          this.selectedGrid.CreateListItem<UIEventCalendarQualifyingEntry>().OnStart();
          break;
      }
    }
    int itemCount2 = this.selectedGrid.itemCount;
    for (int inIndex = 0; inIndex < itemCount2; ++inIndex)
    {
      switch (this.mMode)
      {
        case UIEventCalendarResults.Mode.Race:
          UIEventCalendarRaceEntry calendarRaceEntry = this.selectedGrid.GetItem<UIEventCalendarRaceEntry>(inIndex);
          GameUtility.SetActive(calendarRaceEntry.gameObject, inIndex < count);
          if (calendarRaceEntry.gameObject.activeSelf)
          {
            calendarRaceEntry.Setup(resultDataList[inIndex]);
            calendarRaceEntry.barType = inIndex % 2 != 0 ? UIEventCalendarRaceEntry.BarType.Darker : UIEventCalendarRaceEntry.BarType.Lighter;
            break;
          }
          break;
        case UIEventCalendarResults.Mode.Qualifying:
          UIEventCalendarQualifyingEntry calendarQualifyingEntry = this.selectedGrid.GetItem<UIEventCalendarQualifyingEntry>(inIndex);
          GameUtility.SetActive(calendarQualifyingEntry.gameObject, inIndex < count);
          if (calendarQualifyingEntry.gameObject.activeSelf)
          {
            if (this.mRaceEvent.hasSeveralQualifyingSessions)
              calendarQualifyingEntry.Setup(this.mRaceEvent.results.GetDriverQualifyingData(resultDataList[inIndex].driver));
            else
              calendarQualifyingEntry.Setup(resultDataList[inIndex]);
            calendarQualifyingEntry.barType = inIndex % 2 != 0 ? UIEventCalendarQualifyingEntry.BarType.Darker : UIEventCalendarQualifyingEntry.BarType.Lighter;
            break;
          }
          break;
      }
    }
  }

  private void OnRaceToggle()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!this.raceToggle.isOn)
      return;
    this.SetMode(UIEventCalendarResults.Mode.Race);
  }

  private void OnQualifyingToggle()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!this.qualifyingToggle.isOn)
      return;
    this.SetMode(UIEventCalendarResults.Mode.Qualifying);
  }

  public enum Mode
  {
    Race,
    Qualifying,
  }
}
