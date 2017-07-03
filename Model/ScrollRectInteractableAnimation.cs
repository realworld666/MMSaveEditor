// Decompiled with JetBrains decompiler
// Type: ScrollRectInteractableAnimation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollRectInteractableAnimation : MonoBehaviour
{
  public float animationDuration = 25f;
  public float startDelay = 5f;
  public ScrollRect scrollRect;
  public bool checkForCursor;
  private ScrollRectInteractableAnimation.State mState;
  private float mStateTimer;

  private void Start()
  {
  }

  private void OnEnable()
  {
    this.SetState(ScrollRectInteractableAnimation.State.StartDelay);
  }

  private void Update()
  {
    this.mStateTimer += GameTimer.deltaTime;
    switch (this.mState)
    {
      case ScrollRectInteractableAnimation.State.StartDelay:
        if ((double) this.mStateTimer < (double) this.startDelay)
          break;
        this.SetState(ScrollRectInteractableAnimation.State.Animating);
        break;
      case ScrollRectInteractableAnimation.State.Animating:
        if (this.IsUserInteractingWithScrollRect())
        {
          this.SetState(ScrollRectInteractableAnimation.State.WaitAfterUserInput);
          break;
        }
        this.scrollRect.verticalNormalizedPosition = this.scrollRect.verticalNormalizedPosition - 1f / this.animationDuration * GameTimer.deltaTime;
        break;
      case ScrollRectInteractableAnimation.State.WaitAfterUserInput:
        if (this.IsUserInteractingWithScrollRect())
          break;
        this.SetState(ScrollRectInteractableAnimation.State.StartDelay);
        break;
    }
  }

  private bool IsUserInteractingWithScrollRect()
  {
    bool flag = false;
    if (this.checkForCursor)
      return UIManager.instance.IsObjectAtMousePosition(this.gameObject);
    if (Input.GetMouseButton(0))
    {
      List<RaycastResult> objectsAtMousePosition = UIManager.instance.UIObjectsAtMousePosition;
      for (int index = 0; index < objectsAtMousePosition.Count && !flag; ++index)
      {
        if (GameUtility.IsChildInHierarchy(objectsAtMousePosition[index].gameObject.transform, this.scrollRect.content.transform))
          flag = true;
      }
    }
    return flag;
  }

  private void SetState(ScrollRectInteractableAnimation.State inState)
  {
    this.mState = inState;
    this.mStateTimer = 0.0f;
  }

  public enum State
  {
    StartDelay,
    Animating,
    WaitAfterUserInput,
  }
}
