// Decompiled with JetBrains decompiler
// Type: QuickRaceSetupState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

public class QuickRaceSetupState : GameState
{
  private string mCircuit = string.Empty;
  private QuickRaceSetupState.RaceLength mRaceLength = QuickRaceSetupState.RaceLength.Medium;
  private QuickRaceSetupState.RaceWeekend mRaceWeekend;
  private QuickRaceSetupState.GridOptions mGridOptions;
  private int mDriver1GridPosition;
  private int mDriver2GridPosition;
  private SessionDetails.WeatherSettings mWeatherSettings;
  private Circuit.TrackLayout mTrackLayout;

  public override GameState.Type type
  {
    get
    {
      return GameState.Type.QuickRaceSetup;
    }
  }

  public override UIManager.ScreenSet screenSet
  {
    get
    {
      return UIManager.ScreenSet.Frontend;
    }
  }

  public override GameState.Group group
  {
    get
    {
      return GameState.Group.Frontend;
    }
  }

  public QuickRaceSetupState.RaceLength raceLength
  {
    get
    {
      return this.mRaceLength;
    }
  }

  public QuickRaceSetupState.RaceWeekend raceWeekend
  {
    get
    {
      return this.mRaceWeekend;
    }
  }

  public QuickRaceSetupState.GridOptions gridOptions
  {
    get
    {
      return this.mGridOptions;
    }
  }

  public int driver1GridPosition
  {
    get
    {
      return this.mDriver1GridPosition;
    }
  }

  public int driver2GridPosition
  {
    get
    {
      return this.mDriver2GridPosition;
    }
  }

  public SessionDetails.WeatherSettings weatherSettings
  {
    get
    {
      return this.mWeatherSettings;
    }
  }

  public Circuit.TrackLayout trackLayout
  {
    get
    {
      return this.mTrackLayout;
    }
  }

  public override void OnEnter(bool fromSave = false)
  {
    base.OnEnter(fromSave);
    GameUtility.Assert(Game.instance == null, "Game.instance == null", (UnityEngine.Object) null);
    Game.instance = new Game();
    Game.instance.gameType = Game.GameType.SingleEvent;
    Game.instance.SetupNewGame();
    Game.instance.StartNewGame();
    Game.instance.tutorialSystem.SetTutorialActive(false);
    Game.instance.player.SetName("Player", "One");
  }

  public override void Update()
  {
    base.Update();
  }

  public override void GetFirstScreenForState(out string screenName, out UIManager.ScreenTransition screenTransition, out float transitionDuration, bool fromSave = false)
  {
    screenName = "QuickRaceSetupScreen";
    screenTransition = UIManager.ScreenTransition.Fade;
    transitionDuration = 1.5f;
  }

  public void SetCircuit(string inCircuit)
  {
    this.mCircuit = inCircuit;
  }

  public void SetRaceLength(QuickRaceSetupState.RaceLength inRaceLength)
  {
    this.mRaceLength = inRaceLength;
  }

  public void SetRaceWeekend(QuickRaceSetupState.RaceWeekend inRaceWeekend)
  {
    this.mRaceWeekend = inRaceWeekend;
  }

  public void SetGridOptions(QuickRaceSetupState.GridOptions inGridOptions)
  {
    this.mGridOptions = inGridOptions;
  }

  public void SetDriver1GridPosition(int inDriver1GridPosition)
  {
    this.mDriver1GridPosition = inDriver1GridPosition;
  }

  public void SetDriver2GridPosition(int inDriver2GridPosition)
  {
    this.mDriver2GridPosition = inDriver2GridPosition;
  }

  public void SetWeatherSettings(SessionDetails.WeatherSettings inWeatherSettings)
  {
    this.mWeatherSettings = inWeatherSettings;
  }

  public void SetTrackLayout(Circuit.TrackLayout inLayout)
  {
    this.mTrackLayout = inLayout;
  }

  public void PrepareCircuit()
  {
    if (!Game.IsActive())
      return;
    Championship championship = Game.instance.player.team.championship;
    RaceEventDetails raceEventDetails = championship.calendar[0];
    RaceEventCalendarData eventCalendarData = new RaceEventCalendarData();
    eventCalendarData.circuit = Game.instance.circuitManager.GetCircuitByLocationNameLayout(this.mCircuit, this.mTrackLayout);
    eventCalendarData.week = 25;
    raceEventDetails.sessions.Clear();
    raceEventDetails.circuit = eventCalendarData.circuit;
    raceEventDetails.CalculateEventDate(eventCalendarData.week, DateTime.Now.Year, championship.rules);
    raceEventDetails.SetupWeather(this.weatherSettings, championship);
    StringVariableParser.newTrack = raceEventDetails.circuit;
    StringVariableParser.randomChampionship = championship;
    CalendarEvent_v1 calendarEvent1 = new CalendarEvent_v1() { showOnCalendar = true, category = CalendarEventCategory.TravelToEvent, triggerDate = raceEventDetails.eventDate, triggerState = GameState.Type.FrontendState, interruptGameTime = true, uiState = CalendarEvent.UIState.TimeBarFillTarget, effect = (EventEffect) new GoToEventPreStateEffect() };
    calendarEvent1.SetDynamicDescription("PSG_10009141");
    Game.instance.calendar.AddEvent(calendarEvent1);
    CalendarEvent_v1 calendarEvent2 = new CalendarEvent_v1() { triggerDate = raceEventDetails.eventDate.AddHours(1.0), triggerState = GameState.Type.TravelArrangements, effect = (EventEffect) new GoToEventStateEffect() };
    Game.instance.calendar.AddEvent(calendarEvent2);
    using (List<SessionDetails>.Enumerator enumerator = raceEventDetails.practiceSessions.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        SessionDetails current = enumerator.Current;
        CalendarEvent_v1 calendarEvent3 = new CalendarEvent_v1() { showOnCalendar = true, category = CalendarEventCategory.Race, triggerDate = current.sessionDateTime };
        calendarEvent3.SetDynamicDescription("PSG_10009142");
        Game.instance.calendar.AddEvent(calendarEvent3);
      }
    }
    using (List<SessionDetails>.Enumerator enumerator = raceEventDetails.qualifyingSessions.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        SessionDetails current = enumerator.Current;
        CalendarEvent_v1 calendarEvent3 = new CalendarEvent_v1() { showOnCalendar = true, category = CalendarEventCategory.Race, triggerDate = current.sessionDateTime };
        calendarEvent3.SetDynamicDescription("PSG_10009143");
        Game.instance.calendar.AddEvent(calendarEvent3);
      }
    }
    using (List<SessionDetails>.Enumerator enumerator = raceEventDetails.raceSessions.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        SessionDetails current = enumerator.Current;
        CalendarEvent_v1 calendarEvent3 = new CalendarEvent_v1() { showOnCalendar = true, category = CalendarEventCategory.Race, triggerDate = current.sessionDateTime };
        calendarEvent3.SetDynamicDescription("PSG_10009144");
        Game.instance.calendar.AddEvent(calendarEvent3);
      }
    }
  }

  public enum RaceLength
  {
    Short,
    Medium,
    Long,
  }

  public enum RaceWeekend
  {
    Full,
    RaceOnly,
  }

  public enum GridOptions
  {
    Random,
    Predefined,
  }
}
