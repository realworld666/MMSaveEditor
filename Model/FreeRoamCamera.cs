// Decompiled with JetBrains decompiler
// Type: FreeRoamCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class FreeRoamCamera : MonoBehaviour
{
  [SerializeField]
  private float rotationXSpeedModifier = 0.01f;
  [SerializeField]
  private float rotationYSpeedModifier = 0.01f;
  [SerializeField]
  private float minRotationX = 25f;
  [SerializeField]
  private float maxRotationX = 80f;
  [SerializeField]
  private float keyRotationSpeed = 750f;
  [SerializeField]
  private float maxRotationSpeed = 7.5f;
  [SerializeField]
  private float dragModifier = 1f;
  [SerializeField]
  private float autoRotateAfterSeconds = 3f;
  [SerializeField]
  private float autoRotateAroundSpeedSecond = 10f;
  [SerializeField]
  private FreeRoamCamera.RotationDirection autoRotationDirection = FreeRoamCamera.RotationDirection.Right;
  [SerializeField]
  private float minZoom = 100f;
  [SerializeField]
  private float maxZoom = 220f;
  [SerializeField]
  private float zoomSpeedModifier = 10f;
  private float gamepadZoomSpeedModifier = 8f;
  [SerializeField]
  private float transitionDuration = 1f;
  [SerializeField]
  private float zoomBlendDuration = 0.25f;
  private Vector3 mPreviousPosition = Vector3.zero;
  private Vector3 mPreviousHUDRotation = Vector3.zero;
  public Action<GameObject> OnTargetChange;
  public Action OnLateUpdate;
  private GameCamera mGameCamera;
  private GameObject mTarget;
  private UnityVehicle mVehicleTarget;
  [SerializeField]
  private bool autoRotateAroundTarget;
  [SerializeField]
  private bool startRotateOnEnable;
  [SerializeField]
  private float dragInitialDelay;
  private float mOrbitSpeedX;
  private float mOrbitSpeedY;
  [SerializeField]
  private EasingUtility.Easing autoRotateCurve;
  private float mZoomSpeed;
  private float mZoomSpeedButtons;
  private float mZoom;
  [SerializeField]
  private EasingUtility.Easing zoomCurve;
  [SerializeField]
  private float minTransitionDistanceForZoom;
  [SerializeField]
  private float maxTransitionDistanceForZoom;
  private float mPreviousZoom;
  private float mTransitionZoom;
  private float mCameraRightVectorOffset;
  private FreeRoamCamera.State mState;
  private float mStateTimer;
  private float mAutoRotateTimer;
  private float mDragDelayTimer;
  private bool mDisablePanControls;
  private FreeRoamCamera.RotationAxis mRotationAxis;

  public GameObject target
  {
    get
    {
      return this.mTarget;
    }
  }

  public float zoom
  {
    set
    {
      this.mZoom = Mathf.Lerp(this.minZoom, this.maxZoom, Mathf.Clamp01(value));
    }
  }

  public float zoomNormalized
  {
    get
    {
      return Mathf.Clamp01(1f - Mathf.InverseLerp(this.minZoom, this.maxZoom, this.mZoom));
    }
  }

  public void SetGameCamera(GameCamera inGameCamera)
  {
    this.mGameCamera = inGameCamera;
  }

  private void OnEnable()
  {
    IT_Gesture.onDraggingE += new IT_Gesture.DraggingHandler(this.OnDragging);
    IT_Gesture.onPinchE += new IT_Gesture.PinchHandler(this.OnPinch);
    this.mOrbitSpeedX = 0.0f;
    this.mOrbitSpeedY = 0.0f;
    this.mDragDelayTimer = 0.0f;
    if ((UnityEngine.Object) this.transform.parent != (UnityEngine.Object) null)
      this.transform.parent.position = Vector3.zero;
    if ((UnityEngine.Object) this.mVehicleTarget != (UnityEngine.Object) null && App.instance.gameStateManager.currentState.IsSimulation())
    {
      if (Game.instance.sessionManager.sessionType == SessionDetails.SessionType.Race)
      {
        if (!Game.instance.sessionManager.isSessionActive)
          this.transform.parent.rotation = Quaternion.Euler(45f, this.mVehicleTarget.transform.rotation.eulerAngles.y - 120f, 0.0f);
        else
          this.transform.parent.rotation = Quaternion.Euler(this.mPreviousHUDRotation);
      }
      else if (MathsUtility.ApproximatelyZero(Game.instance.sessionManager.GetNormalizedSessionTime()))
        this.transform.parent.rotation = Quaternion.Euler(50f, this.mVehicleTarget.transform.rotation.eulerAngles.y + 160f, 0.0f);
      else
        this.transform.parent.rotation = Quaternion.Euler(this.mPreviousHUDRotation);
    }
    else
      this.transform.parent.rotation = Quaternion.Euler(38f, 74f, 0.0f);
    this.mZoom = App.instance.gameStateManager == null || App.instance.gameStateManager.currentState == null || !App.instance.gameStateManager.currentState.IsSimulation() ? Mathf.Lerp(this.minZoom, this.maxZoom, 0.5f) : this.minZoom;
    if ((UnityEngine.Object) this.mGameCamera != (UnityEngine.Object) null)
    {
      this.mGameCamera.SetBlurActive(false);
      this.mGameCamera.SetTiltShiftActive(true);
      this.mGameCamera.GetCamera().fieldOfView = 40f;
    }
    if (!this.startRotateOnEnable)
      return;
    this.SetState(FreeRoamCamera.State.AutoRotating);
  }

  private void OnDisable()
  {
    IT_Gesture.onDraggingE -= new IT_Gesture.DraggingHandler(this.OnDragging);
    IT_Gesture.onPinchE -= new IT_Gesture.PinchHandler(this.OnPinch);
    this.CheckRotationReset();
    this.mPreviousHUDRotation = this.transform.parent.rotation.eulerAngles;
    this.mCameraRightVectorOffset = 0.0f;
  }

  public void SetTarget(GameObject inTarget, CameraManager.Transition inTransition, float inCameraRightVectorOffset = 0.0f)
  {
    this.mCameraRightVectorOffset = inCameraRightVectorOffset;
    if (!((UnityEngine.Object) this.mTarget != (UnityEngine.Object) inTarget))
      return;
    this.mTarget = inTarget;
    if ((UnityEngine.Object) this.mTarget != (UnityEngine.Object) null)
      this.mVehicleTarget = this.mTarget.GetComponent<UnityVehicle>();
    this.OnTargetSet(inTransition);
    if (this.OnTargetChange == null)
      return;
    this.OnTargetChange(inTarget);
  }

  public void SetDisablePanControls(bool inValue)
  {
    this.mDisablePanControls = inValue;
  }

  private void SetState(FreeRoamCamera.State inState)
  {
    this.mState = inState;
    this.mStateTimer = 0.0f;
    if (this.mState != FreeRoamCamera.State.TransitionToTarget)
      return;
    this.mPreviousPosition = this.transform.parent.position;
    this.mPreviousZoom = this.mZoom;
    float magnitude = (this.mPreviousPosition - this.target.gameObject.transform.position).magnitude;
    if ((double) magnitude > (double) this.minTransitionDistanceForZoom)
      this.mTransitionZoom = Mathf.Lerp(this.mZoom, this.maxZoom, Mathf.Clamp01(magnitude / (this.maxTransitionDistanceForZoom - this.minTransitionDistanceForZoom)));
    else
      this.mTransitionZoom = this.mZoom;
  }

  public void LateUpdate()
  {
    if ((UnityEngine.Object) this.target == (UnityEngine.Object) null && (this.mState == FreeRoamCamera.State.FollowingTarget || this.mState == FreeRoamCamera.State.TransitionToTarget))
      this.SetTarget((GameObject) null, CameraManager.Transition.Instant, 0.0f);
    this.mStateTimer += GameTimer.deltaTime;
    this.mDragDelayTimer += GameTimer.deltaTime;
    this.ApplyRotation();
    switch (this.mState)
    {
      case FreeRoamCamera.State.FreeRoam:
        this.UpdateFreeRoam();
        break;
      case FreeRoamCamera.State.FollowingTarget:
        this.UpdateFollowingTarget();
        break;
      case FreeRoamCamera.State.AutoRotating:
        this.UpdateAutoRotate();
        this.UpdateFollowingTarget();
        break;
      case FreeRoamCamera.State.TransitionAutoRotating:
        this.UpdateTransitionAutoRotate();
        this.UpdateFollowingTarget();
        break;
      case FreeRoamCamera.State.TransitionToTarget:
        this.UpdateTransitionToTarget();
        break;
    }
    if (this.mState != FreeRoamCamera.State.TransitionToTarget)
      this.mZoom += this.mZoomSpeed;
    this.mZoom = Mathf.Clamp(this.mZoom, this.minZoom, this.maxZoom);
    this.transform.localPosition = new Vector3(0.0f, 0.0f, -this.mZoom);
    this.mOrbitSpeedX *= (float) (1.0 - (double) GameTimer.deltaTime * 12.0);
    this.mOrbitSpeedY *= (float) (1.0 - (double) GameTimer.deltaTime * 3.0);
    this.mZoomSpeed *= (float) (1.0 - (double) GameTimer.deltaTime * 4.0);
    if (!this.mDisablePanControls && this.mState != FreeRoamCamera.State.TransitionToTarget && (!Game.IsActive() || Game.IsActive() && !Game.instance.time.IsPauseTypeActive(GameTimer.PauseType.App)))
    {
      if (App.instance.preferencesManager.GetSettingBool(Preference.pName.Game_Gamepad, false))
      {
        this.mZoomSpeed += (GamePad.GetLeftTrigger(PlayerIndex.One) - GamePad.GetRightTrigger(PlayerIndex.One)) * this.gamepadZoomSpeedModifier * GameTimer.deltaTime;
      }
      else
      {
        this.mZoomSpeedButtons = !InputManager.instance.GetKeyAny(KeyBinding.Name.ZoomIn) ? 0.0f : 0.1f;
        this.mZoomSpeedButtons += !InputManager.instance.GetKeyAny(KeyBinding.Name.ZoomOut) ? 0.0f : -0.1f;
        this.mZoomSpeed += (Input.GetAxis("Mouse ScrollWheel") + this.mZoomSpeedButtons) * this.zoomSpeedModifier * GameTimer.deltaTime;
      }
    }
    if (this.autoRotateAroundTarget && this.mState == FreeRoamCamera.State.FollowingTarget)
    {
      this.mAutoRotateTimer += GameTimer.deltaTime;
      if ((double) this.mAutoRotateTimer >= (double) this.autoRotateAfterSeconds)
      {
        this.mAutoRotateTimer = 0.0f;
        this.SetState(FreeRoamCamera.State.TransitionAutoRotating);
      }
    }
    if (this.OnLateUpdate == null)
      return;
    this.OnLateUpdate();
  }

  private void ApplyRotation()
  {
    float x = this.transform.parent.rotation.eulerAngles.x;
    float y = this.transform.parent.rotation.eulerAngles.y;
    if ((double) x > 180.0)
      x -= 360f;
    if (!this.mDisablePanControls && InputManager.instance.GetKey(KeyBinding.Name.RotateLeft))
    {
      this.mOrbitSpeedY += this.keyRotationSpeed * this.rotationYSpeedModifier * GameTimer.deltaTime;
      this.CheckRotationReset();
    }
    if (!this.mDisablePanControls && InputManager.instance.GetKey(KeyBinding.Name.RotateRight))
    {
      this.mOrbitSpeedY -= this.keyRotationSpeed * this.rotationYSpeedModifier * GameTimer.deltaTime;
      this.CheckRotationReset();
    }
    this.mOrbitSpeedX = Mathf.Clamp(this.mOrbitSpeedX, -this.maxRotationSpeed, this.maxRotationSpeed);
    this.mOrbitSpeedY = Mathf.Clamp(this.mOrbitSpeedY, -this.maxRotationSpeed, this.maxRotationSpeed);
    Quaternion quaternion1 = Quaternion.Euler(0.0f, y, 0.0f);
    Quaternion quaternion2 = Quaternion.Euler(x, 0.0f, 0.0f);
    switch (this.mRotationAxis)
    {
      case FreeRoamCamera.RotationAxis.All:
        quaternion2 = Quaternion.Euler(Mathf.Clamp(x + this.mOrbitSpeedX, this.minRotationX, this.maxRotationX), 0.0f, 0.0f);
        quaternion1 = Quaternion.Euler(0.0f, y + this.mOrbitSpeedY, 0.0f);
        break;
      case FreeRoamCamera.RotationAxis.X:
        quaternion2 = Quaternion.Euler(Mathf.Clamp(x + this.mOrbitSpeedX, this.minRotationX, this.maxRotationX), 0.0f, 0.0f);
        break;
      case FreeRoamCamera.RotationAxis.Y:
        quaternion1 = Quaternion.Euler(0.0f, y + this.mOrbitSpeedY, 0.0f);
        break;
    }
    this.transform.parent.rotation = quaternion1 * quaternion2;
  }

  private void CheckRotationReset()
  {
    if (!this.autoRotateAroundTarget || this.mState != FreeRoamCamera.State.TransitionAutoRotating && this.mState != FreeRoamCamera.State.AutoRotating)
      return;
    this.mAutoRotateTimer = 0.0f;
    this.SetState(FreeRoamCamera.State.FollowingTarget);
  }

  private void UpdateFreeRoam()
  {
  }

  public void UpdateFollowingTarget()
  {
    if (!((UnityEngine.Object) this.target != (UnityEngine.Object) null))
      return;
    this.transform.parent.position = this.target.transform.position + this.transform.parent.right * this.mCameraRightVectorOffset;
  }

  private void UpdateTransitionToTarget()
  {
    float num = this.transitionDuration - this.zoomBlendDuration;
    if ((double) this.mStateTimer < (double) this.zoomBlendDuration)
      this.mZoom = EasingUtility.EaseByType(this.zoomCurve, this.mPreviousZoom, this.mTransitionZoom, Mathf.Clamp01(this.mStateTimer / this.zoomBlendDuration));
    else if ((double) this.mStateTimer > (double) num)
      this.mZoom = EasingUtility.EaseByType(this.zoomCurve, this.mTransitionZoom, this.mPreviousZoom, Mathf.Clamp01((this.mStateTimer - num) / this.zoomBlendDuration));
    if ((UnityEngine.Object) this.target != (UnityEngine.Object) null)
      this.transform.parent.position = EasingUtility.EaseByType(this.zoomCurve, this.mPreviousPosition, this.target.gameObject.transform.position, Mathf.Clamp01(this.mStateTimer / this.transitionDuration));
    if ((double) this.mStateTimer <= (double) this.transitionDuration)
      return;
    this.SetState(FreeRoamCamera.State.FollowingTarget);
  }

  private void UpdateTransitionAutoRotate()
  {
    float x = this.transform.parent.rotation.eulerAngles.x;
    float y = this.transform.parent.rotation.eulerAngles.y;
    this.mOrbitSpeedY = EasingUtility.EaseByType(this.autoRotateCurve, 0.0f, this.autoRotateAroundSpeedSecond * this.GetAutoRotateDirection() * GameTimer.deltaTime, Mathf.Clamp01(this.mStateTimer / this.transitionDuration));
    Quaternion quaternion = Quaternion.Euler(x, 0.0f, 0.0f);
    this.transform.parent.rotation = Quaternion.Euler(0.0f, y + this.mOrbitSpeedY, 0.0f) * quaternion;
    if ((double) this.mStateTimer < (double) this.transitionDuration)
      return;
    this.SetState(FreeRoamCamera.State.AutoRotating);
  }

  private void UpdateAutoRotate()
  {
    float x = this.transform.parent.rotation.eulerAngles.x;
    float y = this.transform.parent.rotation.eulerAngles.y;
    float max = this.autoRotateAroundSpeedSecond * GameTimer.deltaTime;
    this.mOrbitSpeedY += this.autoRotateAroundSpeedSecond * this.GetAutoRotateDirection() * GameTimer.deltaTime;
    this.mOrbitSpeedY = Mathf.Clamp(this.mOrbitSpeedY, -max, max);
    Quaternion quaternion = Quaternion.Euler(x, 0.0f, 0.0f);
    this.transform.parent.rotation = Quaternion.Euler(0.0f, y + this.mOrbitSpeedY, 0.0f) * quaternion;
  }

  private void OnDragging(DragInfo dragInfo)
  {
    if (!Input.GetMouseButton(0) || (double) this.mDragDelayTimer < (double) this.dragInitialDelay || this.IsCursorOverUI())
      return;
    this.mOrbitSpeedX = (float) -((double) dragInfo.delta.y * (double) this.rotationXSpeedModifier * (double) GameTimer.deltaTime * (double) Screen.height) * this.dragModifier;
    this.mOrbitSpeedY = dragInfo.delta.x * this.rotationYSpeedModifier * GameTimer.deltaTime * (float) Screen.width * this.dragModifier;
    this.CheckRotationReset();
  }

  private void OnPinch(PinchInfo pinfo)
  {
    if (this.IsCursorOverUI() || this.mState == FreeRoamCamera.State.TransitionToTarget)
      return;
    this.mZoomSpeed -= pinfo.magnitude * this.zoomSpeedModifier * GameTimer.deltaTime;
  }

  private void OnTargetSet(CameraManager.Transition inTransition)
  {
    if ((UnityEngine.Object) this.target != (UnityEngine.Object) null)
    {
      if (inTransition == CameraManager.Transition.Smooth)
        this.SetState(FreeRoamCamera.State.TransitionToTarget);
      else
        this.SetState(FreeRoamCamera.State.FollowingTarget);
    }
    else
      this.SetState(FreeRoamCamera.State.FreeRoam);
  }

  private float GetAutoRotateDirection()
  {
    switch (this.autoRotationDirection)
    {
      case FreeRoamCamera.RotationDirection.Left:
        return 1f;
      case FreeRoamCamera.RotationDirection.Right:
        return -1f;
      default:
        return -1f;
    }
  }

  private bool IsCursorOverUI()
  {
    return UIManager.instance.UIObjectsAtMousePosition.Count > 0;
  }

  private enum AccelerationAxis
  {
    All,
    X,
    Y,
    None,
  }

  private enum RotationAxis
  {
    All,
    X,
    Y,
    None,
  }

  private enum RotationDirection
  {
    Left,
    Right,
  }

  private enum State
  {
    FreeRoam,
    FollowingTarget,
    AutoRotating,
    TransitionAutoRotating,
    TransitionToTarget,
  }
}
