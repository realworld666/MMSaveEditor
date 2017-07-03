// Decompiled with JetBrains decompiler
// Type: UICharacterToolEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICharacterToolEntry : MonoBehaviour
{
  private DateTime mDefaultDOB = new DateTime();
  private string mDefaultFirstName = string.Empty;
  private string mDefaultLastName = string.Empty;
  public Toggle toggle;
  public UICharacterPortrait portrait;
  public UITeamLogo teamLogo;
  public Flag flag;
  public TextMeshProUGUI staffName;
  public TextMeshProUGUI genderAge;
  public UICharacterToolProfiles widget;
  private Person mPerson;
  private Person.Gender mDefaultGender;
  private Portrait mDefaultPortrait;
  private Nationality mDefaultNationality;

  public bool selected
  {
    get
    {
      return this.toggle.isOn;
    }
  }

  public Person person
  {
    get
    {
      return this.mPerson;
    }
  }

  public void OnStart()
  {
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle()));
  }

  public void Setup(Person inPerson)
  {
    if (inPerson == null)
      return;
    this.mPerson = inPerson;
    this.SetDefaults();
    this.SetDetails();
    this.UpdatePortrait();
  }

  private void SetDefaults()
  {
    this.mDefaultFirstName = this.mPerson.firstName;
    this.mDefaultLastName = this.mPerson.lastName;
    this.mDefaultGender = this.mPerson.gender;
    this.mDefaultDOB = this.mPerson.dateOfBirth;
    this.mDefaultNationality = new Nationality();
    this.mDefaultNationality = Nationality.GetNationalityByName(this.mPerson.nationality.localisedCountry);
    this.mDefaultPortrait = new Portrait();
    this.mDefaultPortrait.accessory = this.mPerson.portrait.accessory;
    this.mDefaultPortrait.brows = this.mPerson.portrait.brows;
    this.mDefaultPortrait.facialHair = this.mPerson.portrait.facialHair;
    this.mDefaultPortrait.glasses = this.mPerson.portrait.glasses;
    this.mDefaultPortrait.hair = this.mPerson.portrait.hair;
    this.mDefaultPortrait.hairColor = this.mPerson.portrait.hairColor;
    this.mDefaultPortrait.head = this.mPerson.portrait.head;
  }

  public void LoadDefaults()
  {
    this.mPerson.SetName(this.mDefaultFirstName, this.mDefaultLastName);
    this.mPerson.gender = this.mDefaultGender;
    this.mPerson.dateOfBirth = this.mDefaultDOB;
    this.mPerson.nationality = Nationality.GetNationalityByName(this.mDefaultNationality.localisedCountry);
    this.mPerson.portrait.accessory = this.mDefaultPortrait.accessory;
    this.mPerson.portrait.brows = this.mDefaultPortrait.brows;
    this.mPerson.portrait.facialHair = this.mDefaultPortrait.facialHair;
    this.mPerson.portrait.glasses = this.mDefaultPortrait.glasses;
    this.mPerson.portrait.hair = this.mDefaultPortrait.hair;
    this.mPerson.portrait.hairColor = this.mDefaultPortrait.hairColor;
    this.mPerson.portrait.head = this.mDefaultPortrait.head;
    this.SetDetails();
    this.UpdatePortrait();
  }

  public void SetDetails()
  {
    if (this.mPerson == null)
      return;
    Team inTeam = (Team) null;
    if (this.mPerson.contract != null)
      inTeam = this.mPerson.contract.GetTeam();
    this.teamLogo.gameObject.SetActive(inTeam != null);
    if (inTeam != null && this.teamLogo.gameObject.activeSelf)
      this.teamLogo.SetTeam(inTeam);
    this.flag.SetNationality(this.mPerson.nationality);
    this.staffName.text = this.mPerson.shortName;
    this.genderAge.text = this.mPerson.gender.ToString() + " " + this.mPerson.GetAge().ToString();
  }

  public void UpdatePortrait()
  {
    if (this.mPerson == null)
      return;
    this.portrait.SetPortrait(this.mPerson);
  }

  public void SetGender(Person.Gender inGender)
  {
    if (this.mPerson == null)
      return;
    this.mPerson.gender = inGender;
    this.genderAge.text = this.mPerson.gender.ToString() + " " + this.mPerson.GetAge().ToString();
  }

  public void SetDefaultGender()
  {
    if (this.mPerson == null)
      return;
    this.mPerson.gender = this.mDefaultGender;
    this.genderAge.text = this.mPerson.gender.ToString() + " " + this.mPerson.GetAge().ToString();
  }

  public void SetAge(int inAge)
  {
    if (this.mPerson == null)
      return;
    this.mPerson.dateOfBirth = Game.instance.time.now.AddYears(-inAge).AddDays(-30.0);
    this.genderAge.text = this.mPerson.gender.ToString() + " " + this.mPerson.GetAge().ToString();
  }

  public void SetDefaultAge()
  {
    if (this.mPerson == null)
      return;
    this.mPerson.dateOfBirth = this.mDefaultDOB;
    this.genderAge.text = this.mPerson.gender.ToString() + " " + this.mPerson.GetAge().ToString();
  }

  private void OnToggle()
  {
    this.widget.ToggleItem(this, this.toggle.isOn);
  }
}
