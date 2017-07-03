// Decompiled with JetBrains decompiler
// Type: UIHQUnlockTickEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIHQUnlockTickEntry : MonoBehaviour
{
  public TextMeshProUGUI headingLabel;
  public GameObject tick;
  private HQsDependency mDependency;
  private UnlockRequirementHQBuildingLevel mRequirement;

  public void Setup(HQsDependency inDependency, bool inComplete)
  {
    this.mDependency = inDependency;
    StringVariableParser.intValue1 = this.mDependency.requiredLevel + 1;
    StringVariableParser.building = Game.instance.player.team.headquarters.GetBuilding(this.mDependency.buildingType);
    this.headingLabel.text = Localisation.LocaliseID("PSG_10010247", (GameObject) null);
    GameUtility.SetActive(this.tick, inComplete);
  }

  public void Setup(UnlockRequirementHQBuildingLevel inRequirement)
  {
    this.mRequirement = inRequirement;
    StringVariableParser.intValue1 = this.mRequirement.buildingLevel + 1;
    StringVariableParser.building = Game.instance.player.team.headquarters.GetBuilding(this.mRequirement.buildingType);
    this.headingLabel.text = Localisation.LocaliseID("PSG_10010247", (GameObject) null);
    GameUtility.SetActive(this.tick, !this.mRequirement.IsLocked(Game.instance.player.team));
  }
}
