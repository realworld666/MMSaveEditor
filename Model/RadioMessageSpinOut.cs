// Decompiled with JetBrains decompiler
// Type: RadioMessageSpinOut
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RadioMessageSpinOut : RadioMessage
{
  public RadioMessageSpinOut(RacingVehicle inVehicle, TeamRadio inTeamRadio)
    : base(inVehicle, inTeamRadio)
  {
    inVehicle.behaviourManager.GetBehaviour<AISpinBehaviour>().OnSpinMessage += new Action(this.CreateDialogQuery);
  }

  public override void OnLoad()
  {
    this.mVehicle.behaviourManager.GetBehaviour<AISpinBehaviour>().OnSpinMessage -= new Action(this.CreateDialogQuery);
    this.mVehicle.behaviourManager.GetBehaviour<AISpinBehaviour>().OnSpinMessage += new Action(this.CreateDialogQuery);
  }

  protected override void OnSimulationUpdate()
  {
  }

  public void CreateDialogQuery()
  {
    if (this.mVehicle == null || Game.instance.tutorialSystem.isTutorialActiveInCurrentGameState || !this.mVehicle.timer.hasSeenChequeredFlag)
      return;
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    StringVariableParser.SetStaticData(this.personWhoSpeaks);
    DialogQuery inQuery = new DialogQuery();
    this.AddPersonCriteria(inQuery, (Person) this.mVehicle.driver);
    inQuery.AddCriteria("Source", "Spin");
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(inQuery, false);
    if (this.dialogRule == null)
      return;
    this.SendRadioMessage();
  }
}
