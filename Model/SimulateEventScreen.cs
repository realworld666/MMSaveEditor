// Decompiled with JetBrains decompiler
// Type: SimulateEventScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimulateEventScreen : UIScreen
{
  private float mDuration = 4f;
  public Slider progressSlider;
  public TextMeshProUGUI descriptionLabel;
  public UISimulateEventEntry[] championshipEntries;
  private Championship[] mChampionships;
  private float mTimer;

  public override void OnStart()
  {
    this.canEnterPreferencesScreen = false;
    base.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = false;
    this.mChampionships = Game.instance.championshipManager.GetChampionshipsRacingToday(false, Game.instance.player.GetChampionshipSeries());
    this.mTimer = 0.0f;
    UIManager.instance.ClearBackStack();
    if (Game.instance.sessionManager.isCircuitLoaded)
    {
      Game.instance.sessionManager.SetCircuitActive(true);
      App.instance.cameraManager.gameCamera.SetBlurActive(true);
      App.instance.cameraManager.ActivateMode(CameraManager.CameraMode.SimulatingSession);
    }
    for (int index = 0; index < this.championshipEntries.Length; ++index)
      GameUtility.SetActive(this.championshipEntries[index].gameObject, false);
    for (int index = 0; index < this.mChampionships.Length; ++index)
    {
      if (index < this.championshipEntries.Length)
      {
        Championship mChampionship = this.mChampionships[index];
        GameUtility.SetActive(this.championshipEntries[mChampionship.championshipID].gameObject, true);
        this.championshipEntries[mChampionship.championshipID].Setup(mChampionship);
      }
    }
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionScreen, 0.0f);
  }

  public override void OnExit()
  {
    base.OnExit();
    App.instance.cameraManager.gameCamera.SetBlurActive(false);
    UIManager.instance.ClearNavigationStacks();
  }

  private void Update()
  {
    this.mTimer += GameTimer.deltaTime;
    this.progressSlider.value = Mathf.Clamp01(this.mTimer / this.mDuration);
    if ((double) this.mTimer <= (double) this.mDuration || App.instance.gameStateManager.isChangingState)
      return;
    UIManager.instance.ChangeScreen("EventSummaryScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }
}
