
using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ContractManagerTeam
{
    private List<EmployeeSlot> mEmployeeSlots = new List<EmployeeSlot>();
    private List<EmployeeSlot> mNextYearEmployeeSlots = new List<EmployeeSlot>();
    private List<Person> mProposedDrafts = new List<Person>();
    private List<Person> mCachedPeople = new List<Person>();
    private List<EmployeeSlot> mCachedEmployedSlots = new List<EmployeeSlot>();
    private List<Contract> mSignedContracts;
    private Team mTeam;
    private Driver mLatestFiredActiveDriver;

}
