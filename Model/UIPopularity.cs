// Decompiled with JetBrains decompiler
// Type: UIPopularity
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopularity : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI headingLabel;
  [SerializeField]
  private TextMeshProUGUI valueLabel;
  [SerializeField]
  private Image fill;

  public void SetValue(string inHeading, float inValue)
  {
    this.headingLabel.text = inHeading;
    this.valueLabel.text = Mathf.RoundToInt(inValue * 100f).ToString() + "%";
    this.fill.fillAmount = Mathf.Clamp01(inValue);
  }
}
