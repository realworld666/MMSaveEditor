// Decompiled with JetBrains decompiler
// Type: StrategyPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StrategyPanel : HUDPanel
{
  public UICharacterPortrait[] driverPortraits = new UICharacterPortrait[0];
  public GameObject[] activeStrategyEngineMode = new GameObject[0];
  public GameObject[] activeStrategyDrivingStyle = new GameObject[0];
  private DrivingStyle.Mode mSelectedDrivingStyle = DrivingStyle.Mode.Neutral;
  private Fuel.EngineMode mSelectedEngineMode = Fuel.EngineMode.Medium;
  public GameObject teamOrdersContainer;
  public GameObject teamOrdersDisabledContainer;
  public GameObject strategyContainer;
  public GameObject strategyDisabledContainer;
  public Button teamOrderRaceButton;
  public Button teamOrderAllowTeamMateThroughButton;
  public Toggle attackToggle;
  public Toggle pushToggle;
  public Toggle neutralToggle;
  public Toggle conserveToggle;
  public Toggle backUpToggle;
  public Toggle superOvertakeToggle;
  public Toggle overtakeToggle;
  public Toggle highToggle;
  public Toggle mediumToggle;
  public Toggle lowToggle;
  private bool mUpdateToggles;

  public override HUDPanel.Type type
  {
    get
    {
      return HUDPanel.Type.Strategy;
    }
  }

  private void Awake()
  {
    this.teamOrderRaceButton.onClick.AddListener(new UnityAction(this.OnTeamOrderRaceButton));
    this.teamOrderAllowTeamMateThroughButton.onClick.AddListener(new UnityAction(this.OnTeamOrderAllowTeamMateThroughButton));
  }

  private void OnEnable()
  {
    if (this.vehicle != null)
    {
      scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
      this.mSelectedDrivingStyle = this.vehicle.performance.drivingStyleMode;
      this.mSelectedEngineMode = this.vehicle.performance.fuel.engineMode;
      this.teamOrderRaceButton.interactable = this.vehicle.strategy.teamOrders != SessionStrategy.TeamOrders.Race;
      this.teamOrderAllowTeamMateThroughButton.interactable = this.vehicle.strategy.teamOrders != SessionStrategy.TeamOrders.AllowTeamMateThrough;
      this.UpdateToggles();
      bool inIsActive1 = this.vehicle.driver.personalityTraitController.HasSpecialCase(PersonalityTrait.SpecialCaseType.TurnOffStrategy);
      bool inIsActive2 = this.vehicle.driver.personalityTraitController.HasSpecialCase(PersonalityTrait.SpecialCaseType.TurnOffTeamOrders) || inIsActive1;
      GameUtility.SetActive(this.teamOrdersContainer, Game.instance.sessionManager.sessionType == SessionDetails.SessionType.Race && !inIsActive2);
      GameUtility.SetActive(this.teamOrdersDisabledContainer, inIsActive2);
      GameUtility.SetActive(this.strategyContainer, !inIsActive1);
      GameUtility.SetActive(this.strategyDisabledContainer, inIsActive1);
      if (!inIsActive1 && !inIsActive2)
        return;
      for (int index = 0; index < this.driverPortraits.Length; ++index)
        this.driverPortraits[index].SetPortrait((Person) this.vehicle.driver);
    }
    else
      this.mUpdateToggles = true;
  }

  private void OnDisable()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Back, 0.0f);
  }

  protected override void Update()
  {
    base.Update();
    this.UpdateTraitControlledStrategy();
    if (!this.mUpdateToggles || this.vehicle == null)
      return;
    this.mUpdateToggles = false;
    this.UpdateToggles();
  }

  private void UpdateTraitControlledStrategy()
  {
    if (this.vehicle == null || !this.strategyDisabledContainer.activeSelf)
      return;
    for (int index = 0; index < this.activeStrategyEngineMode.Length; ++index)
      GameUtility.SetActive(this.activeStrategyEngineMode[index], this.vehicle.performance.fuel.engineMode == (Fuel.EngineMode) index);
    for (int index = 0; index < this.activeStrategyDrivingStyle.Length; ++index)
      GameUtility.SetActive(this.activeStrategyDrivingStyle[index], this.vehicle.performance.drivingStyleMode == (DrivingStyle.Mode) index);
  }

  private void UpdateToggles()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.attackToggle.isOn = this.mSelectedDrivingStyle == DrivingStyle.Mode.Attack;
    this.pushToggle.isOn = this.mSelectedDrivingStyle == DrivingStyle.Mode.Push;
    this.neutralToggle.isOn = this.mSelectedDrivingStyle == DrivingStyle.Mode.Neutral;
    this.conserveToggle.isOn = this.mSelectedDrivingStyle == DrivingStyle.Mode.Conserve;
    this.backUpToggle.isOn = this.mSelectedDrivingStyle == DrivingStyle.Mode.BackUp;
    this.superOvertakeToggle.isOn = this.mSelectedEngineMode == Fuel.EngineMode.SuperOvertake;
    this.overtakeToggle.isOn = this.mSelectedEngineMode == Fuel.EngineMode.Overtake;
    this.highToggle.isOn = this.mSelectedEngineMode == Fuel.EngineMode.High;
    this.mediumToggle.isOn = this.mSelectedEngineMode == Fuel.EngineMode.Medium;
    this.lowToggle.isOn = this.mSelectedEngineMode == Fuel.EngineMode.Low;
    GameUtility.SetActive(this.superOvertakeToggle.gameObject, this.vehicle.bonuses.IsBonusActive(MechanicBonus.Trait.SuperOvertakeMode));
  }

  public void OnAttackToggle()
  {
    this.mSelectedDrivingStyle = DrivingStyle.Mode.Attack;
    this.vehicle.performance.drivingStyle.SetDrivingStyle(this.mSelectedDrivingStyle);
    this.UpdateToggles();
  }

  public void OnPushToggle()
  {
    this.mSelectedDrivingStyle = DrivingStyle.Mode.Push;
    this.vehicle.performance.drivingStyle.SetDrivingStyle(this.mSelectedDrivingStyle);
    this.UpdateToggles();
  }

  public void OnNeutralToggle()
  {
    this.mSelectedDrivingStyle = DrivingStyle.Mode.Neutral;
    this.vehicle.performance.drivingStyle.SetDrivingStyle(this.mSelectedDrivingStyle);
    this.UpdateToggles();
  }

  public void OnConserveToggle()
  {
    this.mSelectedDrivingStyle = DrivingStyle.Mode.Conserve;
    this.vehicle.performance.drivingStyle.SetDrivingStyle(this.mSelectedDrivingStyle);
    this.UpdateToggles();
  }

  public void OnBackUpToggle()
  {
    this.mSelectedDrivingStyle = DrivingStyle.Mode.BackUp;
    this.vehicle.performance.drivingStyle.SetDrivingStyle(this.mSelectedDrivingStyle);
    this.UpdateToggles();
  }

  public void OnSuperOvertakeToggle()
  {
    this.mSelectedEngineMode = Fuel.EngineMode.SuperOvertake;
    this.vehicle.performance.fuel.SetEngineMode(this.mSelectedEngineMode, false);
    this.UpdateToggles();
  }

  public void OnOvertakeToggle()
  {
    this.mSelectedEngineMode = Fuel.EngineMode.Overtake;
    this.vehicle.performance.fuel.SetEngineMode(this.mSelectedEngineMode, false);
    this.UpdateToggles();
  }

  public void OnHighToggle()
  {
    this.mSelectedEngineMode = Fuel.EngineMode.High;
    this.vehicle.performance.fuel.SetEngineMode(this.mSelectedEngineMode, false);
    this.UpdateToggles();
  }

  public void OnMediumToggle()
  {
    this.mSelectedEngineMode = Fuel.EngineMode.Medium;
    this.vehicle.performance.fuel.SetEngineMode(this.mSelectedEngineMode, false);
    this.UpdateToggles();
  }

  public void OnLowToggle()
  {
    this.mSelectedEngineMode = Fuel.EngineMode.Low;
    this.vehicle.performance.fuel.SetEngineMode(this.mSelectedEngineMode, false);
    this.UpdateToggles();
  }

  public void OnTeamOrderRaceButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    int index = this.vehicle.carID != 0 ? 0 : 1;
    RacingVehicle playerVehicle = Game.instance.vehicleManager.GetPlayerVehicles()[index];
    this.vehicle.strategy.SetTeamOrders(SessionStrategy.TeamOrders.Race);
    playerVehicle.strategy.SetTeamOrders(SessionStrategy.TeamOrders.Race);
    this.teamOrderRaceButton.interactable = this.vehicle.strategy.teamOrders != SessionStrategy.TeamOrders.Race;
    this.teamOrderAllowTeamMateThroughButton.interactable = this.vehicle.strategy.teamOrders != SessionStrategy.TeamOrders.AllowTeamMateThrough;
  }

  public void OnTeamOrderAllowTeamMateThroughButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    int index = this.vehicle.carID != 0 ? 0 : 1;
    RacingVehicle playerVehicle = Game.instance.vehicleManager.GetPlayerVehicles()[index];
    this.vehicle.strategy.SetTeamOrders(SessionStrategy.TeamOrders.AllowTeamMateThrough);
    playerVehicle.strategy.SetTeamOrders(SessionStrategy.TeamOrders.Race);
    this.teamOrderRaceButton.interactable = this.vehicle.strategy.teamOrders != SessionStrategy.TeamOrders.Race;
    this.teamOrderAllowTeamMateThroughButton.interactable = this.vehicle.strategy.teamOrders != SessionStrategy.TeamOrders.AllowTeamMateThrough;
  }
}
