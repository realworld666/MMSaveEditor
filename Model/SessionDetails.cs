using System;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SessionDetails
{
    private int mSessionNumber = -1;
    private DateTime mSessionDateTime;
    private SessionWeatherDetails mSessionWeatherDetails;
    private SessionDetails.SessionType mSessionType;
    private bool mHasEnded;
    private bool mIsSessionSimulated;
    private int mTVAudience;
    private int mAttendence;


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
