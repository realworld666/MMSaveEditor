// Decompiled with JetBrains decompiler
// Type: PitStopPositionWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class PitStopPositionWidget : MonoBehaviour
{
  private int mPosition = -1;
  public Action OnEndAnimation;
  public GameObject singleSeaterCarIcon;
  public GameObject gtCarIcon;
  public TextMeshProUGUI driverPosition;
  public TextMeshProUGUI changePosition;
  public GameObject changeParent;
  public GameObject changeUp;
  public GameObject changeDown;
  public Animator animator;
  private RacingVehicle mVehicle;
  private int mPositionChange;

  public void Setup(RacingVehicle inVehicle)
  {
    if (inVehicle == null)
      return;
    this.mVehicle = inVehicle;
    GameUtility.SetActive(this.gtCarIcon, Game.instance.sessionManager.championship.series == Championship.Series.GTSeries);
    GameUtility.SetActive(this.singleSeaterCarIcon, Game.instance.sessionManager.championship.series == Championship.Series.SingleSeaterSeries);
    this.mPosition = -1;
    this.mPositionChange = 0;
    this.UpdatePosition();
  }

  private void Update()
  {
    this.UpdatePosition();
    this.CheckAnimation();
  }

  private void UpdatePosition()
  {
    if (this.mVehicle == null)
      return;
    SessionTimer.PitstopData currentPitstop = this.mVehicle.timer.currentPitstop;
    int num1 = currentPitstop.exitPosition <= 0 ? this.mVehicle.standingsPosition : currentPitstop.exitPosition;
    if (num1 != this.mPosition)
    {
      this.mPosition = num1;
      this.driverPosition.text = GameUtility.FormatForPosition(this.mPosition, (string) null);
    }
    int num2 = currentPitstop.entrancePosition - (currentPitstop.exitPosition <= 0 ? this.mVehicle.standingsPosition : currentPitstop.exitPosition);
    GameUtility.SetActive(this.changeParent, num2 != 0);
    if (!this.changeParent.activeSelf || num2 == this.mPositionChange)
      return;
    GameUtility.SetActive(this.changeUp, num2 > 0);
    GameUtility.SetActive(this.changeDown, num2 < 0);
    this.changePosition.text = Mathf.Abs(num2).ToString();
    this.changePosition.color = num2 <= 0 ? UIConstants.negativeColor : UIConstants.positiveColor;
    this.mPositionChange = num2;
  }

  public void PlayAnimation(bool inEnter)
  {
    if (inEnter)
      this.animator.Play(AnimationHashes.EnterPits, 0, 0.0f);
    else
      this.animator.Play(AnimationHashes.ExitPits, 0, 0.0f);
  }

  private void CheckAnimation()
  {
    AnimatorStateInfo animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
    if (animatorStateInfo.shortNameHash != AnimationHashes.ExitPits || (double) animatorStateInfo.normalizedTime < 1.0 || this.OnEndAnimation == null)
      return;
    this.OnEndAnimation();
    this.OnEndAnimation = (Action) null;
  }
}
