using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Finance
{
    public TransactionHistory transactionHistory = new TransactionHistory();
    public bool saveTransactionHistory;
    public Team team;
    public long initialBudget;
    public long currentBudget;
}
