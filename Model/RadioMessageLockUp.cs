// Decompiled with JetBrains decompiler
// Type: RadioMessageLockUp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RadioMessageLockUp : RadioMessage
{
  public RadioMessageLockUp(RacingVehicle inVehicle, TeamRadio inTeamRadio)
    : base(inVehicle, inTeamRadio)
  {
  }

  public override void OnLoad()
  {
  }

  protected override void OnSimulationUpdate()
  {
    if (!this.mVehicle.timer.hasSeenChequeredFlag && this.mVehicle.behaviourManager.currentBehaviour is AILockUpBehaviour)
    {
      if (this.mHasPlayerBeenInformedAlready[0])
        return;
      this.CreateDialogRule();
      this.mHasPlayerBeenInformedAlready[0] = true;
    }
    else
      this.mHasPlayerBeenInformedAlready[0] = false;
  }

  private void CreateDialogRule()
  {
    DialogQuery inQuery = new DialogQuery();
    inQuery.AddCriteria("Source", "LockedUp");
    this.AddPersonCriteria(inQuery, (Person) this.mVehicle.driver);
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(inQuery, false);
    if (this.dialogRule == null)
      return;
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    this.SendRadioMessage();
  }
}
