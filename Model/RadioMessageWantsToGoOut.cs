// Decompiled with JetBrains decompiler
// Type: RadioMessageWantsToGoOut
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using System;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RadioMessageWantsToGoOut : RadioMessage
{
  private float mTimeUntilNextMessage;
  private float mDriverFeedbackRating;

  public RadioMessageWantsToGoOut(RacingVehicle inVehicle, TeamRadio inTeamRadio)
    : base(inVehicle, inTeamRadio)
  {
    this.mDriverFeedbackRating = this.mVehicle.driver.GetDriverStats().feedback;
    inVehicle.pathState.OnPitlaneEnter += new Action(this.OnPitlaneEnter);
    this.CalculateNextPromptTime();
  }

  public override void OnLoad()
  {
    this.mVehicle.pathState.OnPitlaneEnter -= new Action(this.OnPitlaneEnter);
    this.mVehicle.pathState.OnPitlaneEnter += new Action(this.OnPitlaneEnter);
  }

  public override bool DontDelayForDriverFeedback()
  {
    return true;
  }

  protected override void OnSimulationUpdate()
  {
    SessionDetails.SessionType sessionType = Game.instance.sessionManager.sessionType;
    if (!(this.mVehicle.pathState.currentState is GaragePathState) || sessionType == SessionDetails.SessionType.Race || (Game.instance.sessionManager.flag == SessionManager.Flag.Chequered || (double) Game.instance.sessionManager.time <= 120.0) || this.mVehicle.setup.state != SessionSetup.State.Setup)
      return;
    this.mTimeUntilNextMessage -= GameTimer.simulationDeltaTime;
    if ((double) this.mTimeUntilNextMessage >= 0.0)
      return;
    this.CalculateNextPromptTime();
    DialogQuery inQuery = new DialogQuery();
    inQuery.AddCriteria("Source", "WantToGoOut");
    this.AddPersonCriteria(inQuery, (Person) this.mVehicle.driver);
    if (this.mVehicle.timer.lapData.Count == 0)
    {
      inQuery.AddCriteria("WantToGoOutReason", "NoLapSet");
      if (sessionType == SessionDetails.SessionType.Practice)
        inQuery.AddCriteria("SessionType", "Practice");
      else
        inQuery.AddCriteria("SessionType", "Qualifying");
    }
    else if (!this.mHasPlayerBeenInformedAlready[0])
    {
      this.mHasPlayerBeenInformedAlready[0] = true;
      float practiceBonusAmount1 = this.mVehicle.practiceKnowledge.practiceReport.GetPracticeBonusAmount(PracticeReportSessionData.KnowledgeType.QualifyingTrim);
      float practiceBonusAmount2 = this.mVehicle.practiceKnowledge.practiceReport.GetPracticeBonusAmount(PracticeReportSessionData.KnowledgeType.RaceTrim);
      bool qualifyingBasedActive = this.mVehicle.driver.contract.GetTeam().championship.rules.qualifyingBasedActive;
      if (sessionType == SessionDetails.SessionType.Practice && (!MathsUtility.ApproximatelyZero(practiceBonusAmount1) || !MathsUtility.ApproximatelyZero(practiceBonusAmount2)) && (double) RandomUtility.GetRandom01() < 0.600000023841858)
      {
        if ((double) practiceBonusAmount2 < (double) practiceBonusAmount1 || !qualifyingBasedActive)
          inQuery.AddCriteria("WantToGoOutReason", "RaceTrim");
        else
          inQuery.AddCriteria("WantToGoOutReason", "QualiTrim");
      }
      else
        inQuery.AddCriteria("WantToGoOutReason", "General");
    }
    else if (!this.mHasPlayerBeenInformedAlready[1] && (double) Game.instance.sessionManager.currentSessionWeather.GetNormalizedTrackRubber() > 0.400000005960464)
    {
      this.mHasPlayerBeenInformedAlready[1] = true;
      inQuery.AddCriteria("WantToGoOutReason", "TrackConditionGood");
    }
    else if (!this.mHasPlayerBeenInformedAlready[2] && (double) Game.instance.sessionManager.currentSessionWeather.GetNormalizedTrackRubber() > 0.699999988079071)
    {
      this.mHasPlayerBeenInformedAlready[2] = true;
      inQuery.AddCriteria("WantToGoOutReason", "TrackRubberedIn");
    }
    this.CreateMessage(inQuery);
  }

  public void OnPitlaneEnter()
  {
    this.CalculateNextPromptTime();
    for (int index = 0; index < this.mHasPlayerBeenInformedAlready.Length; ++index)
      this.mHasPlayerBeenInformedAlready[index] = false;
  }

  private void CalculateNextPromptTime()
  {
    this.mTimeUntilNextMessage = Mathf.Lerp(50f, 0.0f, Mathf.Clamp01(this.mDriverFeedbackRating / 20f)) + RandomUtility.GetRandom(50f, 100f);
  }

  private void CreateMessage(DialogQuery inQuery)
  {
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(inQuery, false);
    if (this.dialogRule == null)
      return;
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    this.SendRadioMessage();
  }
}
