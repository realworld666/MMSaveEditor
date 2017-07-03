
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class TransactionHistory
{
	public List<Transaction> transactions = new List<Transaction>();
	private List<Transaction> mReturnTransactions = new List<Transaction>();
	private List<Transaction> mReturnGroupTransactions = new List<Transaction>();


	public enum TimeOption
	{
		ThisYear,
		AllTime,
	}
}
