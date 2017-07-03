// Decompiled with JetBrains decompiler
// Type: UIPreferencesAudioSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPreferencesAudioSettings : MonoBehaviour
{
  private float mTestSoundTimerReset = 0.1f;
  public UIPreferencesSlider mainVolume;
  public UIPreferencesSlider sfxVolume;
  public UIPreferencesSlider uiVolume;
  public UIPreferencesSlider musicVolume;
  public PreferencesScreen screen;
  private PreferencesManager mManager;
  private int dropSliderValue;
  private scSoundContainer mTestVolumeSound;
  private float mTestSoundTimer;
  private SoundID mTestSoundRequested;

  public void OnStart()
  {
    this.mManager = App.instance.preferencesManager;
    this.mainVolume.slider.onValueChanged.AddListener(new UnityAction<float>(this.SetMainVolume));
    this.sfxVolume.slider.onValueChanged.AddListener(new UnityAction<float>(this.SetSfxVolume));
    this.uiVolume.slider.onValueChanged.AddListener(new UnityAction<float>(this.SetUIVolume));
    this.musicVolume.slider.onValueChanged.AddListener(new UnityAction<float>(this.SetMusicVolume));
    this.Refresh();
  }

  private void Update()
  {
    this.mTestSoundTimer = Mathf.Max(0.0f, this.mTestSoundTimer - GameTimer.deltaTime);
    if (this.mTestSoundRequested == SoundID.Unset || (double) this.mTestSoundTimer != 0.0)
      return;
    this.mTestSoundTimer = this.mTestSoundTimerReset;
    scSoundManager.CheckStopSound(ref this.mTestVolumeSound);
    scSoundManager.CheckPlaySound(this.mTestSoundRequested, ref this.mTestVolumeSound, 0.0f);
    this.mTestSoundRequested = SoundID.Unset;
  }

  private void SetMainVolume(float v)
  {
    this.SetSetting(Preference.pName.Audio_MainVolume, this.mainVolume, UIPreferencesAudioSettings.SettingType.pInt);
    this.mTestSoundRequested = SoundID.Sfx_Test_Master_Volume;
    this.mTestSoundTimerReset = 0.05f;
  }

  private void SetSfxVolume(float v)
  {
    this.SetSetting(Preference.pName.Audio_SfxVolume, this.sfxVolume, UIPreferencesAudioSettings.SettingType.pInt);
    this.mTestSoundRequested = SoundID.Sfx_Test_Sfx_Volume;
    this.mTestSoundTimerReset = 1.5f;
  }

  private void SetUIVolume(float v)
  {
    this.SetSetting(Preference.pName.Audio_UIVolume, this.uiVolume, UIPreferencesAudioSettings.SettingType.pInt);
    this.mTestSoundRequested = SoundID.Sfx_Test_UI_Volume;
    this.mTestSoundTimerReset = 0.05f;
  }

  private void PlayTestSound()
  {
  }

  private void SetMusicVolume(float v)
  {
    this.SetSetting(Preference.pName.Audio_MusicVolume, this.musicVolume, UIPreferencesAudioSettings.SettingType.pInt);
  }

  public void Refresh()
  {
    this.ReadSliderSetting(Preference.pName.Audio_MainVolume, this.mainVolume);
    this.ReadSliderSetting(Preference.pName.Audio_SfxVolume, this.sfxVolume);
    this.ReadSliderSetting(Preference.pName.Audio_UIVolume, this.uiVolume);
    this.ReadSliderSetting(Preference.pName.Audio_MusicVolume, this.musicVolume);
  }

  public void ConfirmSettings()
  {
    this.ChangeMainVolume(this.mainVolume);
    this.ChangeSfxVolume(this.sfxVolume);
    this.ChangeUIVolume(this.uiVolume);
    this.ChangeMusicVolume(this.musicVolume);
  }

  private void ChangeMainVolume(UIPreferencesSlider inSlider)
  {
    this.dropSliderValue = inSlider.GetSliderValue();
    this.mManager.audioPreferences.SetAudioMainVolume(this.mManager.GetSliderValue(this.dropSliderValue));
  }

  private void ChangeSfxVolume(UIPreferencesSlider inSlider)
  {
    this.dropSliderValue = inSlider.GetSliderValue();
    this.mManager.audioPreferences.SetAudioSfxVolume(this.mManager.GetSliderValue(this.dropSliderValue));
  }

  private void ChangeUIVolume(UIPreferencesSlider inSlider)
  {
    this.dropSliderValue = inSlider.GetSliderValue();
    this.mManager.audioPreferences.SetAudioUIVolume(this.mManager.GetSliderValue(this.dropSliderValue));
  }

  private void ChangeMusicVolume(UIPreferencesSlider inSlider)
  {
    this.dropSliderValue = inSlider.GetSliderValue();
    this.mManager.audioPreferences.SetAudioMusicVolume(this.mManager.GetSliderValue(this.dropSliderValue));
  }

  private void ReadSetting(Preference.pName inName, Dropdown inDropDown, UIPreferencesAudioSettings.SettingType inValueType)
  {
    switch (inValueType)
    {
      case UIPreferencesAudioSettings.SettingType.pString:
        string settingString = this.mManager.GetSettingString(inName, true);
        this.SetSettingValue(inDropDown, settingString);
        break;
      case UIPreferencesAudioSettings.SettingType.pFloat:
        float settingFloat = this.mManager.GetSettingFloat(inName, true);
        this.SetSettingValue(inDropDown, settingFloat);
        break;
      case UIPreferencesAudioSettings.SettingType.pInt:
        int settingInt = this.mManager.GetSettingInt(inName, true);
        this.SetSettingValue(inDropDown, settingInt);
        break;
    }
  }

  private void ReadSliderSetting(Preference.pName inName, UIPreferencesSlider inSlider)
  {
    int settingInt = this.mManager.GetSettingInt(inName, true);
    this.SetSliderSettingValue(inSlider, settingInt);
  }

  private void SetSettingValue(Dropdown inDropdown, string inValue)
  {
    for (int index = 0; index < inDropdown.get_options().Count; ++index)
    {
      if (inDropdown.get_options()[index].text == inValue)
        inDropdown.value = index;
    }
  }

  private void SetSettingValue(Dropdown inDropdown, int inValue)
  {
    for (int index = 0; index < inDropdown.get_options().Count; ++index)
    {
      if (int.Parse(inDropdown.get_options()[index].text) == inValue)
        inDropdown.value = index;
    }
  }

  private void SetSettingValue(Dropdown inDropdown, float inValue)
  {
    for (int index = 0; index < inDropdown.get_options().Count; ++index)
    {
      if ((double) float.Parse(inDropdown.get_options()[index].text) == (double) inValue)
        inDropdown.value = index;
    }
  }

  private void SetSliderSettingValue(UIPreferencesSlider inSlider, int inValue)
  {
    inSlider.SetSliderValue((float) inValue);
  }

  private void SetSetting(Preference.pName inName, Dropdown inDropDown, UIPreferencesAudioSettings.SettingType inValueType)
  {
    switch (inValueType)
    {
      case UIPreferencesAudioSettings.SettingType.pString:
        this.mManager.SetSetting(inName, (object) inDropDown.get_options()[inDropDown.value].text, true);
        break;
      case UIPreferencesAudioSettings.SettingType.pFloat:
        this.mManager.SetSetting(inName, (object) float.Parse(inDropDown.get_options()[inDropDown.value].text), true);
        break;
      case UIPreferencesAudioSettings.SettingType.pInt:
        this.mManager.SetSetting(inName, (object) int.Parse(inDropDown.get_options()[inDropDown.value].text), true);
        break;
    }
    this.screen.UpdateSettingsChangedStates(true);
  }

  private void SetSetting(Preference.pName inName, UIPreferencesSlider inSlider, UIPreferencesAudioSettings.SettingType inValueType)
  {
    switch (inValueType)
    {
      case UIPreferencesAudioSettings.SettingType.pString:
        this.mManager.SetSetting(inName, (object) inSlider.slider.value.ToString(), true);
        break;
      case UIPreferencesAudioSettings.SettingType.pFloat:
        this.mManager.SetSetting(inName, (object) inSlider.slider.value, true);
        break;
      case UIPreferencesAudioSettings.SettingType.pInt:
        this.mManager.SetSetting(inName, (object) Mathf.RoundToInt(inSlider.slider.value), true);
        break;
    }
    inSlider.SetSliderValue(inSlider.slider.value);
    this.screen.UpdateSettingsChangedStates(true);
    scSoundManager.Instance.SetUserVolumes(true);
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
