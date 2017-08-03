
using FullSerializer;
using System.Collections.Generic;
using UnityEngine;

public class PathSpline
{

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public struct SplinePosition
    {
        public int id;
        public Vector3 position;
        public Vector3 forward;
        public Vector3 right;
        public float pathDistance;
    }
}
