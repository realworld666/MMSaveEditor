// Decompiled with JetBrains decompiler
// Type: PitStopTimeWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class PitStopTimeWidget : MonoBehaviour
{
  private float mDisplayedTime = float.MaxValue;
  public GameObject singleSeaterCarIcon;
  public GameObject gtCarIcon;
  public PitStopTimeWidget.TimerType type;
  public TextMeshProUGUI timer;
  public GameObject error;
  public TextMeshProUGUI errorTimer;
  public Animator animator;
  public PitStopPanel widget;
  private RacingVehicle mVehicle;
  private bool mInPitLane;
  private bool mInPitStop;
  private float mTimer;

  public void Setup(RacingVehicle inVehicle)
  {
    if (inVehicle == null)
      return;
    this.mVehicle = inVehicle;
    GameUtility.SetActive(this.singleSeaterCarIcon, Game.instance.sessionManager.championship.series == Championship.Series.SingleSeaterSeries);
    GameUtility.SetActive(this.gtCarIcon, Game.instance.sessionManager.championship.series == Championship.Series.GTSeries);
    this.mInPitLane = false;
    this.mInPitStop = false;
    if ((UnityEngine.Object) this.error != (UnityEngine.Object) null)
      this.error.SetActive(false);
    this.mDisplayedTime = float.MaxValue;
    this.mTimer = 0.0f;
    this.SetTimer();
  }

  private void Update()
  {
    if (this.mVehicle == null)
      return;
    SessionTimer.PitstopData currentPitstop = this.mVehicle.timer.currentPitstop;
    if (this.type == PitStopTimeWidget.TimerType.Pitlane)
    {
      if (this.mVehicle.pathState.IsInPitlaneArea())
      {
        if (!this.mInPitLane)
        {
          this.PlayAnimation(true);
          this.mInPitLane = true;
        }
      }
      else if (this.mInPitLane)
      {
        this.widget.positionWidget.PlayAnimation(false);
        this.mInPitLane = false;
      }
      this.mTimer = currentPitstop.pitlaneTime;
    }
    else if (this.type == PitStopTimeWidget.TimerType.Pitstop)
    {
      if (this.mVehicle.pathState.pathStateGroup == PathStateManager.PathStateGroup.InPitbox)
      {
        if (!this.mInPitStop)
        {
          this.PlayAnimation(true);
          this.widget.timerPitLane.PlayAnimation(false);
          this.mInPitStop = true;
        }
      }
      else if (this.mInPitStop)
      {
        this.PlayAnimation(false);
        this.widget.positionWidget.PlayAnimation(true);
        this.mInPitStop = false;
      }
      this.mTimer = currentPitstop.stopTime;
    }
    this.SetTimer();
    this.CheckError();
  }

  private void SetTimer()
  {
    if ((double) Mathf.Abs(this.mDisplayedTime - this.mTimer) < 0.001)
      return;
    this.mDisplayedTime = this.mTimer;
    this.timer.text = this.mTimer.ToString("0.000", (IFormatProvider) Localisation.numberFormatter);
  }

  public void PlayAnimation(bool inEnter)
  {
    if (inEnter)
      this.animator.Play(AnimationHashes.EnterPits, 0, 0.0f);
    else
      this.animator.Play(AnimationHashes.ExitPits, 0, 0.0f);
  }

  private void CheckError()
  {
    float a = Mathf.Max(this.mVehicle.timer.currentPitstop.stopTime - this.mVehicle.timer.currentPitstop.estimatedPitStopTime, 0.0f);
    bool flag = this.mVehicle.setup.changes.hasMistakeOccured && !Mathf.Approximately(a, 0.0f) && (double) a >= 0.0500000007450581;
    if (!((UnityEngine.Object) this.error != (UnityEngine.Object) null))
      return;
    this.error.SetActive(flag);
    if (!flag)
      return;
    this.errorTimer.text = a.ToString("0.000", (IFormatProvider) Localisation.numberFormatter);
  }

  public enum TimerType
  {
    Pitlane,
    Pitstop,
  }
}
