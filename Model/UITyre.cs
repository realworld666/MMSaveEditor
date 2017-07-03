// Decompiled with JetBrains decompiler
// Type: UITyre
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITyre : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI shortNameLabel;
  [SerializeField]
  private Text shortNameTextLabel;
  [SerializeField]
  private Image tyreWear;
  [SerializeField]
  private Image background;
  private TyreSet mTyre;

  public TyreSet tyre
  {
    get
    {
      return this.mTyre;
    }
  }

  private void Update()
  {
    if (this.mTyre == null)
      return;
    this.tyreWear.fillAmount = this.mTyre.GetCondition();
  }

  public void SetTyre(TyreSet inTyre)
  {
    if (this.mTyre == inTyre)
      return;
    this.mTyre = inTyre;
    if ((Object) this.shortNameLabel != (Object) null)
      this.shortNameLabel.text = this.mTyre.GetShortName();
    else if ((Object) this.shortNameTextLabel != (Object) null)
      this.shortNameTextLabel.text = this.mTyre.GetShortName();
    Color color1 = inTyre.GetColor();
    if ((Object) this.background != (Object) null)
    {
      Color color2 = this.background.color;
      color2.r = color1.r;
      color2.g = color1.g;
      color2.b = color1.b;
      this.background.color = color2;
    }
    Color color3 = this.tyreWear.color;
    color3.r = color1.r;
    color3.g = color1.g;
    color3.b = color1.b;
    this.tyreWear.color = color3;
    this.Update();
  }
}
