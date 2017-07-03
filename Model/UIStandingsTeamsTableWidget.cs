// Decompiled with JetBrains decompiler
// Type: UIStandingsTeamsTableWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIStandingsTeamsTableWidget : MonoBehaviour
{
  private bool mCurrentStandings = true;
  public UIGridList teamsGrid;
  public TextMeshProUGUI eventNumber;
  private Championship mChampionship;
  private ChampionshipStandings mStandings;
  private ChampionshipEntry_v1 mFirstPlace;

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
    this.SetGrid();
    this.SetDetails();
  }

  private void SetGrid()
  {
    int itemCount1 = this.teamsGrid.itemCount;
    int teamEntryCount = this.mStandings.teamEntryCount;
    int num = teamEntryCount - itemCount1;
    for (int index = 0; index < num; ++index)
      this.teamsGrid.CreateListItem<UIStandingsTeamEntry>().OnStart();
    int itemCount2 = this.teamsGrid.itemCount;
    for (int inIndex = 0; inIndex < itemCount2; ++inIndex)
    {
      UIStandingsTeamEntry standingsTeamEntry = this.teamsGrid.GetItem<UIStandingsTeamEntry>(inIndex);
      standingsTeamEntry.gameObject.SetActive(inIndex < teamEntryCount);
      if (standingsTeamEntry.gameObject.activeSelf)
      {
        ChampionshipEntry_v1 teamEntry = this.mStandings.GetTeamEntry(inIndex);
        if (inIndex == 0)
          this.mFirstPlace = teamEntry;
        standingsTeamEntry.barType = inIndex % 2 != 0 ? UIStandingsTeamEntry.BarType.Darker : UIStandingsTeamEntry.BarType.Lighter;
        standingsTeamEntry.Setup(this, teamEntry, this.mCurrentStandings);
      }
      else
      {
        this.teamsGrid.DestroyListItem(standingsTeamEntry.gameObject);
        --itemCount2;
      }
    }
    if (num != 0)
      return;
    this.teamsGrid.Refresh();
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
