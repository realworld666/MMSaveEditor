// Decompiled with JetBrains decompiler
// Type: InGamePauseMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InGamePauseMenu : UIDialogBox
{
  public Button preferencesButton;
  public GameObject preferencesButtonObject;
  public Button quitToMainMenuButton;
  public Button exitGamesButton;
  public GameObject cancelTutorialButtonObject;
  public Button cancelTutorialButton;
  private bool mUnpauseGameOnExit;

  public override bool pauseOnEnable
  {
    get
    {
      return false;
    }
  }

  protected override void Awake()
  {
    base.Awake();
    this.preferencesButton.onClick.AddListener(new UnityAction(this.OnPreferencesButton));
    this.quitToMainMenuButton.onClick.AddListener(new UnityAction(this.OnQuitToMainMenuButton));
    this.exitGamesButton.onClick.AddListener(new UnityAction(this.OnExitGameButton));
  }

  protected override void OnEnable()
  {
    base.OnEnable();
    this.OnOKButton += new Action(this.OnContinueButton);
    if (Game.IsActive() && Game.instance.tutorialSystem.isTutorialActive)
    {
      GameUtility.SetActive(this.cancelTutorialButtonObject, true);
      this.cancelTutorialButton.onClick.RemoveAllListeners();
      this.cancelTutorialButton.onClick.AddListener(new UnityAction(this.OnCancelTutorialButton));
    }
    else
    {
      GameUtility.SetActive(this.cancelTutorialButtonObject, false);
      this.cancelTutorialButton.onClick.RemoveAllListeners();
    }
    GameUtility.SetActive(this.preferencesButtonObject, UIManager.instance.currentScreen.canEnterPreferencesScreen);
    this.mUnpauseGameOnExit = Game.IsActive() && !Game.instance.time.isPaused;
    if (!Game.IsActive() || !this.mUnpauseGameOnExit)
      return;
    Game.instance.time.Pause(GameTimer.PauseType.Game);
    scSoundManager.Instance.Pause(false, false, false);
  }

  private void HidePauseMenu(bool inCheckUnpause = true)
  {
    base.Hide();
    if (!inCheckUnpause)
      return;
    this.CheckUnpause();
  }

  public override void Hide()
  {
    base.Hide();
    this.CheckUnpause();
  }

  private void OnContinueButton()
  {
    this.CheckUnpause();
  }

  private void OnPreferencesButton()
  {
    this.HidePauseMenu(true);
    UIManager.instance.ChangeScreen("PreferencesScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  private void OnQuitToMainMenuButton()
  {
    bool flag1 = Game.IsActive() && Game.instance.isCareer && App.instance.saveSystem.NeedSaveConfirmation();
    bool flag2 = !flag1 && Game.IsActive() && !Game.instance.isCareer;
    Action inConfirmAction = (Action) (() =>
    {
      this.HidePauseMenu(true);
      MMDropdown.GetQuitToMainMenuAction()();
    });
    Action inCancelAction = (Action) (() => this.CheckUnpause());
    if (flag1 || flag2)
    {
      GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
      string inTitle = !flag1 ? Localisation.LocaliseID("PSG_10009094", (GameObject) null) : Localisation.LocaliseID("PSG_10009093", (GameObject) null);
      string inText = !flag1 ? Localisation.LocaliseID("PSG_10009096", (GameObject) null) : Localisation.LocaliseID("PSG_10009095", (GameObject) null);
      string inCancelString = Localisation.LocaliseID("PSG_10009077", (GameObject) null);
      string inConfirmString = Localisation.LocaliseID("PSG_10009078", (GameObject) null);
      dialog.Show(inCancelAction, inCancelString, inConfirmAction, inConfirmString, inText, inTitle);
      this.HidePauseMenu(false);
    }
    else
      inConfirmAction();
  }

  private void OnExitGameButton()
  {
    bool flag = Game.IsActive() && Game.instance.isCareer && App.instance.saveSystem.NeedSaveConfirmation();
    Action inConfirmAction = (Action) (() =>
    {
      this.CheckUnpause();
      Application.Quit();
    });
    Action inCancelAction = (Action) (() => this.CheckUnpause());
    GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
    string inTitle = !flag ? Localisation.LocaliseID("PSG_10009094", (GameObject) null) : Localisation.LocaliseID("PSG_10009093", (GameObject) null);
    string inText = !flag ? Localisation.LocaliseID("PSG_10009096", (GameObject) null) : Localisation.LocaliseID("PSG_10009095", (GameObject) null);
    string inCancelString = Localisation.LocaliseID("PSG_10009077", (GameObject) null);
    string inConfirmString = Localisation.LocaliseID("PSG_10009078", (GameObject) null);
    dialog.Show(inCancelAction, inCancelString, inConfirmAction, inConfirmString, inText, inTitle);
    this.HidePauseMenu(false);
  }

  private void OnCancelTutorialButton()
  {
    Game.instance.tutorialSystem.CancelAndResetTutorial(true);
    this.HidePauseMenu(true);
  }

  private void CheckUnpause()
  {
    if (!Game.IsActive() || !this.mUnpauseGameOnExit)
      return;
    Game.instance.time.UnPause(GameTimer.PauseType.Game);
    scSoundManager.Instance.UnPause(false);
  }
}
