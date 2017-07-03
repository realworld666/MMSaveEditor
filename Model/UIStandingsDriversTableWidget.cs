// Decompiled with JetBrains decompiler
// Type: UIStandingsDriversTableWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIStandingsDriversTableWidget : MonoBehaviour
{
  private bool mCurrentStandings = true;
  public UIGridList driversGrid;
  public TextMeshProUGUI eventNumber;
  private Championship mChampionship;
  private ChampionshipStandings mStandings;
  private ChampionshipEntry_v1 mFirstPlace;
  private ChampionshipStandingsHistoryEntry mHistoryEntry;

  public ChampionshipEntry_v1 firstPlace
  {
    get
    {
      return this.mFirstPlace;
    }
  }

  public void Setup(ChampionshipStandings inStandings)
  {
    if (inStandings == null)
      return;
    this.mStandings = inStandings;
    this.mChampionship = this.mStandings.championship;
    this.mCurrentStandings = this.mChampionship.standings == this.mStandings;
    this.mHistoryEntry = !this.mCurrentStandings ? this.mChampionship.standingsHistory.GetEntry(this.mStandings) : (ChampionshipStandingsHistoryEntry) null;
    this.SetGrid();
    this.SetDetails();
  }

  private void SetGrid()
  {
    int itemCount1 = this.driversGrid.itemCount;
    int driverEntryCount = this.mStandings.driverEntryCount;
    int num = driverEntryCount - itemCount1;
    for (int index = 0; index < num; ++index)
      this.driversGrid.CreateListItem<UIStandingsDriverEntry>().OnStart();
    int itemCount2 = this.driversGrid.itemCount;
    for (int inIndex = 0; inIndex < itemCount2; ++inIndex)
    {
      UIStandingsDriverEntry standingsDriverEntry = this.driversGrid.GetItem<UIStandingsDriverEntry>(inIndex);
      GameUtility.SetActive(standingsDriverEntry.gameObject, inIndex < driverEntryCount);
      if (standingsDriverEntry.gameObject.activeSelf)
      {
        ChampionshipEntry_v1 driverEntry = this.mStandings.GetDriverEntry(inIndex);
        if (inIndex == 0)
          this.mFirstPlace = driverEntry;
        standingsDriverEntry.barType = inIndex % 2 != 0 ? UIStandingsDriverEntry.BarType.Darker : UIStandingsDriverEntry.BarType.Lighter;
        standingsDriverEntry.Setup(this, driverEntry, this.mCurrentStandings, this.mHistoryEntry);
      }
      else
      {
        this.driversGrid.DestroyListItem(standingsDriverEntry.gameObject);
        --inIndex;
        --itemCount2;
      }
    }
    this.driversGrid.Refresh();
  }

  private void SetDetails()
  {
    GameUtility.SetActive(this.eventNumber.gameObject, this.mCurrentStandings);
    if (!this.mCurrentStandings)
      return;
    StringVariableParser.intValue1 = this.mChampionship.eventNumberForUI;
    StringVariableParser.intValue2 = this.mChampionship.eventCount;
    this.eventNumber.text = Localisation.LocaliseID("PSG_10002217", (GameObject) null);
  }
}
