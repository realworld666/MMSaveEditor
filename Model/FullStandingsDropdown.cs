// Decompiled with JetBrains decompiler
// Type: FullStandingsDropdown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class FullStandingsDropdown : UIBaseSessionHudDropdown
{
  public float refreshRate = 1f;
  public bool requireMouseOver = true;
  public bool isTopBar = true;
  public static bool isActivated;
  public GameObject headingPracticeQualifying;
  public GameObject headingRace;
  public GameObject batteryInfoHeader;
  public GameObject standings;
  private float mTimeUntilNextRefresh;
  private StandingsEntry[] mStandingsEntry;
  private SessionDetails.SessionType mSessionType;

  protected override void OnEnable()
  {
    base.OnEnable();
    if (Game.IsActive() && (this.isTopBar || !this.isTopBar && UIManager.instance.currentScreen is SkipSessionScreen))
    {
      if (this.mStandingsEntry == null)
      {
        this.mStandingsEntry = this.standings.GetComponentsInChildren<StandingsEntry>(true);
        for (int index = 0; index < this.mStandingsEntry.Length; ++index)
          this.mStandingsEntry[index].ShowObjective(true);
      }
      this.mSessionType = Game.instance.sessionManager.eventDetails.currentSession.sessionType;
      switch (this.mSessionType)
      {
        case SessionDetails.SessionType.Practice:
        case SessionDetails.SessionType.Qualifying:
          this.headingPracticeQualifying.SetActive(true);
          this.headingRace.SetActive(false);
          break;
        case SessionDetails.SessionType.Race:
          this.headingPracticeQualifying.SetActive(false);
          this.headingRace.SetActive(true);
          GameUtility.SetActiveAndCheckNull(this.batteryInfoHeader, Game.IsActive() && Game.instance.sessionManager.championship.rules.isEnergySystemActive);
          break;
      }
      this.RefreshStandings();
    }
    FullStandingsDropdown.isActivated = true;
    if (this.isTopBar)
      return;
    App.instance.widgetManager.UnregisterWidget(this.gameObject);
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    FullStandingsDropdown.isActivated = false;
  }

  protected override void Update()
  {
    this.mTimeUntilNextRefresh -= GameTimer.deltaTime;
    if ((double) this.mTimeUntilNextRefresh < 0.0)
      this.RefreshStandings();
    if (!this.requireMouseOver)
      return;
    base.Update();
  }

  private void RefreshStandings()
  {
    if (!Game.IsActive())
      return;
    List<RacingVehicle> standings = Game.instance.sessionManager.standings;
    RaceEventDetails eventDetails = Game.instance.sessionManager.eventDetails;
    if (standings.Count <= 0)
      return;
    for (int inEntryIndex = 0; inEntryIndex < this.mStandingsEntry.Length; ++inEntryIndex)
    {
      bool inIsActive = inEntryIndex < standings.Count;
      if (eventDetails.currentSession.sessionType == SessionDetails.SessionType.Qualifying && eventDetails.hasSeveralQualifyingSessions)
        inIsActive = inIsActive && (!eventDetails.results.IsDriverOutOfQualifying(standings[inEntryIndex].driver) || eventDetails.results.GetDriverQualifyingData(standings[inEntryIndex].driver).qualifyingSession == eventDetails.currentSession.sessionNumberForUI);
      GameUtility.SetActive(this.mStandingsEntry[inEntryIndex].gameObject, inIsActive);
      this.mStandingsEntry[inEntryIndex].SetVehicle(inEntryIndex, standings[inEntryIndex], standings[inEntryIndex].driver.IsPlayersDriver(), StandingsEntry.Mode.Detailed, this.mSessionType);
    }
    this.mTimeUntilNextRefresh = this.refreshRate;
  }
}
