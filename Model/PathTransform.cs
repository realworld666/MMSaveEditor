
using FullSerializer;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PathTransform
{
    private Vector3 mPosition = Vector3.zero;
    private Vector3 mForward = Vector3.forward;
    private Vector3 mRight = Vector3.right;
    private CollisionBounds mCollisionBounds;


}
