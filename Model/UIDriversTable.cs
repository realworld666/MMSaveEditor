// Decompiled with JetBrains decompiler
// Type: UIDriversTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIDriversTable : MonoBehaviour
{
  public UIGridList gridList;

  public void CreateTable()
  {
    this.gridList.DestroyListItems();
    ChampionshipStandings standings = Game.instance.sessionManager.championship.standings;
    int driverEntryCount = standings.driverEntryCount;
    ChampionshipEntry_v1 driverEntry1 = standings.GetDriverEntry(0);
    for (int inIndex = 0; inIndex < driverEntryCount; ++inIndex)
    {
      ChampionshipEntry_v1 driverEntry2 = standings.GetDriverEntry(inIndex);
      Driver entity = driverEntry2.GetEntity<Driver>();
      UIDriverStandingEntry listItem = this.gridList.CreateListItem<UIDriverStandingEntry>();
      listItem.barType = inIndex % 2 != 0 ? UIDriverStandingEntry.BarType.Darker : UIDriverStandingEntry.BarType.Lighter;
      listItem.SetInfo(entity, driverEntry2, driverEntry1);
    }
  }
}
