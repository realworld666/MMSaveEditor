// Decompiled with JetBrains decompiler
// Type: UIPartComponentIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UIPartComponentIcon : MonoBehaviour
{
  public GameObject emptyIcon;
  public GameObject icon;
  public GameObject circleContainer;
  public GameObject hexagonContainer;
  public Image circleBacking;
  public Image hexagonBacking;
  public UIComponentIcon partIcon;
  public RectTransform rectTransform;
  private CarPartComponent mComponent;

  public void Setup(CarPartComponent inComponent)
  {
    this.mComponent = inComponent;
    if (this.mComponent == null)
    {
      this.icon.SetActive(false);
      this.emptyIcon.SetActive(true);
    }
    else
    {
      this.icon.SetActive(true);
      this.emptyIcon.SetActive(false);
      this.partIcon.SetComponent(this.mComponent);
      this.circleContainer.SetActive(this.mComponent.componentType == CarPartComponent.ComponentType.Engineer);
      this.hexagonContainer.SetActive(this.mComponent.componentType != CarPartComponent.ComponentType.Engineer);
      this.circleBacking.color = UIConstants.GetPartLevelColor(this.mComponent.level);
      this.hexagonBacking.color = this.circleBacking.color;
    }
  }

  private void ShowComponentTooltip()
  {
    if (this.mComponent == null)
      return;
    scSoundManager.BlockSoundEvents = true;
    UIManager.instance.dialogBoxManager.GetDialog<UIDialogBoxComponentTooltip>().ShowTooltip(this.mComponent);
    scSoundManager.BlockSoundEvents = false;
  }

  private void HideComponentTooltip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<UIDialogBoxComponentTooltip>().HideTooltip();
  }
}
