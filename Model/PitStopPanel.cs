// Decompiled with JetBrains decompiler
// Type: PitStopPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class PitStopPanel : MonoBehaviour
{
  public GameObject[] containersForPitPenalty = new GameObject[0];
  public PitStopTimeWidget timerPitLane;
  public PitStopTimeWidget timerPitStop;
  public PitStopPositionWidget positionWidget;
  public PitStopBatteryWidget batteryWidget;
  public PitStopFuelWidget fuelWidget;
  public PitStopTyresWidget tyresWidget;
  public PitStopRepairWidget repairWidget;
  public Animator animator;
  public DriverActionButtons widget;
  private RacingVehicle mVehicle;

  public bool isVisible
  {
    get
    {
      return this.gameObject.activeSelf;
    }
  }

  public void SetVehicle(RacingVehicle inVehicle)
  {
    if (inVehicle == null)
      return;
    this.mVehicle = inVehicle;
    this.timerPitLane.Setup(this.mVehicle);
    this.timerPitStop.Setup(this.mVehicle);
    this.positionWidget.Setup(this.mVehicle);
    this.batteryWidget.Setup(this.mVehicle);
    this.fuelWidget.Setup(this.mVehicle);
    this.tyresWidget.Setup(this.mVehicle);
    this.repairWidget.Setup(this.mVehicle);
    this.SetOutcome();
  }

  private void Update()
  {
    this.SetOutcome();
    this.CheckAnimation();
  }

  private void SetOutcome()
  {
    if (this.mVehicle == null)
      return;
    bool inValue = this.mVehicle.setup.state == SessionSetup.State.Setup && (double) this.mVehicle.timer.currentPitstop.stopTime > 0.0;
    this.fuelWidget.ShowOutcome(inValue);
    this.tyresWidget.ShowOutcome(inValue);
    this.repairWidget.ShowOutcome(inValue);
  }

  private void CheckAnimation()
  {
    AnimatorStateInfo animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
    if (animatorStateInfo.shortNameHash != AnimationHashes.Hide || (double) animatorStateInfo.normalizedTime < 1.0)
      return;
    this.OnComplete();
  }

  private void OnEnable()
  {
    this.positionWidget.OnEndAnimation += new Action(this.OnEndAnimation);
  }

  private void OnDisable()
  {
    this.positionWidget.OnEndAnimation -= new Action(this.OnEndAnimation);
  }

  public void Show(RacingVehicle inVehicle)
  {
    GameUtility.SetActive(this.gameObject, true);
    if (inVehicle != null)
      this.SetVehicle(inVehicle);
    for (int index = 0; index < this.containersForPitPenalty.Length; ++index)
      GameUtility.SetActive(this.containersForPitPenalty[index], this.mVehicle.strategy.status != SessionStrategy.Status.PitThruPenalty);
  }

  private void OnEndAnimation()
  {
    this.animator.Play(AnimationHashes.Hide, 0, 0.0f);
  }

  private void OnComplete()
  {
    this.Hide();
    this.widget.OnPitlaneExit();
  }

  public void Hide()
  {
    GameUtility.SetActive(this.gameObject, false);
  }
}
