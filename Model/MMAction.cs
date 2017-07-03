using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class MMAction 
{
  [fsProperty]
  private List<object> targets = new List<object>();
  [fsProperty]
  private List<string> methodNames = new List<string>();
  private Action actionCache;
  private Action actionCacheNonSerialized;
    
}
