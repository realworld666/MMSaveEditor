
using FullSerializer;
using System;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class SprinklersDirector
{
	public Action OnSprinklersOn;
	public Action OnSprinklersOff;
	private SessionManager mSessionManager;
	private float mSprinklerChance;
	private bool mSprinklersSetup;

}
