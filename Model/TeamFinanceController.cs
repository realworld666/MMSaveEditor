using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TeamFinanceController
{
    private Finance finance = new Finance();
    public string financialHealth = string.Empty;
    public List<Transaction> unnallocatedTransactions = new List<Transaction>();
    private List<Transaction> mReturnTransactions = new List<Transaction>();
    private List<Transaction> mReturnBonusTransactions = new List<Transaction>();
    private List<Transaction> mEventTransactions = new List<Transaction>();
    private List<Transaction> mEventBonusTransactions = new List<Transaction>();
    private TeamFinanceController.NextYearCarInvestement mInvestement = TeamFinanceController.NextYearCarInvestement.Medium;
    private long racePayment;
    private long racePaymentOffset;
    private long worth;
    private long availableFunds;
    private long fullChairmanFunds;
    private int stockPrice;
    private long moneyForCarDev;
    private Team mTeam;

    public TeamFinanceController.NextYearCarInvestement nextYearCarInvestement
    {
        get
        {
            return this.mInvestement;
        }
    }

    public long MoneyForCarDev
    {
        get
        {
            return moneyForCarDev;
        }

        set
        {
            moneyForCarDev = value;
        }
    }

    public int StockPrice
    {
        get
        {
            return stockPrice;
        }

        set
        {
            stockPrice = value;
        }
    }

    public long FullChairmanFunds
    {
        get
        {
            return fullChairmanFunds;
        }

        set
        {
            fullChairmanFunds = value;
        }
    }

    public long AvailableFunds
    {
        get
        {
            return availableFunds;
        }

        set
        {
            availableFunds = value;
        }
    }

    public long Worth
    {
        get
        {
            return worth;
        }

        set
        {
            worth = value;
        }
    }

    public long RacePaymentOffset
    {
        get
        {
            return racePaymentOffset;
        }

        set
        {
            racePaymentOffset = value;
        }
    }

    public long RacePayment
    {
        get
        {
            return racePayment;
        }

        set
        {
            racePayment = value;
        }
    }

    public Finance Finance
    {
        get
        {
            return finance;
        }

        set
        {
            finance = value;
        }
    }

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
