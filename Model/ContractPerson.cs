using FullSerializer;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ContractPerson : Contract
{
    private ContractPerson.Status mCurrentStatus = ContractPerson.Status.Reserve;
    private ContractPerson.Status mProposedStatus = ContractPerson.Status.Reserve;
    public DateTime optionClauseEndDate = new DateTime();
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
    public string employeerName = string.Empty;
    public bool hasSignOnFee;
    public bool hasRaceBonus;
    public bool hasQualifyingBonus;
    public ContractPerson.ContractLength length;
    public ContractPerson.BuyoutClauseSplit buyoutSplit = ContractPerson.BuyoutClauseSplit.PersonPaysAll;
    private Person mPerson;
    protected Team mEmployeerTeam;

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

    public ContractPerson.Status proposedStatus
    {
        get
        {
            return this.mProposedStatus;
        }
    }

    public ContractPerson()
    {
    }

    public ContractPerson(ContractPerson inContractPerson)
    {
        this.mCurrentStatus = inContractPerson.currentStatus;
        this.mProposedStatus = inContractPerson.proposedStatus;
        this.optionClauseEndDate = inContractPerson.optionClauseEndDate;
        this.SetContractState(inContractPerson.contractStatus);
        this.startDate = inContractPerson.startDate;
        this.endDate = inContractPerson.endDate;
        this.length = inContractPerson.length;
        this.yearlyWages = inContractPerson.yearlyWages;
        this.hasSignOnFee = inContractPerson.hasSignOnFee;
        this.signOnFee = inContractPerson.signOnFee;
        this.hasQualifyingBonus = inContractPerson.hasQualifyingBonus;
        this.qualifyingBonus = inContractPerson.qualifyingBonus;
        this.qualifyingBonusTargetPosition = inContractPerson.qualifyingBonusTargetPosition;
        this.hasRaceBonus = inContractPerson.hasRaceBonus;
        this.raceBonus = inContractPerson.raceBonus;
        this.raceBonusTargetPosition = inContractPerson.raceBonusTargetPosition;
        this.championBonus = inContractPerson.championBonus;
        this.payDriver = inContractPerson.payDriver;
        this.buyoutSplit = inContractPerson.buyoutSplit;
        this.amountForContractorToPay = inContractPerson.amountForContractorToPay;
        this.amountForTargetToPay = inContractPerson.amountForTargetToPay;
        this.employeer = inContractPerson.employeer;
        this.employeerName = inContractPerson.employeerName;
        this.job = inContractPerson.job;
        this.mPerson = inContractPerson.mPerson;
        this.mCalendarEvent = inContractPerson.mCalendarEvent;
        if (this.mPerson == null || this.mCalendarEvent == null)
            return;
        this.mCalendarEvent.showOnCalendar = !this.mPerson.IsReplacementPerson();
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
        this.mEmployeerTeam = t;
    }

    public void SetPerson(Person inPerson)
    {
        this.mPerson = inPerson;
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

        ContractPerson myOriginalContract = new ContractPerson(this);
        ContractPerson replacingOriginalContract = new ContractPerson(replacing.Contract);

        if (oldSlot == null || newSlot == null)
        {
            MessageBoxResult result = MessageBox.Show("Currently you can only swap drivers that both have a team.", "Work in Progress", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (!inNewPersonToHire.IsFreeAgent())
        {
            flag = oldTeam.IsPlayersTeam();
            //if (!person.IsReplacementPerson())
            //  this.PayOtherTeamTerminationCosts(team, inNewPersonToHire.contract);
            oldTeam.contractManager.FirePerson(inNewPersonToHire, Contract.ContractTerminationType.HiredBySomeoneElse);


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
            inNewPersonToHire.Contract.SetPerson(inNewPersonToHire);
            inNewPersonToHire.Contract.SetContractState(Contract.ContractStatus.OnGoing);
            newTeam.contractManager.AddSignedContract(inNewPersonToHire.Contract);

            if (inNewPersonToHire is TeamPrincipal)
                inNewPersonToHire.Contract.GetTeam().chairman.ResetHappiness();
            if (inNewPersonToHire is Chairman)
                (inNewPersonToHire as Chairman).ResetHappiness();
        }

        Debug.Assert(oldSlot != null);
        {
            oldSlot.personHired = replacing;
            replacing.Contract.SetTeam(oldTeam);
            replacing.Contract.SetPerson(replacing);
            replacing.Contract.SetContractState(Contract.ContractStatus.OnGoing);
            oldTeam.contractManager.AddSignedContract(inNewPersonToHire.Contract);

            if (replacing is TeamPrincipal)
                replacing.Contract.GetTeam().chairman.ResetHappiness();
            (replacing as Chairman)?.ResetHappiness();
        }

        Mechanic newMechanic = inNewPersonToHire as Mechanic;
        if (newMechanic != null)
        {
            Mechanic oldMechanic = replacing as Mechanic;
            Debug.Assert(oldMechanic != null);

            int oldDriver = oldMechanic.driver;
            oldMechanic.driver = newMechanic.driver;
            newMechanic.driver = oldDriver;

            newMechanic.SetDefaultDriverRelationship();

            newTeam.carManager.partImprovement.mechanicOnPerformance = null;
            newTeam.carManager.partImprovement.mechanicOnReliability = null;

            newTeam.carManager.partImprovement.AssignChiefMechanics();
            if (newTeam.HasAIPitcrew)
            {
                newTeam.pitCrewController.AIPitCrew.RegenerateTaskStats();
            }

            oldMechanic.SetDefaultDriverRelationship();

            oldTeam.carManager.partImprovement.mechanicOnPerformance = null;
            oldTeam.carManager.partImprovement.mechanicOnReliability = null;

            oldTeam.carManager.partImprovement.AssignChiefMechanics();
            if (oldTeam.HasAIPitcrew)
            {
                oldTeam.pitCrewController.AIPitCrew.RegenerateTaskStats();
            }
        }

        if (inNewPersonToHire is Driver)
        {
            Driver newDriver = inNewPersonToHire as Driver;
            DriverManager driverManager = Game.instance.driverManager;

            // Swap some of the contract details around
            newDriver.contract.currentStatus = replacingOriginalContract.currentStatus;

            newDriver.joinsAnySeries = true;

            // No longer a reserve driver after the swap
            if (myOriginalContract.currentStatus == Status.Reserve && replacingOriginalContract.currentStatus != Status.Reserve)
            {
                driverManager.AddDriverToChampionship(newDriver);
                newTeam.AssignDriverToCar(newDriver);
                newTeam.SelectMainDriversForSession();
                newTeam.championship.standings.UpdateStandings();
            }
            else if (myOriginalContract.currentStatus != Status.Reserve && replacingOriginalContract.currentStatus == Status.Reserve)
            {
                // Switching from main driver to reserve
                driverManager.RemoveDriverEntryFromChampionship(newDriver);
                newDriver.SetCarID(-1);
                newTeam.SelectMainDriversForSession();
                newTeam.championship.standings.UpdateStandings();
            }
            Mechanic mechanicOfDriver = newTeam.GetMechanicOfDriver(newDriver);
            mechanicOfDriver?.SetDriverRelationship(0.0f, 0);


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
            oldDriver.joinsAnySeries = true;

            // Swap some of the contract details around
            oldDriver.contract.currentStatus = myOriginalContract.currentStatus;

            // No longer a reserve driver after the swap
            if (myOriginalContract.currentStatus != Status.Reserve && replacingOriginalContract.currentStatus == Status.Reserve)
            {
                driverManager.AddDriverToChampionship(oldDriver);
                oldTeam.AssignDriverToCar(oldDriver);
                oldTeam.SelectMainDriversForSession();
                oldTeam.championship.standings.UpdateStandings();
            }
            else if (myOriginalContract.currentStatus == Status.Reserve && replacingOriginalContract.currentStatus != Status.Reserve)
            {
                // Switching from main driver to reserve
                driverManager.RemoveDriverEntryFromChampionship(oldDriver);
                oldDriver.SetCarID(-1);
                oldTeam.SelectMainDriversForSession();
                oldTeam.championship.standings.UpdateStandings();
            }
            mechanicOfDriver = oldTeam.GetMechanicOfDriver(oldDriver);
            mechanicOfDriver?.SetDriverRelationship(0.0f, 0);

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

        if (inNewPersonToHire is Mechanic)
        {
            oldTeam.RefreshMechanics();
            newTeam.RefreshMechanics();
        }
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
