// Decompiled with JetBrains decompiler
// Type: NationalityManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

public class NationalityManager
{
  private List<Nationality> mNationalities = new List<Nationality>();
  private Dictionary<string, Nationality> mNationalitiesDict = new Dictionary<string, Nationality>();

  public Dictionary<string, Nationality> nationalitiesDict
  {
    get
    {
      return this.mNationalitiesDict;
    }
  }

  public void Start()
  {
    List<DatabaseEntry> databaseEntryList = App.instance.assetManager.ReadDatabase(Database.DatabaseType.Nationality);
    for (int index = 0; index < databaseEntryList.Count; ++index)
    {
      DatabaseEntry databaseEntry = databaseEntryList[index];
      string stringValue1 = databaseEntry.GetStringValue("Sprite Reference");
      if (!this.mNationalitiesDict.ContainsKey(stringValue1))
      {
        this.mNationalitiesDict[stringValue1] = new Nationality();
        this.mNationalitiesDict[stringValue1].continent = NationalityManager.GetContinent(databaseEntry.GetStringValue("Continent"));
        this.mNationalitiesDict[stringValue1].SetKey(stringValue1);
        this.mNationalities.Add(this.mNationalitiesDict[stringValue1]);
      }
      string stringValue2 = databaseEntry.GetStringValue("ID");
      string stringValue3 = databaseEntry.GetStringValue("Type");
      if (stringValue3 != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (NationalityManager.\u003C\u003Ef__switch\u0024map0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NationalityManager.\u003C\u003Ef__switch\u0024map0 = new Dictionary<string, int>(2)
          {
            {
              "Country",
              0
            },
            {
              "Nationality",
              1
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (NationalityManager.\u003C\u003Ef__switch\u0024map0.TryGetValue(stringValue3, out num))
        {
          if (num != 0)
          {
            if (num == 1)
              this.mNationalitiesDict[stringValue1].SetNationality(stringValue2);
          }
          else
            this.mNationalitiesDict[stringValue1].SetCountry(stringValue2);
        }
      }
    }
  }

  public static Nationality.Continent GetContinent(string inString)
  {
    string key = inString;
    if (key != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (NationalityManager.\u003C\u003Ef__switch\u0024map1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NationalityManager.\u003C\u003Ef__switch\u0024map1 = new Dictionary<string, int>(5)
        {
          {
            "Europe",
            0
          },
          {
            "Oceania",
            1
          },
          {
            "Asia",
            2
          },
          {
            "North America",
            3
          },
          {
            "South America",
            4
          }
        };
      }
      int num;
      // ISSUE: reference to a compiler-generated field
      if (NationalityManager.\u003C\u003Ef__switch\u0024map1.TryGetValue(key, out num))
      {
        switch (num)
        {
          case 0:
            return Nationality.Continent.Europe;
          case 1:
            return Nationality.Continent.Oceania;
          case 2:
            return Nationality.Continent.Asia;
          case 3:
            return Nationality.Continent.NorthAmerica;
          case 4:
            return Nationality.Continent.SouthAmerica;
        }
      }
    }
    return Nationality.Continent.Africa;
  }

  public List<Nationality> GetNationalitiesForContinent(Nationality.Continent inContinent)
  {
    List<Nationality> nationalityList = new List<Nationality>();
    int count = this.mNationalities.Count;
    for (int index = 0; index < count; ++index)
    {
      Nationality mNationality = this.mNationalities[index];
      if (mNationality.continent == inContinent)
        nationalityList.Add(mNationality);
    }
    return nationalityList;
  }
}
