using FullSerializer;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ContractPerson : Contract
{
    private ContractPerson.Status mCurrentStatus = ContractPerson.Status.Reserve;
    private ContractPerson.Status mProposedStatus = ContractPerson.Status.Reserve;
    public DateTime optionClauseEndDate = new DateTime();
    public string employeerName = string.Empty;
    public ContractPerson.BuyoutClauseSplit buyoutSplit = ContractPerson.BuyoutClauseSplit.PersonPaysAll;
    public int yearlyWages;
    public int signOnFee;
    public int qualifyingBonus;
    public int qualifyingBonusTargetPosition;
    public int raceBonus;
    public int raceBonusTargetPosition;
    public int championBonus;
    public int payDriver;
    public int amountForContractorToPay;
    public int amountForTargetToPay;
    public Entity employeer;
    public bool hasSignOnFee;
    public bool hasRaceBonus;
    public bool hasQualifyingBonus;
    public ContractPerson.ContractLength length;
    private Person mPerson;

    public ContractPerson.Status currentStatus
    {
        get
        {
            return this.mCurrentStatus;
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
