// Decompiled with JetBrains decompiler
// Type: UICarSetupTyreIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICarSetupTyreIcon : MonoBehaviour
{
  [Tooltip("Tyre icons order: \nSuperSoft\nSoft\nMedium\nHard\nIntermediate\nWet\nUltra")]
  public Color[] tyreIconColors = new Color[0];
  public Image fillImage;
  public UITyreIcon tyreIcon;
  public UITyreSetMiniIcon miniTyreIcon;
  public TextMeshProUGUI compoundName;

  public void SetTyre(TyreSet inTyreSet)
  {
    if ((Object) this.tyreIcon != (Object) null)
      this.tyreIcon.SetTyreIcon(inTyreSet);
    else if ((Object) this.miniTyreIcon != (Object) null)
      this.miniTyreIcon.SetTyreSet(inTyreSet);
    GameUtility.SetImageFillAmountIfDifferent(this.fillImage, inTyreSet.GetCondition(), 1f / 360f);
    this.fillImage.color = this.tyreIconColors[(int) inTyreSet.GetCompound()];
    if (this.fillImage.fillClockwise)
      this.fillImage.fillClockwise = false;
    if (!((Object) this.compoundName != (Object) null))
      return;
    this.compoundName.text = inTyreSet.GetCompound().ToString();
  }

  public void SetTyre(TyreSet.Compound inCompound, float inCondition)
  {
    if ((Object) this.tyreIcon != (Object) null)
      this.tyreIcon.SetTyreIcon(inCompound);
    else if ((Object) this.miniTyreIcon != (Object) null)
      this.miniTyreIcon.SetTyreSet(inCompound);
    this.fillImage.fillClockwise = false;
    GameUtility.SetImageFillAmountIfDifferent(this.fillImage, inCondition, 1f / 512f);
    this.fillImage.color = this.tyreIconColors[(int) inCompound];
  }
}
