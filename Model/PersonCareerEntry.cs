// Decompiled with JetBrains decompiler
// Type: PersonCareerEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class PersonCareerEntry : MonoBehaviour
{
  public UITeamLogo teamLogo;
  public TextMeshProUGUI yearLabel;
  public TextMeshProUGUI racesLabel;
  public TextMeshProUGUI podiumsLabel;
  public TextMeshProUGUI winsLabel;
  public TextMeshProUGUI championshipLabel;

  public void Setup(CareerHistoryEntry inEntry)
  {
    if (inEntry == null)
      return;
    this.teamLogo.SetTeam(inEntry.team);
    this.yearLabel.text = inEntry.year.ToString();
    this.racesLabel.text = inEntry.races.ToString();
    this.podiumsLabel.text = inEntry.podiums.ToString();
    this.winsLabel.text = inEntry.wins.ToString();
    this.championshipLabel.text = inEntry.championship.GetChampionshipName(false);
  }
}
