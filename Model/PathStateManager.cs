using FullSerializer;
using System;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class PathStateManager
{
	private GaragePathState garageState = new GaragePathState();
	private GarageEntryPathState garageEntryState = new GarageEntryPathState();
	private GarageExitPathState garageExitState = new GarageExitPathState();
	private PitboxPathState pitboxState = new PitboxPathState();
	private PitboxEntryPathState pitboxEntryState = new PitboxEntryPathState();
	private PitboxExitPathState pitboxExitState = new PitboxExitPathState();
	private GridPathState gridState = new GridPathState();
	private PitlanePathState pitlaneState = new PitlanePathState();
	private TrackPathState trackState = new TrackPathState();
	private PathStateManager.StateType mStateType = PathStateManager.StateType.Racing;
	private PathStateManager.PathStateGroup mPathStateGroup = PathStateManager.PathStateGroup.InPitbox;
	private PathStateManager.PathStateGroup mPreviousPathStateGroup = PathStateManager.PathStateGroup.InPitbox;
	public Action OnPathStateGroupChanged;
	public Action OnPitlaneEnter;
	public Action OnPitlaneExit;
	public Action OnGarageEnter;
	public Action OnGarageExit;
	private Vehicle mVehicle;
	private PathState mCurrentState;
	private PathState mPreviousState;

	public enum StateType
	{
		Garage,
		GarageEntry,
		GarageExit,
		Grid,
		Pitbox,
		PitboxEntry,
		PitboxExit,
		Pitlane,
		Racing,
	}

	public enum PathStateGroup
	{
		OnTrack,
		OnGrid,
		InGarage,
		InPitbox,
		InPitlane,
	}
}
