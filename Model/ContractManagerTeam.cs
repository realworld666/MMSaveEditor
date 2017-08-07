
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
}
