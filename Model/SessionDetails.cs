using System;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SessionDetails
{
    public static int sessionTypeCount = 3;
    private DateTime mSessionDateTime;
    private SessionWeatherDetails mSessionWeatherDetails;
    private SessionDetails.SessionType mSessionType;
    private bool mHasEnded;
    private bool mIsSessionSimulated;
    private int mTVAudience;
    private int mAttendence;
    private int mSessionNumber = -1;
    private bool mWillHaveCatastrophicPitstopMistake;

    public DateTime sessionDateTime
    {
        get
        {
            return this.mSessionDateTime;
        }
    }

    public SessionDetails.SessionType sessionType
    {
        get
        {
            return this.mSessionType;
        }
    }

    public bool hasEnded
    {
        get
        {
            return this.mHasEnded;
        }
    }

    public enum SessionType
    {
        [LocalisationID("PSG_10002224")] Practice,
        [LocalisationID("PSG_10002225")] Qualifying,
        [LocalisationID("PSG_10002226")] Race,
    }

    public enum WeatherSettings
    {
        Dynamic,
        Dry,
        Wet,
    }
}
