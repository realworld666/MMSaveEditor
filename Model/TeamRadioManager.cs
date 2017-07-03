using System;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TeamRadioManager
{
    public Action<RadioMessage> OnRadioMessage;
    public Action<RadioMessage> OnDilemma;
    private RadioMessage mLastDilemma;


}
