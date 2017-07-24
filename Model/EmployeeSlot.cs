
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class EmployeeSlot
{
    public Contract.Job jobType = Contract.Job.Unemployed;
    public int slotID = -1;
    public Person personHired;
    private Team mTeam;

    public bool IsAvailable()
    {
        return this.personHired == null || this.personHired.contract.contractStatus == Contract.ContractStatus.Terminated;
    }
}
