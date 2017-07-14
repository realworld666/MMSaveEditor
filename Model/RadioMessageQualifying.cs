
using FullSerializer;
using System;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class RadioMessageQualifying : RadioMessage
{
    private int mExpectedPosition;

}
