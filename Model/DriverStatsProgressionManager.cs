using FullSerializer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class DriverStatsProgressionManager
{
    public static DriverStatsProgressionManager Instance = new DriverStatsProgressionManager();

    private Dictionary<string, DriverStatsProgression> statsProgressionDictionary;

    public DriverStatsProgressionManager()
    {
        Instance = this;

        try
        {
            var uri = new Uri("pack://application:,,,/Assets/Driver Stat Progression.txt");
            System.Windows.Resources.StreamResourceInfo resourceStream = Application.GetResourceStream(uri);

            using (var reader = new StreamReader(resourceStream.Stream))
            {
                string traitsText = reader.ReadToEnd();
                List<DatabaseEntry> result = DatabaseReader.LoadFromText(traitsText);

                LoadDriverProgressionFromDatabase(result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            //throw;
        }
    }

    public void LoadDriverProgressionFromDatabase(List<DatabaseEntry> statProgressionData)
    {
        this.statsProgressionDictionary = new Dictionary<string, DriverStatsProgression>();
        for (int index = 0; index < statProgressionData.Count; ++index)
        {
            DatabaseEntry inEntry = statProgressionData[index];
            string stringValue = inEntry.GetStringValue("Type");
            if (stringValue.Contains("/"))
            {
                int num = int.Parse(stringValue[1].ToString());
                string inType = stringValue.Substring(3);
                for (int inIndex = 0; inIndex < num; ++inIndex)
                {
                    DriverStatsProgression statsProgression = new DriverStatsProgression(inEntry, inIndex, inType);
                    this.statsProgressionDictionary.Add(inType + (object)inIndex, statsProgression);
                }
            }
            else
            {
                DriverStatsProgression statsProgression = new DriverStatsProgression(inEntry, stringValue);
                this.statsProgressionDictionary.Add(stringValue, statsProgression);
            }
        }
    }

    public DriverStatsProgression GetDriverStatsProgression(string inType)
    {
        if (this.statsProgressionDictionary != null && this.statsProgressionDictionary.ContainsKey(inType))
            return this.statsProgressionDictionary[inType];
        return (DriverStatsProgression)null;
    }
}
