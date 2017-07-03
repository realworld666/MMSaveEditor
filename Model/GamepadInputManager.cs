// Decompiled with JetBrains decompiler
// Type: GamepadInputManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

public class GamepadInputManager : MonoBehaviour
{
  public float m_Speed = 5f;
  public float m_RightStickDeadZone = 0.1f;
  public float m_SecondsToSlowFor = 1f;
  public float m_SecondsToLerpToFullSpeed = 0.5f;
  public Vector2 m_LeftAnchor = new Vector2(0.1f, 0.1f);
  public Vector2 m_RightAnchor = new Vector2(0.85f, 0.1f);
  public Vector2 m_TopAnchor = new Vector2(0.5f, 0.9f);
  public Vector2 m_BottomAnchor = new Vector2(0.5f, 0.1f);
  private float m_PowerCurverPower = 2f;
  private bool[] m_IsButtonDown = new bool[10];
  private bool[] m_IsDpadButtonDown = new bool[4];
  private RacingVehicle[] m_RacingVehicles = new RacingVehicle[2];
  private CButton m_BumperHeld = CButton.Back;
  private string[] m_FrontendScreens = new string[13]
  {
    "HomeScreen",
    "PlayerScreen",
    "MailScreen",
    "CarScreen",
    "HeadquartersScreen",
    "TeamScreen",
    "AllDriversScreen",
    "StaffScreen",
    "ScoutingScreen",
    "FinanceScreen",
    "SponsorsScreen",
    "StandingsScreen",
    "EventCalendarScreen"
  };
  private const int NUM_BUTTONS = 10;
  public float m_LeftStickDeadZone;
  private CursorManager m_CursorManager;
  private Selectable m_ObjectOver;
  private float m_TimeToStopBeingSlow;
  private bool m_IsDialogBoxOpen;
  private bool m_HasFocus;
  private bool m_IsOverSelectableObject;
  private float m_TimeBumperToOpen;
  private GamepadInputManager.ERadialMenu m_RadialState;
  private static GamepadInputManager sInstance;

  public static GamepadInputManager instance
  {
    get
    {
      return GamepadInputManager.sInstance;
    }
  }

  public static bool InstanceExists
  {
    get
    {
      return (UnityEngine.Object) GamepadInputManager.sInstance != (UnityEngine.Object) null;
    }
  }

  public static event GamepadInputManager.RightStickInput onRightStickInput;

  public static event GamepadInputManager.PauseButtonInput onPauseButtonInput;

  public static event GamepadInputManager.DialogBoxInput onDialogBoxInput;

  public static event GamepadInputManager.ToggleRadialMenu onToggleRadialMenu;

  private void Awake()
  {
    if ((UnityEngine.Object) GamepadInputManager.sInstance == (UnityEngine.Object) null)
    {
      GamepadInputManager.sInstance = this;
      for (int index = 0; index < 10; ++index)
        this.m_IsButtonDown[index] = false;
      this.m_IsDpadButtonDown[0] = false;
      this.m_IsDpadButtonDown[1] = false;
      this.m_IsDpadButtonDown[2] = false;
      this.m_IsDpadButtonDown[3] = false;
    }
    else
      Debug.LogError((object) "Trying to create a second singleton", (UnityEngine.Object) null);
  }

  private void OnApplicationFocus(bool focus)
  {
    this.m_HasFocus = focus;
  }

  private void LateUpdate()
  {
    if (!App.instance.preferencesManager.GetSettingBool(Preference.pName.Game_Gamepad, false) || !this.m_HasFocus || this.UpdateRacingVehicleStuff())
      return;
    Vector2 globalMousePosition = ProMouse.Instance.GetGlobalMousePosition();
    int num1 = Mathf.RoundToInt(this.PowerCurve(GamePad.GetAxis(CAxis.LX, PlayerIndex.One)) * this.m_Speed);
    int num2 = Mathf.RoundToInt(this.PowerCurve(GamePad.GetAxis(CAxis.LY, PlayerIndex.One)) * -this.m_Speed);
    bool flag = this.IsOverASelectableObject();
    if (!flag || num1 == 0 && num2 == 0)
      this.m_IsOverSelectableObject = false;
    else if (flag && !this.m_IsOverSelectableObject)
    {
      this.m_TimeToStopBeingSlow = Time.time + this.m_SecondsToSlowFor;
      this.m_IsOverSelectableObject = true;
    }
    if (this.m_IsOverSelectableObject)
    {
      float t = 0.0f;
      if ((double) Time.time > (double) this.m_TimeToStopBeingSlow)
        t = (Time.time - this.m_TimeToStopBeingSlow) / this.m_SecondsToLerpToFullSpeed;
      if (num1 < -1 || num1 > 1)
      {
        float b = (float) num1;
        num1 = Mathf.RoundToInt(Mathf.Lerp(b / 2f, b, t));
      }
      if (num2 < -1 || num2 > 1)
      {
        float b = (float) num2;
        num2 = Mathf.RoundToInt(Mathf.Lerp(b / 2f, b, t));
      }
    }
    if (num1 != 0 || num2 != 0)
    {
      globalMousePosition.x += (float) num1;
      globalMousePosition.y += (float) num2;
      ProMouse.Instance.SetGlobalCursorPosition((int) globalMousePosition.x, (int) globalMousePosition.y);
    }
    if (GamepadInputManager.onRightStickInput != null)
    {
      float axis1 = GamePad.GetAxis(CAxis.RX, PlayerIndex.One);
      float axis2 = GamePad.GetAxis(CAxis.RY, PlayerIndex.One);
      if ((double) axis1 < -(double) this.m_RightStickDeadZone || (double) axis1 > (double) this.m_RightStickDeadZone || ((double) axis2 < -(double) this.m_RightStickDeadZone || (double) axis2 > (double) this.m_RightStickDeadZone))
        GamepadInputManager.onRightStickInput(axis1, axis2);
    }
    if (GamepadInputManager.onDialogBoxInput == null)
    {
      this.m_IsDialogBoxOpen = false;
      if (this.HasButtonBeenReleased(CButton.X))
        UIManager.instance.navigationBars.bottomBar.PressActionButton();
      if (this.HasButtonBeenReleased(CButton.Start) && GamepadInputManager.onPauseButtonInput != null)
        GamepadInputManager.onPauseButtonInput();
      if (this.HasButtonBeenReleased(CButton.RB))
        this.NavigateRight();
      if (this.HasButtonBeenReleased(CButton.LB))
        this.NavigateLeft();
      if (this.HasButtonBeenReleased(CButton.B) && !UIManager.instance.blur.isActive)
      {
        UIManager.instance.OnBackButton();
        UIManager.instance.dialogBoxManager.HideAll();
      }
      Vector2 dpad = GamePad.GetDPad(PlayerIndex.One);
      if (this.HasDpadBeenReleased(GamepadInputManager.EDpadDirection.Right, ref dpad))
        this.MoveCursorToAnchor(ref this.m_RightAnchor);
      else if (this.HasDpadBeenReleased(GamepadInputManager.EDpadDirection.Left, ref dpad))
        this.MoveCursorToAnchor(ref this.m_LeftAnchor);
      else if (this.HasDpadBeenReleased(GamepadInputManager.EDpadDirection.Up, ref dpad))
      {
        this.MoveCursorToAnchor(ref this.m_TopAnchor);
      }
      else
      {
        if (!this.HasDpadBeenReleased(GamepadInputManager.EDpadDirection.Down, ref dpad))
          return;
        this.MoveCursorToAnchor(ref this.m_BottomAnchor);
      }
    }
    else
    {
      if (this.m_IsDialogBoxOpen)
      {
        if (this.HasButtonBeenReleased(CButton.A))
          GamepadInputManager.onDialogBoxInput(GamepadInputManager.EButtonAction.Confirm);
        if (this.HasButtonBeenReleased(CButton.B))
          GamepadInputManager.onDialogBoxInput(GamepadInputManager.EButtonAction.Cancel);
        if (this.HasButtonBeenReleased(CButton.Y))
          GamepadInputManager.onDialogBoxInput(GamepadInputManager.EButtonAction.SkipSession);
      }
      this.m_IsDialogBoxOpen = true;
    }
  }

  private bool HasButtonBeenReleased(CButton button)
  {
    int index = (int) button;
    if (GamePad.GetButton(button, PlayerIndex.One) && !this.m_IsButtonDown[index])
      this.m_IsButtonDown[index] = true;
    else if (this.m_IsButtonDown[index] && !GamePad.GetButton(button, PlayerIndex.One))
    {
      this.m_IsButtonDown[index] = false;
      Debug.Log((object) ("<color=yellow>Frame: " + (object) Time.frameCount + " " + ((PSButton) button).ToString() + " pressed</color>"), (UnityEngine.Object) null);
      return true;
    }
    return false;
  }

  private bool HasDpadBeenReleased(GamepadInputManager.EDpadDirection direction, ref Vector2 dpad)
  {
    int index = (int) direction;
    if (!this.m_IsDpadButtonDown[index])
    {
      if (direction == GamepadInputManager.EDpadDirection.Up && (double) dpad.y < 0.0 || direction == GamepadInputManager.EDpadDirection.Down && (double) dpad.y > 0.0 || (direction == GamepadInputManager.EDpadDirection.Left && (double) dpad.x < 0.0 || direction == GamepadInputManager.EDpadDirection.Right && (double) dpad.x > 0.0))
        this.m_IsDpadButtonDown[index] = true;
    }
    else if (this.m_IsDpadButtonDown[index] && (index < 2 && (double) dpad.y == 0.0 || index > 1 && (double) dpad.x == 0.0))
    {
      this.m_IsDpadButtonDown[index] = false;
      Debug.Log((object) ("<color=yellow>Frame: " + (object) Time.frameCount + " " + direction.ToString() + " pressed</color>"), (UnityEngine.Object) null);
      return true;
    }
    return false;
  }

  private bool IsOverASelectableObject()
  {
    if (this.m_CursorManager == null)
      this.m_CursorManager = UIManager.instance.cursorManager;
    this.m_ObjectOver = this.m_CursorManager.FindInteractableObject(UIManager.instance.UIObjectsAtMousePosition);
    return (UnityEngine.Object) this.m_ObjectOver != (UnityEngine.Object) null;
  }

  private float PowerCurve(float value)
  {
    return Mathf.Pow(Mathf.Abs(value), this.m_PowerCurverPower) * Mathf.Sign(value);
  }

  private void MoveCursorToAnchor(ref Vector2 anchor)
  {
    ProMouse.Instance.SetCursorPosition(Mathf.RoundToInt((float) Screen.width * anchor.x), Mathf.RoundToInt((float) Screen.height * anchor.y));
  }

  private void NavigateRight()
  {
    string currentScreenName = UIManager.instance.currentScreen_name;
    for (int index = 0; index < this.m_FrontendScreens.Length; ++index)
    {
      if (currentScreenName.Equals(this.m_FrontendScreens[index], StringComparison.Ordinal))
      {
        if (index == this.m_FrontendScreens.Length - 1)
        {
          UIManager.instance.GamepadQuickNavigation(this.m_FrontendScreens[0]);
          break;
        }
        UIManager.instance.GamepadQuickNavigation(this.m_FrontendScreens[index + 1]);
        break;
      }
    }
  }

  private void NavigateLeft()
  {
    string currentScreenName = UIManager.instance.currentScreen_name;
    for (int index = 0; index < this.m_FrontendScreens.Length; ++index)
    {
      if (currentScreenName.Equals(this.m_FrontendScreens[index], StringComparison.Ordinal))
      {
        if (index == 0)
        {
          UIManager.instance.GamepadQuickNavigation(this.m_FrontendScreens[this.m_FrontendScreens.Length - 1]);
          break;
        }
        UIManager.instance.GamepadQuickNavigation(this.m_FrontendScreens[index - 1]);
        break;
      }
    }
  }

  public float GetRightStickDeadZone()
  {
    return this.m_RightStickDeadZone;
  }

  public void RegisterRacingVehicle(RacingVehicle racing_vehicle, int driver_index)
  {
    this.m_RacingVehicles[driver_index] = racing_vehicle;
  }

  private bool UpdateRacingVehicleStuff()
  {
    if (GamepadInputManager.onToggleRadialMenu != null)
    {
      bool button1 = GamePad.GetButton(CButton.RB, PlayerIndex.One);
      bool button2 = GamePad.GetButton(CButton.LB, PlayerIndex.One);
      if (this.m_RadialState == GamepadInputManager.ERadialMenu.Closed && (button1 || button2))
      {
        this.m_TimeBumperToOpen = Time.time + 0.2f;
        this.m_RadialState = GamepadInputManager.ERadialMenu.Opening;
        this.m_BumperHeld = !button1 ? CButton.LB : CButton.RB;
        return true;
      }
      if (this.m_RadialState == GamepadInputManager.ERadialMenu.Opening && (button1 && this.m_BumperHeld == CButton.RB || button2 && this.m_BumperHeld == CButton.LB))
      {
        if ((double) Time.time >= (double) this.m_TimeBumperToOpen)
        {
          this.m_RadialState = GamepadInputManager.ERadialMenu.Open;
          RacingVehicle racing_vehicle = !button2 ? this.m_RacingVehicles[1] : this.m_RacingVehicles[0];
          App.instance.cameraManager.SetTarget((Vehicle) racing_vehicle, CameraManager.Transition.Smooth);
          GamepadInputManager.onToggleRadialMenu(true, racing_vehicle);
        }
        return true;
      }
      if (this.m_RadialState == GamepadInputManager.ERadialMenu.Open && (button1 && this.m_BumperHeld == CButton.RB || button2 && this.m_BumperHeld == CButton.LB))
        return true;
      if (!button2 && !button1)
      {
        if (this.m_RadialState == GamepadInputManager.ERadialMenu.Opening)
        {
          App.instance.cameraManager.SetTarget(this.m_BumperHeld != CButton.LB ? (Vehicle) this.m_RacingVehicles[1] : (Vehicle) this.m_RacingVehicles[0], CameraManager.Transition.Smooth);
          this.m_RadialState = GamepadInputManager.ERadialMenu.Closed;
          return true;
        }
        if (this.m_RadialState == GamepadInputManager.ERadialMenu.Open)
        {
          this.m_RadialState = GamepadInputManager.ERadialMenu.Closed;
          GamepadInputManager.onToggleRadialMenu(false, (RacingVehicle) null);
          return true;
        }
      }
    }
    return false;
  }

  public enum EButtonAction
  {
    Confirm,
    Cancel,
    SkipSession,
  }

  public enum EDpadDirection
  {
    Up,
    Down,
    Left,
    Right,
  }

  private enum ERadialMenu
  {
    Closed,
    Opening,
    Open,
  }

  public delegate void RightStickInput(float x_axis, float y_axis);

  public delegate void PauseButtonInput();

  public delegate void DialogBoxInput(GamepadInputManager.EButtonAction button);

  public delegate void ToggleRadialMenu(bool show, RacingVehicle racing_vehicle);
}
