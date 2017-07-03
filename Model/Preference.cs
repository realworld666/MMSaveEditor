// Decompiled with JetBrains decompiler
// Type: Preference
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;

public class Preference
{
  public Preference.pName name = Preference.pName.Audio_MainVolume;
  public Preference.pType keyType = Preference.pType.pString;
  public string keyString = string.Empty;
  public Preference.pGroup group;
  public int keyInt;
  public float keyFloat;
  public bool keyBool;

  public Preference(Preference.pName inName)
  {
    this.name = inName;
  }

  public void SetValue(object inValue)
  {
    switch (this.keyType)
    {
      case Preference.pType.pInteger:
        this.keyInt = Convert.ToInt32(inValue);
        break;
      case Preference.pType.pString:
        this.keyString = Convert.ToString(inValue);
        break;
      case Preference.pType.pFloat:
        this.keyFloat = Convert.ToSingle(inValue);
        break;
      case Preference.pType.pBool:
        this.keyBool = Convert.ToBoolean(inValue);
        break;
    }
  }

  public void SetValues(Preference inOther)
  {
    this.keyInt = inOther.keyInt;
    this.keyFloat = inOther.keyFloat;
    this.keyString = inOther.keyString;
    this.keyBool = inOther.keyBool;
  }

  public void SetValues(Preference.pGroup inGroup, Preference.pType inType, object inValue)
  {
    this.group = inGroup;
    this.keyType = inType;
    switch (this.keyType)
    {
      case Preference.pType.pInteger:
        this.keyInt = Convert.ToInt32(inValue);
        break;
      case Preference.pType.pString:
        this.keyString = Convert.ToString(inValue);
        break;
      case Preference.pType.pFloat:
        this.keyFloat = Convert.ToSingle(inValue);
        break;
      case Preference.pType.pBool:
        this.keyBool = Convert.ToBoolean(inValue);
        break;
    }
  }

  public string prefToString()
  {
    switch (this.keyType)
    {
      case Preference.pType.pInteger:
        return "-" + this.name.ToString() + ":" + this.group.ToString() + ":" + this.keyType.ToString() + ":" + this.keyInt.ToString();
      case Preference.pType.pString:
        return "-" + this.name.ToString() + ":" + this.group.ToString() + ":" + this.keyType.ToString() + ":" + this.keyString;
      case Preference.pType.pFloat:
        return "-" + this.name.ToString() + ":" + this.group.ToString() + ":" + this.keyType.ToString() + ":" + this.keyFloat.ToString();
      case Preference.pType.pBool:
        return "-" + this.name.ToString() + ":" + this.group.ToString() + ":" + this.keyType.ToString() + ":" + this.keyBool.ToString();
      default:
        return string.Empty;
    }
  }

  public string prefToConsole()
  {
    switch (this.keyType)
    {
      case Preference.pType.pInteger:
        return "->" + this.name.ToString() + " - " + this.keyInt.ToString();
      case Preference.pType.pString:
        return "->" + this.name.ToString() + " - " + this.keyString;
      case Preference.pType.pFloat:
        return "->" + this.name.ToString() + " - " + this.keyFloat.ToString();
      case Preference.pType.pBool:
        return "->" + this.name.ToString() + " - " + this.keyBool.ToString();
      default:
        return string.Empty;
    }
  }

  public enum pName
  {
    Game_Language = 0,
    Game_CurrencySymbol = 1,
    Game_Temperature = 2,
    Game_Speed = 3,
    Game_DateFormat = 4,
    Game_Weight = 5,
    Game_Height = 6,
    Game_FinancesNegative = 8,
    Game_LanguageFormat = 9,
    Game_RaceLenghts = 12,
    Game_Tooltips = 13,
    Game_NumberRollingAutosaves = 14,
    Game_Autosave = 15,
    Game_PauseGameOnLosingFocus = 16,
    Video_Preset = 17,
    Video_Display = 18,
    Video_Resolution = 19,
    Video_Vsync = 20,
    Video_WorldDetail = 21,
    Video_Traffic = 22,
    Video_Crowds = 23,
    Video_Decals = 24,
    Video_VFX = 25,
    Video_TiltShift = 33,
    Video_AmbientOcclusion = 34,
    Video_Contrast = 38,
    Video_Brightness = 39,
    Audio_MainVolume = 40,
    Audio_SfxVolume = 41,
    Audio_UIVolume = 42,
    Audio_MusicVolume = 43,
    Control_Pause = 44,
    Control_Speedx1 = 45,
    Control_Speedx2 = 46,
    Control_Speedx3 = 47,
    Control_Home = 48,
    Control_Profile = 49,
    Control_Factory = 50,
    Control_Car = 51,
    Control_Standings = 52,
    Control_Headquarters = 53,
    Control_RotateLeft = 58,
    Control_RotateRight = 59,
    Control_ZoomIn = 62,
    Control_ZoomOut = 63,
    Control_SessionStandings = 69,
    Control_WeatherConditions = 70,
    Control_WaterConditions = 71,
    Control_RubberConditions = 72,
    Control_DataCentre = 73,
    Game_RollManualSaves = 74,
    Video_UnityQuality = 75,
    Video_AntiAliasing = 76,
    Video_RunInBackground = 79,
    Game_SteamLanguage = 80,
    Video_SSR = 81,
    Video_ToneMapping = 82,
    Video_Bloom = 83,
    Video_VignetteChromaticAbberation = 84,
    Video_DOF = 85,
    Video_WaterQuality = 86,
    Video_DynamicLights = 87,
    Game_Gamepad = 88,
    Video_3D = 89,
    Game_AIDevDifficulty = 90,
    Control_ChangeViewpoint = 91,
    Game_AutoERS = 92,
    Game_AutoOutlap = 93,
    Game_BlueFlags = 94,
  }

  public enum pGroup
  {
    Game,
    Video,
    Audio,
    Controls,
  }

  public enum pType
  {
    pInteger,
    pString,
    pFloat,
    pBool,
  }
}
