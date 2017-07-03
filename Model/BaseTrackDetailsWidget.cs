// Decompiled with JetBrains decompiler
// Type: BaseTrackDetailsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class BaseTrackDetailsWidget : MonoBehaviour
{
  private float mDuration = 3f;
  private bool mIsComplete;
  private float mTimer;

  public bool isComplete
  {
    get
    {
      return this.mIsComplete;
    }
  }

  protected void SetComplete(bool inIsComplete)
  {
    this.mIsComplete = inIsComplete;
  }

  public virtual void Show()
  {
    this.gameObject.SetActive(true);
    this.SetComplete(false);
    this.mTimer = 0.0f;
  }

  public virtual void Hide()
  {
    this.gameObject.SetActive(false);
  }

  protected virtual void Update()
  {
    this.mTimer += GameTimer.deltaTime;
    if ((double) this.mTimer < (double) this.mDuration)
      return;
    this.SetComplete(true);
  }
}
