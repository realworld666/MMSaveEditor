using FullSerializer;
using System;
using System.ComponentModel.DataAnnotations;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ContractPerson : Contract
{
    private ContractPerson.Status mCurrentStatus = ContractPerson.Status.Reserve;
    private ContractPerson.Status mProposedStatus = ContractPerson.Status.Reserve;
    public DateTime optionClauseEndDate = new DateTime();
    private int yearlyWages;
    private int signOnFee;
    private int qualifyingBonus;
    private int qualifyingBonusTargetPosition;
    private int raceBonus;
    private int raceBonusTargetPosition;
    private int championBonus;
    public int payDriver;
    public int amountForContractorToPay;
    public int amountForTargetToPay;
    public Entity employeer;
    public string employeerName = string.Empty;
    public bool hasSignOnFee;
    public bool hasRaceBonus;
    public bool hasQualifyingBonus;
    public ContractLength length;
    public ContractPerson.BuyoutClauseSplit buyoutSplit = ContractPerson.BuyoutClauseSplit.PersonPaysAll;
    private Person mPerson;

    public ContractPerson.Status currentStatus
    {
        get
        {
            return this.mCurrentStatus;
        }
    }

    [DisplayFormat(DataFormatString = "{0:n0}")]
    public int YearlyWages
    {
        get
        {
            return yearlyWages;
        }

        set
        {
            yearlyWages = value;
        }
    }

    public int SignOnFee
    {
        get
        {
            return signOnFee;
        }

        set
        {
            signOnFee = value;
        }
    }

    public int QualifyingBonus
    {
        get
        {
            return qualifyingBonus;
        }

        set
        {
            qualifyingBonus = value;
        }
    }

    public int QualifyingBonusTargetPosition
    {
        get
        {
            return qualifyingBonusTargetPosition;
        }

        set
        {
            qualifyingBonusTargetPosition = value;
        }
    }

    public int RaceBonus
    {
        get
        {
            return raceBonus;
        }

        set
        {
            raceBonus = value;
        }
    }

    public int RaceBonusTargetPosition
    {
        get
        {
            return raceBonusTargetPosition;
        }

        set
        {
            raceBonusTargetPosition = value;
        }
    }

    public int ChampionBonus
    {
        get
        {
            return championBonus;
        }

        set
        {
            championBonus = value;
        }
    }

    public Team GetTeam()
    {
        if (this.employeer is Team)
            return (Team)this.employeer;
        return (Team)Game.Instance.teamManager.nullTeam;
    }

    public enum Status
    {
        [LocalisationID("PSG_10005057")] Equal,
        [LocalisationID("PSG_10005068")] One,
        [LocalisationID("PSG_10005069")] Two,
        [LocalisationID("PSG_10005070")] Reserve,
    }

    public enum ContractLength
    {
        Short,
        Medium,
        Long,
    }

    public enum BuyoutClauseSplit
    {
        [LocalisationID("PSG_10009283")] TeamPaysAll,
        [LocalisationID("PSG_10009282")] EvenSplit,
        [LocalisationID("PSG_10009281")] PersonPaysAll,
    }
}
