
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class MechanicStatsProgression
{
	public string progressionType;
	public float reliability;
	public float performance;
	public float concentration;
	public float speed;
	public float pitStops;
	public float leadership;


}
