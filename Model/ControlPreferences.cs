// Decompiled with JetBrains decompiler
// Type: ControlPreferences
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class ControlPreferences
{
  private PreferencesManager mManager;

  public void Start(PreferencesManager inManager)
  {
    this.mManager = inManager;
  }

  public bool Load()
  {
    bool flag = this.LoadKeyBinding(Preference.pName.Control_ChangeViewpoint, KeyBinding.Name.ChangeViewpoint, KeyBinding.KeyEvent.OnButtonDown) || (this.LoadKeyBinding(Preference.pName.Control_DataCentre, KeyBinding.Name.DataCentre, KeyBinding.KeyEvent.OnButtonDown) || (this.LoadKeyBinding(Preference.pName.Control_RubberConditions, KeyBinding.Name.RubberConditions, KeyBinding.KeyEvent.OnButtonDown) || (this.LoadKeyBinding(Preference.pName.Control_WaterConditions, KeyBinding.Name.WaterConditions, KeyBinding.KeyEvent.OnButtonDown) || (this.LoadKeyBinding(Preference.pName.Control_WeatherConditions, KeyBinding.Name.WeatherConditions, KeyBinding.KeyEvent.OnButtonDown) || (this.LoadKeyBinding(Preference.pName.Control_SessionStandings, KeyBinding.Name.SessionStandings, KeyBinding.KeyEvent.OnButtonDown) || (this.LoadKeyBinding(Preference.pName.Control_ZoomOut, KeyBinding.Name.ZoomOut, KeyBinding.KeyEvent.OnButtonDown) || (this.LoadKeyBinding(Preference.pName.Control_ZoomIn, KeyBinding.Name.ZoomIn, KeyBinding.KeyEvent.OnButtonDown) || (this.LoadKeyBinding(Preference.pName.Control_RotateRight, KeyBinding.Name.RotateRight, KeyBinding.KeyEvent.OnButtonDown) || (this.LoadKeyBinding(Preference.pName.Control_RotateLeft, KeyBinding.Name.RotateLeft, KeyBinding.KeyEvent.OnButtonDown) || (this.LoadKeyBinding(Preference.pName.Control_Headquarters, KeyBinding.Name.Headquarters, KeyBinding.KeyEvent.OnButtonDown) || (this.LoadKeyBinding(Preference.pName.Control_Standings, KeyBinding.Name.Standings, KeyBinding.KeyEvent.OnButtonDown) || (this.LoadKeyBinding(Preference.pName.Control_Car, KeyBinding.Name.Car, KeyBinding.KeyEvent.OnButtonDown) || (this.LoadKeyBinding(Preference.pName.Control_Factory, KeyBinding.Name.Factory, KeyBinding.KeyEvent.OnButtonDown) || (this.LoadKeyBinding(Preference.pName.Control_Profile, KeyBinding.Name.Profile, KeyBinding.KeyEvent.OnButtonDown) || (this.LoadKeyBinding(Preference.pName.Control_Home, KeyBinding.Name.Home, KeyBinding.KeyEvent.OnButtonDown) || (this.LoadKeyBinding(Preference.pName.Control_Speedx3, KeyBinding.Name.Speedx3, KeyBinding.KeyEvent.OnButtonDown) || (this.LoadKeyBinding(Preference.pName.Control_Speedx2, KeyBinding.Name.Speedx2, KeyBinding.KeyEvent.OnButtonDown) || (this.LoadKeyBinding(Preference.pName.Control_Speedx1, KeyBinding.Name.Speedx1, KeyBinding.KeyEvent.OnButtonDown) || this.LoadKeyBinding(Preference.pName.Control_Pause, KeyBinding.Name.Pause, KeyBinding.KeyEvent.OnButtonDown)))))))))))))))))));
    this.CreateMouseBinding(KeyBinding.Name.MouseLeft, 0, KeyBinding.KeyEvent.Any);
    this.CreateMouseBinding(KeyBinding.Name.MouseRight, 1, KeyBinding.KeyEvent.Any);
    this.CreateMouseBinding(KeyBinding.Name.MouseMiddle, 2, KeyBinding.KeyEvent.Any);
    this.CreateKeyBinding(KeyBinding.Name.Return, KeyCode.Return, KeyBinding.KeyEvent.OnButtonDown);
    this.CreateKeyBinding(KeyBinding.Name.Escape, KeyCode.Escape, KeyBinding.KeyEvent.OnButtonDown);
    return flag;
  }

  private bool LoadKeyBinding(Preference.pName inName, KeyBinding.Name inKeyBinding, KeyBinding.KeyEvent inKeyEvent)
  {
    string settingString = this.mManager.GetSettingString(inName, false);
    KeyBinding keyBinding = InputManager.instance.AddKeyBind(inKeyBinding, (Action[]) null);
    KeyCode inKeySecondCode = KeyCode.None;
    KeyCode keyCode;
    if (settingString.Contains(";"))
    {
      string[] strArray = settingString.Split(';');
      inKeySecondCode = this.GetKeyCode(strArray[0]);
      keyCode = this.GetKeyCode(strArray[1]);
    }
    else
      keyCode = this.GetKeyCode(settingString);
    if (inKeySecondCode != KeyCode.None && keyCode != KeyCode.None)
    {
      keyBinding.AddKeyComboBind(keyCode, inKeySecondCode, KeyBinding.KeyEvent.OnButtonDown);
      return true;
    }
    if (inKeySecondCode != KeyCode.None || keyCode == KeyCode.None)
      return false;
    keyBinding.AddKeyBind(keyCode, inKeyEvent);
    return true;
  }

  private void CreateKeyBinding(KeyBinding.Name inName, KeyCode inKeyCode, KeyBinding.KeyEvent inKeyEvent)
  {
    InputManager.instance.AddKeyBind(inName, (Action[]) null).AddKeyBind(inKeyCode, inKeyEvent);
  }

  private void CreateKeyBindingMultiple(KeyBinding.Name inName, KeyBinding.KeyEvent inKeyEvent, params KeyCode[] inKeyCodes)
  {
    KeyBinding keyBinding = InputManager.instance.AddKeyBind(inName, (Action[]) null);
    for (int index = 0; index < inKeyCodes.Length; ++index)
    {
      if (inKeyCodes[index] != KeyCode.None)
        keyBinding.AddKeyBind(inKeyCodes[index], inKeyEvent);
    }
  }

  private void CreateKeyComboBinding(KeyBinding.Name inName, KeyCode inKeyCode, KeyCode inKeySecondCode, KeyBinding.KeyEvent inKeyEvent)
  {
    InputManager.instance.AddKeyBind(inName, (Action[]) null).AddKeyComboBind(inKeyCode, inKeySecondCode, inKeyEvent);
  }

  private void CreateMouseBinding(KeyBinding.Name inName, int inMouseButton, KeyBinding.KeyEvent inKeyEvent)
  {
    InputManager.instance.AddKeyBind(inName, (Action[]) null).AddMouseBind(inMouseButton, inKeyEvent);
  }

  public string GetKeyDisplayText(KeyCode inModifierKey, KeyCode inButtonKey)
  {
    if (inModifierKey != KeyCode.None)
      return UIKeyButtonsConstants.getDisplayKeyCodesText(inModifierKey) + " + " + UIKeyButtonsConstants.getDisplayKeyCodesText(inButtonKey);
    return UIKeyButtonsConstants.getDisplayKeyCodesText(inButtonKey);
  }

  public string GetKeyText(KeyCode inModifierKey, KeyCode inButtonKey)
  {
    if (inModifierKey == KeyCode.None && inButtonKey == KeyCode.None)
      return string.Empty;
    if (inModifierKey != KeyCode.None)
      return ((int) inModifierKey).ToString() + ";" + ((int) inButtonKey).ToString();
    return ((int) inButtonKey).ToString();
  }

  public KeyCode GetKeyCode(string inKeyCodeText)
  {
    int result = 0;
    if (int.TryParse(inKeyCodeText, out result))
      return (KeyCode) result;
    return KeyCode.None;
  }

  public void SetButtonKey(string inKeyName, string inKeyCodeText)
  {
    KeyBinding keyBinding = InputManager.instance.GetKeyBinding((KeyBinding.Name) Enum.Parse(typeof (KeyBinding.Name), inKeyName.Replace("Control_", string.Empty)));
    if (keyBinding == null)
      return;
    KeyCode inKeySecondCode = KeyCode.None;
    KeyCode keyCode;
    if (inKeyCodeText.Contains(";"))
    {
      string[] strArray = inKeyCodeText.Split(';');
      inKeySecondCode = this.GetKeyCode(strArray[0]);
      keyCode = this.GetKeyCode(strArray[1]);
    }
    else
      keyCode = this.GetKeyCode(inKeyCodeText);
    keyBinding.RemoveAllBinds();
    if (inKeySecondCode != KeyCode.None && keyCode != KeyCode.None)
    {
      keyBinding.AddKeyComboBind(keyCode, inKeySecondCode, KeyBinding.KeyEvent.OnButtonDown);
    }
    else
    {
      if (inKeySecondCode != KeyCode.None || keyCode == KeyCode.None)
        return;
      keyBinding.AddKeyBind(keyCode, KeyBinding.KeyEvent.OnButtonDown);
    }
  }
}
