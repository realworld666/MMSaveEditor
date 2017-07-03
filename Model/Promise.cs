
using FullSerializer;
using System;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class Promise
{
	private Promise.PromiseType mPromiseType = Promise.PromiseType.Count;
	private CarPart.PartType mCarPartPromised = CarPart.PartType.None;
	private int mChampionshipPositionPromised = -1;
	private int mOldYearlyWage = -1;
	private DateTime mPromiseMadeDate = new DateTime();
	private Driver mDriverPromisedTo;
	private HQsBuilding_v1 mHQBuildingPromised;
	private Mechanic mMechanicToFire;
	private Engineer mEngineerToFire;
	private Driver mReserveToFire;


	public enum PromiseType
	{
		NewPart,
		IncreaseIncome,
		IncreaseChampionshipPosition,
		HQBuildingBuilt,
		FireMechanic,
		FireEngineer,
		FireReserve,
		Count,
	}
}
