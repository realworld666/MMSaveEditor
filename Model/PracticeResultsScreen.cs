﻿// Decompiled with JetBrains decompiler
// Type: PracticeResultsScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class PracticeResultsScreen : UIScreen
{
  public TextMeshProUGUI title;
  public TextMeshProUGUI lapRecordLabel;
  public UIQualifyPracticeTable table;

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    App.instance.cameraManager.ActivateMode(CameraManager.CameraMode.PostSession);
    this.table.CreateTable();
    this.title.text = Game.instance.time.now.Year.ToString() + " " + Localisation.LocaliseID(Game.instance.sessionManager.eventDetails.circuit.locationNameID, (GameObject) null) + " - " + Game.instance.sessionManager.championship.GetChampionshipName(false);
    UIManager.instance.ClearBackStack();
    Game.instance.sessionManager.SetCircuitActive(true);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionScreen, 0.0f);
    scMusicController.PostRaceLoop2();
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    if (Game.instance.gameType == Game.GameType.SingleEvent)
      return base.OnContinueButton();
    UIManager.instance.ChangeScreen("MediaScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }
}