// Decompiled with JetBrains decompiler
// Type: InputManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
  private List<KeyBinding> keyBindings = new List<KeyBinding>();
  private bool mMouseLeftHand;

  public static InputManager instance
  {
    get
    {
      return App.instance.inputManager;
    }
  }

  public bool mouseLeftHand
  {
    get
    {
      return this.mMouseLeftHand;
    }
  }

  public void Update()
  {
    for (int index = 0; index < this.keyBindings.Count; ++index)
    {
      if (this.keyBindings[index].CheckInput(false))
        this.keyBindings[index].TriggerActions();
    }
  }

  public void SetMouseLeftHand(bool inValue)
  {
    this.mMouseLeftHand = inValue;
  }

  public KeyBinding AddKeyBind(KeyBinding.Name inName, params Action[] inActions)
  {
    KeyBinding keyBinding = this.GetKeyBinding(inName);
    if (keyBinding != null)
    {
      Debug.Log((object) "Key Binding already Assigned", (UnityEngine.Object) null);
    }
    else
    {
      keyBinding = new KeyBinding(inName, inActions);
      this.keyBindings.Add(keyBinding);
    }
    return keyBinding;
  }

  public void ChangeKeyBind(KeyBinding.Name inName, KeyCode inKeyCode, KeyBinding.KeyEvent inKeyEvent)
  {
    KeyBinding keyBinding = this.GetKeyBinding(inName);
    if (keyBinding == null)
      return;
    keyBinding.RemoveAllBinds();
    keyBinding.AddKeyBind(inKeyCode, inKeyEvent);
  }

  public void RemoveKeyBind(KeyBinding.Name inName)
  {
    KeyBinding keyBinding = this.GetKeyBinding(inName);
    if (keyBinding == null)
      return;
    this.keyBindings.Remove(keyBinding);
  }

  public KeyBinding GetKeyBinding(KeyBinding.Name inName)
  {
    for (int index = 0; index < this.keyBindings.Count; ++index)
    {
      if (this.keyBindings[index].name == inName)
        return this.keyBindings[index];
    }
    return (KeyBinding) null;
  }

  public bool GetKey(KeyBinding.Name inName)
  {
    return this.GetInput(inName, KeyBinding.KeyEvent.OnButton);
  }

  public bool GetKeyDown(KeyBinding.Name inName)
  {
    return this.GetInput(inName, KeyBinding.KeyEvent.OnButtonDown);
  }

  public bool GetKeyUp(KeyBinding.Name inName)
  {
    return this.GetInput(inName, KeyBinding.KeyEvent.OnButtonUp);
  }

  public bool GetKeyAny(KeyBinding.Name inName)
  {
    return this.GetInput(inName, KeyBinding.KeyEvent.Any);
  }

  public bool GetInput(KeyBinding.Name inName)
  {
    KeyBinding keyBinding = this.GetKeyBinding(inName);
    if (keyBinding != null)
      return keyBinding.CheckInput(false);
    return false;
  }

  public bool GetInput(KeyBinding.Name inName, bool inAllBindsTriggered)
  {
    KeyBinding keyBinding = this.GetKeyBinding(inName);
    if (keyBinding != null)
      return keyBinding.CheckInput(inAllBindsTriggered);
    return false;
  }

  public bool GetInput(KeyBinding.Name inName, KeyBinding.KeyEvent inKeyEvent)
  {
    KeyBinding keyBinding = this.GetKeyBinding(inName);
    if (keyBinding != null)
      return keyBinding.CheckInput(inKeyEvent);
    return false;
  }
}
