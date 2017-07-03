// Decompiled with JetBrains decompiler
// Type: SpecialStringsGenderTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class SpecialStringsGenderTable
{
  public Dictionary<string, SpecialStringsGenderTable.Data> table = new Dictionary<string, SpecialStringsGenderTable.Data>();

  public void LoadGenderTableDatabase(AssetManager inAssetManager)
  {
    List<DatabaseEntry> databaseEntryList = inAssetManager.ReadDatabase(Database.DatabaseType.SpecialStringTable);
    for (int index1 = 0; index1 < databaseEntryList.Count; ++index1)
    {
      DatabaseEntry databaseEntry = databaseEntryList[index1];
      string stringValue = databaseEntry.GetStringValue("Key");
      if (!this.table.ContainsKey(stringValue))
      {
        SpecialStringsGenderTable.Data data = new SpecialStringsGenderTable.Data();
        for (int index2 = 0; index2 < Localisation.supportedLanguages.Length; ++index2)
        {
          string supportedLanguage = Localisation.supportedLanguages[index2];
          data.languageGender.Add(supportedLanguage, databaseEntry.GetStringValue(supportedLanguage));
        }
        data.specialString = databaseEntry.GetStringValue("Special String");
        this.table.Add(stringValue, data);
      }
    }
  }

  public string GetGenderForLanguage(string inKey, string inLanguage)
  {
    if (this.table.ContainsKey(inKey))
      return this.table[inKey].languageGender[inLanguage];
    Debug.LogError((object) (inKey + " is not present in the gender table dictionary!"), (Object) null);
    return "Error";
  }

  public class Data
  {
    public Dictionary<string, string> languageGender = new Dictionary<string, string>();
    public string specialString = "{example}";
  }
}
