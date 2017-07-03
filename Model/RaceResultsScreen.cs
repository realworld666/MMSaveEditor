// Decompiled with JetBrains decompiler
// Type: RaceResultsScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceResultsScreen : UIScreen
{
  public TextMeshProUGUI title;
  public UITimedRaceTable table;
  public UIHUBSessionSponsor sponsorWidget;

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.sponsorWidget.Setup();
    App.instance.cameraManager.ActivateMode(CameraManager.CameraMode.PostSession);
    this.title.text = Game.instance.time.now.Year.ToString() + " " + Localisation.LocaliseID(Game.instance.sessionManager.eventDetails.circuit.locationNameID, (GameObject) null) + " - " + Game.instance.sessionManager.championship.GetChampionshipName(false);
    this.table.CreateTable();
    UIManager.instance.ClearBackStack();
    Game.instance.sessionManager.SetCircuitActive(true);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionResults, 0.0f);
    scMusicController.PostRaceLoop2();
  }

  private bool isPlayerTeamInPodium()
  {
    List<RaceEventResults.ResultData> resultData = Game.instance.sessionManager.eventDetails.results.GetResultsForSession(SessionDetails.SessionType.Race).resultData;
    int count = resultData.Count;
    for (int index = 0; index < 3; ++index)
    {
      if (index < count && resultData[index].driver.IsPlayersDriver())
        return true;
    }
    return false;
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    if (this.isPlayerTeamInPodium())
    {
      UIManager.instance.ChangeScreen("RacePodiumScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
      return UIScreen.NavigationButtonEvent.HandledByScreen;
    }
    if (!Game.instance.isCareer)
      return UIScreen.NavigationButtonEvent.LetGameStateHandle;
    UIManager.instance.ChangeScreen("DriverStandingsScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }
}
