// Decompiled with JetBrains decompiler
// Type: HomeScreenWipWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreenWipWidget : MonoBehaviour
{
  private List<HQsBuilding_v1> mBuildings = new List<HQsBuilding_v1>();
  private List<TeamWipHeader> mHeaders = new List<TeamWipHeader>();
  public UIGridList grid;
  public GameObject prefabHeader;
  public GameObject prefabItem;
  public GameObject noWip;
  private ScoutingManager mScoutingManager;
  private Headquarters mHeadquarters;
  private CarPartDesign mCarPartDesign;
  private PartImprovement mPartImprovement;
  private bool mScouting;
  private bool mHQBuilding;
  private bool mPartBuilding;
  private bool mPartImprovementReliability;
  private bool mPartImprovementPerformance;
  private TeamWipEntry mReliability;

  public ScoutingManager scoutingManager
  {
    get
    {
      return this.mScoutingManager;
    }
  }

  public void Setup()
  {
    this.mScoutingManager = Game.instance.scoutingManager;
    this.mHeadquarters = Game.instance.player.team.headquarters;
    this.mCarPartDesign = Game.instance.player.team.carManager.carPartDesign;
    this.mPartImprovement = Game.instance.player.team.carManager.partImprovement;
    this.mBuildings = this.mHeadquarters.GetBuildingsOnState(HQsBuilding_v1.BuildingState.BuildingInProgress, HQsBuilding_v1.BuildingState.Upgrading);
    this.mBuildings.Sort((Comparison<HQsBuilding_v1>) ((x, y) => x.timeRemaining.TotalSeconds.CompareTo(y.timeRemaining.TotalSeconds)));
    this.mReliability = (TeamWipEntry) null;
    this.mHeaders.Clear();
    this.UpdateStatus();
    this.grid.DestroyListItems();
    GameUtility.SetActive(this.prefabHeader, true);
    GameUtility.SetActive(this.prefabItem, true);
    GameUtility.SetActive(this.noWip, !this.mScouting && !this.mHQBuilding && (!this.mPartBuilding && !this.mPartImprovementReliability) && !this.mPartImprovementPerformance);
    if (this.mScouting)
    {
      this.CreateHeader(Localisation.LocaliseID("PSG_10001578", (GameObject) null), TeamWipEntry.Type.Scouting);
      this.CreateScoutingEntries();
    }
    if (this.mHQBuilding)
    {
      this.CreateHeader(Localisation.LocaliseID("PSG_10002250", (GameObject) null), TeamWipEntry.Type.HQBuilding);
      int count = this.mBuildings.Count;
      for (int index = 0; index < count; ++index)
        this.CreateHQEntry(this.mBuildings[index]);
    }
    if (this.mPartBuilding)
    {
      this.CreateHeader(Localisation.LocaliseID("PSG_10002082", (GameObject) null), TeamWipEntry.Type.PartBuilding);
      this.CreatePartDesignEntry();
    }
    if (this.mPartImprovementReliability || this.mPartImprovementPerformance)
    {
      this.CreateHeader(Localisation.LocaliseID("PSG_10010986", (GameObject) null), TeamWipEntry.Type.PartImprovements);
      if (this.mPartImprovementReliability)
        this.CreatePartImprovementEntry(CarPartStats.CarPartStat.Reliability);
      if (this.mPartImprovementPerformance)
        this.CreatePartImprovementEntry(CarPartStats.CarPartStat.Performance);
    }
    GameUtility.SetActive(this.prefabHeader, false);
    GameUtility.SetActive(this.prefabItem, false);
  }

  private void CreateScoutingEntries()
  {
    int currentScoutingsCount = this.mScoutingManager.currentScoutingsCount;
    for (int index = 0; index < currentScoutingsCount; ++index)
      this.CreateScoutEntry(this.mScoutingManager.GetCurrentScoutingEntry(index).driver, index + 1);
  }

  private void DestroyEntries(TeamWipEntry.Type inEntryType)
  {
    for (int inIndex = this.grid.itemCount - 1; inIndex >= 0; --inIndex)
    {
      TeamWipEntry teamWipEntry = this.grid.GetItem<TeamWipEntry>(inIndex);
      if ((UnityEngine.Object) teamWipEntry != (UnityEngine.Object) null && teamWipEntry.type == inEntryType)
        this.grid.DestroyListItem(inIndex);
    }
  }

  public void UpdateHeader(TeamWipEntry.Type inHeaderType)
  {
    this.UpdateStatus();
    switch (inHeaderType)
    {
      case TeamWipEntry.Type.Scouting:
        this.ActivateHeader(inHeaderType, this.mScouting);
        break;
      case TeamWipEntry.Type.HQBuilding:
        this.ActivateHeader(inHeaderType, this.mHQBuilding);
        break;
      case TeamWipEntry.Type.PartBuilding:
        this.ActivateHeader(inHeaderType, this.mPartBuilding);
        break;
      case TeamWipEntry.Type.PartImprovements:
        this.ActivateHeader(inHeaderType, this.mPartImprovementPerformance || this.mPartImprovementReliability);
        break;
    }
  }

  public void UpdateEntries(TeamWipEntry.Type inEntriesType)
  {
    this.UpdateStatus();
    if (inEntriesType != TeamWipEntry.Type.Scouting)
      return;
    this.DestroyEntries(inEntriesType);
    if (!this.mScouting)
      return;
    GameUtility.SetActive(this.prefabItem, true);
    this.CreateScoutingEntries();
    GameUtility.SetActive(this.prefabItem, false);
  }

  private void UpdateStatus()
  {
    this.mScouting = this.mScoutingManager.IsScouting();
    this.mHQBuilding = this.mBuildings.Count > 0;
    this.mPartBuilding = this.mCarPartDesign.stage == CarPartDesign.Stage.Designing;
    this.mPartImprovementReliability = this.mPartImprovement.WorkOnStatActive(CarPartStats.CarPartStat.Reliability);
    this.mPartImprovementPerformance = this.mPartImprovement.WorkOnStatActive(CarPartStats.CarPartStat.Performance);
  }

  private void CreateHeader(string inHeader, TeamWipEntry.Type inHeaderType)
  {
    this.grid.itemPrefab = this.prefabHeader;
    TeamWipHeader listItem = this.grid.CreateListItem<TeamWipHeader>();
    listItem.Setup(inHeader, inHeaderType);
    this.mHeaders.Add(listItem);
  }

  private TeamWipHeader GetHeader(TeamWipEntry.Type inHeaderType)
  {
    int count = this.mHeaders.Count;
    for (int index = 0; index < count; ++index)
    {
      TeamWipHeader mHeader = this.mHeaders[index];
      if (mHeader.type == inHeaderType)
        return mHeader;
    }
    return (TeamWipHeader) null;
  }

  private void ActivateHeader(TeamWipEntry.Type inHeaderType, bool inValue)
  {
    GameUtility.SetActive(this.GetHeader(inHeaderType).gameObject, inValue);
  }

  private void CreateScoutEntry(Driver inDriver, int siblingIndex)
  {
    this.grid.itemPrefab = this.prefabItem;
    TeamWipHeader header = this.GetHeader(TeamWipEntry.Type.Scouting);
    TeamWipEntry listItem = this.grid.CreateListItem<TeamWipEntry>();
    listItem.type = TeamWipEntry.Type.Scouting;
    listItem.Setup(inDriver);
    listItem.transform.SetSiblingIndex(header.transform.GetSiblingIndex() + siblingIndex);
  }

  private void CreateHQEntry(HQsBuilding_v1 inBuilding)
  {
    this.grid.itemPrefab = this.prefabItem;
    TeamWipEntry listItem = this.grid.CreateListItem<TeamWipEntry>();
    listItem.type = TeamWipEntry.Type.HQBuilding;
    listItem.Setup(inBuilding);
  }

  private void CreatePartDesignEntry()
  {
    this.grid.itemPrefab = this.prefabItem;
    TeamWipEntry listItem = this.grid.CreateListItem<TeamWipEntry>();
    listItem.type = TeamWipEntry.Type.PartBuilding;
    listItem.Set();
  }

  private void CreatePartImprovementEntry(CarPartStats.CarPartStat inStat)
  {
    this.grid.itemPrefab = this.prefabItem;
    TeamWipEntry listItem = this.grid.CreateListItem<TeamWipEntry>();
    listItem.type = TeamWipEntry.Type.PartImprovements;
    listItem.Setup(inStat);
    switch (inStat)
    {
      case CarPartStats.CarPartStat.Reliability:
        this.mReliability = listItem;
        break;
      case CarPartStats.CarPartStat.Performance:
        if (!((UnityEngine.Object) this.mReliability != (UnityEngine.Object) null) || listItem.timeRemaining.TotalSeconds >= this.mReliability.timeRemaining.TotalSeconds)
          break;
        listItem.transform.SetSiblingIndex(this.mReliability.transform.GetSiblingIndex());
        break;
    }
  }
}
