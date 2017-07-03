// Decompiled with JetBrains decompiler
// Type: UIPartConditionBar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UIPartConditionBar : MonoBehaviour
{
  public Slider conditionDangerZone;
  public Slider conditionValueSlider;
  public Image conditionBacking;

  public void Setup(CarPart inPart)
  {
    this.conditionDangerZone.normalizedValue = Mathf.Min(inPart.partCondition.redZone, inPart.partCondition.condition);
    this.conditionValueSlider.normalizedValue = inPart.partCondition.condition;
    this.conditionValueSlider.fillRect.GetComponent<Image>().color = inPart.partCondition.GetConditionColor();
    GameUtility.SetImageFillAmountIfDifferent(this.conditionBacking, inPart.stats.reliability, 1f / 512f);
  }
}
