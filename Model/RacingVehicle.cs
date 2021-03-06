﻿using FullSerializer;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RacingVehicle : Vehicle
{
    public Driver driver;
    public RacingVehicle leader;
    public RacingVehicle ahead;
    public RacingVehicle behind;
    public RaceEventResults.ResultData resultData;
    public int carID;
    public Car car;

    private SessionTimer mTimer = new SessionTimer();
    private SessionStats mStats = new SessionStats();
    private SessionStrategy mStrategy = new SessionStrategy();
    private SessionSetup mSetup = new SessionSetup();
    private SessionStints mStints = new SessionStints();
    private SessionPartFailure mPartFailure = new SessionPartFailure();
    private SessionCarBonuses mBonuses = new SessionCarBonuses();
    private SessionEvents mSessionEvents = new SessionEvents();
    private PracticeKnowledge mPracticeKnowledge = new PracticeKnowledge();
    private SessionPenalty mSessionPenalty = new SessionPenalty();
    private ERSController mERSController = new ERSController();
    private TeamRadio mTeamRadio = new TeamRadio();
    private int mStandingsPosition;
    private int mPreviousStandingsPosition;
    private int mPreviousStandingsPositionForCommentary;
    private float mTimeUntilNextPreviousPositionUpdate;
    private int mLapsBehindLeader;
    private int mSessionSetupCount;
    private bool mFavourite;
    private float mCommentaryCooldown;
    private bool mWaitingForCooldown;
    private float mSpeedTrapSpeed;
    private RaceEventResults.ResultData mSessionData = new RaceEventResults.ResultData();
    private Driver[] mDriversForCar;
    private string mDriverNameCache = string.Empty;
    public Action OnDriverChange;
    private int mConcurrentStandingsPosition;
    private int mOvertakes;
    private bool mIsPlayerDriver;

    private bool mRefreshTyrePerformance;

}
