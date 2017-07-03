// Decompiled with JetBrains decompiler
// Type: UITravelWeatherWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UITravelWeatherWidget : MonoBehaviour
{
  public UIWeatherIcon weatherIcon;
  public TextMeshProUGUI weatherType;
  public TextMeshProUGUI temperature;

  public void SetupWeatherWidget(SessionDetails inSessionDetails)
  {
    Weather inWeather = inSessionDetails.sessionWeather.forecastWeather ?? inSessionDetails.sessionWeather.GetCurrentWeather();
    this.weatherIcon.SetIcon(inWeather);
    if ((Object) this.weatherType != (Object) null)
      this.weatherType.text = inWeather.currentRainWeatherString;
    if (!((Object) this.temperature != (Object) null))
      return;
    this.temperature.text = GameUtility.GetTemperatureText((float) inWeather.airTemperature);
  }
}
