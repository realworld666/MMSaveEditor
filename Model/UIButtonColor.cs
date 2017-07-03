// Decompiled with JetBrains decompiler
// Type: UIButtonColor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UIButtonColor : MonoBehaviour
{
  private static Color regularNormalColor = GameUtility.ColorFromInts(84, 148, 165);
  private static Color regularHighlightedColor = GameUtility.ColorFromInts(66, 122, 137);
  private static Color regularPressedColor = GameUtility.ColorFromInts(46, 93, 105);
  private static Color regularDisabledColor = GameUtility.ColorFromInts(94, 94, 94);
  private static Color confirmNormalColor = GameUtility.ColorFromInts(0, (int) byte.MaxValue, 0);
  private static Color confirmHighlightedColor = GameUtility.ColorFromInts(0, (int) byte.MaxValue, 50);
  private static Color confirmPressedColor = GameUtility.ColorFromInts(50, (int) byte.MaxValue, 0);
  private static Color confirmDisabledColor = GameUtility.ColorFromInts(94, 94, 94);
  private static Color cancelNormalColor = GameUtility.ColorFromInts(171, 60, 60);
  private static Color cancelHighlightedColor = GameUtility.ColorFromInts(156, 40, 40);
  private static Color cancelPressedColor = GameUtility.ColorFromInts(129, 24, 24);
  private static Color cancelDisabledColor = GameUtility.ColorFromInts(94, 94, 94);
  private static Color closeNormalColor = GameUtility.ColorFromInts(150, 150, 150);
  private static Color closeHighlightedColor = GameUtility.ColorFromInts(130, 130, 130);
  private static Color closePressedColor = GameUtility.ColorFromInts(110, 110, 110);
  private static Color closeDisabledColor = GameUtility.ColorFromInts(94, 94, 94);
  private static Color continueNormalColor = GameUtility.ColorFromInts(242, 133, 24);
  private static Color continueHighlightedColor = GameUtility.ColorFromInts(240, 150, 60);
  private static Color continuePressedColor = GameUtility.ColorFromInts(242, 133, 24);
  private static Color continueDisabledColor = GameUtility.ColorFromInts(94, 94, 94);
  public float alpha = 1f;
  private float mCurrentAlpha = 1f;
  public UIButtonColor.ColorMode mode;
  private UIButtonColor.ColorMode mCurrentMode;
  private Image mImage;
  private Button mButton;
  private Toggle mToggle;

  private void Awake()
  {
    this.mImage = this.GetComponent<Image>();
    this.mButton = this.GetComponent<Button>();
    this.mToggle = this.GetComponent<Toggle>();
  }

  private void OnEnable()
  {
    this.UpdateColors();
  }

  private void Update()
  {
    if (this.mCurrentMode == this.mode && MathsUtility.Approximately(this.mCurrentAlpha, this.alpha, 0.0001f))
      return;
    this.UpdateColors();
  }

  public void SetMode(UIButtonColor.ColorMode inColorMode)
  {
    if (this.mCurrentMode == inColorMode)
      return;
    this.mode = inColorMode;
    this.UpdateColors();
  }

  private void UpdateColors()
  {
    this.mCurrentMode = this.mode;
    this.mCurrentAlpha = this.alpha;
    if (!((Object) this.mButton != (Object) null) && !((Object) this.mToggle != (Object) null))
      return;
    ColorBlock inColorBlock = !((Object) this.mButton != (Object) null) ? this.mToggle.colors : this.mButton.colors;
    switch (this.mode)
    {
      case UIButtonColor.ColorMode.Regular:
        this.SetColorBlock(ref inColorBlock, UIButtonColor.regularNormalColor, UIButtonColor.regularHighlightedColor, UIButtonColor.regularPressedColor, UIButtonColor.regularDisabledColor);
        break;
      case UIButtonColor.ColorMode.Confirm:
        this.SetColorBlock(ref inColorBlock, UIButtonColor.confirmNormalColor, UIButtonColor.confirmHighlightedColor, UIButtonColor.confirmPressedColor, UIButtonColor.confirmDisabledColor);
        break;
      case UIButtonColor.ColorMode.Cancel:
        this.SetColorBlock(ref inColorBlock, UIButtonColor.cancelNormalColor, UIButtonColor.cancelHighlightedColor, UIButtonColor.cancelPressedColor, UIButtonColor.cancelDisabledColor);
        break;
      case UIButtonColor.ColorMode.Close:
        this.SetColorBlock(ref inColorBlock, UIButtonColor.closeNormalColor, UIButtonColor.closeHighlightedColor, UIButtonColor.closePressedColor, UIButtonColor.closeDisabledColor);
        break;
      case UIButtonColor.ColorMode.Continue:
        this.SetColorBlock(ref inColorBlock, UIButtonColor.continueNormalColor, UIButtonColor.continueHighlightedColor, UIButtonColor.continuePressedColor, UIButtonColor.continueDisabledColor);
        break;
    }
    if ((Object) this.mButton != (Object) null)
      this.mButton.colors = inColorBlock;
    else if ((Object) this.mToggle != (Object) null)
      this.mToggle.colors = inColorBlock;
    if (!((Object) this.mImage != (Object) null))
      return;
    if ((Object) this.mButton != (Object) null && this.mButton.interactable || (Object) this.mToggle != (Object) null && this.mToggle.interactable)
      this.mImage.CrossFadeColor(inColorBlock.normalColor, 0.0f, true, true);
    else
      this.mImage.CrossFadeColor(inColorBlock.disabledColor, 0.0f, true, true);
  }

  private void SetColorBlock(ref ColorBlock inColorBlock, Color inNormal, Color inHighlighted, Color inPressed, Color inDisabled)
  {
    Color color1 = inNormal;
    color1.a = this.alpha;
    inColorBlock.normalColor = color1;
    Color color2 = inHighlighted;
    color2.a = this.alpha;
    inColorBlock.highlightedColor = color2;
    Color color3 = inPressed;
    color3.a = this.alpha;
    inColorBlock.pressedColor = color3;
    Color color4 = inDisabled;
    color4.a = this.alpha;
    inColorBlock.disabledColor = color4;
  }

  public enum ColorMode
  {
    Regular,
    Confirm,
    Cancel,
    Close,
    Continue,
  }
}
