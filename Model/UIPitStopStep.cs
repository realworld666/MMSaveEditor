// Decompiled with JetBrains decompiler
// Type: UIPitStopStep
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPitStopStep : MonoBehaviour
{
  public Toggle toggle;
  public GameObject tick;
  public TextMeshProUGUI changedText;
  public UIPitOptionsSelectionWidget optionsSelectionWidget;
  private GameObject mStepOptionsWidget;
  private bool mIsComplete;
  private bool mIsChanged;

  public bool isComplete
  {
    get
    {
      return this.mIsComplete;
    }
  }

  public void OnStart(GameObject inOptionWidget)
  {
    this.mStepOptionsWidget = inOptionWidget;
    GameUtility.SetActive(this.tick, false);
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle()));
    GameUtility.SetActive(this.mStepOptionsWidget, false);
  }

  public void Setup()
  {
    this.SetComplete(false);
    this.mIsChanged = false;
    this.UpdateChangedText(this.mIsChanged);
    GameUtility.SetActive(this.mStepOptionsWidget, false);
  }

  public void OnToggle()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.SetComplete(true);
    GameUtility.SetActive(this.mStepOptionsWidget, this.toggle.isOn);
  }

  public void SetComplete(bool inValue)
  {
    this.mIsComplete = inValue;
    GameUtility.SetActive(this.tick, this.mIsComplete);
    this.optionsSelectionWidget.NotifyStepComplete();
  }

  public void SetChanged(bool inChanged)
  {
    if (inChanged == this.mIsChanged)
      return;
    this.mIsChanged = inChanged;
    this.UpdateChangedText(inChanged);
  }

  private void UpdateChangedText(bool inChanged)
  {
    if (inChanged)
    {
      this.changedText.SetText(Localisation.LocaliseID("PSG_10010647", (GameObject) null));
      this.changedText.color = UIConstants.pitStopChangedOrange;
    }
    else
    {
      this.changedText.SetText(Localisation.LocaliseID("PSG_10010648", (GameObject) null));
      this.changedText.color = UIConstants.driverPerformanceOutcomeGrey;
    }
  }

  public void OnDisable()
  {
    this.toggle.isOn = false;
    this.OnToggle();
  }
}
