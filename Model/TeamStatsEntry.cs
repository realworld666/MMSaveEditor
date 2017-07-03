// Decompiled with JetBrains decompiler
// Type: TeamStatsEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamStatsEntry : MonoBehaviour
{
  public GameObject backingPlayer;
  public Image teamStrip;
  public TextMeshProUGUI teamName;
  public TextMeshProUGUI teamPosition;

  public void Setup(Team inTeam, int inPosition)
  {
    this.teamStrip.color = inTeam.GetTeamColor().primaryUIColour.normal;
    this.teamName.text = inTeam.name;
    this.teamPosition.text = GameUtility.FormatForPosition(inPosition, (string) null);
    GameUtility.SetActive(this.backingPlayer, inTeam.IsPlayersTeam());
  }
}
