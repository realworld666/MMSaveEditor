// Decompiled with JetBrains decompiler
// Type: UIPreferencesSlider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPreferencesSlider : MonoBehaviour
{
  public Slider slider;
  public TextMeshProUGUI number;
  private int sliderValue;

  public void SetSliderValue(float inValue)
  {
    GameUtility.SetSliderAmountIfDifferent(this.slider, Mathf.Clamp(inValue, this.slider.minValue, this.slider.maxValue), 1000f);
    this.sliderValue = Mathf.RoundToInt(this.slider.value);
    if (!((Object) this.number != (Object) null))
      return;
    this.number.text = this.sliderValue.ToString() + "%";
  }

  public int GetSliderValue()
  {
    return Mathf.RoundToInt(this.slider.value);
  }
}
