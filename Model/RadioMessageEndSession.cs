// Decompiled with JetBrains decompiler
// Type: RadioMessageEndSession
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RadioMessageEndSession : RadioMessage
{
  public RadioMessageEndSession(RacingVehicle inVehicle, TeamRadio inTeamRadio)
    : base(inVehicle, inTeamRadio)
  {
    RacingVehicle racingVehicle = inVehicle;
    Action action = racingVehicle.OnLapEnd + new Action(this.CreateDialogQuery);
    racingVehicle.OnLapEnd = action;
  }

  public override void OnLoad()
  {
    RacingVehicle mVehicle1 = this.mVehicle;
    Action action1 = mVehicle1.OnLapEnd - new Action(this.CreateDialogQuery);
    mVehicle1.OnLapEnd = action1;
    RacingVehicle mVehicle2 = this.mVehicle;
    Action action2 = mVehicle2.OnLapEnd + new Action(this.CreateDialogQuery);
    mVehicle2.OnLapEnd = action2;
  }

  protected override bool CanDisplayMessageAfterOrOnLastLap()
  {
    return true;
  }

  protected override void OnSimulationUpdate()
  {
  }

  private void CreateDialogQuery()
  {
    if (Game.instance.tutorialSystem.isTutorialActiveInCurrentGameState || !this.mVehicle.timer.hasSeenChequeredFlag)
      return;
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    StringVariableParser.SetStaticData(this.personWhoSpeaks);
    RacingVehicle mVehicle = this.mVehicle;
    Action action = mVehicle.OnLapEnd - new Action(this.CreateDialogQuery);
    mVehicle.OnLapEnd = action;
    DialogQuery inQuery = new DialogQuery();
    this.AddPersonCriteria(inQuery, (Person) this.mVehicle.driver);
    if (Game.instance.sessionManager.eventDetails.currentSession.sessionType != SessionDetails.SessionType.Race)
      return;
    inQuery.AddCriteria("Source", "RaceEnd");
    this.AddRaceExpectationCriteria(inQuery);
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(inQuery, false);
    if (this.dialogRule == null)
      return;
    this.SendRadioMessage();
  }

  private void AddRaceExpectationCriteria(DialogQuery inQuery)
  {
    Team team = this.mVehicle.driver.contract.GetTeam();
    int standingsPosition = this.mVehicle.standingsPosition;
    Circuit circuit = Game.instance.sessionManager.eventDetails.circuit;
    int num = Game.instance.teamManager.CalculateExpectedPositionForRace(team, circuit) - standingsPosition;
    if (standingsPosition == 1)
      inQuery.AddCriteria("Performance", "Winner");
    else if (num < -5)
      inQuery.AddCriteria("Performance", "Bad");
    else if (num < 0)
      inQuery.AddCriteria("Performance", "Average");
    else
      inQuery.AddCriteria("Performance", "Good");
  }
}
