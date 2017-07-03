// Decompiled with JetBrains decompiler
// Type: ColorPickerCustomColorEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorPickerCustomColorEntry : MonoBehaviour
{
  private Color mColor = Color.white;
  public Toggle toggle;
  public Image imageColor;
  private ColorPickerDialogBox mColorPickerDialogBox;

  public Color color
  {
    get
    {
      return this.mColor;
    }
  }

  public void Setup(Color inColor, ColorPickerDialogBox inDialogBox)
  {
    this.mColor = inColor;
    this.mColorPickerDialogBox = inDialogBox;
    this.RemoveListener();
    this.AddListener();
    this.imageColor.color = this.mColor;
    GameUtility.SetActive(this.gameObject, true);
  }

  public void RemoveListener()
  {
    this.toggle.onValueChanged.RemoveAllListeners();
  }

  public void AddListener()
  {
    this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggle));
  }

  public void SetToggle(bool inValue)
  {
    if (this.toggle.isOn || !inValue)
      return;
    this.RemoveListener();
    this.toggle.isOn = inValue;
    this.AddListener();
  }

  private void OnToggle(bool inValue)
  {
    if (!inValue)
      return;
    this.mColorPickerDialogBox.SelectCustomColor(this.mColor);
  }
}
