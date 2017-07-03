// Decompiled with JetBrains decompiler
// Type: UITravelStep
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITravelStep : MonoBehaviour
{
  public UITravelStep.Step step = UITravelStep.Step.Sponsor;
  private bool mRequiresAction = true;
  public Toggle toggle;
  public GameObject tick;
  public UITravelStepOption option;
  public UITravelSelectionWidget selectionWidget;
  private bool mIsComplete;
  private bool mIsSeen;

  public bool requiresAction
  {
    get
    {
      return this.mRequiresAction;
    }
    set
    {
      this.mRequiresAction = value;
      this.selectionWidget.RefreshIsCompleteAndGetCurrentWarning();
    }
  }

  public void OnStart()
  {
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle()));
    this.option.OnStart();
    GameUtility.SetActive(this.option.gameObject, false);
    this.mIsComplete = false;
    this.mIsSeen = false;
  }

  public void Setup()
  {
    this.option.Setup();
    this.SetComplete(this.mIsComplete);
    GameUtility.SetActive(this.option.gameObject, false);
  }

  public void RefreshText()
  {
    this.option.RefreshText();
  }

  private void Update()
  {
    if (this.mIsComplete != this.IsComplete())
    {
      this.mIsComplete = this.IsComplete();
      this.requiresAction = !this.option.IsReady();
    }
    GameUtility.SetActive(this.tick, this.mIsComplete);
  }

  public void SetComplete(bool inValue)
  {
    this.mIsComplete = inValue;
    GameUtility.SetActive(this.tick, this.mIsComplete);
    this.requiresAction = !this.option.IsReady();
  }

  public void OnToggle()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    GameUtility.SetActive(this.option.gameObject, this.toggle.isOn);
    if (!this.toggle.isOn)
      return;
    this.mIsSeen = true;
  }

  public bool IsComplete()
  {
    if (this.step == UITravelStep.Step.TyreSelection || this.step == UITravelStep.Step.Sponsor || this.mIsSeen)
      return this.option.IsReady();
    return false;
  }

  public enum Step
  {
    Ultimatum,
    Sponsor,
    TyreSelection,
    CarFitting,
  }
}
