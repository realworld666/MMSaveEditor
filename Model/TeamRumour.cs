
using FullSerializer;
using System;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class TeamRumour
{
	public Person mPerson;
	public Team mDestTeam;
	public TeamRumour.Type mType;
	public DateTime mDate;
	public HQsBuilding_v1 mBuilding;
	public CarPart mCarPart;
	public enum Type
	{
		WantsToLeave,
		LookingAtTeam,
		TeamUnhappyWith,
		TeamLookingAt,
		Retiring,
		UnhappyWithTeammate,
		HQBuildingComplete,
		HQBuildingUpgrade,
		CarPartComplete,
	}
}
