// Decompiled with JetBrains decompiler
// Type: UIWeatherDropdownBarEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWeatherDropdownBarEntry : MonoBehaviour
{
  public Color barColorRubber = new Color();
  public Color barColorWater = new Color();
  public Color barColorClouds = new Color();
  public GameObject[] highLightContainer;
  public Image[] imagesToColor;
  public UIWeatherIcon weatherIcon;
  public TextMeshProUGUI label;
  public Slider barSlider;
  public CanvasGroup canvasGroup;

  public void SetVisibility(SessionInformationDropdown.DataType inDataType, float inTime, bool inHighlight, float inNormalizedSessionTime)
  {
    this.weatherIcon.gameObject.SetActive(inDataType == SessionInformationDropdown.DataType.Clouds);
    this.label.gameObject.SetActive(inDataType == SessionInformationDropdown.DataType.Clouds);
    float num1 = inNormalizedSessionTime;
    for (int index = 0; index < this.highLightContainer.Length; ++index)
      GameUtility.SetActive(this.highLightContainer[index], inHighlight);
    HQsBuilding_v1 building = Game.instance.player.team.headquarters.GetBuilding(HQsBuildingInfo.Type.LogisticsCentre);
    int num2 = 5;
    if (building.isBuilt)
      num2 += (building.currentLevel + 1) * 5;
    float num3 = 0.05f * (float) num2;
    if ((double) inTime < (double) num1 || inHighlight)
    {
      this.gameObject.SetActive(true);
      this.canvasGroup.alpha = 1f;
    }
    else if ((double) inTime < (double) num1 + (double) num3)
    {
      this.gameObject.SetActive(true);
      this.label.text = "-";
      this.canvasGroup.alpha = 1f - Mathf.Clamp01((inTime - num1) / (0.05f * (float) num2));
    }
    else
      this.gameObject.SetActive(false);
  }

  private void ColorImages(Color inColor)
  {
    for (int index = 0; index < this.imagesToColor.Length; ++index)
      this.imagesToColor[index].color = inColor;
  }

  public void SetupWaterData(float inWater, float inTime)
  {
    this.barSlider.minValue = 0.0f;
    this.barSlider.maxValue = 1f;
    GameUtility.SetSliderAmountIfDifferent(this.barSlider, inWater, 1000f);
    this.barSlider.image.color = this.barColorWater;
    this.ColorImages(this.barColorWater);
  }

  public void SetupRubberData(float inRubber, float inTime)
  {
    this.barSlider.minValue = 0.0f;
    this.barSlider.maxValue = 1f;
    GameUtility.SetSliderAmountIfDifferent(this.barSlider, inRubber, 1000f);
    this.barSlider.image.color = this.barColorRubber;
    this.ColorImages(this.barColorRubber);
  }

  public void SetupCloudData(Weather inWeather, SessionWeatherDetails inWeatherDetails, float inTime)
  {
    this.weatherIcon.SetIcon(inWeather);
    this.label.text = GameUtility.GetTemperatureText((float) inWeather.airTemperature);
    this.barSlider.minValue = inWeatherDetails.minAirTemp - 3f;
    this.barSlider.maxValue = inWeatherDetails.maxAirTemp + 3f;
    GameUtility.SetSliderAmountIfDifferent(this.barSlider, (float) inWeather.airTemperature, 1000f);
    this.barSlider.image.color = this.barColorClouds;
    this.ColorImages(this.barColorClouds);
  }

  private void SetAlpha(float inTime)
  {
    float normalizedSessionTime = Game.instance.sessionManager.GetNormalizedSessionTime();
    if ((double) inTime < (double) normalizedSessionTime)
      this.canvasGroup.alpha = 0.8f;
    else
      this.canvasGroup.alpha = 1f;
  }
}
