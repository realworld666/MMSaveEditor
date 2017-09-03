using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using MMSaveEditor;

public class NationalityManager
{
    public static NationalityManager Instance = new NationalityManager();

    private List<Nationality> mNationalities = new List<Nationality>();
    private Dictionary<string, Nationality> mNationalitiesDict = new Dictionary<string, Nationality>();

    public Dictionary<string, Nationality> nationalitiesDict
    {
        get
        {
            return this.mNationalitiesDict;
        }
    }

    public NationalityManager()
    {
        try
        {
            var uri = new Uri("pack://application:,,,/Assets/Nationalities.txt");
            System.Windows.Resources.StreamResourceInfo resourceStream = Application.GetResourceStream(uri);

            using (var reader = new StreamReader(resourceStream.Stream))
            {
                string traitsText = reader.ReadToEnd();
                List<DatabaseEntry> result = DatabaseReader.LoadFromText(traitsText);

                LoadNationalitiesFromDatabase(result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            //throw;
        }

    }

    public void LoadNationalitiesFromDatabase(List<DatabaseEntry> databaseEntryList)
    {
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
            if (stringValue3.Equals("Country"))
            {
                this.mNationalitiesDict[stringValue1].SetCountry(stringValue2);
            }
            else if (stringValue3.Equals("Nationality"))
            {
                this.mNationalitiesDict[stringValue1].SetNationality(stringValue2);
            }
        }
    }

    public static Nationality.Continent GetContinent(string inString)
    {
        switch (inString)
        {
            case "Europe":
                return Nationality.Continent.Europe;
            case "Oceania":
                return Nationality.Continent.Oceania;
            case "Asia":
                return Nationality.Continent.Asia;
            case "North America":
                return Nationality.Continent.NorthAmerica;
            case "South America":
                return Nationality.Continent.SouthAmerica;
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
