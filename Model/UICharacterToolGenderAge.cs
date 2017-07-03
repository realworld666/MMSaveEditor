// Decompiled with JetBrains decompiler
// Type: UICharacterToolGenderAge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICharacterToolGenderAge : MonoBehaviour
{
  public Toggle maleToggle;
  public Toggle femaleToggle;
  public Button defaultButton;
  public Slider ageSlider;
  public TextMeshProUGUI ageStatus;
  public CharacterCreatorToolScreen screen;

  public void OnStart()
  {
    this.maleToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle(this.maleToggle, Person.Gender.Male)));
    this.femaleToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle(this.femaleToggle, Person.Gender.Female)));
    this.defaultButton.onClick.AddListener(new UnityAction(this.OnDefault));
    this.ageSlider.onValueChanged.AddListener((UnityAction<float>) (param0 => this.OnSlider()));
  }

  public void Setup()
  {
    this.screen.profilesWidget.OnSelection -= new Action(this.UpdateButtonsState);
    this.screen.profilesWidget.OnSelection += new Action(this.UpdateButtonsState);
    this.UpdateButtonsState();
  }

  private void OnToggle(Toggle inToggle, Person.Gender inGender)
  {
    if (!inToggle.isOn)
      return;
    this.screen.profilesWidget.SetGenderSelected(inGender, false);
    if (this.screen.profilesWidget.selectedNum != 1)
      return;
    this.screen.customizeWidget.portrait.UpdateAgeGender();
  }

  private void OnDefault()
  {
    this.screen.profilesWidget.SetGenderSelected(Person.Gender.Male, true);
  }

  private void OnSlider()
  {
    int inAge = Mathf.RoundToInt(this.ageSlider.value);
    if (inAge < 18)
    {
      this.ageStatus.text = "( DEFAULT )";
      this.screen.profilesWidget.SetAgeSelected(inAge, true);
    }
    else
    {
      this.ageStatus.text = "( " + inAge.ToString() + " YEARS )";
      this.screen.profilesWidget.SetAgeSelected(inAge, false);
    }
    if (this.screen.profilesWidget.selectedNum != 1)
      return;
    this.screen.customizeWidget.portrait.UpdateAgeGender();
    this.screen.customizeWidget.portrait.UpdatePortrait();
  }

  private void UpdateButtonsState()
  {
    this.defaultButton.interactable = this.screen.profilesWidget.selectedNum > 0;
  }
}
