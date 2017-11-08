
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SetupDetails
{

    public Driver driver;
    public SetupInput_v1 input = new SetupInput_v1();
    public TyreSet tyreSet;
    public SessionSetup.Trim trim;
    public int fuelLevel;
    public float batteryCharge;
}
