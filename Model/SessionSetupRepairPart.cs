
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SessionSetupRepairPart
{
    public CarPart carPart;
    public float repairTime;
}
