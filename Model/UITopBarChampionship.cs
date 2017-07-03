// Decompiled with JetBrains decompiler
// Type: UITopBarChampionship
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITopBarChampionship : MonoBehaviour
{
  public Button button;
  public UIChampionshipLogo logo;
  public UIGridList eventFlagsGridList;
  public UIGridList driversGridList;
  public GameObject topDriversHeader;
  public GameObject dropdown;

  private void Awake()
  {
    this.button.onClick.AddListener(new UnityAction(this.OnButtonPressed));
  }

  public void SetListener()
  {
    UIManager.OnScreenChange += new Action(this.UpdateVisibility);
  }

  public void OnDestroy()
  {
    UIManager.OnScreenChange -= new Action(this.UpdateVisibility);
  }

  public void OnUnload()
  {
    UIManager.OnScreenChange -= new Action(this.UpdateVisibility);
  }

  private void UpdateVisibility()
  {
    if (!Game.IsActive())
      return;
    GameState currentState = App.instance.gameStateManager.currentState;
    GameUtility.SetActive(this.gameObject, !(currentState is PreSeasonState) && !currentState.IsEvent() && !currentState.IsSimulation());
  }

  private void OnEnable()
  {
    if (!Game.IsActive())
      return;
    Team team = Game.instance.player.team;
    if (team is NullTeam)
    {
      GameUtility.SetActive(this.gameObject, false);
    }
    else
    {
      this.logo.SetChampionship(team.championship);
      this.SetRolloverData();
    }
  }

  private void SetRolloverData()
  {
    Championship championship = Game.instance.player.team.championship;
    int count = championship.calendar.Count;
    int itemCount1 = this.eventFlagsGridList.itemCount;
    int num1 = count - itemCount1;
    for (int index = 0; index < num1; ++index)
      this.eventFlagsGridList.CreateListItem<Flag>();
    int itemCount2 = this.eventFlagsGridList.itemCount;
    for (int inIndex = 0; inIndex < itemCount2; ++inIndex)
    {
      Flag flag = this.eventFlagsGridList.GetItem<Flag>(inIndex);
      GameUtility.SetActive(flag.gameObject, inIndex < count);
      if (flag.gameObject.activeSelf)
        flag.SetNationality(championship.calendar[inIndex].circuit.nationality);
    }
    int driverEntryCount = championship.standings.driverEntryCount;
    int itemCount3 = this.driversGridList.itemCount;
    int num2 = driverEntryCount - itemCount3;
    if (itemCount3 < 28)
    {
      for (int index = 0; index < num2; ++index)
      {
        this.driversGridList.CreateListItem<UITopBarDriverEntry>();
        ++itemCount3;
        if (itemCount3 >= 28)
          break;
      }
    }
    int itemCount4 = this.driversGridList.itemCount;
    for (int inIndex = 0; inIndex < Mathf.Min(itemCount4, 28); ++inIndex)
    {
      UITopBarDriverEntry topBarDriverEntry = this.driversGridList.GetItem<UITopBarDriverEntry>(inIndex);
      GameUtility.SetActive(topBarDriverEntry.gameObject, inIndex < driverEntryCount);
      if (topBarDriverEntry.gameObject.activeSelf)
      {
        ChampionshipEntry_v1 driverEntry = championship.standings.GetDriverEntry(inIndex);
        topBarDriverEntry.SetChampionshipEntry(driverEntry);
      }
    }
    GameUtility.SetActive(this.topDriversHeader, itemCount4 >= 28);
  }

  public void OnButtonPressed()
  {
    if (UIManager.instance.IsScreenOpen("StandingsScreen") || App.instance.gameStateManager.currentState.group != GameState.Group.Frontend)
      return;
    UIManager.instance.ChangeScreen("StandingsScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    GameUtility.SetActive(this.dropdown, false);
  }
}
