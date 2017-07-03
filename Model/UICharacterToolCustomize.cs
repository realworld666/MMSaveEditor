// Decompiled with JetBrains decompiler
// Type: UICharacterToolCustomize
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICharacterToolCustomize : MonoBehaviour
{
  public UICharacterToolPortrait portrait;
  public UICharacterToolGenderAge ageGender;
  public UICharacterToolSkinTone skinTone;
  public UICharacterToolHairColour hairColor;
  public UICharacterToolHairStyle hairStyle;
  public UICharacterToolGlasses glasses;
  public UICharacterToolAccessories accessories;
  public UICharacterToolFacialHair facialHair;
  public Button editButton;
  public CharacterCreatorToolScreen screen;

  public void OnStart()
  {
    this.ageGender.OnStart();
    this.editButton.onClick.AddListener(new UnityAction(this.OnEditButton));
  }

  public void Setup()
  {
    this.screen.profilesWidget.OnSelection -= new Action(this.UpdateButtonsState);
    this.screen.profilesWidget.OnSelection += new Action(this.UpdateButtonsState);
    this.ageGender.Setup();
    this.portrait.Setup();
    this.skinTone.Setup();
    this.hairColor.Setup();
    this.hairStyle.Setup();
    this.glasses.Setup();
    this.accessories.Setup();
    this.facialHair.Setup();
  }

  public void SetTrait(UICharacterToolTrait inTrait, int inNum)
  {
    this.screen.profilesWidget.SetTraitSelected(inTrait.trait, inNum);
  }

  private void UpdateButtonsState()
  {
    this.editButton.interactable = this.screen.profilesWidget.selectedNum == 1;
  }

  private void OnEditButton()
  {
    if (this.screen.profilesWidget.selectedNum != 1)
      return;
    this.screen.editPopup.Setup(this.screen.profilesWidget.selectedPerson);
  }
}
