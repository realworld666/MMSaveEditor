
using System;
using System.Collections.Generic;
using FullSerializer;


[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class StatModificationHistory
{
    private readonly int mMaxHistoryEntries = 5;
    private List<StatModificationHistory.StatModificationEntry> mStatModificationHistoryEntries = new List<StatModificationHistory.StatModificationEntry>();
    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class StatModificationEntry
    {
        private DateTime mModificationAppliedDate = new DateTime();
        private float mModificationAmount;
        private string mModificationName;

    }
}
