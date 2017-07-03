// Decompiled with JetBrains decompiler
// Type: UIWeatherSessionWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIWeatherSessionWidget : MonoBehaviour
{
  public UIWeatherIcon weatherIcon;
  public TextMeshProUGUI timeLabel;
  public TextMeshProUGUI sessionTypeLabel;
  public TextMeshProUGUI temperatureLabel;
  public TextMeshProUGUI minTemperatureLabel;
  public TextMeshProUGUI rainStateLabel;

  public void Setup(Weather inWeather)
  {
    this.weatherIcon.SetIcon(inWeather);
    this.temperatureLabel.text = inWeather.airTemperature.ToString() + "°";
    this.rainStateLabel.text = inWeather.currentRainWeatherString;
  }

  public void SetMinAirTemperature(SessionWeatherDetails inDetails)
  {
    if (!((Object) this.minTemperatureLabel != (Object) null))
      return;
    this.minTemperatureLabel.text = ((double) inDetails.minAirTemp).ToString() + "°";
  }
}
