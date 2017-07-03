// Decompiled with JetBrains decompiler
// Type: GameCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class GameCamera : MonoBehaviour
{
  private Camera mCamera;
  private AnimatedCamera mAnimatedCamera;
  private FreeRoamCamera mFreeRoamCamera;
  private TrackGuideCamera mTrackGuideCamera;
  private TiltShift mTiltShift;
  private BlurOptimized mBlur;
  private DepthOfField mDepthOfField;
  private VignetteAndChromaticAberration mVignette;
  private RainEffectController mRainEffectController;
  private bool mTiltShiftActive;

  public AnimatedCamera animatedCamera
  {
    get
    {
      return this.mAnimatedCamera;
    }
  }

  public FreeRoamCamera freeRoamCamera
  {
    get
    {
      return this.mFreeRoamCamera;
    }
  }

  public TrackGuideCamera trackGuideCamera
  {
    get
    {
      return this.mTrackGuideCamera;
    }
  }

  public DepthOfField depthOfField
  {
    get
    {
      return this.mDepthOfField;
    }
  }

  public BlurOptimized blur
  {
    get
    {
      return this.mBlur;
    }
  }

  private void Awake()
  {
    this.mCamera = this.GetComponentInChildren<Camera>();
    this.mAnimatedCamera = this.GetComponentInChildren<AnimatedCamera>();
    this.mFreeRoamCamera = this.GetComponentInChildren<FreeRoamCamera>();
    this.mTrackGuideCamera = this.GetComponentInChildren<TrackGuideCamera>();
    this.mTiltShift = this.GetComponentInChildren<TiltShift>();
    this.mBlur = this.GetComponentInChildren<BlurOptimized>();
    this.mDepthOfField = this.GetComponentInChildren<DepthOfField>();
    this.mVignette = this.GetComponentInChildren<VignetteAndChromaticAberration>();
    this.mRainEffectController = this.GetComponentInChildren<RainEffectController>();
    this.mAnimatedCamera.SetGameCamera(this);
    this.mFreeRoamCamera.SetGameCamera(this);
    this.mTrackGuideCamera.SetGameCamera(this);
    this.mAnimatedCamera.enabled = false;
    this.mFreeRoamCamera.enabled = false;
    this.mTrackGuideCamera.enabled = false;
  }

  private void Start()
  {
    this.SetTiltShiftActive(false);
    this.SetBlurActive(false);
  }

  private void OnEnable()
  {
    App.instance.gameCameraManager.RegisterCamera(this);
  }

  private void OnDisable()
  {
    App.instance.gameCameraManager.UnregisterCamera(this);
  }

  public void SimulationUpdate()
  {
    if (!((Object) this.mRainEffectController != (Object) null))
      return;
    this.mRainEffectController.SimulationUpdate();
  }

  public void SetTiltShiftActive(bool inIsActive)
  {
    if (!((Object) this.mTiltShift != (Object) null))
      return;
    bool settingBool = App.instance.preferencesManager.GetSettingBool(Preference.pName.Video_TiltShift, false);
    if (inIsActive && !settingBool)
      inIsActive = false;
    this.mTiltShiftActive = inIsActive;
    this.mTiltShift.enabled = inIsActive;
  }

  public void SetBlurActive(bool inIsActive)
  {
    if (!((Object) this.mBlur != (Object) null))
      return;
    this.mBlur.enabled = inIsActive;
  }

  public void SetDepthOfField(bool inIsActive)
  {
    if (!((Object) this.mDepthOfField != (Object) null))
      return;
    this.mDepthOfField.enabled = inIsActive;
  }

  public void SetPostProcessingEffect(Preference.pName inName, bool inValue)
  {
    switch (inName)
    {
      case Preference.pName.Video_TiltShift:
        if (!((Object) this.mTiltShift != (Object) null) || !this.mTiltShiftActive)
          break;
        this.mTiltShift.enabled = inValue;
        break;
    }
  }

  public void SetVignette(bool inIsActive)
  {
    if (!((Object) this.mVignette != (Object) null))
      return;
    this.mVignette.enabled = inIsActive;
  }

  public Camera GetCamera()
  {
    return this.mCamera;
  }
}
