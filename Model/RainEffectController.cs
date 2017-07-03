// Decompiled with JetBrains decompiler
// Type: RainEffectController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class RainEffectController : MonoBehaviour
{
  [SerializeField]
  private ParticleSystem rainParticleSystem;
  private EnvironmentManager.MinMaxFloat mParticleSize;
  private EnvironmentManager.MinMaxFloat mRate;
  private EnvironmentManager.MinMaxFloat mSpeed;

  private void Awake()
  {
    this.mRate = new EnvironmentManager.MinMaxFloat(0.0f, 8000f, 0.0f);
    this.mParticleSize = new EnvironmentManager.MinMaxFloat(0.05f, 0.3f, 0.05f);
    this.mSpeed = new EnvironmentManager.MinMaxFloat(0.0f, 20f, 0.0f);
  }

  public void SimulationUpdate()
  {
    GameUtility.SetActive(this.rainParticleSystem.gameObject, true);
    SessionWeatherDetails currentSessionWeather = Game.instance.sessionManager.currentSessionWeather;
    float inNormalizedValue = 0.0f;
    if (App.instance.gameStateManager.currentState.group != GameState.Group.Frontend)
      inNormalizedValue = currentSessionWeather.GetNormalizedRain();
    this.mRate.SetNormalizedValue(inNormalizedValue);
    this.mParticleSize.SetNormalizedValue(inNormalizedValue);
    this.mSpeed.SetNormalizedValue(inNormalizedValue);
    this.rainParticleSystem.emission.rate = new ParticleSystem.MinMaxCurve(this.mRate.value);
    this.rainParticleSystem.forceOverLifetime.z = new ParticleSystem.MinMaxCurve(this.mSpeed.value);
    this.rainParticleSystem.startSize = this.mParticleSize.value;
  }

  private void LateUpdate()
  {
    this.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
  }
}
