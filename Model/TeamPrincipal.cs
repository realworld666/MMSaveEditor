using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TeamPrincipal : Person
{
    public static int daysUntilCanRejoinTeam = 365;
    public static int daysJoinedTeamRecently = 60;
    public static int daysJoinedProtection = 90;
    public TeamPrincipalStats stats = new TeamPrincipalStats();
    public TeamPrincipal.Backstory backStory;

    public override bool IsReplacementPerson()
    {
        return Game.instance.teamPrincipalManager.IsReplacementPerson(this);
    }

    public enum Backstory
    {
        [LocalisationID("PSG_10005815")] None,
        [LocalisationID("PSG_10005816")] Business,
        [LocalisationID("PSG_10005817")] ExDriver,
        [LocalisationID("PSG_10005818")] ExEngineer,
        [LocalisationID("PSG_10005819")] Legal,
    }

    public enum JobSecurity
    {
        [LocalisationID("PSG_10005820")] Edge,
        [LocalisationID("PSG_10005821")] Risk,
        [LocalisationID("PSG_10005822")] Safe,
        [LocalisationID("PSG_10005823")] Great,
    }
}
