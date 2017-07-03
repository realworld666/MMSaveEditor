// Decompiled with JetBrains decompiler
// Type: SessionHUBResultsScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SessionHUBResultsScreen : UIScreen
{
  public UIResultsWidget practiceResultsWidget;
  public UIResultsWidget qualifyingResultsWidget;
  public UIResultsWidget raceResultsWidget;
  public Toggle practiceResults;
  public Toggle qualifyingResults;
  public Toggle raceResults;
  public TextMeshProUGUI championshipName;
  public TextMeshProUGUI resultsName;
  private RaceEventResults mEventStandings;

  private void OnButtonPressed(UIResultsWidget inWidget, List<RaceEventResults.SessonResultData> inList, bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (inValue)
    {
      inWidget.Activate(inList.Count > 0);
      GameUtility.SetActive(inWidget.noData, inList.Count == 0);
      if (inWidget is UIPracticeResultsWidget)
        this.resultsName.text = Localisation.LocaliseID("PSG_10010593", (GameObject) null);
      else if (inWidget is UIQualifyingResultsWidget)
        this.resultsName.text = Localisation.LocaliseID("PSG_10010594", (GameObject) null);
      else
        this.resultsName.text = Localisation.LocaliseID("PSG_10007791", (GameObject) null);
    }
    else
      inWidget.Activate(false);
  }

  public override void OnEnter()
  {
    scSoundManager.BlockSoundEvents = true;
    base.OnEnter();
    this.SetupScreen();
    this.showNavigationBars = true;
    scSoundManager.BlockSoundEvents = false;
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
  }

  private void SetupScreen()
  {
    bool qualifyingBasedActive = Game.instance.player.team.championship.rules.qualifyingBasedActive;
    this.championshipName.text = Game.instance.time.now.Year.ToString() + " " + Localisation.LocaliseID(Game.instance.sessionManager.eventDetails.circuit.locationNameID, (GameObject) null) + " - " + Game.instance.sessionManager.championship.GetChampionshipName(false);
    this.mEventStandings = Game.instance.sessionManager.eventDetails.results;
    this.practiceResults.onValueChanged.RemoveAllListeners();
    this.practiceResults.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnButtonPressed(this.practiceResultsWidget, this.mEventStandings.GetAllResultsForSession(SessionDetails.SessionType.Practice), value)));
    this.qualifyingResults.onValueChanged.RemoveAllListeners();
    this.qualifyingResults.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnButtonPressed(this.qualifyingResultsWidget, this.mEventStandings.GetAllResultsForSession(SessionDetails.SessionType.Qualifying), value)));
    this.raceResults.onValueChanged.RemoveAllListeners();
    this.raceResults.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnButtonPressed(this.raceResultsWidget, this.mEventStandings.GetAllResultsForSession(SessionDetails.SessionType.Race), value)));
    this.practiceResultsWidget.PopulateList(this.mEventStandings.GetAllResultsForSession(SessionDetails.SessionType.Practice));
    this.practiceResultsWidget.Activate(false);
    this.practiceResults.isOn = false;
    GameUtility.SetActive(this.qualifyingResults.gameObject, qualifyingBasedActive);
    this.qualifyingResultsWidget.PopulateList(this.mEventStandings.GetAllResultsForSession(SessionDetails.SessionType.Qualifying));
    this.qualifyingResultsWidget.Activate(false);
    this.qualifyingResults.isOn = false;
    this.raceResultsWidget.PopulateList(this.mEventStandings.GetAllResultsForSession(SessionDetails.SessionType.Race));
    this.raceResultsWidget.Activate(false);
    this.raceResults.isOn = false;
    if (this.mEventStandings.GetAllResultsForSession(SessionDetails.SessionType.Race).Count > 0)
      this.raceResults.isOn = true;
    else if (qualifyingBasedActive && this.mEventStandings.GetAllResultsForSession(SessionDetails.SessionType.Qualifying).Count > 0)
      this.qualifyingResults.isOn = true;
    else
      this.practiceResults.isOn = true;
  }
}
