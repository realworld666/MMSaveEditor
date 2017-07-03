// Decompiled with JetBrains decompiler
// Type: SimpleStandings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class SimpleStandings : MonoBehaviour
{
  public float refreshRate = 1f;
  public SimpleStandingsEntry[] standingsEntry;
  public CanvasGroup canvasGroup;
  public GameObject standings;
  public GameObject backingRace;
  public GameObject backingDefault;
  public GameObject raceHeader;
  public GameObject defaultHeader;
  private float mTimeUntilNextRefresh;
  private SessionDetails.SessionType mSessionType;

  private void OnEnable()
  {
    if (!Game.IsActive() || !App.instance.gameStateManager.currentState.IsSimulation())
      return;
    for (int index = 0; index < this.standingsEntry.Length; ++index)
      this.standingsEntry[index].ShowObjective(true);
    this.mSessionType = Game.instance.sessionManager.eventDetails.currentSession.sessionType;
    GameUtility.SetActive(this.backingRace, this.mSessionType == SessionDetails.SessionType.Race);
    GameUtility.SetActive(this.raceHeader, this.mSessionType == SessionDetails.SessionType.Race);
    GameUtility.SetActive(this.backingDefault, this.mSessionType != SessionDetails.SessionType.Race);
    GameUtility.SetActive(this.defaultHeader, this.mSessionType != SessionDetails.SessionType.Race);
    this.RefreshStandings();
  }

  private void Update()
  {
    this.canvasGroup.alpha = !FullStandingsDropdown.isActivated ? 1f : 0.0f;
    this.mTimeUntilNextRefresh -= GameTimer.deltaTime;
    if ((double) this.mTimeUntilNextRefresh >= 0.0)
      return;
    this.RefreshStandings();
  }

  private void RefreshStandings()
  {
    if (!Game.IsActive())
      return;
    List<RacingVehicle> standings = Game.instance.sessionManager.standings;
    RaceEventDetails eventDetails = Game.instance.sessionManager.eventDetails;
    if (standings.Count <= 0)
      return;
    for (int inEntryIndex = 0; inEntryIndex < this.standingsEntry.Length; ++inEntryIndex)
    {
      bool inIsActive = inEntryIndex < standings.Count;
      if (eventDetails.currentSession.sessionType == SessionDetails.SessionType.Qualifying && eventDetails.hasSeveralQualifyingSessions)
        inIsActive = inIsActive && (!eventDetails.results.IsDriverOutOfQualifying(standings[inEntryIndex].driver) || eventDetails.results.GetDriverQualifyingData(standings[inEntryIndex].driver).qualifyingSession == eventDetails.currentSession.sessionNumberForUI);
      GameUtility.SetActive(this.standingsEntry[inEntryIndex].gameObject, inIsActive);
      this.standingsEntry[inEntryIndex].SetVehicle(inEntryIndex, standings[inEntryIndex], standings[inEntryIndex].driver.IsPlayersDriver(), this.mSessionType);
    }
    this.mTimeUntilNextRefresh = this.refreshRate;
  }

  public class LapTimesData
  {
    public float previousLapTime;
    public float animationNormalizedProgress;
  }
}
