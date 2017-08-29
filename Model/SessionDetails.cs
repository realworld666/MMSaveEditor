using System;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SessionDetails
{
    private DateTime mSessionDateTime;
    private SessionWeatherDetails mSessionWeatherDetails;
    private SessionDetails.SessionType mSessionType;
    private bool mHasEnded;
    private bool mIsSessionSimulated;
    private int mTVAudience;
    private int mAttendence;
    private int mSessionNumber = -1;
    public int sessionTypeCount;

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
