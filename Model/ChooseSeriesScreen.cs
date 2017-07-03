// Decompiled with JetBrains decompiler
// Type: ChooseSeriesScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class ChooseSeriesScreen : UIScreen
{
  public UIChooseSeriesSelection selectionWidget;
  public UIChooseSeriesOverview overviewWidget;
  public GameObject tutorialOffErrorMessage;
  public GameObject getGtSeries;
  private Championship mChampionship;
  private bool mAllowMenuSounds;

  public Championship championship
  {
    get
    {
      return this.mChampionship;
    }
  }

  public override void OnStart()
  {
    base.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.mAllowMenuSounds = false;
    this.showNavigationBars = true;
    UIManager.instance.ClearForwardStack();
    this.continueButtonInteractable = false;
    this.SetTopBarMode(UITopBar.Mode.Core);
    this.SetBottomBarMode(UIBottomBar.Mode.Core);
    this.RefreshDlcObject();
    App.instance.dlcManager.OnOwnedDlcChanged += new Action(this.RefreshDlcObject);
    this.selectionWidget.Setup();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionStandings, 0.0f);
    this.mAllowMenuSounds = true;
  }

  public override void OnExit()
  {
    App.instance.dlcManager.OnOwnedDlcChanged -= new Action(this.RefreshDlcObject);
    base.OnExit();
  }

  public void SelectChampionship(Championship inChampionship)
  {
    if (inChampionship == null)
      return;
    if (this.mChampionship != inChampionship && this.mChampionship != null && this.mAllowMenuSounds)
      scSoundManager.Instance.PlaySound(SoundID.Sfx_SelectSeries, 0.0f);
    this.mChampionship = inChampionship;
    this.overviewWidget.Setup(inChampionship);
    this.continueButtonInteractable = true;
    GameUtility.SetActive(this.tutorialOffErrorMessage, !this.mChampionship.IsBaseGameChampionship && Game.instance.tutorialSystem.isTutorialActive);
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    if (this.mChampionship != null)
    {
      if (Game.instance.isCareer && !Game.instance.challengeManager.IsAttemptingChallenge() && CreateTeamManager.isCreatingTeam)
      {
        CreateTeamManager.SelectChampionship(this.mChampionship);
        MovieScreen screen = UIManager.instance.GetScreen<MovieScreen>();
        screen.PlayMovie(SoundID.Video_ChampVideo, this.mChampionship.GetIdentMovieString());
        screen.SetNextScreen("CreateTeamScreen", 1.5f, (Entity) this.mChampionship);
        UIManager.instance.ChangeScreen("MovieScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
      }
      else
      {
        MovieScreen screen = UIManager.instance.GetScreen<MovieScreen>();
        screen.PlayMovie(SoundID.Video_ChampVideo, this.mChampionship.GetIdentMovieString());
        screen.SetNextScreen("ChooseTeamScreen", 1.5f, (Entity) this.mChampionship);
        UIManager.instance.ChangeScreen("MovieScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
      }
    }
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }

  private void RefreshDlcObject()
  {
    GameUtility.SetActive(this.getGtSeries, !App.instance.dlcManager.IsDlcInstalled(DLCManager.GetDlcByName("New Championship").appId));
  }
}
