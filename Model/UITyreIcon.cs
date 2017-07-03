// Decompiled with JetBrains decompiler
// Type: UITyreIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UITyreIcon : MonoBehaviour
{
  public Sprite[] tyreSprites = new Sprite[0];
  private Image mTyreImage;
  private TyreSet mCurrentTyreSet;

  public Image tyreImage
  {
    get
    {
      return this.mTyreImage;
    }
  }

  public void SetTyreIcon(TyreSet inTyreSet)
  {
    if (this.tyreSprites.Length == 0 || inTyreSet == null)
      return;
    if ((Object) this.mTyreImage == (Object) null)
      this.mTyreImage = this.gameObject.GetComponent<Image>();
    if (!((Object) this.mTyreImage != (Object) null) || this.mCurrentTyreSet != null && this.mCurrentTyreSet.GetCompound() == inTyreSet.GetCompound())
      return;
    this.mCurrentTyreSet = inTyreSet;
    this.mTyreImage.sprite = this.tyreSprites[(int) inTyreSet.GetCompound()];
  }

  public void SetTyreIcon(TyreSet.Compound inTyreSetCompound)
  {
    if (this.tyreSprites.Length == 0)
      return;
    if ((Object) this.mTyreImage == (Object) null)
      this.mTyreImage = this.gameObject.GetComponent<Image>();
    this.mTyreImage.sprite = this.tyreSprites[(int) inTyreSetCompound];
  }
}
