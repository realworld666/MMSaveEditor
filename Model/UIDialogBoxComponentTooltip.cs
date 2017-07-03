// Decompiled with JetBrains decompiler
// Type: UIDialogBoxComponentTooltip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIDialogBoxComponentTooltip : UIDialogBox
{
  [SerializeField]
  private RectTransform rectTransform;
  [SerializeField]
  private UIPartComponentIcon componentIcon;
  [SerializeField]
  private TextMeshProUGUI title;
  private CarPartComponent mComponent;

  public void ShowTooltip(CarPartComponent inComponent)
  {
    if (inComponent == null)
      return;
    this.mComponent = inComponent;
    GameUtility.SetTooltipTransformInsideScreen(this.rectTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
    this.componentIcon.Setup(this.mComponent);
    this.title.text = this.mComponent.GetName((CarPart) null);
    GameUtility.SetActive(this.gameObject, true);
  }

  private void Update()
  {
    GameUtility.SetTooltipTransformInsideScreen(this.rectTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
  }

  public void HideTooltip()
  {
    GameUtility.SetActive(this.gameObject, false);
  }
}
