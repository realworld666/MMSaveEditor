
using FullSerializer;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PathTransform
{
    private Vector3 mPosition;
    private Vector3 mForward;
    private Vector3 mRight;
    private CollisionBounds mCollisionBounds;


}
