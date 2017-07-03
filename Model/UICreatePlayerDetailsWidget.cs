// Decompiled with JetBrains decompiler
// Type: UICreatePlayerDetailsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICreatePlayerDetailsWidget : MonoBehaviour
{
  private bool mAllowMenuSounds = true;
  public TMP_InputField firstNameInput;
  public TMP_InputField lastNameInput;
  public TextMeshProUGUI firstNamePlaceHolder;
  public TextMeshProUGUI lastNamePlaceHolder;
  public Dropdown dobDay;
  public Dropdown dobMonth;
  public Dropdown dobYear;
  public GameObject dobDayTitle;
  public GameObject dobMonthTitle;
  public Dropdown continents;
  public UIGridList countriesGrid;
  public ScrollRect scrollRect;
  public ToggleGroup toggleGroup;
  public CreatePlayerScreen screen;
  public Button leftBackstoryOption;
  public Button rightBackstoryOption;
  public TextMeshProUGUI backstoryName;
  public TextMeshProUGUI backstoryDescription;
  private int mCurrentBackStorySelected;
  private Vector3 mLeftDOBTitlePos;
  private Vector3 mRightDOBTitlePos;
  private Vector3 mLeftoDOBDropdownPos;
  private Vector3 mRightDOBDropdownPos;
  private bool mPositionsCaptured;

  public void Setup()
  {
    this.CaptureDOBGameObjectStartingPositions();
    this.UpdateStringsForLocalisation();
    this.mAllowMenuSounds = false;
    this.dobYear.onValueChanged.RemoveAllListeners();
    this.dobMonth.onValueChanged.RemoveAllListeners();
    this.dobDay.onValueChanged.RemoveAllListeners();
    this.continents.onValueChanged.RemoveAllListeners();
    this.leftBackstoryOption.onClick.RemoveAllListeners();
    this.rightBackstoryOption.onClick.RemoveAllListeners();
    this.firstNamePlaceHolder.text = Localisation.LocaliseID("PSG_10008592", (GameObject) null);
    this.lastNamePlaceHolder.text = Localisation.LocaliseID("PSG_10008593", (GameObject) null);
    this.SetDOBYears();
    this.SetDOBMonths();
    this.SetDOBDays(true);
    this.SelectDateOfBirth();
    this.SetContinents();
    this.SetNationalityGrid();
    this.SelectBackstory();
    switch (App.instance.preferencesManager.GetSettingEnum<PrefGameDateFormat.Type>(Preference.pName.Game_DateFormat, false))
    {
      case PrefGameDateFormat.Type.DDMMYYYY:
        this.dobMonthTitle.transform.localPosition = this.mRightDOBTitlePos;
        this.dobDayTitle.transform.localPosition = this.mLeftDOBTitlePos;
        this.dobDay.transform.localPosition = this.mLeftoDOBDropdownPos;
        this.dobMonth.transform.localPosition = this.mRightDOBDropdownPos;
        break;
      case PrefGameDateFormat.Type.MMDDYYYY:
        this.dobMonthTitle.transform.localPosition = this.mLeftDOBTitlePos;
        this.dobDayTitle.transform.localPosition = this.mRightDOBTitlePos;
        this.dobDay.transform.localPosition = this.mRightDOBDropdownPos;
        this.dobMonth.transform.localPosition = this.mLeftoDOBDropdownPos;
        break;
    }
    this.dobYear.onValueChanged.AddListener((UnityAction<int>) (param0 =>
    {
      this.SetDOBDays(false);
      this.SelectDateOfBirth();
    }));
    this.dobMonth.onValueChanged.AddListener((UnityAction<int>) (param0 =>
    {
      this.SetDOBDays(false);
      this.SelectDateOfBirth();
    }));
    this.dobDay.onValueChanged.AddListener((UnityAction<int>) (param0 => this.SelectDateOfBirth()));
    this.continents.onValueChanged.AddListener((UnityAction<int>) (param0 => this.SetNationalityGrid()));
    this.leftBackstoryOption.onClick.AddListener(new UnityAction(this.OnLeftBackstoryButton));
    this.rightBackstoryOption.onClick.AddListener(new UnityAction(this.OnRightBackstoryButton));
    this.mAllowMenuSounds = true;
  }

  private void CaptureDOBGameObjectStartingPositions()
  {
    if (this.mPositionsCaptured)
      return;
    this.mLeftDOBTitlePos = new Vector3(this.dobDayTitle.transform.localPosition.x, this.dobDayTitle.transform.localPosition.y, this.dobDayTitle.transform.localPosition.z);
    this.mRightDOBTitlePos = new Vector3(this.dobMonthTitle.transform.localPosition.x, this.dobMonthTitle.transform.localPosition.y, this.dobMonthTitle.transform.localPosition.z);
    this.mLeftoDOBDropdownPos = new Vector3(this.dobDay.gameObject.transform.localPosition.x, this.dobDay.gameObject.transform.localPosition.y, this.dobDay.gameObject.transform.localPosition.z);
    this.mRightDOBDropdownPos = new Vector3(this.dobMonth.gameObject.transform.localPosition.x, this.dobMonth.gameObject.transform.localPosition.y, this.dobMonth.gameObject.transform.localPosition.z);
    this.mPositionsCaptured = true;
  }

  private void SetDOBDays(bool inForce = false)
  {
    int num1 = DateTime.DaysInMonth(int.Parse(this.dobYear.get_options()[this.dobYear.value].text), this.dobMonth.value + 1);
    int num2 = Mathf.Clamp(Game.instance.player.dateOfBirth.Day - 1, 0, num1 - 1);
    if (num1 == this.dobDay.get_options().Count && !inForce)
      return;
    this.dobDay.ClearOptions();
    for (int index = 0; index < num1; ++index)
      this.dobDay.get_options().Add(new Dropdown.OptionData()
      {
        text = (index + 1).ToString("D2")
      });
    this.dobDay.value = num2;
    this.dobDay.RefreshShownValue();
  }

  private void SetDOBMonths()
  {
    this.dobMonth.ClearOptions();
    for (int index = 0; index < 12; ++index)
      this.dobMonth.get_options().Add(new Dropdown.OptionData()
      {
        text = (index + 1).ToString("D2")
      });
    this.dobMonth.value = Game.instance.player.dateOfBirth.Month - 1;
    this.dobMonth.RefreshShownValue();
  }

  private void SetDOBYears()
  {
    this.dobYear.ClearOptions();
    int num1 = 0;
    int num2 = 1996;
    while (num2 >= 1930)
    {
      this.dobYear.get_options().Add(new Dropdown.OptionData()
      {
        text = num2.ToString()
      });
      if (num2 == Game.instance.player.dateOfBirth.Year)
        this.dobYear.value = num1;
      --num2;
      ++num1;
    }
    this.dobYear.RefreshShownValue();
  }

  private void SetContinents()
  {
    this.continents.ClearOptions();
    int length = Enum.GetValues(typeof (Nationality.Continent)).Length;
    Nationality.Continent continent = Game.instance.player.nationality.continent;
    for (int index = 0; index < length; ++index)
    {
      Nationality.Continent inContinent = (Nationality.Continent) index;
      this.continents.get_options().Add(new Dropdown.OptionData()
      {
        text = Localisation.LocaliseID(Nationality.GetContinentStringID(inContinent), (GameObject) null).ToUpper()
      });
      if (inContinent == continent)
        this.continents.value = index;
    }
    this.continents.RefreshShownValue();
  }

  private void SetNationalityGrid()
  {
    Nationality.Continent inContinent = (Nationality.Continent) this.continents.value;
    List<Nationality> nationalitiesForContinent = App.instance.nationalityManager.GetNationalitiesForContinent(inContinent);
    Nationality nationality = Game.instance.player.nationality;
    RectTransform target = (RectTransform) null;
    nationalitiesForContinent.Sort();
    this.toggleGroup.SetAllTogglesOff();
    this.countriesGrid.DestroyListItems();
    int count = nationalitiesForContinent.Count;
    for (int inIndex = 0; inIndex < count; ++inIndex)
    {
      Nationality inNationality = nationalitiesForContinent[inIndex];
      UICreatePlayerNationalityEntry nationalityEntry = this.countriesGrid.GetOrCreateItem<UICreatePlayerNationalityEntry>(inIndex);
      nationalityEntry.Setup(inNationality);
      nationalityEntry.toggle.isOn = nationality.continent == inContinent ? nationality.localisedCountry == inNationality.localisedCountry : inIndex == 0;
      if (nationalityEntry.toggle.isOn)
        target = nationalityEntry.GetComponent<RectTransform>();
    }
    GameUtility.SetActive(this.countriesGrid.itemPrefab, false);
    if (!((UnityEngine.Object) target != (UnityEngine.Object) null))
      return;
    GameUtility.SnapScrollRectTo(target, this.scrollRect, GameUtility.AnchorType.Y, GameUtility.AnchorLocation.Top);
  }

  public void SetName()
  {
    this.firstNameInput.text = Game.instance.player.firstName;
    this.lastNameInput.text = Game.instance.player.lastName;
  }

  private void SelectDateOfBirth()
  {
    this.PlayMenuSound();
    int year = int.Parse(this.dobYear.get_options()[this.dobYear.value].text);
    int month = this.dobMonth.value + 1;
    int day = this.dobDay.value + 1;
    Game.instance.player.dateOfBirth = new DateTime(year, month, day);
    this.screen.profileWidget.RefreshPortrait();
  }

  public void SelectCountry(Nationality inNationality)
  {
    if (!(inNationality != (Nationality) null))
      return;
    this.PlayMenuSound();
    Game.instance.player.nationality = inNationality;
    this.screen.profileWidget.RefreshWidget();
  }

  private void OnLeftBackstoryButton()
  {
    this.UpdateStringsForLocalisation();
    --this.mCurrentBackStorySelected;
    if (this.mCurrentBackStorySelected < 0)
      this.mCurrentBackStorySelected = 5 - 1;
    scSoundManager.Instance.PlaySound(SoundID.Button_AppearanceChange, 0.0f);
    this.SelectBackstory();
  }

  private void OnRightBackstoryButton()
  {
    this.UpdateStringsForLocalisation();
    this.mCurrentBackStorySelected = (this.mCurrentBackStorySelected + 1) % 5;
    scSoundManager.Instance.PlaySound(SoundID.Button_AppearanceChange, 0.0f);
    this.SelectBackstory();
  }

  private void SelectBackstory()
  {
    Game.instance.player.SetPlayerBackStoryType((PlayerBackStory.PlayerBackStoryType) this.mCurrentBackStorySelected);
    this.backstoryName.text = Game.instance.player.playerBackStoryString;
    this.backstoryDescription.text = Game.instance.player.playerBackStoryDescriptionString;
  }

  public void PlayMenuSound()
  {
    if (!this.mAllowMenuSounds)
      return;
    scSoundManager.Instance.PlaySound(SoundID.Button_AppearanceChange, 0.0f);
  }

  public void UpdateStringsForLocalisation()
  {
    CreatePlayerScreen screen = UIManager.instance.GetScreen<CreatePlayerScreen>();
    StringVariableParser.subject = new Person();
    StringVariableParser.subject.gender = screen.playerGender;
    StringVariableParser.subject.nationality = Game.instance.player.nationality;
    StringVariableParser.nationalityGender = screen.playerGender.ToString();
    this.SelectBackstory();
  }
}
