// Decompiled with JetBrains decompiler
// Type: InterviewData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class InterviewData
{
  private List<DialogCriteria> mData = new List<DialogCriteria>();

  public List<DialogCriteria> data
  {
    get
    {
      return this.mData;
    }
  }

  public void GatherData()
  {
    this.mData.Clear();
    this.SetInterviewSubject();
    Player player = Game.instance.player;
    Championship inChampionship = player.team == null ? (Championship) null : player.team.championship;
    RaceEventDetails raceEventDetails = inChampionship == null ? (RaceEventDetails) null : inChampionship.GetPreviousEventDetails();
    RaceEventResults.SessonResultData sessonResultData1 = raceEventDetails == null ? (RaceEventResults.SessonResultData) null : raceEventDetails.results.GetResultsForSession(SessionDetails.SessionType.Practice);
    RaceEventResults.SessonResultData sessonResultData2 = raceEventDetails == null ? (RaceEventResults.SessonResultData) null : raceEventDetails.results.GetResultsForSession(SessionDetails.SessionType.Qualifying);
    RaceEventResults.SessonResultData sessonResultData3 = raceEventDetails == null ? (RaceEventResults.SessonResultData) null : raceEventDetails.results.GetResultsForSession(SessionDetails.SessionType.Race);
    RaceEventResults.ResultData resultData = sessonResultData3 == null || StringVariableParser.interviewSubject == null ? (RaceEventResults.ResultData) null : sessonResultData3.GetResultForDriver(StringVariableParser.interviewSubject as Driver);
    RaceEventResults.ResultData inData1 = sessonResultData3 == null ? (RaceEventResults.ResultData) null : sessonResultData3.GetBestPlayerDriverResult();
    RaceEventResults.ResultData inData2 = sessonResultData2 == null ? (RaceEventResults.ResultData) null : sessonResultData2.GetBestPlayerDriverResult();
    RaceEventResults.ResultData inData3 = sessonResultData1 == null ? (RaceEventResults.ResultData) null : sessonResultData1.GetBestPlayerDriverResult();
    RaceEventResults.ResultData inData4 = sessonResultData3 == null ? (RaceEventResults.ResultData) null : sessonResultData3.GetOtherPlayerDriverResult();
    RaceEventResults.ResultData inData5 = sessonResultData1 == null || inData4 == null ? (RaceEventResults.ResultData) null : sessonResultData1.GetResultForDriver(inData4.driver);
    int num1 = 0;
    int num2 = 0;
    if (inData1 != null)
    {
      if (inData1.carState == RaceEventResults.ResultData.CarState.Crashed)
        ++num1;
      if (inData1.carState == RaceEventResults.ResultData.CarState.Retired)
        ++num2;
    }
    if (inData4 != null)
    {
      if (inData4.carState == RaceEventResults.ResultData.CarState.Crashed)
        ++num1;
      if (inData4.carState == RaceEventResults.ResultData.CarState.Retired)
        ++num2;
    }
    for (InterviewData.CriteriaType criteriaType = InterviewData.CriteriaType.PlayerEmployed; criteriaType < InterviewData.CriteriaType.Count; ++criteriaType)
    {
      DialogCriteria dialogCriteria = new DialogCriteria(criteriaType.ToString(), string.Empty);
      switch (criteriaType)
      {
        case InterviewData.CriteriaType.PlayerEmployed:
          dialogCriteria.mCriteriaInfo = (!player.IsUnemployed()).ToString();
          break;
        case InterviewData.CriteriaType.PlayerBackstory:
          dialogCriteria.mCriteriaInfo = player.playerBackStoryType.ToString();
          break;
        case InterviewData.CriteriaType.PlayerSupportsVote:
          continue;
        case InterviewData.CriteriaType.PlayerRacesInChargeOfTeam:
          dialogCriteria.mCriteriaInfo = player.careerHistory.GetTotalCareerRaces(player.team).ToString();
          break;
        case InterviewData.CriteriaType.PlayerOnPole:
          if (inData2 != null)
          {
            dialogCriteria.mCriteriaInfo = (inData2.position == 1).ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.PlayerOneTwo:
          if (inData1 != null)
          {
            dialogCriteria.mCriteriaInfo = (inData1.position == 1 && inData4 != null && inData4.position == 2).ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.ExpectedPlayerTeamPosition:
          if (!player.IsUnemployed())
          {
            dialogCriteria.mCriteriaInfo = Game.instance.teamManager.CalculateExpectedPositionForChampionship(player.team).ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.VoteType:
          if (inChampionship != null && inChampionship.politicalSystem.activeVote != null)
          {
            dialogCriteria.mCriteriaInfo = inChampionship.politicalSystem.activeVote.messageCriteria.GetSource().ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.FiredDriverBeforeInterview:
          dialogCriteria.mCriteriaInfo = (sessonResultData3 != null && (!inData1.driver.contract.GetTeam().IsPlayersTeam() || !inData4.driver.contract.GetTeam().IsPlayersTeam())).ToString();
          break;
        case InterviewData.CriteriaType.PlayerDriver1RacePositionVsExpectation:
          if (inData1 != null)
          {
            dialogCriteria.mType = "#1PlayerDriverRacePositionVsExpectation";
            dialogCriteria.mCriteriaInfo = DialogQueryCreator.GetDriverExpectedResultVsActualResult(inData1).ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.PlayerDriver2RacePositionVsExpectation:
          if (inData4 != null)
          {
            dialogCriteria.mType = "#2PlayerDriverRacePositionVsExpectation";
            dialogCriteria.mCriteriaInfo = DialogQueryCreator.GetDriverExpectedResultVsActualResult(inData4).ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.PlayerDriver1RacePosition:
        case InterviewData.CriteriaType.PlayerDriver1Position:
          if (inData1 != null)
          {
            dialogCriteria.mType = criteriaType != InterviewData.CriteriaType.PlayerDriver1Position ? "#1PlayerDriverPosition" : "#1PlayerDriverRacePosition";
            dialogCriteria.mCriteriaInfo = inData1.position.ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.PlayerDriver2RacePosition:
        case InterviewData.CriteriaType.PlayerDriver2Position:
          if (inData4 != null)
          {
            dialogCriteria.mType = criteriaType != InterviewData.CriteriaType.PlayerDriver1Position ? "#2PlayerDriverPosition" : "#2PlayerDriverRacePosition";
            dialogCriteria.mCriteriaInfo = inData4.position.ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.PlayerDriver1DNF:
          if (inData1 != null)
          {
            dialogCriteria.mType = "#1PlayerDriverDNF";
            dialogCriteria.mCriteriaInfo = (inData1.carState == RaceEventResults.ResultData.CarState.Retired).ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.PlayerDriver2DNF:
          if (inData4 != null)
          {
            dialogCriteria.mType = "#2PlayerDriverDNF";
            dialogCriteria.mCriteriaInfo = (inData4.carState == RaceEventResults.ResultData.CarState.Retired).ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.PlayerDriver1Crashed:
          if (inData1 != null)
          {
            dialogCriteria.mCriteriaInfo = (inData1.carState == RaceEventResults.ResultData.CarState.Crashed).ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.PlayerDriver2Crashed:
          if (inData4 != null)
          {
            dialogCriteria.mCriteriaInfo = (inData4.carState == RaceEventResults.ResultData.CarState.Crashed).ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.TeamOrdersUsed:
          if (inData1 != null && inData4 != null)
          {
            dialogCriteria.mCriteriaInfo = (inData1.usedTeamOrders || inData4.usedTeamOrders).ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.InterviewedAboutPriorities:
          dialogCriteria.mCriteriaInfo = (Game.instance.player.dialogQuery.GetMemory(criteriaType.ToString()) != null).ToString();
          break;
        case InterviewData.CriteriaType.PlayerQualifyingPosition:
          if (inData2 != null && inChampionship.rules.qualifyingBasedActive)
          {
            dialogCriteria.mCriteriaInfo = inData2.position.ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.PlayerQualifyingPositionVsExpectation:
          if (inData2 != null && inChampionship.rules.qualifyingBasedActive)
          {
            dialogCriteria.mCriteriaInfo = DialogQueryCreator.GetDriverExpectedResultVsActualResult(inData2).ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.PlayerPracticePositionVsExpectation:
          if (inData3 != null)
          {
            dialogCriteria.mCriteriaInfo = DialogQueryCreator.GetDriverExpectedResultVsActualResult(inData3).ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.PlayerRacePositionVsExpectation:
          if (inData1 != null)
          {
            dialogCriteria.mCriteriaInfo = DialogQueryCreator.GetDriverExpectedResultVsActualResult(inData1).ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.ISDNFs:
          if (sessonResultData3 != null)
          {
            dialogCriteria.mCriteriaInfo = num2.ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.ISPlayerCrashes:
          if (sessonResultData3 != null)
          {
            dialogCriteria.mCriteriaInfo = num1.ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.ISRiskyPitStopFailed:
          if (resultData != null)
          {
            dialogCriteria.mCriteriaInfo = resultData.riskyPitStopFailed.ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.PlayerLetISTyresRunOut:
          if (resultData != null)
          {
            dialogCriteria.mCriteriaInfo = resultData.tyresRunOut.ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.PlayerLetISFuelRunOut:
          if (resultData != null)
          {
            dialogCriteria.mCriteriaInfo = resultData.fuelRunOut.ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.ISCausedCrash:
          if (resultData != null)
          {
            dialogCriteria.mCriteriaInfo = resultData.driverCausedCrash.ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.ISCrashedInto:
          if (resultData != null)
          {
            dialogCriteria.mCriteriaInfo = resultData.driverCrashedInto.ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.ISFixedLowReliabilityPart:
          if (resultData != null)
          {
            dialogCriteria.mCriteriaInfo = resultData.lowReliabilityPartFixed.ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.ISLowReliabilityPartBroke:
          if (resultData != null)
          {
            dialogCriteria.mCriteriaInfo = resultData.lowReliabilityPartBroke.ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.ISPenalisedCausedCollision:
          if (resultData != null)
          {
            bool flag = false;
            for (int index = 0; index < resultData.penalties.Count; ++index)
            {
              if (resultData.penalties[index].cause == Penalty.PenaltyCause.Collision)
                flag = true;
            }
            dialogCriteria.mCriteriaInfo = !flag ? "False" : "True";
            break;
          }
          continue;
        case InterviewData.CriteriaType.ISScrutineeringPenalty:
          if (resultData != null)
          {
            bool flag = false;
            for (int index = 0; index < resultData.penalties.Count; ++index)
            {
              if (resultData.penalties[index].penaltyType == Penalty.PenaltyType.PartPenalty)
                flag = true;
            }
            dialogCriteria.mCriteriaInfo = !flag ? "False" : "True";
            break;
          }
          continue;
        case InterviewData.CriteriaType.AIDriverHadAwfulRace:
          if (StringVariableParser.GetObject("AIDriverHadAwfulRace") != null)
          {
            dialogCriteria.mCriteriaInfo = "True";
            break;
          }
          break;
        case InterviewData.CriteriaType.BadPracticeSessionAndRace:
          if (inData1 != null && inData3 != null && (inData4 != null && inData5 != null))
          {
            if (DialogQueryCreator.GetDriverExpectedResultVsActualResult(inData1) > 4 && DialogQueryCreator.GetDriverExpectedResultVsActualResult(inData4) > 4 && (DialogQueryCreator.GetDriverExpectedResultVsActualResult(inData3) > 4 && DialogQueryCreator.GetDriverExpectedResultVsActualResult(inData5) > 4))
            {
              dialogCriteria.mCriteriaInfo = "True";
              break;
            }
            break;
          }
          continue;
        case InterviewData.CriteriaType.ISCarHadLowQualityPart:
          if (!Game.instance.player.IsUnemployed())
          {
            if (TeamStatistics.GetLowQualityPart(inChampionship) != CarPart.PartType.None)
            {
              dialogCriteria.mCriteriaInfo = "True";
              break;
            }
            break;
          }
          continue;
        case InterviewData.CriteriaType.PlayerDriverPerformingBadly:
          if (!Game.instance.player.IsUnemployed())
          {
            if (StringVariableParser.GetObject("PlayerDriverPerformingBadly") is Driver)
            {
              dialogCriteria.mCriteriaInfo = "True";
              break;
            }
            break;
          }
          continue;
        case InterviewData.CriteriaType.ISTopOfDC:
          if (!Game.instance.player.IsUnemployed() && resultData != null)
          {
            dialogCriteria.mCriteriaInfo = Game.instance.player.team.championship.standings.GetDriverEntry(0).GetEntity<Driver>() != resultData.driver ? "False" : "True";
            break;
          }
          continue;
        case InterviewData.CriteriaType.TCPositionVsChairmanExpectation:
          if (!Game.instance.player.IsUnemployed())
          {
            Team team = Game.instance.player.team;
            dialogCriteria.mCriteriaInfo = (team.championship.standings.GetEntry((Entity) team).GetCurrentChampionshipPosition() - team.chairman.expectedTeamChampionshipResult).ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.PlayerAcceptedBribe:
          if (!Game.instance.player.IsUnemployed())
          {
            PoliticalSystem.VoteResults latestVoteResults = Game.instance.player.team.championship.politicalSystem.GetLatestVoteResults();
            if (latestVoteResults != null)
            {
              dialogCriteria.mCriteriaInfo = (latestVoteResults.votedSubject.playerBribe != DilemmaSystem.BribedOption.None).ToString();
              break;
            }
            continue;
          }
          continue;
        case InterviewData.CriteriaType.GMAVoteRecently:
          if (!Game.instance.player.IsUnemployed())
          {
            List<CalendarEvent_v1> calendarEventsForSeason = Game.instance.player.team.championship.politicalSystem.voteCalendarEventsForSeason;
            bool flag = false;
            for (int index = 0; index < calendarEventsForSeason.Count; ++index)
            {
              int days = (calendarEventsForSeason[index].triggerDate - Game.instance.time.now).Days;
              if (days <= 0 && days > -15)
                flag = true;
            }
            dialogCriteria.mCriteriaInfo = flag.ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.TimeGap1stTo2nd:
          if (sessonResultData3 != null)
          {
            dialogCriteria.mCriteriaInfo = sessonResultData3.resultData[1].gapToLeader.ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.RumouredDriverMove:
          if (!Game.instance.player.IsUnemployed())
          {
            dialogCriteria.mCriteriaInfo = !(StringVariableParser.GetObject("DriverTransferRumour") is TeamRumour) ? "False" : "True";
            break;
          }
          continue;
        case InterviewData.CriteriaType.StandingsGapBetweenDrivers:
          if (!Game.instance.player.IsUnemployed())
          {
            Team team = Game.instance.player.team;
            int num3 = team.championship.standings.GetEntry((Entity) team.GetDriver(0)).GetCurrentChampionshipPosition() - team.championship.standings.GetEntry((Entity) team.GetDriver(1)).GetCurrentChampionshipPosition();
            dialogCriteria.mCriteriaInfo = Mathf.Abs(num3).ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.ISPosition:
          if (resultData != null)
          {
            dialogCriteria.mCriteriaInfo = resultData.position.ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.ISErrorCostWin:
          if (!Game.instance.player.IsUnemployed() && resultData != null)
          {
            dialogCriteria.mCriteriaInfo = resultData.crashedWhenInFirstPlace.ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.ISWrongCompoundInRain:
          if (!Game.instance.player.IsUnemployed())
            break;
          continue;
        case InterviewData.CriteriaType.ISNoPitStopWhenWet:
          if (!Game.instance.player.IsUnemployed())
            break;
          continue;
        case InterviewData.CriteriaType.RivalsWonTC:
          if (!Game.instance.player.IsUnemployed())
          {
            bool flag = inChampionship.IsTeamChampionshipWon() && inChampionship.standings.GetTeamEntry(0).GetEntity<Team>() == Game.instance.player.team.rivalTeam;
            dialogCriteria.mCriteriaInfo = flag.ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.PlayerDriverWonDC:
          if (!Game.instance.player.IsUnemployed())
          {
            bool flag = inChampionship.IsDriverChampionshipWon() && inChampionship.standings.GetDriverEntry(0).GetEntity<Driver>().contract.GetTeam() == Game.instance.player.team;
            dialogCriteria.mCriteriaInfo = flag.ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.PlayerWonTC:
          if (!Game.instance.player.IsUnemployed())
          {
            bool flag = inChampionship.IsTeamChampionshipWon() && inChampionship.standings.GetTeamEntry(0).GetEntity<Team>() == Game.instance.player.team;
            dialogCriteria.mCriteriaInfo = flag.ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.IS2ndInTeam:
          if (resultData != null)
          {
            dialogCriteria.mCriteriaInfo = (resultData.position != inData1.position).ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.PlayerTCPosition:
          if (!Game.instance.player.IsUnemployed())
          {
            Team team = Game.instance.player.team;
            dialogCriteria.mCriteriaInfo = team.championship.standings.GetEntry((Entity) team).GetCurrentChampionshipPosition().ToString();
            break;
          }
          continue;
        case InterviewData.CriteriaType.IsLastRaceOfSeason:
          if (!player.IsUnemployed())
          {
            dialogCriteria.mCriteriaInfo = player.team.championship.GetFinalEventDetails().hasEventEnded.ToString();
            break;
          }
          continue;
      }
      this.mData.Add(dialogCriteria);
    }
  }

  private void SetInterviewSubject()
  {
    Player player = Game.instance.player;
    Championship championship = player.team == null ? (Championship) null : player.team.championship;
    Person person = (Person) null;
    int num = 0;
    for (int inIndex = 0; inIndex < Team.mainDriverCount; ++inIndex)
    {
      RaceEventDetails raceEventDetails = championship == null ? (RaceEventDetails) null : championship.GetPreviousEventDetails();
      RaceEventResults.SessonResultData sessonResultData = raceEventDetails == null ? (RaceEventResults.SessonResultData) null : raceEventDetails.results.GetResultsForSession(SessionDetails.SessionType.Race);
      if (sessonResultData != null)
      {
        Driver driver = player.team.GetDriver(inIndex);
        RaceEventResults.ResultData resultForDriver = sessonResultData.GetResultForDriver(driver);
        if (person == null || num < this.GetCountOfInterestingEventsForResult(resultForDriver))
        {
          num = this.GetCountOfInterestingEventsForResult(resultForDriver);
          person = (Person) driver;
        }
      }
    }
    StringVariableParser.interviewSubject = person;
  }

  private int GetCountOfInterestingEventsForResult(RaceEventResults.ResultData inData)
  {
    int num = 0;
    if (inData == null)
      return num;
    if (inData.driverCausedCrash)
      ++num;
    if (inData.driverCrashedInto)
      ++num;
    if (inData.carState != RaceEventResults.ResultData.CarState.None)
      ++num;
    if (inData.fuelRunOut)
      ++num;
    if (inData.lowReliabilityPartBroke)
      ++num;
    if (inData.lowReliabilityPartFixed)
      ++num;
    if (inData.penalties.Count > 0)
      ++num;
    if (inData.riskyPitStopFailed)
      ++num;
    if (inData.tyresRunOut)
      ++num;
    if (inData.usedTeamOrders)
      ++num;
    return num;
  }

  public enum CriteriaType
  {
    PlayerEmployed,
    PlayerBackstory,
    PlayerSupportsVote,
    PlayerRacesInChargeOfTeam,
    PlayerOnPole,
    PlayerOneTwo,
    ExpectedPlayerTeamPosition,
    LegendaryTeam,
    VoteType,
    RuleBenefits,
    ExpectedPartLevel,
    FiredDriverBeforeInterview,
    PlayerDriver1RacePositionVsExpectation,
    PlayerDriver2RacePositionVsExpectation,
    PlayerDriver1RacePosition,
    PlayerDriver2RacePosition,
    PlayerDriver1DNF,
    PlayerDriver2DNF,
    PlayerDriver1Crashed,
    PlayerDriver2Crashed,
    PlayerDriver1Position,
    PlayerDriver2Position,
    TeamOrdersUsed,
    InterviewedAboutPriorities,
    PlayerQualifyingPosition,
    PlayerQualifyingPositionVsExpectation,
    PlayerPracticePositionVsExpectation,
    PlayerRacePositionVsExpectation,
    ISDNFs,
    ISPlayerCrashes,
    ISRiskyPitStopFailed,
    PlayerLetISTyresRunOut,
    PlayerLetISFuelRunOut,
    ISCausedCrash,
    ISCrashedInto,
    ISFixedLowReliabilityPart,
    ISLowReliabilityPartBroke,
    ISPenalisedCausedCollision,
    ISScrutineeringPenalty,
    AIDriverHadAwfulRace,
    BadPracticeSessionAndRace,
    ISCarHadLowQualityPart,
    PlayerDriverPerformingBadly,
    ISTopOfDC,
    TCPositionVsChairmanExpectation,
    PlayerAcceptedBribe,
    GMAVoteRecently,
    TimeGap1stTo2nd,
    RumouredDriverMove,
    StandingsGapBetweenDrivers,
    ISPosition,
    ISErrorCostWin,
    ISWrongCompoundInRain,
    ISNoPitStopWhenWet,
    RivalsWonTC,
    PlayerDriverWonDC,
    PlayerWonTC,
    IS2ndInTeam,
    PlayerTCPosition,
    IsLastRaceOfSeason,
    Count,
  }
}
