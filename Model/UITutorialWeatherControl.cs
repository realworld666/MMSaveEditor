// Decompiled with JetBrains decompiler
// Type: UITutorialWeatherControl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UITutorialWeatherControl : UITutorialBespokeScript
{
  public UITutorialWeatherControl.WeatherControl weatherControl;

  protected override void Activate()
  {
    switch (this.weatherControl)
    {
      case UITutorialWeatherControl.WeatherControl.ClearSky:
        UITutorialWeatherControl.ClearSky();
        break;
      case UITutorialWeatherControl.WeatherControl.AddRain:
        UITutorialWeatherControl.AddRain();
        break;
    }
  }

  public static void ClearSky()
  {
    SessionWeatherDetails currentSessionWeather = Game.instance.sessionManager.currentSessionWeather;
    AnimationCurve wrappedCurve = new AnimationCurve();
    for (int index = 0; index < 50; ++index)
    {
      Keyframe key = new Keyframe((float) index, 0.0f);
      key.time += (float) index;
      wrappedCurve.AddKey(key);
    }
    currentSessionWeather.sessionRainIntensityCurve = new AnimationCurveKeysWrapper(wrappedCurve);
    currentSessionWeather.UpdateCurvesUsingRain();
    Game.instance.sessionManager.eventDetails.currentSession.SetSessionWeather(currentSessionWeather);
  }

  public static void AddRain()
  {
    Game.instance.sessionManager.currentSessionWeather.AddTutorialRain();
  }

  public enum WeatherControl
  {
    ClearSky,
    AddRain,
  }
}
