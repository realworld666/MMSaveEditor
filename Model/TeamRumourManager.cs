using System;
using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TeamRumourManager
{
    private List<TeamRumour> mCurrentRumours = new List<TeamRumour>();
    private List<TeamRumour> mLastDispatchedRumours = new List<TeamRumour>();
    private int mNumDaysToWait;
    private DateTime mLastRumourDate;
    private readonly int maxNumRumoursToDispatch = 1;
    private readonly int coolDownPeriod = 365;
    private readonly int coolDownPeriodRetiring = 365;
    private readonly int minDelayBetweenRumours = 5;
    private readonly int maxDelayBetweenRumours = 21;
    private List<Person> mPeopleToRemove = new List<Person>();

}
