// Decompiled with JetBrains decompiler
// Type: PitStopTyresWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class PitStopTyresWidget : MonoBehaviour
{
  private int mTyresReady = -1;
  private bool mHasLeftPitlaneArea = true;
  public PitStopTyreEntry[] tyres;
  public TextMeshProUGUI tyresLabel;
  public GameObject complete;
  public GameObject mistake;
  public GameObject outcome;
  public UIPitOutcome changeOutcome;
  public CanvasGroup canvasGroup;
  public GameObject inProgressContainer;
  private RacingVehicle mVehicle;
  private int mCount;
  private bool mShowMistake;
  private bool mActivated;

  public void Setup(RacingVehicle inVehicle)
  {
    if (inVehicle == null)
      return;
    this.mVehicle = inVehicle;
    this.UpdatePitlaneStatus();
    GameUtility.SetActive(this.complete, false);
    this.mActivated = this.mVehicle.setup.IsChangingTyres();
    this.mCount = this.tyres.Length;
    if (this.mActivated)
      this.mHasLeftPitlaneArea = false;
    if (!this.mActivated && !this.mHasLeftPitlaneArea)
      this.mActivated = true;
    this.ShowMistake(false);
    this.ShowOutcome(false);
    GameUtility.SetActive(this.gameObject, this.mActivated);
    if (!this.mActivated)
      return;
    for (int index = 0; index < this.mCount; ++index)
      this.tyres[index].Setup(this.mVehicle);
    this.mTyresReady = -1;
    this.SetLabel();
    this.SetCanvasGroup();
  }

  private void UpdatePitlaneStatus()
  {
    if (this.mHasLeftPitlaneArea || this.mVehicle == null || this.mVehicle.pathController.IsOnPitlanePath())
      return;
    this.mHasLeftPitlaneArea = true;
  }

  private void Update()
  {
    this.UpdatePitlaneStatus();
    this.SetLabel();
  }

  private void SetLabel()
  {
    if (!this.mActivated)
      return;
    this.mShowMistake = false;
    int num = 0;
    bool flag = false;
    bool inIsActive = true;
    for (int index = 0; index < this.mCount; ++index)
    {
      PitStopTyreEntry tyre = this.tyres[index];
      if (tyre.isComplete)
        ++num;
      else
        inIsActive = false;
      if (!this.mShowMistake && tyre.inMistake)
        this.mShowMistake = true;
      if (tyre.isStarted)
        flag = true;
    }
    GameUtility.SetActive(this.inProgressContainer, flag && !inIsActive);
    GameUtility.SetActive(this.complete, inIsActive);
    if (this.mTyresReady != num)
    {
      this.mTyresReady = num;
      this.tyresLabel.text = num.ToString() + "/" + this.mCount.ToString();
    }
    this.ShowMistake(this.mShowMistake);
  }

  private void ShowMistake(bool inValue)
  {
    GameUtility.SetActive(this.mistake, inValue);
  }

  public void ShowOutcome(bool inValue)
  {
    GameUtility.SetActive(this.outcome, this.mActivated && inValue);
    if (!this.outcome.activeSelf)
      return;
    this.changeOutcome.SetOutcome(this.mVehicle.setup.changes.GetOutcome(SessionSetupChangeEntry.Target.Tyres));
  }

  private void SetCanvasGroup()
  {
    this.canvasGroup.alpha = !this.mActivated ? 0.35f : 1f;
    this.canvasGroup.interactable = this.mActivated;
  }
}
