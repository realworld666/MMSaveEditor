// Decompiled with JetBrains decompiler
// Type: Localisation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using Steamworks;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

public class Localisation
{
  public static Action OnLanguageChange = (Action) null;
  public static string[] localisationSheets = new string[13]{ "Data/Localisation/Dilemmas", "Data/Localisation/Frontend", "Data/Localisation/MediaReports", "Data/Localisation/Interviews", "Data/Localisation/Tweets", "Data/Localisation/Messages", "Data/Localisation/PreRaceTalk", "Data/Localisation/RaceEvent", "Data/Localisation/Shared", "Data/Localisation/Simulation", "Data/Localisation/TeamRadio", "Data/Localisation/Tutorials", "Data/Localisation/Nationalities" };
  public static string[] supportedLanguages = new string[10]{ "English", "French", "Italian", "German", "Spanish", "Brasilian", "Dutch", "Hungarian", "Polish", "Russian" };
  private static string mCurrentLanguage = Localisation.supportedLanguages[0];
  private static int mCurrentLanguageIndex = 0;
  private static Localisation.LapTimeFormatting mLapTimeFormatting = Localisation.LapTimeFormatting.English;
  private static NumberFormatInfo mNumberFormatter = new NumberFormatInfo();
  private static bool mUseCommaDecimalSeperator = false;
  private static Dictionary<string, LocalisationEntry> mEntries = (Dictionary<string, LocalisationEntry>) null;
  private static Dictionary<string, LocalisationGroup> mGroups = (Dictionary<string, LocalisationGroup>) null;
  private const string LapTimeFormatEnglishShort = "0.000";
  private const string LapTimeFormatEnglishLong = "0:00.000";
  private const string LapTimeFormatSpanishShort = "0,000";
  private const string LapTimeFormatSpanishLong = "0:00:000";
  private const string LapTimeFormatOtherShort = "0,000";
  private const string LapTimeFormatOtherLong = "0:00,000";

  public static string currentLanguage
  {
    get
    {
      return Localisation.mCurrentLanguage;
    }
    set
    {
      string result;
      if (!Localisation.ConvertStringToLanguage(value, out result))
      {
        Debug.LogWarningFormat("Cannot determine language from {0}", (object) value);
      }
      else
      {
        if (!(Localisation.mCurrentLanguage != result))
          return;
        Localisation.mCurrentLanguage = result;
        Localisation.UpdateCurrentLanguageIndex();
        Localisation.UpdateNumberFormattingEnums();
        if (Localisation.OnLanguageChange == null)
          return;
        Localisation.OnLanguageChange();
      }
    }
  }

  public static int currentLanguageIndex
  {
    get
    {
      return Localisation.mCurrentLanguageIndex;
    }
  }

  public static NumberFormatInfo numberFormatter
  {
    get
    {
      return Localisation.mNumberFormatter;
    }
  }

  public static bool UseCommaDecimalSeperator
  {
    get
    {
      return Localisation.mUseCommaDecimalSeperator;
    }
  }

  public static void Start()
  {
    Localisation.currentLanguage = !SteamManager.Initialized ? ((Enum) Application.systemLanguage).ToString() : SteamApps.GetCurrentGameLanguage();
    Localisation.LoadData();
    if (Localisation.OnLanguageChange == null)
      return;
    Localisation.OnLanguageChange();
  }

  public static void LoadData()
  {
    if (Localisation.mEntries != null || Localisation.mGroups != null)
      return;
    Localisation.mEntries = new Dictionary<string, LocalisationEntry>();
    Localisation.mGroups = new Dictionary<string, LocalisationGroup>();
    for (int index = 0; index < Localisation.localisationSheets.Length; ++index)
      LocalisationReader.LoadFromFile(Localisation.localisationSheets[index], Localisation.mEntries, Localisation.mGroups);
  }

  public static string LocaliseID(string inID, GameObject inObject = null)
  {
    if (inID.Length != "PSG_10000001".Length)
      return "<color=yellow>PSG ID not set correctly</color>";
    return Localisation.LocaliseID(inID, Localisation.currentLanguage, inObject, string.Empty);
  }

  public static string LocaliseID(string inID, string inLanguage, GameObject inObject = null, string inGender = "")
  {
    string text = Localisation.GetText(inID, inLanguage);
    string inString = inGender == "Male" || inGender == "Female" ? StringVariableParser.GetGenderText(inID, text, inLanguage, inObject, inGender) : StringVariableParser.GetGenderText(inID, text, inLanguage, inObject, string.Empty);
    return Localisation.GetSpecialStringsData(inID, inString, inLanguage);
  }

  private static string GetText(string inID, string inLanguage)
  {
    using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
    {
      StringBuilder stringBuilder = builderSafe.stringBuilder;
      if (Localisation.mEntries.ContainsKey(inID))
      {
        LocalisationEntry mEntry = Localisation.mEntries[inID];
        if (!mEntry.text.ContainsKey(inLanguage))
        {
          Debug.Log((object) ("Language Not Present: " + inLanguage), (UnityEngine.Object) null);
          Debug.Log((object) "Defaulting to English", (UnityEngine.Object) null);
          inLanguage = "English";
          if (Localisation.OnLanguageChange != null)
            Localisation.OnLanguageChange();
        }
        stringBuilder.Append(mEntry.text[inLanguage]);
      }
      else
        Debug.LogWarning((object) (inID + " NOT LOCALISED"), (UnityEngine.Object) null);
      return stringBuilder.ToString();
    }
  }

  public static string GetTextFromGroup(string inGroupName)
  {
    string str = string.Empty;
    if (Localisation.mGroups.ContainsKey(inGroupName))
    {
      LocalisationGroup mGroup = Localisation.mGroups[inGroupName];
      if (Game.instance != null)
      {
        str = Game.instance.localisationGroupManager.GetTextFromGroup(mGroup);
      }
      else
      {
        int random = RandomUtility.GetRandom(0, mGroup.entries.Count);
        str = mGroup.entries[random].text[Localisation.mCurrentLanguage];
      }
    }
    else
      Debug.LogException(new Exception("LOCALISATION: No group found called " + inGroupName));
    return str;
  }

  public static string ProcessVariables(string inText, params string[] inVariableData)
  {
    string str = inText;
    if (inVariableData.Length % 2 != 0)
    {
      Debug.LogException(new Exception("An even number of variable data is required for processing the string: " + inText));
    }
    else
    {
      int index = 0;
      while (index < inVariableData.Length)
      {
        str = str.Replace("{" + inVariableData[index] + "}", inVariableData[index + 1]);
        index += 2;
      }
    }
    return str;
  }

  public static bool HasID(string inID)
  {
    if (Localisation.mEntries == null)
      Localisation.LoadData();
    return Localisation.mEntries.ContainsKey(inID);
  }

  public static List<string> GetIDs()
  {
    if (Localisation.mEntries == null)
      Localisation.LoadData();
    return new List<string>((IEnumerable<string>) Localisation.mEntries.Keys);
  }

  public static List<LocalisationEntry> GetTextEntries()
  {
    if (Localisation.mEntries == null)
      Localisation.LoadData();
    List<LocalisationEntry> localisationEntryList = new List<LocalisationEntry>();
    using (Dictionary<string, LocalisationEntry>.Enumerator enumerator = Localisation.mEntries.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        KeyValuePair<string, LocalisationEntry> current = enumerator.Current;
        localisationEntryList.Add(current.Value);
      }
    }
    return localisationEntryList;
  }

  public static LocalisationEntry GetEntry(string inID)
  {
    if (Localisation.mEntries == null)
      Localisation.LoadData();
    if (!Localisation.mEntries.ContainsKey(inID))
      Debug.Log((object) inID, (UnityEngine.Object) null);
    return Localisation.mEntries[inID];
  }

  public static bool IsValueEmpty(string inValue)
  {
    if (!string.IsNullOrEmpty(inValue))
      return inValue == "0";
    return true;
  }

  public static string LocaliseEnum(Enum inEnum)
  {
    return Localisation.LocaliseEnum(inEnum, Localisation.currentLanguage, string.Empty);
  }

  public static string LocaliseEnum(Enum inEnum, string inLanguage, string inGender = "")
  {
    string enumLocalisationId = Localisation.GetEnumLocalisationID(inEnum);
    if (!string.IsNullOrEmpty(enumLocalisationId))
      return Localisation.LocaliseID(enumLocalisationId, inLanguage, (GameObject) null, inGender);
    return "{Enum: " + inEnum.ToString() + "}";
  }

  public static T[] GetAttributes<T>(Enum[] inEnumValue) where T : Attribute
  {
    List<T> objList = new List<T>();
    for (int index = 0; index < inEnumValue.Length; ++index)
    {
      Enum @enum = inEnumValue[index];
      MemberInfo memberInfo = ((IEnumerable<MemberInfo>) @enum.GetType().GetMember(@enum.ToString())).FirstOrDefault<MemberInfo>();
      if (memberInfo != null)
      {
        T obj = (T) ((IEnumerable<object>) memberInfo.GetCustomAttributes(typeof (T), false)).FirstOrDefault<object>();
        objList.Add(obj);
      }
    }
    return objList.ToArray();
  }

  public static T GetAttribute<T>(Enum inEnumValue) where T : Attribute
  {
    MemberInfo memberInfo = ((IEnumerable<MemberInfo>) inEnumValue.GetType().GetMember(inEnumValue.ToString())).FirstOrDefault<MemberInfo>();
    if (memberInfo != null)
      return (T) ((IEnumerable<object>) memberInfo.GetCustomAttributes(typeof (T), false)).FirstOrDefault<object>();
    return (T) null;
  }

  public static string GetEnumLocalisationID(Enum inEnumValue)
  {
    LocalisationIDAttribute attribute = Localisation.GetAttribute<LocalisationIDAttribute>(inEnumValue);
    if (attribute != null)
      return attribute.ID;
    return string.Empty;
  }

  private static string GetSpecialStringsData(string inID, string inString, string inLanguage)
  {
    using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
    {
      string str = inString;
      bool flag = false;
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = builderSafe.stringBuilder;
      for (int index = 0; index < str.Length; ++index)
      {
        if ((int) str[index] == 123)
          flag = true;
        else if (flag && (int) str[index] != 125)
          stringBuilder1.Append(str[index]);
        else if ((int) str[index] == 125)
        {
          StringVariableParser.GetData(stringBuilder1.ToString(), inLanguage, ref stringBuilder2);
          stringBuilder1 = new StringBuilder();
          flag = false;
        }
        else
          stringBuilder2.Append(str[index]);
      }
      if (stringBuilder2.ToString().Trim() == string.Empty)
        return string.Format("<color=#008080ff><b>String in database not yet translated, {0}</b></color>", (object) inID);
      return stringBuilder2.ToString();
    }
  }

  public static bool ConvertStringToLanguage(string input, out string result)
  {
    int index = 0;
    string str = input.ToLower();
    if (str.Contains("portuguese") || str == "brazilian")
      str = "brasilian";
    for (; index < Localisation.supportedLanguages.Length; ++index)
    {
      if (str == Localisation.supportedLanguages[index].ToLower())
      {
        result = Localisation.supportedLanguages[index];
        return true;
      }
    }
    result = Localisation.supportedLanguages[0];
    return false;
  }

  private static void UpdateCurrentLanguageIndex()
  {
    for (int index = 0; index < Localisation.supportedLanguages.Length; ++index)
    {
      if (Localisation.supportedLanguages[index].Equals(Localisation.mCurrentLanguage, StringComparison.Ordinal))
        Localisation.mCurrentLanguageIndex = index;
    }
  }

  public static bool UseOrdinalsInDates(string inLanguage)
  {
    string result;
    if (!Localisation.ConvertStringToLanguage(inLanguage, out result))
    {
      Debug.LogFormat("Could not check if dates use ordinal string for language: {0}", (object) result);
      return false;
    }
    return !string.Equals("dutch", result, StringComparison.OrdinalIgnoreCase) && !string.Equals("french", result, StringComparison.OrdinalIgnoreCase) && (!string.Equals("italian", result, StringComparison.OrdinalIgnoreCase) && !string.Equals("spanish", result, StringComparison.OrdinalIgnoreCase)) && !string.Equals("brasilian", result, StringComparison.OrdinalIgnoreCase);
  }

  private static void UpdateNumberFormattingEnums()
  {
    Localisation.mLapTimeFormatting = !Localisation.mCurrentLanguage.Equals(Localisation.supportedLanguages[4], StringComparison.Ordinal) ? (Localisation.mCurrentLanguage.Equals(Localisation.supportedLanguages[1], StringComparison.Ordinal) || Localisation.mCurrentLanguage.Equals(Localisation.supportedLanguages[7], StringComparison.Ordinal) || Localisation.mCurrentLanguage.Equals(Localisation.supportedLanguages[8], StringComparison.Ordinal) ? Localisation.LapTimeFormatting.Other : Localisation.LapTimeFormatting.English) : Localisation.LapTimeFormatting.Spanish;
    if (Localisation.mCurrentLanguage.Equals(Localisation.supportedLanguages[0], StringComparison.Ordinal))
    {
      Localisation.mNumberFormatter.NumberDecimalSeparator = ".";
      Localisation.mNumberFormatter.PercentDecimalSeparator = ".";
      Localisation.mNumberFormatter.NumberGroupSeparator = ",";
      Localisation.mUseCommaDecimalSeperator = false;
    }
    else
    {
      Localisation.mNumberFormatter.NumberDecimalSeparator = ",";
      Localisation.mNumberFormatter.PercentDecimalSeparator = ",";
      Localisation.mNumberFormatter.NumberGroupSeparator = " ";
      Localisation.mUseCommaDecimalSeperator = true;
    }
  }

  public static string GetLapTimeFormatting(bool shot_format)
  {
    switch (Localisation.mLapTimeFormatting)
    {
      case Localisation.LapTimeFormatting.English:
        return shot_format ? "0.000" : "0:00.000";
      case Localisation.LapTimeFormatting.Spanish:
        return shot_format ? "0,000" : "0:00:000";
      case Localisation.LapTimeFormatting.Other:
        return shot_format ? "0,000" : "0:00,000";
      default:
        return string.Empty;
    }
  }

  public static bool IsCurrentLanguageFrench()
  {
    return Localisation.mCurrentLanguageIndex == 1;
  }

  public enum LapTimeFormatting
  {
    English,
    Spanish,
    Other,
  }
}
