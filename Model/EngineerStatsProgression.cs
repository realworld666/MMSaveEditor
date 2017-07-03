
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class EngineerStatsProgression
{
	public string progressionType;
	public float acceleration;
	public float braking;
	public float topSpeed;
	public float highSpeedCorners;
	public float mediumSpeedCorners;
	public float lowSpeedCorners;


}
