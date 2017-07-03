// Decompiled with JetBrains decompiler
// Type: UIHUBStrategyEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHUBStrategyEntry : MonoBehaviour
{
  private DrivingStyle.Mode mSelectedDrivingStyle = DrivingStyle.Mode.Neutral;
  private Fuel.EngineMode mSelectedEngineMode = Fuel.EngineMode.Medium;
  public Button drivingStyleButton;
  public Button engineModesButton;
  public Transform drivingStyleBar;
  public Transform engineModesBar;
  public TextMeshProUGUI drivingStyle;
  public TextMeshProUGUI engineModes;
  public UIHUBStrategyDropdown drivingStyleDropdown;
  public UIHUBStrategyDropdown engineModesDropdown;
  public GameObject superOvertakeMode;
  public UIHUBSelection widget;
  public GameObject goneRogueTraitContainer;
  public GameObject[] dropdownsContainer;
  private RacingVehicle mVehicle;
  private UIHUBStrategyToggle[] mEntries;

  public void OnStart()
  {
    this.drivingStyleButton.onClick.AddListener(new UnityAction(this.OnDrivingStyleButton));
    this.engineModesButton.onClick.AddListener(new UnityAction(this.OnEngineModesButton));
    this.mEntries = this.transform.GetComponentsInChildren<UIHUBStrategyToggle>(true);
    for (int index = 0; index < this.mEntries.Length; ++index)
    {
      this.mEntries[index].toggle.isOn = false;
      this.mEntries[index].OnStart();
    }
  }

  public void Setup(RacingVehicle inVehicle)
  {
    if (inVehicle == null)
      return;
    this.mVehicle = inVehicle;
    bool inIsActive = this.mVehicle.driver.personalityTraitController.HasSpecialCase(PersonalityTrait.SpecialCaseType.TurnOffStrategy);
    GameUtility.SetActive(this.goneRogueTraitContainer, inIsActive);
    for (int index = 0; index < this.dropdownsContainer.Length; ++index)
      GameUtility.SetActive(this.dropdownsContainer[index], !inIsActive);
    this.mSelectedDrivingStyle = this.mVehicle.performance.drivingStyleMode;
    this.mSelectedEngineMode = this.mVehicle.performance.fuel.engineMode;
    this.SetEngineMode();
    this.SetDrivingStyle();
  }

  public void OnShow()
  {
    if (this.mVehicle == null)
      return;
    GameUtility.SetActive(this.superOvertakeMode, this.mVehicle.bonuses.IsBonusActive(MechanicBonus.Trait.SuperOvertakeMode));
    if (this.mVehicle.performance.fuel.engineMode != Fuel.EngineMode.SuperOvertake || this.mVehicle.bonuses.IsBonusActive(MechanicBonus.Trait.SuperOvertakeMode))
      return;
    this.mSelectedEngineMode = Fuel.EngineMode.Overtake;
    this.SetEngineMode();
    UIHUBStep step = this.widget.GetStep(UIHUBStep.Step.Strategy);
    step.SetSeen(false);
    step.SetComplete(false);
  }

  public void SetEngineMode()
  {
    for (int index = 0; index < this.mEntries.Length; ++index)
    {
      UIHUBStrategyToggle mEntry = this.mEntries[index];
      if (mEntry.type == UIHUBStrategyToggle.Type.EngineModes)
      {
        mEntry.toggle.isOn = mEntry.engineMode == this.mSelectedEngineMode;
        if (mEntry.toggle.isOn)
          mEntry.OnToggle();
      }
    }
  }

  public void SetDrivingStyle()
  {
    for (int index = 0; index < this.mEntries.Length; ++index)
    {
      UIHUBStrategyToggle mEntry = this.mEntries[index];
      if (mEntry.type == UIHUBStrategyToggle.Type.DrivingStyle)
      {
        mEntry.toggle.isOn = mEntry.drivingStyle == this.mSelectedDrivingStyle;
        if (mEntry.toggle.isOn)
          mEntry.OnToggle();
      }
    }
  }

  public void SelectDrivingStyle(DrivingStyle.Mode inDrivingStyle, Image inImage)
  {
    this.mSelectedDrivingStyle = inDrivingStyle;
    this.mVehicle.performance.drivingStyle.SetDrivingStyle(this.mSelectedDrivingStyle);
    for (int index = 0; index < this.drivingStyleBar.childCount; ++index)
      GameUtility.SetActive(this.drivingStyleBar.GetChild(index).gameObject, (DrivingStyle.Mode) index == this.mVehicle.performance.drivingStyleMode);
    this.drivingStyle.text = Localisation.LocaliseEnum((Enum) this.mSelectedDrivingStyle);
    GameUtility.SetActive(this.drivingStyleDropdown.gameObject, false);
  }

  public void SelectEngineModes(Fuel.EngineMode inEngineMode, Image inImage)
  {
    this.mSelectedEngineMode = inEngineMode;
    this.mVehicle.performance.fuel.SetEngineMode(this.mSelectedEngineMode, false);
    for (int index = 0; index < this.engineModesBar.childCount; ++index)
      GameUtility.SetActive(this.engineModesBar.GetChild(index).gameObject, (Fuel.EngineMode) index == this.mVehicle.performance.fuel.engineMode);
    this.engineModes.text = Localisation.LocaliseEnum((Enum) this.mSelectedEngineMode);
    GameUtility.SetActive(this.engineModesDropdown.gameObject, false);
  }

  private void OnDrivingStyleButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    GameUtility.SetActive(this.drivingStyleDropdown.gameObject, !this.drivingStyleDropdown.gameObject.activeSelf);
  }

  private void OnEngineModesButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    GameUtility.SetActive(this.engineModesDropdown.gameObject, !this.engineModesDropdown.gameObject.activeSelf);
  }
}
