// Decompiled with JetBrains decompiler
// Type: UIFinanceDetailsEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIFinanceDetailsEntry : MonoBehaviour
{
  public TextMeshProUGUI descriptionLabel;
  public TextMeshProUGUI valueLabel;

  public void SetLabels(string inDescription, string inValue)
  {
    this.descriptionLabel.text = inDescription;
    this.valueLabel.text = inValue;
  }

  public void SetLabels(string inDescription, string inValue, Color inColor)
  {
    this.descriptionLabel.text = inDescription;
    this.valueLabel.text = inValue;
    this.valueLabel.color = inColor;
  }

  public void SetLabels(string inDescription, long inCurrencyValue)
  {
    this.descriptionLabel.text = inDescription;
    this.valueLabel.text = GameUtility.GetCurrencyStringWithSign(inCurrencyValue, 0);
    this.valueLabel.color = GameUtility.GetCurrencyColor(inCurrencyValue);
  }

  public void SetLabels(string inDescription, float inValue, Color inColor, bool inShowSign)
  {
    this.descriptionLabel.text = inDescription;
    if (inShowSign)
      this.valueLabel.text = GameUtility.FormatSecondsToStringWithSign(inValue);
    else
      this.valueLabel.text = GameUtility.FormatSecondsToString(inValue, 1);
    this.valueLabel.color = inColor;
  }
}
