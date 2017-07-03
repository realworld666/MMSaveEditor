// Decompiled with JetBrains decompiler
// Type: RadialRaceMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

public class RadialRaceMenu : MonoBehaviour
{
  private DrivingStyle.Mode mSelectedDrivingStyle = DrivingStyle.Mode.Neutral;
  private Fuel.EngineMode mSelectedEngineMode = Fuel.EngineMode.Medium;
  private RacingVehicle mVehicle;
  public Button actionButton;
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
  private bool m_ButtonDown;

  private void Show(RacingVehicle target_vehicle)
  {
    if (target_vehicle == null)
      return;
    this.mVehicle = target_vehicle;
    this.mSelectedDrivingStyle = this.mVehicle.performance.drivingStyleMode;
    this.mSelectedEngineMode = this.mVehicle.performance.fuel.engineMode;
    this.UpdateToggles();
    GameUtility.SetActive(this.gameObject, true);
  }

  private void Hide()
  {
    GameUtility.SetActive(this.gameObject, false);
  }

  public void OnEnter()
  {
    GamepadInputManager.onToggleRadialMenu += new GamepadInputManager.ToggleRadialMenu(this.OnToggle);
    this.Hide();
  }

  public void OnExit()
  {
    GamepadInputManager.onToggleRadialMenu -= new GamepadInputManager.ToggleRadialMenu(this.OnToggle);
    this.Hide();
  }

  private void OnToggle(bool show, RacingVehicle racing_vehicle)
  {
    if (show && racing_vehicle != null)
      this.Show(racing_vehicle);
    else
      this.Hide();
  }

  private void LateUpdate()
  {
    float axis1 = GamePad.GetAxis(CAxis.LX, PlayerIndex.One);
    float axis2 = GamePad.GetAxis(CAxis.LY, PlayerIndex.One);
    float rightStickDeadZone = GamepadInputManager.instance.GetRightStickDeadZone();
    bool flag1 = false;
    if (GamePad.GetButton(CButton.A, PlayerIndex.One) && !this.m_ButtonDown)
      this.m_ButtonDown = true;
    else if (this.m_ButtonDown && !GamePad.GetButton(CButton.A, PlayerIndex.One))
    {
      this.m_ButtonDown = false;
      flag1 = true;
    }
    bool flag2 = false;
    if ((double) axis1 < -(double) rightStickDeadZone || (double) axis1 > (double) rightStickDeadZone || ((double) axis2 < -(double) rightStickDeadZone || (double) axis2 > (double) rightStickDeadZone))
    {
      int num = Mathf.RoundToInt(Vector2.Angle(new Vector2(0.0f, -1f), new Vector2(axis1, axis2)) / 24f);
      if ((double) axis1 < 0.0)
      {
        if (num == 0)
        {
          this.attackToggle.Select();
          if (flag1)
            this.OnDrivingStyleToggle(DrivingStyle.Mode.Attack);
        }
        else if (num == 1)
        {
          this.pushToggle.Select();
          if (flag1)
            this.OnDrivingStyleToggle(DrivingStyle.Mode.Push);
        }
        else if (num == 2)
        {
          this.neutralToggle.Select();
          if (flag1)
            this.OnDrivingStyleToggle(DrivingStyle.Mode.Neutral);
        }
        else if (num == 3)
        {
          this.conserveToggle.Select();
          if (flag1)
            this.OnDrivingStyleToggle(DrivingStyle.Mode.Conserve);
        }
        else if (num == 4)
        {
          this.backUpToggle.Select();
          if (flag1)
            this.OnDrivingStyleToggle(DrivingStyle.Mode.BackUp);
        }
        else
          flag2 = true;
      }
      else if (num == 0 && this.mVehicle.bonuses.IsBonusActive(MechanicBonus.Trait.SuperOvertakeMode))
      {
        this.superOvertakeToggle.Select();
        if (flag1)
          this.OnEngineModeToggle(Fuel.EngineMode.SuperOvertake);
      }
      else if (num == 1)
      {
        this.overtakeToggle.Select();
        if (flag1)
          this.OnEngineModeToggle(Fuel.EngineMode.Overtake);
      }
      else if (num == 2)
      {
        this.highToggle.Select();
        if (flag1)
          this.OnEngineModeToggle(Fuel.EngineMode.High);
      }
      else if (num == 3)
      {
        this.mediumToggle.Select();
        if (flag1)
          this.OnEngineModeToggle(Fuel.EngineMode.Medium);
      }
      else if (num == 4)
      {
        this.lowToggle.Select();
        if (flag1)
          this.OnEngineModeToggle(Fuel.EngineMode.Low);
      }
      else
        flag2 = true;
    }
    else
      flag2 = true;
    if (!flag2)
      return;
    this.actionButton.Select();
    if (!flag1)
      return;
    this.OnPitButton();
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
    GameUtility.SetActive(this.superOvertakeToggle.gameObject, this.mVehicle.bonuses.IsBonusActive(MechanicBonus.Trait.SuperOvertakeMode));
  }

  public void OnDrivingStyleToggle(DrivingStyle.Mode mode)
  {
    this.mSelectedDrivingStyle = mode;
    this.mVehicle.performance.drivingStyle.SetDrivingStyle(this.mSelectedDrivingStyle);
    this.UpdateToggles();
  }

  public void OnEngineModeToggle(Fuel.EngineMode mode)
  {
    this.mSelectedEngineMode = mode;
    this.mVehicle.performance.fuel.SetEngineMode(this.mSelectedEngineMode, false);
    this.UpdateToggles();
  }

  public void OnPitButton()
  {
    UIManager.instance.GetScreen<PitScreen>().Setup(this.mVehicle, PitScreen.Mode.Pitting);
    UIManager.instance.ClearNavigationStacks();
    UIManager.instance.ChangeScreen("PitScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }
}
