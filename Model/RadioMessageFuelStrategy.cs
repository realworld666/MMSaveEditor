using FullSerializer;
using System;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class RadioMessageFuelStrategy : RadioMessage
{
    private bool mIsRefuelingOn = true;

}
