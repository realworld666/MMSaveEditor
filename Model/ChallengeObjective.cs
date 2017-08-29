using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChallengeObjective : InstanceCounter
{
    protected bool mIsObjectiveComplete;


    public enum ObjectiveType
    {
        RacePosition,
        ChampionshipPosition,
        FinancesBudget,
        StaffQuality,
        WinTopTier,
        RaceChampionshipPosition,
        Count,
    }
}
