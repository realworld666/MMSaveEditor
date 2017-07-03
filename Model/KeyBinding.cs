// Decompiled with JetBrains decompiler
// Type: KeyBinding
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyBinding
{
  public KeyBinding.Name name = KeyBinding.Name.Home;
  private List<KeyBinding.KeyBind> keyBinds = new List<KeyBinding.KeyBind>();
  private List<Action> keyActions = new List<Action>();
  public bool canRunOverDialogs;

  public KeyBinding(KeyBinding.Name inName, params Action[] inActions)
  {
    this.name = inName;
    if (inActions != null)
    {
      for (int index = 0; index < inActions.Length; ++index)
        this.keyActions.Add(inActions[index]);
    }
    this.canRunOverDialogs = KeyBinding.CanRunOverDialogs(this.name);
  }

  public void AddKeyBind(KeyCode inKeyCode, KeyBinding.KeyEvent inKeyEvent)
  {
    KeyBinding.KeyBind inKeyBind = new KeyBinding.KeyBind(inKeyCode, inKeyEvent);
    if (this.CheckBind(inKeyBind))
      return;
    this.keyBinds.Add(inKeyBind);
  }

  public void AddKeyComboBind(KeyCode inKeyCode, KeyCode inKeySecondCode, KeyBinding.KeyEvent inKeyEvent)
  {
    KeyBinding.KeyBind inKeyBind = new KeyBinding.KeyBind(inKeyCode, inKeySecondCode, inKeyEvent);
    if (this.CheckBind(inKeyBind))
      return;
    this.keyBinds.Add(inKeyBind);
  }

  public void AddMouseBind(int inMouseCode, KeyBinding.KeyEvent inKeyEvent)
  {
    KeyBinding.KeyBind inKeyBind = new KeyBinding.KeyBind(inMouseCode, inKeyEvent);
    if (this.CheckBind(inKeyBind))
      return;
    this.keyBinds.Add(inKeyBind);
  }

  public void AddBind(KeyBinding.KeyBind inKeyBind)
  {
    if (this.CheckBind(inKeyBind))
      return;
    this.keyBinds.Add(inKeyBind);
  }

  public void AddBinds(params KeyBinding.KeyBind[] inKeyBinds)
  {
    for (int index = 0; index < inKeyBinds.Length; ++index)
    {
      if (!this.CheckBind(inKeyBinds[index]))
        this.keyBinds.Add(inKeyBinds[index]);
    }
  }

  public void RemoveKeyBind(KeyCode inKeyCode, KeyBinding.KeyEvent inKeyEvent)
  {
    KeyBinding.KeyBind inKeyBind = new KeyBinding.KeyBind(inKeyCode, inKeyEvent);
    if (!this.CheckBind(inKeyBind))
      return;
    this.keyBinds.Remove(inKeyBind);
  }

  public void RemoveKeyComboBind(KeyCode inKeyCode, KeyCode inKeySecondCode, KeyBinding.KeyEvent inKeyEvent)
  {
    KeyBinding.KeyBind inKeyBind = new KeyBinding.KeyBind(inKeyCode, inKeySecondCode, inKeyEvent);
    if (!this.CheckBind(inKeyBind))
      return;
    this.keyBinds.Remove(inKeyBind);
  }

  public void RemoveMouseBind(int inMouseCode, KeyBinding.KeyEvent inKeyEvent)
  {
    KeyBinding.KeyBind inKeyBind = new KeyBinding.KeyBind(inMouseCode, inKeyEvent);
    if (!this.CheckBind(inKeyBind))
      return;
    this.keyBinds.Remove(inKeyBind);
  }

  public void RemoveBind(KeyBinding.KeyBind inKeyBind)
  {
    if (!this.CheckBind(inKeyBind))
      return;
    this.keyBinds.Remove(inKeyBind);
  }

  public void RemoveBinds(params KeyBinding.KeyBind[] inKeyBinds)
  {
    for (int index = 0; index < inKeyBinds.Length; ++index)
    {
      if (this.CheckBind(inKeyBinds[index]))
        this.keyBinds.Remove(inKeyBinds[index]);
    }
  }

  public void RemoveAllBinds()
  {
    this.keyBinds.Clear();
  }

  private bool CheckBind(KeyBinding.KeyBind inKeyBind)
  {
    for (int index = 0; index < this.keyBinds.Count; ++index)
    {
      if (this.keyBinds[index] == inKeyBind)
        return true;
    }
    return false;
  }

  public void AddAction(Action inAction)
  {
    if (this.CheckAction(inAction))
      return;
    this.keyActions.Add(inAction);
  }

  public void AddActions(params Action[] inActions)
  {
    for (int index = 0; index < inActions.Length; ++index)
    {
      if (!this.CheckAction(inActions[index]))
        this.keyActions.Add(inActions[index]);
    }
  }

  public void RemoveAction(Action inAction)
  {
    if (this.CheckAction(inAction))
      return;
    this.keyActions.Remove(inAction);
  }

  public void RemoveActions(params Action[] inActions)
  {
    for (int index = 0; index < inActions.Length; ++index)
    {
      if (!this.CheckAction(inActions[index]))
        this.keyActions.Remove(inActions[index]);
    }
  }

  public bool CheckAction(Action inAction)
  {
    for (int index = 0; index < this.keyActions.Count; ++index)
    {
      if ((MulticastDelegate) this.keyActions[index] == (MulticastDelegate) inAction)
        return true;
    }
    return false;
  }

  public void TriggerActions()
  {
    for (int index = 0; index < this.keyActions.Count; ++index)
      this.keyActions[index]();
  }

  public bool CheckInput(bool inAllBindsTriggered)
  {
    bool flag = false;
    if (!this.CanTriggerBind())
      return false;
    for (int index = 0; index < this.keyBinds.Count; ++index)
    {
      KeyBinding.KeyBind keyBind = this.keyBinds[index];
      switch (keyBind.keyType)
      {
        case KeyBinding.KeyType.Key:
          flag = this.CheckKey(keyBind, keyBind.keyEvent);
          break;
        case KeyBinding.KeyType.KeyCombo:
          flag = this.CheckComboKey(keyBind, keyBind.keyEvent);
          break;
        case KeyBinding.KeyType.Mouse:
          flag = this.CheckMouse(keyBind, keyBind.keyEvent);
          break;
      }
      if (!inAllBindsTriggered && flag)
        return true;
      if (inAllBindsTriggered && !flag)
        return false;
    }
    return inAllBindsTriggered;
  }

  public bool CheckInput(KeyBinding.KeyEvent inKeyEvent)
  {
    bool flag = false;
    if (!this.CanTriggerBind())
      return false;
    for (int index = 0; index < this.keyBinds.Count; ++index)
    {
      KeyBinding.KeyBind keyBind = this.keyBinds[index];
      switch (keyBind.keyType)
      {
        case KeyBinding.KeyType.Key:
          flag = this.CheckKey(keyBind, inKeyEvent);
          break;
        case KeyBinding.KeyType.KeyCombo:
          flag = this.CheckComboKey(keyBind, inKeyEvent);
          break;
        case KeyBinding.KeyType.Mouse:
          flag = this.CheckMouse(keyBind, inKeyEvent);
          break;
      }
      if (flag)
        return true;
    }
    return false;
  }

  private bool CheckKey(KeyBinding.KeyBind inKeyBind, KeyBinding.KeyEvent inKeyEvent)
  {
    bool flag = false;
    switch (inKeyEvent)
    {
      case KeyBinding.KeyEvent.Any:
        if (Input.GetKeyDown(inKeyBind.keyCode))
        {
          flag = true;
          break;
        }
        if (Input.GetKeyUp(inKeyBind.keyCode))
        {
          flag = true;
          break;
        }
        if (Input.GetKey(inKeyBind.keyCode))
        {
          flag = true;
          break;
        }
        break;
      case KeyBinding.KeyEvent.OnButtonDown:
        if (Input.GetKeyDown(inKeyBind.keyCode))
        {
          flag = true;
          break;
        }
        break;
      case KeyBinding.KeyEvent.OnButton:
        if (Input.GetKey(inKeyBind.keyCode))
        {
          flag = true;
          break;
        }
        break;
      case KeyBinding.KeyEvent.OnButtonUp:
        if (Input.GetKeyUp(inKeyBind.keyCode))
        {
          flag = true;
          break;
        }
        break;
    }
    if (flag && flag)
      return !this.CheckOtherModifiers(inKeyBind.keyCode, KeyCode.None);
    return false;
  }

  private bool CheckComboKey(KeyBinding.KeyBind inKeyBind, KeyBinding.KeyEvent inKeyEvent)
  {
    bool flag = false;
    switch (inKeyEvent)
    {
      case KeyBinding.KeyEvent.Any:
        if (Input.GetKeyDown(inKeyBind.keyCode) && Input.GetKey(inKeyBind.keySecondCode))
        {
          flag = true;
          break;
        }
        if (Input.GetKeyUp(inKeyBind.keyCode) && Input.GetKey(inKeyBind.keySecondCode))
        {
          flag = true;
          break;
        }
        if (Input.GetKey(inKeyBind.keyCode) && Input.GetKey(inKeyBind.keySecondCode))
        {
          flag = true;
          break;
        }
        break;
      case KeyBinding.KeyEvent.OnButtonDown:
        if (Input.GetKeyDown(inKeyBind.keyCode) && Input.GetKey(inKeyBind.keySecondCode))
        {
          flag = true;
          break;
        }
        break;
      case KeyBinding.KeyEvent.OnButton:
        if (Input.GetKey(inKeyBind.keyCode) && Input.GetKey(inKeyBind.keySecondCode))
        {
          flag = true;
          break;
        }
        break;
      case KeyBinding.KeyEvent.OnButtonUp:
        if (Input.GetKeyUp(inKeyBind.keyCode) && Input.GetKey(inKeyBind.keySecondCode))
        {
          flag = true;
          break;
        }
        break;
    }
    if (flag && flag)
      return !this.CheckOtherModifiers(inKeyBind.keyCode, inKeyBind.keySecondCode);
    return false;
  }

  private bool CheckMouse(KeyBinding.KeyBind inKeyBind, KeyBinding.KeyEvent inKeyEvent)
  {
    int button = inKeyBind.mouseCode;
    if (InputManager.instance.mouseLeftHand && button < 2)
      button = button != 0 ? 0 : 1;
    switch (inKeyEvent)
    {
      case KeyBinding.KeyEvent.Any:
        if (Input.GetMouseButtonDown(button) || Input.GetMouseButtonUp(button) || Input.GetMouseButton(button))
          return true;
        break;
      case KeyBinding.KeyEvent.OnButtonDown:
        if (Input.GetMouseButtonDown(button))
          return true;
        break;
      case KeyBinding.KeyEvent.OnButton:
        if (Input.GetMouseButton(button))
          return true;
        break;
      case KeyBinding.KeyEvent.OnButtonUp:
        if (Input.GetMouseButtonUp(button))
          return true;
        break;
    }
    return false;
  }

  private bool CheckOtherModifiers(KeyCode inPressedKeyCode, KeyCode inSecondPressedKeyCode = KeyCode.None)
  {
    for (int index = 0; index < UIKeyButtonsConstants.keyModifiers.Length; ++index)
    {
      KeyCode keyModifier = UIKeyButtonsConstants.keyModifiers[index];
      if (keyModifier != inPressedKeyCode && (inSecondPressedKeyCode == KeyCode.None || inSecondPressedKeyCode != keyModifier) && (Input.GetKey(keyModifier) || Input.GetKeyDown(keyModifier) || Input.GetKeyUp(keyModifier)))
        return true;
    }
    return false;
  }

  private static bool CanRunOverDialogs(KeyBinding.Name inName)
  {
    switch (inName)
    {
      case KeyBinding.Name.Escape:
      case KeyBinding.Name.Return:
      case KeyBinding.Name.MouseLeft:
      case KeyBinding.Name.MouseRight:
      case KeyBinding.Name.MouseMiddle:
        return true;
      default:
        return false;
    }
  }

  private bool CanTriggerBind()
  {
    return this.canRunOverDialogs || UIManager.instance.dialogBoxManager != null && UIManager.instance.dialogBoxManager.GetActiveDialogBoxCount() == 0;
  }

  public enum Name
  {
    Pause,
    Speedx1,
    Speedx2,
    Speedx3,
    Home,
    Profile,
    Factory,
    Car,
    Standings,
    Headquarters,
    RotateLeft,
    RotateRight,
    ZoomIn,
    ZoomOut,
    SessionStandings,
    WeatherConditions,
    WaterConditions,
    RubberConditions,
    DataCentre,
    ChangeViewpoint,
    Escape,
    Return,
    MouseLeft,
    MouseRight,
    MouseMiddle,
  }

  public enum KeyType
  {
    Key,
    KeyCombo,
    Mouse,
  }

  public enum KeyEvent
  {
    Any,
    OnButtonDown,
    OnButton,
    OnButtonUp,
  }

  public class KeyBind
  {
    public KeyBinding.KeyEvent keyEvent = KeyBinding.KeyEvent.OnButton;
    public KeyBinding.KeyType keyType;
    public KeyCode keyCode;
    public KeyCode keySecondCode;
    public int mouseCode;

    public KeyBind(KeyCode inKeyCode, KeyBinding.KeyEvent inKeyEvent)
    {
      this.keyType = KeyBinding.KeyType.Key;
      this.keyCode = inKeyCode;
      this.keyEvent = inKeyEvent;
    }

    public KeyBind(KeyCode inKeyCode, KeyCode inKeySecondCode, KeyBinding.KeyEvent inKeyEvent)
    {
      this.keyType = KeyBinding.KeyType.KeyCombo;
      this.keyCode = inKeyCode;
      this.keySecondCode = inKeySecondCode;
      this.keyEvent = inKeyEvent;
    }

    public KeyBind(int inMouseCode, KeyBinding.KeyEvent inKeyEvent)
    {
      this.keyType = KeyBinding.KeyType.Mouse;
      this.mouseCode = inMouseCode;
      this.keyEvent = inKeyEvent;
    }
  }
}
