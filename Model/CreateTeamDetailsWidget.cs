// Decompiled with JetBrains decompiler
// Type: CreateTeamDetailsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CreateTeamDetailsWidget : MonoBehaviour
{
  public TMP_InputField mainTextInput;
  public TMP_InputField subTextInput;
  public Dropdown continentDropdown;
  public UIGridList nationalityGrid;
  public ScrollRect nationalityScrollRect;
  public ToggleGroup nationalityToggleGroup;
  public UICharacterPortrait driver;
  public UICharacterPortrait engineer;
  public UICharacterPortrait mechanic;
  public Image primaryColor;
  public Image secondaryColor;
  public Toggle primaryColorToggle;
  public Toggle secondaryColorToggle;
  public RectTransform primaryColorRect;
  public RectTransform secondaryColorRect;
  public UITeamCreateLogo teamCreateLogo;
  public TextMeshProUGUI teamLogoLabel;
  public Button leftButtonTeamLogo;
  public Button rightButtonTeamLogo;
  public Flag teamNationality;
  public TextMeshProUGUI teamNationalityName;
  public TextMeshProUGUI hatStyleLabel;
  public Button leftButtonHatStyle;
  public Button rightButtonHatStyle;
  public TextMeshProUGUI shirtStyleLabel;
  public Button leftButtonShirtStyle;
  public Button rightButtonShirtStyle;

  public void OnStart()
  {
    this.leftButtonTeamLogo.onClick.AddListener(new UnityAction(this.OnLeftButtonTeamLogo));
    this.rightButtonTeamLogo.onClick.AddListener(new UnityAction(this.OnRightButtonTeamLogo));
    this.leftButtonHatStyle.onClick.AddListener(new UnityAction(this.OnLeftButtonHatStyle));
    this.rightButtonHatStyle.onClick.AddListener(new UnityAction(this.OnRightButtonHatStyle));
    this.leftButtonShirtStyle.onClick.AddListener(new UnityAction(this.OnLeftButtonShirtStyle));
    this.rightButtonShirtStyle.onClick.AddListener(new UnityAction(this.OnRightButtonShirtStyle));
    this.primaryColorToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnPrimaryColorToggle));
    this.secondaryColorToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnSecondaryColorToggle));
  }

  public void Setup()
  {
    this.continentDropdown.onValueChanged.RemoveAllListeners();
    this.mainTextInput.onValueChanged.RemoveAllListeners();
    this.subTextInput.onValueChanged.RemoveAllListeners();
    this.LoadTeamName();
    this.UpdateButtonTeamLogoState();
    this.UpdateButtonHatStyleState();
    this.UpdateButtonShirtStyleState();
    this.teamCreateLogo.SetStyle();
    this.SetContinents();
    this.SetNationalityGrid();
    this.UpdateTeamPortraits();
    this.UpdateColors();
    this.continentDropdown.onValueChanged.AddListener((UnityAction<int>) (param0 => this.SetNationalityGrid()));
    this.mainTextInput.onValueChanged.AddListener(new UnityAction<string>(this.ValidateInputText));
    this.subTextInput.onValueChanged.AddListener(new UnityAction<string>(this.ValidateInputText));
  }

  private void LoadTeamName()
  {
    this.mainTextInput.text = CreateTeamManager.teamFirstName;
    this.subTextInput.text = CreateTeamManager.teamLastName;
    this.teamCreateLogo.SetName(this.mainTextInput.text, this.subTextInput.text);
  }

  private void SetContinents()
  {
    this.continentDropdown.ClearOptions();
    int length = Enum.GetValues(typeof (Nationality.Continent)).Length;
    Nationality.Continent continent = CreateTeamManager.newTeam.nationality.continent;
    for (int index = 0; index < length; ++index)
    {
      Nationality.Continent inContinent = (Nationality.Continent) index;
      this.continentDropdown.get_options().Add(new Dropdown.OptionData()
      {
        text = Localisation.LocaliseID(Nationality.GetContinentStringID(inContinent), (GameObject) null).ToUpper()
      });
      if (inContinent == continent)
        this.continentDropdown.value = index;
    }
    this.continentDropdown.RefreshShownValue();
  }

  private void SetNationalityGrid()
  {
    Nationality.Continent inContinent = (Nationality.Continent) this.continentDropdown.value;
    List<Nationality> nationalitiesForContinent = App.instance.nationalityManager.GetNationalitiesForContinent(inContinent);
    Nationality nationality = CreateTeamManager.newTeam.nationality;
    RectTransform target = (RectTransform) null;
    nationalitiesForContinent.Sort();
    this.nationalityToggleGroup.SetAllTogglesOff();
    this.nationalityGrid.DestroyListItems();
    int count = nationalitiesForContinent.Count;
    for (int inIndex = 0; inIndex < count; ++inIndex)
    {
      Nationality inNationality = nationalitiesForContinent[inIndex];
      UICreatePlayerNationalityEntry nationalityEntry = this.nationalityGrid.GetOrCreateItem<UICreatePlayerNationalityEntry>(inIndex);
      nationalityEntry.Setup(inNationality);
      nationalityEntry.toggle.isOn = nationality.continent == inContinent ? nationality.localisedCountry == inNationality.localisedCountry : inIndex == 0;
      if (nationalityEntry.toggle.isOn)
        target = nationalityEntry.GetComponent<RectTransform>();
    }
    GameUtility.SetActive(this.nationalityGrid.itemPrefab, false);
    if (!((UnityEngine.Object) target != (UnityEngine.Object) null))
      return;
    GameUtility.SnapScrollRectTo(target, this.nationalityScrollRect, GameUtility.AnchorType.Y, GameUtility.AnchorLocation.Top);
  }

  public void SelectCountry(Nationality inNationality)
  {
    if (!(inNationality != (Nationality) null))
      return;
    this.PlayMenuSound();
    CreateTeamManager.SetTeamNationality(inNationality);
    this.RefreshCountry(inNationality);
  }

  private void RefreshCountry(Nationality inNationality)
  {
    this.teamNationality.SetNationality(inNationality);
    this.teamNationalityName.text = inNationality.localisedCountry;
  }

  public void UpdateTeamPortraits()
  {
    Person defaultPerson1 = CreateTeamManager.defaultPersons[0];
    Person defaultPerson2 = CreateTeamManager.defaultPersons[1];
    Person defaultPerson3 = CreateTeamManager.defaultPersons[2];
    this.driver.SetPortrait(defaultPerson1.portrait, defaultPerson1.gender, 25, -1, CreateTeamManager.newTeamColor, UICharacterPortraitBody.BodyType.Driver, CreateTeamManager.newTeam.driversHatStyle, CreateTeamManager.newTeam.driversBodyStyle);
    this.engineer.SetPortrait(defaultPerson2.portrait, defaultPerson2.gender, 25, -1, CreateTeamManager.newTeamColor, UICharacterPortraitBody.BodyType.Engineer, -1, CreateTeamManager.newTeam.driversBodyStyle);
    this.mechanic.SetPortrait(defaultPerson3.portrait, defaultPerson3.gender, 25, -1, CreateTeamManager.newTeamColor, UICharacterPortraitBody.BodyType.Mechanic, -1, CreateTeamManager.newTeam.driversBodyStyle);
  }

  private void UpdateColors()
  {
    this.primaryColor.color = CreateTeamManager.newTeamColor.staffColor.primary;
    this.secondaryColor.color = CreateTeamManager.newTeamColor.staffColor.secondary;
  }

  private void ValidateInputText(string inString)
  {
    CreateTeamManager.SetTeamName(this.mainTextInput.text, this.subTextInput.text);
    this.teamCreateLogo.SetName(this.mainTextInput.text, this.subTextInput.text);
  }

  private void OnLeftButtonHatStyle()
  {
    --CreateTeamManager.newTeam.driversHatStyle;
    this.UpdateButtonHatStyleState();
    this.UpdateTeamPortraits();
  }

  private void OnRightButtonHatStyle()
  {
    ++CreateTeamManager.newTeam.driversHatStyle;
    this.UpdateButtonHatStyleState();
    this.UpdateTeamPortraits();
  }

  private void UpdateButtonHatStyleState()
  {
    this.leftButtonHatStyle.interactable = CreateTeamManager.newTeam.driversHatStyle > 0;
    this.rightButtonHatStyle.interactable = CreateTeamManager.newTeam.driversHatStyle < Portrait.hatStyles.Length - 1;
    this.UpdateHatStyleText();
  }

  private void UpdateHatStyleText()
  {
    this.hatStyleLabel.text = Portrait.hatStyles[CreateTeamManager.newTeam.driversHatStyle];
  }

  private void OnLeftButtonShirtStyle()
  {
    --CreateTeamManager.newTeam.driversBodyStyle;
    this.UpdateButtonShirtStyleState();
    this.UpdateTeamPortraits();
  }

  private void OnRightButtonShirtStyle()
  {
    ++CreateTeamManager.newTeam.driversBodyStyle;
    this.UpdateButtonShirtStyleState();
    this.UpdateTeamPortraits();
  }

  private void UpdateButtonShirtStyleState()
  {
    this.leftButtonShirtStyle.interactable = CreateTeamManager.newTeam.driversBodyStyle > 0;
    this.rightButtonShirtStyle.interactable = CreateTeamManager.newTeam.driversBodyStyle < Portrait.shirtStyles.Length - 1;
    this.UpdateShirtStyleText();
  }

  private void UpdateShirtStyleText()
  {
    this.shirtStyleLabel.text = Portrait.shirtStyles[CreateTeamManager.newTeam.driversBodyStyle];
  }

  private void OnLeftButtonTeamLogo()
  {
    --CreateTeamManager.newTeam.customLogo.styleID;
    this.UpdateButtonTeamLogoState();
    this.teamCreateLogo.SetStyle();
  }

  private void OnRightButtonTeamLogo()
  {
    ++CreateTeamManager.newTeam.customLogo.styleID;
    this.UpdateButtonTeamLogoState();
    this.teamCreateLogo.SetStyle();
  }

  private void UpdateButtonTeamLogoState()
  {
    this.leftButtonTeamLogo.interactable = CreateTeamManager.newTeam.customLogo.styleID > 0;
    this.rightButtonTeamLogo.interactable = CreateTeamManager.newTeam.customLogo.styleID < UITeamCreateLogo.logos.Length - 1;
    this.UpdateTeamLogoText();
  }

  private void UpdateTeamLogoText()
  {
    this.teamLogoLabel.text = UITeamCreateLogo.logos[CreateTeamManager.newTeam.customLogo.styleID];
  }

  public void PlayMenuSound()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_AppearanceChange, 0.0f);
  }

  private void OnPrimaryColorToggle(bool inValue)
  {
    if (inValue)
    {
      ColorPickerDialogBox.Open(this.primaryColorRect, CreateTeamManager.newTeamColor.staffColor.primary, (Color[]) null, false);
      ColorPickerDialogBox.OnColorPicked += new Action<Color>(this.OnPrimaryColorPicked);
      ColorPickerDialogBox.OnClose += new Action(this.OnPrimaryColorClose);
    }
    else
      ColorPickerDialogBox.Close();
  }

  private void OnSecondaryColorToggle(bool inValue)
  {
    if (inValue)
    {
      ColorPickerDialogBox.Open(this.secondaryColorRect, CreateTeamManager.newTeamColor.staffColor.secondary, (Color[]) null, false);
      ColorPickerDialogBox.OnColorPicked += new Action<Color>(this.OnSecondaryColorPicked);
      ColorPickerDialogBox.OnClose += new Action(this.OnSecondaryColorClose);
    }
    else
      ColorPickerDialogBox.Close();
  }

  private void OnPrimaryColorPicked(Color inColor)
  {
    CreateTeamManager.SetStaffColors(inColor, CreateTeamManager.newTeamColor.staffColor.secondary);
    this.UpdateColors();
    this.UpdateTeamPortraits();
    this.teamCreateLogo.RefreshColors();
  }

  private void OnPrimaryColorClose()
  {
    this.primaryColorToggle.isOn = false;
  }

  private void OnSecondaryColorPicked(Color inColor)
  {
    CreateTeamManager.SetStaffColors(CreateTeamManager.newTeamColor.staffColor.primary, inColor);
    this.UpdateColors();
    this.UpdateTeamPortraits();
    this.teamCreateLogo.RefreshColors();
  }

  private void OnSecondaryColorClose()
  {
    this.secondaryColorToggle.isOn = false;
  }
}
