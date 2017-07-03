// Decompiled with JetBrains decompiler
// Type: UIEventCalendarSelectionWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class UIEventCalendarSelectionWidget : MonoBehaviour
{
  public UIGridList eventsGrid;
  public UIChampionshipLogo championshipLogo;
  public EventCalendarScreen screen;
  private Championship mChampionship;

  public void Setup(Championship inChampionship)
  {
    if (inChampionship == null)
      return;
    scSoundManager.BlockSoundEvents = true;
    this.mChampionship = inChampionship;
    this.SetGrid();
    this.championshipLogo.SetChampionship(this.mChampionship);
    scSoundManager.BlockSoundEvents = false;
  }

  public void SetGrid()
  {
    List<RaceEventDetails> calendar = this.mChampionship.calendar;
    RaceEventDetails currentEventDetails = this.mChampionship.GetCurrentEventDetails();
    int count = calendar.Count;
    int itemCount1 = this.eventsGrid.itemCount;
    int num = count - itemCount1;
    this.eventsGrid.itemPrefab.SetActive(true);
    for (int index = 0; index < num; ++index)
    {
      UIEventCalendarEntry listItem = this.eventsGrid.CreateListItem<UIEventCalendarEntry>();
      listItem.toggle.onValueChanged.RemoveAllListeners();
      listItem.toggle.isOn = false;
      listItem.OnStart();
    }
    this.eventsGrid.itemPrefab.SetActive(false);
    int itemCount2 = this.eventsGrid.itemCount;
    for (int index = 0; index < itemCount2; ++index)
    {
      UIEventCalendarEntry eventCalendarEntry = this.eventsGrid.GetItem<UIEventCalendarEntry>(index);
      eventCalendarEntry.gameObject.SetActive(index < count);
      if (eventCalendarEntry.gameObject.activeSelf)
      {
        RaceEventDetails inRaceEvent = calendar[index];
        eventCalendarEntry.Setup(inRaceEvent, index);
        if (index == this.screen.nextEvent || this.screen.nextEvent == -1 && inRaceEvent == currentEventDetails)
        {
          if (!eventCalendarEntry.toggle.isOn)
            eventCalendarEntry.toggle.isOn = true;
          else
            eventCalendarEntry.OnToggle();
        }
      }
    }
  }
}
