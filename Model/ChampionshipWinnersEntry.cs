using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChampionshipWinnersEntry
{
    public Driver driverChampion;
    public Team driversTeam;
    public Person driversTeamPrincipal;
    public int driverPoints;
    public int driverRaces;
    public int driverPodiums;
    public int driverWins;
    public int driverDNFs;
    public Team teamChampion;
    public Person teamsTeamPrincipal;
    public Person[] teamsDrivers = new Person[2];
    public int[] teamsDriversPosition = new int[2];
    public int[] teamsDriversPoints = new int[2];
    public int teamPoints;
    public long teamPrizeMoney;
    public int year;
}
