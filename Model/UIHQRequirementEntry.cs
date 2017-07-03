// Decompiled with JetBrains decompiler
// Type: UIHQRequirementEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHQRequirementEntry : MonoBehaviour
{
  public Button button;
  public TextMeshProUGUI headingLabel;
  public HeadquartersScreen screen;
  private HQsBuilding_v1 mBuilding;
  private HQsDependency mDependency;

  private void Awake()
  {
    this.button.onClick.AddListener(new UnityAction(this.OnButton));
  }

  public void Setup(HQsDependency inDependency)
  {
    this.mDependency = inDependency;
    this.mBuilding = Game.instance.player.team.headquarters.GetBuilding(this.mDependency.buildingType);
    StringVariableParser.intValue1 = this.mDependency.requiredLevel + 1;
    StringVariableParser.building = this.mBuilding;
    this.headingLabel.text = Localisation.LocaliseID("PSG_10010247", (GameObject) null);
  }

  private void OnButton()
  {
    if (this.mDependency == null || this.mBuilding == null || !((Object) this.screen != (Object) null))
      return;
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.screen.SelectBuilding(this.mBuilding);
  }
}
