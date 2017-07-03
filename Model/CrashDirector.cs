
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class CrashDirector
{
	private int mRealSafetyCarCount = 1;
	private int mSessionCrashCount;
	private float mVirtualSafetyFlagDuration;
	private SafetyVehicle mSafetyCar;
	private SessionManager mSessionManager;


}
