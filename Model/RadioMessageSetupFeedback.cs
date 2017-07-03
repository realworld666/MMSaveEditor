// Decompiled with JetBrains decompiler
// Type: RadioMessageSetupFeedback
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using System;
using System.Collections.Generic;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RadioMessageSetupFeedback : RadioMessage
{
  private float mTimeUntilNextFeedback;
  private float mDriverFeedbackRating;

  public RadioMessageSetupFeedback(RacingVehicle inVehicle, TeamRadio inTeamRadio)
    : base(inVehicle, inTeamRadio)
  {
    this.mDriverFeedbackRating = this.mVehicle.driver.GetDriverStats().feedback;
    inVehicle.pathState.OnPitlaneExit += new Action(this.OnPitlaneExit);
    this.mHasPlayerBeenInformedAlready = new bool[6];
  }

  public override void OnLoad()
  {
    this.mVehicle.pathState.OnPitlaneExit -= new Action(this.OnPitlaneExit);
    this.mVehicle.pathState.OnPitlaneExit += new Action(this.OnPitlaneExit);
  }

  protected override bool CanDisplayMessageAfterOrOnLastLap()
  {
    return true;
  }

  public override bool DontDelayForDriverFeedback()
  {
    return true;
  }

  protected override void OnSimulationUpdate()
  {
    if (Game.instance.sessionManager.sessionType != SessionDetails.SessionType.Practice)
      return;
    if (this.mVehicle.pathState.IsInPitlaneArea())
    {
      this.CalculateNextFeedbackTime();
    }
    else
    {
      this.mTimeUntilNextFeedback -= GameTimer.simulationDeltaTime;
      if ((double) this.mTimeUntilNextFeedback >= 0.0)
        return;
      this.CreateDialogQuery();
      this.CalculateNextFeedbackTime();
    }
  }

  private void CalculateNextFeedbackTime()
  {
    this.mTimeUntilNextFeedback = Mathf.Lerp(30f, 0.0f, Mathf.Clamp01(this.mDriverFeedbackRating / 20f)) + RandomUtility.GetRandom(60f, 120f);
  }

  public void OnPitlaneExit()
  {
    for (int index = 0; index < this.mHasPlayerBeenInformedAlready.Length; ++index)
      this.mHasPlayerBeenInformedAlready[index] = false;
  }

  private void CreateDialogQuery()
  {
    if (Game.instance.tutorialSystem.isTutorialActiveInCurrentGameState)
      return;
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    StringVariableParser.SetStaticData(this.personWhoSpeaks);
    DialogQuery inQuery = new DialogQuery();
    this.AddPersonCriteria(inQuery, (Person) this.mVehicle.driver);
    this.AddSetupOpinionCriteria(inQuery);
    inQuery.AddCriteria("Source", "SetupFeedback");
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(inQuery, false);
    if (this.dialogRule == null)
      return;
    this.SendRadioMessage();
  }

  private void AddSetupOpinionCriteria(DialogQuery inQuery)
  {
    List<SetupStintData> setupStintData1 = Game.instance.persistentEventData.GetSetupStintData(this.mVehicle);
    if (setupStintData1.Count <= 0)
      return;
    SetupStintData setupStintData2 = setupStintData1[setupStintData1.Count - 1];
    if (setupStintData1.Count == 1)
    {
      this.GiveGeneralFeedback(inQuery, setupStintData2);
    }
    else
    {
      SetupStintData inPreviousStintData = setupStintData1[setupStintData1.Count - 2];
      if (this.GiveSetupChangeFeedback(inQuery, inPreviousStintData, setupStintData2))
        return;
      this.GiveGeneralFeedback(inQuery, setupStintData2);
    }
  }

  private bool GiveSetupChangeFeedback(DialogQuery inQuery, SetupStintData inPreviousStintData, SetupStintData inCurrentStintData)
  {
    if (!this.mHasPlayerBeenInformedAlready[3] && (double) Mathf.Abs(inPreviousStintData.deltaFromOptimumAerodynamics - inCurrentStintData.deltaFromOptimumAerodynamics) < 0.100000001490116)
      this.mHasPlayerBeenInformedAlready[3] = true;
    if (!this.mHasPlayerBeenInformedAlready[4] && (double) Mathf.Abs(inPreviousStintData.deltaFromOptimumHandling - inCurrentStintData.deltaFromOptimumHandling) < 0.100000001490116)
      this.mHasPlayerBeenInformedAlready[4] = true;
    if (!this.mHasPlayerBeenInformedAlready[5] && (double) Mathf.Abs(inPreviousStintData.deltaFromOptimumSpeedBalance - inCurrentStintData.deltaFromOptimumSpeedBalance) < 0.100000001490116)
      this.mHasPlayerBeenInformedAlready[5] = true;
    if (!this.mHasPlayerBeenInformedAlready[3])
    {
      this.mHasPlayerBeenInformedAlready[3] = true;
      if (inPreviousStintData.aerodynamicsOpinion < inCurrentStintData.aerodynamicsOpinion)
      {
        this.AddAeroCriteria(inQuery);
        inQuery.AddCriteria("Setup", "Better");
        return true;
      }
      if (inPreviousStintData.aerodynamicsOpinion > inCurrentStintData.aerodynamicsOpinion)
      {
        this.AddAeroCriteria(inQuery);
        inQuery.AddCriteria("Setup", "Worse");
        return true;
      }
    }
    else if (!this.mHasPlayerBeenInformedAlready[4])
    {
      this.mHasPlayerBeenInformedAlready[4] = true;
      if (inPreviousStintData.handlingOpinion < inCurrentStintData.handlingOpinion)
      {
        inQuery.AddCriteria("SetupArea", "Handling");
        inQuery.AddCriteria("Setup", "Better");
        return true;
      }
      if (inPreviousStintData.handlingOpinion > inCurrentStintData.handlingOpinion)
      {
        inQuery.AddCriteria("SetupArea", "Handling");
        inQuery.AddCriteria("Setup", "Worse");
        return true;
      }
    }
    else if (!this.mHasPlayerBeenInformedAlready[5])
    {
      this.mHasPlayerBeenInformedAlready[5] = true;
      if (inPreviousStintData.speedBalanceOpinion < inCurrentStintData.speedBalanceOpinion)
      {
        inQuery.AddCriteria("SetupArea", "Speed");
        inQuery.AddCriteria("Setup", "Better");
        return true;
      }
      if (inPreviousStintData.speedBalanceOpinion > inCurrentStintData.speedBalanceOpinion)
      {
        inQuery.AddCriteria("SetupArea", "Speed");
        inQuery.AddCriteria("Setup", "Worse");
        return true;
      }
    }
    return false;
  }

  private void AddAeroCriteria(DialogQuery inQuery)
  {
    if (Game.instance.sessionManager.championship.series == Championship.Series.SingleSeaterSeries)
      inQuery.AddCriteria("SetupArea", "Aero");
    else
      inQuery.AddCriteria("SetupArea", "Cornering");
  }

  private void GiveGeneralFeedback(DialogQuery inQuery, SetupStintData inStintData)
  {
    bool flag1 = !this.mHasPlayerBeenInformedAlready[0] && (double) inStintData.deltaFromOptimumAerodynamics > (double) inStintData.deltaFromOptimumHandling && (double) inStintData.deltaFromOptimumAerodynamics > (double) inStintData.deltaFromOptimumSpeedBalance;
    bool flag2 = !this.mHasPlayerBeenInformedAlready[1] && (double) inStintData.deltaFromOptimumHandling > (double) inStintData.deltaFromOptimumAerodynamics && (double) inStintData.deltaFromOptimumHandling > (double) inStintData.deltaFromOptimumSpeedBalance;
    bool flag3 = !this.mHasPlayerBeenInformedAlready[2] && (double) inStintData.deltaFromOptimumSpeedBalance > (double) inStintData.deltaFromOptimumAerodynamics && (double) inStintData.deltaFromOptimumSpeedBalance > (double) inStintData.deltaFromOptimumHandling;
    if (!flag1 && !flag2 && !flag3)
    {
      if (!this.mHasPlayerBeenInformedAlready[0])
        flag1 = true;
      else if (!this.mHasPlayerBeenInformedAlready[1])
        flag2 = true;
      else if (!this.mHasPlayerBeenInformedAlready[2])
        flag3 = true;
    }
    if (flag1)
    {
      this.mHasPlayerBeenInformedAlready[0] = true;
      this.AddAeroCriteria(inQuery);
      if (inStintData.aerodynamicsOpinion >= SessionSetup.SetupOpinion.Good)
        inQuery.AddCriteria("Setup", "Good");
      else if ((double) inStintData.deltaFromOptimumAerodynamics > 0.0)
        inQuery.AddCriteria("Setup", "High");
      else
        inQuery.AddCriteria("Setup", "Low");
    }
    else if (flag2)
    {
      this.mHasPlayerBeenInformedAlready[1] = true;
      inQuery.AddCriteria("SetupArea", "Handling");
      if (inStintData.handlingOpinion >= SessionSetup.SetupOpinion.Good)
        inQuery.AddCriteria("Setup", "Good");
      else if ((double) inStintData.deltaFromOptimumHandling > 0.0)
        inQuery.AddCriteria("Setup", "Low");
      else
        inQuery.AddCriteria("Setup", "High");
    }
    else
    {
      if (!flag3)
        return;
      this.mHasPlayerBeenInformedAlready[2] = true;
      inQuery.AddCriteria("SetupArea", "Speed");
      if (inStintData.speedBalanceOpinion >= SessionSetup.SetupOpinion.Good)
        inQuery.AddCriteria("Setup", "Good");
      else if ((double) inStintData.deltaFromOptimumSpeedBalance > 0.0)
        inQuery.AddCriteria("Setup", "Low");
      else
        inQuery.AddCriteria("Setup", "High");
    }
  }
}
