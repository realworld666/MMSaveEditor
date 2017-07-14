
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class RadioMessageTyreWear : RadioMessage
{
    private List<TyreSet> mTyreSetUsed = new List<TyreSet>();
    private TyreSet mCurrentTyreSet;
    private bool mHasDonePunctureMessage;

}
