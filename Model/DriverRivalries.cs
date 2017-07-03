
using FullSerializer;
using System;
using System.Collections.Generic;
using System.Text;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class DriverRivalries
{
	private int mRivalryValueThreshold = 5;
	private readonly int mRivalryStandingsPositionMaxDifference = 2;
	private Dictionary<Guid, int> mDriverRivalries = new Dictionary<Guid, int>();
	private Map<Guid, int> driverRivalries;
	private Driver mRivalDriver;
	private Driver mDriver;


}
