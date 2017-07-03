// Decompiled with JetBrains decompiler
// Type: MiniStandingsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MiniStandingsWidget : MonoBehaviour
{
  public TextMeshProUGUI roundNumberLabel;
  public Button standingsButton;
  public Toggle driversButton;
  public GameObject driversTable;
  public UIGridList driversGrid;
  public Toggle teamsButton;
  public GameObject teamsTable;
  public UIGridList teamsGrid;
  private MiniStandingsWidget.Table mTable;
  private Championship mChampionship;

  private void Awake()
  {
    if ((UnityEngine.Object) this.standingsButton != (UnityEngine.Object) null)
      this.standingsButton.onClick.AddListener(new UnityAction(this.OnStandingsButton));
    this.driversButton.onValueChanged.AddListener(new UnityAction<bool>(this.OnDriversButton));
    this.teamsButton.onValueChanged.AddListener(new UnityAction<bool>(this.OnTeamsButton));
  }

  public void OnEnter(Championship inChampionship)
  {
    this.driversButton.isOn = true;
    this.teamsButton.isOn = false;
    this.mChampionship = inChampionship;
    this.PopulateDriverStandings();
    this.PopulateTeamStandings();
    if ((UnityEngine.Object) this.roundNumberLabel != (UnityEngine.Object) null)
    {
      StringVariableParser.intValue1 = this.mChampionship.eventNumberForUI;
      StringVariableParser.intValue2 = this.mChampionship.eventCount;
      this.roundNumberLabel.text = Localisation.LocaliseID("PSG_10002217", (GameObject) null);
    }
    this.SetTable(MiniStandingsWidget.Table.Drivers);
  }

  public void OnExit()
  {
    this.driversGrid.DestroyListItems();
    this.teamsGrid.DestroyListItems();
  }

  private void PopulateDriverStandings()
  {
    int driverEntryCount = this.mChampionship.standings.driverEntryCount;
    for (int inIndex = 0; inIndex < driverEntryCount; ++inIndex)
    {
      ChampionshipEntry_v1 driverEntry = this.mChampionship.standings.GetDriverEntry(inIndex);
      MiniDriverStandingsEntry listItem = this.driversGrid.CreateListItem<MiniDriverStandingsEntry>();
      if (inIndex % 2 == 0)
        listItem.SetBarType(MiniDriverStandingsEntry.BarType.Lighter);
      else
        listItem.SetBarType(MiniDriverStandingsEntry.BarType.Darker);
      listItem.SetChampionshipEntry(driverEntry);
    }
    this.driversGrid.ResetScrollbar();
  }

  private void PopulateTeamStandings()
  {
    int teamEntryCount = this.mChampionship.standings.teamEntryCount;
    for (int inIndex = 0; inIndex < teamEntryCount; ++inIndex)
    {
      ChampionshipEntry_v1 teamEntry = this.mChampionship.standings.GetTeamEntry(inIndex);
      MiniTeamStandingsEntry listItem = this.teamsGrid.CreateListItem<MiniTeamStandingsEntry>();
      if (inIndex % 2 == 0)
        listItem.SetBarType(MiniTeamStandingsEntry.BarType.Lighter);
      else
        listItem.SetBarType(MiniTeamStandingsEntry.BarType.Darker);
      listItem.SetChampionshipEntry(teamEntry);
    }
    this.teamsGrid.ResetScrollbar();
  }

  public void SetTable(MiniStandingsWidget.Table inTable)
  {
    this.mTable = inTable;
    switch (this.mTable)
    {
      case MiniStandingsWidget.Table.Drivers:
        this.driversTable.SetActive(true);
        this.teamsTable.SetActive(false);
        this.driversButton.isOn = true;
        this.teamsButton.isOn = false;
        break;
      case MiniStandingsWidget.Table.Teams:
        this.driversTable.SetActive(false);
        this.teamsTable.SetActive(true);
        this.driversButton.isOn = false;
        this.teamsButton.isOn = true;
        break;
    }
  }

  public void OnDriversButton(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!inValue)
      return;
    this.SetTable(MiniStandingsWidget.Table.Drivers);
  }

  public void OnTeamsButton(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!inValue)
      return;
    this.SetTable(MiniStandingsWidget.Table.Teams);
  }

  public void OnStandingsButton()
  {
    UIManager.instance.ChangeScreen("StandingsScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  public enum Table
  {
    Drivers,
    Teams,
  }
}
