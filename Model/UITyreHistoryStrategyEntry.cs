// Decompiled with JetBrains decompiler
// Type: UITyreHistoryStrategyEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITyreHistoryStrategyEntry : MonoBehaviour
{
  public RectTransform rectTransform;
  public Color[] tyreIconColors;
  public Image sliderFill;
  public TextMeshProUGUI laps;
  public UITyreSetIcon tyreIcon;

  public void Setup(SessionStints.SessionStintData inStintData, float inPositionX, float inWidth, int inLaps)
  {
    if (inStintData == null)
      return;
    this.tyreIcon.SetTyreSet(inStintData.tyreCompound);
    this.sliderFill.color = this.tyreIconColors[(int) inStintData.tyreCompound];
    this.rectTransform.anchoredPosition = new Vector2(inPositionX, this.rectTransform.anchoredPosition.y);
    this.rectTransform.sizeDelta = new Vector2(inWidth, this.rectTransform.sizeDelta.y);
    if (inLaps > 0)
    {
      if (!this.laps.gameObject.activeSelf)
        this.laps.gameObject.SetActive(true);
      this.laps.text = inLaps.ToString();
    }
    else
    {
      if (!this.laps.gameObject.activeSelf)
        return;
      this.laps.gameObject.SetActive(false);
    }
  }
}
