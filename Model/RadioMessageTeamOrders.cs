// Decompiled with JetBrains decompiler
// Type: RadioMessageTeamOrders
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RadioMessageTeamOrders : RadioMessage
{
  private float mCooldownTimer;

  public RadioMessageTeamOrders(RacingVehicle inVehicle, TeamRadio inTeamRadio)
    : base(inVehicle, inTeamRadio)
  {
  }

  public override void OnLoad()
  {
  }

  protected override void OnSimulationUpdate()
  {
    if (this.mVehicle.timer.hasSeenChequeredFlag || !this.mHasPlayerBeenInformedAlready[0])
      return;
    this.mCooldownTimer -= GameTimer.simulationDeltaTime;
    if ((double) this.mCooldownTimer >= 0.0)
      return;
    this.mHasPlayerBeenInformedAlready[0] = false;
  }

  public void SendLettingTeamMateThroughMessage()
  {
    if (this.mHasPlayerBeenInformedAlready[0] || Game.instance.tutorialSystem.isTutorialActiveInCurrentGameState || Game.instance.sessionManager.GetLapsRemaining() == 0)
      return;
    this.mHasPlayerBeenInformedAlready[0] = true;
    this.mCooldownTimer = GameUtility.MinutesToSeconds(2f);
    DialogQuery inQuery = new DialogQuery();
    this.AddPersonCriteria(inQuery, (Person) this.mVehicle.driver);
    int index = this.mVehicle.carID != 0 ? 0 : 1;
    StringVariableParser.subject = (Person) Game.instance.vehicleManager.GetPlayerVehicles()[index].driver;
    inQuery.AddCriteria("Source", "TeamOrdersLetTeammateThrough");
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(inQuery, false);
    if (this.dialogRule == null)
      return;
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    this.SendRadioMessage();
  }

  public void SendRefusalOfTeamOrdersMessage()
  {
    if (Game.instance.tutorialSystem.isTutorialActiveInCurrentGameState)
      return;
    DialogQuery inQuery = new DialogQuery();
    this.AddPersonCriteria(inQuery, (Person) this.mVehicle.driver);
    inQuery.AddCriteria("Source", "TeamOrdersRefused");
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(inQuery, false);
    if (this.dialogRule == null)
      return;
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    this.SendRadioMessage();
  }
}
