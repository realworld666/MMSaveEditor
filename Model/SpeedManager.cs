using FullSerializer;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class SpeedManager
{
	private TrackLayoutSpeedController mTrackLayoutController = new TrackLayoutSpeedController();
	private AvoidanceSpeedController mAvoidanceController = new AvoidanceSpeedController();
	private InOutLapSpeedController mInOutLapLapController = new InOutLapSpeedController();
	private PathTypeSpeedController mPathTypeSpeedController = new PathTypeSpeedController();
	private SafetyCarSpeedController mSafetyCarSpeedController = new SafetyCarSpeedController();
	private TyreLockUpSpeedController mTyreLockUpSpeedController = new TyreLockUpSpeedController();
	private SpiningOutSpeedController mSpiningOutSpeedController = new SpiningOutSpeedController();
	private BlueFlagSpeedController mBlueFlagSpeedController = new BlueFlagSpeedController();
	private GroupSpeedController mGroupSpeedController = new GroupSpeedController();
	private List<SpeedController> mControllers = new List<SpeedController>();
	private Vehicle mVehicle;
	private float mDesiredSpeed;

	public enum Controller
	{
		TrackLayout,
		Avoidance,
		InOutLap,
		PathType,
		SafetyCar,
		TyreLockUp,
		SpiningOut,
		BlueFlag,
		GroupSpeed,
	}
}
