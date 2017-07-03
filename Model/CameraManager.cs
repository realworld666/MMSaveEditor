// Decompiled with JetBrains decompiler
// Type: CameraManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class CameraManager
{
  private Camera[] mCameras = new Camera[10];
  private CameraManager.CameraMode mActiveMode = CameraManager.CameraMode.None;
  public Action OnTargetChange;
  private Transform mCameraContainer;
  private GameCamera mGameCamera;

  public CameraManager.CameraMode activeMode
  {
    get
    {
      return this.mActiveMode;
    }
  }

  public GameObject target
  {
    get
    {
      return this.mGameCamera.freeRoamCamera.target;
    }
  }

  public GameCamera gameCamera
  {
    get
    {
      return this.mGameCamera;
    }
  }

  public void Start()
  {
    if (!((UnityEngine.Object) this.mCameraContainer == (UnityEngine.Object) null))
      return;
    GameObject go = new GameObject("Camera");
    this.mCameraContainer = go.transform;
    UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(go, FirstActiveSceneHolder.firstActiveScene);
    GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Cameras/GameCamera"));
    this.mGameCamera = gameObject.GetComponent<GameCamera>();
    gameObject.transform.SetParent(this.mCameraContainer, false);
    this.Deactivate();
    Game.OnGameDataChanged -= new Action(this.ResetCameraMode);
    Game.OnGameDataChanged += new Action(this.ResetCameraMode);
  }

  private void ResetCameraMode()
  {
    this.mActiveMode = CameraManager.CameraMode.None;
  }

  public void RegisterCameraForMode(CameraManager.CameraMode inMode, Camera inCamera)
  {
    int index1 = (int) inMode;
    this.mCameras[index1] = inCamera;
    if ((UnityEngine.Object) this.mCameras[index1] != (UnityEngine.Object) null)
      this.mCameras[index1].enabled = false;
    Animator[] componentsInChildren = inCamera.GetComponentsInChildren<Animator>(true);
    if (componentsInChildren == null)
      return;
    for (int index2 = 0; index2 < componentsInChildren.Length; ++index2)
      componentsInChildren[index2].updateMode = AnimatorUpdateMode.UnscaledTime;
  }

  public void ActivateMode(CameraManager.CameraMode inMode)
  {
    this.mGameCamera.GetCamera().enabled = App.instance.preferencesManager.GetSettingBool(Preference.pName.Video_3D, true);
    if (inMode == this.mActiveMode)
      return;
    this.mActiveMode = inMode;
    int mActiveMode = (int) this.mActiveMode;
    if (inMode == CameraManager.CameraMode.FreeRoam)
    {
      this.mGameCamera.animatedCamera.enabled = false;
      this.mGameCamera.trackGuideCamera.enabled = false;
      this.mGameCamera.freeRoamCamera.enabled = true;
    }
    else if (inMode == CameraManager.CameraMode.TrackGuide)
    {
      this.mGameCamera.freeRoamCamera.enabled = false;
      this.mGameCamera.animatedCamera.enabled = false;
      this.mGameCamera.trackGuideCamera.enabled = true;
    }
    else
    {
      this.mGameCamera.freeRoamCamera.enabled = false;
      this.mGameCamera.animatedCamera.enabled = true;
      this.mGameCamera.trackGuideCamera.enabled = false;
      this.mGameCamera.animatedCamera.SetTargetCamera(this.mCameras[mActiveMode]);
    }
  }

  public void Deactivate()
  {
    if (!((UnityEngine.Object) this.mGameCamera != (UnityEngine.Object) null))
      return;
    if ((UnityEngine.Object) this.mGameCamera.GetCamera() != (UnityEngine.Object) null)
      this.mGameCamera.GetCamera().enabled = false;
    if ((UnityEngine.Object) this.mGameCamera.freeRoamCamera != (UnityEngine.Object) null)
      this.mGameCamera.freeRoamCamera.enabled = false;
    if ((UnityEngine.Object) this.mGameCamera.animatedCamera != (UnityEngine.Object) null)
      this.mGameCamera.animatedCamera.enabled = false;
    if (!((UnityEngine.Object) this.mGameCamera.trackGuideCamera != (UnityEngine.Object) null))
      return;
    this.mGameCamera.trackGuideCamera.enabled = false;
  }

  public void SetTarget(GameObject inTarget, CameraManager.Transition inTransition)
  {
    this.mGameCamera.freeRoamCamera.SetTarget(inTarget, inTransition, 0.0f);
    if (this.OnTargetChange == null)
      return;
    this.OnTargetChange();
  }

  public void SetTarget(Vehicle inVehicle, CameraManager.Transition inTransition)
  {
    GameUtility.Assert(inVehicle != null, "CameraManager.SetTarget passed null Vehicle", (UnityEngine.Object) null);
    GameUtility.Assert((UnityEngine.Object) inVehicle.unityTransform != (UnityEngine.Object) null, "CameraManager.SetTarget passed Vehicle with null unityTransform", (UnityEngine.Object) null);
    GameUtility.Assert((UnityEngine.Object) this.mGameCamera != (UnityEngine.Object) null, "CameraManager.SetTarget called whilst mGameCamera is null", (UnityEngine.Object) null);
    GameUtility.Assert((UnityEngine.Object) this.mGameCamera.freeRoamCamera != (UnityEngine.Object) null, "CameraManager.SetTarget called whilst mGameCamera.freeRoamCamera is null", (UnityEngine.Object) null);
    this.mGameCamera.freeRoamCamera.SetTarget(inVehicle.unityTransform.gameObject, inTransition, 0.0f);
    if ((UnityEngine.Object) Simulation2D.instance != (UnityEngine.Object) null)
      Simulation2D.instance.camera2D.SetTarget(inVehicle, inTransition);
    if (this.OnTargetChange == null)
      return;
    this.OnTargetChange();
  }

  public void SetRunningMode(bool inLeavingScreen = false)
  {
    if (this.mActiveMode != CameraManager.CameraMode.FreeRoam)
      return;
    this.mGameCamera.GetCamera().enabled = inLeavingScreen || !App.instance.preferencesManager.videoPreferences.isSimulationRunning2DMode;
  }

  public Camera GetCamera()
  {
    return this.mGameCamera.GetCamera();
  }

  public enum CameraMode
  {
    FreeRoam,
    PracticeIntro,
    QualifyingIntro,
    RaceIntro,
    PracticeHUB,
    QualifyingHUB,
    RaceHUB,
    PostSession,
    SimulatingSession,
    TrackGuide,
    Count,
    None,
  }

  public enum Transition
  {
    Instant,
    Smooth,
  }
}
