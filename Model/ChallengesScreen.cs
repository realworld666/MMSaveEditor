// Decompiled with JetBrains decompiler
// Type: ChallengesScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class ChallengesScreen : UIScreen
{
  public UIChallengesSelectWidget selectWidget;
  public UIChallengesOverviewWidget overviewWidget;
  private Challenge mSelectedChallenge;

  public override void OnStart()
  {
    base.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    UIManager.instance.ClearForwardStack();
    if (Game.instance != null)
    {
      Game.instance.Destroy();
      Game.instance = (Game) null;
    }
    Game.instance = new Game();
    Game.instance.gameType = Game.GameType.Career;
    Game.instance.SetupNewGame();
    Game.instance.queuedAutosave = true;
    this.selectWidget.Setup();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionFactory, 0.0f);
  }

  public void SelectChallenge(Challenge inChallenge)
  {
    this.mSelectedChallenge = inChallenge;
    this.overviewWidget.Setup(this.mSelectedChallenge);
  }

  public override UIScreen.NavigationButtonEvent OnBackButton()
  {
    Game.instance.Destroy();
    return base.OnBackButton();
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    Game.instance.StartNewGame();
    Game.instance.challengeManager.AttemptChallenge(this.mSelectedChallenge);
    UIManager.instance.ChangeScreen("NewCareerScreen", (Entity) null, UIManager.ScreenTransition.Fade, 1.5f, UIManager.NavigationType.Normal);
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }
}
