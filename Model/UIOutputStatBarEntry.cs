// Decompiled with JetBrains decompiler
// Type: UIOutputStatBarEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIOutputStatBarEntry : MonoBehaviour
{
  private CarChassisStats.Stats mStat = CarChassisStats.Stats.Count;
  public CanvasGroup canvasGroup;
  public Slider statSlider;
  public Slider supplierContributionSlider;
  public Slider previewSlider;
  public TextMeshProUGUI statName;
  public UIAbilityStars stars;
  public Transform icon;

  public CarChassisStats.Stats stat
  {
    get
    {
      return this.mStat;
    }
  }

  public void Setup(CarChassisStats.Stats inStat, float inValue)
  {
    this.mStat = inStat;
    this.statName.text = Localisation.LocaliseEnum((Enum) this.mStat);
    this.statSlider.value = inValue;
    float chassisStatMax = GameStatsConstants.chassisStatMax;
    this.statSlider.minValue = 0.0f;
    this.statSlider.maxValue = chassisStatMax;
    this.supplierContributionSlider.minValue = 0.0f;
    this.supplierContributionSlider.maxValue = chassisStatMax;
    this.previewSlider.minValue = 0.0f;
    this.previewSlider.maxValue = chassisStatMax;
    this.SetIcon(this.mStat);
    this.HidePreview();
  }

  public void ResetStat()
  {
    this.SetMinStatValue(0.0f);
  }

  public void AddToStatValue(float inValue)
  {
    this.SetMinStatValue(this.statSlider.value += inValue);
  }

  public void SetSupplierContribution(float inValue)
  {
    this.supplierContributionSlider.value = inValue;
  }

  public void SetMaxStatValue(float inValue)
  {
    this.statSlider.value = this.statSlider.value + inValue;
    this.UpdateStarsLabel();
  }

  private void SetMinStatValue(float inValue)
  {
    this.statSlider.value = inValue;
    this.UpdateStarsLabel();
  }

  private void UpdateStarsLabel()
  {
    this.stars.SetStarsValue((float) ((double) this.statSlider.value / (double) GameStatsConstants.chassisStatMax * 5.0), 0.0f);
  }

  private void SetIcon(CarChassisStats.Stats inStat)
  {
    if (!((UnityEngine.Object) this.icon != (UnityEngine.Object) null))
      return;
    for (int index = 0; index < this.icon.childCount; ++index)
    {
      if ((CarChassisStats.Stats) index == inStat)
        this.icon.GetChild(index).gameObject.SetActive(true);
      else
        this.icon.GetChild(index).gameObject.SetActive(false);
    }
  }

  public void ShowPreview(float inValue)
  {
    this.previewSlider.gameObject.SetActive(true);
    this.previewSlider.value = this.statSlider.value + inValue;
  }

  public void HidePreview()
  {
    this.previewSlider.gameObject.SetActive(false);
  }

  public void Highlight(bool inState)
  {
    if (inState)
      this.canvasGroup.alpha = 1f;
    else
      this.canvasGroup.alpha = 0.3f;
  }

  public float GetStatValue()
  {
    return this.statSlider.value;
  }
}
