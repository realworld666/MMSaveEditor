// Decompiled with JetBrains decompiler
// Type: UIPreferencesGameSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPreferencesGameSettings : MonoBehaviour
{
  public UIPreferencesDropdown language;
  public UIPreferencesDropdown currencySymbol;
  public UIPreferencesDropdown temperature;
  public UIPreferencesDropdown speed;
  public UIPreferencesDropdown dateFormat;
  public UIPreferencesDropdown weight;
  public UIPreferencesDropdown height;
  public UIPreferencesDropdown financesNegative;
  public Dropdown languageFormat;
  public UIPreferencesDropdown raceLenghts;
  public UIPreferencesDropdown aiDifficulty;
  public Toggle tooltips;
  public Toggle autoERS;
  public Toggle autoOutlap;
  public Toggle blueFlag;
  public Toggle autosave;
  public UIPreferencesDropdown numberRollingSaves;
  public Toggle rollManualSaves;
  public Toggle useGamepad;
  public PreferencesScreen screen;
  private PreferencesManager mManager;

  public void OnStart()
  {
    this.mManager = App.instance.preferencesManager;
    this.language.OnStart();
    this.currencySymbol.OnStart();
    this.speed.OnStart();
    this.temperature.OnStart();
    this.dateFormat.OnStart();
    this.financesNegative.OnStart();
    this.weight.OnStart();
    this.height.OnStart();
    this.raceLenghts.OnStart();
    this.aiDifficulty.OnStart();
    this.numberRollingSaves.OnStart();
    GameUtility.SetActive(this.language.gameObject, false);
    this.currencySymbol.dropdown.onValueChanged.AddListener((UnityAction<int>) (param0 => this.SetSetting(Preference.pName.Game_CurrencySymbol, this.currencySymbol, UIPreferencesGameSettings.SettingType.pString)));
    this.temperature.dropdown.onValueChanged.AddListener((UnityAction<int>) (param0 => this.SetSetting(Preference.pName.Game_Temperature, this.temperature, UIPreferencesGameSettings.SettingType.pString)));
    this.speed.dropdown.onValueChanged.AddListener((UnityAction<int>) (param0 => this.SetSetting(Preference.pName.Game_Speed, this.speed, UIPreferencesGameSettings.SettingType.pString)));
    this.dateFormat.dropdown.onValueChanged.AddListener((UnityAction<int>) (param0 => this.SetSetting(Preference.pName.Game_DateFormat, this.dateFormat, UIPreferencesGameSettings.SettingType.pString)));
    this.weight.dropdown.onValueChanged.AddListener((UnityAction<int>) (param0 => this.SetSetting(Preference.pName.Game_Weight, this.weight, UIPreferencesGameSettings.SettingType.pString)));
    this.height.dropdown.onValueChanged.AddListener((UnityAction<int>) (param0 => this.SetSetting(Preference.pName.Game_Height, this.height, UIPreferencesGameSettings.SettingType.pString)));
    this.financesNegative.dropdown.onValueChanged.AddListener((UnityAction<int>) (param0 => this.SetSetting(Preference.pName.Game_FinancesNegative, this.financesNegative, UIPreferencesGameSettings.SettingType.pString)));
    this.languageFormat.onValueChanged.AddListener((UnityAction<int>) (param0 => this.SetSetting(Preference.pName.Game_LanguageFormat, this.languageFormat, UIPreferencesGameSettings.SettingType.pString)));
    this.raceLenghts.dropdown.onValueChanged.AddListener((UnityAction<int>) (param0 => this.SetSetting(Preference.pName.Game_RaceLenghts, this.raceLenghts, UIPreferencesGameSettings.SettingType.pString)));
    this.aiDifficulty.dropdown.onValueChanged.AddListener((UnityAction<int>) (param0 => this.SetSetting(Preference.pName.Game_AIDevDifficulty, this.aiDifficulty, UIPreferencesGameSettings.SettingType.pString)));
    this.autoERS.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetToggleSetting(Preference.pName.Game_AutoERS, this.autoERS)));
    this.autoOutlap.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetToggleSetting(Preference.pName.Game_AutoOutlap, this.autoOutlap)));
    this.blueFlag.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetToggleSetting(Preference.pName.Game_BlueFlags, this.blueFlag)));
    this.tooltips.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetToggleSetting(Preference.pName.Game_Tooltips, this.tooltips)));
    this.autosave.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetToggleSetting(Preference.pName.Game_Autosave, this.autosave)));
    this.numberRollingSaves.dropdown.onValueChanged.AddListener((UnityAction<int>) (param0 => this.SetSetting(Preference.pName.Game_NumberRollingAutosaves, this.numberRollingSaves, UIPreferencesGameSettings.SettingType.pInt)));
    this.rollManualSaves.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetToggleSetting(Preference.pName.Game_RollManualSaves, this.rollManualSaves)));
    this.useGamepad.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetToggleSetting(Preference.pName.Game_Gamepad, this.useGamepad)));
    this.Refresh();
  }

  public void Refresh()
  {
    this.ReadSetting(Preference.pName.Game_Language, this.language, UIPreferencesGameSettings.SettingType.pString);
    this.ReadSetting(Preference.pName.Game_CurrencySymbol, this.currencySymbol, UIPreferencesGameSettings.SettingType.pString);
    this.ReadSetting(Preference.pName.Game_Temperature, this.temperature, UIPreferencesGameSettings.SettingType.pString);
    this.ReadSetting(Preference.pName.Game_Speed, this.speed, UIPreferencesGameSettings.SettingType.pString);
    this.ReadSetting(Preference.pName.Game_DateFormat, this.dateFormat, UIPreferencesGameSettings.SettingType.pString);
    this.ReadSetting(Preference.pName.Game_Weight, this.weight, UIPreferencesGameSettings.SettingType.pString);
    this.ReadSetting(Preference.pName.Game_Height, this.height, UIPreferencesGameSettings.SettingType.pString);
    this.ReadSetting(Preference.pName.Game_FinancesNegative, this.financesNegative, UIPreferencesGameSettings.SettingType.pString);
    this.ReadSettingString(Preference.pName.Game_LanguageFormat, this.languageFormat);
    this.ReadSetting(Preference.pName.Game_RaceLenghts, this.raceLenghts, UIPreferencesGameSettings.SettingType.pString);
    this.ReadSetting(Preference.pName.Game_AIDevDifficulty, this.aiDifficulty, UIPreferencesGameSettings.SettingType.pString);
    this.ReadToggleSetting(Preference.pName.Game_AutoERS, this.autoERS);
    this.ReadToggleSetting(Preference.pName.Game_AutoOutlap, this.autoOutlap);
    this.ReadToggleSetting(Preference.pName.Game_BlueFlags, this.blueFlag);
    this.ReadToggleSetting(Preference.pName.Game_Tooltips, this.tooltips);
    this.ReadToggleSetting(Preference.pName.Game_Autosave, this.autosave);
    this.ReadSetting(Preference.pName.Game_NumberRollingAutosaves, this.numberRollingSaves, UIPreferencesGameSettings.SettingType.pInt);
    this.ReadToggleSetting(Preference.pName.Game_RollManualSaves, this.rollManualSaves);
    this.ReadToggleSetting(Preference.pName.Game_Gamepad, this.useGamepad);
    this.aiDifficulty.gameObject.SetActive(false);
  }

  public void RefreshUIDropdownLocalisation()
  {
    this.language.RefreshDropdownLocalisation();
    this.currencySymbol.RefreshDropdownLocalisation();
    this.speed.RefreshDropdownLocalisation();
    this.temperature.RefreshDropdownLocalisation();
    this.dateFormat.RefreshDropdownLocalisation();
    this.financesNegative.RefreshDropdownLocalisation();
    this.weight.RefreshDropdownLocalisation();
    this.height.RefreshDropdownLocalisation();
    this.raceLenghts.RefreshDropdownLocalisation();
    this.aiDifficulty.RefreshDropdownLocalisation();
    this.numberRollingSaves.RefreshDropdownLocalisation();
  }

  public void ConfirmSettings()
  {
    this.ChangeLanguage(this.language);
    this.ChangeCurrencySymbol(this.currencySymbol);
    this.ChangeTemperatureUnits(this.temperature);
    this.ChangeSpeedUnits(this.speed);
    this.ChangeDateFormat(this.dateFormat);
    this.ChangeHeightUnits(this.height);
    this.ChangeWeightUnits(this.weight);
    this.ChangeFinanceNegative(this.financesNegative);
    this.ChangeRaceLength(this.raceLenghts);
    this.ChangeAIDifficulty(this.aiDifficulty);
  }

  private void ReadSettingString(Preference.pName inName, Dropdown inDropDown)
  {
    string settingString = this.mManager.GetSettingString(inName, true);
    this.SetSettingValue(inDropDown, settingString);
  }

  private void ReadSetting(Preference.pName inName, UIPreferencesDropdown inDropDown, UIPreferencesGameSettings.SettingType inType = UIPreferencesGameSettings.SettingType.pString)
  {
    switch (inType)
    {
      case UIPreferencesGameSettings.SettingType.pString:
        string settingString = this.mManager.GetSettingString(inName, true);
        if (string.IsNullOrEmpty(settingString))
          break;
        inDropDown.SetValue(settingString);
        break;
      case UIPreferencesGameSettings.SettingType.pInt:
        int settingInt = this.mManager.GetSettingInt(inName, true);
        inDropDown.SetValue(settingInt.ToString());
        break;
    }
  }

  private void ReadToggleSetting(Preference.pName inName, Toggle inToggle)
  {
    inToggle.isOn = this.mManager.GetSettingBool(inName, true);
  }

  private void SetSettingValue(Dropdown inDropdown, string inValue)
  {
    for (int index = 0; index < inDropdown.get_options().Count; ++index)
    {
      if (inDropdown.get_options()[index].text.Equals(inValue, StringComparison.OrdinalIgnoreCase))
        inDropdown.value = index;
    }
  }

  private void SetSetting(Preference.pName inName, Dropdown inDropDown, UIPreferencesGameSettings.SettingType inValueType)
  {
    switch (inValueType)
    {
      case UIPreferencesGameSettings.SettingType.pString:
        this.mManager.SetSetting(inName, (object) inDropDown.get_options()[inDropDown.value].text, true);
        break;
      case UIPreferencesGameSettings.SettingType.pFloat:
        this.mManager.SetSetting(inName, (object) float.Parse(inDropDown.get_options()[inDropDown.value].text), true);
        break;
      case UIPreferencesGameSettings.SettingType.pInt:
        this.mManager.SetSetting(inName, (object) int.Parse(inDropDown.get_options()[inDropDown.value].text), true);
        break;
    }
    this.screen.UpdateSettingsChangedStates(true);
  }

  private void SetSetting(Preference.pName inName, UIPreferencesDropdown inDropDown, UIPreferencesGameSettings.SettingType inValueType)
  {
    bool flag = false;
    switch (inValueType)
    {
      case UIPreferencesGameSettings.SettingType.pString:
        flag = this.mManager.SetSetting(inName, (object) inDropDown.GetValue(), true);
        break;
      case UIPreferencesGameSettings.SettingType.pFloat:
        flag = this.mManager.SetSetting(inName, (object) float.Parse(inDropDown.GetValue()), true);
        break;
      case UIPreferencesGameSettings.SettingType.pInt:
        flag = this.mManager.SetSetting(inName, (object) int.Parse(inDropDown.GetValue()), true);
        break;
    }
    if (!flag)
      return;
    this.screen.UpdateSettingsChangedStates(true);
  }

  private void SetToggleSetting(Preference.pName inName, Toggle inToggle)
  {
    if (this.mManager.SetSetting(inName, (object) inToggle.isOn, true))
      this.screen.UpdateSettingsChangedStates(true);
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  private void ChangeLanguage(UIPreferencesDropdown inDropdown)
  {
    if (inDropdown.GetEnumValue<PrefGameLanguage.Type>().ToString().Equals(Localisation.currentLanguage, StringComparison.OrdinalIgnoreCase))
      return;
    this.mManager.gamePreferences.SetLanguage(inDropdown.GetEnumValue<PrefGameLanguage.Type>());
  }

  private void ChangeCurrencySymbol(UIPreferencesDropdown inDropdown)
  {
    this.mManager.gamePreferences.SetCurrencyLocaleString(inDropdown.GetEnumValue<PrefGameCurrencySymbol.Type>());
  }

  private void ChangeTemperatureUnits(UIPreferencesDropdown inDropdown)
  {
    this.mManager.gamePreferences.SetTemperatureUnits(inDropdown.GetEnumValue<PrefGameTemperatureUnits.Type>());
  }

  private void ChangeSpeedUnits(UIPreferencesDropdown inDropdown)
  {
    this.mManager.gamePreferences.SetSpeedUnits(inDropdown.GetEnumValue<PrefGameSpeedUnits.Type>());
  }

  private void ChangeFinanceNegative(UIPreferencesDropdown inDropdown)
  {
    this.mManager.gamePreferences.SetFinanceNegative(inDropdown.GetEnumValue<PrefGameFinancesNegative.Type>());
  }

  private void ChangeHeightUnits(UIPreferencesDropdown inDropdown)
  {
    this.mManager.gamePreferences.SetHeightUnits(inDropdown.GetEnumValue<PrefGameHeight.Type>());
  }

  private void ChangeWeightUnits(UIPreferencesDropdown inDropdown)
  {
    this.mManager.gamePreferences.SetWeightUnits(inDropdown.GetEnumValue<PrefGameWeight.Type>());
  }

  private void ChangeDateFormat(UIPreferencesDropdown inDropdown)
  {
    this.mManager.gamePreferences.SetDateFormatString(inDropdown.GetEnumValue<PrefGameDateFormat.Type>());
  }

  private void ChangeRaceLength(UIPreferencesDropdown inDropdown)
  {
    this.mManager.gamePreferences.SetRaceLength(inDropdown.GetEnumValue<PrefGameRaceLength.Type>());
  }

  private void ChangeAIDifficulty(UIPreferencesDropdown inDropdown)
  {
    this.mManager.gamePreferences.SetAIDevDifficulty(inDropdown.GetEnumValue<PrefGameAIDevDifficulty.Type>());
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
    pBool,
  }
}
