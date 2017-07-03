// Decompiled with JetBrains decompiler
// Type: UICharacterToolEdit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICharacterToolEdit : MonoBehaviour
{
  public UICharacterToolPortrait portrait;
  public Dropdown continent;
  public Dropdown nationality;
  public InputField firstName;
  public InputField lastName;
  public Button saveButton;
  public Button closeButton;
  public CharacterCreatorToolScreen screen;
  private List<Nationality> mEurope;
  private List<Nationality> mAfrica;
  private List<Nationality> mAsia;
  private List<Nationality> mNorthAmerica;
  private List<Nationality> mOceania;
  private List<Nationality> mSouthAmerica;
  private Person mPerson;

  public void OnStart()
  {
    this.continent.ClearOptions();
    this.mEurope = App.instance.nationalityManager.GetNationalitiesForContinent(Nationality.Continent.Europe);
    this.mAfrica = App.instance.nationalityManager.GetNationalitiesForContinent(Nationality.Continent.Africa);
    this.mAsia = App.instance.nationalityManager.GetNationalitiesForContinent(Nationality.Continent.Asia);
    this.mNorthAmerica = App.instance.nationalityManager.GetNationalitiesForContinent(Nationality.Continent.NorthAmerica);
    this.mOceania = App.instance.nationalityManager.GetNationalitiesForContinent(Nationality.Continent.Oceania);
    this.mSouthAmerica = App.instance.nationalityManager.GetNationalitiesForContinent(Nationality.Continent.SouthAmerica);
    for (int index = 0; index < 6; ++index)
    {
      Dropdown.OptionData optionData = new Dropdown.OptionData();
      Nationality.Continent continent = (Nationality.Continent) index;
      optionData.text = continent.ToString();
      this.continent.get_options().Add(optionData);
    }
    this.continent.onValueChanged.AddListener((UnityAction<int>) (param0 => this.SetContinent()));
    this.nationality.onValueChanged.AddListener((UnityAction<int>) (param0 => this.SetNationality()));
    this.firstName.onValueChanged.AddListener((UnityAction<string>) (param0 => this.SetName()));
    this.lastName.onValueChanged.AddListener((UnityAction<string>) (param0 => this.SetName()));
    this.saveButton.onClick.AddListener(new UnityAction(this.Save));
    this.closeButton.onClick.AddListener(new UnityAction(this.Hide));
    this.Hide();
  }

  public void Setup(Person inPerson)
  {
    if (inPerson == null)
      return;
    this.mPerson = inPerson;
    this.portrait.Setup(this.mPerson);
    this.SelectNationality((int) this.mPerson.nationality.continent);
    this.firstName.text = this.mPerson.firstName;
    this.lastName.text = this.mPerson.lastName;
    this.Show();
  }

  public void SelectNationality(int inValue)
  {
    this.continent.value = inValue;
    this.continent.RefreshShownValue();
    Nationality.Continent continent = (Nationality.Continent) inValue;
    List<Nationality> nationalityList = new List<Nationality>();
    switch (continent)
    {
      case Nationality.Continent.Africa:
        nationalityList = this.mAfrica;
        break;
      case Nationality.Continent.Asia:
        nationalityList = this.mAsia;
        break;
      case Nationality.Continent.Europe:
        nationalityList = this.mEurope;
        break;
      case Nationality.Continent.NorthAmerica:
        nationalityList = this.mNorthAmerica;
        break;
      case Nationality.Continent.Oceania:
        nationalityList = this.mOceania;
        break;
      case Nationality.Continent.SouthAmerica:
        nationalityList = this.mSouthAmerica;
        break;
    }
    this.nationality.ClearOptions();
    int count = nationalityList.Count;
    int num = 0;
    for (int index = 0; index < count; ++index)
    {
      Dropdown.OptionData optionData = new Dropdown.OptionData();
      string localisedCountry = nationalityList[index].localisedCountry;
      optionData.text = localisedCountry;
      if (this.mPerson != null && localisedCountry == this.mPerson.nationality.localisedCountry)
        num = index;
      this.nationality.get_options().Add(optionData);
    }
    this.nationality.value = num;
    this.nationality.RefreshShownValue();
  }

  public void SetContinent()
  {
    this.SelectNationality(this.continent.value);
  }

  public void SetNationality()
  {
    Nationality nationality = new Nationality();
    if (this.nationality.value >= this.nationality.get_options().Count)
      return;
    this.portrait.flag.SetNationality(Nationality.GetNationalityByName(this.nationality.get_options()[this.nationality.value].text));
  }

  public void SetName()
  {
    if (string.IsNullOrEmpty(this.firstName.text) || string.IsNullOrEmpty(this.lastName.text))
      return;
    this.portrait.staffName.text = ((int) this.firstName.text[0]).ToString() + ". " + this.lastName.text;
  }

  private void Save()
  {
    Nationality nationality = new Nationality();
    this.mPerson.nationality = Nationality.GetNationalityByName(this.nationality.get_options()[this.nationality.value].text);
    if (!string.IsNullOrEmpty(this.firstName.text) && !string.IsNullOrEmpty(this.lastName.text))
      this.mPerson.SetName(this.firstName.text, this.lastName.text);
    this.screen.profilesWidget.TriggerOnSelection();
    this.screen.profilesWidget.selectedEntry.SetDetails();
    this.Hide();
  }

  public void Show()
  {
    this.gameObject.SetActive(true);
  }

  public void Hide()
  {
    this.gameObject.SetActive(false);
  }
}
