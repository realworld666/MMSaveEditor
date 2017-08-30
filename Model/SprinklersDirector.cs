
using FullSerializer;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SprinklersDirector
{
    private SessionManager mSessionManager;
    private float mSprinklerChance;
    private bool mSprinklersSetup;
    private bool mIsSprinklerGoingToActivate;

}
