// Decompiled with JetBrains decompiler
// Type: UIMechanicBonusIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIMechanicBonusIcon : MonoBehaviour
{
  public GameObject emptyIcon;
  public GameObject icon;
  public UIComponentIcon partIcon;
  public RectTransform rectTransform;
  private MechanicBonus mBonus;
  private bool mUnlocked;

  public void Setup(bool inUnlocked, MechanicBonus inBonus)
  {
    this.mBonus = inBonus;
    this.mUnlocked = inUnlocked;
    if (this.mBonus == null || !this.mUnlocked)
    {
      GameUtility.SetActive(this.icon, false);
      if (!((Object) this.emptyIcon != (Object) null))
        return;
      GameUtility.SetActive(this.emptyIcon, true);
    }
    else
    {
      GameUtility.SetActive(this.icon, true);
      if ((Object) this.emptyIcon != (Object) null)
        GameUtility.SetActive(this.emptyIcon, false);
      this.partIcon.SetComponent(this.mBonus);
    }
  }

  private void ShowComponentTooltip()
  {
    if (this.mBonus == null)
      return;
    scSoundManager.BlockSoundEvents = true;
    UIManager.instance.dialogBoxManager.GetDialog<UIDialogBoxMechanicBonusTooltip>().ShowTooltip(this.mUnlocked, this.mBonus);
    scSoundManager.BlockSoundEvents = false;
  }

  private void HideComponentTooltip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<UIDialogBoxMechanicBonusTooltip>().HideTooltip();
  }
}
