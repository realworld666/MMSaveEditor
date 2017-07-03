// Decompiled with JetBrains decompiler
// Type: UIDataCenterTimeWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDataCenterTimeWidget : UIBaseSessionTopBarWidget
{
  private int mLapNumber = -1;
  private float mTime = -1f;
  public Animator animator;
  public Image timeBar;
  public TextMeshProUGUI lapLabel;
  private float mAnimatorTime;

  public override void OnEnter()
  {
    this.animator.Play(AnimationHashes.PausedAnimation, 0, this.mAnimatorTime);
    GameUtility.SetImageFillAmountIfDifferent(this.timeBar, this.GetNormalizedTime(), 1f / 512f);
    this.GetRaceTypeString(ref this.lapLabel);
  }

  private void Update()
  {
    this.mAnimatorTime = this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
  }

  private void GetRaceTypeString(ref TextMeshProUGUI label)
  {
    if (Game.instance.sessionManager.endCondition == SessionManager.EndCondition.LapCount)
    {
      if (Game.instance.sessionManager.lap == this.mLapNumber)
        return;
      this.mLapNumber = Game.instance.sessionManager.lap;
      label.text = Game.instance.sessionManager.GetCurrentLapAndCounter();
    }
    else
    {
      if (Game.instance.sessionManager.endCondition != SessionManager.EndCondition.Time)
        return;
      float inTime = Mathf.Round(Game.instance.sessionManager.time);
      if ((double) this.mTime == (double) inTime)
        return;
      this.mTime = inTime;
      label.text = GameUtility.GetSessionTimeText(inTime);
    }
  }

  private float GetNormalizedTime()
  {
    float num = 0.0f;
    if (Game.instance.sessionManager.endCondition == SessionManager.EndCondition.LapCount)
      num = (float) Game.instance.sessionManager.lap / (float) Game.instance.sessionManager.lapCount;
    else if (Game.instance.sessionManager.endCondition == SessionManager.EndCondition.Time && (double) Game.instance.sessionManager.time != 0.0)
      num = (float) (1.0 - (double) Game.instance.sessionManager.time / (double) Game.instance.sessionManager.championship.rules.qualifyingDuration[0]);
    return num;
  }

  public override bool ShouldBeEnabled()
  {
    if (App.instance.gameStateManager.currentState is SessionState)
      return !(UIManager.instance.currentScreen is SessionHUD);
    return false;
  }
}
