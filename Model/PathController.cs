using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PathController
{
    public List<Vehicle> nearbyObstacles = new List<Vehicle>(20);
    private PathController.Path[] mPath = new PathController.Path[11];
    private List<int> mGateIDsPassedWhileOffTrack = new List<int>(32);
    private int mCurrentPathTypeInt = -1;
    private int mNextBrakingGateId = -1;
    public Vehicle vehicleAheadOnPath;
    public Vehicle vehicleBehindOnPath;
    private Vehicle mVehicle;
    private PathController.PathType mCurrentPathType;
    private float mDistanceSinceLastGate;
    //private static Plane3 mVehiclePathSpacePlane;
    private float[] mDistanceToVehicle = new float[0];
    private bool[] mIsInComparablePath = new bool[25];
    private float mDistanceAlongTrackPath01;
    private SessionDetails.SessionType mCachedSessionType;

    public enum PathType
    {
        Track,
        Pitlane,
        PitlaneEntry,
        PitlaneExit,
        PitboxEntry,
        PitboxExit,
        GarageEntry,
        GarageExit,
        CrashLane,
        Count,
    }

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class Path
    {
        private int previousGateId = -1;
        private int nextGateId = -1;
        public PathSpline.SplinePosition racingLinePosition = new PathSpline.SplinePosition();
        public PathSpline.SplinePosition centerLinePosition = new PathSpline.SplinePosition();
        public PathController.PathType pathType;
        public Vehicle vehicle;
        public float pathSpace;
        private int mPreviousGateId = -1;
        private int mNextGateId = -1;
        public int pathID;
    }
}
