
using FullSerializer;
using System.Text;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class AIPitCrew
{
    private AIPitCrewTaskStat[] carOneTaskStats;
    private AIPitCrewTaskStat[] carTwoTaskStats;
    private Team mTeam;

    public void RegenerateTaskStats()
    {
        this.RandomizeTaskStats(ref this.carOneTaskStats, this.mTeam.GetMechanicOfDriver(this.mTeam.GetDriverForCar(0)));
        this.RandomizeTaskStats(ref this.carTwoTaskStats, this.mTeam.GetMechanicOfDriver(this.mTeam.GetDriverForCar(1)));
    }

    private void RandomizeTaskStats(ref AIPitCrewTaskStat[] inTaskStats, Mechanic inMechanic)
    {
        for (int index = 0; index < inTaskStats.Length; ++index)
        {
            inTaskStats[index].taskType = (SessionSetupChangeEntry.Target)index;
            inTaskStats[index].taskStat = inMechanic.stats.pitStops;
            inTaskStats[index].taskConfidence = RandomUtility.GetRandom(0.75f, 1f);
        }
    }
}
