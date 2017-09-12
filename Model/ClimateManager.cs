using FullSerializer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ClimateManager
{
    public static ClimateManager Instance = new ClimateManager();
    private List<Climate> mClimates = new List<Climate>();

    public ClimateManager()
    {
        Instance = this;

        try
        {
            var uri = new Uri("pack://application:,,,/Assets/Climate.txt");
            System.Windows.Resources.StreamResourceInfo resourceStream = Application.GetResourceStream(uri);

            using (var reader = new StreamReader(resourceStream.Stream))
            {
                string traitsText = reader.ReadToEnd();
                List<DatabaseEntry> result = DatabaseReader.LoadFromText(traitsText);

                LoadClimatesFromDatabase(result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            //throw;
        }

    }

    public void LoadClimatesFromDatabase(List<DatabaseEntry> climateData)
    {
        Debug.Assert(this.mClimates.Count == 0, "Loading from database when content is already loaded; this will work but indicates that the game is loading in a strange unintended way.");
        this.mClimates.Clear();

        for (int index = 0; index < 5; ++index)
        {
            Climate inClimate = new Climate();
            this.mClimates.Add(inClimate);
            inClimate.climateType = (Climate.ClimateType)index;
            this.SetClimateData(inClimate, climateData);
        }
    }

    private void SetClimateData(Climate inClimate, List<DatabaseEntry> climateDatabase)
    {
        string str = this.ConvertClimateTypeToString(inClimate.climateType);
        for (int index = 0; index < 12; ++index)
        {
            DatabaseEntry databaseEntry = climateDatabase[index];
            inClimate.minimumTemperatures[index] = databaseEntry.GetIntValue(str + " - Min");
            inClimate.maximumTemperatures[index] = databaseEntry.GetIntValue(str + " - Max");
            inClimate.clearChance[index] = databaseEntry.GetFloatValue(str + " - Clear");
            inClimate.cloudyChance[index] = databaseEntry.GetFloatValue(str + " - Cloudy");
            inClimate.overcastChance[index] = databaseEntry.GetFloatValue(str + " - Overcast");
            inClimate.stormyChance[index] = databaseEntry.GetFloatValue(str + " - Stormy");
        }
    }

    private string ConvertClimateTypeToString(Climate.ClimateType climateType)
    {
        string str = "Cool";
        switch (climateType)
        {
            case Climate.ClimateType.Moderate:
                str = "Moderate";
                break;
            case Climate.ClimateType.Warm:
                str = "Warm";
                break;
            case Climate.ClimateType.Tropical:
                str = "Tropical";
                break;
            case Climate.ClimateType.Dry:
                str = "Dry";
                break;
        }
        return str;
    }

    public Climate GetClimate(int inIndex)
    {
        return this.mClimates[inIndex];
    }
}
