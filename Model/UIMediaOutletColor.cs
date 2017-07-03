// Decompiled with JetBrains decompiler
// Type: UIMediaOutletColor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMediaOutletColor : MonoBehaviour
{
  [SerializeField]
  private UIMediaOutletColor.ColorScheme colorScheme;

  public void SetColor(MediaOutlet inOutlet)
  {
    Image component1 = this.GetComponent<Image>();
    TextMeshProUGUI component2 = this.GetComponent<TextMeshProUGUI>();
    Color color = new Color();
    switch (this.colorScheme)
    {
      case UIMediaOutletColor.ColorScheme.Primary:
        color = inOutlet.primaryColor;
        break;
      case UIMediaOutletColor.ColorScheme.Secondary:
        color = inOutlet.secondaryColor;
        break;
    }
    if ((Object) component1 != (Object) null)
      component1.color = color;
    if (!((Object) component2 != (Object) null))
      return;
    component2.color = color;
  }

  public enum ColorScheme
  {
    Primary,
    Secondary,
  }
}
