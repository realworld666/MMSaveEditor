// Decompiled with JetBrains decompiler
// Type: RadioMessageCrash
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RadioMessageCrash : RadioMessage
{
  public RadioMessageCrash(RacingVehicle inVehicle, TeamRadio inTeamRadio)
    : base(inVehicle, inTeamRadio)
  {
  }

  public override void OnLoad()
  {
  }

  protected override void OnSimulationUpdate()
  {
    if (!this.mVehicle.timer.hasSeenChequeredFlag && this.mVehicle.behaviourManager.currentBehaviour is AICrashingBehaviour)
    {
      if (this.mHasPlayerBeenInformedAlready[0] || !((AICrashingBehaviour) this.mVehicle.behaviourManager.currentBehaviour).isOutOfTrack || !this.mVehicle.HasStopped())
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
    inQuery.AddCriteria("Source", "Crash");
    this.AddPersonCriteria(inQuery, (Person) this.mVehicle.driver);
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(inQuery, false);
    if (this.dialogRule == null)
      return;
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    this.SendRadioMessage();
  }
}
