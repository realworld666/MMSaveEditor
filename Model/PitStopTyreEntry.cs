// Decompiled with JetBrains decompiler
// Type: PitStopTyreEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PitStopTyreEntry : MonoBehaviour
{
  public SessionSetupChangeEntry.TyreSlot tyreSlot = SessionSetupChangeEntry.TyreSlot.BackLeft;
  public GameObject tyreFull;
  public GameObject mistakeIcon;
  private RacingVehicle mVehicle;
  private SessionSetup mSetup;
  private SessionSetupChangeEntry mChange;
  private bool mComplete;
  private bool mMistakeInProgress;
  private bool mIsStarted;

  public bool isComplete
  {
    get
    {
      return this.mComplete;
    }
  }

  public bool inMistake
  {
    get
    {
      return this.mMistakeInProgress;
    }
  }

  public bool isStarted
  {
    get
    {
      return this.mIsStarted;
    }
  }

  public void Setup(RacingVehicle inVehicle)
  {
    if (inVehicle == null)
      return;
    this.mVehicle = inVehicle;
    this.mSetup = this.mVehicle.setup;
    this.mChange = this.mSetup.changes.GetChangeTyre(this.tyreSlot);
    GameUtility.SetActive(this.tyreFull, false);
    GameUtility.SetActive(this.mistakeIcon, false);
    this.mComplete = false;
    this.mMistakeInProgress = false;
  }

  public void Update()
  {
    float stopTime = this.mVehicle.timer.currentPitstop.stopTime;
    if (this.mChange == null || (double) stopTime <= 0.0)
      return;
    this.mIsStarted = (double) stopTime >= (double) this.mChange.pitStopStart;
    this.mComplete = this.mChange.isSet && (double) stopTime >= (double) this.mChange.mistakeEnd;
    this.mMistakeInProgress = this.mChange.isSet && (double) stopTime >= (double) this.mChange.mistakeStart && (double) stopTime < (double) this.mChange.mistakeEnd;
    GameUtility.SetActive(this.mistakeIcon.gameObject, this.mMistakeInProgress);
    GameUtility.SetActive(this.tyreFull.gameObject, this.mComplete);
  }
}
