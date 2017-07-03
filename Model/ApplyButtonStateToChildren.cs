// Decompiled with JetBrains decompiler
// Type: ApplyButtonStateToChildren
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ApplyButtonStateToChildren : MonoBehaviour
{
  public Color enabledColor = Color.white;
  public Color disabledColor = Color.white;
  public bool affectAlphaOnly;
  private Button mButton;
  private bool mIsInteractable;
  private Image[] mImage;
  private TextMeshProUGUI[] mText;

  private void Awake()
  {
    this.mButton = this.GetComponent<Button>();
    if (!((Object) this.mButton != (Object) null))
      return;
    this.mIsInteractable = this.mButton.interactable;
    this.mImage = this.GetComponentsInChildren<Image>(true);
    this.mText = this.GetComponentsInChildren<TextMeshProUGUI>(true);
    this.UpdateState();
  }

  private void LateUpdate()
  {
    if (!((Object) this.mButton != (Object) null) || this.mIsInteractable == this.mButton.interactable)
      return;
    this.UpdateState();
  }

  private void UpdateState()
  {
    for (int index = 0; index < this.mImage.Length; ++index)
    {
      if ((Object) this.mButton.image != (Object) this.mImage[index])
      {
        if (this.affectAlphaOnly)
        {
          Color color1 = this.mImage[index].color;
          Color color2 = this.mImage[index].color;
          color1.a = this.enabledColor.a;
          color2.a = this.disabledColor.a;
          this.mImage[index].color = !this.mButton.interactable ? color2 : color1;
        }
        else
          this.mImage[index].color = !this.mButton.interactable ? this.disabledColor : this.enabledColor;
      }
    }
    for (int index = 0; index < this.mText.Length; ++index)
    {
      if (this.affectAlphaOnly)
      {
        Color color1 = this.mText[index].color;
        Color color2 = this.mText[index].color;
        color1.a = this.enabledColor.a;
        color2.a = this.disabledColor.a;
        this.mText[index].color = !this.mButton.interactable ? color2 : color1;
      }
      else
        this.mText[index].color = !this.mButton.interactable ? this.disabledColor : this.enabledColor;
    }
    this.mIsInteractable = this.mButton.interactable;
  }
}
