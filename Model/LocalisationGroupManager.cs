using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class LocalisationGroupManager
{
  private Map<string, LocalisationGroupManager.GroupInfo> mUsedGroups = new Map<string, LocalisationGroupManager.GroupInfo>();
  
  [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
  public class GroupInfo
  {
    public List<int> usedEntries = new List<int>();
  }
}
