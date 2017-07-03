// Decompiled with JetBrains decompiler
// Type: AudioTimerControlsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class AudioTimerControlsWidget : MonoBehaviour
{
  private int mLastSpeed;

  private void Update()
  {
    if (Game.instance == null)
      return;
    this.mLastSpeed = (int) Game.instance.time.speed;
  }

  private void OnEnable()
  {
    if (Game.instance == null)
      return;
    Game.instance.time.OnChangeSpeed -= new Action(this.OnChangeSpeed);
    Game.instance.time.OnChangeSpeed += new Action(this.OnChangeSpeed);
  }

  private void OnDisable()
  {
    if (Game.instance == null)
      return;
    Game.instance.time.OnChangeSpeed -= new Action(this.OnChangeSpeed);
  }

  private void OnChangeSpeed()
  {
    if (Game.instance == null)
      return;
    GameState currentState = App.instance.gameStateManager.currentState;
    if (!currentState.IsFrontend() && !currentState.IsSimulation() || (Game.instance.time.timeState != GameTimer.TimeState.Standard || (GameTimer.Speed) this.mLastSpeed == Game.instance.time.speed))
      ;
  }
}
