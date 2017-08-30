
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChallengeRestriction
{
    private bool mHasFailed;


    public enum RestrictionType
    {
        TeamsAllowed,
        LeaveTeam,
        DriverLeaveTeam,
        BuildCarParts,
        Count,
    }
}
