using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Weather
{
    private Weather.RainType mRainType;
    private Weather.CloudType mCloudType;
    private float mAirTemperature;

    public enum RainType
    {
        None,
        Light,
        Medium,
        Heavy,
        Monsooon,
    }

    public enum CloudType
    {
        Clear,
        Cloudy,
        Overcast,
        Stormy,
        Count,
    }

    public enum AirTemp
    {
        Freezing,
        Low,
        Average,
        Warm,
        Hot,
        Count,
    }

    public enum WaterLevel
    {
        [LocalisationID("PSG_10004878")] Dry,
        [LocalisationID("PSG_10004879")] Damp,
        [LocalisationID("PSG_10004880")] Wet,
        [LocalisationID("PSG_10004881")] Soaked,
    }

    public enum RubberLevel
    {
        [LocalisationID("PSG_10004882")] None,
        [LocalisationID("PSG_10004883")] Low,
        [LocalisationID("PSG_10004884")] Medium,
        [LocalisationID("PSG_10004885")] High,
    }
}
