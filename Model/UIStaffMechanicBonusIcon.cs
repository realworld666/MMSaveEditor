// Decompiled with JetBrains decompiler
// Type: UIStaffMechanicBonusIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UIStaffMechanicBonusIcon : MonoBehaviour
{
  public Image backing;
  public RectTransform rectTransform;
  public GameObject unlocked;
  private MechanicBonus mBonus;
  private bool mUnlocked;

  public void Setup(bool inUnlocked, MechanicBonus inBonus)
  {
    if (inBonus == null)
      return;
    this.mBonus = inBonus;
    this.mUnlocked = inUnlocked;
    RectTransform component = this.transform.parent.GetComponent<RectTransform>();
    Vector3 anchoredPosition = (Vector3) this.rectTransform.anchoredPosition;
    anchoredPosition.x = component.sizeDelta.x * ((float) inBonus.bonusUnlockAt / 100f);
    this.rectTransform.anchoredPosition = (Vector2) anchoredPosition;
    this.SetIcon(this.mUnlocked, inBonus);
  }

  public void SetIcon(bool isUnlocked, MechanicBonus inMechanicBonus)
  {
    GameUtility.SetActive(this.backing.gameObject, true);
    this.backing.color = !isUnlocked ? UIConstants.staffScreenGrey : UIConstants.staffScreenOrange;
    GameUtility.SetActive(this.unlocked, isUnlocked);
  }

  private void ShowMechanicBonusTooltip()
  {
    if (this.mBonus == null)
      return;
    UIManager.instance.dialogBoxManager.GetDialog<UIDialogBoxMechanicBonusTooltip>().ShowTooltip(this.mUnlocked, this.mBonus);
  }

  private void HideMechanicBonusTooltip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<UIDialogBoxMechanicBonusTooltip>().HideTooltip();
  }
}
