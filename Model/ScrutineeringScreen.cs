// Decompiled with JetBrains decompiler
// Type: ScrutineeringScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;

public class ScrutineeringScreen : UIScreen
{
  public ScrutineeringWidget widget;

  public override void OnStart()
  {
    this.canEnterPreferencesScreen = false;
    base.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    Game.instance.sessionManager.ScrutinizePartRules();
    this.showNavigationBars = true;
    this.widget.Setup(this);
    App.instance.cameraManager.ActivateMode(CameraManager.CameraMode.PostSession);
    UIManager.instance.ClearBackStack();
    Game.instance.sessionManager.SetCircuitActive(true);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionTeam, 0.0f);
  }

  public void Update()
  {
    this.continueButtonInteractable = this.widget.currentState == ScrutineeringWidget.State.Complete;
  }

  public override void OnExit()
  {
    base.OnExit();
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    UIManager.instance.ChangeScreen("RaceResultsScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }
}
