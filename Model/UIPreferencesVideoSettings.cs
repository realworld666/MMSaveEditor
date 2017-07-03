// Decompiled with JetBrains decompiler
// Type: UIPreferencesVideoSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPreferencesVideoSettings : MonoBehaviour
{
  private bool mBlockInitialSound = true;
  public UIPreferencesDropdown preset;
  public UIPreferencesDropdown unityQuality;
  public UIPreferencesDropdown display;
  public UIPreferencesDropdown resolution;
  public Toggle vsync;
  public Toggle worldDetail;
  public Toggle traffic;
  public Toggle crowds;
  public Toggle decals;
  public Toggle vfx;
  public Toggle antiAliasing;
  public Toggle tiltShift;
  public Toggle ambientOcclusion;
  public Toggle runInBackground;
  public Toggle SSR;
  public Toggle toneMapping;
  public Toggle bloom;
  public Toggle vignetteChromaticAbberation;
  public Toggle DOF;
  public Toggle WaterQuality;
  public Toggle DynamicLights;
  public Toggle lowSpecMode;
  public Toggle highSpecMode;
  public GameObject lowSpecModeInactive;
  public GameObject highSpecModeInactive;
  public GameObject[] highSpecModeDisabled;
  public PreferencesScreen screen;
  private bool mPresetChanged;
  private PreferencesManager mManager;

  public void OnStart()
  {
    this.mManager = App.instance.preferencesManager;
    this.preset.OnStart();
    this.unityQuality.OnStart();
    this.display.OnStart();
    this.resolution.OnStart();
    this.preset.dropdown.onValueChanged.AddListener((UnityAction<int>) (param0 =>
    {
      this.SetSetting(Preference.pName.Video_Preset, this.preset, UIPreferencesVideoSettings.SettingType.pString);
      this.ChangePreset(this.preset);
    }));
    this.unityQuality.dropdown.onValueChanged.AddListener((UnityAction<int>) (param0 => this.SetSetting(Preference.pName.Video_UnityQuality, this.unityQuality, UIPreferencesVideoSettings.SettingType.pString)));
    this.display.dropdown.onValueChanged.AddListener((UnityAction<int>) (param0 => this.SetSetting(Preference.pName.Video_Display, this.display, UIPreferencesVideoSettings.SettingType.pString)));
    this.resolution.dropdown.onValueChanged.AddListener((UnityAction<int>) (param0 => this.SetSetting(Preference.pName.Video_Resolution, this.resolution, UIPreferencesVideoSettings.SettingType.pString)));
    this.vsync.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetSetting(Preference.pName.Video_Vsync, this.vsync, UIPreferencesVideoSettings.SettingType.pBool)));
    this.worldDetail.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetSetting(Preference.pName.Video_WorldDetail, this.worldDetail, UIPreferencesVideoSettings.SettingType.pBool)));
    this.traffic.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetSetting(Preference.pName.Video_Traffic, this.traffic, UIPreferencesVideoSettings.SettingType.pBool)));
    this.crowds.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetSetting(Preference.pName.Video_Crowds, this.crowds, UIPreferencesVideoSettings.SettingType.pBool)));
    this.decals.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetSetting(Preference.pName.Video_Decals, this.decals, UIPreferencesVideoSettings.SettingType.pBool)));
    this.vfx.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetSetting(Preference.pName.Video_VFX, this.vfx, UIPreferencesVideoSettings.SettingType.pBool)));
    this.antiAliasing.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetSetting(Preference.pName.Video_AntiAliasing, this.antiAliasing, UIPreferencesVideoSettings.SettingType.pBool)));
    this.tiltShift.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetSetting(Preference.pName.Video_TiltShift, this.tiltShift, UIPreferencesVideoSettings.SettingType.pBool)));
    this.ambientOcclusion.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetSetting(Preference.pName.Video_AmbientOcclusion, this.ambientOcclusion, UIPreferencesVideoSettings.SettingType.pBool)));
    this.runInBackground.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetSetting(Preference.pName.Video_RunInBackground, this.runInBackground, UIPreferencesVideoSettings.SettingType.pBool)));
    this.SSR.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetSetting(Preference.pName.Video_SSR, this.SSR, UIPreferencesVideoSettings.SettingType.pBool)));
    this.toneMapping.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetSetting(Preference.pName.Video_ToneMapping, this.toneMapping, UIPreferencesVideoSettings.SettingType.pBool)));
    this.bloom.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetSetting(Preference.pName.Video_Bloom, this.bloom, UIPreferencesVideoSettings.SettingType.pBool)));
    this.vignetteChromaticAbberation.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetSetting(Preference.pName.Video_VignetteChromaticAbberation, this.vignetteChromaticAbberation, UIPreferencesVideoSettings.SettingType.pBool)));
    this.DOF.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetSetting(Preference.pName.Video_DOF, this.DOF, UIPreferencesVideoSettings.SettingType.pBool)));
    this.lowSpecMode.onValueChanged.AddListener((UnityAction<bool>) (param0 =>
    {
      this.SetSetting(Preference.pName.Video_3D, this.highSpecMode, UIPreferencesVideoSettings.SettingType.pBool);
      this.ActivateSpecMode(this.lowSpecMode);
    }));
    this.highSpecMode.onValueChanged.AddListener((UnityAction<bool>) (param0 =>
    {
      this.SetSetting(Preference.pName.Video_3D, this.highSpecMode, UIPreferencesVideoSettings.SettingType.pBool);
      this.ActivateSpecMode(this.highSpecMode);
    }));
    this.WaterQuality.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetSetting(Preference.pName.Video_WaterQuality, this.WaterQuality, UIPreferencesVideoSettings.SettingType.pBool)));
    this.DynamicLights.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetSetting(Preference.pName.Video_DynamicLights, this.DynamicLights, UIPreferencesVideoSettings.SettingType.pBool)));
    this.Refresh();
    this.OnSetupChange();
  }

  public void Refresh()
  {
    this.ReadSetting(Preference.pName.Video_Preset, this.preset);
    this.ReadSetting(Preference.pName.Video_UnityQuality, this.unityQuality);
    this.ReadSetting(Preference.pName.Video_Display, this.display);
    this.ReadSetting(Preference.pName.Video_Resolution, this.resolution);
    this.ReadToggleSetting(Preference.pName.Video_Vsync, this.vsync);
    this.ReadToggleSetting(Preference.pName.Video_WorldDetail, this.worldDetail);
    this.ReadToggleSetting(Preference.pName.Video_Traffic, this.traffic);
    this.ReadToggleSetting(Preference.pName.Video_Crowds, this.crowds);
    this.ReadToggleSetting(Preference.pName.Video_Decals, this.decals);
    this.ReadToggleSetting(Preference.pName.Video_VFX, this.vfx);
    this.ReadToggleSetting(Preference.pName.Video_AntiAliasing, this.antiAliasing);
    this.ReadToggleSetting(Preference.pName.Video_TiltShift, this.tiltShift);
    this.ReadToggleSetting(Preference.pName.Video_AmbientOcclusion, this.ambientOcclusion);
    this.ReadToggleSetting(Preference.pName.Video_RunInBackground, this.runInBackground);
    this.ReadToggleSetting(Preference.pName.Video_SSR, this.SSR);
    this.ReadToggleSetting(Preference.pName.Video_ToneMapping, this.toneMapping);
    this.ReadToggleSetting(Preference.pName.Video_Bloom, this.bloom);
    this.ReadToggleSetting(Preference.pName.Video_VignetteChromaticAbberation, this.vignetteChromaticAbberation);
    this.ReadToggleSetting(Preference.pName.Video_DOF, this.DOF);
    this.ReadToggleSetting(Preference.pName.Video_WaterQuality, this.WaterQuality);
    this.ReadToggleSetting(Preference.pName.Video_DynamicLights, this.DynamicLights);
    this.ReadToggleSpecModeSetting();
  }

  public void RefreshUIDropdownLocalisation()
  {
    this.preset.RefreshDropdownLocalisation();
    this.unityQuality.RefreshDropdownLocalisation();
    this.display.RefreshDropdownLocalisation();
    this.resolution.RefreshDropdownLocalisation();
  }

  public void ConfirmSettings()
  {
    this.ChangeDisplay(this.display);
    this.ChangeUnityPreset(this.unityQuality);
    this.ChangeResolution(this.resolution);
    this.ChangeVsync(this.vsync);
    this.ChangeAA(this.antiAliasing);
    this.ChangeBasicDetail(Preference.pName.Video_WorldDetail, this.worldDetail);
    this.ChangeBasicDetail(Preference.pName.Video_Traffic, this.traffic);
    this.ChangeBasicDetail(Preference.pName.Video_Crowds, this.crowds);
    this.ChangeBasicDetail(Preference.pName.Video_Decals, this.decals);
    this.ChangeBasicDetail(Preference.pName.Video_VFX, this.vfx);
    this.ChangeBasicDetail(Preference.pName.Video_WaterQuality, this.WaterQuality);
    this.ChangeBasicDetail(Preference.pName.Video_DynamicLights, this.DynamicLights);
    this.ChangePostProcessing(Preference.pName.Video_TiltShift, this.tiltShift);
    this.ChangePostProcessing(Preference.pName.Video_AmbientOcclusion, this.ambientOcclusion);
    this.ChangeRunInBackground(this.runInBackground);
    this.ChangePostProcessing(Preference.pName.Video_SSR, this.SSR);
    this.ChangePostProcessing(Preference.pName.Video_ToneMapping, this.toneMapping);
    this.ChangePostProcessing(Preference.pName.Video_Bloom, this.bloom);
    this.ChangePostProcessing(Preference.pName.Video_VignetteChromaticAbberation, this.vignetteChromaticAbberation);
    this.ChangePostProcessing(Preference.pName.Video_DOF, this.DOF);
    this.ChangeBasicDetail(Preference.pName.Video_3D, this.highSpecMode);
    this.TriggerSpecModeChangeMessage();
  }

  public void OnSetupChange()
  {
    PrefVideoPreset.Type settingEnum = this.mManager.GetSettingEnum<PrefVideoPreset.Type>(Preference.pName.Video_Preset, true);
    int count = this.preset.dropdown.get_options().Count;
    if (settingEnum == PrefVideoPreset.Type.Custom)
    {
      if (count < 5)
      {
        this.preset.dropdown.get_options().Add(new Dropdown.OptionData(Localisation.LocaliseEnum((Enum) PrefVideoPreset.Type.Custom)));
        this.preset.dropdown.value = this.preset.dropdown.get_options().Count - 1;
      }
    }
    else if (count >= 5)
      this.preset.dropdown.get_options().RemoveAt(count - 1);
    this.preset.dropdown.RefreshShownValue();
  }

  private void ChangePreset(UIPreferencesDropdown inDropDown)
  {
    this.mPresetChanged = true;
    this.mManager.LoadPreset(this.preset.GetEnumValue<PrefVideoPreset.Type>());
    this.Refresh();
    this.OnSetupChange();
    this.mPresetChanged = false;
  }

  private void ChangeUnityPreset(UIPreferencesDropdown inDropDown)
  {
    this.mManager.videoPreferences.SetQuality(this.mManager.GetSettingEnum<PrefVideoUnityQuality.Type>(Preference.pName.Video_UnityQuality, true));
  }

  private void ChangeAA(Toggle inToggle)
  {
    this.mManager.videoPreferences.SetAntiAliasing(inToggle.isOn);
  }

  private void ChangeDisplay(UIPreferencesDropdown inDropDown)
  {
    this.mManager.videoPreferences.SetVideoDisplay(this.mManager.videoPreferences.GetVideoDisplay(inDropDown.GetValue()));
  }

  private void ChangeResolution(UIPreferencesDropdown inDropDown)
  {
    int[] videoResolution = this.mManager.videoPreferences.GetVideoResolution(inDropDown.GetValue());
    this.mManager.videoPreferences.SetVideoResolution(videoResolution[0], videoResolution[1], this.mManager.videoPreferences.currentFullscreenMode);
  }

  private void ChangeVsync(Toggle inToggle)
  {
    this.mManager.videoPreferences.SetVsync(inToggle.isOn);
  }

  private void ChangeRunInBackground(Toggle inToggle)
  {
    this.mManager.videoPreferences.SetRunInBackground(inToggle.isOn);
  }

  private void ChangeBasicDetail(Preference.pName inName, Toggle inToggle)
  {
    this.mManager.videoPreferences.SetBasicDetail(inName, inToggle.isOn);
  }

  private void ChangePostProcessing(Preference.pName inName, Toggle inToggle)
  {
    this.mManager.videoPreferences.SetPostProcessing(inName, inToggle.isOn);
  }

  private void ReadSetting(Preference.pName inName, UIPreferencesDropdown inDropDown)
  {
    string settingString = this.mManager.GetSettingString(inName, true);
    if (string.IsNullOrEmpty(settingString))
      return;
    inDropDown.SetValue(settingString);
  }

  private void ReadSliderSetting(Preference.pName inName, UIPreferencesSlider inSlider)
  {
    int settingInt = this.mManager.GetSettingInt(inName, true);
    this.SetSliderSettingValue(inSlider, settingInt);
  }

  private void ReadToggleSetting(Preference.pName inName, Toggle inToggle)
  {
    inToggle.isOn = this.mManager.GetSettingBool(inName, true);
  }

  private void ReadToggleSpecModeSetting()
  {
    bool settingBool = this.mManager.GetSettingBool(Preference.pName.Video_3D, true);
    this.lowSpecMode.isOn = !settingBool;
    this.highSpecMode.isOn = settingBool;
  }

  private void SetSliderSettingValue(UIPreferencesSlider inSlider, int inValue)
  {
    inSlider.SetSliderValue((float) inValue);
  }

  private void SetSetting(Preference.pName inName, UIPreferencesDropdown inDropDown, UIPreferencesVideoSettings.SettingType inValueType)
  {
    bool flag = false;
    switch (inValueType)
    {
      case UIPreferencesVideoSettings.SettingType.pString:
        flag = this.mManager.SetSetting(inName, (object) inDropDown.GetValue(), true);
        break;
      case UIPreferencesVideoSettings.SettingType.pFloat:
        flag = this.mManager.SetSetting(inName, (object) float.Parse(inDropDown.GetValue()), true);
        break;
      case UIPreferencesVideoSettings.SettingType.pInt:
        flag = this.mManager.SetSetting(inName, (object) int.Parse(inDropDown.GetValue()), true);
        break;
    }
    if (!flag)
      return;
    this.SetPresetCustom(inName);
    this.screen.UpdateSettingsChangedStates(true);
  }

  private void SetSetting(Preference.pName inName, UIPreferencesSlider inSlider, UIPreferencesVideoSettings.SettingType inValueType)
  {
    bool flag = false;
    switch (inValueType)
    {
      case UIPreferencesVideoSettings.SettingType.pString:
        flag = this.mManager.SetSetting(inName, (object) inSlider.slider.value.ToString(), true);
        break;
      case UIPreferencesVideoSettings.SettingType.pFloat:
        flag = this.mManager.SetSetting(inName, (object) inSlider.slider.value, true);
        break;
      case UIPreferencesVideoSettings.SettingType.pInt:
        flag = this.mManager.SetSetting(inName, (object) Mathf.RoundToInt(inSlider.slider.value), true);
        break;
    }
    inSlider.SetSliderValue(inSlider.slider.value);
    if (!flag)
      return;
    this.SetPresetCustom(inName);
    this.screen.UpdateSettingsChangedStates(true);
  }

  private void SetSetting(Preference.pName inName, Toggle inToggle, UIPreferencesVideoSettings.SettingType inValueType)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    bool flag = false;
    switch (inValueType)
    {
      case UIPreferencesVideoSettings.SettingType.pString:
        flag = this.mManager.SetSetting(inName, (object) PrefToggle.GetValue(inToggle.isOn), true);
        break;
      case UIPreferencesVideoSettings.SettingType.pFloat:
        flag = this.mManager.SetSetting(inName, (object) (float) (!inToggle.isOn ? 0.0 : 1.0), true);
        break;
      case UIPreferencesVideoSettings.SettingType.pInt:
        flag = this.mManager.SetSetting(inName, (object) (!inToggle.isOn ? 0 : 1), true);
        break;
      case UIPreferencesVideoSettings.SettingType.pBool:
        flag = this.mManager.SetSetting(inName, (object) inToggle.isOn, true);
        break;
    }
    if (inName == Preference.pName.Video_RunInBackground && flag && inToggle.isOn)
      UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>().Show((Action) null, Localisation.LocaliseID("PSG_10009081", (GameObject) null), (Action) null, string.Empty, Localisation.LocaliseID("PSG_10009125", (GameObject) null), Localisation.LocaliseID("PSG_10009126", (GameObject) null));
    if (!flag)
      return;
    this.SetPresetCustom(inName);
    this.screen.UpdateSettingsChangedStates(true);
  }

  private void SetPresetCustom(Preference.pName inName)
  {
    if (this.mPresetChanged || inName == Preference.pName.Video_Preset || (inName == Preference.pName.Video_Resolution || inName == Preference.pName.Video_Display))
      return;
    this.mManager.SetSetting(Preference.pName.Video_Preset, (object) PrefVideoPreset.Type.Custom, true);
  }

  private void ActivateSpecMode(Toggle inToggle)
  {
    if (!inToggle.isOn)
      return;
    bool isOn = this.highSpecMode.isOn;
    if (!isOn && this.preset.isDropdownActive())
      this.preset.ResetDropdown();
    GameUtility.SetActive(this.worldDetail.gameObject, isOn);
    GameUtility.SetActive(this.traffic.gameObject, isOn);
    GameUtility.SetActive(this.crowds.gameObject, isOn);
    GameUtility.SetActive(this.decals.gameObject, isOn);
    GameUtility.SetActive(this.vfx.gameObject, isOn);
    GameUtility.SetActive(this.tiltShift.gameObject, isOn);
    GameUtility.SetActive(this.ambientOcclusion.gameObject, isOn);
    GameUtility.SetActive(this.SSR.gameObject, isOn);
    GameUtility.SetActive(this.toneMapping.gameObject, isOn);
    GameUtility.SetActive(this.bloom.gameObject, isOn);
    GameUtility.SetActive(this.vignetteChromaticAbberation.gameObject, isOn);
    GameUtility.SetActive(this.DOF.gameObject, isOn);
    GameUtility.SetActive(this.WaterQuality.gameObject, isOn);
    GameUtility.SetActive(this.DynamicLights.gameObject, isOn);
    GameUtility.SetActive(this.preset.dropdown.gameObject, isOn);
    GameUtility.SetActive(this.unityQuality.dropdown.gameObject, isOn);
    GameUtility.SetActive(this.highSpecModeInactive, !isOn);
    GameUtility.SetActive(this.lowSpecModeInactive, isOn);
    for (int index = 0; index < this.highSpecModeDisabled.Length; ++index)
      GameUtility.SetActive(this.highSpecModeDisabled[index].gameObject, !isOn);
  }

  private void TriggerSpecModeChangeMessage()
  {
    if (this.mManager.GetSettingBool(Preference.pName.Video_3D, false) == this.mManager.GetSettingBool(Preference.pName.Video_3D, true))
      return;
    if (this.mManager.GetSettingBool(Preference.pName.Video_3D, true))
    {
      if (Game.IsActive() && Game.instance.sessionManager.isCircuitLoaded && Game.instance.sessionManager.circuit.hasLoaded3DGeometry || SceneManager.instance.hasLoaded3DGeometry)
      {
        this.mManager.videoPreferences.SetRunning2DMode(false);
      }
      else
      {
        GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
        Action inCancelAction = (Action) null;
        if (this.screen.leavingScreen)
          inCancelAction = (Action) (() => this.screen.OnContinueButtonComplete(true));
        dialog.Show(inCancelAction, Localisation.LocaliseID("PSG_10004912", (GameObject) null), (Action) null, string.Empty, Localisation.LocaliseID("PSG_10011745", (GameObject) null), Localisation.LocaliseID("PSG_10011744", (GameObject) null));
      }
    }
    else
      this.mManager.videoPreferences.SetRunning2DMode(true);
  }

  private void OnEnable()
  {
    if (!this.mBlockInitialSound)
      scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionScreen, 0.0f);
    else
      this.mBlockInitialSound = false;
  }

  public enum SettingType
  {
    pString,
    pFloat,
    pInt,
    pBool,
  }
}
