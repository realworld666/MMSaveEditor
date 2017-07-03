// Decompiled with JetBrains decompiler
// Type: PreferencesManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Wenzil.Console;

public class PreferencesManager
{
  private static readonly int mPreferenceCount = Enum.GetNames(typeof (Preference.pName)).Length;
  private static readonly int[] mPreferenceValues = Enum.GetValues(typeof (Preference.pName)).Cast<int>().ToArray<int>();
  public GamePreferences gamePreferences = new GamePreferences();
  public VideoPreferences videoPreferences = new VideoPreferences();
  public AudioPreferences audioPreferences = new AudioPreferences();
  public ControlPreferences controlPreferences = new ControlPreferences();
  private Dictionary<int, Preference> mPlayerPreferences = new Dictionary<int, Preference>();
  private Dictionary<int, Preference> mChangedPreferences = new Dictionary<int, Preference>();

  public void OnStart()
  {
    this.ValidatePreferenceEnum();
    this.CreateDictionaries();
    if (!this.ReadSettings())
    {
      switch (this.GetSettingEnum<PrefVideoUnityQuality.Type>(Preference.pName.Video_UnityQuality, false))
      {
        case PrefVideoUnityQuality.Type.High:
          this.LoadPresetOptions(DefaultPreferences.preset_High);
          break;
        case PrefVideoUnityQuality.Type.MediumHigh:
          this.LoadPresetOptions(DefaultPreferences.preset_MediumHigh);
          break;
        case PrefVideoUnityQuality.Type.MediumLow:
          this.LoadPresetOptions(DefaultPreferences.preset_MediumLow);
          break;
        case PrefVideoUnityQuality.Type.Low:
          this.LoadPresetOptions(DefaultPreferences.preset_Low);
          break;
      }
      this.ApplyChanges();
    }
    this.StartAllPreferences();
    this.LoadAllPreferences();
    ConsoleCommandsDatabase.RegisterCommand("ListPreferences", new ConsoleCommandCallback(this.ConsoleListPreferences), "List preferences settings", "ListPreferences");
    this.HandleSteamLanguage();
  }

  public void OnApplicationQuit()
  {
    if (this.videoPreferences.currentFullscreenMode == Screen.fullScreen)
      return;
    this.videoPreferences.currentFullscreenMode = Screen.fullScreen;
    this.SetSetting(Preference.pName.Video_Display, !Screen.fullScreen ? (object) "Windowed" : (object) "Fullscreen", false);
    this.SaveSettings();
  }

  private void ValidatePreferenceEnum()
  {
    int num = ((IEnumerable<int>) PreferencesManager.mPreferenceValues).Distinct<int>().Count<int>();
    if (PreferencesManager.mPreferenceValues.Length == num)
      return;
    Debug.LogErrorFormat("Error: Duplicate values in preference pname enum: Length {0} contains {1} distinct values", new object[2]
    {
      (object) PreferencesManager.mPreferenceValues.Length,
      (object) num
    });
  }

  private void CreateDictionaries()
  {
    for (int index = 0; index < PreferencesManager.mPreferenceCount; ++index)
    {
      Preference.pName mPreferenceValue = (Preference.pName) PreferencesManager.mPreferenceValues[index];
      this.mPlayerPreferences.Add((int) mPreferenceValue, new Preference(mPreferenceValue));
      this.mChangedPreferences.Add((int) mPreferenceValue, new Preference(mPreferenceValue));
    }
  }

  public void LoadPreset(PrefVideoPreset.Type inPreset)
  {
    switch (inPreset)
    {
      case PrefVideoPreset.Type.High:
        this.LoadPresetOptions(DefaultPreferences.preset_High);
        break;
      case PrefVideoPreset.Type.MediumHigh:
        this.LoadPresetOptions(DefaultPreferences.preset_MediumHigh);
        break;
      case PrefVideoPreset.Type.MediumLow:
        this.LoadPresetOptions(DefaultPreferences.preset_MediumLow);
        break;
      case PrefVideoPreset.Type.Low:
        this.LoadPresetOptions(DefaultPreferences.preset_Low);
        break;
    }
  }

  private void LoadPresetOptions(Dictionary<Preference.pName, object> inPresetList)
  {
    Preference.pName[] array = new Preference.pName[inPresetList.Count];
    inPresetList.Keys.CopyTo(array, 0);
    for (int index1 = 0; index1 < array.Length; ++index1)
    {
      Preference.pName index2 = array[index1];
      this.mChangedPreferences[(int) index2].SetValue(inPresetList[index2]);
    }
  }

  private void SetDefaultSettings(bool inChangedSettings)
  {
    for (int index = 0; index < PreferencesManager.mPreferenceCount; ++index)
    {
      Preference.pName mPreferenceValue = (Preference.pName) PreferencesManager.mPreferenceValues[index];
      Preference preference1 = !inChangedSettings ? this.GetPreference(mPreferenceValue) : this.GetChangedPreference(mPreferenceValue);
      DefaultPreferences.PStruct preference2 = DefaultPreferences.preferences[mPreferenceValue];
      preference1.SetValues(preference2.group, preference2.type, preference2.value);
    }
    this.LoadPreset(this.GetSettingEnum<PrefVideoPreset.Type>(Preference.pName.Video_Preset, true));
  }

  private void SetGroupDefaultSettings(Preference.pGroup inGroup, bool inChangedSettings)
  {
    for (int index = 0; index < PreferencesManager.mPreferenceCount; ++index)
    {
      Preference.pName mPreferenceValue = (Preference.pName) PreferencesManager.mPreferenceValues[index];
      Preference preference1 = !inChangedSettings ? this.GetPreference(mPreferenceValue) : this.GetChangedPreference(mPreferenceValue);
      if (preference1.group == inGroup)
      {
        DefaultPreferences.PStruct preference2 = DefaultPreferences.preferences[mPreferenceValue];
        preference1.SetValues(preference2.group, preference2.type, preference2.value);
      }
    }
    if (inGroup == Preference.pGroup.Video)
      this.LoadPreset(this.GetSettingEnum<PrefVideoPreset.Type>(Preference.pName.Video_Preset, true));
    if (inChangedSettings)
      return;
    this.SaveSettings();
  }

  private void StartAllPreferences()
  {
    this.videoPreferences.Start(this);
    this.audioPreferences.Start(this);
    this.controlPreferences.Start(this);
    this.gamePreferences.Start(this);
  }

  private void LoadAllPreferences()
  {
    this.videoPreferences.Load();
    this.audioPreferences.Load();
    if (!this.controlPreferences.Load())
    {
      Debug.Log((object) "Couldn't load controls, falling back to defaults", (UnityEngine.Object) null);
      this.ResetGroupDefault(Preference.pGroup.Controls);
    }
    this.gamePreferences.Load();
    ColorPreferences.LoadCustomColors();
  }

  public void ResetGroupDefault(Preference.pGroup inGroup)
  {
    this.SetGroupDefaultSettings(inGroup, true);
  }

  public void ApplyChanges()
  {
    this.ClonePreferences(this.mPlayerPreferences, this.mChangedPreferences);
    this.SaveSettings();
  }

  public void CancelChanges()
  {
    this.ClonePreferences(this.mChangedPreferences, this.mPlayerPreferences);
  }

  private void ClonePreferences(Dictionary<int, Preference> inBase, Dictionary<int, Preference> inCloned)
  {
    for (int index = 0; index < PreferencesManager.mPreferenceCount; ++index)
    {
      Preference.pName mPreferenceValue = (Preference.pName) PreferencesManager.mPreferenceValues[index];
      inBase[(int) mPreferenceValue].SetValues(inCloned[(int) mPreferenceValue]);
    }
  }

  private Preference GetPreference(Preference.pName inName)
  {
    if (this.mPlayerPreferences.ContainsKey((int) inName))
      return this.mPlayerPreferences[(int) inName];
    return (Preference) null;
  }

  private Preference GetChangedPreference(Preference.pName inName)
  {
    if (this.mChangedPreferences.ContainsKey((int) inName))
      return this.mChangedPreferences[(int) inName];
    return (Preference) null;
  }

  public bool SetSetting(Preference.pName inKeyName, object inValue, bool inChangedValue)
  {
    Preference preference = !inChangedValue ? this.mPlayerPreferences[(int) inKeyName] : this.mChangedPreferences[(int) inKeyName];
    switch (preference.keyType)
    {
      case Preference.pType.pInteger:
        int keyInt = preference.keyInt;
        preference.keyInt = !(inValue is int) ? 0 : Convert.ToInt32(inValue);
        return keyInt != preference.keyInt;
      case Preference.pType.pString:
        string keyString = preference.keyString;
        preference.keyString = inValue is string || inValue is Enum ? Convert.ToString(inValue) : string.Empty;
        return keyString != preference.keyString;
      case Preference.pType.pFloat:
        float keyFloat = preference.keyFloat;
        preference.keyFloat = !(inValue is float) ? 0.0f : Convert.ToSingle(inValue);
        return (double) keyFloat != (double) preference.keyFloat;
      case Preference.pType.pBool:
        bool keyBool = preference.keyBool;
        preference.keyBool = inValue is bool && Convert.ToBoolean(inValue);
        return keyBool != preference.keyBool;
      default:
        return false;
    }
  }

  public bool GetSettingBool(Preference.pName inName, bool inChangedValue)
  {
    Preference preference = !inChangedValue ? this.GetPreference(inName) : this.GetChangedPreference(inName);
    if (preference != null)
      return preference.keyBool;
    Debug.LogErrorFormat(" Could not find requested bool: {0}", (object) inName);
    return false;
  }

  public int GetSettingInt(Preference.pName inName, bool inChangedValue)
  {
    Preference preference = !inChangedValue ? this.GetPreference(inName) : this.GetChangedPreference(inName);
    if (preference != null)
      return preference.keyInt;
    Debug.LogErrorFormat(" Could not find requested int: {0}", (object) inName);
    return 0;
  }

  public float GetSettingFloat(Preference.pName inName, bool inChangedValue)
  {
    Preference preference = !inChangedValue ? this.GetPreference(inName) : this.GetChangedPreference(inName);
    if (preference != null)
      return preference.keyFloat;
    Debug.LogErrorFormat(" Could not find requested float: {0}", (object) inName);
    return 0.0f;
  }

  public string GetSettingString(Preference.pName inName, bool inChangedValue)
  {
    Preference preference = !inChangedValue ? this.GetPreference(inName) : this.GetChangedPreference(inName);
    if (preference != null)
      return preference.keyString;
    Debug.LogErrorFormat(" Could not find requested string: {0}", (object) inName);
    return string.Empty;
  }

  public T GetSettingEnum<T>(Preference.pName inName, bool inChangedValue) where T : struct
  {
    string settingString = this.GetSettingString(inName, inChangedValue);
    try
    {
      return (T) Enum.Parse(typeof (T), settingString, true);
    }
    catch
    {
      Debug.LogErrorFormat(" Could not find requested enum: {0}", (object) inName);
      return default (T);
    }
  }

  private bool ReadSettings()
  {
    bool flag = false;
    this.SetDefaultSettings(true);
    this.SetDefaultSettings(false);
    if (PlayerPrefs.HasKey("Preferences"))
    {
      string[] strArray1 = PlayerPrefs.GetString("Preferences").Split('-');
      if (strArray1.Length > 0)
      {
        for (int index = 0; index < strArray1.Length; ++index)
        {
          if (!string.IsNullOrEmpty(strArray1[index]))
          {
            string[] strArray2 = strArray1[index].Split(':');
            if (strArray2.Length == 4 && Enum.IsDefined(typeof (Preference.pName), (object) strArray2[0]))
              this.mPlayerPreferences[(int) Enum.Parse(typeof (Preference.pName), strArray2[0])].SetValues((Preference.pGroup) Enum.Parse(typeof (Preference.pGroup), strArray2[1]), (Preference.pType) Enum.Parse(typeof (Preference.pType), strArray2[2]), (object) strArray2[3]);
            else if (!flag)
              flag = true;
          }
        }
        if (strArray1.Length != this.mChangedPreferences.Count && !flag)
          flag = true;
        this.ClonePreferences(this.mChangedPreferences, this.mPlayerPreferences);
        if (flag)
          this.SaveSettings();
        return true;
      }
    }
    return false;
  }

  private void SaveSettings()
  {
    string empty = string.Empty;
    for (int index = 0; index < PreferencesManager.mPreferenceCount; ++index)
    {
      Preference.pName mPreferenceValue = (Preference.pName) PreferencesManager.mPreferenceValues[index];
      empty += this.mPlayerPreferences[(int) mPreferenceValue].prefToString();
    }
    PlayerPrefs.SetString("Preferences", empty);
    PlayerPrefs.Save();
  }

  private void ClearSettings()
  {
    PlayerPrefs.DeleteAll();
    PlayerPrefs.Save();
  }

  public float GetSliderValue(int inDropdownValue)
  {
    return (float) inDropdownValue / 100f;
  }

  private ConsoleCommandResult ConsoleListPreferences(string[] args)
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < PreferencesManager.mPreferenceCount; ++index)
    {
      Preference.pName mPreferenceValue = (Preference.pName) PreferencesManager.mPreferenceValues[index];
      stringBuilder.AppendLine(this.mPlayerPreferences[(int) mPreferenceValue].prefToConsole());
    }
    return ConsoleCommandResult.Succeeded(stringBuilder.ToString());
  }

  private void HandleSteamLanguage()
  {
    string result;
    if (!SteamManager.Initialized || !Localisation.ConvertStringToLanguage(SteamApps.GetCurrentGameLanguage(), out result))
      return;
    string settingString = this.GetSettingString(Preference.pName.Game_SteamLanguage, false);
    if (!(result != settingString))
      return;
    Localisation.currentLanguage = result;
    this.SetSetting(Preference.pName.Game_SteamLanguage, (object) result, false);
    this.SetSetting(Preference.pName.Game_Language, (object) result, false);
    this.SaveSettings();
  }
}
