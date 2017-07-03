// Decompiled with JetBrains decompiler
// Type: UIBlur
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class UIBlur : MonoBehaviour
{
  [SerializeField]
  private float transitionDuration = 0.75f;
  [SerializeField]
  private EasingUtility.Easing transitionCurve = EasingUtility.Easing.InOutSin;
  private List<Canvas> mUnblurredCanvases = new List<Canvas>();
  private List<GameObject> mLastBlurRequestingObjects = new List<GameObject>();
  [SerializeField]
  private Camera nonBlurCamera;
  [SerializeField]
  private CanvasGroup canvasGroup;
  private Canvas mCanvas;
  private UIBlur.State mState;
  private float mStateTimer;

  public bool isActive
  {
    get
    {
      if (this.mState != UIBlur.State.Active)
        return this.mState == UIBlur.State.TransitionIn;
      return true;
    }
  }

  private void Awake()
  {
    this.mCanvas = this.GetComponentInParent<Canvas>();
    if ((Object) this.mCanvas == (Object) null)
      this.mCanvas = this.GetComponent<Canvas>();
    GameUtility.SetActive(this.gameObject, false);
  }

  private void Update()
  {
    this.mStateTimer += Mathf.Clamp(GameTimer.deltaTime, 0.0f, 0.06666667f);
    switch (this.mState)
    {
      case UIBlur.State.InActive:
        this.canvasGroup.alpha = 0.0f;
        break;
      case UIBlur.State.TransitionIn:
        this.canvasGroup.alpha = EasingUtility.EaseByType(this.transitionCurve, 0.0f, 1f, Mathf.Clamp01(this.mStateTimer / this.transitionDuration));
        if ((double) this.mStateTimer < (double) this.transitionDuration)
          break;
        this.SetState(UIBlur.State.Active);
        break;
      case UIBlur.State.Active:
        this.canvasGroup.alpha = 1f;
        break;
      case UIBlur.State.TransitionOut:
        this.canvasGroup.alpha = EasingUtility.EaseByType(this.transitionCurve, 1f, 0.0f, Mathf.Clamp01(this.mStateTimer / this.transitionDuration));
        if ((double) this.mStateTimer < (double) this.transitionDuration)
          break;
        this.SetState(UIBlur.State.InActive);
        break;
    }
  }

  private void SetState(UIBlur.State inState)
  {
    this.mStateTimer = 0.0f;
    this.mState = inState;
    switch (this.mState)
    {
      case UIBlur.State.InActive:
        this.canvasGroup.alpha = 0.0f;
        for (int index = 0; index < this.mUnblurredCanvases.Count; ++index)
        {
          if ((Object) this.mUnblurredCanvases[index] != (Object) null)
            UIManager.instance.SetupCanvasCamera(this.mUnblurredCanvases[index]);
        }
        UIManager.instance.SetupCanvasCamera(this.mCanvas);
        GameUtility.SetActive(this.gameObject, false);
        this.mUnblurredCanvases.Clear();
        break;
      case UIBlur.State.TransitionIn:
        GameUtility.SetActive(this.gameObject, true);
        this.mCanvas.worldCamera = this.nonBlurCamera;
        this.mStateTimer = this.transitionDuration * this.canvasGroup.alpha;
        break;
      case UIBlur.State.Active:
        this.canvasGroup.alpha = 1f;
        break;
      case UIBlur.State.TransitionOut:
        this.mStateTimer = this.transitionDuration * Mathf.Clamp01(1f - this.canvasGroup.alpha);
        break;
    }
  }

  public void Show(GameObject inRequestingObject, params Canvas[] inUnblurredCanvases)
  {
    if (!this.mLastBlurRequestingObjects.Contains(inRequestingObject))
      this.mLastBlurRequestingObjects.Add(inRequestingObject);
    if ((Object) this.mCanvas == (Object) null)
      this.mCanvas = this.GetComponentInParent<Canvas>();
    for (int index = 0; index < this.mUnblurredCanvases.Count; ++index)
      UIManager.instance.SetupCanvasCamera(this.mUnblurredCanvases[index]);
    this.mUnblurredCanvases.Clear();
    this.mUnblurredCanvases.AddRange((IEnumerable<Canvas>) inUnblurredCanvases);
    for (int index = 0; index < this.mUnblurredCanvases.Count; ++index)
      this.mUnblurredCanvases[index].worldCamera = this.nonBlurCamera;
    this.SetState(UIBlur.State.TransitionIn);
  }

  public void Hide(GameObject inRequestingObject)
  {
    if (!this.mLastBlurRequestingObjects.Remove(inRequestingObject) || this.mLastBlurRequestingObjects.Count != 0)
      return;
    this.SetState(UIBlur.State.TransitionOut);
  }

  public void ForceHide()
  {
    this.SetState(UIBlur.State.TransitionOut);
    this.mLastBlurRequestingObjects.Clear();
  }

  public enum State
  {
    InActive,
    TransitionIn,
    Active,
    TransitionOut,
  }
}
