// Decompiled with JetBrains decompiler
// Type: UIHUBSelectDriverEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHUBSelectDriverEntry : MonoBehaviour
{
  public CanvasGroup canvasGroup;
  public Toggle toggle;
  public UICharacterPortrait driverPortrait;
  public Flag driverFlag;
  public TextMeshProUGUI driverName;
  public TextMeshProUGUI driverStatus;
  public UIAbilityStars stars;
  public Image feedbackBar;
  public Image feedbackBacking;
  public TextMeshProUGUI feedbackLabel;
  public UIHUBDrivers widget;
  private Driver mDriver;
  private Team mTeam;

  public bool isSelected
  {
    get
    {
      return this.toggle.isOn;
    }
  }

  public void OnStart()
  {
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle()));
  }

  private void Update()
  {
    if (this.widget.selectionComplete && !this.toggle.isOn)
    {
      this.canvasGroup.alpha = 1f;
      this.canvasGroup.interactable = false;
    }
    else
    {
      if (this.widget.selectionComplete || this.toggle.isOn)
        return;
      this.canvasGroup.alpha = 1f;
      this.canvasGroup.interactable = true;
    }
  }

  public void Setup(Driver inDriver)
  {
    if (inDriver == null)
      return;
    this.mDriver = inDriver;
    this.mTeam = this.mDriver.contract.GetTeam();
    this.SetDetails();
  }

  private void SetDetails()
  {
    this.toggle.isOn = this.mTeam.IsDriverSelectedForSession(this.mDriver);
    this.driverPortrait.SetPortrait((Person) this.mDriver);
    this.driverFlag.SetNationality(this.mDriver.nationality);
    this.driverName.text = this.mDriver.name;
    this.driverStatus.text = !this.mDriver.IsMainDriver() ? "RESERVE DRIVER" : "MAIN DRIVER";
    this.stars.SetAbilityStarsData(this.mDriver);
    GameUtility.SetImageFillAmountIfDifferent(this.feedbackBar, Mathf.Clamp01(this.mDriver.GetDriverStats().feedback / 20f), 1f / 512f);
    this.feedbackLabel.text = Mathf.RoundToInt(this.mDriver.GetDriverStats().feedback).ToString();
    float num = Mathf.Clamp01((float) (int) this.mDriver.GetDriverStats().feedback / 20f);
    Color color = UIConstants.colorBandGreen;
    if ((double) num <= 0.25)
      color = UIConstants.colorBandRed;
    else if ((double) num <= 0.5)
      color = UIConstants.colorBandYellow;
    else if ((double) num <= 0.75)
      color = UIConstants.colorBandBlue;
    color.a = this.feedbackBar.color.a;
    this.feedbackBar.color = color;
    color.a = this.feedbackBacking.color.a;
    this.feedbackBacking.color = color;
  }

  private void OnToggle()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mDriver == null)
      return;
    if (this.toggle.isOn && !this.mTeam.IsDriverSelectedForSession(this.mDriver))
    {
      this.widget.AddDriver(this.mDriver);
    }
    else
    {
      if (this.toggle.isOn || !this.mTeam.IsDriverSelectedForSession(this.mDriver))
        return;
      this.widget.RemoveDriver(this.mDriver);
    }
  }
}
