// Decompiled with JetBrains decompiler
// Type: UITopBarTeam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UITopBarTeam : MonoBehaviour
{
  public GameObject colorStripe;
  public UITeamLogo teamLogo;
  public GameObject gameLogo;
  private Team playerTeam;

  private void Awake()
  {
  }

  private void OnEnable()
  {
    this.playerTeam = (Team) null;
  }

  private void Update()
  {
    if (Game.IsActive())
    {
      if (Game.instance.player.team != this.playerTeam)
      {
        this.playerTeam = Game.instance.player.team;
        this.teamLogo.SetTeam(this.playerTeam);
      }
      GameUtility.SetActive(this.teamLogo.gameObject, this.playerTeam != null);
    }
    else
      GameUtility.SetActive(this.teamLogo.gameObject, false);
    GameUtility.SetActive(this.gameLogo, !this.teamLogo.gameObject.activeSelf);
    GameUtility.SetActive(this.colorStripe, this.teamLogo.gameObject.activeSelf);
  }
}
