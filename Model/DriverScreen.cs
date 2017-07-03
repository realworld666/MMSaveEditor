// Decompiled with JetBrains decompiler
// Type: DriverScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class DriverScreen : UIScreen
{
  public UIDriverScreenInfoWidget driverInfoWidget;
  public UIDriverScreenStatsWidget driverStatsWidget;
  public UIChemistryWithMech chemistryWithMech;
  public UIDriverScreenMarketabilityWidget marketabilityWidget;
  public DriverCareerFormWidget careerFormWidget;
  public UIDriverScreenChampionshipFormWidget championshipFormWidget;
  public PersonCareerWidget careerWidget;
  public UIDriverScreenTraitsWidget traitsWidget;
  private Driver mDriver;

  public Driver driver
  {
    get
    {
      return this.mDriver;
    }
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.mDriver = (Driver) this.data;
    this.driverInfoWidget.Setup(this.mDriver);
    this.driverStatsWidget.Setup(this.mDriver);
    this.marketabilityWidget.Setup(this.mDriver);
    this.careerFormWidget.SetDriver(this.mDriver);
    this.careerWidget.Setup((Person) this.mDriver);
    this.chemistryWithMech.SetupForMechanic(this.mDriver, this.mDriver.contract.GetTeam().GetMechanicOfDriver(this.mDriver));
    this.championshipFormWidget.Setup(this.mDriver);
    this.traitsWidget.SetupDriverTraits(this.mDriver);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
  }

  public override void OnExit()
  {
    base.OnExit();
  }
}
