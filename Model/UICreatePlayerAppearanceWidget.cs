// Decompiled with JetBrains decompiler
// Type: UICreatePlayerAppearanceWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICreatePlayerAppearanceWidget : MonoBehaviour
{
  private bool mAllowMenuSounds = true;
  public Dropdown gender;
  public UICreatePlayerOption[] options;
  public GameObject genderParent;
  public GameObject facialHairParent;
  public UIGridList skinToneGrid;
  public UIGridList hairColourGrid;
  public CreatePlayerScreen screen;

  public void OnStart()
  {
    for (int index = 0; index < this.options.Length; ++index)
      this.options[index].OnStart();
  }

  public void Setup()
  {
    this.mAllowMenuSounds = false;
    this.SetGender();
    this.SetSkinTones(true);
    this.SetHairColours(true);
    this.mAllowMenuSounds = true;
  }

  private void SetGender()
  {
    this.gender.onValueChanged.RemoveAllListeners();
    this.gender.ClearOptions();
    this.gender.AddOptions(new List<string>()
    {
      Localisation.LocaliseID("PSG_10002344", (GameObject) null),
      Localisation.LocaliseID("PSG_10002345", (GameObject) null)
    });
    int count = this.gender.get_options().Count;
    for (int index = 0; index < count; ++index)
    {
      if ((Person.Gender) index == this.screen.playerGender)
        this.gender.value = index;
    }
    this.gender.RefreshShownValue();
    this.gender.onValueChanged.AddListener((UnityAction<int>) (param0 => this.SelectGender()));
    GameUtility.SetActive(this.genderParent, !Game.IsActive());
    GameUtility.SetActive(this.facialHairParent, this.screen.playerGender == Person.Gender.Male);
  }

  public void SetSkinTones(bool inSetup)
  {
    int length = Portrait.skinColors.Length;
    int head = this.screen.playerPortrait.head;
    for (int inIndex = 0; inIndex < length; ++inIndex)
    {
      UICreatePlayerColorEntry playerColorEntry = this.skinToneGrid.GetOrCreateItem<UICreatePlayerColorEntry>(inIndex);
      if (inSetup)
        playerColorEntry.Setup(inIndex, Portrait.skinColors[inIndex]);
      playerColorEntry.toggle.isOn = inIndex == head;
    }
    GameUtility.SetActive(this.skinToneGrid.itemPrefab, false);
  }

  public void SetHairColours(bool inSetup)
  {
    int length = Portrait.hairColors.Length;
    int hairColor = this.screen.playerPortrait.hairColor;
    for (int inIndex = 0; inIndex < length; ++inIndex)
    {
      UICreatePlayerColorEntry playerColorEntry = this.hairColourGrid.GetOrCreateItem<UICreatePlayerColorEntry>(inIndex);
      if (inSetup)
        playerColorEntry.Setup(inIndex, Portrait.hairColors[inIndex]);
      playerColorEntry.toggle.isOn = inIndex == hairColor;
    }
    GameUtility.SetActive(this.hairColourGrid.itemPrefab, false);
  }

  public void SetHairStyles()
  {
    this.UpdateLabel(UICreatePlayerOption.Type.HairStyle);
  }

  public void SetFacialHair()
  {
    if (this.screen.playerGender != Person.Gender.Male)
      return;
    this.UpdateLabel(UICreatePlayerOption.Type.FacialHair);
  }

  public void SetGlasses()
  {
    this.UpdateLabel(UICreatePlayerOption.Type.Glasses);
  }

  public void SetAccessories()
  {
    this.UpdateLabel(UICreatePlayerOption.Type.Accessories);
  }

  private void UpdateLabel(UICreatePlayerOption.Type inType)
  {
    for (int index = 0; index < this.options.Length; ++index)
    {
      if (this.options[index].type == inType)
        this.options[index].RefreshLabel();
    }
  }

  private void SelectGender()
  {
    this.screen.playerGender = (Person.Gender) this.gender.value;
    GameUtility.SetActive(this.facialHairParent, this.screen.playerGender == Person.Gender.Male);
    this.PlayMenuSound();
    this.mAllowMenuSounds = false;
    this.screen.AutoGeneratePortrait();
    this.mAllowMenuSounds = true;
    this.screen.detailsWidget.UpdateStringsForLocalisation();
    UILocaliseLabel.ForceRefreshOnLateUpdate();
  }

  public void SelectSkinTone(int inIndex)
  {
    this.screen.playerPortrait.head = inIndex;
    this.PlayMenuSound();
  }

  public void SelectHairStyle()
  {
    this.screen.profileWidget.RefreshPortrait();
    this.PlayMenuSound();
  }

  public void SelectHairColour(int inIndex)
  {
    this.screen.playerPortrait.hairColor = inIndex;
    this.PlayMenuSound();
  }

  public void SelectFacialHair()
  {
    this.screen.profileWidget.RefreshPortrait();
    this.PlayMenuSound();
  }

  public void SelectGlasses()
  {
    this.screen.profileWidget.RefreshPortrait();
    this.PlayMenuSound();
  }

  public void SelectAccessory()
  {
    this.screen.profileWidget.RefreshPortrait();
    this.PlayMenuSound();
  }

  public void PlayMenuSound()
  {
    if (!this.mAllowMenuSounds)
      return;
    scSoundManager.Instance.PlaySound(SoundID.Button_AppearanceChange, 0.0f);
  }
}
