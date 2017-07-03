// Decompiled with JetBrains decompiler
// Type: UICreatePlayerProfileWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICreatePlayerProfileWidget : MonoBehaviour
{
  public Button generateButton;
  public UICharacterPortrait portrait;
  public Flag playerFlag;
  public TextMeshProUGUI playerName;
  public CreatePlayerScreen screen;

  public void OnStart()
  {
    this.generateButton.onClick.RemoveAllListeners();
    this.generateButton.onClick.AddListener(new UnityAction(this.screen.AutoGeneratePortrait));
  }

  public void Setup()
  {
    this.RefreshWidget();
    this.RefreshPortrait();
  }

  public void RefreshWidget()
  {
    this.playerFlag.SetNationality(Game.instance.player.nationality);
    this.playerName.text = this.screen.detailsWidget.firstNameInput.text + " " + this.screen.detailsWidget.lastNameInput.text;
  }

  public void RefreshPortrait()
  {
    int age = Game.instance.player.GetAge();
    int inTeamID = -1;
    TeamColor inTeamColor = (TeamColor) null;
    if (Game.IsActive() && !Game.instance.player.IsUnemployed())
    {
      inTeamID = Game.instance.player.team.teamID;
      inTeamColor = Game.instance.player.team.GetTeamColor();
    }
    this.portrait.SetPortrait(this.screen.playerPortrait, this.screen.playerGender, age, inTeamID, inTeamColor, UICharacterPortraitBody.BodyType.Chairman, -1, -1);
  }
}
