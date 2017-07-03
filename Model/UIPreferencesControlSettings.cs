// Decompiled with JetBrains decompiler
// Type: UIPreferencesControlSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;

public class UIPreferencesControlSettings : MonoBehaviour
{
  public UIPreferencesKeyButton[] controlKeys;
  public PreferencesScreen screen;
  private KeyCode[] modifiersKeyPresses;
  private KeyCode[] validKeyPresses;
  private UIPreferencesKeyButton mListenButton;
  private PreferencesManager mManager;

  public void OnStart()
  {
    this.mManager = App.instance.preferencesManager;
    this.controlKeys = this.gameObject.GetComponentsInChildren<UIPreferencesKeyButton>(true);
    this.CreateValidKeyPresses();
    for (int index = 0; index < this.controlKeys.Length; ++index)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UIPreferencesControlSettings.\u003COnStart\u003Ec__AnonStoreyAC startCAnonStoreyAc = new UIPreferencesControlSettings.\u003COnStart\u003Ec__AnonStoreyAC();
      // ISSUE: reference to a compiler-generated field
      startCAnonStoreyAc.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      startCAnonStoreyAc.key = this.controlKeys[index];
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      startCAnonStoreyAc.key.button.onClick.AddListener(new UnityAction(startCAnonStoreyAc.\u003C\u003Em__228));
    }
    this.Refresh();
  }

  public void Update()
  {
    if (!((Object) this.mListenButton != (Object) null))
      return;
    this.DetectAnyKeyPress();
  }

  public void CreateValidKeyPresses()
  {
    this.validKeyPresses = UIKeyButtonsConstants.keyCodes;
    this.modifiersKeyPresses = UIKeyButtonsConstants.keyModifiers;
  }

  public void DetectAnyKeyPress()
  {
    KeyCode keyCode = KeyCode.None;
    KeyCode inKeyCode = KeyCode.None;
    for (int index = 0; index < this.modifiersKeyPresses.Length; ++index)
    {
      KeyCode modifiersKeyPress = this.modifiersKeyPresses[index];
      if (Input.GetKey(modifiersKeyPress) || Input.GetKeyUp(modifiersKeyPress))
        keyCode = modifiersKeyPress;
    }
    for (int index = 0; index < this.validKeyPresses.Length; ++index)
    {
      KeyCode validKeyPress = this.validKeyPresses[index];
      if (Input.GetKeyUp(validKeyPress))
        inKeyCode = !this.IsKeyModifier(validKeyPress) ? this.validKeyPresses[index] : KeyCode.None;
    }
    if (keyCode != KeyCode.None && Input.GetKeyUp(keyCode) && inKeyCode == KeyCode.None)
    {
      inKeyCode = keyCode;
      keyCode = KeyCode.None;
    }
    if (inKeyCode == KeyCode.None)
      return;
    this.EmptyKey(this.mListenButton.group, keyCode, inKeyCode);
    this.mListenButton.SetModifierKey(keyCode);
    this.mListenButton.SetMainKey(inKeyCode);
    this.mListenButton.SetButtonText(this.GetDisplayText(this.mListenButton));
    this.SetSetting(this.mListenButton.keyName, this.mListenButton, UIPreferencesControlSettings.SettingType.pString);
    this.mListenButton = (UIPreferencesKeyButton) null;
    this.screen.UpdateSettingsChangedStates(true);
  }

  public bool IsKeyModifier(KeyCode inKey)
  {
    for (int index = 0; index < this.modifiersKeyPresses.Length; ++index)
    {
      if (this.modifiersKeyPresses[index] == inKey)
        return true;
    }
    return false;
  }

  public void CancelListen()
  {
    if ((Object) this.mListenButton != (Object) null)
      this.mListenButton.SetButtonText(this.GetDisplayText(this.mListenButton));
    this.mListenButton = (UIPreferencesKeyButton) null;
  }

  public void Refresh()
  {
    for (int index = 0; index < this.controlKeys.Length; ++index)
      this.ReadButtonSetting(this.controlKeys[index].keyName, this.controlKeys[index]);
  }

  public void ConfirmSettings()
  {
    this.CancelListen();
    for (int index = 0; index < this.controlKeys.Length; ++index)
    {
      this.SetSetting(this.controlKeys[index].keyName, this.controlKeys[index], UIPreferencesControlSettings.SettingType.pString);
      this.mManager.controlPreferences.SetButtonKey(this.controlKeys[index].keyName.ToString(), this.GetKeyText(this.controlKeys[index]));
    }
  }

  private void ReadButtonSetting(Preference.pName inName, UIPreferencesKeyButton inButton)
  {
    string settingString = this.mManager.GetSettingString(inName, true);
    KeyCode inModifierKey = KeyCode.None;
    string empty = string.Empty;
    if (!settingString.Contains(";"))
    {
      KeyCode keyCode = this.mManager.controlPreferences.GetKeyCode(settingString);
      string keyDisplayText = this.mManager.controlPreferences.GetKeyDisplayText(inModifierKey, keyCode);
      this.SetButtonSettingValue(inButton, keyDisplayText, inModifierKey, keyCode);
    }
    else
    {
      string[] strArray = settingString.Split(';');
      KeyCode keyCode1 = this.mManager.controlPreferences.GetKeyCode(strArray[0]);
      KeyCode keyCode2 = this.mManager.controlPreferences.GetKeyCode(strArray[1]);
      string keyDisplayText = this.mManager.controlPreferences.GetKeyDisplayText(keyCode1, keyCode2);
      this.SetButtonSettingValue(inButton, keyDisplayText, keyCode1, keyCode2);
    }
  }

  private void SetButtonSettingValue(UIPreferencesKeyButton inButton, string inValue, KeyCode inModifierKey, KeyCode inButtonKey)
  {
    inButton.SetModifierKey(inModifierKey);
    inButton.SetMainKey(inButtonKey);
    inButton.SetButtonText(inValue);
  }

  private void ListenButton(UIPreferencesKeyButton inButton)
  {
    if ((Object) this.mListenButton != (Object) null && (Object) this.mListenButton != (Object) inButton)
      this.ReadButtonSetting(this.mListenButton.keyName, this.mListenButton);
    this.mListenButton = inButton;
    inButton.SetButtonText(Localisation.LocaliseID("PSG_10010693", (GameObject) null));
  }

  private void SetSetting(Preference.pName inName, UIPreferencesKeyButton inButton, UIPreferencesControlSettings.SettingType inValueType)
  {
    switch (inValueType)
    {
      case UIPreferencesControlSettings.SettingType.pString:
        this.mManager.SetSetting(inName, (object) this.GetKeyText(inButton), true);
        break;
      case UIPreferencesControlSettings.SettingType.pFloat:
        this.mManager.SetSetting(inName, (object) float.Parse(this.GetKeyText(inButton)), true);
        break;
      case UIPreferencesControlSettings.SettingType.pInt:
        this.mManager.SetSetting(inName, (object) int.Parse(this.GetKeyText(inButton)), true);
        break;
    }
  }

  private string GetDisplayText(UIPreferencesKeyButton inButton)
  {
    return this.mManager.controlPreferences.GetKeyDisplayText(inButton.GetModifierKey(), inButton.GetMainKey());
  }

  private string GetKeyText(UIPreferencesKeyButton inButton)
  {
    return this.mManager.controlPreferences.GetKeyText(inButton.GetModifierKey(), inButton.GetMainKey());
  }

  private void EmptyKey(UIPreferencesKeyButton.Group inGroup, KeyCode inModifierKeyCode, KeyCode inKeyCode)
  {
    for (int index = 0; index < this.controlKeys.Length; ++index)
    {
      UIPreferencesKeyButton controlKey = this.controlKeys[index];
      if ((inGroup == UIPreferencesKeyButton.Group.Shared || controlKey.group == inGroup || controlKey.group == UIPreferencesKeyButton.Group.Shared) && controlKey.GetModifierKey() == inModifierKeyCode && controlKey.GetMainKey() == inKeyCode)
      {
        controlKey.SetModifierKey(KeyCode.None);
        controlKey.SetMainKey(KeyCode.None);
        controlKey.SetButtonText(string.Empty);
        this.SetSetting(controlKey.keyName, controlKey, UIPreferencesControlSettings.SettingType.pString);
      }
    }
  }

  private void OnEnable()
  {
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionScreen, 0.0f);
  }

  public enum SettingType
  {
    pString,
    pFloat,
    pInt,
  }
}
