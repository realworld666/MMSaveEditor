
using System;
using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ContractManagerTeam
{
    private List<EmployeeSlot> mEmployeeSlots = new List<EmployeeSlot>();
    private List<EmployeeSlot> mNextYearEmployeeSlots = new List<EmployeeSlot>();
    private List<Contract> mSignedContracts;
    private List<Person> mProposedDrafts = new List<Person>();
    private List<Person> mCachedPeople = new List<Person>();
    private List<EmployeeSlot> mCachedEmployedSlots = new List<EmployeeSlot>();
    private Team mTeam;
    private Driver mLatestFiredActiveDriver;
    private Driver mHealingDriver;

    public void GetAllDrivers(ref List<Driver> drivers)
    {
        int count = this.mEmployeeSlots.Count;
        for (int index = 0; index < count; ++index)
        {
            if (this.mEmployeeSlots[index].jobType == Contract.Job.Driver && !this.mEmployeeSlots[index].IsAvailable())
            {
                Driver personHired = this.mEmployeeSlots[index].personHired as Driver;
                drivers.Add(personHired);
            }
        }
    }

    public Person GetPersonOnJob(Contract.Job inJob)
    {
        for (int index = 0; index < this.mEmployeeSlots.Count; ++index)
        {
            if (this.mEmployeeSlots[index].jobType == inJob && !this.mEmployeeSlots[index].IsAvailable())
                return this.mEmployeeSlots[index].personHired;
        }
        return (Person)null;
    }

    public EmployeeSlot GetSlotForPerson(Person inPerson)
    {
        EmployeeSlot employeeSlot = (EmployeeSlot)null;
        for (int index = 0; index < this.mEmployeeSlots.Count; ++index)
        {
            if (this.mEmployeeSlots[index].personHired == inPerson)
            {
                employeeSlot = this.mEmployeeSlots[index];
                break;
            }
        }
        return employeeSlot;
    }

    public List<Person> GetAllPeopleOnJob(Contract.Job inJob)
    {
        this.mCachedPeople.Clear();
        for (int index = 0; index < this.mEmployeeSlots.Count; ++index)
        {
            if (this.mEmployeeSlots[index].jobType == inJob && !this.mEmployeeSlots[index].IsAvailable())
                this.mCachedPeople.Add(this.mEmployeeSlots[index].personHired);
        }
        return this.mCachedPeople;
    }

    public void FirePerson(Person inPersonToFire, Contract.ContractTerminationType inContractTerminationType = Contract.ContractTerminationType.Generic)
    {
        /*if (this.hasDraftProposal(inPersonToFire) && inPersonToFire.contractManager.isRenewProposal)
        {
            if (this.mTeam.IsPlayersTeam())
                Game.instance.dialogSystem.OnContractElapsedOrFiredWhileRenewing(inPersonToFire);
            if (inPersonToFire.contractManager.isConsideringProposal)
                this.CancelDraftProposal(inPersonToFire);
            else
                this.RemoveDraftProposal(inPersonToFire);
        }*/
        //inPersonToFire.Contract.Job1 = Contract.Job.Unemployed;
        //inPersonToFire.Contract.SetContractTerminated(inContractTerminationType);
        //this.GetSlotForPerson(inPersonToFire).personHired = (Person)null;
        inPersonToFire.careerHistory.MarkLastEntryTeamAsFinished(this.mTeam);
        /*if (Game.IsActive() && this.mTeam.IsPlayersTeam() && (inPersonToFire != Game.instance.player && !Game.instance.dilemmaSystem.isFiringBecauseOfDilemma))
            Game.instance.player.ApplyLoyaltyChange(Player.LoyaltyChange.FiringTeamMember);
        if (!Game.IsActive())
            return;
        Game.instance.teamManager.teamRumourManager.RemoveRumoursForPerson(inPersonToFire);*/
    }

    public List<EmployeeSlot> GetAllEmployeeSlotsForJob(Contract.Job inJob)
    {
        this.mCachedEmployedSlots.Clear();
        for (int index = 0; index < this.mEmployeeSlots.Count; ++index)
        {
            if (this.mEmployeeSlots[index].jobType == inJob)
                this.mCachedEmployedSlots.Add(this.mEmployeeSlots[index]);
        }
        return this.mCachedEmployedSlots;
    }

    public void GetAllEmployeeSlotsForJob(Contract.Job inJob, ref List<EmployeeSlot> employee_slots)
    {
        for (int index = 0; index < this.mEmployeeSlots.Count; ++index)
        {
            if (this.mEmployeeSlots[index].jobType == inJob)
                employee_slots.Add(this.mEmployeeSlots[index]);
        }
    }

    public void GetAllMechanics(ref List<Mechanic> mechanics)
    {
        int count = this.mEmployeeSlots.Count;
        for (int index = 0; index < count; ++index)
        {
            if (this.mEmployeeSlots[index].jobType == Contract.Job.Mechanic && !this.mEmployeeSlots[index].IsAvailable())
            {
                Mechanic personHired = this.mEmployeeSlots[index].personHired as Mechanic;
                mechanics.Add(personHired);
            }
        }
    }

    public void AddSignedContract(ContractPerson inContract)
    {
        //inContract.SignContractAndSetContractEndEvents();

        //this.RemoveDraftProposal(inContract.person);
    }

    public bool IsSittingOutEvent(Driver inDriver)
    {
        return inDriver == this.mHealingDriver;
    }

    public void GetAllDriversForCar(ref List<Driver> drivers, int inCarIndex)
    {
        int count = this.mEmployeeSlots.Count;
        for (int index = 0; index < count; ++index)
        {
            if (this.mEmployeeSlots[index].jobType == Contract.Job.Driver && !this.mEmployeeSlots[index].IsAvailable())
            {
                Driver personHired = this.mEmployeeSlots[index].personHired as Driver;
                if (personHired.carID == inCarIndex)
                    drivers.Add(personHired);
            }
        }
    }
}
