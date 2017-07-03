// Decompiled with JetBrains decompiler
// Type: StaffScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

public class StaffScreen : UIScreen
{
  public UIStaffPersonInfoWidget headDesign;
  public UIStaffPersonInfoWidget headMechanicFirstDriver;
  public UIStaffPersonInfoWidget headMechanicSecondDriver;
  public UIStaffComponentsWidget componentsWidget;
  public UIContractNegotiationsWidget contractNegotiations;

  public override void OnStart()
  {
    base.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.componentsWidget.Setup();
    this.headDesign.Setup(Game.instance.player.team.contractManager.GetPersonOnJob(Contract.Job.EngineerLead));
    List<Person> allPeopleOnJob = Game.instance.player.team.contractManager.GetAllPeopleOnJob(Contract.Job.Mechanic);
    if (allPeopleOnJob.Count >= 2)
    {
      this.headMechanicFirstDriver.Setup(allPeopleOnJob[0]);
      this.headMechanicSecondDriver.Setup(allPeopleOnJob[1]);
    }
    this.contractNegotiations.Setup(new List<Contract.Job>()
    {
      Contract.Job.EngineerLead,
      Contract.Job.Mechanic
    });
    Game.instance.notificationManager.GetNotification("StaffContractConsidered").ResetCount();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
  }
}
