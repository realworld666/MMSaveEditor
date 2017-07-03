// Decompiled with JetBrains decompiler
// Type: RadioMessageQualifying
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RadioMessageQualifying : RadioMessage
{
  private int mExpectedPosition;

  public RadioMessageQualifying(RacingVehicle inVehicle, TeamRadio inTeamRadio)
    : base(inVehicle, inTeamRadio)
  {
    RacingVehicle racingVehicle = inVehicle;
    Action action = racingVehicle.OnLapEnd + new Action(this.OnLapEnd);
    racingVehicle.OnLapEnd = action;
    inVehicle.pathState.OnPitlaneEnter += new Action(this.OnPitlaneEnter);
    Team team = this.mVehicle.driver.contract.GetTeam();
    Circuit circuit = Game.instance.sessionManager.eventDetails.circuit;
    this.mExpectedPosition = Game.instance.teamManager.CalculateExpectedPositionForRace(team, circuit);
  }

  public override void OnLoad()
  {
    this.mVehicle.pathState.OnPitlaneEnter -= new Action(this.OnPitlaneEnter);
    this.mVehicle.pathState.OnPitlaneEnter += new Action(this.OnPitlaneEnter);
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
    if (Game.instance.sessionManager.sessionType != SessionDetails.SessionType.Qualifying || this.mVehicle.pathState.IsInPitlaneArea() || (this.mHasPlayerBeenInformedAlready[1] || this.mVehicle.performance.temperatureOptimisation.mode != TemperatureOptimisation.Mode.Complete))
      return;
    this.mHasPlayerBeenInformedAlready[1] = true;
    this.SendMessageOnTyreTemperatures();
  }

  private void SendMessageOnTyreTemperatures()
  {
    if (this.mVehicle.performance.temperatureOptimisation.tyreStatus == TemperatureOptimisation.Status.Overheated)
    {
      DialogQuery dialogQuery = this.CreateDialogQuery();
      dialogQuery.AddCriteria("Source", "QualifyingPanelFeedback");
      dialogQuery.AddCriteria("TyresTooHot", "True");
      this.SendMessage(dialogQuery);
    }
    else
    {
      if (this.mVehicle.performance.temperatureOptimisation.tyreStatus != TemperatureOptimisation.Status.Cold)
        return;
      DialogQuery dialogQuery = this.CreateDialogQuery();
      dialogQuery.AddCriteria("Source", "QualifyingPanelFeedback");
      dialogQuery.AddCriteria("TyresTooCold", "True");
      this.SendMessage(dialogQuery);
    }
  }

  private void OnLapEnd()
  {
    if (Game.instance.sessionManager.sessionType != SessionDetails.SessionType.Qualifying)
      return;
    if (this.mVehicle.timer.lapData.Count > 0 && !this.mVehicle.pathState.IsInPitlaneArea() && !this.mVehicle.timer.wasLastLapAnOutLap)
    {
      this.SendLapReaction();
    }
    else
    {
      if (this.mVehicle.pathState.IsInPitlaneArea() || !this.mVehicle.timer.wasLastLapAnOutLap || Game.instance.sessionManager.hasSessionEnded)
        return;
      this.SendStartingFlyingLap();
    }
  }

  private void OnPitlaneEnter()
  {
    for (int index = 0; index < this.mHasPlayerBeenInformedAlready.Length; ++index)
      this.mHasPlayerBeenInformedAlready[index] = false;
  }

  private void SendStartingFlyingLap()
  {
    DialogQuery dialogQuery = this.CreateDialogQuery();
    dialogQuery.AddCriteria("Source", "QualifyingLapStarting");
    this.SendMessage(dialogQuery);
  }

  private void SendLapReaction()
  {
    DialogQuery dialogQuery = this.CreateDialogQuery();
    dialogQuery.AddCriteria("Source", "QualifyingLapReaction");
    bool flag1 = this.mVehicle.performance.temperatureOptimisation.brakeStatus >= TemperatureOptimisation.Status.Good;
    bool flag2 = this.mVehicle.performance.temperatureOptimisation.tyreStatus >= TemperatureOptimisation.Status.Good;
    bool flag3 = flag1 || flag2;
    if (Game.instance.sessionManager.standings[10].timer.lapData.Count > 0)
    {
      bool flag4 = this.mVehicle.timer.lapData.Count == 1 || this.mVehicle.previousStandingsPosition > this.mVehicle.standingsPosition;
      if (flag3 && this.mVehicle.standingsPosition <= this.mExpectedPosition && flag4)
        dialogQuery.AddCriteria("Performance", "Good");
      else
        dialogQuery.AddCriteria("Performance", "Bad");
    }
    else if (flag1 || this.mVehicle.standingsPosition <= 3)
      dialogQuery.AddCriteria("Performance", "Good");
    else
      dialogQuery.AddCriteria("Performance", "Bad");
    this.SendMessage(dialogQuery);
  }

  private DialogQuery CreateDialogQuery()
  {
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    StringVariableParser.SetStaticData(this.personWhoSpeaks);
    DialogQuery inQuery = new DialogQuery();
    this.AddPersonCriteria(inQuery, (Person) this.mVehicle.driver);
    return inQuery;
  }

  private void SendMessage(DialogQuery inQuery)
  {
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(inQuery, false);
    if (this.dialogRule == null)
      return;
    this.SendRadioMessage();
  }

  public void OnSessionEnd()
  {
    if (this.mHasPlayerBeenInformedAlready[0])
      return;
    this.mHasPlayerBeenInformedAlready[0] = true;
    if (this.mVehicle.standingsPosition != 1 || Game.instance.sessionManager.eventDetails.hasSeveralQualifyingSessions && !Game.instance.sessionManager.eventDetails.IsLastOfMultipleQualifyingSessions())
      return;
    DialogQuery dialogQuery = this.CreateDialogQuery();
    dialogQuery.AddCriteria("Source", "QualifyingSessionReaction");
    dialogQuery.AddCriteria("Performance", "Pole");
    this.SendMessage(dialogQuery);
  }
}
