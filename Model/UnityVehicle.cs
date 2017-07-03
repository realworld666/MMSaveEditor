// Decompiled with JetBrains decompiler
// Type: UnityVehicle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnityVehicle : MonoBehaviour
{
  [SerializeField]
  private float lightMaxValue = 1.25f;
  private List<Material> mHeadLightMaterials = new List<Material>();
  private List<Material> mFlashingLightMaterials = new List<Material>();
  private float mHeadLightIntensity = -1f;
  private Vector3 mRotationTarget = new Vector3();
  private Color mWaterSprayColor = Color.white;
  [SerializeField]
  private ParticleSystem waterSpray;
  [SerializeField]
  private ParticleSystem sparks;
  [SerializeField]
  private ParticleSystem[] tyreLockupSmoke;
  [SerializeField]
  private ParticleSystem partFailureSmoke;
  [SerializeField]
  private ParticleSystem partMinorFailureSmoke;
  [SerializeField]
  private ParticleSystem dirtEffect;
  [SerializeField]
  private ParticleSystem grassEffect;
  [SerializeField]
  private ParticleSystem sandEffect;
  [SerializeField]
  private BoxCollider boxCollider;
  public AudioCarWidget audioCar;
  [Header("Light Settings")]
  [SerializeField]
  private Light[] redLight;
  [SerializeField]
  private Light[] frontLight;
  private Championship.Series mSeries;
  private EnvironmentManager.MinMaxFloat mLightRange;
  private float mLightIntensity;
  private float mLightTimer;
  private float mHeadLightChangeCooldown;
  private float mHeadLightChangeTimer;
  private UnityVehicle.RotationTarget mRotationTargetType;
  private Vehicle mVehicle;
  public static UnityVehicle.VehicleMouseEnter OnVehicleMouseEnter;

  public Vehicle vehicle
  {
    get
    {
      return this.mVehicle;
    }
  }

  private void Awake()
  {
    this.mLightRange = new EnvironmentManager.MinMaxFloat(0.0f, this.lightMaxValue, 0.0f);
    this.mHeadLightChangeCooldown = RandomUtility.GetRandom01();
    this.mHeadLightChangeTimer = this.mHeadLightChangeCooldown;
    this.DisableAllEffects();
  }

  public void DisableAllEffects()
  {
    GameUtility.EnableEmmission(this.waterSpray, false);
    if ((Object) this.sparks != (Object) null)
      GameUtility.EnableEmmission(this.sparks, false);
    GameUtility.EnableEmmission(this.partFailureSmoke, false);
    GameUtility.EnableEmmission(this.partMinorFailureSmoke, false);
    this.DisableAllCrashParticleEffects();
  }

  public void DisableAllCrashParticleEffects()
  {
    GameUtility.EnableEmmission(this.grassEffect, false);
    GameUtility.EnableEmmission(this.sandEffect, false);
    GameUtility.EnableEmmission(this.dirtEffect, false);
    for (int index = 0; index < this.tyreLockupSmoke.Length; ++index)
      GameUtility.EnableEmmission(this.tyreLockupSmoke[index], false);
  }

  public void ActivateCrashParticleEffect(PathData.GateParticleType inType, bool inValue = true)
  {
    switch (inType)
    {
      case PathData.GateParticleType.Dust:
        GameUtility.EnableEmmission(this.dirtEffect, inValue);
        break;
      case PathData.GateParticleType.Grass:
        GameUtility.EnableEmmission(this.grassEffect, inValue);
        break;
      case PathData.GateParticleType.Sand:
        GameUtility.EnableEmmission(this.sandEffect, inValue);
        break;
      case PathData.GateParticleType.TyreWhiteSmoke:
        if (inValue)
        {
          if ((double) RandomUtility.GetRandom01() < 0.75)
          {
            if (this.vehicle.pathController.IsInCorner() && this.vehicle.pathController.GetCurrentCorner().direction == PathData.Corner.Direction.Left)
            {
              GameUtility.EnableEmmission(this.tyreLockupSmoke[0], true);
              GameUtility.EnableEmmission(this.tyreLockupSmoke[1], false);
              break;
            }
            GameUtility.EnableEmmission(this.tyreLockupSmoke[0], false);
            GameUtility.EnableEmmission(this.tyreLockupSmoke[1], true);
            break;
          }
          for (int index = 0; index < this.tyreLockupSmoke.Length; ++index)
            GameUtility.EnableEmmission(this.tyreLockupSmoke[index], true);
          break;
        }
        for (int index = 0; index < this.tyreLockupSmoke.Length; ++index)
          GameUtility.EnableEmmission(this.tyreLockupSmoke[index], false);
        break;
      case PathData.GateParticleType.BlackSmoke:
        GameUtility.EnableEmmission(this.partMinorFailureSmoke, inValue);
        break;
      case PathData.GateParticleType.BlackSmokeIntense:
        GameUtility.EnableEmmission(this.partFailureSmoke, inValue);
        break;
    }
  }

  private void GetLightMaterials()
  {
    foreach (Renderer componentsInChild in this.GetComponentsInChildren<Renderer>())
    {
      for (int index = 0; index < componentsInChild.materials.Length; ++index)
      {
        Material material = componentsInChild.materials[index];
        if ((Object) material != (Object) null && (Object) material.shader != (Object) null)
        {
          switch (this.mSeries)
          {
            case Championship.Series.SingleSeaterSeries:
              if (material.shader.name == "Car/Car_FlashingLight")
              {
                this.mFlashingLightMaterials.Add(material);
                continue;
              }
              continue;
            case Championship.Series.GTSeries:
              if (material.shader.name == "Custom/Standard_LightVolume" && material.name.Contains("Brake"))
              {
                this.mFlashingLightMaterials.Add(material);
                continue;
              }
              if (material.shader.name == "Custom/Standard_LightVolume" && !material.name.Contains("Brake"))
              {
                this.mHeadLightMaterials.Add(material);
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
    }
  }

  private void ControlRedLight()
  {
    if (this.redLight == null || (Object) Game.instance.sessionManager.circuit == (Object) null)
      return;
    bool flag = this.vehicle.IsLightOn();
    bool enviromentLightsOn = Game.instance.sessionManager.circuit.environmentManager.enviromentLightsOn;
    switch (this.mSeries)
    {
      case Championship.Series.SingleSeaterSeries:
        if (!Mathf.Approximately(this.mLightTimer, 0.0f) || (enviromentLightsOn || flag))
        {
          this.mLightTimer += GameTimer.deltaTime * 2f;
          if ((double) this.mLightTimer * 2.0 <= 1.0)
            this.mLightIntensity = EasingUtility.OutCubic(0.0f, 1f, this.mLightTimer * 2f);
          else if ((double) this.mLightTimer <= 1.0)
          {
            this.mLightIntensity = EasingUtility.OutCubic(1f, 0.0f, (float) (((double) this.mLightTimer - 0.5) * 2.0));
          }
          else
          {
            this.mLightTimer = 0.0f;
            this.mLightIntensity = 0.0f;
          }
          this.mLightRange.SetNormalizedValue(this.mLightIntensity);
          break;
        }
        break;
      case Championship.Series.GTSeries:
        this.mLightTimer += GameTimer.deltaTime;
        if (flag)
        {
          this.mLightIntensity = Mathf.Approximately(this.mLightIntensity, 1f) ? 1f : Mathf.Clamp01(EasingUtility.OutCubic(!enviromentLightsOn ? 0.0f : 0.25f, 1f, this.mLightTimer * 2f));
        }
        else
        {
          this.mLightIntensity = !enviromentLightsOn ? 0.0f : 0.25f;
          this.mLightTimer = 0.0f;
        }
        this.mLightRange.SetNormalizedValue(this.mLightIntensity);
        break;
    }
    for (int index = 0; index < this.mFlashingLightMaterials.Count; ++index)
      this.mFlashingLightMaterials[index].SetFloat(MaterialPropertyHashes._Intensity, this.mLightIntensity);
    for (int index = 0; index < this.redLight.Length; ++index)
      this.redLight[index].intensity = this.mLightRange.value;
  }

  private void ControlHeadLight()
  {
    if (this.frontLight == null || this.mSeries == Championship.Series.SingleSeaterSeries || (Object) Game.instance.sessionManager.circuit == (Object) null)
      return;
    float num = !Game.instance.sessionManager.circuit.environmentManager.enviromentLightsOn ? 0.0f : 1f;
    if ((double) this.mHeadLightIntensity == (double) num)
      return;
    this.mHeadLightChangeTimer += GameTimer.totalSimulationDeltaTimeCurrentFrame;
    if ((double) this.mHeadLightChangeTimer <= (double) this.mHeadLightChangeCooldown)
      return;
    this.mHeadLightIntensity = num;
    this.mHeadLightChangeTimer = 0.0f;
    for (int index = 0; index < this.mHeadLightMaterials.Count; ++index)
      this.mHeadLightMaterials[index].SetFloat(MaterialPropertyHashes._Intensity, this.mHeadLightIntensity);
    for (int index = 0; index < this.frontLight.Length; ++index)
      this.frontLight[index].intensity = this.mHeadLightIntensity;
  }

  public void OnStart(Vehicle vehicle)
  {
    this.mVehicle = vehicle;
    if (this.mVehicle is RacingVehicle)
      this.mSeries = Game.instance.sessionManager.championship.series;
    this.GetLightMaterials();
    this.UpdateColor();
    this.mWaterSprayColor = this.waterSpray.startColor;
    if (!((Object) this.audioCar != (Object) null))
      return;
    this.audioCar.OnStart(this.mVehicle);
  }

  private void UpdateColor()
  {
    if (!(this.mVehicle is RacingVehicle))
      return;
    RacingVehicle mVehicle = (RacingVehicle) this.mVehicle;
    Team team = mVehicle.driver.contract.GetTeam();
    TeamColor.LiveryColour livery1 = team.GetTeamColor().livery;
    if (mVehicle.driver.contract.GetTeam() != null)
    {
      Renderer[] componentsInChildren = this.GetComponentsInChildren<Renderer>(true);
      for (int index1 = 0; index1 < componentsInChildren.Length; ++index1)
      {
        for (int index2 = 0; index2 < componentsInChildren[index1].sharedMaterials.Length; ++index2)
        {
          Material sharedMaterial = componentsInChildren[index1].sharedMaterials[index2];
          if (!((Object) sharedMaterial == (Object) null) && sharedMaterial.name.Contains("Car_Body"))
          {
            if (sharedMaterial.shader.name.Contains("Custom/RaceCar"))
            {
              sharedMaterial.SetColor(MaterialPropertyHashes._PrimaryColor, mVehicle.driver.contract.GetTeam().GetTeamColor().livery.primary);
              sharedMaterial.SetColor(MaterialPropertyHashes._SecondaryColour, mVehicle.driver.contract.GetTeam().GetTeamColor().livery.secondary);
              sharedMaterial.SetColor(MaterialPropertyHashes._TertiaryColour, mVehicle.driver.contract.GetTeam().GetTeamColor().livery.tertiary);
            }
            else
            {
              sharedMaterial.SetColor(MaterialPropertyHashes._PrimaryColor, livery1.primary);
              sharedMaterial.SetColor(MaterialPropertyHashes._SecondaryColor, livery1.secondary);
              sharedMaterial.SetColor(MaterialPropertyHashes._TertiaryColor, livery1.tertiary);
              sharedMaterial.SetColor(MaterialPropertyHashes._TrimColor, livery1.trim);
              if ((Object) this.boxCollider != (Object) null)
              {
                Vector4 vector1 = new Vector4();
                Vector4 vector2 = new Vector4();
                vector1.x = (float) -((double) this.boxCollider.size.x / 2.0);
                vector1.y = this.boxCollider.center.y;
                vector1.z = (float) -((double) this.boxCollider.size.z / 2.0) + this.boxCollider.center.z;
                vector1.w = 0.0f;
                vector2.x = this.boxCollider.size.x;
                vector2.y = this.boxCollider.size.y + this.boxCollider.center.y;
                vector2.z = this.boxCollider.size.z + this.boxCollider.center.z;
                vector2.w = 0.0f;
                sharedMaterial.SetVector("_BoundingBoxMin", vector1);
                sharedMaterial.SetVector("_BoundingBoxSize", vector2);
              }
              LiveryData livery2 = Game.instance.liveryManager.GetLivery(team.liveryID);
              Texture baseTexture = (Texture) null;
              Texture detailTexture = (Texture) null;
              livery2.chassis.GetTexture(out baseTexture, out detailTexture);
              sharedMaterial.SetFloat("Base Livery Projection", (float) livery2.chassis.baseProjection);
              sharedMaterial.SetFloat("Detail Livery Projection", (float) livery2.chassis.detailProjection);
              sharedMaterial.SetTexture(MaterialPropertyHashes._BaseLivery, baseTexture);
              sharedMaterial.SetTexture(MaterialPropertyHashes._DetailLivery, detailTexture);
            }
          }
        }
      }
    }
    if (!((Object) this.boxCollider != (Object) null))
      return;
    Object.Destroy((Object) this.boxCollider);
    this.boxCollider = (BoxCollider) null;
  }

  public void SetPartConditionEffects(CarPartCondition.PartState inState)
  {
    if (!(this.mVehicle is RacingVehicle))
      return;
    switch (inState)
    {
      case CarPartCondition.PartState.Optimal:
        GameUtility.EnableEmmission(this.partFailureSmoke, false);
        GameUtility.EnableEmmission(this.partMinorFailureSmoke, false);
        break;
      case CarPartCondition.PartState.Failure:
        GameUtility.EnableEmmission(this.partFailureSmoke, false);
        GameUtility.EnableEmmission(this.partMinorFailureSmoke, true);
        break;
      case CarPartCondition.PartState.CatastrophicFailure:
        GameUtility.EnableEmmission(this.partFailureSmoke, true);
        GameUtility.EnableEmmission(this.partMinorFailureSmoke, false);
        break;
    }
  }

  private void OnDrawGizmosSelected()
  {
  }

  private void OnDestroy()
  {
    this.waterSpray = (ParticleSystem) null;
    this.sparks = (ParticleSystem) null;
    this.tyreLockupSmoke = (ParticleSystem[]) null;
    this.partFailureSmoke = (ParticleSystem) null;
    this.partMinorFailureSmoke = (ParticleSystem) null;
    this.dirtEffect = (ParticleSystem) null;
    this.grassEffect = (ParticleSystem) null;
    this.sandEffect = (ParticleSystem) null;
    this.mVehicle = (Vehicle) null;
  }

  private void Update()
  {
    CircuitScene circuit = Game.instance.sessionManager.circuit;
    Vector3 forward = this.mVehicle.transform.forward;
    if ((bool) ((Object) circuit) && circuit.isPathCollisionMeshActive && this.IsRendered())
    {
      bool flag = false;
      float num = VehicleConstants.vehicleLength * 0.5f;
      Vector3 vector3_1 = this.mVehicle.transform.position + this.mVehicle.transform.forward * num;
      RaycastHit hitInfo;
      if (Physics.Raycast(vector3_1 + Vector3.up * 2f, Vector3.down, out hitInfo, float.PositiveInfinity, circuit.pathCollisionLayerMask))
        vector3_1 = hitInfo.point;
      else
        flag = true;
      Vector3 vector3_2 = this.mVehicle.transform.position + this.mVehicle.transform.forward * -num;
      if (Physics.Raycast(vector3_2 + Vector3.up * 2f, Vector3.down, out hitInfo, float.PositiveInfinity, circuit.pathCollisionLayerMask))
        vector3_2 = hitInfo.point;
      else
        flag = true;
      if (!flag)
      {
        this.transform.position = vector3_2 + (vector3_1 - vector3_2) * 0.5f;
        forward = (vector3_1 - vector3_2).normalized;
      }
      else
        this.transform.position = this.mVehicle.transform.position;
    }
    else
      this.transform.position = this.mVehicle.transform.position;
    switch (this.mRotationTargetType)
    {
      case UnityVehicle.RotationTarget.ForwardVector:
        this.transform.rotation = Quaternion.LookRotation(forward);
        break;
      case UnityVehicle.RotationTarget.Target:
        this.transform.rotation = Quaternion.LookRotation(this.mRotationTarget);
        break;
    }
    this.DebugDrawCollisionBounds();
    this.UpdateWeatherParticles();
    this.ControlRedLight();
    this.ControlHeadLight();
  }

  public void SetRotationTargetType(UnityVehicle.RotationTarget inTargetType)
  {
    this.mRotationTargetType = inTargetType;
  }

  public void SetRotationTarget(Vector3 inTarget)
  {
    this.mRotationTarget = inTarget;
  }

  private void DebugDrawCollisionBounds()
  {
  }

  public bool IsRendered()
  {
    GameCamera firstActiveCamera = App.instance.gameCameraManager.GetFirstActiveCamera();
    if ((Object) firstActiveCamera != (Object) null)
    {
      Vector3 viewportPoint = firstActiveCamera.GetCamera().WorldToViewportPoint(this.transform.position);
      if ((double) viewportPoint.z > 0.0 && (double) viewportPoint.x > 0.0 && ((double) viewportPoint.x < 1.0 && (double) viewportPoint.y > 0.0) && (double) viewportPoint.y < 1.0)
        return true;
    }
    return false;
  }

  public void ProduceSparks()
  {
    if (!((Object) this.sparks != (Object) null))
      return;
    GameUtility.EnableEmmission(this.sparks, true);
    this.sparks.Play();
  }

  private void UpdateWeatherParticles()
  {
    if (Game.instance.sessionManager.eventDetails == null)
      return;
    SessionWeatherDetails currentSessionWeather = Game.instance.sessionManager.currentSessionWeather;
    ParticleSystem.EmissionModule emission = this.waterSpray.emission;
    if (currentSessionWeather != null)
    {
      if (!true || (double) currentSessionWeather.GetNormalizedTrackWater() <= (double) DesignDataManager.instance.tyreData.minLightTreadSurfaceWaterRange)
      {
        emission.enabled = false;
      }
      else
      {
        emission.enabled = true;
        this.mWaterSprayColor.a = (double) currentSessionWeather.GetNormalizedTrackWater() >= (double) DesignDataManager.instance.tyreData.maxLightTreadSurfaceWaterRange ? Mathf.Clamp01(this.mVehicle.speed / GameUtility.MilesPerHourToMetersPerSecond(180f)) : Mathf.Clamp01(this.mVehicle.speed / GameUtility.MilesPerHourToMetersPerSecond(180f)) * 0.5f;
        this.waterSpray.startColor = this.mWaterSprayColor;
      }
    }
    else
      emission.enabled = false;
  }

  private void OnMouseDown()
  {
    if (EventSystem.current.IsPointerOverGameObject())
      return;
    App.instance.cameraManager.SetTarget(this.mVehicle, CameraManager.Transition.Smooth);
  }

  private void OnMouseExit()
  {
  }

  public enum RotationTarget
  {
    ForwardVector,
    Target,
  }

  public delegate void VehicleMouseEnter(Vehicle inVehicle);
}
