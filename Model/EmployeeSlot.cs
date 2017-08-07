
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class EmployeeSlot
{
    public Contract.Job jobType = Contract.Job.Unemployed;
    public Person personHired;
    public int slotID = -1;
    private Team mTeam;

    public bool IsAvailable()
    {
        return this.personHired == null || this.personHired.Contract.contractStatus == Contract.ContractStatus.Terminated;
    }
}
