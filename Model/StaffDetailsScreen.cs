// Decompiled with JetBrains decompiler
// Type: StaffDetailsScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

public class StaffDetailsScreen : UIScreen
{
  public UIStaffDetailsScreenPersonInfoWidget personInfoWidget;
  public UIStaffDetailsScreenPersonStatsWidget statsWidget;
  public UIStaffDetailsScreenEngineerComponentsWidget engineerComponentsWidget;
  public PersonCareerWidget careerWidget;
  public UIStaffScreenDriverWidget driverWidget;
  private Person mPerson;

  public Person person
  {
    get
    {
      return this.mPerson;
    }
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.mPerson = (Person) this.data;
    this.SetStats(this.mPerson);
    this.careerWidget.Setup(this.mPerson);
    this.personInfoWidget.Setup(this.mPerson);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
  }

  private void SetStats(Person inPerson)
  {
    Dictionary<string, float> stats = PersonStats.GetStats(inPerson, Game.instance.player.team.championship.series);
    GameUtility.SetActive(this.driverWidget.gameObject, inPerson is Mechanic);
    if (this.driverWidget.gameObject.activeSelf)
      this.driverWidget.SetupForDriver((Mechanic) inPerson);
    GameUtility.SetActive(this.engineerComponentsWidget.gameObject, inPerson is Engineer);
    if (this.engineerComponentsWidget.gameObject.activeSelf)
      this.engineerComponentsWidget.Setup((Engineer) inPerson);
    this.statsWidget.Setup(inPerson, stats);
  }

  public override void OnExit()
  {
    base.OnExit();
  }
}
