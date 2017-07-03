// Decompiled with JetBrains decompiler
// Type: DataCenterScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public abstract class DataCenterScreen : UIScreen
{
  public override void OnEnter()
  {
    base.OnEnter();
    Game.instance.time.Pause(GameTimer.PauseType.UI);
    App.instance.cameraManager.ActivateMode(CameraManager.CameraMode.PostSession);
    App.instance.cameraManager.gameCamera.SetBlurActive(true);
  }

  public override void OnExit()
  {
    base.OnExit();
  }
}
