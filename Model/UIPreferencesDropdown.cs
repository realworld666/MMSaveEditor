// Decompiled with JetBrains decompiler
// Type: UIPreferencesDropdown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPreferencesDropdown : MonoBehaviour
{
  public Preference.pName prefName = Preference.pName.Video_Display;
  private bool mAllowMenuSounds = true;
  public Dropdown dropdown;
  private Enum mEnum;
  private bool mChangesLength;
  private bool mAddedListener_OnDropDownValueChanged;

  public bool allowMenuSounds
  {
    get
    {
      return this.mAllowMenuSounds;
    }
    set
    {
      this.mAllowMenuSounds = value;
    }
  }

  public void OnStart()
  {
    Preference.pName prefName = this.prefName;
    switch (prefName)
    {
      case Preference.pName.Game_Language:
        this.mEnum = (Enum) PrefGameLanguage.Type.English;
        this.CreateDropdownEnum(this.mEnum);
        break;
      case Preference.pName.Game_CurrencySymbol:
        this.mEnum = (Enum) PrefGameCurrencySymbol.Type.GBP;
        this.CreateDropdownEnum(this.mEnum);
        break;
      case Preference.pName.Game_Temperature:
        this.mEnum = (Enum) PrefGameTemperatureUnits.Type.Celsius;
        this.CreateDropdownEnum(this.mEnum);
        break;
      case Preference.pName.Game_Speed:
        this.mEnum = (Enum) PrefGameSpeedUnits.Type.KMPH;
        this.CreateDropdownEnum(this.mEnum);
        break;
      case Preference.pName.Game_DateFormat:
        this.mEnum = (Enum) PrefGameDateFormat.Type.DDMMYYYY;
        this.CreateDropdownEnum(this.mEnum);
        break;
      case Preference.pName.Game_Weight:
        this.mEnum = (Enum) PrefGameWeight.Type.kg;
        this.CreateDropdownEnum(this.mEnum);
        break;
      case Preference.pName.Game_Height:
        this.mEnum = (Enum) PrefGameHeight.Type.ftInches;
        this.CreateDropdownEnum(this.mEnum);
        break;
      case Preference.pName.Game_FinancesNegative:
        this.mEnum = (Enum) PrefGameFinancesNegative.Type.MinusSign;
        this.CreateDropdownEnum(this.mEnum);
        break;
      case Preference.pName.Game_RaceLenghts:
        this.mEnum = (Enum) PrefGameRaceLength.Type.Medium;
        this.CreateDropdownEnum(this.mEnum);
        break;
      case Preference.pName.Game_NumberRollingAutosaves:
        this.CreateDropdownInt(PrefGameNumberRollingSaves.Type);
        break;
      case Preference.pName.Video_Preset:
        this.mEnum = (Enum) PrefVideoPreset.Type.High;
        this.CreateDropdownEnum(this.mEnum);
        this.mChangesLength = true;
        break;
      case Preference.pName.Video_Display:
        this.mEnum = (Enum) PrefVideoDisplay.Type.Fullscreen;
        this.CreateDropdownEnum(this.mEnum);
        break;
      case Preference.pName.Video_Resolution:
        this.CreateDropdownString(App.instance.preferencesManager.videoPreferences.GetScreenResolutionList());
        break;
      default:
        if (prefName != Preference.pName.Video_UnityQuality)
        {
          if (prefName == Preference.pName.Game_AIDevDifficulty)
          {
            this.mEnum = (Enum) PrefGameAIDevDifficulty.Type.Realistic;
            this.CreateDropdownEnum(this.mEnum);
            break;
          }
          break;
        }
        this.mEnum = (Enum) PrefVideoUnityQuality.Type.High;
        this.CreateDropdownEnum(this.mEnum);
        break;
    }
    if (this.mAddedListener_OnDropDownValueChanged)
      return;
    this.mAddedListener_OnDropDownValueChanged = true;
    this.dropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnDropDownValueChanged));
  }

  private void OnDropDownValueChanged(int index)
  {
    if (!this.allowMenuSounds)
      return;
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  public string GetValue()
  {
    Preference.pName prefName = this.prefName;
    switch (prefName)
    {
      case Preference.pName.Game_Language:
        return ((PrefGameLanguage.Type) this.dropdown.value).ToString();
      case Preference.pName.Game_CurrencySymbol:
        return ((PrefGameCurrencySymbol.Type) this.dropdown.value).ToString();
      case Preference.pName.Game_Temperature:
        return ((PrefGameTemperatureUnits.Type) this.dropdown.value).ToString();
      case Preference.pName.Game_Speed:
        return ((PrefGameSpeedUnits.Type) this.dropdown.value).ToString();
      case Preference.pName.Game_DateFormat:
        return ((PrefGameDateFormat.Type) this.dropdown.value).ToString();
      case Preference.pName.Game_Weight:
        return ((PrefGameWeight.Type) this.dropdown.value).ToString();
      case Preference.pName.Game_Height:
        return ((PrefGameHeight.Type) this.dropdown.value).ToString();
      case Preference.pName.Game_FinancesNegative:
        return ((PrefGameFinancesNegative.Type) this.dropdown.value).ToString();
      case Preference.pName.Game_RaceLenghts:
        return ((PrefGameRaceLength.Type) this.dropdown.value).ToString();
      case Preference.pName.Game_NumberRollingAutosaves:
        return this.dropdown.get_options()[this.dropdown.value].text;
      case Preference.pName.Video_Preset:
        return ((PrefVideoPreset.Type) this.dropdown.value).ToString();
      case Preference.pName.Video_Display:
        return ((PrefVideoDisplay.Type) this.dropdown.value).ToString();
      case Preference.pName.Video_Resolution:
        return this.dropdown.get_options()[this.dropdown.value].text;
      default:
        if (prefName == Preference.pName.Video_UnityQuality)
          return ((PrefVideoUnityQuality.Type) this.dropdown.value).ToString();
        if (prefName == Preference.pName.Game_AIDevDifficulty)
          return ((PrefGameAIDevDifficulty.Type) this.dropdown.value).ToString();
        return (string) null;
    }
  }

  public T GetEnumValue<T>() where T : struct
  {
    return (T) Enum.Parse(typeof (T), this.GetValue());
  }

  public void SetValue(string inValue)
  {
    Preference.pName prefName = this.prefName;
    switch (prefName)
    {
      case Preference.pName.Game_Language:
        this.SetDropdownEnum((Enum) PrefGameLanguage.Type.English, inValue);
        break;
      case Preference.pName.Game_CurrencySymbol:
        this.SetDropdownEnum((Enum) PrefGameCurrencySymbol.Type.GBP, inValue);
        break;
      case Preference.pName.Game_Temperature:
        this.SetDropdownEnum((Enum) PrefGameTemperatureUnits.Type.Celsius, inValue);
        break;
      case Preference.pName.Game_Speed:
        this.SetDropdownEnum((Enum) PrefGameSpeedUnits.Type.KMPH, inValue);
        break;
      case Preference.pName.Game_DateFormat:
        this.SetDropdownEnum((Enum) PrefGameDateFormat.Type.DDMMYYYY, inValue);
        break;
      case Preference.pName.Game_Weight:
        this.SetDropdownEnum((Enum) PrefGameWeight.Type.kg, inValue);
        break;
      case Preference.pName.Game_Height:
        this.SetDropdownEnum((Enum) PrefGameHeight.Type.ftInches, inValue);
        break;
      case Preference.pName.Game_FinancesNegative:
        this.SetDropdownEnum((Enum) PrefGameFinancesNegative.Type.MinusSign, inValue);
        break;
      case Preference.pName.Game_RaceLenghts:
        this.SetDropdownEnum((Enum) PrefGameRaceLength.Type.Medium, inValue);
        break;
      case Preference.pName.Game_NumberRollingAutosaves:
        this.SetDropdownInt(PrefGameNumberRollingSaves.Type, inValue);
        break;
      case Preference.pName.Video_Preset:
        this.SetDropdownEnum((Enum) PrefVideoPreset.Type.High, inValue);
        break;
      case Preference.pName.Video_Display:
        this.SetDropdownEnum((Enum) PrefVideoDisplay.Type.Fullscreen, inValue);
        break;
      case Preference.pName.Video_Resolution:
        this.SetDropdownString(App.instance.preferencesManager.videoPreferences.GetScreenResolutionList(), inValue);
        break;
      default:
        if (prefName != Preference.pName.Video_UnityQuality)
        {
          if (prefName != Preference.pName.Game_AIDevDifficulty)
            break;
          this.SetDropdownEnum((Enum) PrefGameAIDevDifficulty.Type.Slowed, inValue);
          break;
        }
        this.SetDropdownEnum((Enum) PrefVideoUnityQuality.Type.High, inValue);
        break;
    }
  }

  private void SetDropdownEnum(Enum inEnum, string inValue)
  {
    List<string> stringList = new List<string>();
    foreach (object obj in Enum.GetValues(inEnum.GetType()))
      stringList.Add(obj.ToString());
    int count = stringList.Count;
    for (int index = 0; index < count; ++index)
    {
      if (stringList[index] == inValue && index < this.dropdown.get_options().Count)
      {
        this.dropdown.value = index;
        this.dropdown.RefreshShownValue();
        break;
      }
    }
  }

  private void SetDropdownString(string[] inArray, string inValue)
  {
    for (int index = 0; index < inArray.Length; ++index)
    {
      if (inArray[index] == inValue && index < this.dropdown.get_options().Count)
      {
        this.dropdown.value = index;
        this.dropdown.RefreshShownValue();
        break;
      }
    }
  }

  private void SetDropdownInt(int[] inArray, string inValueString)
  {
    for (int index = 0; index < inArray.Length; ++index)
    {
      if (inArray[index].ToString() == inValueString && index < this.dropdown.get_options().Count)
      {
        this.dropdown.value = index;
        this.dropdown.RefreshShownValue();
        break;
      }
    }
  }

  private void CreateDropdownEnum(Enum inEnum)
  {
    StringVariableParser.nationalityGender = "Male";
    this.dropdown.ClearOptions();
    foreach (Enum inEnum1 in Enum.GetValues(inEnum.GetType()))
      this.dropdown.get_options().Add(new Dropdown.OptionData(Localisation.LocaliseEnum(inEnum1)));
    this.dropdown.value = 0;
    this.dropdown.RefreshShownValue();
  }

  public void RefreshDropdownLocalisation()
  {
    if (this.mEnum == null)
      return;
    Array values = Enum.GetValues(this.mEnum.GetType());
    if (values.Length != this.dropdown.get_options().Count && !this.mChangesLength)
    {
      Debug.LogWarningFormat("Could not refresh the dropdown for {0}", (object) this.mEnum);
    }
    else
    {
      for (int index = 0; index < this.dropdown.get_options().Count; ++index)
      {
        Dropdown.OptionData optionData = new Dropdown.OptionData(Localisation.LocaliseEnum((Enum) values.GetValue(index)));
        this.dropdown.get_options()[index] = optionData;
      }
      this.dropdown.RefreshShownValue();
    }
  }

  private void CreateDropdownString(string[] inArray)
  {
    this.dropdown.ClearOptions();
    for (int index = 0; index < inArray.Length; ++index)
    {
      string text = inArray[index];
      if (!string.IsNullOrEmpty(text))
        this.dropdown.get_options().Add(new Dropdown.OptionData(text));
    }
    this.dropdown.value = 0;
    this.dropdown.RefreshShownValue();
  }

  private void CreateDropdownInt(int[] inArray)
  {
    this.dropdown.ClearOptions();
    for (int index = 0; index < inArray.Length; ++index)
      this.dropdown.get_options().Add(new Dropdown.OptionData(inArray[index].ToString()));
    this.dropdown.value = 0;
    this.dropdown.RefreshShownValue();
  }

  public void ResetDropdown()
  {
    if (!((UnityEngine.Object) this.dropdown != (UnityEngine.Object) null))
      return;
    Transform transform = this.dropdown.transform.Find("Dropdown List");
    if (!((UnityEngine.Object) transform != (UnityEngine.Object) null))
      return;
    UnityEngine.Object.Destroy((UnityEngine.Object) transform.gameObject);
  }

  public bool isDropdownActive()
  {
    if ((UnityEngine.Object) this.dropdown != (UnityEngine.Object) null)
    {
      Transform transform = this.dropdown.transform.Find("Dropdown List");
      if ((UnityEngine.Object) transform != (UnityEngine.Object) null)
        return transform.gameObject.activeSelf;
    }
    return false;
  }
}
