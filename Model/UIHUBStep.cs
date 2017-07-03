// Decompiled with JetBrains decompiler
// Type: UIHUBStep
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHUBStep : MonoBehaviour
{
  public UIHUBStep.Step step;
  public Toggle toggle;
  public GameObject tick;
  public UIHUBStepOption option;
  public Animator highlightAnimator;
  private bool mIsComplete;
  private bool mIsSeen;

  public void OnStart()
  {
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle()));
    this.option.OnStart();
    this.mIsSeen = false;
    this.mIsComplete = false;
  }

  public void Reset()
  {
    this.toggle.onValueChanged.RemoveAllListeners();
    this.mIsComplete = false;
    this.mIsSeen = false;
    this.toggle.isOn = false;
    GameUtility.SetActive(this.option.gameObject, false);
    this.SetComplete(false);
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle()));
  }

  public void Setup()
  {
    this.Reset();
    this.option.Setup();
  }

  private void Update()
  {
    this.UpdateTick(false);
  }

  public void SetSeen(bool inValue)
  {
    this.mIsSeen = inValue;
  }

  public void SetComplete(bool inValue)
  {
    this.mIsComplete = inValue;
    GameUtility.SetActive(this.tick, this.mIsComplete);
  }

  private void UpdateTick(bool inToggle = false)
  {
    if (!this.option.gameObject.activeSelf && !inToggle)
      return;
    this.mIsComplete = this.IsComplete();
    GameUtility.SetActive(this.tick, this.mIsComplete);
  }

  public void OnToggle()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.option.OnShow();
    GameUtility.SetActive(this.option.gameObject, this.toggle.isOn);
    if (!this.toggle.isOn)
      return;
    this.mIsSeen = true;
    this.UpdateTick(true);
  }

  public void AnimateHighlight()
  {
    if (!((Object) this.highlightAnimator != (Object) null))
      return;
    this.highlightAnimator.SetTrigger("Animate");
  }

  public bool IsComplete()
  {
    if (this.mIsSeen)
      return this.option.IsReady();
    return false;
  }

  public enum Step
  {
    Drivers,
    Tyres,
    Strategy,
    Setup,
    Knowledge,
  }
}
