// Decompiled with JetBrains decompiler
// Type: FlagWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FlagWidget : MonoBehaviour
{
  public SessionManager.Flag flag;
  public bool turnOffAutomaticly;
  public float stayOnDuration;
  public Animator animator;
  private float mAnimationTimer;

  public virtual void FlagChange()
  {
    if (this.flag == Game.instance.sessionManager.flag && this.flag != SessionManager.Flag.Green)
      this.Show();
    else
      this.Hide();
  }

  public virtual void Show()
  {
    this.gameObject.SetActive(true);
  }

  public virtual void Hide()
  {
    this.gameObject.SetActive(false);
  }

  public virtual void Update()
  {
    if (!((Object) this.animator != (Object) null) || !this.turnOffAutomaticly)
      return;
    this.mAnimationTimer += GameTimer.deltaTime;
    if ((double) this.mAnimationTimer < (double) this.stayOnDuration)
      return;
    this.mAnimationTimer = 0.0f;
    this.Hide();
  }
}
