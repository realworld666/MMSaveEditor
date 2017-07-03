// Decompiled with JetBrains decompiler
// Type: TeamStandingsScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class TeamStandingsScreen : UIScreen
{
  public TextMeshProUGUI title;
  public TextMeshProUGUI roundsLabel;
  public UIConstructorsTable constructorsTable;

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.constructorsTable.CreateTable();
    Championship championship = Game.instance.sessionManager.championship;
    StringVariableParser.intValue1 = championship.eventNumberForUI;
    StringVariableParser.intValue2 = championship.calendar.Count;
    this.roundsLabel.text = Localisation.LocaliseID("PSG_10010814", (GameObject) null);
    this.title.text = Localisation.LocaliseID(Game.instance.sessionManager.eventDetails.circuit.locationNameID, (GameObject) null);
    Game.instance.sessionManager.SetCircuitActive(true);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionTeam, 0.0f);
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    UIManager.instance.ChangeScreen("MediaScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }
}
