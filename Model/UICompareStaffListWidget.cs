// Decompiled with JetBrains decompiler
// Type: UICompareStaffListWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICompareStaffListWidget : MonoBehaviour
{
  public List<UICompareStaffListWidget.ListItemGroup> groups = new List<UICompareStaffListWidget.ListItemGroup>();
  private List<Championship> mChampionships = new List<Championship>();
  private List<Driver> mReturnDrivers = new List<Driver>();
  private List<Engineer> mReturnEngineers = new List<Engineer>();
  private List<Mechanic> mReturnMechanics = new List<Mechanic>();
  private List<UICompareStaffItemEntry> mStaffItemEntries = new List<UICompareStaffItemEntry>();
  public ToggleGroup toggleGroup;
  public Toggle[] toggles;
  public TextMeshProUGUI[] toggleLabels;
  public GameObject baseGameTogglesContainer;
  public TextMeshProUGUI[] singleSeaterText;
  public Toggle singleSeaterSeriesToggle;
  public TextMeshProUGUI[] GTSeriesText;
  public Toggle GTSeriesToggle;
  public Toggle unemployedToggle;
  public Toggle favouritesToggle;
  public Button championshipLeftButton;
  public Button championshipRightButton;
  public GameObject GTTogglesContainer;
  public TextMeshProUGUI championshipName;
  public GameObject seriesSelectContainer;
  public UIGridList headersGrid;
  public UIGridList listGrid;
  public TextMeshProUGUI title;
  public CompareStaffScreen screen;
  private Championship.Series mSelectedSeries;
  private Championship mSelectedChampionship;
  private UICompareStaffListWidget.Mode mMode;

  public void Setup(Championship inChampionship)
  {
    this.mChampionships = Game.instance.championshipManager.GetEntityList();
    this.mSelectedChampionship = inChampionship != null ? inChampionship : Game.instance.championshipManager.GetMainChampionship(Championship.Series.SingleSeaterSeries);
    this.OnSeriesChangeToggle(true, this.mSelectedChampionship.series);
    int count = this.mChampionships.Count;
    for (int index = 0; index < this.toggles.Length - 1; ++index)
    {
      if (index < count)
        this.toggleLabels[index].text = this.mChampionships[index].GetAcronym(false);
    }
    this.toggleGroup.SetAllTogglesOff();
    for (int index = 0; index < this.toggles.Length; ++index)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UICompareStaffListWidget.\u003CSetup\u003Ec__AnonStorey78 setupCAnonStorey78 = new UICompareStaffListWidget.\u003CSetup\u003Ec__AnonStorey78();
      // ISSUE: reference to a compiler-generated field
      setupCAnonStorey78.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      setupCAnonStorey78.toggleIndex = index;
      this.toggles[index].onValueChanged.RemoveAllListeners();
      this.toggles[index].isOn = index == this.mSelectedChampionship.championshipOrder;
      // ISSUE: reference to a compiler-generated method
      this.toggles[index].onValueChanged.AddListener(new UnityAction<bool>(setupCAnonStorey78.\u003C\u003Em__144));
    }
    this.SetupGTToggles();
    this.SetTitle();
    GameUtility.SetActive(this.baseGameTogglesContainer, Game.instance.championshipManager.GetMainChampionship(Championship.Series.GTSeries) == null);
    GameUtility.SetActive(this.GTTogglesContainer, !this.baseGameTogglesContainer.activeSelf);
  }

  private void SetupGTToggles()
  {
    for (int index = 0; index < this.singleSeaterText.Length; ++index)
      this.singleSeaterText[index].text = Localisation.LocaliseEnum((Enum) Championship.Series.SingleSeaterSeries);
    for (int index = 0; index < this.GTSeriesText.Length; ++index)
      this.GTSeriesText[index].text = Localisation.LocaliseEnum((Enum) Championship.Series.GTSeries);
    this.singleSeaterSeriesToggle.onValueChanged.RemoveAllListeners();
    this.GTSeriesToggle.onValueChanged.RemoveAllListeners();
    this.unemployedToggle.onValueChanged.RemoveAllListeners();
    this.favouritesToggle.onValueChanged.RemoveAllListeners();
    this.singleSeaterSeriesToggle.isOn = this.mSelectedChampionship.series == Championship.Series.SingleSeaterSeries;
    this.GTSeriesToggle.isOn = this.mSelectedChampionship.series == Championship.Series.GTSeries;
    this.singleSeaterSeriesToggle.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnSeriesChangeToggle(value, Championship.Series.SingleSeaterSeries)));
    this.GTSeriesToggle.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnSeriesChangeToggle(value, Championship.Series.GTSeries)));
    this.unemployedToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 =>
    {
      this.mMode = UICompareStaffListWidget.Mode.Unemployed;
      this.RefreshList();
    }));
    this.favouritesToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 =>
    {
      this.mMode = UICompareStaffListWidget.Mode.Favourites;
      this.RefreshList();
    }));
    this.championshipLeftButton.onClick.RemoveAllListeners();
    this.championshipLeftButton.onClick.AddListener(new UnityAction(this.SelectHigherTierChampionship));
    this.championshipRightButton.onClick.RemoveAllListeners();
    this.championshipRightButton.onClick.AddListener(new UnityAction(this.SelectLowerTierChampionship));
    this.UpdateButtonData();
  }

  private void SelectHigherTierChampionship()
  {
    this.mSelectedChampionship = this.mSelectedChampionship.GetNextTierChampionship();
    this.UpdateButtonData();
    this.RefreshList();
  }

  private void SelectLowerTierChampionship()
  {
    this.mSelectedChampionship = this.mSelectedChampionship.GetPreviousTierChampionship();
    this.UpdateButtonData();
    this.RefreshList();
  }

  private void UpdateButtonData()
  {
    this.championshipName.text = this.mSelectedChampionship.GetChampionshipName(false);
    this.UpdateButtonsInteractability();
  }

  private void OnSeriesChangeToggle(bool isOn, Championship.Series inSeries)
  {
    if (!isOn)
      return;
    this.mMode = UICompareStaffListWidget.Mode.Championship;
    this.mSelectedSeries = inSeries;
    this.UpdateSelectedSeries();
    this.RefreshList();
  }

  private void UpdateSelectedSeries()
  {
    this.mSelectedChampionship = Game.instance.championshipManager.GetMainChampionship(this.mSelectedSeries);
    this.UpdateButtonData();
  }

  private void UpdateButtonsInteractability()
  {
    this.championshipLeftButton.interactable = this.mSelectedChampionship.GetNextTierChampionship() != null;
    this.championshipRightButton.interactable = this.mSelectedChampionship.GetPreviousTierChampionship() != null;
  }

  public void RefreshList()
  {
    GameUtility.SetActive(this.seriesSelectContainer, this.mMode == UICompareStaffListWidget.Mode.Championship);
    this.ClearList();
    this.SetTitle();
    switch (this.screen.mode)
    {
      case CompareStaffScreen.Mode.Driver:
        this.SetDriverList();
        break;
      case CompareStaffScreen.Mode.Engineer:
        this.SetEngineerList();
        break;
      case CompareStaffScreen.Mode.Mechanic:
        this.SetMechanicList();
        break;
    }
  }

  public void SetTitle()
  {
    switch (this.screen.mode)
    {
      case CompareStaffScreen.Mode.Driver:
        this.title.text = Localisation.LocaliseID("PSG_10010674", (GameObject) null);
        break;
      case CompareStaffScreen.Mode.Engineer:
        this.title.text = Localisation.LocaliseID("PSG_10010672", (GameObject) null);
        break;
      case CompareStaffScreen.Mode.Mechanic:
        this.title.text = Localisation.LocaliseID("PSG_10010673", (GameObject) null);
        break;
    }
  }

  private void ClearList()
  {
    this.groups.Clear();
    this.headersGrid.DestroyListItems();
    this.listGrid.DestroyListItems();
  }

  public void SetDriverList()
  {
    int inStartIndex = 0;
    string empty = string.Empty;
    List<Person> allPeopleOnJob = Game.instance.player.team.contractManager.GetAllPeopleOnJob(Contract.Job.Driver);
    Person.SortByAbility<Person>(allPeopleOnJob, false);
    if (allPeopleOnJob.Count > 0)
    {
      UICompareStaffListWidget.ListItemGroup group;
      if (!this.CheckGroup("My Drivers"))
      {
        group = this.CreateGroup("My Drivers", Localisation.LocaliseID("PSG_10010676", (GameObject) null), allPeopleOnJob.ToArray());
      }
      else
      {
        group = this.GetGroup("My Drivers");
        this.ValidateGroup(group, allPeopleOnJob.ToArray());
      }
      inStartIndex = this.OrderGroup(group, inStartIndex);
    }
    int num;
    if (this.mMode == UICompareStaffListWidget.Mode.Championship)
    {
      List<Driver> championshipDrivers = this.GetChampionshipDrivers(this.mSelectedChampionship);
      Person.SortByAbility<Driver>(championshipDrivers, false);
      if (championshipDrivers.Count > 0)
      {
        UICompareStaffListWidget.ListItemGroup group;
        if (!this.CheckGroup(this.mSelectedChampionship.GetChampionshipName(false) + " Drivers"))
        {
          StringVariableParser.randomChampionship = this.mSelectedChampionship;
          group = this.CreateGroup(this.mSelectedChampionship.GetChampionshipName(false) + " Drivers", Localisation.LocaliseID("PSG_10010677", (GameObject) null), (Person[]) championshipDrivers.ToArray());
        }
        else
        {
          group = this.GetGroup(this.mSelectedChampionship.GetChampionshipName(false) + " Drivers");
          this.ValidateGroup(group, (Person[]) championshipDrivers.ToArray());
        }
        num = this.OrderGroup(group, inStartIndex);
      }
      else
        this.ClearGroup(this.mSelectedChampionship.GetChampionshipName(false) + " Drivers");
    }
    else if (this.mMode == UICompareStaffListWidget.Mode.Favourites)
    {
      List<Driver> favouriteDrivers = this.GetFavouriteDrivers(Championship.Series.Count);
      Person.SortByAbility<Driver>(favouriteDrivers, false);
      if (favouriteDrivers.Count > 0)
      {
        UICompareStaffListWidget.ListItemGroup group;
        if (!this.CheckGroup("Favourite Drivers"))
        {
          group = this.CreateGroup("Favourite Drivers", Localisation.LocaliseID("PSG_10010678", (GameObject) null), (Person[]) favouriteDrivers.ToArray());
        }
        else
        {
          group = this.GetGroup("Favourite Drivers");
          this.ValidateGroup(group, (Person[]) favouriteDrivers.ToArray());
        }
        num = this.OrderGroup(group, inStartIndex);
      }
      else
        this.ClearGroup("Favourite Drivers");
    }
    else
    {
      List<Driver> unemployedDrivers = this.GetUnemployedDrivers(Championship.Series.Count);
      Person.SortByAbility<Driver>(unemployedDrivers, false);
      if (unemployedDrivers.Count > 0)
      {
        UICompareStaffListWidget.ListItemGroup group;
        if (!this.CheckGroup("Unemployed Drivers"))
        {
          group = this.CreateGroup("Unemployed Drivers", Localisation.LocaliseID("PSG_10011129", (GameObject) null), (Person[]) unemployedDrivers.ToArray());
        }
        else
        {
          group = this.GetGroup("Unemployed Drivers");
          this.ValidateGroup(group, (Person[]) unemployedDrivers.ToArray());
        }
        num = this.OrderGroup(group, inStartIndex);
      }
      else
        this.ClearGroup("Unemployed Drivers");
    }
  }

  public void SetEngineerList()
  {
    int inStartIndex = 0;
    string empty = string.Empty;
    List<Engineer> myEngineer = this.GetMyEngineer();
    Person.SortByAbility<Engineer>(myEngineer, false);
    if (myEngineer.Count > 0)
    {
      UICompareStaffListWidget.ListItemGroup group;
      if (!this.CheckGroup("My Engineer"))
      {
        group = this.CreateGroup("My Engineer", Localisation.LocaliseID("PSG_10010682", (GameObject) null), (Person[]) myEngineer.ToArray());
      }
      else
      {
        group = this.GetGroup("My Engineer");
        this.ValidateGroup(group, (Person[]) myEngineer.ToArray());
      }
      inStartIndex = this.OrderGroup(group, inStartIndex);
    }
    int num;
    if (this.mMode == UICompareStaffListWidget.Mode.Championship)
    {
      List<Engineer> championshipEngineers = this.GetChampionshipEngineers(this.mSelectedChampionship);
      Person.SortByAbility<Engineer>(championshipEngineers, false);
      if (championshipEngineers.Count > 0)
      {
        UICompareStaffListWidget.ListItemGroup group;
        if (!this.CheckGroup(this.mSelectedChampionship.GetChampionshipName(false) + " Engineers"))
        {
          StringVariableParser.randomChampionship = this.mSelectedChampionship;
          group = this.CreateGroup(this.mSelectedChampionship.GetChampionshipName(false) + " Engineers", Localisation.LocaliseID("PSG_10010683", (GameObject) null), (Person[]) championshipEngineers.ToArray());
        }
        else
        {
          group = this.GetGroup(this.mSelectedChampionship.GetChampionshipName(false) + " Engineers");
          this.ValidateGroup(group, (Person[]) championshipEngineers.ToArray());
        }
        num = this.OrderGroup(group, inStartIndex);
      }
      else
        this.ClearGroup(this.mSelectedChampionship.GetChampionshipName(false) + " Engineers");
    }
    else if (this.mMode == UICompareStaffListWidget.Mode.Favourites)
    {
      List<Engineer> favouriteEngineers = this.GetFavouriteEngineers();
      Person.SortByAbility<Engineer>(favouriteEngineers, false);
      if (favouriteEngineers.Count > 0)
      {
        UICompareStaffListWidget.ListItemGroup group;
        if (!this.CheckGroup("Favourite Engineers"))
        {
          group = this.CreateGroup("Favourite Engineers", Localisation.LocaliseID("PSG_10010684", (GameObject) null), (Person[]) favouriteEngineers.ToArray());
        }
        else
        {
          group = this.GetGroup("Favourite Engineers");
          this.ValidateGroup(group, (Person[]) favouriteEngineers.ToArray());
        }
        num = this.OrderGroup(group, inStartIndex);
      }
      else
        this.ClearGroup("Favourite Engineers");
    }
    else
    {
      List<Engineer> unemployedEngineers = this.GetUnemployedEngineers();
      Person.SortByAbility<Engineer>(unemployedEngineers, false);
      if (unemployedEngineers.Count > 0)
      {
        UICompareStaffListWidget.ListItemGroup group;
        if (!this.CheckGroup("Unemployed Engineers"))
        {
          group = this.CreateGroup("Unemployed Engineers", Localisation.LocaliseID("PSG_10011130", (GameObject) null), (Person[]) unemployedEngineers.ToArray());
        }
        else
        {
          group = this.GetGroup("Unemployed Engineers");
          this.ValidateGroup(group, (Person[]) unemployedEngineers.ToArray());
        }
        num = this.OrderGroup(group, inStartIndex);
      }
      else
        this.ClearGroup("Unemployed Engineers");
    }
  }

  public void SetMechanicList()
  {
    int inStartIndex = 0;
    string empty = string.Empty;
    List<Mechanic> myMechanics = this.GetMyMechanics();
    Person.SortByAbility<Mechanic>(myMechanics, false);
    if (myMechanics.Count > 0)
    {
      UICompareStaffListWidget.ListItemGroup group;
      if (!this.CheckGroup("My Mechanics"))
      {
        group = this.CreateGroup("My Mechanics", Localisation.LocaliseID("PSG_10010679", (GameObject) null), (Person[]) myMechanics.ToArray());
      }
      else
      {
        group = this.GetGroup("My Mechanics");
        this.ValidateGroup(group, (Person[]) myMechanics.ToArray());
      }
      inStartIndex = this.OrderGroup(group, inStartIndex);
    }
    int num;
    if (this.mMode == UICompareStaffListWidget.Mode.Championship)
    {
      List<Mechanic> championshipMechanics = this.GetChampionshipMechanics(this.mSelectedChampionship);
      Person.SortByAbility<Mechanic>(championshipMechanics, false);
      if (championshipMechanics.Count > 0)
      {
        UICompareStaffListWidget.ListItemGroup group;
        if (!this.CheckGroup(this.mSelectedChampionship.GetChampionshipName(false) + " Mechanics"))
        {
          StringVariableParser.randomChampionship = this.mSelectedChampionship;
          group = this.CreateGroup(this.mSelectedChampionship.GetChampionshipName(false) + " Mechanics", Localisation.LocaliseID("PSG_10010680", (GameObject) null), (Person[]) championshipMechanics.ToArray());
        }
        else
        {
          group = this.GetGroup(this.mSelectedChampionship.GetChampionshipName(false) + " Mechanics");
          this.ValidateGroup(group, (Person[]) championshipMechanics.ToArray());
        }
        num = this.OrderGroup(group, inStartIndex);
      }
      else
        this.ClearGroup(this.mSelectedChampionship.GetChampionshipName(false) + " Mechanics");
    }
    else if (this.mMode == UICompareStaffListWidget.Mode.Favourites)
    {
      List<Mechanic> favouriteMechanics = this.GetFavouriteMechanics();
      Person.SortByAbility<Mechanic>(favouriteMechanics, false);
      if (favouriteMechanics.Count > 0)
      {
        UICompareStaffListWidget.ListItemGroup group;
        if (!this.CheckGroup("Favourite Mechanics"))
        {
          group = this.CreateGroup("Favourite Mechanics", Localisation.LocaliseID("PSG_10010681", (GameObject) null), (Person[]) favouriteMechanics.ToArray());
        }
        else
        {
          group = this.GetGroup("Favourite Mechanics");
          this.ValidateGroup(group, (Person[]) favouriteMechanics.ToArray());
        }
        num = this.OrderGroup(group, inStartIndex);
      }
      else
        this.ClearGroup("Favourite Mechanics");
    }
    else
    {
      List<Mechanic> unemployedMechanics = this.GetUnemployedMechanics();
      Person.SortByAbility<Mechanic>(unemployedMechanics, false);
      if (unemployedMechanics.Count > 0)
      {
        UICompareStaffListWidget.ListItemGroup group;
        if (!this.CheckGroup("Unemployed Mechanics"))
        {
          group = this.CreateGroup("Unemployed Mechanics", Localisation.LocaliseID("PSG_10011131", (GameObject) null), (Person[]) unemployedMechanics.ToArray());
        }
        else
        {
          group = this.GetGroup("Unemployed Mechanics");
          this.ValidateGroup(group, (Person[]) unemployedMechanics.ToArray());
        }
        num = this.OrderGroup(group, inStartIndex);
      }
      else
        this.ClearGroup("Unemployed Mechanics");
    }
  }

  public UICompareStaffListWidget.ListItemGroup CreateGroup(string inName, string inLocalisedName, Person[] inPersonArray)
  {
    UICompareStaffListWidget.ListItemGroup listItemGroup = (UICompareStaffListWidget.ListItemGroup) null;
    if (inPersonArray.Length > 0)
    {
      GameUtility.SetActive(this.headersGrid.itemPrefab, true);
      UICompareStaffHeaderEntry listItem1 = this.headersGrid.CreateListItem<UICompareStaffHeaderEntry>();
      listItem1.Setup(inName, inLocalisedName);
      GameUtility.SetActive(this.headersGrid.itemPrefab, false);
      GameUtility.SetActive(this.listGrid.itemPrefab, true);
      this.mStaffItemEntries.Clear();
      for (int index = 0; index < inPersonArray.Length; ++index)
      {
        UICompareStaffItemEntry listItem2 = this.listGrid.CreateListItem<UICompareStaffItemEntry>();
        this.mStaffItemEntries.Add(listItem2);
        listItem2.Setup(inPersonArray[index]);
        listItem2.SetButtonInteratable(!this.CheckPerson(inPersonArray[index]));
      }
      GameUtility.SetActive(this.listGrid.itemPrefab, false);
      listItemGroup = new UICompareStaffListWidget.ListItemGroup(inName, listItem1, this.mStaffItemEntries.ToArray());
      this.groups.Add(listItemGroup);
    }
    return listItemGroup;
  }

  public void ValidateGroup(UICompareStaffListWidget.ListItemGroup inGroup, Person[] inPersonArray)
  {
    int count1 = inGroup.items.Count;
    if (count1 != inPersonArray.Length)
    {
      GameUtility.SetActive(this.listGrid.itemPrefab, true);
      int num = Mathf.Abs(count1 - inPersonArray.Length);
      UICompareStaffHeaderEntry header = inGroup.header;
      if (count1 < inPersonArray.Length)
      {
        for (int index = 0; index < num; ++index)
        {
          UICompareStaffItemEntry listItem = this.listGrid.CreateListItem<UICompareStaffItemEntry>();
          inGroup.items.Add(listItem);
          if ((UnityEngine.Object) header != (UnityEngine.Object) null && header.status == UICompareStaffHeaderEntry.Status.Closed)
            GameUtility.SetActive(listItem.gameObject, false);
        }
      }
      else
      {
        for (int index = 0; index < num; ++index)
        {
          UICompareStaffItemEntry inEntry = inGroup.items[count1 - 1 - index];
          inGroup.items.RemoveAt(inGroup.items.IndexOf(inEntry));
          this.listGrid.DestroyListItem(this.GetGridIndex(inEntry));
          --count1;
        }
      }
      GameUtility.SetActive(this.listGrid.itemPrefab, false);
    }
    int count2 = inGroup.items.Count;
    for (int index = 0; index < inGroup.items.Count; ++index)
    {
      UICompareStaffItemEntry compareStaffItemEntry = inGroup.items[index];
      if (compareStaffItemEntry.person != inPersonArray[index])
        compareStaffItemEntry.Setup(inPersonArray[index]);
      compareStaffItemEntry.button.interactable = !this.CheckPerson(compareStaffItemEntry.person);
    }
  }

  public int GetGridIndex(UICompareStaffItemEntry inEntry)
  {
    if ((UnityEngine.Object) inEntry != (UnityEngine.Object) null)
    {
      int itemCount = this.listGrid.itemCount;
      for (int inIndex = 0; inIndex < itemCount; ++inIndex)
      {
        UICompareStaffItemEntry compareStaffItemEntry = this.listGrid.GetItem<UICompareStaffItemEntry>(inIndex);
        if ((UnityEngine.Object) compareStaffItemEntry != (UnityEngine.Object) null && (UnityEngine.Object) compareStaffItemEntry == (UnityEngine.Object) inEntry)
          return inIndex;
      }
    }
    return -1;
  }

  public bool CheckPerson(Person inPerson)
  {
    return inPerson == this.screen.leftPanel.person || inPerson == this.screen.rightPanel.person;
  }

  public bool CheckGroup(string inName)
  {
    if (!string.IsNullOrEmpty(inName))
    {
      int count = this.groups.Count;
      for (int index = 0; index < count; ++index)
      {
        if (this.groups[index].name == inName)
          return true;
      }
    }
    return false;
  }

  public UICompareStaffListWidget.ListItemGroup GetGroup(string inName)
  {
    if (!string.IsNullOrEmpty(inName))
    {
      int count = this.groups.Count;
      for (int index = 0; index < count; ++index)
      {
        if (this.groups[index].name == inName)
          return this.groups[index];
      }
    }
    return (UICompareStaffListWidget.ListItemGroup) null;
  }

  public int OrderGroup(UICompareStaffListWidget.ListItemGroup inGroup, int inStartIndex)
  {
    int inSiblingIndex = inStartIndex;
    GameUtility.SetSiblingIndex(inGroup.header.transform, inSiblingIndex);
    int count = inGroup.items.Count;
    for (int index = 0; index < count; ++index)
    {
      ++inSiblingIndex;
      GameUtility.SetSiblingIndex(inGroup.items[index].gameObject.transform, inSiblingIndex);
    }
    return inSiblingIndex + 1;
  }

  public void SetGroup(string inName, bool inOpen)
  {
    if (string.IsNullOrEmpty(inName))
      return;
    UICompareStaffListWidget.ListItemGroup group = this.GetGroup(inName);
    if (group == null)
      return;
    int count = group.items.Count;
    for (int index = 0; index < count; ++index)
      GameUtility.SetActive(group.items[index].gameObject, inOpen);
  }

  public void ClearGroup(string inName)
  {
    if (string.IsNullOrEmpty(inName))
      return;
    UICompareStaffListWidget.ListItemGroup group = this.GetGroup(inName);
    if (group == null)
      return;
    if ((UnityEngine.Object) group.header != (UnityEngine.Object) null)
      this.headersGrid.DestroyListItem(group.header.gameObject);
    int count = group.items.Count;
    for (int index = 0; index < count; ++index)
      this.listGrid.DestroyListItem(group.items[index].gameObject);
    group.header = (UICompareStaffHeaderEntry) null;
    group.items.Clear();
    this.groups.RemoveAt(this.groups.IndexOf(group));
  }

  public void SetShortListed(Person inPerson)
  {
    int count1 = this.groups.Count;
    for (int index1 = 0; index1 < count1; ++index1)
    {
      UICompareStaffListWidget.ListItemGroup group = this.groups[index1];
      int count2 = group.items.Count;
      for (int index2 = 0; index2 < count2; ++index2)
      {
        UICompareStaffItemEntry compareStaffItemEntry = group.items[index2];
        if (compareStaffItemEntry.person == inPerson)
          compareStaffItemEntry.toggle.isOn = inPerson.isShortlisted;
      }
    }
  }

  public List<Driver> GetFavouriteDrivers(Championship.Series inSeries = Championship.Series.Count)
  {
    List<Driver> entityList = Game.instance.driverManager.GetEntityList();
    this.mReturnDrivers.Clear();
    int count = entityList.Count;
    for (int index = 0; index < count; ++index)
    {
      Driver driver = entityList[index];
      if (driver.isShortlisted && !driver.IsPlayersDriver() && (inSeries == Championship.Series.Count || driver.joinsAnySeries || driver.preferedSeries == inSeries))
        this.mReturnDrivers.Add(driver);
    }
    return this.mReturnDrivers;
  }

  public List<Driver> GetUnemployedDrivers(Championship.Series inSeries = Championship.Series.Count)
  {
    HQsBuilding_v1 building = Game.instance.player.team.headquarters.GetBuilding(HQsBuildingInfo.Type.ScoutingFacility);
    List<Driver> entityList = Game.instance.driverManager.GetEntityList();
    this.mReturnDrivers.Clear();
    int count = entityList.Count;
    for (int index = 0; index < count; ++index)
    {
      Driver inDriver = entityList[index];
      if (!inDriver.HasRetired() && inDriver.IsFreeAgent() && this.FilterScoutingLevel(inDriver, building) && (inSeries == Championship.Series.Count || inDriver.joinsAnySeries || inDriver.preferedSeries == inSeries))
        this.mReturnDrivers.Add(inDriver);
    }
    return this.mReturnDrivers;
  }

  private bool FilterScoutingLevel(Driver inDriver, HQsBuilding_v1 inScoutingFacility)
  {
    if (inDriver != null)
    {
      int scoutingLevelRequired = inDriver.GetDriverStats().scoutingLevelRequired;
      if (scoutingLevelRequired == 0)
        return true;
      if (inScoutingFacility != null && inScoutingFacility.isBuilt)
        return inScoutingFacility.currentLevel >= scoutingLevelRequired - 1;
    }
    return false;
  }

  public List<Driver> GetChampionshipDrivers(Championship inChampionship)
  {
    this.mReturnDrivers.Clear();
    if (inChampionship != null)
    {
      ChampionshipStandings standings = inChampionship.standings;
      int driverEntryCount = standings.driverEntryCount;
      for (int inIndex = 0; inIndex < driverEntryCount; ++inIndex)
      {
        ChampionshipEntry_v1 driverEntry = standings.GetDriverEntry(inIndex);
        if (driverEntry != null)
          this.mReturnDrivers.Add(driverEntry.GetEntity<Driver>());
      }
    }
    return this.mReturnDrivers;
  }

  public List<Engineer> GetMyEngineer()
  {
    List<Engineer> engineerList = new List<Engineer>();
    Engineer personOnJob = (Engineer) Game.instance.player.team.contractManager.GetPersonOnJob(Contract.Job.EngineerLead);
    if (personOnJob != null)
      engineerList.Add(personOnJob);
    return engineerList;
  }

  public List<Engineer> GetFavouriteEngineers()
  {
    List<Engineer> entityList = Game.instance.engineerManager.GetEntityList();
    this.mReturnEngineers.Clear();
    int count = entityList.Count;
    for (int index = 0; index < count; ++index)
    {
      Engineer engineer = entityList[index];
      if (engineer.isShortlisted && !engineer.contract.GetTeam().IsPlayersTeam())
        this.mReturnEngineers.Add(engineer);
    }
    return this.mReturnEngineers;
  }

  public List<Engineer> GetUnemployedEngineers()
  {
    List<Engineer> entityList = Game.instance.engineerManager.GetEntityList();
    this.mReturnEngineers.Clear();
    int count = entityList.Count;
    for (int index = 0; index < count; ++index)
    {
      Engineer engineer = entityList[index];
      if (!engineer.HasRetired() && engineer.IsFreeAgent())
        this.mReturnEngineers.Add(engineer);
    }
    return this.mReturnEngineers;
  }

  public List<Engineer> GetChampionshipEngineers(Championship inChampionship)
  {
    this.mReturnEngineers.Clear();
    if (inChampionship != null)
    {
      ChampionshipStandings standings = inChampionship.standings;
      int teamEntryCount = standings.teamEntryCount;
      for (int inIndex = 0; inIndex < teamEntryCount; ++inIndex)
      {
        ChampionshipEntry_v1 teamEntry = standings.GetTeamEntry(inIndex);
        if (teamEntry != null)
        {
          Team entity = teamEntry.GetEntity<Team>();
          if (entity != null)
          {
            Engineer personOnJob = (Engineer) entity.contractManager.GetPersonOnJob(Contract.Job.EngineerLead);
            if (personOnJob != null)
              this.mReturnEngineers.Add(personOnJob);
          }
        }
      }
    }
    return this.mReturnEngineers;
  }

  public List<Mechanic> GetMyMechanics()
  {
    List<Person> allPeopleOnJob = Game.instance.player.team.contractManager.GetAllPeopleOnJob(Contract.Job.Mechanic);
    this.mReturnMechanics.Clear();
    if (allPeopleOnJob.Count > 0)
      this.mReturnMechanics = allPeopleOnJob.ConvertAll<Mechanic>((Converter<Person, Mechanic>) (x => (Mechanic) x));
    return this.mReturnMechanics;
  }

  public List<Mechanic> GetFavouriteMechanics()
  {
    List<Mechanic> entityList = Game.instance.mechanicManager.GetEntityList();
    this.mReturnMechanics.Clear();
    int count = entityList.Count;
    for (int index = 0; index < count; ++index)
    {
      Mechanic mechanic = entityList[index];
      if (mechanic.isShortlisted && !mechanic.contract.GetTeam().IsPlayersTeam())
        this.mReturnMechanics.Add(mechanic);
    }
    return this.mReturnMechanics;
  }

  public List<Mechanic> GetUnemployedMechanics()
  {
    List<Mechanic> entityList = Game.instance.mechanicManager.GetEntityList();
    this.mReturnMechanics.Clear();
    int count = entityList.Count;
    for (int index = 0; index < count; ++index)
    {
      Mechanic mechanic = entityList[index];
      if (!mechanic.HasRetired() && mechanic.IsFreeAgent())
        this.mReturnMechanics.Add(mechanic);
    }
    return this.mReturnMechanics;
  }

  public List<Mechanic> GetChampionshipMechanics(Championship inChampionship)
  {
    this.mReturnMechanics.Clear();
    if (inChampionship != null)
    {
      ChampionshipStandings standings = inChampionship.standings;
      int teamEntryCount = standings.teamEntryCount;
      for (int inIndex = 0; inIndex < teamEntryCount; ++inIndex)
      {
        ChampionshipEntry_v1 teamEntry = standings.GetTeamEntry(inIndex);
        if (teamEntry != null)
        {
          Team entity = teamEntry.GetEntity<Team>();
          if (entity != null)
          {
            List<Person> allPeopleOnJob = entity.contractManager.GetAllPeopleOnJob(Contract.Job.Mechanic);
            if (allPeopleOnJob.Count > 0)
            {
              for (int index = 0; index < allPeopleOnJob.Count; ++index)
                this.mReturnMechanics.Add((Mechanic) allPeopleOnJob[index]);
            }
          }
        }
      }
    }
    return this.mReturnMechanics;
  }

  private void OnToggle(Toggle inToggle, int inValue)
  {
    if (!inToggle.isOn)
      return;
    if (inValue < 3 && this.mChampionships.Count > 0)
    {
      this.mSelectedChampionship = this.mChampionships[inValue];
      this.mMode = UICompareStaffListWidget.Mode.Championship;
    }
    else
    {
      this.mSelectedChampionship = (Championship) null;
      switch (inValue)
      {
        case 3:
          this.mMode = UICompareStaffListWidget.Mode.Unemployed;
          break;
        case 4:
          this.mMode = UICompareStaffListWidget.Mode.Favourites;
          break;
      }
    }
    this.RefreshList();
  }

  public enum Mode
  {
    Championship,
    Unemployed,
    Favourites,
  }

  public class ListItemGroup
  {
    public string name = string.Empty;
    public List<UICompareStaffItemEntry> items = new List<UICompareStaffItemEntry>();
    public UICompareStaffHeaderEntry header;

    public ListItemGroup(string inName, UICompareStaffHeaderEntry inHeader, params UICompareStaffItemEntry[] inEntries)
    {
      if (string.IsNullOrEmpty(inName) || !((UnityEngine.Object) inHeader != (UnityEngine.Object) null) || inEntries == null)
        return;
      this.name = inName;
      this.header = inHeader;
      for (int index = 0; index < inEntries.Length; ++index)
        this.items.Add(inEntries[index]);
    }
  }
}
