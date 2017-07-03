// Decompiled with JetBrains decompiler
// Type: RaceGridScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;

public class RaceGridScreen : UIScreen
{
  public GridDriverWidget[] driverWidget;

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    Game.instance.sessionManager.SetCircuitActive(true);
    App.instance.cameraManager.ActivateMode(CameraManager.CameraMode.FreeRoam);
    App.instance.cameraManager.SetTarget((Vehicle) Game.instance.vehicleManager.GetVehicle(Game.instance.player.team.GetDriver(0)), CameraManager.Transition.Instant);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionDrivers, 0.0f);
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    if (!UIManager.instance.IsScreenOpen("SessionHUD"))
      UIManager.instance.ChangeScreen("SessionHUD", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    Game.instance.stateInfo.GoToNextState();
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }
}
