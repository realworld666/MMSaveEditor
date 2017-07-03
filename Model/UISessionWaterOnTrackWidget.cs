// Decompiled with JetBrains decompiler
// Type: UISessionWaterOnTrackWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISessionWaterOnTrackWidget : UIBaseSessionTopBarWidget
{
  [SerializeField]
  private Slider waterAmount;
  [SerializeField]
  private TextMeshProUGUI waterLevelLabel;
  private SessionWeatherDetails mWeatherDetails;
  private Weather.WaterLevel mCachedWaterLevel;
  private SessionInformationDropdown dropdownScript;

  protected void Awake()
  {
    GameUtility.Assert((UnityEngine.Object) this.waterAmount != (UnityEngine.Object) null, "UISessionWaterOnTrackWidget.waterAmount != null", (UnityEngine.Object) this);
    GameUtility.Assert((UnityEngine.Object) this.waterLevelLabel != (UnityEngine.Object) null, "UISessionWaterOnTrackWidget.waterLevelLabel != null", (UnityEngine.Object) this);
    this.keybinding = KeyBinding.Name.WaterConditions;
    this.useKeyBinding = true;
    this.dropdownScript = this.dropdown.GetComponent<SessionInformationDropdown>();
  }

  public override void OnEnter()
  {
    this.mWeatherDetails = Game.instance.sessionManager.currentSessionWeather;
    this.waterLevelLabel.text = Localisation.LocaliseEnum((Enum) this.mCachedWaterLevel);
  }

  public override void ToggleDropdown(bool inValue)
  {
    if (this.dropdownScript.dataType != SessionInformationDropdown.DataType.Water)
    {
      inValue = true;
      GameUtility.SetActive(this.dropdown, false);
    }
    if (inValue)
      this.OpenDropdown();
    else
      this.CloseDropdown();
  }

  private void Update()
  {
    GameUtility.SetSliderAmountIfDifferent(this.waterAmount, this.mWeatherDetails.GetNormalizedTrackWater(), 1000f);
    if (this.mCachedWaterLevel != this.mWeatherDetails.waterLevel)
    {
      this.mCachedWaterLevel = this.mWeatherDetails.waterLevel;
      this.waterLevelLabel.text = Localisation.LocaliseEnum((Enum) this.mCachedWaterLevel);
    }
    this.CheckKeyBinding();
  }

  public override void OpenDropdown()
  {
    this.dropdown.GetComponent<SessionInformationDropdown>().dataType = SessionInformationDropdown.DataType.Water;
    GameUtility.SetActive(this.dropdown, true);
  }
}
