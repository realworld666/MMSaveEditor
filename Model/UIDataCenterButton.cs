// Decompiled with JetBrains decompiler
// Type: UIDataCenterButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIDataCenterButton : UIBaseSessionTopBarWidget
{
  public Button button;

  private void Start()
  {
    this.button.onClick.AddListener(new UnityAction(this.OnButtonPressed));
  }

  public override bool ShouldBeEnabled()
  {
    GameState.Type type = App.instance.gameStateManager.currentState.type;
    GameUtility.SetInteractable(this.button, UIManager.instance.currentScreen is SessionHUD);
    return type == GameState.Type.Practice || type == GameState.Type.Qualifying || type == GameState.Type.Race;
  }

  private void Update()
  {
    if (!InputManager.instance.GetKeyUp(KeyBinding.Name.DataCentre) || !this.button.interactable)
      return;
    this.OnButtonPressed();
  }

  public void OnButtonPressed()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    bool flag = !(UIManager.instance.currentScreen is PitScreen);
    if (!scSessionAmbience.IsPaused)
    {
      UIBottomBar.ResumeAudioOnContinue = true;
      scSoundManager.Instance.Pause(true, false, false);
    }
    scSoundManager.Instance.StartTutorialPaused(false);
    if (Game.instance.sessionManager.sessionType == SessionDetails.SessionType.Race)
      UIManager.instance.ChangeScreen("PositionTrackerScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    else
      UIManager.instance.ChangeScreen("LiveTimingsScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    if (!flag)
      return;
    UIManager.instance.ClearNavigationStacks();
  }
}
