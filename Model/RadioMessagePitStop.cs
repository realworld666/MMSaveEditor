// Decompiled with JetBrains decompiler
// Type: RadioMessagePitStop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using System;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RadioMessagePitStop : RadioMessage
{
  public RadioMessagePitStop(RacingVehicle inVehicle, TeamRadio inTeamRadio)
    : base(inVehicle, inTeamRadio)
  {
    inVehicle.pathState.OnPitlaneExit += new Action(this.CheckPitStopOutcome);
  }

  public override void OnLoad()
  {
    this.mVehicle.pathState.OnPitlaneExit -= new Action(this.CheckPitStopOutcome);
    this.mVehicle.pathState.OnPitlaneExit += new Action(this.CheckPitStopOutcome);
  }

  protected override void OnSimulationUpdate()
  {
  }

  private void CheckPitStopOutcome()
  {
    if (this.mVehicle.strategy.previousStatus == SessionStrategy.Status.PitThruPenalty || this.mVehicle.timer.hasSeenChequeredFlag || (!this.isRaceSession || Game.instance.tutorialSystem.isTutorialActiveInCurrentGameState) || (double) (this.mVehicle.driver.GetDriverStats().feedback / 20f) < (double) UnityEngine.Random.Range(0.0f, 1f))
      return;
    if (this.mVehicle.setup.changes.GetChangeCarPartWithMistake() != null)
      StringVariableParser.partForUI = this.mVehicle.setup.changes.GetChangeCarPartWithMistake().repairPart.carPart;
    DialogQuery inQuery = new DialogQuery();
    inQuery.AddCriteria("Source", "PostPitStop");
    this.AddPersonCriteria(inQuery, (Person) this.mVehicle.driver);
    bool flag = Game.instance.sessionManager.flag == SessionManager.Flag.Green && Mathf.Approximately(Game.instance.sessionManager.currentSessionWeather.GetNormalizedTrackWater(), 0.0f) && this.mVehicle.setup.changes.queuedCarBehindTeamMate;
    if (this.mVehicle.setup.changes.GetChangeBatteryRecharge().brokeERS)
      inQuery.AddCriteria("ERS", "Broken");
    else if ((double) this.mVehicle.setup.changes.GetChangeBatteryRecharge().rechargeAmount > 0.0)
      inQuery.AddCriteria("ERS", "Charged");
    if (this.mVehicle.setup.changes.hasMistakeOccured || flag)
      this.AddMistakeCriteria(inQuery);
    else if (this.mVehicle.setup.repairParts.Count > 0)
      this.AddRepairPartsCriteria(inQuery);
    else
      this.AddPitStopSpeedCriteria(inQuery);
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(inQuery, false);
    if (this.dialogRule == null)
      return;
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    this.SendRadioMessage();
  }

  private void AddPitStopSpeedCriteria(DialogQuery inQuery)
  {
    switch (this.mVehicle.setup.changes.pitstopOutcome)
    {
      case SessionSetupChange.Outcome.Bad:
        inQuery.AddCriteria("PitStopTime", "Slow");
        break;
      case SessionSetupChange.Outcome.Great:
        inQuery.AddCriteria("PitStopTime", "Fast");
        break;
    }
  }

  private void AddMistakeCriteria(DialogQuery inQuery)
  {
    if (this.mVehicle.setup.changes.mistake == SessionSetupChangeEntry.Target.Tyres)
      inQuery.AddCriteria("MistakeType", "Tyres");
    else if (this.mVehicle.setup.changes.mistake == SessionSetupChangeEntry.Target.Fuel)
      inQuery.AddCriteria("MistakeType", "Fuel");
    else if (this.mVehicle.setup.changes.mistake == SessionSetupChangeEntry.Target.PartsRepair)
    {
      inQuery.AddCriteria("MistakeType", "Repair");
    }
    else
    {
      if (!this.mVehicle.setup.changes.queuedCarBehindTeamMate)
        return;
      inQuery.AddCriteria("MistakeType", "QueuedCars");
    }
  }

  private void AddRepairPartsCriteria(DialogQuery inQuery)
  {
    if (this.mVehicle.setup.repairParts.Count == 1)
    {
      CarPart.PartType partType = this.mVehicle.setup.repairParts[0].carPart.GetPartType();
      StringVariableParser.partFrontendUI = partType;
      switch (partType)
      {
        case CarPart.PartType.BrakesGT:
          partType = CarPart.PartType.Brakes;
          break;
        case CarPart.PartType.EngineGT:
          partType = CarPart.PartType.Engine;
          break;
        case CarPart.PartType.GearboxGT:
          partType = CarPart.PartType.Gearbox;
          break;
        case CarPart.PartType.SuspensionGT:
          partType = CarPart.PartType.Suspension;
          break;
      }
      inQuery.AddCriteria("PartRepaired", partType.ToString());
    }
    else
      inQuery.AddCriteria("PartRepaired", "Lots");
  }
}
