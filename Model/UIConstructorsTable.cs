// Decompiled with JetBrains decompiler
// Type: UIConstructorsTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIConstructorsTable : MonoBehaviour
{
  public UIGridList gridList;

  public void CreateTable()
  {
    this.gridList.DestroyListItems();
    ChampionshipStandings standings = Game.instance.sessionManager.championship.standings;
    int teamEntryCount = standings.teamEntryCount;
    ChampionshipEntry_v1 teamEntry1 = standings.GetTeamEntry(0);
    for (int inIndex = 0; inIndex < teamEntryCount; ++inIndex)
    {
      ChampionshipEntry_v1 teamEntry2 = standings.GetTeamEntry(inIndex);
      Team entity = teamEntry2.GetEntity<Team>();
      UIConstructorsEntry listItem = this.gridList.CreateListItem<UIConstructorsEntry>();
      listItem.barType = inIndex % 2 != 0 ? UIConstructorsEntry.BarType.Darker : UIConstructorsEntry.BarType.Lighter;
      listItem.SetInfo(entity, teamEntry2, teamEntry1);
    }
  }
}
