
using FullSerializer;
using MM2;
using System;
using System.Collections.Generic;
using System.Linq;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RaceEventResults
{
    private List<RaceEventResults.SessonResultData> practiceSessions = new List<RaceEventResults.SessonResultData>();
    private List<RaceEventResults.SessonResultData> qualifyingSessions = new List<RaceEventResults.SessonResultData>();
    private List<RaceEventResults.SessonResultData> raceSessions = new List<RaceEventResults.SessonResultData>();
    private List<Team> mTeamList = new List<Team>();



    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class ResultData
    {
        public int carID;
        public Driver driver;
        public Driver[] drivers;
        public Team team;
        public List<Penalty> penalties = new List<Penalty>();
        public TyreSet.Compound tyreCompound = TyreSet.Compound.Soft;
        public float time;
        public float gapToAhead;
        public float gapToLeader;
        public float bestLapTime;
        public int stops;
        public int laps;
        public int lapsToLeader;
        public int lapsToAhead;
        public int gridPosition;
        public int position;
        public int expectedPosition;
        public int points;
        public int qualifyingSession;
        public bool qualifyingPositionRecorded;
        public bool sessionFastestLap;
        public bool gotDoublePointsForLastRace;
        public bool gotExtraPointsForFastestLap;
        public bool gotExtraPointsForPolePosition;
        public bool sessionWasSimulated;
        public RaceEventResults.ResultData.CarState carState;
        public Driver accidentDriver;
        public bool usedTeamOrders;
        public bool crashedWhenInFirstPlace;
        public bool riskyPitStopFailed;
        public bool tyresRunOut;
        public bool fuelRunOut;
        public int positioWhenOutOfFuel = -1;
        public bool driverCausedCrash;
        public Driver crashVictim;
        public bool driverCrashedInto;
        public Driver crashDriver;
        public bool lowReliabilityPartFixed;
        public bool lowReliabilityPartBroke;
        public bool racedWithWorstCar;
        public float[] normalizedTimeDriven = new float[0];
        public bool[] hasStaminaRechedZero = new bool[0];

        public int concurrentPosition;
        public float totalDistanceMiles;

        public bool retiredWhenInFirstPlace;
        public bool had2MinutesGap;
        public bool pitMistakeCostWin;

        public enum CarState
        {
            None,
            [LocalisationID("PSG_10007789")]
            Crashed,
            [LocalisationID("PSG_10007790")]
            Retired,
        }
    }

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class SessonResultData
    {
        public List<RaceEventResults.ResultData> resultData = new List<RaceEventResults.ResultData>();


    }
}
