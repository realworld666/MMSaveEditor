// Decompiled with JetBrains decompiler
// Type: Camera2D
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class Camera2D : MonoBehaviour
{
  private Vector3 mTargetPosition = Vector3.zero;
  [SerializeField]
  private float minZoom = 100f;
  [SerializeField]
  private float maxZoom = 220f;
  [SerializeField]
  private float zoomSpeedModifier = 10f;
  [SerializeField]
  private float rotationSpeedModifier = 10f;
  [SerializeField]
  private float rotationDragModifier = 1f;
  [SerializeField]
  private float transitionDuration = 1f;
  [SerializeField]
  private float zoomBlendDuration = 0.25f;
  private Vector3 mPreviousPosition = Vector3.zero;
  public Action<Vehicle> OnTargetChange;
  public Action OnLateUpdate;
  public Camera orthographicCamera;
  private Vehicle mTarget;
  private Simulation2D mSimulation2D;
  [SerializeField]
  private float scrollWheelScalar;
  private float mZoomSpeed;
  private float mZoomSpeedButtons;
  private float mZoom;
  private float mRotationSpeed;
  private float mRotation;
  [SerializeField]
  private EasingUtility.Easing zoomCurve;
  [SerializeField]
  private float minTransitionDistanceForZoom;
  [SerializeField]
  private float maxTransitionDistanceForZoom;
  private float mPreviousZoom;
  private float mTransitionZoom;
  private Camera2D.State mState;
  private float mStateTimer;

  public Vehicle target
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

  private void OnEnable()
  {
    IT_Gesture.onDraggingE += new IT_Gesture.DraggingHandler(this.OnDragging);
    this.mZoom = Mathf.Lerp(this.minZoom, this.maxZoom, 0.5f);
    if (this.target == null)
      return;
    this.transform.position = this.mTargetPosition;
  }

  private void OnDisable()
  {
    IT_Gesture.onDraggingE -= new IT_Gesture.DraggingHandler(this.OnDragging);
  }

  public void SetSimulation2D(Simulation2D inSimulation2D)
  {
    this.mSimulation2D = inSimulation2D;
  }

  public void SetTarget(Vehicle inTarget, CameraManager.Transition inTransition)
  {
    if (this.mTarget == inTarget)
      return;
    this.mTarget = inTarget;
    this.OnTargetSet(inTransition);
    if (this.OnTargetChange == null)
      return;
    this.OnTargetChange(this.mTarget);
  }

  private void SetState(Camera2D.State inState)
  {
    this.mState = inState;
    this.mStateTimer = 0.0f;
    if (this.mState != Camera2D.State.TransitionToTarget)
      return;
    this.mPreviousPosition = this.transform.position;
    this.mPreviousZoom = this.mZoom;
    this.mTargetPosition = this.GetTargetPosition();
    float magnitude = (this.mPreviousPosition - this.mTargetPosition).magnitude;
    if ((double) magnitude > (double) this.minTransitionDistanceForZoom)
      this.mTransitionZoom = Mathf.Lerp(this.mZoom, this.maxZoom, Mathf.Clamp01(magnitude / (this.maxTransitionDistanceForZoom - this.minTransitionDistanceForZoom)));
    else
      this.mTransitionZoom = this.mZoom;
  }

  public void LateUpdate()
  {
    if (this.target == null && (this.mState == Camera2D.State.FollowingTarget || this.mState == Camera2D.State.TransitionToTarget))
      this.SetTarget((Vehicle) null, CameraManager.Transition.Instant);
    this.mTargetPosition = this.GetTargetPosition();
    this.mStateTimer += GameTimer.deltaTime;
    switch (this.mState)
    {
      case Camera2D.State.FollowingTarget:
        this.UpdateFollowingTarget();
        break;
      case Camera2D.State.TransitionToTarget:
        this.UpdateTransitionToTarget();
        break;
    }
    if (this.mState != Camera2D.State.TransitionToTarget)
    {
      this.mZoom += this.mZoomSpeed;
      this.mRotation += this.mRotationSpeed;
    }
    this.mZoom = Mathf.Clamp(this.mZoom, this.minZoom, this.maxZoom);
    this.orthographicCamera.orthographicSize = this.mZoom;
    this.mZoomSpeed *= (float) (1.0 - (double) GameTimer.deltaTime * 4.0);
    if (this.mState != Camera2D.State.TransitionToTarget && Game.IsActive() && !Game.instance.time.IsPauseTypeActive(GameTimer.PauseType.App))
    {
      this.mZoomSpeedButtons = !InputManager.instance.GetKeyAny(KeyBinding.Name.ZoomIn) ? 0.0f : 0.1f;
      this.mZoomSpeedButtons += !InputManager.instance.GetKeyAny(KeyBinding.Name.ZoomOut) ? 0.0f : -0.1f;
      this.mZoomSpeed -= (Input.GetAxis("Mouse ScrollWheel") * this.scrollWheelScalar + this.mZoomSpeedButtons) * this.zoomSpeedModifier * GameTimer.deltaTime;
    }
    this.orthographicCamera.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, this.mRotation);
    this.mRotationSpeed *= (float) (1.0 - (double) GameTimer.deltaTime * 4.0);
    if (this.mState != Camera2D.State.TransitionToTarget && Game.IsActive() && !Game.instance.time.IsPauseTypeActive(GameTimer.PauseType.App))
      this.mRotationSpeed += (float) ((!InputManager.instance.GetKey(KeyBinding.Name.RotateLeft) ? 0.0 : 0.100000001490116) + (!InputManager.instance.GetKey(KeyBinding.Name.RotateRight) ? 0.0 : -0.100000001490116)) * this.rotationSpeedModifier * GameTimer.deltaTime;
    this.transform.position += Vector3.back * 10f;
    if (this.OnLateUpdate == null)
      return;
    this.OnLateUpdate();
  }

  public void UpdateFollowingTarget()
  {
    if (this.target == null)
      return;
    this.transform.position = this.mTargetPosition;
  }

  private void UpdateTransitionToTarget()
  {
    float num = this.transitionDuration - this.zoomBlendDuration;
    if ((double) this.mStateTimer < (double) this.zoomBlendDuration)
      this.mZoom = EasingUtility.EaseByType(this.zoomCurve, this.mPreviousZoom, this.mTransitionZoom, Mathf.Clamp01(this.mStateTimer / this.zoomBlendDuration));
    else if ((double) this.mStateTimer > (double) num)
      this.mZoom = EasingUtility.EaseByType(this.zoomCurve, this.mTransitionZoom, this.mPreviousZoom, Mathf.Clamp01((this.mStateTimer - num) / this.zoomBlendDuration));
    this.transform.position = EasingUtility.EaseByType(this.zoomCurve, this.mPreviousPosition, this.mTargetPosition, Mathf.Clamp01(this.mStateTimer / this.transitionDuration));
    if ((double) this.mStateTimer <= (double) this.transitionDuration)
      return;
    this.SetState(Camera2D.State.FollowingTarget);
  }

  private void OnDragging(DragInfo dragInfo)
  {
    if (this.IsCursorOverUI())
      return;
    this.mRotationSpeed -= dragInfo.delta.x * this.rotationDragModifier * (float) Screen.width * GameTimer.deltaTime * GameTimer.deltaTime;
  }

  private void OnTargetSet(CameraManager.Transition inTransition)
  {
    if (inTransition == CameraManager.Transition.Smooth)
      this.SetState(Camera2D.State.TransitionToTarget);
    else
      this.SetState(Camera2D.State.FollowingTarget);
  }

  private Vector3 GetTargetPosition()
  {
    return this.mSimulation2D.miniMapWidget.GetWorldPositionOfEntry(this.mTarget);
  }

  private bool IsCursorOverUI()
  {
    return UIManager.instance.UIObjectsAtMousePosition.Count > 0;
  }

  private enum State
  {
    FollowingTarget,
    TransitionToTarget,
  }
}
