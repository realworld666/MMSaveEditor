// Decompiled with JetBrains decompiler
// Type: PositionTrackerScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class PositionTrackerScreen : DataCenterScreen
{
  public UIPositionTrackerDriverToggleWidget driversWidget;
  public UIPositionTrackerGraph graphWidget;

  public override void OnStart()
  {
    base.OnStart();
    this.graphWidget.OnStart();
    this.driversWidget.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.graphWidget.Setup();
    this.driversWidget.Setup();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
    scMusicController.PostRaceLoop2();
  }

  public override void OnExit()
  {
    base.OnExit();
    UIPositionTrackerRollover.HideRollover();
  }
}
