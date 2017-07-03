// Decompiled with JetBrains decompiler
// Type: RadioMessageSafetyCar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RadioMessageSafetyCar : RadioMessage
{
  private float mWaitTimer;

  public RadioMessageSafetyCar(RacingVehicle inVehicle, TeamRadio inTeamRadio)
    : base(inVehicle, inTeamRadio)
  {
    this.dilemmaType = RadioMessage.DilemmaType.SafetyCar;
  }

  public override void OnLoad()
  {
  }

  protected override void OnSimulationUpdate()
  {
  }

  public void OnFlagChange()
  {
    this.mHasPlayerBeenInformedAlready[0] = false;
    this.mWaitTimer = 0.0f;
  }

  public override bool DontDelayForDriverFeedback()
  {
    return true;
  }

  protected override bool CheckForDilemmaAndSendToPlayer()
  {
    if (Game.instance.sessionManager.sessionType == SessionDetails.SessionType.Race && this.IsVehicleReadyToDisplayMessage())
      return this.CheckForSafetyCar();
    return false;
  }

  private bool CheckForSafetyCar()
  {
    if (!this.mHasPlayerBeenInformedAlready[0] && (Game.instance.sessionManager.flag == SessionManager.Flag.SafetyCar || Game.instance.sessionManager.flag == SessionManager.Flag.VirtualSafetyCar))
    {
      this.mWaitTimer += GameTimer.simulationDeltaTime;
      if ((double) this.mWaitTimer > 5.0)
      {
        this.mWaitTimer = 0.0f;
        this.mHasPlayerBeenInformedAlready[0] = true;
        this.CreateDialogQuery();
        return true;
      }
    }
    return false;
  }

  private bool IsVehicleReadyToDisplayMessage()
  {
    if (this.mVehicle.pathController.currentPathType == PathController.PathType.Track && !this.mVehicle.strategy.IsGoingToPit() && (!this.mVehicle.timer.hasSeenChequeredFlag && this.isRaceSession))
      return !this.mVehicle.behaviourManager.isOutOfRace;
    return false;
  }

  private void CreateDialogQuery()
  {
    DialogQuery inQuery = new DialogQuery();
    inQuery.AddCriteria("Source", Game.instance.sessionManager.flag != SessionManager.Flag.SafetyCar ? "VSCDilemma" : "SafetyCarDilemma");
    this.AddPersonCriteria(inQuery, (Person) this.mVehicle.driver);
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(inQuery, false);
    if (this.dialogRule == null)
      return;
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    this.SendDilemma();
  }
}
