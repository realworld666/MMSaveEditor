// Decompiled with JetBrains decompiler
// Type: DriverCareerFormWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DriverCareerFormWidget : MonoBehaviour
{
  private static Color goodFormColor = GameUtility.ColorFromInts(99, 184, 106);
  private static Color formColor = GameUtility.ColorFromInts(99, 184, 170);
  public Slider[] formSlider;
  public Image[] formFillImage;
  public TextMeshProUGUI[] formLabel;

  private void Awake()
  {
    for (int index = 0; index < this.formSlider.Length; ++index)
    {
      this.formSlider[index].minValue = DriverCareerForm.minFormRating;
      this.formSlider[index].maxValue = DriverCareerForm.maxFormRating;
    }
  }

  public void SetDriver(Driver inDriver)
  {
    if (inDriver.IsFreeAgent() || inDriver.IsReserveDriver() && inDriver.GetChampionshipEntry() == null)
    {
      GameUtility.SetActive(this.gameObject, false);
    }
    else
    {
      GameUtility.SetActive(this.gameObject, true);
      float[] formValues = inDriver.careerForm.formValues;
      for (int index1 = this.formSlider.Length - 1; index1 >= 0; --index1)
      {
        float num = 0.0f;
        int index2 = this.formSlider.Length - 1 - index1;
        if (index2 < formValues.Length)
          num = formValues[index2];
        this.formSlider[index1].value = num;
        if ((double) num > 0.0)
          this.formLabel[index1].text = num.ToString("0.0", (IFormatProvider) Localisation.numberFormatter);
        else
          this.formLabel[index1].text = "-";
        if ((double) num >= 8.0)
          this.formFillImage[index1].color = DriverCareerFormWidget.goodFormColor;
        else
          this.formFillImage[index1].color = DriverCareerFormWidget.formColor;
      }
    }
  }
}
