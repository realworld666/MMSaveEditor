using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CareerHistory
{
  private List<CareerHistoryEntry> mCareer = new List<CareerHistoryEntry>();
  private Person mPerson;
}
