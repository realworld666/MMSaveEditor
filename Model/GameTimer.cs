using FullSerializer;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class GameTimer
{
    public static float baseSimulationDeltaTime = 0.03333334f;
    private static float maxDeltaTimeRecip = 0.06666667f;
    public static float totalSimulationDeltaTimeCurrentFrame = 0.0f;
    public static float debugSimSpeed = 0.0f;
    public static float[] skipSimSpeed = new float[4] { 0.0f, 20f, 30f, 40f };
    public static float debugSkipSpeed = 0.0f;
    public static float minSkipSpeed = 60000f;
    public static float maxSkipSpeed = 120000f;
    public static DateTime gameStartDate = new DateTime(2016, 3, 1);
    private readonly float mPreSeasonSkipSpeed = 300000f;
    private DateTime mNow = GameTimer.gameStartDate;
    public Action OnYearEnd;
    public Action OnMonthEnd;
    public Action OnHourEnd;
    public Action OnDayEnd;
    public Action OnWeekEnd;
    public Action OnPause;
    public Action OnPlay;
    public Action OnChangeSpeed;
    public Action OnSkipTargetReached;
    public Action<GameTimer.TimeState> OnChangeTimeState;
    private float mDeltaTime;
    private GameTimer.TimeState mTimeState;
    public float[,] speedMultipliers = new float[3, 3] { { 250f, 2500f, 25000f }, { 1f, 10f, 50f }, { 1.75f, 2.75f, 3.75f } };
    private bool[] mPauseState = new bool[5];

    public DateTime now
    {
        get
        {
            return this.mNow;
        }
        set
        {
            mNow = value;
        }
    }
    public enum Speed
    {
        Slow,
        Medium,
        Fast,
    }

    public enum SimSkipSpeed
    {
        Pause,
        Slow,
        Medium,
        Fast,
    }

    public enum SpeedMode
    {
        Frontend,
        Event,
        Simulation,
    }

    public enum TimeState
    {
        Standard,
        Skip,
    }

    public enum PauseType
    {
        Game,
        App,
        UI,
        Tutorial,
        Dilemma,
        Count,
    }
}
