// Decompiled with JetBrains decompiler
// Type: UnlockRequirementHQBuildingLevel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UnlockRequirementHQBuildingLevel : CarPartUnlockRequirement
{
  public HQsBuildingInfo.Type buildingType;
  public int buildingLevel;
  public int setLevel;

  public string buildingLevelUI
  {
    get
    {
      return (this.buildingLevel + 1).ToString();
    }
  }

  public override bool IsLocked(Team inTeam)
  {
    HQsBuilding_v1 building = inTeam.headquarters.GetBuilding(this.buildingType);
    return building != null && (!building.isBuilt || building.currentLevel < this.buildingLevel);
  }

  public override string GetDescription(Team inTeam)
  {
    HQsBuilding_v1 building = inTeam.headquarters.GetBuilding(this.buildingType);
    if (building == null)
      return string.Empty;
    StringVariableParser.building = building;
    StringVariableParser.intValue1 = this.buildingLevel + 1;
    return Localisation.LocaliseID("PSG_10011113", (GameObject) null);
  }
}
