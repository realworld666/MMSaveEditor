// Decompiled with JetBrains decompiler
// Type: AnimatedCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class AnimatedCamera : MonoBehaviour
{
  private GameCamera mGameCamera;
  private Transform mTargetTransform;
  private Camera mTargetCamera;
  private bool mDetailsSet;
  private float mNearClipPlane;
  private float mFarClipPlane;

  public Transform targetTransform
  {
    get
    {
      return this.mTargetTransform;
    }
  }

  public Camera targetCamera
  {
    get
    {
      return this.mTargetCamera;
    }
  }

  public void SetGameCamera(GameCamera inGameCamera)
  {
    this.mGameCamera = inGameCamera;
  }

  private void OnEnable()
  {
    if (!((Object) this.mGameCamera != (Object) null))
      return;
    this.mGameCamera.SetTiltShiftActive(true);
  }

  private void OnDisable()
  {
    if (!((Object) this.mGameCamera != (Object) null) || !this.mDetailsSet)
      return;
    this.mGameCamera.GetCamera().nearClipPlane = this.mNearClipPlane;
    this.mGameCamera.GetCamera().farClipPlane = this.mFarClipPlane;
    this.mDetailsSet = false;
  }

  public void SetTargetCamera(Camera inCamera)
  {
    if (!((Object) inCamera != (Object) null))
      return;
    this.mTargetCamera = inCamera;
    this.mTargetTransform = (Transform) null;
    this.transform.localPosition = Vector3.zero;
    if (!((Object) this.mTargetCamera != (Object) null))
      return;
    if ((Object) this.mGameCamera != (Object) null && !this.mDetailsSet)
    {
      this.mDetailsSet = true;
      this.mNearClipPlane = this.mGameCamera.GetCamera().nearClipPlane;
      this.mFarClipPlane = this.mGameCamera.GetCamera().farClipPlane;
      this.mGameCamera.GetCamera().fieldOfView = this.mTargetCamera.fieldOfView;
      this.mGameCamera.GetCamera().nearClipPlane = this.mTargetCamera.nearClipPlane;
      this.mGameCamera.GetCamera().farClipPlane = this.mTargetCamera.farClipPlane;
    }
    this.mTargetTransform = this.mTargetCamera.GetComponent<Transform>();
    Animator component1 = this.mTargetCamera.GetComponent<Animator>();
    if ((Object) component1 != (Object) null)
    {
      component1.Play(0, 0, 0.0f);
    }
    else
    {
      UnityEngine.Animation component2 = this.mTargetCamera.GetComponent<UnityEngine.Animation>();
      if ((Object) component2 != (Object) null)
      {
        component2.Rewind();
        component2.Play();
      }
    }
    this.mTargetCamera.gameObject.SetActive(true);
    this.mTargetCamera.gameObject.GetComponent<AnimatedCamera>().enabled = false;
    this.Update();
  }

  private void Update()
  {
    if (!((Object) this.mTargetTransform != (Object) null) || !((Object) this.mTargetCamera != (Object) null))
      return;
    this.mGameCamera.transform.position = this.mTargetTransform.position;
    this.mGameCamera.transform.rotation = this.mTargetTransform.rotation;
    this.mGameCamera.GetCamera().fieldOfView = this.mTargetCamera.fieldOfView;
    this.mGameCamera.GetCamera().nearClipPlane = this.mTargetCamera.nearClipPlane;
    this.mGameCamera.GetCamera().farClipPlane = this.mTargetCamera.farClipPlane;
  }
}
