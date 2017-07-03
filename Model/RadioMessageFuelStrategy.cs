// Decompiled with JetBrains decompiler
// Type: RadioMessageFuelStrategy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RadioMessageFuelStrategy : RadioMessage
{
  private bool mIsRefuelingOn = true;

  public RadioMessageFuelStrategy(RacingVehicle inVehicle, TeamRadio inTeamRadio)
    : base(inVehicle, inTeamRadio)
  {
    inVehicle.pathState.OnPitlaneExit += new Action(this.ResetPlayerInformationFlags);
    this.mHasPlayerBeenInformedAlready = new bool[4];
    this.dilemmaType = RadioMessage.DilemmaType.Fuel;
    this.mIsRefuelingOn = this.mVehicle.driver.contract.GetTeam().championship.rules.isRefuelingOn;
  }

  public override void OnLoad()
  {
    this.mVehicle.pathState.OnPitlaneExit -= new Action(this.ResetPlayerInformationFlags);
    this.mVehicle.pathState.OnPitlaneExit += new Action(this.ResetPlayerInformationFlags);
  }

  protected override void ResetPlayerInformationFlags()
  {
    this.mHasPlayerBeenInformedAlready[0] = false;
    this.mHasPlayerBeenInformedAlready[1] = false;
    if (this.mVehicle.performance.fuel.GetFuelLapsRemaining() <= 0)
      return;
    this.mHasPlayerBeenInformedAlready[2] = false;
  }

  protected override void OnSimulationUpdate()
  {
    if (!this.IsVehicleReadyToDisplayMessage())
      return;
    this.CheckFuelLevels();
  }

  private bool IsVehicleReadyToDisplayMessage()
  {
    if (!this.IsFillingFuel() && (double) this.mVehicle.performance.fuel.GetNormalisedFuelLevel() < 0.300000011920929 && (!this.mVehicle.pathState.IsInPitlaneArea() && !this.mVehicle.timer.hasSeenChequeredFlag) && this.isRaceSession)
      return !this.mVehicle.behaviourManager.isOutOfRace;
    return false;
  }

  protected override bool CheckForDilemmaAndSendToPlayer()
  {
    if (!this.IsVehicleReadyToDisplayMessage() || (this.mHasPlayerBeenInformedAlready[2] || this.mVehicle.performance.fuel.GetFuelLapsRemaining() > 0))
      return false;
    this.CreateDilemmaDialogQuery();
    this.mHasPlayerBeenInformedAlready[2] = true;
    return true;
  }

  private void CheckFuelLevels()
  {
    bool flag1 = this.mVehicle.performance.fuel.engineMode == Fuel.EngineMode.High;
    bool flag2 = this.mVehicle.performance.fuel.engineMode == Fuel.EngineMode.Overtake || this.mVehicle.performance.fuel.engineMode == Fuel.EngineMode.SuperOvertake;
    bool flag3 = this.mVehicle.performance.fuel.engineMode == Fuel.EngineMode.Low;
    bool flag4 = this.mVehicle.performance.fuel.engineMode == Fuel.EngineMode.Medium;
    if (flag1 || flag2)
    {
      float targetFuelLapDelta = this.mVehicle.performance.fuel.GetTargetFuelLapDelta();
      if ((double) targetFuelLapDelta <= -2.0 && !this.mHasPlayerBeenInformedAlready[0])
      {
        this.CreateDialogRule();
        this.mHasPlayerBeenInformedAlready[0] = true;
      }
      else
      {
        if ((double) targetFuelLapDelta <= 0.0)
          return;
        this.mHasPlayerBeenInformedAlready[0] = false;
      }
    }
    else
    {
      if (!flag3 && !flag4)
        return;
      float targetFuelLapDelta = this.mVehicle.performance.fuel.GetTargetFuelLapDelta();
      if ((double) targetFuelLapDelta >= 1.5 && !this.mHasPlayerBeenInformedAlready[1])
      {
        this.CreateDialogRule();
        this.mHasPlayerBeenInformedAlready[1] = true;
      }
      else
      {
        if ((double) targetFuelLapDelta >= 0.0)
          return;
        this.mHasPlayerBeenInformedAlready[1] = false;
      }
    }
  }

  private bool IsFillingFuel()
  {
    if (this.mVehicle.strategy.IsGoingToPit())
      return this.mVehicle.setup.IsRefueling();
    return false;
  }

  private void CreateDialogRule()
  {
    float targetFuelLapDelta = this.mVehicle.performance.fuel.GetTargetFuelLapDelta();
    DialogQuery inQuery = new DialogQuery();
    inQuery.AddCriteria("Source", "FuelStrategy");
    this.AddPersonCriteria(inQuery, (Person) this.mVehicle.driver);
    if (this.mVehicle.performance.fuel.GetFuelLapsRemaining() <= 0)
      inQuery.AddCriteria("FuelLevel", "Zero");
    else if ((double) targetFuelLapDelta <= -2.0)
      inQuery.AddCriteria("FuelLevel", "Low");
    else if ((double) targetFuelLapDelta >= 1.5)
      inQuery.AddCriteria("FuelLevel", "High");
    inQuery.AddCriteria("LapsToGo", Game.instance.sessionManager.GetLapsRemaining().ToString());
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(inQuery, false);
    if (this.dialogRule == null)
      return;
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    this.SendRadioMessage();
  }

  private void CreateDilemmaDialogQuery()
  {
    DialogQuery inQuery = new DialogQuery();
    inQuery.AddCriteria("Source", "FuelStrategy");
    this.AddPersonCriteria(inQuery, (Person) this.mVehicle.driver);
    if (this.mVehicle.performance.fuel.GetFuelLapsRemaining() <= 0)
      inQuery.AddCriteria("FuelLevel", "Zero");
    else
      inQuery.AddCriteria("FuelLevel", "Low");
    inQuery.AddCriteria("LapsToGo", Game.instance.sessionManager.GetLapsRemaining().ToString());
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(inQuery, false);
    if (this.dialogRule == null)
      return;
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    if (this.mIsRefuelingOn)
      this.SendDilemma();
    else
      this.SendRadioMessage();
  }
}
