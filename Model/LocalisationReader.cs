// Decompiled with JetBrains decompiler
// Type: LocalisationReader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using CsvHelper;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalisationReader
{
  public static void LoadFromFile(string inPath, Dictionary<string, LocalisationEntry> inEntries, Dictionary<string, LocalisationGroup> inGroups)
  {
    CsvReader csvReader = new CsvReader((TextReader) new StringReader((Resources.Load(inPath) as TextAsset).text));
    while (csvReader.Read())
    {
      if (csvReader.CurrentRecord.Length > csvReader.FieldHeaders.Length)
        Debug.LogWarningFormat("Field with more entries than there are columns - some data will be ignored! Check your data! File: {0}, line: {1}, columns: {2}, values in row: {3}", (object) inPath, (object) csvReader.Row, (object) csvReader.FieldHeaders.Length, (object) csvReader.CurrentRecord.Length);
      if (csvReader.CurrentRecord.Length < csvReader.FieldHeaders.Length)
      {
        Debug.LogWarningFormat("Field with fewer entries than there are columns - data is missing and can't be read. This row is being skipped. Check your data! File: {0}, line: {1}, columns: {2}, values in row: {3}", (object) inPath, (object) csvReader.Row, (object) csvReader.FieldHeaders.Length, (object) csvReader.CurrentRecord.Length);
      }
      else
      {
        LocalisationEntry localisationEntry = new LocalisationEntry();
        for (int index1 = 0; index1 < csvReader.FieldHeaders.Length; ++index1)
        {
          string field;
          if (!csvReader.TryGetField<string>(index1, out field))
          {
            Debug.LogWarningFormat("Missing field, line {0} in file \"{1}\"", new object[2]
            {
              (object) csvReader.Row,
              (object) inPath
            });
          }
          else
          {
            string fieldHeader = csvReader.FieldHeaders[index1];
            if (fieldHeader == "Message Group")
            {
              localisationEntry.group = field;
              if (!inGroups.ContainsKey(localisationEntry.group))
                inGroups[localisationEntry.group] = new LocalisationGroup();
              inGroups[localisationEntry.group].entries.Add(localisationEntry);
            }
            else if (fieldHeader == "User Data")
            {
              if (!Localisation.IsValueEmpty(field))
              {
                string str1 = field;
                char[] chArray1 = new char[1]{ ',' };
                foreach (string str2 in str1.Split(chArray1))
                {
                  char[] chArray2 = new char[1]{ '=' };
                  string[] strArray = str2.Split(chArray2);
                  if (strArray.Length < 2)
                    Debug.LogErrorFormat("Bad User Data (missing '=' sign): \"{0}\", line {1} in file \"{2}\"", (object) field, (object) csvReader.Row, (object) inPath);
                  else
                    localisationEntry.userData[strArray[0]] = strArray[1];
                }
              }
            }
            else if (fieldHeader == "ID")
            {
              localisationEntry.id = field;
            }
            else
            {
              for (int index2 = 0; index2 < Localisation.supportedLanguages.Length; ++index2)
              {
                if (fieldHeader == Localisation.supportedLanguages[index2])
                  localisationEntry.text[fieldHeader] = field;
              }
            }
          }
        }
        if (!string.IsNullOrEmpty(localisationEntry.id))
        {
          if (inEntries.ContainsKey(localisationEntry.id))
            Debug.LogError((object) ("LOCALISATION: Duplicate string ID (" + localisationEntry.id + ") found in " + inPath), (Object) null);
          else
            inEntries[localisationEntry.id] = localisationEntry;
        }
      }
    }
  }
}
