using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class CarPart : Entity
{
	private static readonly CarPart.PartType[] singleSeaterSeries = new CarPart.PartType[6] { CarPart.PartType.Brakes, CarPart.PartType.Engine, CarPart.PartType.FrontWing, CarPart.PartType.Gearbox, CarPart.PartType.RearWing, CarPart.PartType.Suspension };
	private static readonly CarPart.PartType[] singleSeaterSeriesStatOrdered = new CarPart.PartType[6] { CarPart.PartType.Engine, CarPart.PartType.Gearbox, CarPart.PartType.Brakes, CarPart.PartType.FrontWing, CarPart.PartType.Suspension, CarPart.PartType.RearWing };
	private static readonly CarPart.PartType[] gtSeries = new CarPart.PartType[5] { CarPart.PartType.BrakesGT, CarPart.PartType.EngineGT, CarPart.PartType.GearboxGT, CarPart.PartType.RearWingGT, CarPart.PartType.SuspensionGT };
	private static readonly CarPart.PartType[] gtSeriesStatOrdered = new CarPart.PartType[5] { CarPart.PartType.EngineGT, CarPart.PartType.GearboxGT, CarPart.PartType.BrakesGT, CarPart.PartType.SuspensionGT, CarPart.PartType.RearWingGT };
	public static string[] PartNames = new string[12] { "Brakes", "Engine", "FrontWing", "Gearbox", "RearWing", "Suspension", "RearWingGT", "BrakesGT", "EngineGT", "GearboxGT", "SuspensionGT", "Count" };
	public List<CarPartComponent> components = new List<CarPartComponent>();
	public Action OnStageChange;
	public DateTime buildDate;
	public Car fittedCar;
	public bool isFitted;
	private bool isBanned;
	private CarPartStats mStats;
	private int mModelId;

	public CarPartStats stats
	{
		get
		{
			return this.mStats;
		}
		set
		{
			this.mStats = value;
		}
	}

	public float reliability
	{
		get
		{
			return this.mStats.reliability;
		}
	}

	public bool IsBanned
	{
		get
		{
			return isBanned;
		}

		set
		{
			isBanned = value;
		}
	}

	public enum PartType
	{
		None = -1,
		[LocalisationID( "PSG_10001657" )] Brakes = 0,
		[LocalisationID( "PSG_10001653" )] Engine = 1,
		[LocalisationID( "PSG_10001651" )] FrontWing = 2,
		[LocalisationID( "PSG_10001654" )] Gearbox = 3,
		[LocalisationID( "PSG_10001652" )] RearWing = 4,
		[LocalisationID( "PSG_10001655" )] Suspension = 5,
		[LocalisationID( "PSG_10011533" )] RearWingGT = 6,
		[LocalisationID( "PSG_10001657" )] BrakesGT = 7,
		[LocalisationID( "PSG_10001653" )] EngineGT = 8,
		[LocalisationID( "PSG_10001654" )] GearboxGT = 9,
		[LocalisationID( "PSG_10001655" )] SuspensionGT = 10,
		Last = 11,
	}

	public enum PartTypePlural
	{
		[LocalisationID( "PSG_10008535" )] Brakes,
		[LocalisationID( "PSG_10008532" )] Engine,
		[LocalisationID( "PSG_10008530" )] FrontWing,
		[LocalisationID( "PSG_10008533" )] Gearbox,
		[LocalisationID( "PSG_10008531" )] RearWing,
		[LocalisationID( "PSG_10008534" )] Suspension,
		[LocalisationID( "PSG_10012157" )] RearWingGT,
		[LocalisationID( "PSG_10008535" )] BrakesGT,
		[LocalisationID( "PSG_10008532" )] EngineGT,
		[LocalisationID( "PSG_10008533" )] GearboxGT,
		[LocalisationID( "PSG_10008534" )] SuspensionGT,
		Count,
	}
}
