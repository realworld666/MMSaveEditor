// Decompiled with JetBrains decompiler
// Type: RadioMessageTyreTemperature
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RadioMessageTyreTemperature : RadioMessage
{
  public RadioMessageTyreTemperature(RacingVehicle inVehicle, TeamRadio inTeamRadio)
    : base(inVehicle, inTeamRadio)
  {
  }

  public override void OnLoad()
  {
  }

  protected override void OnSimulationUpdate()
  {
    if (this.mVehicle.timer.hasSeenChequeredFlag || this.mVehicle.pathState.IsInPitlaneArea() || !this.isRaceSession)
      return;
    float temperature = this.mVehicle.setup.tyreSet.GetTemperature();
    if ((double) temperature <= 0.0500000007450581 && !this.mHasPlayerBeenInformedAlready[0])
    {
      this.CreateDialogRule();
      this.mHasPlayerBeenInformedAlready[0] = true;
    }
    else if ((double) temperature > 0.200000002980232)
      this.mHasPlayerBeenInformedAlready[0] = false;
    if ((double) temperature >= 0.949999988079071 && !this.mHasPlayerBeenInformedAlready[1])
    {
      this.CreateDialogRule();
      this.mHasPlayerBeenInformedAlready[1] = true;
    }
    else
    {
      if ((double) temperature >= 0.800000011920929)
        return;
      this.mHasPlayerBeenInformedAlready[1] = false;
    }
  }

  private void CreateDialogRule()
  {
    DialogQuery inQuery = new DialogQuery();
    inQuery.AddCriteria("Source", "TyreTempProblems");
    this.AddPersonCriteria(inQuery, (Person) this.mVehicle.driver);
    float temperature = this.mVehicle.setup.tyreSet.GetTemperature();
    if ((double) temperature < 0.5)
      inQuery.AddCriteria("TyreTemp", "Low");
    else if ((double) temperature > 0.5)
      inQuery.AddCriteria("TyreTemp", "High");
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(inQuery, false);
    if (this.dialogRule == null)
      return;
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    this.SendRadioMessage();
  }
}
