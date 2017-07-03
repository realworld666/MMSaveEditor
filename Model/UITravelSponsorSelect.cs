// Decompiled with JetBrains decompiler
// Type: UITravelSponsorSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITravelSponsorSelect : UITravelStepOption
{
  [HideInInspector]
  public List<Animator> highlightAnimators = new List<Animator>();
  private UITravelSponsorSelect.Filter mFilter = UITravelSponsorSelect.Filter.Objective;
  private List<SponsorshipDeal> mSponsorshipDeals = new List<SponsorshipDeal>();
  public UITravelTableHeader[] tableHeaders;
  public ToggleGroup toggleGroup;
  public UIGridList sponsorsGrid;
  public TravelArrangementsScreen screen;
  public TextMeshProUGUI[] lastGpData;
  public TextMeshProUGUI[] secontToLastGp;
  public TextMeshProUGUI[] thirdToLastGp;
  public GameObject[] qualifyingEntries;
  public TextMeshProUGUI objectiveLabel;
  private bool mFilterAsc;

  public override void OnStart()
  {
    for (int index = 0; index < this.tableHeaders.Length; ++index)
      this.tableHeaders[index].OnStart();
  }

  public override void Setup()
  {
    this.SetFilter(this.mFilter);
    this.SetGrid();
    this.SetLastRaceEventsResults();
  }

  public override void RefreshText()
  {
    if (this.mSponsorshipDeals.Count <= 0)
      return;
    this.CreateGrid(this.mSponsorshipDeals.ToArray());
  }

  private void SetGrid()
  {
    this.CreateGrid(this.GetFilteredSponsors());
  }

  private void CreateGrid(SponsorshipDeal[] inSponsorships)
  {
    int itemCount1 = this.sponsorsGrid.itemCount;
    int length = inSponsorships.Length;
    int num = length - itemCount1;
    this.sponsorsGrid.itemPrefab.SetActive(true);
    for (int index = 0; index < num; ++index)
    {
      UITravelSponsorSelectEntry listItem = this.sponsorsGrid.CreateListItem<UITravelSponsorSelectEntry>();
      listItem.toggle.isOn = false;
      this.highlightAnimators.Add(listItem.highlightAnimator);
      listItem.OnStart();
    }
    this.sponsorsGrid.itemPrefab.SetActive(false);
    int itemCount2 = this.sponsorsGrid.itemCount;
    for (int inIndex = 0; inIndex < itemCount2; ++inIndex)
    {
      UITravelSponsorSelectEntry sponsorSelectEntry = this.sponsorsGrid.GetItem<UITravelSponsorSelectEntry>(inIndex);
      sponsorSelectEntry.gameObject.SetActive(inIndex < length);
      if (sponsorSelectEntry.gameObject.activeSelf)
      {
        SponsorshipDeal inSponsorship = inSponsorships[inIndex];
        sponsorSelectEntry.Setup(inSponsorship);
        sponsorSelectEntry.toggle.isOn = inSponsorship == this.screen.selectedSponsorshipDeal;
      }
    }
  }

  private void SetLastRaceEventsResults()
  {
    int eventNumber = Game.instance.player.team.championship.eventNumber;
    bool qualifyingBasedActive = Game.instance.player.team.championship.rules.qualifyingBasedActive;
    this.objectiveLabel.text = Localisation.LocaliseID(!qualifyingBasedActive ? "PSG_10011041" : "PSG_10008005", (GameObject) null);
    for (int index = 0; index < this.qualifyingEntries.Length; ++index)
      GameUtility.SetActive(this.qualifyingEntries[index].gameObject, qualifyingBasedActive);
    int index1 = eventNumber - 1;
    if (index1 >= 0)
      this.SetLabels(this.lastGpData, Game.instance.player.team.championship.calendar[index1]);
    else
      this.ResetLabels(this.lastGpData);
    int index2 = index1 - 1;
    if (index2 >= 0)
      this.SetLabels(this.secontToLastGp, Game.instance.player.team.championship.calendar[index2]);
    else
      this.ResetLabels(this.secontToLastGp);
    int index3 = index2 - 1;
    if (index3 >= 0)
      this.SetLabels(this.thirdToLastGp, Game.instance.player.team.championship.calendar[index3]);
    else
      this.ResetLabels(this.thirdToLastGp);
  }

  private void SetLabels(TextMeshProUGUI[] inLabels, RaceEventDetails inEventDetails)
  {
    RaceEventResults.ResultData playerDriverResult1 = inEventDetails.results.GetResultsForSession(SessionDetails.SessionType.Race).GetBestPlayerDriverResult();
    RaceEventResults.ResultData playerDriverResult2 = inEventDetails.results.GetResultsForSession(SessionDetails.SessionType.Qualifying).GetBestPlayerDriverResult();
    inLabels[0].text = Localisation.LocaliseID(inEventDetails.circuit.locationNameID, (GameObject) null);
    if (inLabels[1].gameObject.activeSelf)
      inLabels[1].text = GameUtility.FormatForPosition(playerDriverResult2.position, (string) null);
    inLabels[2].text = GameUtility.FormatForPosition(playerDriverResult1.position, (string) null);
  }

  private void ResetLabels(TextMeshProUGUI[] inLabels)
  {
    inLabels[0].text = "-";
    if (inLabels[1].gameObject.activeSelf)
      inLabels[1].text = "-";
    inLabels[2].text = "-";
  }

  private SponsorshipDeal[] GetFilteredSponsors()
  {
    this.mSponsorshipDeals.Clear();
    SponsorshipDeal[] array = Game.instance.player.team.sponsorController.sponsorshipDeals.ToArray();
    for (int index = 0; index < array.Length; ++index)
    {
      if (array[index].hasRaceBonusReward)
        this.mSponsorshipDeals.Add(array[index]);
    }
    switch (this.mFilter)
    {
      case UITravelSponsorSelect.Filter.Name:
        this.mSponsorshipDeals.Sort((Comparison<SponsorshipDeal>) ((x, y) => string.Compare(x.sponsor.name, y.sponsor.name)));
        break;
      case UITravelSponsorSelect.Filter.Objective:
        this.mSponsorshipDeals.Sort((Comparison<SponsorshipDeal>) ((x, y) => x.raceObjective.targetResult.CompareTo(y.raceObjective.targetResult)));
        break;
      case UITravelSponsorSelect.Filter.TotalBonus:
        this.mSponsorshipDeals.Sort((Comparison<SponsorshipDeal>) ((x, y) => x.GetObjectivesTotalBonus().CompareTo(y.GetObjectivesTotalBonus())));
        break;
    }
    if (!this.mFilterAsc)
      this.mSponsorshipDeals.Reverse();
    return this.mSponsorshipDeals.ToArray();
  }

  public void OnFilter(UITravelSponsorSelect.Filter inFilter)
  {
    this.mFilterAsc = this.mFilter == inFilter ? !this.mFilterAsc : inFilter == UITravelSponsorSelect.Filter.Name;
    if (this.mFilter != inFilter)
      this.mFilter = inFilter;
    this.SetFilter(inFilter);
    this.toggleGroup.SetAllTogglesOff();
    this.SetGrid();
  }

  private void SetFilter(UITravelSponsorSelect.Filter inFilter)
  {
    for (int index = 0; index < this.tableHeaders.Length; ++index)
    {
      UITravelTableHeader tableHeader = this.tableHeaders[index];
      if (tableHeader.filter == inFilter)
        tableHeader.Setup(this.mFilterAsc);
      else
        tableHeader.Reset();
    }
  }

  public override bool IsReady()
  {
    if (this.mSponsorshipDeals.Count != 0)
      return this.screen.selectedSponsorshipDeal != null;
    return true;
  }

  public enum Filter
  {
    Name,
    Objective,
    TotalBonus,
  }
}
