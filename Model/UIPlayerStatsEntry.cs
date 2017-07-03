// Decompiled with JetBrains decompiler
// Type: UIPlayerStatsEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStatsEntry : MonoBehaviour
{
  public GameObject arrowUp;
  public GameObject arrowDown;
  public Image backing;
  public Image bar;
  public TextMeshProUGUI title;
  public TextMeshProUGUI number;
  private int mValue;
  private float mValueNormalized;
  private float mPreviousValue;

  public void Setup(float inValue)
  {
    if ((double) inValue == (double) this.mPreviousValue && (double) inValue != 0.0)
      return;
    this.mValue = Mathf.FloorToInt(inValue);
    this.mValueNormalized = Mathf.Clamp01(inValue / 20f);
    this.bar.fillAmount = Mathf.Clamp01(inValue - (float) this.mValue);
    this.SetColor();
    this.number.text = this.mValue.ToString();
    if ((double) inValue > (double) this.mPreviousValue)
    {
      this.arrowUp.SetActive(true);
      this.arrowDown.SetActive(false);
    }
    else if ((double) inValue < (double) this.mPreviousValue)
    {
      this.arrowUp.SetActive(false);
      this.arrowDown.SetActive(true);
    }
    this.mPreviousValue = inValue;
  }

  public void SetColor()
  {
    Color color = UIConstants.colorBandGreen;
    if ((double) this.mValueNormalized < 0.25)
      color = UIConstants.colorBandRed;
    else if ((double) this.mValueNormalized < 0.5)
      color = UIConstants.colorBandYellow;
    else if ((double) this.mValueNormalized < 0.75)
      color = UIConstants.colorBandBlue;
    color.a = this.bar.color.a;
    this.bar.color = color;
    color.a = this.backing.color.a;
    this.backing.color = color;
  }
}
