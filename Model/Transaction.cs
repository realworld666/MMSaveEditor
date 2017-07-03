
using FullSerializer;
using System;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class Transaction
{
	public Transaction.Group group = Transaction.Group.Count;
	public string name = string.Empty;
	public DateTime transactionDate = new DateTime();
	public Transaction.Type transactionType = Transaction.Type.Debit;
	public long amount;
	public long fundsAfterTransaction;

	public enum Type
	{
		Credit,
		Debit,
	}

	public enum Group
	{
		[LocalisationID( "PSG_10005581" )] CarParts,
		[LocalisationID( "PSG_10002131" )] NextYearCar,
		[LocalisationID( "PSG_10005583" )] Drivers,
		[LocalisationID( "PSG_10005584" )] Mechanics,
		[LocalisationID( "PSG_10005585" )] Designer,
		[LocalisationID( "PSG_10005586" )] HQUpkeepAndStaff,
		[LocalisationID( "PSG_10010181" )] HQ,
		[LocalisationID( "PSG_10005588" )] HQIncome,
		[LocalisationID( "PSG_10005589" )] GlobalMotorsport,
		[LocalisationID( "PSG_10005590" )] TravelCosts,
		[LocalisationID( "PSG_10005591" )] ChairmanPayments,
		[LocalisationID( "PSG_10005592" )] PrizeMoney,
		[LocalisationID( "PSG_10005593" )] Sponsor,
		[LocalisationID( "PSG_10007599" )] Dilemma,
		[LocalisationID( "PSG_10007600" )] BackstoryFinancial,
		Count,
	}
}
