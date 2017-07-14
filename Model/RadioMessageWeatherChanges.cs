
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class RadioMessageWeatherChanges : RadioMessage
{
    private Weather mPreviousWeatherConditions;

}
