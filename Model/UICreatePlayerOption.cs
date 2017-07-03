// Decompiled with JetBrains decompiler
// Type: UICreatePlayerOption
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICreatePlayerOption : MonoBehaviour
{
  public UICreatePlayerOption.Type type = UICreatePlayerOption.Type.Accessories;
  public Button leftButton;
  public Button rightButton;
  public TextMeshProUGUI label;
  public UICreatePlayerAppearanceWidget widget;

  public void OnStart()
  {
    this.leftButton.onClick.RemoveAllListeners();
    this.leftButton.onClick.AddListener(new UnityAction(this.OnLeftButton));
    this.rightButton.onClick.RemoveAllListeners();
    this.rightButton.onClick.AddListener(new UnityAction(this.OnRightButton));
  }

  private void OnLeftButton()
  {
    switch (this.type)
    {
      case UICreatePlayerOption.Type.HairStyle:
        this.widget.screen.playerPortrait.SetDetailType(this.widget.screen.playerPortrait.hair - 1, Portrait.FeatureType.Hair, this.widget.screen.playerGender);
        this.widget.SelectHairStyle();
        break;
      case UICreatePlayerOption.Type.FacialHair:
        this.widget.screen.playerPortrait.SetDetailType(this.widget.screen.playerPortrait.facialHair - 1, Portrait.FeatureType.FacialHair, this.widget.screen.playerGender);
        this.widget.SelectFacialHair();
        break;
      case UICreatePlayerOption.Type.Glasses:
        this.widget.screen.playerPortrait.SetDetailType(this.widget.screen.playerPortrait.glasses - 1, Portrait.FeatureType.Glasses, this.widget.screen.playerGender);
        this.widget.SelectGlasses();
        break;
      case UICreatePlayerOption.Type.Accessories:
        this.widget.screen.playerPortrait.SetDetailType(this.widget.screen.playerPortrait.accessory - 1, Portrait.FeatureType.Accessory, this.widget.screen.playerGender);
        this.widget.SelectAccessory();
        break;
    }
    this.RefreshLabel();
  }

  private void OnRightButton()
  {
    switch (this.type)
    {
      case UICreatePlayerOption.Type.HairStyle:
        this.widget.screen.playerPortrait.SetDetailType(this.widget.screen.playerPortrait.hair + 1, Portrait.FeatureType.Hair, this.widget.screen.playerGender);
        this.widget.SelectHairStyle();
        break;
      case UICreatePlayerOption.Type.FacialHair:
        this.widget.screen.playerPortrait.SetDetailType(this.widget.screen.playerPortrait.facialHair + 1, Portrait.FeatureType.FacialHair, this.widget.screen.playerGender);
        this.widget.SelectFacialHair();
        break;
      case UICreatePlayerOption.Type.Glasses:
        this.widget.screen.playerPortrait.SetDetailType(this.widget.screen.playerPortrait.glasses + 1, Portrait.FeatureType.Glasses, this.widget.screen.playerGender);
        this.widget.SelectGlasses();
        break;
      case UICreatePlayerOption.Type.Accessories:
        this.widget.screen.playerPortrait.SetDetailType(this.widget.screen.playerPortrait.accessory + 1, Portrait.FeatureType.Accessory, this.widget.screen.playerGender);
        this.widget.SelectAccessory();
        break;
    }
    this.RefreshLabel();
  }

  public void RefreshButtonsInteractable()
  {
    switch (this.type)
    {
      case UICreatePlayerOption.Type.HairStyle:
        switch (this.widget.screen.playerGender)
        {
          case Person.Gender.Male:
            this.rightButton.interactable = this.widget.screen.playerPortrait.hair < Portrait.hairStylesMale.Length - 1;
            break;
          case Person.Gender.Female:
            this.rightButton.interactable = this.widget.screen.playerPortrait.hair < Portrait.hairStylesFemale.Length - 1;
            break;
        }
        this.leftButton.interactable = this.widget.screen.playerPortrait.hair > 0;
        break;
      case UICreatePlayerOption.Type.FacialHair:
        switch (this.widget.screen.playerGender)
        {
          case Person.Gender.Male:
            this.rightButton.interactable = this.widget.screen.playerPortrait.facialHair < Portrait.facialHairMale.Length - 1;
            this.leftButton.interactable = this.widget.screen.playerPortrait.facialHair > 0;
            return;
          case Person.Gender.Female:
            this.rightButton.interactable = false;
            this.leftButton.interactable = false;
            return;
          default:
            return;
        }
      case UICreatePlayerOption.Type.Glasses:
        switch (this.widget.screen.playerGender)
        {
          case Person.Gender.Male:
            this.rightButton.interactable = this.widget.screen.playerPortrait.glasses < Portrait.glassesMale.Length - 1;
            break;
          case Person.Gender.Female:
            this.rightButton.interactable = this.widget.screen.playerPortrait.glasses < Portrait.glassesFemale.Length - 1;
            break;
        }
        this.leftButton.interactable = this.widget.screen.playerPortrait.glasses > 0;
        break;
      case UICreatePlayerOption.Type.Accessories:
        switch (this.widget.screen.playerGender)
        {
          case Person.Gender.Male:
            this.rightButton.interactable = this.widget.screen.playerPortrait.accessory < Portrait.accessoriesMale.Length - 1;
            break;
          case Person.Gender.Female:
            this.rightButton.interactable = this.widget.screen.playerPortrait.accessory < Portrait.accessoriesFemale.Length - 1;
            break;
        }
        this.leftButton.interactable = this.widget.screen.playerPortrait.accessory > 0;
        break;
    }
  }

  public void RefreshLabel()
  {
    switch (this.type)
    {
      case UICreatePlayerOption.Type.HairStyle:
        this.label.text = Localisation.LocaliseID((this.widget.screen.playerGender != Person.Gender.Male ? Portrait.hairStylesFemale : Portrait.hairStylesMale)[this.widget.screen.playerPortrait.hair], (GameObject) null);
        break;
      case UICreatePlayerOption.Type.FacialHair:
        this.label.text = Localisation.LocaliseID(Portrait.facialHairMale[this.widget.screen.playerPortrait.facialHair], (GameObject) null);
        break;
      case UICreatePlayerOption.Type.Glasses:
        this.label.text = Localisation.LocaliseID((this.widget.screen.playerGender != Person.Gender.Male ? Portrait.glassesFemale : Portrait.glassesMale)[this.widget.screen.playerPortrait.glasses], (GameObject) null);
        break;
      case UICreatePlayerOption.Type.Accessories:
        this.label.text = (this.widget.screen.playerGender != Person.Gender.Male ? Portrait.accessoriesFemale : Portrait.accessoriesMale)[this.widget.screen.playerPortrait.accessory];
        break;
    }
    this.RefreshButtonsInteractable();
  }

  public enum Type
  {
    HairStyle,
    FacialHair,
    Glasses,
    Accessories,
  }
}
