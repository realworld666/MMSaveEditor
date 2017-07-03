// Decompiled with JetBrains decompiler
// Type: UIScoutingSearchResultsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class UIScoutingSearchResultsWidget : MonoBehaviour
{
  public float filterAbilityStars = 5f;
  private List<Person> mList = new List<Person>();
  private List<UIScoutingStaffEntry> mEntries = new List<UIScoutingStaffEntry>();
  private UIScoutingFilter.Type mFilter = UIScoutingFilter.Type.Ability;
  public UILoadList grid;
  public UIScoutingFilter[] tableFilters;
  public UIScoutingFilterDriveableSeries driveableSeries;
  public UIScoutingFilterJobRole jobRole;
  public UIScoutingFilterView view;
  public UIScoutingFilterAge age;
  public UIScoutingFilterAbility ability;
  public Championship.Series filterDriveableSeries;
  public UIScoutingFilterJobRole.Filter filterJobRole;
  public UIScoutingFilterView.Filter filterView;
  public UIScoutingFilterAge.Filter filterAge;
  public UIScoutingFilterAbility.Filter filterAbility;
  public GameObject driverSeriesHeader;
  private GameObject[] mGridItems;
  private bool mFilterAsc;
  private Notification mNotificationAll;
  private Notification mNotificationFavourites;
  private Notification mNotificationScouted;

  public void OnStart()
  {
    this.driveableSeries.OnStart();
    this.jobRole.OnStart();
    this.view.OnStart();
    this.age.OnStart();
    this.ability.OnStart();
    for (int index = 0; index < this.tableFilters.Length; ++index)
    {
      UIScoutingFilter tableFilter = this.tableFilters[index];
      tableFilter.toggle.isOn = tableFilter.filterType == this.mFilter;
      tableFilter.OnStart();
    }
    this.grid.OnScrollRect -= new Action(this.OnScrollRect);
    this.grid.OnScrollRect += new Action(this.OnScrollRect);
  }

  public void Refresh()
  {
    this.GetFilters();
    this.SetGrid();
  }

  public void RefreshScoutingStatus()
  {
    int count = this.mEntries.Count;
    for (int index = 0; index < count; ++index)
      this.mEntries[index].UpdateAbilityScoutingState();
  }

  public void ApplyFilter(UIScoutingFilter.Type inFilter, bool inAsc)
  {
    this.mFilter = inFilter;
    this.mFilterAsc = inAsc;
    this.Refresh();
  }

  public void ClearGrid()
  {
    this.mEntries.Clear();
  }

  private void SetGrid()
  {
    this.mEntries.Clear();
    this.grid.HideListItems();
    GameUtility.SetActive(this.driverSeriesHeader, this.filterJobRole == UIScoutingFilterJobRole.Filter.Drivers && Game.instance.championshipManager.isGTSeriesActive);
    GameUtility.SetActive(this.driveableSeries.gameObject, Game.instance.championshipManager.isGTSeriesActive && this.filterJobRole == UIScoutingFilterJobRole.Filter.Drivers);
    this.SetNotifications();
    this.GetSortedList();
    this.SortByFilter();
    this.UpdateGrid(true);
  }

  private void UpdateGrid(bool inForceUpdate = false)
  {
    if (!this.grid.SetSize(this.mList.Count, inForceUpdate))
      return;
    this.mGridItems = this.grid.activatedItems;
    int length = this.mGridItems.Length;
    int firstActivatedIndex = this.grid.firstActivatedIndex;
    for (int index = 0; index < length; ++index)
    {
      UIScoutingStaffEntry component = this.mGridItems[index].GetComponent<UIScoutingStaffEntry>();
      Person m = this.mList[firstActivatedIndex];
      if (component.person != m)
        component.SetupEntry(m);
      else
        component.UpdateEntry();
      this.mEntries.Add(component);
      ++firstActivatedIndex;
    }
  }

  private void GetFilters()
  {
    this.ability.UpdateState();
    this.view.UpdateState();
    this.filterDriveableSeries = this.driveableSeries.filter;
    this.filterJobRole = this.jobRole.filter;
    this.filterView = this.view.filter;
    this.filterAge = this.age.filter;
    this.filterAbility = this.ability.filter;
    this.filterAbilityStars = this.ability.abilityStars;
  }

  private void GetSortedList()
  {
    switch (this.filterJobRole)
    {
      case UIScoutingFilterJobRole.Filter.Drivers:
        this.SortList<Driver>(Game.instance.driverManager.GetEntityList());
        this.mList.RemoveAll((Predicate<Person>) (x =>
        {
          if (!((Driver) x).joinsAnySeries)
            return ((Driver) x).preferedSeries != this.filterDriveableSeries;
          return false;
        }));
        break;
      case UIScoutingFilterJobRole.Filter.Designers:
        this.SortList<Engineer>(Game.instance.engineerManager.GetEntityList());
        break;
      case UIScoutingFilterJobRole.Filter.Mechanics:
        this.SortList<Mechanic>(Game.instance.mechanicManager.GetEntityList());
        break;
    }
  }

  private void SortList<T>(List<T> inList) where T : Person
  {
    this.mList.Clear();
    int count = inList.Count;
    for (int index = 0; index < count; ++index)
    {
      Person inPerson = (Person) inList[index];
      if (this.ApplyFilterPerson(inPerson) && this.ApplyFilterPlayerTeam(inPerson) && (this.ApplyFilterScoutingLevel(inPerson) && this.ApplyFilterAge(inPerson)) && (this.ApplyFilterAbility(inPerson) && this.ApplyChampionshipFilter(inPerson)))
      {
        this.AddPersonNotifications(inPerson);
        if (this.ApplyFilterView(inPerson))
          this.mList.Add(inPerson);
      }
    }
  }

  private void AddPersonNotifications(Person inPerson)
  {
    this.mNotificationAll.IncrementCount();
    if (inPerson.isShortlisted)
      this.mNotificationFavourites.IncrementCount();
    if (!(inPerson is Driver) || !(inPerson as Driver).hasBeenScouted)
      return;
    this.mNotificationScouted.IncrementCount();
  }

  private bool ApplyFilterPerson(Person inPerson)
  {
    return !inPerson.IsReplacementPerson() && !inPerson.HasRetired();
  }

  private bool ApplyFilterPlayerTeam(Person inPerson)
  {
    if (!inPerson.IsFreeAgent())
      return !inPerson.contract.GetTeam().IsPlayersTeam();
    return true;
  }

  private bool ApplyFilterScoutingLevel(Person inPerson)
  {
    Driver driver = inPerson as Driver;
    if (driver == null)
      return true;
    int scoutingLevelRequired = driver.GetDriverStats().scoutingLevelRequired;
    if (scoutingLevelRequired == 0)
      return true;
    HQsBuilding_v1 building = Game.instance.player.team.headquarters.GetBuilding(HQsBuildingInfo.Type.ScoutingFacility);
    if (building != null && building.isBuilt)
      return building.currentLevel >= scoutingLevelRequired - 1;
    return false;
  }

  private bool ApplyFilterView(Person inPerson)
  {
    switch (this.filterView)
    {
      case UIScoutingFilterView.Filter.Favourites:
        return inPerson.isShortlisted;
      case UIScoutingFilterView.Filter.Scouted:
        if (inPerson is Driver)
          return ((Driver) inPerson).hasBeenScouted;
        return true;
      default:
        return true;
    }
  }

  private bool ApplyFilterAge(Person inPerson)
  {
    int age = inPerson.GetAge();
    switch (this.filterAge)
    {
      case UIScoutingFilterAge.Filter.Young:
        if (age >= 16)
          return age <= 22;
        return false;
      case UIScoutingFilterAge.Filter.Medium:
        if (age >= 22)
          return age <= 28;
        return false;
      case UIScoutingFilterAge.Filter.Old:
        if (age >= 28)
          return age <= 34;
        return false;
      case UIScoutingFilterAge.Filter.Older:
        return age >= 34;
      default:
        return true;
    }
  }

  private bool ApplyFilterAbility(Person inPerson)
  {
    float ability = inPerson.GetStats().GetAbility();
    if (this.filterAbility != UIScoutingFilterAbility.Filter.Specific)
      return true;
    if (inPerson is Driver && !((Driver) inPerson).CanShowStats())
      return false;
    return (double) ability >= (double) this.filterAbilityStars;
  }

  private bool ApplyChampionshipFilter(Person inPerson)
  {
    if (!inPerson.IsFreeAgent())
      return inPerson.contract.GetTeam().championship.isChoosable;
    return true;
  }

  private void SortByFilter()
  {
    switch (this.mFilter)
    {
      case UIScoutingFilter.Type.Name:
        Person.SortByName<Person>(this.mList, this.mFilterAsc);
        break;
      case UIScoutingFilter.Type.Nationality:
        Person.SortByNationality<Person>(this.mList, this.mFilterAsc);
        break;
      case UIScoutingFilter.Type.Age:
        Person.SortByAge<Person>(this.mList, this.mFilterAsc);
        break;
      case UIScoutingFilter.Type.Ability:
        Person.SortByAbility<Person>(this.mList, this.mFilterAsc);
        break;
      case UIScoutingFilter.Type.Team:
        Person.SortByTeam<Person>(this.mList, this.mFilterAsc);
        break;
      case UIScoutingFilter.Type.RacingSeries:
        Person.SortByRacingSeries<Person>(this.mList, this.mFilterAsc);
        break;
      case UIScoutingFilter.Type.RaceCost:
        Person.SortByRaceCost<Person>(this.mList, this.mFilterAsc);
        break;
      case UIScoutingFilter.Type.BreakClause:
        Person.SortByBreakClauseCost<Person>(this.mList, this.mFilterAsc);
        break;
      case UIScoutingFilter.Type.ContractEnds:
        Person.SortByContractEndDate<Person>(this.mList, this.mFilterAsc);
        break;
    }
  }

  private void SetNotifications()
  {
    if (this.mNotificationAll == null)
      this.mNotificationAll = Game.instance.notificationManager.GetNotification("ScoutingScreenAll");
    if (this.mNotificationFavourites == null)
      this.mNotificationFavourites = Game.instance.notificationManager.GetNotification("ScoutingScreenFavourites");
    if (this.mNotificationScouted == null)
      this.mNotificationScouted = Game.instance.notificationManager.GetNotification("ScoutingScreenScouted");
    this.mNotificationAll.ResetCount();
    this.mNotificationFavourites.ResetCount();
    this.mNotificationScouted.ResetCount();
  }

  public void UpdateFavouriteNotification(bool inIncrease)
  {
    if (this.mNotificationFavourites == null)
      this.mNotificationFavourites = Game.instance.notificationManager.GetNotification("ScoutingScreenFavourites");
    if (inIncrease)
      this.mNotificationFavourites.IncrementCount();
    else
      this.mNotificationFavourites.DecrementCount();
  }

  private void OnScrollRect()
  {
    this.UpdateGrid(false);
  }

  public void HideTooltips()
  {
    DriverInfoRollover.HideTooltip();
    StaffInfoRollover.HideTooltip();
  }
}
