
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class HQsBuildingManager
{
  private static float scalar = 1000000f;
  private List<HQsBuildingInfo> mBuildingsInfo = new List<HQsBuildingInfo>();
    
}
