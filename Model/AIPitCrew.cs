
using FullSerializer;
using System.Text;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class AIPitCrew
{
    private AIPitCrewTaskStat[] carOneTaskStats;
    private AIPitCrewTaskStat[] carTwoTaskStats;
    private Team mTeam;


}
