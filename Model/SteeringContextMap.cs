
using FullSerializer;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SteeringContextMap
{
    public static readonly float slotHeight = 4f;
    private static readonly float[] smoothingKernal = new float[5]
    {
    0.05448868f,
    0.2442013f,
    0.4026199f,
    0.2442013f,
    0.05448868f
    };
    public static int slotCount = 32;
    public static float pathSpacePerSlot = (float)(2.0 / ((double)SteeringContextMap.slotCount - 1.0));
    public static float[] cachedPositionPathSpace = (float[])null;
    private float[] mSlot = new float[SteeringContextMap.slotCount];
    private Ray2 mLeftGradientRay = new Ray2();
    private Ray2 mRightGradientRay = new Ray2();

}
