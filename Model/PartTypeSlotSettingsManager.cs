
using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PartTypeSlotSettingsManager
{
  public Dictionary<int, Dictionary<CarPart.PartType, PartTypeSlotSettings>> championshipPartSettings = new Dictionary<int, Dictionary<CarPart.PartType, PartTypeSlotSettings>>();
    
}
