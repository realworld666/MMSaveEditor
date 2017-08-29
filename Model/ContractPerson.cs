using FullSerializer;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

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
    public ContractPerson.ContractLength length;
    public ContractPerson.BuyoutClauseSplit buyoutSplit = ContractPerson.BuyoutClauseSplit.PersonPaysAll;
    private Person mPerson;

    public Person person
    {
        get
        {
            return this.mPerson;
        }
    }
    public ContractPerson.Status currentStatus
    {
        get
        {
            return this.mCurrentStatus;
        }
        set
        {
            this.mCurrentStatus = value;
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

    public ContractLength Length
    {
        get { return length; }
        set { length = value; }
    }

    public BuyoutClauseSplit BuyoutSplit
    {
        get { return buyoutSplit; }
        set { buyoutSplit = value; }
    }

    public Team GetTeam()
    {
        if (this.employeer is Team)
            return (Team)this.employeer;
        return (Team)Game.instance.teamManager.nullTeam;
    }

    public void SetTeam(Team t)
    {
        this.employeer = t;
    }

    public override void SetContractTerminated(Contract.ContractTerminationType inContractTerminationType = Contract.ContractTerminationType.Generic)
    {
        if (this.contractStatus == Contract.ContractStatus.Terminated)
            return;
        Team team = this.GetTeam();

        base.SetContractTerminated(Contract.ContractTerminationType.Generic);

        this.employeer = (Entity)null;
        this.employeerName = string.Empty;
        if (!(this.mCalendarEvent.triggerDate > Game.instance.time.now))
            return;
    }

    public virtual void Editor_SetTeam(Team newTeam, Person replacing)
    {
        bool flag = false;


        Debug.Assert(replacing != null, "Can not currently make free agents");

        Person inNewPersonToHire = person;

        Team oldTeam = inNewPersonToHire.Contract.GetTeam();
        Team replacingTeam = replacing.Contract.GetTeam();

        Debug.Assert(replacingTeam == newTeam);

        EmployeeSlot oldSlot = oldTeam.contractManager.GetSlotForPerson(inNewPersonToHire);
        EmployeeSlot newSlot = replacingTeam.contractManager.GetSlotForPerson(replacing);

        if (!inNewPersonToHire.IsFreeAgent())
        {
            flag = oldTeam.IsPlayersTeam();
            //if (!person.IsReplacementPerson())
            //  this.PayOtherTeamTerminationCosts(team, inNewPersonToHire.contract);
            oldTeam.contractManager.FirePerson(inNewPersonToHire, Contract.ContractTerminationType.HiredBySomeoneElse);
            /*if (inNewPersonToHire is Mechanic)
                team.contractManager.HireReplacementMechanic();
            else if (inNewPersonToHire is Engineer)
                team.contractManager.HireReplacementEngineer();
            else if (inNewPersonToHire is Driver)
                team.contractManager.HireReplacementDriver();*/

        }
        if (!replacing.IsFreeAgent())
        {
            flag = newTeam.IsPlayersTeam();
            //if (!person.IsReplacementPerson())
            //  this.PayOtherTeamTerminationCosts(team, inNewPersonToHire.contract);
            newTeam.contractManager.FirePerson(replacing, Contract.ContractTerminationType.HiredBySomeoneElse);
            /*if (inNewPersonToHire is Mechanic)
                team.contractManager.HireReplacementMechanic();
            else if (inNewPersonToHire is Engineer)
                team.contractManager.HireReplacementEngineer();
            else if (inNewPersonToHire is Driver)
                team.contractManager.HireReplacementDriver();*/

        }


        Debug.Assert(newSlot != null);
        {
            newSlot.personHired = inNewPersonToHire;
            inNewPersonToHire.Contract.SetTeam(newTeam);
            //inNewPersonToHire.Contract.SetPerson(inNewPersonToHire);
            inNewPersonToHire.Contract.SetContractState(Contract.ContractStatus.OnGoing);
            //this.AddSignedContract(inNewPersonToHire.contract);
            if (inNewPersonToHire is Mechanic)
            {
                (inNewPersonToHire as Mechanic).SetDriverRelationship(0.0f, 0);
                newTeam.carManager.partImprovement.AssignChiefMechanics();
            }
            if (inNewPersonToHire is TeamPrincipal)
                inNewPersonToHire.Contract.GetTeam().chairman.ResetHappiness();
            if (inNewPersonToHire is Chairman)
                (inNewPersonToHire as Chairman).ResetHappiness();
        }

        Debug.Assert(oldSlot != null);
        {
            oldSlot.personHired = replacing;
            replacing.Contract.SetTeam(oldTeam);
            //inNewPersonToHire.Contract.SetPerson(inNewPersonToHire);
            replacing.Contract.SetContractState(Contract.ContractStatus.OnGoing);
            //this.AddSignedContract(inNewPersonToHire.contract);
            if (replacing is Mechanic)
            {
                (replacing as Mechanic).SetDriverRelationship(0.0f, 0);
                newTeam.carManager.partImprovement.AssignChiefMechanics();
            }
            if (replacing is TeamPrincipal)
                replacing.Contract.GetTeam().chairman.ResetHappiness();
            if (replacing is Chairman)
                (replacing as Chairman).ResetHappiness();
        }

        if (inNewPersonToHire is Driver)
        {
            Driver newDriver = inNewPersonToHire as Driver;
            /*if (!inDriver.IsReplacementPerson())
            {
                inDriver.moraleStatModificationHistory.ClearHistory();
                inDriver.UpdateMoraleOnContractSigned();
            }*/
            DriverManager driverManager = Game.instance.driverManager;
            if (inNewPersonToHire.Contract.currentStatus != ContractPerson.Status.Reserve)
            {
                driverManager.AddDriverToChampionship(newDriver);
                newTeam.SelectMainDriversForSession();
                newTeam.championship.standings.UpdateStandings();
                newTeam.GetMechanicOfDriver(newDriver).SetDriverRelationship(0.0f, 0);
            }
            if (newTeam.IsPlayersTeam())
            {
                newDriver.SetBeenScouted();
            }
            else
            {
                //newTeam.teamAIController.RemoveDriverFromScoutingJobs(inDriver);
                //newTeam.teamAIController.EvaluateDriverLineUp();
            }


            Driver oldDriver = replacing as Driver;
            /*if (!inDriver.IsReplacementPerson())
            {
                inDriver.moraleStatModificationHistory.ClearHistory();
                inDriver.UpdateMoraleOnContractSigned();
            }*/
            if (oldDriver.Contract.currentStatus != ContractPerson.Status.Reserve)
            {
                driverManager.AddDriverToChampionship(oldDriver);
                oldTeam.SelectMainDriversForSession();
                oldTeam.championship.standings.UpdateStandings();
                oldTeam.GetMechanicOfDriver(oldDriver).SetDriverRelationship(0.0f, 0);
            }
            if (oldTeam.IsPlayersTeam())
            {
                oldDriver.SetBeenScouted();
            }
            else
            {
                //newTeam.teamAIController.RemoveDriverFromScoutingJobs(inDriver);
                //newTeam.teamAIController.EvaluateDriverLineUp();
            }
        }
    }

    public enum Status
    {
        [LocalisationID("PSG_10005057")] Equal,
        [LocalisationID("PSG_10005068")] One,
        [LocalisationID("PSG_10005069")] Two,
        [LocalisationID("PSG_10005070")] Reserve,
    }

    public enum BuyoutClauseSplit
    {
        [LocalisationID("PSG_10009283")] TeamPaysAll,
        [LocalisationID("PSG_10009282")] EvenSplit,
        [LocalisationID("PSG_10009281")] PersonPaysAll,
    }
}
