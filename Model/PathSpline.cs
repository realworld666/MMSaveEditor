
using FullSerializer;
using System.Collections.Generic;
using UnityEngine;

public class PathSpline
{
    //private PathData.Type mType;
    //private CatmullRomSpline mSpline;
    //private PathSpline.SplinePosition[] mSplinePositions;
    //private float mLength;

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public struct SplinePosition
    {
        public int id;
        public Vector3 position;
        public Vector3 forward;
        public Vector3 right;
        public float pathDistance;
        //private PathData.Type mType;
        //private CatmullRomSpline mSpline;
        //private PathSpline.SplinePosition[] mSplinePositions;
        private float mLength;
    }
}
