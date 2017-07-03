// Decompiled with JetBrains decompiler
// Type: RacePodiumScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;

public class RacePodiumScreen : UIScreen
{
  public UIRacePodiumWinnersWidget winnersWidget;
  public UIRacePodiumTrophyWidget sceneWidget;

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    App.instance.cameraManager.ActivateMode(CameraManager.CameraMode.PostSession);
    App.instance.cameraManager.gameCamera.SetBlurActive(true);
    this.winnersWidget.Setup();
    this.sceneWidget.Setup();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionResults, 0.0f);
  }

  public override void OnExit()
  {
    base.OnExit();
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    if (!Game.instance.isCareer)
      return UIScreen.NavigationButtonEvent.LetGameStateHandle;
    UIManager.instance.ChangeScreen("DriverStandingsScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }
}
