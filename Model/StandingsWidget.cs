// Decompiled with JetBrains decompiler
// Type: StandingsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class StandingsWidget : MonoBehaviour
{
  public float refreshRate = 1f;
  public GameObject headingPracticeQualifying;
  public GameObject headingRace;
  public StandingsEntry[] standingsEntry;
  private RacingVehicle mVehicle;
  private float mTimeUntilNextRefresh;

  private void Awake()
  {
  }

  public void SetVehicle(RacingVehicle inVehicle)
  {
    this.mVehicle = inVehicle;
    switch (Game.instance.sessionManager.eventDetails.currentSession.sessionType)
    {
      case SessionDetails.SessionType.Practice:
      case SessionDetails.SessionType.Qualifying:
        this.headingPracticeQualifying.SetActive(true);
        this.headingRace.SetActive(false);
        break;
      case SessionDetails.SessionType.Race:
        this.headingPracticeQualifying.SetActive(false);
        this.headingRace.SetActive(true);
        break;
    }
    this.RefreshStandings();
  }

  private void Update()
  {
    this.mTimeUntilNextRefresh -= GameTimer.deltaTime;
    if ((double) this.mTimeUntilNextRefresh >= 0.0)
      return;
    this.RefreshStandings();
  }

  private void RefreshStandings()
  {
    if (this.mVehicle == null)
      return;
    SessionDetails.SessionType sessionType = Game.instance.sessionManager.eventDetails.currentSession.sessionType;
    List<RacingVehicle> standings = Game.instance.sessionManager.standings;
    if (standings != null && standings.Count > 0)
    {
      if (this.mVehicle.standingsPosition == 1 || this.mVehicle.standingsPosition == 2)
      {
        for (int inEntryIndex = 0; inEntryIndex < this.standingsEntry.Length; ++inEntryIndex)
          this.standingsEntry[inEntryIndex].SetVehicle(inEntryIndex, standings[inEntryIndex], standings[inEntryIndex] == this.mVehicle, StandingsEntry.Mode.Detailed, sessionType);
      }
      else if (this.mVehicle.standingsPosition == Game.instance.sessionManager.standings.Count || this.mVehicle.standingsPosition == RaceEventResults.GetPositionThreshold(Game.instance.sessionManager.eventDetails.currentSession.sessionNumber - 1))
      {
        int index = this.mVehicle.standingsPosition - 3;
        for (int inEntryIndex = 0; inEntryIndex < this.standingsEntry.Length; ++inEntryIndex)
        {
          this.standingsEntry[inEntryIndex].SetVehicle(inEntryIndex, standings[index], standings[index] == this.mVehicle, StandingsEntry.Mode.Detailed, sessionType);
          ++index;
        }
      }
      else
      {
        this.standingsEntry[0].SetVehicle(0, standings[this.mVehicle.standingsPosition - 2], false, StandingsEntry.Mode.Detailed, sessionType);
        this.standingsEntry[1].SetVehicle(1, standings[this.mVehicle.standingsPosition - 1], true, StandingsEntry.Mode.Detailed, sessionType);
        this.standingsEntry[2].SetVehicle(2, standings[this.mVehicle.standingsPosition], false, StandingsEntry.Mode.Detailed, sessionType);
      }
    }
    this.mTimeUntilNextRefresh = this.refreshRate;
  }
}
