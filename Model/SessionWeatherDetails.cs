using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SessionWeatherDetails
{
    public static float rainMinimum = 0.1f;
    public static int numberOfFrames = 50;
    public static int dropdownNumberOfKeyFrames = 20;

    public float minAirTemp;
    public float maxAirTemp;
    public float maxTrackTemp = 50f;
    public SessionWeatherDetails previousSessionWeather;
    private float mNormalizedTimeElapsed;
    private float mPreviousNormalizedTime;
    private float mClearSkyChance;
    private float mCloudyChance;
    private float mOvercastChance;
    private float mStormyChance;
    private float[] mTrackRubberHistory = new float[SessionWeatherDetails.dropdownNumberOfKeyFrames];
    private float mTrackRubber;
    private int mMonthIndex;
    private float mNormalizedRain;
    private float mNormalizedCloud;
    private float mTrackTemp;
    private float mTrackTempNormalized;
    private float mNormalizedTrackWater;
    private bool mAddTutorialRain;
    private float mTutorialRainNormalizedValue;
    private Circuit mCircuit;
    private SimulationSettings mSimulationSettings;
    private SessionDetails mSessionDetails;
    private SessionDetails.WeatherSettings mWeatherSettings;
    private List<SessionWeatherDetails.CurveName> mCurveUsed = new List<SessionWeatherDetails.CurveName>();
    private List<SessionWeatherDetails.SprinklerData> mSprinklerData = new List<SessionWeatherDetails.SprinklerData>();
    private SessionWeatherDetails.CopyData mCopyData;
    private int mRandomSeed = -1;
    private System.Random mRandomGenerator;

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class SprinklerData
    {
        public float normalizedTimeStart;
        public float duration;
        public float targetSprinkler;
    }

    public enum CurveName
    {
        ClearWeatherCurves,
        CloudyRainWeatherCurves,
        OvercastRainWeatherCurves,
        MoonsoonRainWeatherCurves,
    }

    public enum CopyData
    {
        None,
        TrackRubber,
    }
}
