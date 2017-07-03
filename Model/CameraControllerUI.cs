// Decompiled with JetBrains decompiler
// Type: CameraControllerUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraControllerUI : MonoBehaviour
{
  [SerializeField]
  private float transitionDuration = 1f;
  [SerializeField]
  private float rotateAroundSpeed = 10f;
  [SerializeField]
  private CameraControllerUI.RotationDirection rotationDirection = CameraControllerUI.RotationDirection.Right;
  private Vector3 mStartPosition = Vector3.zero;
  private Vector3 mTargetPosition = Vector3.zero;
  private Quaternion mStartRotation = Quaternion.identity;
  private bool mIgnoreRotate = true;
  [SerializeField]
  private CameraControllerUI.Data[] cameraData;
  [SerializeField]
  private EasingUtility.Easing curve;
  [SerializeField]
  private bool animateFieldOfView;
  [SerializeField]
  private bool animateRotateAround;
  [SerializeField]
  private Transform[] camerasIgnoreRotation;
  private Transform[] mCameras;
  private DepthOfField[] mCameraDOF;
  private Camera mCamera;
  private Camera mTargetCamera;
  private DepthOfField mDepthOfField;
  private bool mAnimating;
  private float mStartFieldOfView;
  private float mStartFocalDistance;
  private float mStartAperture;
  private int mTargetCameraID;
  private float mTimer;

  public Camera cameraComp
  {
    get
    {
      return this.mCamera;
    }
  }

  public int cameraTargetID
  {
    get
    {
      return this.mTargetCameraID;
    }
  }

  private void Start()
  {
    this.mCamera = this.GetComponent<Camera>();
    this.mDepthOfField = this.GetComponent<DepthOfField>();
  }

  public void Setup(Championship inChampionship)
  {
    for (int index = 0; index < this.cameraData.Length; ++index)
    {
      CameraControllerUI.Data data = this.cameraData[index];
      if (data.series == inChampionship.series)
      {
        this.mCameras = data.cameras;
        this.mCameraDOF = data.cameraDOF;
      }
    }
  }

  private void Update()
  {
    if (this.mAnimating)
    {
      this.mTimer += GameTimer.deltaTime;
      float t = EasingUtility.EaseByType(this.curve, 0.0f, 1f, Mathf.Clamp01(this.mTimer / this.transitionDuration));
      this.transform.localPosition = Vector3.Lerp(this.mStartPosition, this.mCameras[this.mTargetCameraID].localPosition, t);
      this.transform.localRotation = Quaternion.Lerp(this.mStartRotation, this.mCameras[this.mTargetCameraID].localRotation, t);
      this.mDepthOfField.focalLength = Mathf.Lerp(this.mStartFocalDistance, this.mCameraDOF[this.mTargetCameraID].focalLength, t);
      this.mDepthOfField.aperture = Mathf.Lerp(this.mStartAperture, this.mCameraDOF[this.mTargetCameraID].aperture, t);
      if (this.animateFieldOfView && (UnityEngine.Object) this.mTargetCamera != (UnityEngine.Object) null)
        this.mCamera.fieldOfView = Mathf.Lerp(this.mStartFieldOfView, this.mTargetCamera.fieldOfView, t);
      if ((double) t >= 1.0 && this.animateRotateAround)
      {
        this.mAnimating = false;
        this.mTargetPosition = this.CalculateTarget();
      }
    }
    if (this.mAnimating || !this.animateRotateAround || (this.mIgnoreRotate || !(this.mTargetPosition != Vector3.zero)))
      return;
    this.transform.LookAt(this.mTargetPosition);
    this.transform.RotateAround(this.mTargetPosition, this.GetRotationAxis(), GameTimer.deltaTime * this.rotateAroundSpeed);
  }

  public void SetStartCamera(int inID)
  {
    this.mTargetCameraID = inID;
    this.transform.localPosition = this.mCameras[this.mTargetCameraID].localPosition;
    this.transform.localRotation = this.mCameras[this.mTargetCameraID].localRotation;
    this.mStartPosition = this.transform.localPosition;
    this.mStartRotation = this.transform.localRotation;
    if ((UnityEngine.Object) this.mDepthOfField == (UnityEngine.Object) null)
      this.mDepthOfField = this.GetComponent<DepthOfField>();
    if ((UnityEngine.Object) this.mCamera == (UnityEngine.Object) null)
      this.mCamera = this.GetComponent<Camera>();
    this.mTargetCamera = this.mCameras[this.mTargetCameraID].GetComponent<Camera>();
    this.mStartFocalDistance = this.mDepthOfField.focalLength;
    this.mStartAperture = this.mDepthOfField.aperture;
    this.mStartFieldOfView = this.mCamera.fieldOfView;
    this.mTimer = this.transitionDuration;
    this.mAnimating = true;
    if (!this.animateRotateAround)
      return;
    this.mIgnoreRotate = this.CheckIgnore(this.mCameras[this.mTargetCameraID]);
  }

  public void SetCamera(int inID)
  {
    this.mTargetCameraID = inID;
    this.mStartPosition = this.transform.localPosition;
    this.mStartRotation = this.transform.localRotation;
    if ((UnityEngine.Object) this.mDepthOfField == (UnityEngine.Object) null)
      this.mDepthOfField = this.GetComponent<DepthOfField>();
    if ((UnityEngine.Object) this.mCamera == (UnityEngine.Object) null)
      this.mCamera = this.GetComponent<Camera>();
    this.mTargetCamera = this.mCameras[this.mTargetCameraID].GetComponent<Camera>();
    this.mStartFocalDistance = this.mDepthOfField.focalLength;
    this.mStartAperture = this.mDepthOfField.aperture;
    this.mStartFieldOfView = this.mCamera.fieldOfView;
    this.mTimer = 0.0f;
    this.mAnimating = true;
    if (!this.animateRotateAround)
      return;
    this.mIgnoreRotate = this.CheckIgnore(this.mCameras[this.mTargetCameraID]);
  }

  private Vector3 CalculateTarget()
  {
    Ray ray = new Ray(this.mCamera.transform.position, this.mCamera.transform.forward);
    Plane plane = new Plane(Vector3.up, Vector3.zero);
    float enter = 0.0f;
    if (plane.Raycast(ray, out enter))
      return ray.GetPoint(enter);
    return Vector3.zero;
  }

  private bool CheckIgnore(Transform inCameraTransform)
  {
    for (int index = 0; index < this.camerasIgnoreRotation.Length; ++index)
    {
      if ((UnityEngine.Object) this.camerasIgnoreRotation[index] == (UnityEngine.Object) inCameraTransform)
        return true;
    }
    return false;
  }

  private Vector3 GetRotationAxis()
  {
    switch (this.rotationDirection)
    {
      case CameraControllerUI.RotationDirection.Left:
        return Vector3.up;
      case CameraControllerUI.RotationDirection.Right:
        return Vector3.down;
      default:
        return Vector3.zero;
    }
  }

  [Serializable]
  public class Data
  {
    public Championship.Series series;
    public Transform[] cameras;
    public DepthOfField[] cameraDOF;
  }

  public enum RotationDirection
  {
    Left,
    Right,
  }
}
