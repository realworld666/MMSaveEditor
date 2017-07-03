
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class ScoutStats : PersonStats
{
	public const int scoutStatsMax = 20;
	public const int scoutStatsNum = 5;
	public const int scoutStatsTotalMax = 100;
	public float drivers;
	public float youngDrivers;
	public float engineers;
	public float mechanics;
	public float sponsors;


}
