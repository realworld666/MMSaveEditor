
using System.Diagnostics;

public abstract class GameState
{
  private float mStateTimer;
  public enum Type
  {
    None,
    StartUp,
    TitleLoading,
    TitleState,
    FrontendLoading,
    FrontendState,
    PreSeasonTestingState,
    EventLoading,
    TravelArrangements,
    PreSessionHUB,
    PostSessionDataCenter,
    SessionWinner,
    PracticePreSession,
    PracticePostSession,
    Practice,
    QualifyingPreSession,
    QualifyingPostSession,
    Qualifying,
    RacePreSession,
    RacePostSession,
    RaceGrid,
    Race,
    SkipSession,
    QuickRaceSetup,
    PreSeasonState,
    PostEventFrontendState,
    Null,
    ChallengeSetup,
    NewGameSetup,
    SimulateEventState,
  }

  public enum Group
  {
    None,
    Frontend,
    Event,
    Simulation,
  }
}
