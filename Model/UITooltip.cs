// Decompiled with JetBrains decompiler
// Type: UITooltip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UITooltip : MonoBehaviour
{
  public CursorManager.State cursorToUse = CursorManager.State.Tooltip;
  private bool mIsEnabled = true;
  public RectTransform target;
  public RectTransform[] otherTargets;
  public Animator animator;
  public bool useMouseObjectStack;
  private bool mAnimating;

  public void OnStart()
  {
    this.ApplyTriggers();
    this.gameObject.SetActive(false);
    if (!((Object) this.animator == (Object) null))
      return;
    Debug.LogErrorFormat("UIToolTip without animator: {0}", (object) this.name);
  }

  private void Update()
  {
    if (!this.mAnimating)
      return;
    this.CheckAnimation();
  }

  public void ApplyTriggers()
  {
    if ((Object) this.target != (Object) null)
      this.target.gameObject.AddComponent<UITooltipTrigger>().Setup(this);
    for (int index = 0; index < this.otherTargets.Length; ++index)
      this.otherTargets[index].gameObject.AddComponent<UITooltipTrigger>().Setup(this);
  }

  private void CheckAnimation()
  {
    if (!this.IsAnimationComplete(AnimationHashes.Hide))
      return;
    this.mAnimating = false;
    this.gameObject.SetActive(false);
  }

  private bool IsAnimationComplete(int inAnimationName)
  {
    AnimatorStateInfo animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
    return animatorStateInfo.shortNameHash == inAnimationName && (double) animatorStateInfo.normalizedTime >= 1.0;
  }

  public void SetTooltipEnabled(bool inIsEnabled)
  {
    this.mIsEnabled = inIsEnabled;
  }

  public void Show()
  {
    if (!this.mIsEnabled)
      return;
    if (!this.gameObject.activeSelf)
      this.gameObject.SetActive(true);
    else
      this.animator.Play(AnimationHashes.Show, 0, 0.0f);
  }

  public void Hide()
  {
    if (!this.gameObject.activeSelf)
      return;
    if (this.IsAnimationComplete(AnimationHashes.Show))
    {
      this.animator.Play(AnimationHashes.Hide, 0, 0.0f);
      this.mAnimating = true;
    }
    else
      this.gameObject.SetActive(false);
  }
}
