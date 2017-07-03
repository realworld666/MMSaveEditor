// Decompiled with JetBrains decompiler
// Type: SessionInformationDropdown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SessionInformationDropdown : UIBaseSessionHudDropdown
{
  public TextMeshProUGUI[] timeStampLabels = new TextMeshProUGUI[0];
  public GameObject[] graphicsContainers = new GameObject[3];
  public float refreshRate = 1f;
  private float mTimeCounter = 1f;
  private Weather mWeather = new Weather();
  public UIWeatherDropdownBarEntry[] sessionForecast;
  public Slider raceDurationSlider;
  public SessionInformationDropdown.DataType dataType;
  private SessionWeatherDetails mSessionWeatherDetails;

  protected override void OnEnable()
  {
    base.OnEnable();
    this.mTimeCounter = this.refreshRate;
    this.mSessionWeatherDetails = Game.instance.sessionManager.currentSessionWeather;
    this.SetForecast();
  }

  protected override void Update()
  {
    base.Update();
    this.mTimeCounter -= GameTimer.deltaTime;
    this.raceDurationSlider.normalizedValue = this.mSessionWeatherDetails.normalizedTimeElapsed;
    if ((double) this.mTimeCounter >= 0.0)
      return;
    this.SetForecast();
    this.mTimeCounter = this.refreshRate;
  }

  private void SetForecast()
  {
    if (this.mSessionWeatherDetails == null)
      return;
    for (int index = 0; index < this.graphicsContainers.Length; ++index)
      this.graphicsContainers[index].SetActive((SessionInformationDropdown.DataType) index == this.dataType);
    float sessionDuration = Game.instance.sessionManager.GetSessionDuration();
    int lapCount = Game.instance.sessionManager.lapCount;
    for (int index = 0; index < this.timeStampLabels.Length; ++index)
    {
      float num = (float) index / ((float) this.timeStampLabels.Length - 1f);
      if (Game.instance.sessionManager.eventDetails.currentSession.sessionType == SessionDetails.SessionType.Race)
      {
        StringVariableParser.intValue1 = Mathf.RoundToInt((float) lapCount * num);
        this.timeStampLabels[index].text = Localisation.LocaliseID("PSG_10010418", (GameObject) null);
      }
      else
      {
        StringVariableParser.intValue1 = Mathf.RoundToInt((float) (((double) sessionDuration - (double) sessionDuration * (double) num) / 60.0));
        this.timeStampLabels[index].text = Localisation.LocaliseID("PSG_10010417", (GameObject) null);
      }
    }
    float num1 = 1f / (float) this.sessionForecast.Length;
    switch (this.dataType)
    {
      case SessionInformationDropdown.DataType.Clouds:
        for (int inIndex = 0; inIndex < this.sessionForecast.Length; ++inIndex)
        {
          float time = this.GetTime(inIndex, this.sessionForecast.Length);
          this.mSessionWeatherDetails.GetWeather(time, ref this.mWeather);
          float num2 = time - this.mSessionWeatherDetails.normalizedTimeElapsed;
          bool inHighlight = (double) num2 >= 0.0 && (double) num2 <= (double) num1;
          this.sessionForecast[inIndex].SetupCloudData(this.mWeather, this.mSessionWeatherDetails, time);
          this.sessionForecast[inIndex].SetVisibility(this.dataType, time, inHighlight, this.mSessionWeatherDetails.normalizedTimeElapsed);
        }
        break;
      case SessionInformationDropdown.DataType.Water:
        for (int inIndex = 0; inIndex < this.sessionForecast.Length; ++inIndex)
        {
          float time = this.GetTime(inIndex, this.sessionForecast.Length);
          float inWater = Mathf.Clamp01(this.mSessionWeatherDetails.sessionWaterLevelCurve.curve.Evaluate(time));
          float num2 = time - this.mSessionWeatherDetails.normalizedTimeElapsed;
          bool inHighlight = (double) num2 >= 0.0 && (double) num2 <= (double) num1;
          this.sessionForecast[inIndex].SetupWaterData(inWater, time);
          this.sessionForecast[inIndex].SetVisibility(this.dataType, time, inHighlight, this.mSessionWeatherDetails.normalizedTimeElapsed);
        }
        break;
      case SessionInformationDropdown.DataType.Rubber:
        for (int inIndex = 0; inIndex < this.sessionForecast.Length; ++inIndex)
        {
          float time = this.GetTime(inIndex, this.sessionForecast.Length);
          float trackRubberAtTime = this.mSessionWeatherDetails.GetTrackRubberAtTime(time);
          float num2 = time - this.mSessionWeatherDetails.normalizedTimeElapsed;
          bool inHighlight = (double) num2 >= 0.0 && (double) num2 <= (double) num1;
          this.sessionForecast[inIndex].SetupRubberData(trackRubberAtTime, time);
          this.sessionForecast[inIndex].SetVisibility(this.dataType, time, inHighlight, this.mSessionWeatherDetails.normalizedTimeElapsed);
        }
        break;
    }
  }

  private float GetTime(int inIndex, int inLength)
  {
    float num = (float) (inIndex + 1) / (float) this.sessionForecast.Length;
    if (inIndex == 0)
      num = 0.0f;
    return num;
  }

  public enum DataType
  {
    Clouds,
    Water,
    Rubber,
    Count,
  }
}
