using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TeamRadio
{
    private List<RadioMessage> mMessages = new List<RadioMessage>();
    private Vehicle mVehicle;


}
