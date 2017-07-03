using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TeamFinanceController
{
    public Finance finance = new Finance();
    public string financialHealth = string.Empty;
    public List<Transaction> unnallocatedTransactions = new List<Transaction>();
    private List<Transaction> mReturnTransactions = new List<Transaction>();
    private List<Transaction> mReturnBonusTransactions = new List<Transaction>();
    private List<Transaction> mEventTransactions = new List<Transaction>();
    private List<Transaction> mEventBonusTransactions = new List<Transaction>();
    private TeamFinanceController.NextYearCarInvestement mInvestement = TeamFinanceController.NextYearCarInvestement.Medium;
    public long racePayment;
    public long racePaymentOffset;
    public long worth;
    public long availableFunds;
    public long fullChairmanFunds;
    public int stockPrice;
    public long moneyForCarDev;
    private Team mTeam;

    public enum RacePaymentType
    {
        [LocalisationID("PSG_10001437")] Low,
        [LocalisationID("PSG_10001438")] Medium,
        [LocalisationID("PSG_10001439")] High,
    }

    public enum NextYearCarInvestement
    {
        [LocalisationID("PSG_10001437")] Low,
        [LocalisationID("PSG_10001438")] Medium,
        [LocalisationID("PSG_10001439")] High,
    }
}
