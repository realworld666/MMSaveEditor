using FullSerializer;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class DriverStamina
{
    private float mMaximumStamina = 1f;
    private float mDangerZone = 0.4f;
    private DriverStamina.DriverState mCurrentState = DriverStamina.DriverState.None;
    public const float DangerZoneMax = 0.4f;
    private const float DangerZoneMin = 0.1f;
    private const float StartingStaminaMax = 1f;
    private const float StartingStaminaMin = 0.6f;
    private const float OptimalZoneSizeShortRacePref = 0.15f;
    private const float OptimalZoneSizeMediumRacePref = 0.15f;
    private const float OptimalZoneSizeLongRacePref = 0.15f;
    private float mOptimalZone;
    private float mCurrentStamina;
    private float mRandomModifier;
    private float mDriverFitness01;
    private float mOptimalZoneHalfSize;
    private RacingVehicle mVehicle;
    private Driver mDriver;
    [NonSerialized]
    private DriverPerformanceDesignData mDriverPerformanceDesignData;


    public enum DriverState
    {
        Resting,
        Driving,
        None,
    }
}
