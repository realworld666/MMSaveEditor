
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class WeatherImpact : PerformanceImpact
{
    private SessionWeatherDetails mWeatherDetails;
    private SessionSetup.SetupOutput mSetupOutput;
}
