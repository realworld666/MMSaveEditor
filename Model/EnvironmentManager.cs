// Decompiled with JetBrains decompiler
// Type: EnvironmentManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using uSky;

public class EnvironmentManager : MonoBehaviour
{
  [SerializeField]
  private RainEffectController[] rainEffectControllers = new RainEffectController[0];
  [SerializeField]
  private float minSunIntensity = 0.5f;
  [SerializeField]
  private float minShadowStrength = 0.5f;
  [SerializeField]
  private float maxLightValue = 8f;
  [SerializeField]
  private float maxFogValue = 1f / 1000f;
  [SerializeField]
  private float mMinUnityFogValue = 1f / 1000f;
  [SerializeField]
  private float mMaxUnityFogValue = 1f / 1000f;
  [SerializeField]
  private Color fogColor = new Color();
  private float mLigthiningCooldown = 5f;
  private List<Light> mTrackLights = new List<Light>();
  private List<Material> mTrackSurfaceShaders = new List<Material>();
  private List<Material> mSurfaceWaterShaders = new List<Material>();
  private List<Material> mEmmissiveLightMaterials = new List<Material>();
  private Material[] mCloudMaterials = new Material[4];
  private Dictionary<Material, List<Renderer>> mSceneMaterialsCache = new Dictionary<Material, List<Renderer>>();
  private float mTargetNormalisedCloud = float.MaxValue;
  private float mCurrentNormalisedCloud = float.MaxValue;
  [SerializeField]
  private bool mSetUnityFogSeperatly;
  [SerializeField]
  private CameraEffectsScriptableObject effectsObject;
  private uSkyManager mSkyManager;
  private uSkyLight mSkyLight;
  private uSkyFog mSkyFog;
  private Light mSunLight;
  private DistanceCloud mCloud;
  private CircuitScene mCircuitScene;
  private LightiningController mLightiningController;
  private EnvironmentManager.MinMaxFloat mSunIntensity;
  private EnvironmentManager.MinMaxFloat mShadowStrenght;
  private EnvironmentManager.MinMaxFloat mWetness;
  private EnvironmentManager.MinMaxFloat mWaterDisplacement;
  private EnvironmentManager.MinMaxFloat mRubberness;
  private EnvironmentManager.MinMaxFloat mFogValues;
  private EnvironmentManager.MinMaxFloat mUnityFogValues;
  private EnvironmentManager.MinMaxFloat mLightValues;
  private bool mIsLightingAvailable;
  private EnvironmentManager.LightOptions mLightQuality;
  private float mNormalizedRampingValue;
  private float mPreviousRampingValue;
  private bool mEmmissiveLightsOn;

  public bool enviromentLightsOn
  {
    get
    {
      return this.mEmmissiveLightsOn;
    }
  }

  public bool setUnityFogSeperatly
  {
    get
    {
      return this.mSetUnityFogSeperatly;
    }
    set
    {
      this.mSetUnityFogSeperatly = value;
    }
  }

  public float minUnityFogValue
  {
    get
    {
      return this.mMinUnityFogValue;
    }
    set
    {
      this.mMinUnityFogValue = value;
    }
  }

  public float maxUnityFogValue
  {
    get
    {
      return this.mMaxUnityFogValue;
    }
    set
    {
      this.mMaxUnityFogValue = value;
    }
  }

  public static event GameCameraEffectsHolderChangeEventHandler GameCameraEffectsHolderChangeEvent;

  public static event GameCameraEffectsHolderResetEventHandler GameCameraEffectsHolderResetEvent;

  private void Awake()
  {
    this.mSkyManager = this.GetComponentInChildren<uSkyManager>();
    this.mSkyLight = this.GetComponentInChildren<uSkyLight>();
    this.mSkyFog = this.GetComponentInChildren<uSkyFog>();
    this.mSunLight = this.mSkyManager.SunLight.GetComponent<Light>();
    this.mCloud = this.GetComponentInChildren<DistanceCloud>();
    this.mCircuitScene = this.transform.parent.GetComponent<CircuitScene>();
    this.mLightiningController = new LightiningController(this.mSkyLight);
    this.mSunIntensity = new EnvironmentManager.MinMaxFloat(this.minSunIntensity, this.mSkyLight.SunIntensity, this.mSkyLight.SunIntensity);
    this.mShadowStrenght = new EnvironmentManager.MinMaxFloat(this.minShadowStrength, this.mSunLight.shadowStrength, this.mSunLight.shadowStrength);
    float density = this.mSkyFog.Density;
    this.mFogValues = new EnvironmentManager.MinMaxFloat(density, this.maxFogValue, density);
    this.mUnityFogValues = new EnvironmentManager.MinMaxFloat(this.mMinUnityFogValue, this.mMaxUnityFogValue, this.mMinUnityFogValue);
    this.mWaterDisplacement = new EnvironmentManager.MinMaxFloat(0.4f, 0.6f, 0.4f);
    this.mLightValues = new EnvironmentManager.MinMaxFloat(0.0f, this.maxLightValue, 0.0f);
    this.mRubberness = new EnvironmentManager.MinMaxFloat(0.0f, 1f, 0.0f);
    this.mWetness = new EnvironmentManager.MinMaxFloat(0.0f, 1f, 0.0f);
    this.mTrackSurfaceShaders = this.FindMaterialsForShader("Custom/Standard_TrackSurface");
    this.mSurfaceWaterShaders = this.FindMaterialsForShader("Custom/Standard_SurfaceWater");
    this.mEmmissiveLightMaterials = this.FindMaterialsForShader("Custom/Standard_LightVolume");
    this.mCloudMaterials[0] = new Material(Resources.Load<Material>("Art/Skies/Default_Clear_Mat"));
    this.mCloudMaterials[1] = new Material(Resources.Load<Material>("Art/Skies/Default_Cloudy_Mat"));
    this.mCloudMaterials[2] = new Material(Resources.Load<Material>("Art/Skies/Default_Overcast_Mat"));
    this.mCloudMaterials[3] = new Material(Resources.Load<Material>("Art/Skies/Default_Storm_Mat"));
    this.LoadLights();
  }

  public void OnLoad()
  {
    this.PrepareForSession();
  }

  private void OnEnable()
  {
    if (EnvironmentManager.GameCameraEffectsHolderChangeEvent == null)
      return;
    EnvironmentManager.GameCameraEffectsHolderChangeEvent(ref this.effectsObject);
  }

  private void OnDisable()
  {
    if (EnvironmentManager.GameCameraEffectsHolderResetEvent == null)
      return;
    EnvironmentManager.GameCameraEffectsHolderResetEvent();
  }

  private void LoadLights()
  {
    Light[] componentsInChildren = this.transform.parent.GetComponentsInChildren<Light>(true);
    int layer = LayerMask.NameToLayer("Env_DynamicLight");
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      Light light = componentsInChildren[index];
      if (light.gameObject.layer == layer)
        this.mTrackLights.Add(light);
    }
    this.mLightQuality = this.GetLightQuality();
  }

  public EnvironmentManager.LightOptions GetLightQuality()
  {
    return EnvironmentManager.LightOptions.On;
  }

  public void SetLightQuality(EnvironmentManager.LightOptions inQuality)
  {
    if (inQuality == this.mLightQuality)
      return;
    this.mLightQuality = inQuality;
    if (inQuality == EnvironmentManager.LightOptions.Off)
    {
      for (int index = 0; index < this.mTrackLights.Count; ++index)
      {
        if (this.mTrackLights[index].enabled)
          this.mTrackLights[index].enabled = false;
      }
    }
    else
      this.mPreviousRampingValue = 0.0f;
  }

  private List<Material> FindMaterialsForShader(string inShaderName)
  {
    List<Material> materialList = new List<Material>();
    if ((Object) this.mCircuitScene == (Object) null)
      return materialList;
    if (this.mSceneMaterialsCache.Count == 0)
    {
      foreach (Renderer componentsInChild in this.mCircuitScene.gameObject.GetComponentsInChildren<Renderer>(true))
      {
        for (int index = 0; index < componentsInChild.sharedMaterials.Length; ++index)
        {
          Material sharedMaterial = componentsInChild.sharedMaterials[index];
          if (!((Object) sharedMaterial == (Object) null))
          {
            if (!this.mSceneMaterialsCache.ContainsKey(sharedMaterial))
              this.mSceneMaterialsCache.Add(sharedMaterial, new List<Renderer>());
            this.mSceneMaterialsCache[sharedMaterial].Add(componentsInChild);
          }
        }
      }
    }
    using (Dictionary<Material, List<Renderer>>.Enumerator enumerator = this.mSceneMaterialsCache.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        KeyValuePair<Material, List<Renderer>> current = enumerator.Current;
        List<Renderer> rendererList = current.Value;
        Material key = current.Key;
        if ((Object) key != (Object) null && (Object) key.shader != (Object) null && key.shader.name == inShaderName)
        {
          Material material1 = new Material(key);
          for (int index1 = 0; index1 < rendererList.Count; ++index1)
          {
            Renderer renderer = rendererList[index1];
            Material[] sharedMaterials = renderer.sharedMaterials;
            for (int index2 = 0; index2 < sharedMaterials.Length; ++index2)
            {
              Material material2 = sharedMaterials[index2];
              if ((Object) key == (Object) material2)
                sharedMaterials[index2] = material1;
            }
            renderer.sharedMaterials = sharedMaterials;
          }
          materialList.Add(material1);
        }
      }
    }
    return materialList;
  }

  public void PrepareForSession()
  {
    if (this.mCircuitScene.trackLayout < (Circuit.TrackLayout) this.mCircuitScene.trackSpecificMaskTextures.Length)
    {
      Debug.Assert((Circuit.TrackLayout) this.mCircuitScene.trackSpecificMaskTextures.Length > this.mCircuitScene.trackLayout, string.Format("Surface water mask variations for track layout {0} are not set.", (object) this.mCircuitScene.trackLayout));
      Texture specificMaskTexture = this.mCircuitScene.trackSpecificMaskTextures[(int) this.mCircuitScene.trackLayout];
      if ((Object) specificMaskTexture != (Object) null)
      {
        for (int index = 0; index < this.mTrackSurfaceShaders.Count; ++index)
          this.mTrackSurfaceShaders[index].SetTexture(MaterialPropertyHashes._TrackMasks, specificMaskTexture);
      }
    }
    this.SetCloudMaterial();
    this.mLigthiningCooldown = (float) RandomUtility.GetRandom(5, 20);
    this.mNormalizedRampingValue = 0.0f;
    RenderSettings.fog = true;
    RenderSettings.fogMode = FogMode.ExponentialSquared;
    RenderSettings.fogColor = this.fogColor;
    SessionWeatherDetails currentSessionWeather = Game.instance.sessionManager.currentSessionWeather;
    if (currentSessionWeather != null)
    {
      this.mTargetNormalisedCloud = currentSessionWeather.GetNormalizedCloud();
      this.mCurrentNormalisedCloud = this.mTargetNormalisedCloud;
    }
    this.PrepareEnviromentVariables();
    this.SetEnviromentVariables();
  }

  public void OnSessionEnd()
  {
    this.SetCloudMaterial();
  }

  private void SetCloudMaterial()
  {
    this.mCloud.CloudMaterial = this.mCloudMaterials[(int) Game.instance.sessionManager.currentSessionWeather.GetCurrentWeather().cloudType];
  }

  private void PrepareEnviromentVariables()
  {
    SessionWeatherDetails currentSessionWeather = Game.instance.sessionManager.currentSessionWeather;
    this.mTargetNormalisedCloud = currentSessionWeather.GetNormalizedCloud();
    this.mSunIntensity.SetNormalizedValue(1f - this.mTargetNormalisedCloud);
    this.mShadowStrenght.SetNormalizedValue(1f - this.mTargetNormalisedCloud);
    this.mWetness.SetNormalizedValue(currentSessionWeather.GetNormalizedTrackWater());
    this.mWaterDisplacement.SetNormalizedValue(1f - currentSessionWeather.GetNormalizedRain());
    this.mRubberness.SetNormalizedValue(currentSessionWeather.GetNormalizedTrackRubber());
    this.mFogValues.SetNormalizedValue(currentSessionWeather.GetNormalizedRain());
    this.mUnityFogValues.SetNormalizedValue(currentSessionWeather.GetNormalizedRain());
    this.mIsLightingAvailable = (double) this.mTargetNormalisedCloud > 0.899999976158142;
  }

  private void UpdateEnviromentVariables()
  {
    SessionWeatherDetails currentSessionWeather = Game.instance.sessionManager.currentSessionWeather;
    this.mTargetNormalisedCloud = currentSessionWeather.GetNormalizedCloud();
    this.mCurrentNormalisedCloud = Mathf.MoveTowards(this.mCurrentNormalisedCloud, this.mTargetNormalisedCloud, 0.0015f);
    this.mSunIntensity.SetNormalizedValue(1f - this.mCurrentNormalisedCloud);
    this.mShadowStrenght.SetNormalizedValue(1f - this.mCurrentNormalisedCloud);
    this.mWetness.SetNormalizedValue(currentSessionWeather.GetNormalizedTrackWater());
    this.mWaterDisplacement.SetNormalizedValue(1f - currentSessionWeather.GetNormalizedRain());
    this.mRubberness.SetNormalizedValue(currentSessionWeather.GetNormalizedTrackRubber());
    this.mFogValues.SetNormalizedValue(currentSessionWeather.GetNormalizedRain());
    this.mUnityFogValues.SetNormalizedValue(currentSessionWeather.GetNormalizedRain());
    this.mIsLightingAvailable = (double) this.mCurrentNormalisedCloud > 0.899999976158142;
  }

  private void SetEnviromentVariables()
  {
    this.mSkyLight.SunIntensity = this.mSunIntensity.value;
    this.mSunLight.shadowStrength = this.mShadowStrenght.value;
    this.mSkyFog.SetFogDensity(this.mFogValues.value);
    RenderSettings.fogDensity = !this.mSetUnityFogSeperatly ? this.mFogValues.value : this.mUnityFogValues.value;
    for (int index = 0; index < this.mTrackSurfaceShaders.Count; ++index)
    {
      Material trackSurfaceShader = this.mTrackSurfaceShaders[index];
      trackSurfaceShader.SetFloat(MaterialPropertyHashes._Wetness, this.mWetness.value);
      trackSurfaceShader.SetFloat(MaterialPropertyHashes._WaterDisplacement, this.mWaterDisplacement.value);
      trackSurfaceShader.SetFloat(MaterialPropertyHashes._Rubberness, this.mRubberness.value);
    }
    for (int index = 0; index < this.mSurfaceWaterShaders.Count; ++index)
      this.mSurfaceWaterShaders[index].SetFloat(MaterialPropertyHashes._Wetness, this.mWetness.value);
  }

  private void ControlStormyWeatherLights()
  {
    SessionWeatherDetails currentSessionWeather = Game.instance.sessionManager.currentSessionWeather;
    if (currentSessionWeather.GetCurrentWeather().cloudType == Weather.CloudType.Overcast)
    {
      this.mEmmissiveLightsOn = true;
      this.mNormalizedRampingValue = Mathf.MoveTowards(this.mNormalizedRampingValue, 0.5f, GameTimer.simulationDeltaTime);
    }
    else if (currentSessionWeather.GetCurrentWeather().cloudType == Weather.CloudType.Stormy)
    {
      this.mEmmissiveLightsOn = true;
      this.mNormalizedRampingValue = Mathf.MoveTowards(this.mNormalizedRampingValue, 1f, GameTimer.simulationDeltaTime);
    }
    else if (this.mEmmissiveLightsOn)
      this.mNormalizedRampingValue = Mathf.MoveTowards(this.mNormalizedRampingValue, 0.0f, GameTimer.simulationDeltaTime);
    if ((double) this.mPreviousRampingValue != (double) this.mNormalizedRampingValue)
    {
      for (int index = 0; index < this.mEmmissiveLightMaterials.Count; ++index)
        this.mEmmissiveLightMaterials[index].SetFloat(MaterialPropertyHashes._Intensity, this.mNormalizedRampingValue);
    }
    this.SetLightsValue();
    if (Mathf.Approximately(this.mNormalizedRampingValue, 0.0f))
      this.mEmmissiveLightsOn = false;
    this.mPreviousRampingValue = this.mNormalizedRampingValue;
  }

  private void SetLightsValue()
  {
    if (this.mLightQuality != EnvironmentManager.LightOptions.On || (double) this.mPreviousRampingValue == (double) this.mNormalizedRampingValue)
      return;
    this.mLightValues.SetNormalizedValue(this.mNormalizedRampingValue);
    bool flag = (double) this.mLightValues.value > 0.0;
    for (int index = 0; index < this.mTrackLights.Count; ++index)
    {
      Light mTrackLight = this.mTrackLights[index];
      mTrackLight.intensity = this.mLightValues.value;
      if (mTrackLight.enabled != flag)
        mTrackLight.enabled = flag;
    }
  }

  public void SimulationUpdate()
  {
    this.UpdateEnviromentVariables();
    this.SetEnviromentVariables();
    this.ControlStormyWeatherLights();
    App.instance.gameCameraManager.SimulationUpdate();
    for (int index = 0; index < this.rainEffectControllers.Length; ++index)
      this.rainEffectControllers[index].SimulationUpdate();
    if (!this.mIsLightingAvailable && this.mLightiningController.state == LightiningController.State.Done)
      return;
    this.mLigthiningCooldown -= GameTimer.simulationDeltaTime;
    if ((double) this.mLigthiningCooldown <= 0.0)
    {
      this.mLightiningController.InitializeLightining();
      this.mLigthiningCooldown = (float) RandomUtility.GetRandom(5, 20);
    }
    this.mLightiningController.UpdateLightining();
  }

  public CameraEffectsScriptableObject GetCameraEffectsScriptableObject()
  {
    return this.effectsObject;
  }

  public class MinMaxFloat
  {
    public float minValue;
    public float maxValue;
    private float mValue;

    public float value
    {
      get
      {
        return this.mValue;
      }
    }

    public MinMaxFloat(float inMin, float inMax, float inValue)
    {
      this.minValue = inMin;
      this.maxValue = inMax;
      this.SetValue(inValue);
    }

    public void SetNormalizedValue(float inNormalizedValue)
    {
      this.mValue = Mathf.Lerp(this.minValue, this.maxValue, inNormalizedValue);
    }

    public void SetValue(float inValue)
    {
      this.mValue = Mathf.Clamp(inValue, this.minValue, this.maxValue);
    }
  }

  public enum LightOptions
  {
    On,
    Off,
  }
}
