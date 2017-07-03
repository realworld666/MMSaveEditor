// Decompiled with JetBrains decompiler
// Type: UICharacterToolProfiles
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICharacterToolProfiles : MonoBehaviour
{
  private List<Person> mStaffList = new List<Person>();
  private List<UICharacterToolEntry> mEntries = new List<UICharacterToolEntry>();
  private List<UICharacterToolEntry> mSelectedEntries = new List<UICharacterToolEntry>();
  public Button defaultSelected;
  public Button randomSelected;
  public Button selectAll;
  public Button deselectAll;
  public Dropdown filterChampionships;
  public Dropdown filterGender;
  public GameObject itemDriver;
  public GameObject itemStaff;
  public UIGridList grid;
  public UICharacterToolProfiles.Gender selectedGender;
  public CharacterCreatorToolScreen screen;
  public Action OnSelection;
  private int mFemaleSelected;
  private int mMaleSelected;

  public int selectedNum
  {
    get
    {
      return this.mSelectedEntries.Count;
    }
  }

  public Person selectedPerson
  {
    get
    {
      return this.mSelectedEntries[0].person;
    }
  }

  public UICharacterToolEntry selectedEntry
  {
    get
    {
      return this.mSelectedEntries[0];
    }
  }

  public List<UICharacterToolEntry> selectedPeople
  {
    get
    {
      return this.mSelectedEntries;
    }
  }

  public void OnStart()
  {
    this.itemDriver.SetActive(false);
    this.itemStaff.SetActive(false);
    this.selectAll.onClick.AddListener(new UnityAction(this.OnSelectAll));
    this.deselectAll.onClick.AddListener(new UnityAction(this.OnDeselectAll));
    this.defaultSelected.onClick.AddListener(new UnityAction(this.OnDefaultAll));
    this.randomSelected.onClick.AddListener(new UnityAction(this.OnRandomAll));
    this.filterChampionships.onValueChanged.AddListener((UnityAction<int>) (param0 => this.OnFilter()));
    this.filterGender.onValueChanged.AddListener((UnityAction<int>) (param0 => this.OnFilter()));
  }

  public void Setup()
  {
    this.SetChampionshipsDD();
    this.SetGrid();
    this.UpdateButtonsState();
  }

  public void ClearGrid()
  {
    this.mEntries.Clear();
    this.mSelectedEntries.Clear();
    this.mFemaleSelected = 0;
    this.mMaleSelected = 0;
    this.grid.DestroyListItems();
    this.UpdateButtonsState();
    this.UpdateGenderState();
    this.TriggerOnSelection();
  }

  public void SetGrid()
  {
    this.ClearGrid();
    this.itemDriver.SetActive(true);
    this.itemStaff.SetActive(true);
    int count = this.mStaffList.Count;
    for (int index = 0; index < count; ++index)
    {
      Person mStaff = this.mStaffList[index];
      this.grid.itemPrefab = !(mStaff is Driver) ? this.itemStaff : this.itemDriver;
      UICharacterToolEntry listItem = this.grid.CreateListItem<UICharacterToolEntry>();
      listItem.OnStart();
      listItem.Setup(mStaff);
      this.mEntries.Add(listItem);
    }
    this.itemDriver.SetActive(false);
    this.itemStaff.SetActive(false);
    this.ApplyFilters();
    this.UpdateButtonsState();
    this.UpdateGenderState();
    this.TriggerOnSelection();
  }

  public void LoadDB(Person inPerson)
  {
    if (inPerson is Chairman)
      this.mStaffList = Game.instance.chairmanManager.GetEntityList().ConvertAll<Person>((Converter<Chairman, Person>) (x => (Person) x));
    else if (inPerson is TeamPrincipal)
      this.mStaffList = Game.instance.teamPrincipalManager.GetEntityList().ConvertAll<Person>((Converter<TeamPrincipal, Person>) (x => (Person) x));
    else if (inPerson is Driver)
      this.mStaffList = Game.instance.driverManager.GetEntityList().ConvertAll<Person>((Converter<Driver, Person>) (x => (Person) x));
    else if (inPerson is Mechanic)
      this.mStaffList = Game.instance.mechanicManager.GetEntityList().ConvertAll<Person>((Converter<Mechanic, Person>) (x => (Person) x));
    else if (inPerson is Engineer)
      this.mStaffList = Game.instance.engineerManager.GetEntityList().ConvertAll<Person>((Converter<Engineer, Person>) (x => (Person) x));
    else if (inPerson is Celebrity)
      this.mStaffList = Game.instance.celebrityManager.GetEntityList().ConvertAll<Person>((Converter<Celebrity, Person>) (x => (Person) x));
    else if (inPerson is Assistant)
      this.mStaffList = Game.instance.assistantManager.GetEntityList().ConvertAll<Person>((Converter<Assistant, Person>) (x => (Person) x));
    else if (inPerson is Scout)
      this.mStaffList = Game.instance.scoutManager.GetEntityList().ConvertAll<Person>((Converter<Scout, Person>) (x => (Person) x));
    int count = this.mStaffList.Count;
    for (int index = 0; index < count; ++index)
    {
      if (this.mStaffList[index].IsReplacementPerson())
      {
        this.mStaffList.RemoveAt(index);
        --index;
        --count;
      }
    }
    this.SetGrid();
  }

  public void LoadDB(Contract.Job inJob)
  {
    if (inJob == Contract.Job.Journalist)
      this.mStaffList = Game.instance.mediaManager.GetEntityList();
    else if (inJob == Contract.Job.SponsorLiasion)
    {
      List<Sponsor> entityList = Game.instance.sponsorManager.GetEntityList();
      int count = entityList.Count;
      for (int index = 0; index < count; ++index)
        this.mStaffList.Add(entityList[index].liaison);
    }
    this.SetGrid();
  }

  public void LoadPerson(Person inPerson)
  {
    this.mStaffList = new List<Person>();
    Person person = inPerson;
    person.SetName("New", "Person");
    person.dateOfBirth = new DateTime(1990, 1, 1);
    person.nationality = Nationality.GetNationalityByName("UK");
    this.mStaffList.Add(person);
    this.SetGrid();
  }

  public void LoadPerson(Contract.Job inJob)
  {
    this.mStaffList = new List<Person>();
    Person person = new Person();
    person.SetName("New", "Person");
    person.dateOfBirth = new DateTime(1990, 1, 1);
    person.nationality = Nationality.GetNationalityByName("UK");
    this.mStaffList.Add(person);
    this.SetGrid();
  }

  public void ToggleItem(UICharacterToolEntry inEntry, bool inToggle)
  {
    UICharacterToolEntry characterToolEntry = this.GetItem(inEntry);
    UICharacterToolProfiles.Gender inGender = (UICharacterToolProfiles.Gender) (inEntry.person.gender + 1);
    if (inToggle)
    {
      if ((UnityEngine.Object) characterToolEntry == (UnityEngine.Object) null)
      {
        this.mSelectedEntries.Add(inEntry);
        this.SetGender(inGender, true);
      }
    }
    else if ((UnityEngine.Object) characterToolEntry != (UnityEngine.Object) null)
    {
      this.mSelectedEntries.RemoveAt(this.mSelectedEntries.IndexOf(inEntry));
      this.SetGender(inGender, false);
    }
    this.UpdateButtonsState();
    this.TriggerOnSelection();
  }

  public UICharacterToolEntry GetItem(UICharacterToolEntry inEntry)
  {
    int count = this.mSelectedEntries.Count;
    for (int index = 0; index < count; ++index)
    {
      if ((UnityEngine.Object) this.mSelectedEntries[index] == (UnityEngine.Object) inEntry)
        return this.mSelectedEntries[index];
    }
    return (UICharacterToolEntry) null;
  }

  public void RemoveSelection(UICharacterToolEntry inEntry)
  {
    int count = this.mSelectedEntries.Count;
    for (int index = 0; index < count; ++index)
    {
      UICharacterToolEntry mSelectedEntry = this.mSelectedEntries[index];
      if ((UnityEngine.Object) mSelectedEntry == (UnityEngine.Object) inEntry && mSelectedEntry.selected)
      {
        mSelectedEntry.toggle.isOn = false;
        break;
      }
    }
  }

  private void OnSelectAll()
  {
    int count = this.mEntries.Count;
    for (int index = 0; index < count; ++index)
    {
      UICharacterToolEntry mEntry = this.mEntries[index];
      if (mEntry.gameObject.activeSelf && !mEntry.selected)
        mEntry.toggle.isOn = true;
    }
  }

  private void OnDeselectAll()
  {
    int count = this.mEntries.Count;
    for (int index = 0; index < count; ++index)
    {
      UICharacterToolEntry mEntry = this.mEntries[index];
      if (mEntry.gameObject.activeSelf && mEntry.selected)
        mEntry.toggle.isOn = false;
    }
  }

  private void OnRandomAll()
  {
    int count = this.mSelectedEntries.Count;
    bool useRegenRandom = this.screen.menuWidget.useRegenRandom;
    for (int index = 0; index < count; ++index)
    {
      UICharacterToolEntry mSelectedEntry = this.mSelectedEntries[index];
      if (useRegenRandom)
        mSelectedEntry.person.portrait.GeneratePortrait(mSelectedEntry.person);
      else
        mSelectedEntry.person.portrait.GenerateRandomPortrait(mSelectedEntry.person.gender, mSelectedEntry.person is Driver);
      mSelectedEntry.UpdatePortrait();
    }
    this.UpdateButtonsState();
    if (count != 1)
      return;
    this.screen.customizeWidget.portrait.UpdatePortrait();
  }

  private void OnDefaultAll()
  {
    int count = this.mSelectedEntries.Count;
    for (int index = 0; index < count; ++index)
      this.mSelectedEntries[index].LoadDefaults();
    this.ApplyFilters();
    this.UpdateButtonsState();
    this.UpdateGenderState();
    this.TriggerOnSelection();
  }

  public void UpdateSelectedPortraits()
  {
    int count = this.mSelectedEntries.Count;
    for (int index = 0; index < count; ++index)
      this.mSelectedEntries[index].UpdatePortrait();
  }

  public void SetTraitSelected(UICharacterToolTrait.Trait inTrait, int inNum)
  {
    int count = this.mSelectedEntries.Count;
    for (int index = 0; index < count; ++index)
    {
      UICharacterToolEntry mSelectedEntry = this.mSelectedEntries[index];
      switch (inTrait)
      {
        case UICharacterToolTrait.Trait.SkinColor:
          mSelectedEntry.person.portrait.head = inNum;
          break;
        case UICharacterToolTrait.Trait.HairColor:
          mSelectedEntry.person.portrait.hairColor = inNum;
          break;
        case UICharacterToolTrait.Trait.HairStyle:
          mSelectedEntry.person.portrait.hair = inNum;
          break;
        case UICharacterToolTrait.Trait.Glasses:
          mSelectedEntry.person.portrait.glasses = inNum;
          break;
        case UICharacterToolTrait.Trait.Accessories:
          mSelectedEntry.person.portrait.accessory = inNum;
          break;
        case UICharacterToolTrait.Trait.FacialHair:
          mSelectedEntry.person.portrait.facialHair = inNum;
          break;
      }
      mSelectedEntry.UpdatePortrait();
    }
    if (count != 1)
      return;
    this.screen.customizeWidget.portrait.UpdatePortrait();
  }

  public void SetGenderSelected(Person.Gender inGender, bool inSetDefault)
  {
    int count = this.mSelectedEntries.Count;
    for (int index = 0; index < count; ++index)
    {
      UICharacterToolEntry mSelectedEntry = this.mSelectedEntries[index];
      if (inSetDefault)
        mSelectedEntry.SetDefaultGender();
      else
        mSelectedEntry.SetGender(inGender);
      mSelectedEntry.UpdatePortrait();
    }
    this.ApplyFilters();
    this.UpdateButtonsState();
    this.UpdateGenderState();
    this.TriggerOnSelection();
  }

  public void SetAgeSelected(int inAge, bool inSetDefault)
  {
    int count = this.mSelectedEntries.Count;
    for (int index = 0; index < count; ++index)
    {
      UICharacterToolEntry mSelectedEntry = this.mSelectedEntries[index];
      if (inSetDefault)
        mSelectedEntry.SetDefaultAge();
      else
        mSelectedEntry.SetAge(inAge);
      mSelectedEntry.UpdatePortrait();
    }
  }

  private void SetGender(UICharacterToolProfiles.Gender inGender, bool inIncrease)
  {
    if (inGender == UICharacterToolProfiles.Gender.Male)
    {
      if (inIncrease)
        ++this.mMaleSelected;
      else
        --this.mMaleSelected;
    }
    else if (inIncrease)
      ++this.mFemaleSelected;
    else
      --this.mFemaleSelected;
    this.UpdateGenderState();
  }

  private void UpdateGenderState()
  {
    if (this.mMaleSelected > 0 && this.mFemaleSelected == 0)
      this.selectedGender = UICharacterToolProfiles.Gender.Male;
    else if (this.mMaleSelected == 0 && this.mFemaleSelected > 0)
      this.selectedGender = UICharacterToolProfiles.Gender.Female;
    else
      this.selectedGender = UICharacterToolProfiles.Gender.Both;
  }

  private void UpdateButtonsState()
  {
    int count1 = this.mSelectedEntries.Count;
    int count2 = this.mEntries.Count;
    this.selectAll.interactable = count1 < count2;
    this.deselectAll.interactable = count1 > 0;
    this.randomSelected.interactable = count1 > 0;
    this.defaultSelected.interactable = count1 > 0;
  }

  public void TriggerOnSelection()
  {
    if (this.OnSelection == null)
      return;
    this.OnSelection();
  }

  private void SetChampionshipsDD()
  {
    this.filterChampionships.get_options().Clear();
    this.filterChampionships.get_options().Add(new Dropdown.OptionData("ALL"));
    this.filterChampionships.get_options().Add(new Dropdown.OptionData("FREE AGENTS"));
    List<Championship> entityList = Game.instance.championshipManager.GetEntityList();
    int count = entityList.Count;
    for (int index = 0; index < count; ++index)
      this.filterChampionships.get_options().Add(new Dropdown.OptionData()
      {
        text = entityList[index].GetChampionshipName(false)
      });
  }

  private void OnFilter()
  {
    if (this.mEntries.Count <= 0)
      return;
    this.ApplyFilters();
    this.UpdateButtonsState();
    this.UpdateGenderState();
    this.TriggerOnSelection();
  }

  private void ApplyFilters()
  {
    int count = this.mEntries.Count;
    UICharacterToolProfiles.Gender gender = (UICharacterToolProfiles.Gender) this.filterGender.value;
    int num = this.filterChampionships.value;
    for (int index = 0; index < count; ++index)
    {
      UICharacterToolEntry mEntry = this.mEntries[index];
      Person person = mEntry.person;
      if (person != null)
      {
        bool flag1 = false;
        switch (person.gender)
        {
          case Person.Gender.Male:
            if (gender != UICharacterToolProfiles.Gender.Female)
            {
              flag1 = true;
              break;
            }
            break;
          case Person.Gender.Female:
            if (gender != UICharacterToolProfiles.Gender.Male)
            {
              flag1 = true;
              break;
            }
            break;
        }
        bool flag2 = false;
        if (num == 0)
          flag2 = true;
        else if (num == 1)
        {
          if (person.contract != null && person.contract.GetTeam() == null)
            flag2 = true;
        }
        else if (num >= 2 && person.contract != null)
        {
          Team team = person.contract.GetTeam();
          if (team != null)
          {
            int inIndex = num - 2;
            Championship entity = Game.instance.championshipManager.GetEntity(inIndex);
            if (team.championship == entity)
              flag2 = true;
          }
        }
        bool flag3 = flag1 && flag2;
        mEntry.gameObject.SetActive(flag3);
        if (!flag3 && mEntry.selected)
          this.RemoveSelection(mEntry);
      }
    }
  }

  public enum Gender
  {
    Both,
    Male,
    Female,
  }
}
