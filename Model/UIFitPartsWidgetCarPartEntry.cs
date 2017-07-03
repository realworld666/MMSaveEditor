// Decompiled with JetBrains decompiler
// Type: UIFitPartsWidgetCarPartEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFitPartsWidgetCarPartEntry : MonoBehaviour
{
  public TextMeshProUGUI levelLabel;
  public Image levelBacking;
  public Transform iconParent;
  private CarPart mPart;

  public void Setup(CarPart inPart)
  {
    this.gameObject.SetActive(true);
    this.mPart = inPart;
    this.levelLabel.text = CarPart.GetLevelUIString(this.mPart.stats.level);
    this.levelBacking.color = UIConstants.GetPartLevelColor(this.mPart.stats.level);
    for (int index = 0; index < this.iconParent.childCount; ++index)
      this.iconParent.GetChild(index).gameObject.SetActive((CarPart.PartType) index == this.mPart.GetPartType());
  }

  public void ShowTooltip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<UIPartInfoPopupWidget>().ShowTooltip(this.mPart, (RectTransform) null);
  }

  public void HideTooltip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<UIPartInfoPopupWidget>().HideTooltip();
  }
}
