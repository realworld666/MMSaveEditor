
using FullSerializer;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SteeringContextMap
{
    public static readonly float slotHeight = 4f;
    public static int slotCount = 32;
    public static float pathSpacePerSlot = (float)(2.0 / ((double)SteeringContextMap.slotCount - 1.0));
    public static float[] cachedPositionPathSpace = (float[])null;
    private float[] mSlot = new float[SteeringContextMap.slotCount];
    private Ray2 mLeftGradientRay = new Ray2();
    private Ray2 mRightGradientRay = new Ray2();

}
