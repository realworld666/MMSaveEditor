// Decompiled with JetBrains decompiler
// Type: UISessionRubberOnTrackWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISessionRubberOnTrackWidget : UIBaseSessionTopBarWidget
{
  [SerializeField]
  private Slider rubberAmount;
  [SerializeField]
  private TextMeshProUGUI rubberLevelLabel;
  private SessionWeatherDetails mWeatherDetails;
  private Weather.RubberLevel mRubberLevel;
  private SessionInformationDropdown dropdownScript;

  protected void Awake()
  {
    GameUtility.Assert((UnityEngine.Object) this.rubberAmount != (UnityEngine.Object) null, "UISessionRubberOnTrackWidget.rubberAmount != null", (UnityEngine.Object) this);
    GameUtility.Assert((UnityEngine.Object) this.rubberLevelLabel != (UnityEngine.Object) null, "UISessionRubberOnTrackWidget.rubberLevelLabel != null", (UnityEngine.Object) this);
    this.keybinding = KeyBinding.Name.RubberConditions;
    this.useKeyBinding = true;
    this.dropdownScript = this.dropdown.GetComponent<SessionInformationDropdown>();
  }

  private void Start()
  {
    this.mRubberLevel = this.mWeatherDetails.rubberLevel;
    this.rubberLevelLabel.text = Localisation.LocaliseEnum((Enum) this.mRubberLevel);
  }

  public override void OnEnter()
  {
    this.mWeatherDetails = Game.instance.sessionManager.currentSessionWeather;
  }

  private void Update()
  {
    GameUtility.SetSliderAmountIfDifferent(this.rubberAmount, this.mWeatherDetails.GetNormalizedTrackRubber(), 1000f);
    Weather.RubberLevel rubberLevel = this.mWeatherDetails.rubberLevel;
    if (rubberLevel != this.mRubberLevel)
    {
      this.mRubberLevel = rubberLevel;
      this.rubberLevelLabel.text = Localisation.LocaliseEnum((Enum) this.mRubberLevel);
    }
    this.CheckKeyBinding();
  }

  public override void ToggleDropdown(bool inValue)
  {
    if (this.dropdownScript.dataType != SessionInformationDropdown.DataType.Rubber)
    {
      inValue = true;
      GameUtility.SetActive(this.dropdown, false);
    }
    if (inValue)
      this.OpenDropdown();
    else
      this.CloseDropdown();
  }

  public override void OpenDropdown()
  {
    this.dropdown.GetComponent<SessionInformationDropdown>().dataType = SessionInformationDropdown.DataType.Rubber;
    GameUtility.SetActive(this.dropdown, true);
  }
}
