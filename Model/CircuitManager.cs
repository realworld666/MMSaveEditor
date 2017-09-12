using FullSerializer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CircuitManager
{
    public static CircuitManager Instance = new CircuitManager();

    private List<Circuit> mCircuits = new List<Circuit>();
    public int circuitCount
    {
        get
        {
            return this.mCircuits.Count;
        }
    }

    public List<Circuit> circuits
    {
        get
        {
            return this.mCircuits;
        }
    }

    public CircuitManager()
    {
        Instance = this;
        try
        {
            var uri = new Uri("pack://application:,,,/Assets/Locations.txt");
            System.Windows.Resources.StreamResourceInfo resourceStream = Application.GetResourceStream(uri);

            using (var reader = new StreamReader(resourceStream.Stream))
            {
                string traitsText = reader.ReadToEnd();
                List<DatabaseEntry> result = DatabaseReader.LoadFromText(traitsText);

                LoadCircuitsFromDatabase(result, ClimateManager.Instance);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            //throw;
        }

    }

    public void LoadCircuitsFromDatabase(List<DatabaseEntry> locationData, ClimateManager climateManager)
    {
        Debug.Assert(this.mCircuits.Count == 0, "Loading from database when content is already loaded; this will work but indicates that the game is loading in a strange unintended way.");
        this.mCircuits.Clear();

        for (int index = 0; index < locationData.Count; ++index)
        {
            DatabaseEntry databaseEntry = locationData[index];
            Circuit circuit = new Circuit();
            this.mCircuits.Add(circuit);
            circuit.circuitID = databaseEntry.GetIntValue("Circuit ID");
            circuit.locationName = databaseEntry.GetStringValue("Location");
            circuit.travelCost = (long)((double)databaseEntry.GetFloatValue("Travel Cost") * (double)GameStatsConstants.millionScalar);
            circuit.locationNameID = databaseEntry.GetStringValue("Location ID");
            circuit.nationalityKey = databaseEntry.GetStringValue("Country");
            circuit.countryNameID = databaseEntry.GetStringValue("Country ID");
            circuit.spriteName = databaseEntry.GetStringValue("Sprite Name");
            circuit.trackRubberModifier = databaseEntry.GetFloatValue("Track Rubber Modifier");
            circuit.safetyCarFlagProbability = databaseEntry.GetFloatValue("Safety Car Flag Chance");
            circuit.virtualSafetyCarProbability = databaseEntry.GetFloatValue("Virtual Safety Car Chance");
            circuit.minimapRotation = databaseEntry.GetIntValue("Minimap Rotation");
            circuit.minimapAxisX = databaseEntry.GetStringValue("Minimap Axis") == "x";
            circuit.trackStatsCharacteristics.SetStat(CarStats.StatType.TopSpeed, databaseEntry.GetFloatValue("TS"));
            circuit.trackStatsCharacteristics.SetStat(CarStats.StatType.Acceleration, databaseEntry.GetFloatValue("ACC"));
            circuit.trackStatsCharacteristics.SetStat(CarStats.StatType.Braking, databaseEntry.GetFloatValue("DEC"));
            circuit.trackStatsCharacteristics.SetStat(CarStats.StatType.HighSpeedCorners, databaseEntry.GetFloatValue("HSC"));
            circuit.trackStatsCharacteristics.SetStat(CarStats.StatType.MediumSpeedCorners, databaseEntry.GetFloatValue("MSC"));
            circuit.trackStatsCharacteristics.SetStat(CarStats.StatType.LowSpeedCorners, databaseEntry.GetFloatValue("LSC"));
            circuit.isInNorthHemisphere = databaseEntry.GetStringValue("Hemisphere") == "North";
            circuit.trackLayout = CircuitManager.GetTrackLayoutFromString(databaseEntry.GetStringValue("Track Layout"));
            circuit.trackLengthMiles = databaseEntry.GetFloatValue("Track Length");
            circuit.firstTyreOption = (TyreSet.Compound)Enum.Parse(typeof(TyreSet.Compound), databaseEntry.GetStringValue("First Tyre Option"));
            circuit.secondTyreOption = (TyreSet.Compound)Enum.Parse(typeof(TyreSet.Compound), databaseEntry.GetStringValue("Second Tyre Option"));
            circuit.thirdTyreOption = (TyreSet.Compound)Enum.Parse(typeof(TyreSet.Compound), databaseEntry.GetStringValue("Third Tyre Option"));
            circuit.tyreWearRate = (Circuit.Rate)Enum.Parse(typeof(Circuit.Rate), databaseEntry.GetStringValue("Tyre Wear Rate"));
            circuit.fuelBurnRate = (Circuit.Rate)Enum.Parse(typeof(Circuit.Rate), databaseEntry.GetStringValue("Fuel Burn Rate"));
            float num1 = 120f;
            float num2 = 90f;
            float inMinutes1 = (float)((double)circuit.trackLengthMiles / (double)num1 * 60.0);
            float inMinutes2 = (float)((double)circuit.trackLengthMiles / (double)num2 * 60.0);
            circuit.bestPossibleLapTime = GameUtility.MinutesToSeconds(inMinutes1);
            circuit.worstPossibleLapTime = GameUtility.MinutesToSeconds(inMinutes2);
            circuit.scene = databaseEntry.GetStringValue("Scene");

            circuit.climate = CircuitManager.GetClimateFromString(databaseEntry.GetStringValue("Climate"), climateManager);
            circuit.driverStats = Game.instance.driverStatsProgressionManager.GetDriverStatsProgression(circuit.locationName);
        }
    }

    private static Climate GetClimateFromString(string climateType, ClimateManager climateManager)
    {
        Climate climate = climateManager.GetClimate(0);
        switch (climateType)
        {
            case "Moderate":
                climate = climateManager.GetClimate(1);
                break;
            case "Warm":
                climate = climateManager.GetClimate(2);
                break;
            case "Tropical":
                climate = climateManager.GetClimate(3);
                break;
            case "Dry":
                climate = climateManager.GetClimate(4);
                break;
        }
        return climate;
    }

    public static Circuit.TrackLayout GetTrackLayoutFromString(string inTrackLayout)
    {
        Circuit.TrackLayout result = Circuit.TrackLayout.TrackA;
        switch (inTrackLayout)
        {
            case "A":
                result = Circuit.TrackLayout.TrackA;
                break;
            case "B":
                result = Circuit.TrackLayout.TrackB;
                break;
            case "C":
                result = Circuit.TrackLayout.TrackC;
                break;
            case "D":
                result = Circuit.TrackLayout.TrackD;
                break;
            case "E":
                result = Circuit.TrackLayout.TrackE;
                break;
            case "F":
                result = Circuit.TrackLayout.TrackF;
                break;
        }
        return result;
    }
}
