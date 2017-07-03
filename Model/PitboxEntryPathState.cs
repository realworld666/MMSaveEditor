using FullSerializer;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class PitboxEntryPathState : PathState
{
	private RacingVehicle mTeamMate;
	private bool mNeedsToWaitForTeamMate;

}
