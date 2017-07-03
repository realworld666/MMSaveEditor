// Decompiled with JetBrains decompiler
// Type: RadioMessageOvertakes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RadioMessageOvertakes : RadioMessage
{
  private float[] mCooldownTimer = new float[3];

  public RadioMessageOvertakes(RacingVehicle inVehicle, TeamRadio inTeamRadio)
    : base(inVehicle, inTeamRadio)
  {
  }

  public override void OnLoad()
  {
  }

  public override bool DontDelayForDriverFeedback()
  {
    return true;
  }

  protected override void OnSimulationUpdate()
  {
    if (this.mVehicle.timer.hasSeenChequeredFlag)
      return;
    for (int index = 0; index < this.mCooldownTimer.Length; ++index)
    {
      this.mCooldownTimer[index] -= GameTimer.simulationDeltaTime;
      if ((double) this.mCooldownTimer[index] < 0.0)
        this.mHasPlayerBeenInformedAlready[index] = false;
    }
  }

  public void SendOvertakeMessage(RacingVehicle inVehicleWePassed)
  {
    if (Game.instance.tutorialSystem.isTutorialActiveInCurrentGameState || inVehicleWePassed.pathState.IsInPitlaneArea() || (this.mVehicle.pathState.IsInPitlaneArea() || this.mVehicle.standingsPosition >= this.mVehicle.previousStandingsPosition))
      return;
    bool flag = false;
    if (!this.mHasPlayerBeenInformedAlready[0] && this.mVehicle.standingsPosition == 1 && (double) Game.instance.sessionManager.GetNormalizedSessionTime() > 0.5)
    {
      flag = true;
      this.mHasPlayerBeenInformedAlready[0] = true;
      this.mCooldownTimer[0] = GameUtility.MinutesToSeconds(2f);
    }
    else if (!this.mHasPlayerBeenInformedAlready[1])
    {
      flag = true;
      this.mHasPlayerBeenInformedAlready[1] = true;
      this.mCooldownTimer[1] = GameUtility.MinutesToSeconds(10f);
    }
    if (!flag)
      return;
    StringVariableParser.subject = (Person) inVehicleWePassed.driver;
    DialogQuery inQuery = new DialogQuery();
    this.AddPersonCriteria(inQuery, (Person) this.mVehicle.driver);
    if (this.mVehicle.driver.contract.GetTeam() == inVehicleWePassed.driver.contract.GetTeam())
      inQuery.AddCriteria("Source", "OvertookTeammate");
    else
      inQuery.AddCriteria("Source", "OvertookRival");
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(inQuery, false);
    if (this.dialogRule == null)
      return;
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    this.SendRadioMessage();
  }

  public void SendOvertakanMessage(RacingVehicle inVehicleThatPassedUs)
  {
    if (Game.instance.tutorialSystem.isTutorialActiveInCurrentGameState || this.mVehicle.performance.IsExperiencingCriticalIssue() || (inVehicleThatPassedUs.pathState.IsInPitlaneArea() || this.mVehicle.pathState.IsInPitlaneArea()) || (this.mVehicle.standingsPosition <= this.mVehicle.previousStandingsPosition || this.mHasPlayerBeenInformedAlready[2]))
      return;
    this.mHasPlayerBeenInformedAlready[2] = true;
    this.mCooldownTimer[2] = GameUtility.MinutesToSeconds(10f);
    StringVariableParser.subject = (Person) inVehicleThatPassedUs.driver;
    DialogQuery inQuery = new DialogQuery();
    this.AddPersonCriteria(inQuery, (Person) this.mVehicle.driver);
    inQuery.AddCriteria("Source", "OvertakenByRival");
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(inQuery, false);
    if (this.dialogRule == null)
      return;
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    this.SendRadioMessage();
  }
}
