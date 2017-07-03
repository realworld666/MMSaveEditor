// Decompiled with JetBrains decompiler
// Type: UISessionInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class UISessionInfo : MonoBehaviour
{
  private static float borderSafe = 10f;
  private Vector2 mPreviewAnchorPosition = Vector2.zero;
  private Vector2 mDefaultAnchorPosition = Vector2.zero;
  private Vector2 mTargetAnchorPosition = Vector2.zero;
  private Rect mCheckInfoRect = new Rect();
  private Rect mTargetRect = new Rect();
  private List<UISessionInfo> mSessionInfos = new List<UISessionInfo>();
  private static float checkInterval;
  public RectTransform container;
  public RectTransform animationRect;
  public RectTransform lineRect;
  public UIDriverInfoHUD widget;
  private float mInterval;
  private float mDefaultLineHeight;
  private float mDefaultLineAdjustment;

  public Vector2 targetAnchorPosition
  {
    get
    {
      return this.mTargetAnchorPosition;
    }
  }

  private void Awake()
  {
    this.mDefaultAnchorPosition = this.animationRect.anchoredPosition;
    this.mDefaultLineHeight = this.lineRect.rect.height;
    this.mDefaultLineAdjustment = Mathf.Max(this.mDefaultLineHeight - this.mDefaultAnchorPosition.y, 0.0f);
    UISessionInfo.checkInterval = this.animationRect.anchoredPosition.y / 5f;
    this.mTargetAnchorPosition = this.mDefaultAnchorPosition;
  }

  public void AvoidAnyOverlapping()
  {
    if (!this.gameObject.activeSelf)
      return;
    this.mSessionInfos.Clear();
    for (int index = 0; index < this.widget.sessionInfos.Length; ++index)
    {
      UISessionInfo sessionInfo = this.widget.sessionInfos[index];
      if (!((Object) sessionInfo == (Object) this))
      {
        if (sessionInfo.gameObject.activeSelf)
          this.mSessionInfos.Add(sessionInfo);
      }
      else
        break;
    }
    if (this.mSessionInfos.Count <= 0)
      return;
    this.AvoidOverlapping();
  }

  private void AvoidOverlapping()
  {
    for (int index = 0; index < 5; ++index)
    {
      this.mInterval = UISessionInfo.checkInterval * (float) index;
      this.mPreviewAnchorPosition.x = this.container.anchoredPosition.x + this.mDefaultAnchorPosition.x;
      this.mPreviewAnchorPosition.y = this.container.anchoredPosition.y + this.mDefaultAnchorPosition.y - this.mInterval;
      this.SetRectPosition(this, true, ref this.mTargetRect);
      if (!this.OverlapsAnyInfo())
      {
        this.mTargetAnchorPosition.Set(this.mDefaultAnchorPosition.x, this.mDefaultAnchorPosition.y - this.mInterval);
        break;
      }
    }
    if (Mathf.Approximately(Vector2.Distance(this.animationRect.anchoredPosition, this.mTargetAnchorPosition), 0.0f))
      return;
    this.animationRect.anchoredPosition = new Vector2(this.animationRect.anchoredPosition.x, EasingUtility.EaseByType(EasingUtility.Easing.InOutCubic, this.animationRect.anchoredPosition.y, this.mTargetAnchorPosition.y, GameTimer.deltaTime * 7f));
    this.lineRect.sizeDelta = new Vector2(this.lineRect.sizeDelta.x, Mathf.Max(0.0f, Mathf.Clamp01(Mathf.Abs(this.animationRect.anchoredPosition.y + this.mDefaultLineAdjustment) / (this.mDefaultAnchorPosition.y + this.mDefaultLineAdjustment)) * this.mDefaultLineHeight));
  }

  private bool OverlapsAnyInfo()
  {
    int count = this.mSessionInfos.Count;
    for (int index = 0; index < count; ++index)
    {
      this.SetRectPosition(this.mSessionInfos[index], false, ref this.mCheckInfoRect);
      if (this.mTargetRect.Overlaps(this.mCheckInfoRect))
        return true;
    }
    return false;
  }

  public void ResetInstant()
  {
    if ((double) this.lineRect.sizeDelta.y == (double) this.mDefaultLineHeight)
      return;
    this.lineRect.sizeDelta = new Vector2(this.lineRect.sizeDelta.x, this.mDefaultLineHeight);
    this.animationRect.anchoredPosition = this.mDefaultAnchorPosition;
  }

  private void SetRectPosition(UISessionInfo inInfo, bool inPreviewPoint, ref Rect inRect)
  {
    if (inPreviewPoint)
    {
      inRect.x = this.mPreviewAnchorPosition.x;
      inRect.y = this.mPreviewAnchorPosition.y;
      inRect.width = this.animationRect.rect.width;
      inRect.height = this.animationRect.rect.height;
    }
    else
    {
      inRect.x = (float) ((double) inInfo.container.anchoredPosition.x + (double) inInfo.targetAnchorPosition.x - (double) UISessionInfo.borderSafe / 2.0);
      inRect.y = (float) ((double) inInfo.container.anchoredPosition.y + (double) inInfo.targetAnchorPosition.y + (double) UISessionInfo.borderSafe / 2.0);
      inRect.width = inInfo.animationRect.rect.width + UISessionInfo.borderSafe;
      inRect.height = inInfo.animationRect.rect.height + UISessionInfo.borderSafe;
    }
  }
}
