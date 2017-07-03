// Decompiled with JetBrains decompiler
// Type: CreateTeamOverviewWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class CreateTeamOverviewWidget : MonoBehaviour
{
  public UITeamCreateLogo teamCreateLogo;
  public UICharacterPortrait playerPortrait;
  public TextMeshProUGUI playerAge;
  public TextMeshProUGUI playerName;
  public Flag playerFlag;
  public TextMeshProUGUI teamCountry;
  public Flag teamFlag;

  public void Setup()
  {
    this.RefreshLogo();
    this.RefreshNationality();
    this.playerAge.text = Game.instance.player.GetAge().ToString();
    this.playerName.text = Game.instance.player.name;
    this.playerFlag.SetNationality(Game.instance.player.nationality);
  }

  public void RefreshLogo()
  {
    this.playerPortrait.SetPortrait(Game.instance.player.portrait, Game.instance.player.gender, Game.instance.player.GetAge(), -1, CreateTeamManager.newTeamColor, UICharacterPortraitBody.BodyType.Chairman, CreateTeamManager.newTeam.driversHatStyle, CreateTeamManager.newTeam.driversBodyStyle);
    this.teamCreateLogo.Refresh();
  }

  public void RefreshNationality()
  {
    this.teamCountry.text = CreateTeamManager.newTeam.nationality.localisedCountry;
    this.teamFlag.SetNationality(CreateTeamManager.newTeam.nationality);
  }
}
