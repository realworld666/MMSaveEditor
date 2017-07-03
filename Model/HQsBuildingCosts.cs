
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class HQsBuildingCosts
{
	private HQsBuilding_v1 mBuilding;

	public enum TimePeriod
	{
		Monthly,
		Yearly,
		PerRace,
	}
}
