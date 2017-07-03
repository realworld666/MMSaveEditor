using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public abstract class RadioMessage
{
    protected DialogQueryCreator mQueryCreator = new DialogQueryCreator();
    protected bool[] mHasBeenTriggered = new bool[3];
    private bool mOnlySendIfVehicleIsRacing = true;
    public DialogRule dialogRule;
    public TextDynamicData text;
    public Person personWhoSpeaks;
    public RadioMessage.DilemmaType dilemmaType;
    public RadioMessage.MessageCategory messageCategory;
    protected RacingVehicle mVehicle;
    protected TeamRadio mTeamRadio;
    private float mFeedbackMessageDelayDuration;
    private float mFeedbackDelayTimer;
    public enum DilemmaType
    {
        None,
        Fuel,
        Tyres,
        CarPartCondition,
        SafetyCar,
    }

    public enum MessageCategory
    {
        None,
        WeatherChange,
    }
}
