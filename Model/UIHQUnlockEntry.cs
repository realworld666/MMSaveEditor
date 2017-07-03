// Decompiled with JetBrains decompiler
// Type: UIHQUnlockEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHQUnlockEntry : MonoBehaviour
{
  public Button button;
  public TextMeshProUGUI headerLabel;
  public HeadquartersScreen screen;
  private HQsBuilding_v1 mBuilding;
  private HQsBuildingInfo.Type mType;
  private int mLevel;

  private void Awake()
  {
    if (!((Object) this.button != (Object) null))
      return;
    this.button.onClick.AddListener(new UnityAction(this.OnButton));
  }

  public void Setup(HQsBuilding_v1 inBuilding, HQsBuildingInfo.Type inType, int inLevel)
  {
    this.mBuilding = inBuilding;
    this.mType = inType;
    this.mLevel = inLevel;
    int count = this.mBuilding.info.dependencies.Count;
    int num1 = 0;
    for (int index = 0; index < count; ++index)
    {
      HQsDependency dependency = this.mBuilding.info.dependencies[index];
      if (dependency.isComplete(this.mBuilding.team) || dependency.CheckComplete(this.mType, this.mLevel))
        ++num1;
    }
    int num2 = Mathf.Min(Mathf.RoundToInt((float) ((double) num1 / (double) count * 100.0)), 100);
    this.headerLabel.text = (num2 >= 100 ? string.Empty : num2.ToString() + "% ") + this.mBuilding.buildingName;
  }

  private void OnButton()
  {
    if (this.mBuilding == null)
      return;
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.screen.SelectBuilding(this.mBuilding);
  }
}
