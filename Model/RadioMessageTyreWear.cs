// Decompiled with JetBrains decompiler
// Type: RadioMessageTyreWear
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RadioMessageTyreWear : RadioMessage
{
  private List<TyreSet> mTyreSetUsed = new List<TyreSet>();
  private TyreSet mCurrentTyreSet;
  private bool mHasDonePunctureMessage;

  public RadioMessageTyreWear(RacingVehicle inVehicle, TeamRadio inTeamRadio)
    : base(inVehicle, inTeamRadio)
  {
    Game.instance.sessionManager.OnSessionStart += new Action(this.UpdateTyreSet);
    this.mCurrentTyreSet = this.mVehicle.setup.tyreSet;
    this.dilemmaType = RadioMessage.DilemmaType.Tyres;
  }

  public override void OnLoad()
  {
    Game.instance.sessionManager.OnSessionStart -= new Action(this.UpdateTyreSet);
    Game.instance.sessionManager.OnSessionStart += new Action(this.UpdateTyreSet);
  }

  public void UpdateCurrentTyreSet()
  {
    if (this.mCurrentTyreSet == this.mVehicle.setup.tyreSet)
      return;
    this.mTyreSetUsed.Add(this.mCurrentTyreSet);
    this.ResetPlayerInformationFlags();
    this.mHasDonePunctureMessage = false;
    this.mCurrentTyreSet = this.mVehicle.setup.tyreSet;
  }

  private void UpdateTyreSet()
  {
    Game.instance.sessionManager.OnSessionStart -= new Action(this.UpdateTyreSet);
    if (this.mCurrentTyreSet == this.mVehicle.setup.tyreSet)
      return;
    this.mCurrentTyreSet = this.mVehicle.setup.tyreSet;
  }

  protected override void OnSimulationUpdate()
  {
    if (!this.IsVehicleReadyToDisplayMessage())
      return;
    this.CheckTyreWear();
    this.CheckForPuncture();
  }

  private bool IsVehicleReadyToDisplayMessage()
  {
    if (!this.IsChangingTyres() && !this.mVehicle.pathState.IsInPitlaneArea() && (!this.mVehicle.timer.hasSeenChequeredFlag && this.isRaceSession))
      return !this.mVehicle.behaviourManager.isOutOfRace;
    return false;
  }

  private void CheckForPuncture()
  {
    if (this.mHasDonePunctureMessage || !this.mCurrentTyreSet.isPunctured)
      return;
    this.mHasDonePunctureMessage = true;
    DialogQuery inQuery = new DialogQuery();
    inQuery.AddCriteria("Source", "Puncture");
    this.AddPersonCriteria(inQuery, (Person) this.mVehicle.driver);
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(inQuery, false);
    if (this.dialogRule == null)
      return;
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    this.SendRadioMessage();
  }

  protected override bool CheckForDilemmaAndSendToPlayer()
  {
    if (!this.IsVehicleReadyToDisplayMessage() || this.mHasPlayerBeenInformedAlready[0] || (double) this.mCurrentTyreSet.GetCondition() > 0.0)
      return false;
    this.CreateDilemmaDialogQuery();
    this.mHasPlayerBeenInformedAlready[0] = true;
    return true;
  }

  private void CheckTyreWear()
  {
    if (this.mTyreSetUsed.Contains(this.mCurrentTyreSet) || this.mHasPlayerBeenInformedAlready[1] || !this.mCurrentTyreSet.IsInLowPerformanceRange())
      return;
    this.CreateDialogQuery();
    this.mHasPlayerBeenInformedAlready[1] = true;
  }

  private bool IsChangingTyres()
  {
    if (this.mVehicle.strategy.IsGoingToPit())
      return this.mVehicle.setup.IsChangingTyres();
    return false;
  }

  protected void CreateDialogQuery()
  {
    DialogQuery dialogQuery = new DialogQuery();
    dialogQuery.AddCriteria("Source", "TyreWearProblems");
    this.AddTyresCriteria(dialogQuery);
    this.AddPersonCriteria(dialogQuery, (Person) this.mVehicle.driver);
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(dialogQuery, false);
    if (this.dialogRule == null)
      return;
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    this.SendRadioMessage();
  }

  private void AddTyresCriteria(DialogQuery inDialogQuery)
  {
    if ((double) this.mCurrentTyreSet.GetCondition() <= 0.0)
    {
      if (Game.instance.sessionManager.teamRadioManager.CanSendDilemma((RadioMessage) this))
        inDialogQuery.AddCriteria("TyreWear", "Zero");
      else
        inDialogQuery.AddCriteria("TyreWear", "VeryLow");
    }
    else
      inDialogQuery.AddCriteria("TyreWear", "Low");
  }

  private void CreateDilemmaDialogQuery()
  {
    DialogQuery dialogQuery = new DialogQuery();
    dialogQuery.AddCriteria("Source", "TyreWearProblems");
    this.AddTyresCriteria(dialogQuery);
    this.AddPersonCriteria(dialogQuery, (Person) this.mVehicle.driver);
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(dialogQuery, false);
    if (this.dialogRule == null)
      return;
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    this.SendDilemma();
  }
}
