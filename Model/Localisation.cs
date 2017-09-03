using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

public class Localisation
{
    public static string[] localisationSheets = new string[14]
    {
    "Dilemmas",
    "Frontend",
    "MediaReports",
    "Interviews",
    "Tweets",
    "Messages",
    "PreRaceTalk",
    "RaceEvent",
    "Shared",
    "Simulation",
    "TeamRadio",
    "Tutorials",
    "Nationalities",
    "Challenges"
    };

    private static Dictionary<string, LocalisationEntry> mEntries = (Dictionary<string, LocalisationEntry>)null;
    private static Dictionary<string, LocalisationGroup> mGroups = (Dictionary<string, LocalisationGroup>)null;


    public static void LoadData()
    {
        if (Localisation.mEntries != null || Localisation.mGroups != null)
            return;
        Localisation.mEntries = new Dictionary<string, LocalisationEntry>();
        Localisation.mGroups = new Dictionary<string, LocalisationGroup>();
        for (int index = 0; index < Localisation.localisationSheets.Length; ++index)
            LocalisationReader.LoadFromFile(Localisation.localisationSheets[index], Localisation.mEntries, Localisation.mGroups);
    }

    public static string LocaliseID(string inID)
    {
        if (inID.Length != "PSG_10000001".Length)
            return "<color=yellow>PSG ID not set correctly</color>";
        return Localisation.LocaliseID(inID, "English");
    }

    public static string LocaliseID(string inID, string inLanguage)
    {
        return Localisation.GetText(inID, inLanguage);
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
                    Debug.WriteLine(("Language Not Present: " + inLanguage));
                    Debug.WriteLine("Defaulting to English");
                    inLanguage = "English";
                }
                stringBuilder.Append(mEntry.text[inLanguage]);
            }
            else
                Debug.WriteLine((inID + " NOT LOCALISED"));
            return stringBuilder.ToString();
        }
    }


    public static List<string> GetIDs()
    {
        if (Localisation.mEntries == null)
            Localisation.LoadData();
        return new List<string>((IEnumerable<string>)Localisation.mEntries.Keys);
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
        return Localisation.LocaliseEnum(inEnum, "", string.Empty);
    }

    public static string LocaliseEnum(Enum inEnum, string inLanguage, string inGender = "")
    {
        string enumLocalisationId = Localisation.GetEnumLocalisationID(inEnum);
        //if (!string.IsNullOrEmpty(enumLocalisationId))
        // return Localisation.LocaliseID(enumLocalisationId, inLanguage, inGender);
        return "{Enum: " + inEnum.ToString() + "}";
    }

    public static T[] GetAttributes<T>(Enum[] inEnumValue) where T : Attribute
    {
        List<T> objList = new List<T>();
        for (int index = 0; index < inEnumValue.Length; ++index)
        {
            Enum @enum = inEnumValue[index];
            MemberInfo memberInfo = ((IEnumerable<MemberInfo>)@enum.GetType().GetMember(@enum.ToString())).FirstOrDefault<MemberInfo>();
            if (memberInfo != null)
            {
                T obj = (T)((IEnumerable<object>)memberInfo.GetCustomAttributes(typeof(T), false)).FirstOrDefault<object>();
                objList.Add(obj);
            }
        }
        return objList.ToArray();
    }

    public static T GetAttribute<T>(Enum inEnumValue) where T : Attribute
    {
        MemberInfo memberInfo = ((IEnumerable<MemberInfo>)inEnumValue.GetType().GetMember(inEnumValue.ToString())).FirstOrDefault<MemberInfo>();
        if (memberInfo != null)
            return (T)((IEnumerable<object>)memberInfo.GetCustomAttributes(typeof(T), false)).FirstOrDefault<object>();
        return (T)null;
    }

    public static string GetEnumLocalisationID(Enum inEnumValue)
    {
        LocalisationIDAttribute attribute = Localisation.GetAttribute<LocalisationIDAttribute>(inEnumValue);
        if (attribute != null)
            return attribute.ID;
        return string.Empty;
    }


    public enum LapTimeFormatting
    {
        English,
        Spanish,
        Other,
    }
}
