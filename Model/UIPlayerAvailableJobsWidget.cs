// Decompiled with JetBrains decompiler
// Type: UIPlayerAvailableJobsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerAvailableJobsWidget : MonoBehaviour
{
  private List<UIPlayerJobEntry> mEntries = new List<UIPlayerJobEntry>();
  private List<Team> mTeams = new List<Team>();
  public UIGridList grid;
  public GameObject jobEntry;
  public GameObject noJobsEntry;
  public GameObject championshipEntry;

  public void Setup()
  {
    this.grid.DestroyListItems();
    List<Championship> entityList = Game.instance.championshipManager.GetEntityList();
    GameUtility.SetActive(this.jobEntry, true);
    GameUtility.SetActive(this.noJobsEntry, true);
    GameUtility.SetActive(this.championshipEntry, true);
    for (int index1 = 0; index1 < entityList.Count; ++index1)
    {
      Championship inChampionship = entityList[index1];
      if (inChampionship.isChoosable && App.instance.dlcManager.IsSeriesAvailable(inChampionship.series))
      {
        this.mTeams.Clear();
        this.mEntries.Clear();
        int teamEntryCount = inChampionship.standings.teamEntryCount;
        this.grid.itemPrefab = this.championshipEntry;
        this.grid.CreateListItem<UIPlayerJobChampionshipEntry>().Setup(inChampionship);
        int num = 0;
        this.grid.itemPrefab = this.jobEntry;
        for (int inIndex = 0; inIndex < teamEntryCount; ++inIndex)
        {
          Team entity = inChampionship.standings.GetTeamEntry(inIndex).GetEntity<Team>();
          if (!Game.instance.player.HasAppliedForTeam(entity) && Game.instance.player.canJoinTeam(entity) && entity.teamPrincipal.jobSecurity <= TeamPrincipal.JobSecurity.Risk)
          {
            this.mEntries.Add(this.grid.CreateListItem<UIPlayerJobEntry>());
            this.mTeams.Add(entity);
            ++num;
          }
        }
        this.mTeams.Sort((Comparison<Team>) ((x, y) => x.GetVacancyAppeal().CompareTo(y.GetVacancyAppeal())));
        int count = this.mTeams.Count;
        for (int index2 = 0; index2 < count; ++index2)
          this.mEntries[index2].Setup(this.mTeams[index2]);
        if (num <= 0)
        {
          this.grid.itemPrefab = this.noJobsEntry;
          this.grid.CreateListItem<Transform>();
        }
      }
    }
    GameUtility.SetActive(this.jobEntry, false);
    GameUtility.SetActive(this.noJobsEntry, false);
    GameUtility.SetActive(this.championshipEntry, false);
  }
}
