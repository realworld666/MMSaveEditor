// Decompiled with JetBrains decompiler
// Type: UITeamColor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UITeamColor : MonoBehaviour
{
  [SerializeField]
  private bool doNormalColor = true;
  [SerializeField]
  private bool doHighlightedColor = true;
  [SerializeField]
  private bool doPressedColor = true;
  [SerializeField]
  private bool doDisabledColor = true;
  [SerializeField]
  private float alpha = 1f;
  private bool mAutoUpdate = true;
  [SerializeField]
  private bool useSecondaryColour;
  private Image mImage;
  private Button mButton;
  private Toggle mToggle;
  private TextMeshProUGUI mText;

  private void Awake()
  {
    this.mImage = this.GetComponent<Image>();
    this.mButton = this.GetComponent<Button>();
    this.mToggle = this.GetComponent<Toggle>();
    this.mText = this.GetComponent<TextMeshProUGUI>();
  }

  private void OnEnable()
  {
    if (!this.mAutoUpdate)
      return;
    this.UpdateColor();
  }

  public void UpdateColor()
  {
    TeamColor currentColour = App.instance.uiTeamColourManager.currentColour;
    TeamColor.UIColour inTeamColour = !this.useSecondaryColour ? currentColour.primaryUIColour : currentColour.secondaryUIColour;
    if ((Object) this.mImage != (Object) null && (Object) this.mButton == (Object) null && ((Object) this.mToggle == (Object) null && (Object) this.mText == (Object) null))
    {
      Color normal = inTeamColour.normal;
      normal.a = this.alpha;
      this.mImage.color = normal;
    }
    if ((Object) this.mText != (Object) null)
    {
      Color normal = inTeamColour.normal;
      normal.a = this.alpha;
      this.mText.color = normal;
    }
    this.SetColorBlock(inTeamColour);
  }

  public void SetTeamColor(TeamColor inTeamColour)
  {
    this.mAutoUpdate = false;
    this.SetColorBlock(!this.useSecondaryColour ? inTeamColour.primaryUIColour : inTeamColour.secondaryUIColour);
  }

  private void SetColorBlock(TeamColor.UIColour inTeamColour)
  {
    if ((Object) this.mButton != (Object) null || (Object) this.mToggle != (Object) null)
    {
      ColorBlock colorBlock = !((Object) this.mButton != (Object) null) ? this.mToggle.colors : this.mButton.colors;
      if (this.doNormalColor)
      {
        Color normal = inTeamColour.normal;
        normal.a = this.alpha;
        colorBlock.normalColor = normal;
      }
      if (this.doHighlightedColor)
      {
        Color highlighted = inTeamColour.highlighted;
        highlighted.a = this.alpha;
        colorBlock.highlightedColor = highlighted;
      }
      if (this.doPressedColor)
      {
        Color pressed = inTeamColour.pressed;
        pressed.a = this.alpha;
        colorBlock.pressedColor = pressed;
      }
      if (this.doDisabledColor)
      {
        Color disabled = inTeamColour.disabled;
        disabled.a = this.alpha;
        colorBlock.disabledColor = disabled;
      }
      if ((Object) this.mButton != (Object) null)
        this.mButton.colors = colorBlock;
      else if ((Object) this.mToggle != (Object) null)
        this.mToggle.colors = colorBlock;
      if (!((Object) this.mImage != (Object) null))
        return;
      if ((Object) this.mButton != (Object) null && this.mButton.interactable || (Object) this.mToggle != (Object) null && this.mToggle.interactable)
        this.mImage.CrossFadeColor(colorBlock.normalColor, 0.0f, true, true);
      else
        this.mImage.CrossFadeColor(colorBlock.disabledColor, 0.0f, true, true);
    }
    else
    {
      if (this.mAutoUpdate || !((Object) this.mImage != (Object) null))
        return;
      this.mImage.color = inTeamColour.normal;
    }
  }
}
