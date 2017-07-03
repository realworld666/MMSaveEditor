// Decompiled with JetBrains decompiler
// Type: RadioMessageCollision
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RadioMessageCollision : RadioMessage
{
  public RadioMessageCollision(RacingVehicle inVehicle, TeamRadio inTeamRadio)
    : base(inVehicle, inTeamRadio)
  {
  }

  public override void OnLoad()
  {
  }

  protected override void OnSimulationUpdate()
  {
  }

  public void SendMessageCollisionVictim(RacingVehicle inCause)
  {
    if (Game.instance.tutorialSystem.isTutorialActiveInCurrentGameState)
      return;
    StringVariableParser.subject = (Person) inCause.driver;
    DialogQuery inQuery = new DialogQuery();
    this.AddPersonCriteria(inQuery, (Person) this.mVehicle.driver);
    if (this.mVehicle.driver.contract.GetTeam() == inCause.driver.contract.GetTeam())
      inQuery.AddCriteria("Source", "CrashedIntoByTeammate");
    else if ((double) RandomUtility.GetRandom01() < 0.5)
      inQuery.AddCriteria("Source", "CrashedIntoByRival");
    else
      inQuery.AddCriteria("Source", "CrashedIntoByRandom");
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(inQuery, false);
    if (this.dialogRule == null)
      return;
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    this.SendRadioMessage();
  }

  public void SendMessageCollisionCause(RacingVehicle inVictim)
  {
    if (Game.instance.tutorialSystem.isTutorialActiveInCurrentGameState)
      return;
    StringVariableParser.subject = (Person) inVictim.driver;
    DialogQuery inQuery = new DialogQuery();
    this.AddPersonCriteria(inQuery, (Person) this.mVehicle.driver);
    if (this.mVehicle.driver.contract.GetTeam() == inVictim.driver.contract.GetTeam())
      inQuery.AddCriteria("Source", "CrashedIntoTeammate");
    else if ((double) RandomUtility.GetRandom01() < 0.5)
      inQuery.AddCriteria("Source", "CrashedIntoRival");
    else
      inQuery.AddCriteria("Source", "CrashedIntoRandom");
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(inQuery, false);
    if (this.dialogRule == null)
      return;
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    this.SendRadioMessage();
  }
}
