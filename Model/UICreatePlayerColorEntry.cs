// Decompiled with JetBrains decompiler
// Type: UICreatePlayerColorEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICreatePlayerColorEntry : MonoBehaviour
{
  public UICreatePlayerColorEntry.Type type;
  public Toggle toggle;
  public Image image;
  public UICreatePlayerAppearanceWidget widget;
  private int mIndex;

  public void Setup(int inIndex, Color inColor)
  {
    this.toggle.onValueChanged.RemoveAllListeners();
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle()));
    this.mIndex = inIndex;
    this.image.color = inColor;
  }

  private void OnToggle()
  {
    if (!this.toggle.isOn)
      return;
    if (this.type == UICreatePlayerColorEntry.Type.SkinTone)
      this.widget.SelectSkinTone(this.mIndex);
    else
      this.widget.SelectHairColour(this.mIndex);
    this.widget.screen.profileWidget.RefreshPortrait();
  }

  public enum Type
  {
    SkinTone,
    HairColour,
  }
}
