using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject("v0", MemberSerialization = fsMemberSerialization.OptOut)]
public class LocalisationGroupManager
{
    private Map<string, LocalisationGroupManager.GroupInfo> mUsedGroups = new Map<string, LocalisationGroupManager.GroupInfo>();

    [fsObject("v0", MemberSerialization = fsMemberSerialization.OptOut)]
    public class GroupInfo
    {
        public List<int> usedEntries = new List<int>();
    }

    internal string GetTextFromGroup(LocalisationGroup mGroup)
    {
        throw new NotImplementedException();
    }
}
