// Decompiled with JetBrains decompiler
// Type: MMDropdown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MMDropdown : MonoBehaviour
{
  [SerializeField]
  private RectTransform[] autoHideTransforms;
  [SerializeField]
  private Button saveButton;
  [SerializeField]
  private Button saveAsButton;
  [SerializeField]
  private Button loadButton;
  [SerializeField]
  private Button preferencesButton;
  [SerializeField]
  private Button quitToMainMenuButton;
  [SerializeField]
  private Button exitButton;
  [SerializeField]
  private Button skipSessionButton;
  [SerializeField]
  private Button cancelTutorialButton;
  private bool mHasMouseEnteredRect;

  private void Start()
  {
    this.saveButton.onClick.AddListener(new UnityAction(this.OnSaveButton));
    this.saveAsButton.onClick.AddListener(new UnityAction(this.OnSaveAsButton));
    this.loadButton.onClick.AddListener(new UnityAction(this.OnLoadButton));
    this.preferencesButton.onClick.AddListener(new UnityAction(this.OnPreferencesButton));
    this.quitToMainMenuButton.onClick.AddListener(new UnityAction(this.OnQuitToMainMenuButton));
    this.exitButton.onClick.AddListener(new UnityAction(this.OnExitButton));
    this.skipSessionButton.onClick.AddListener(new UnityAction(this.OnSkipSessionButton));
    this.cancelTutorialButton.onClick.AddListener(new UnityAction(this.OnCancelTutorialButton));
  }

  private void OnSaveButton()
  {
    this.gameObject.SetActive(false);
    App.instance.saveSystem.ManualSave();
  }

  private void OnSaveAsButton()
  {
    this.gameObject.SetActive(false);
    SaveLoadDialog dialog = UIManager.instance.dialogBoxManager.GetDialog<SaveLoadDialog>();
    dialog.mode = SaveLoadDialog.Mode.Save;
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
  }

  private void OnLoadButton()
  {
    this.gameObject.SetActive(false);
    SaveLoadDialog.ShowLoadDialogueOrAskForConfirmation();
  }

  private void OnPreferencesButton()
  {
    this.gameObject.SetActive(false);
    scSoundManager.Instance.Pause(false, false, false);
    UIManager.instance.ChangeScreen("PreferencesScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  private void OnQuitToMainMenuButton()
  {
    this.gameObject.SetActive(false);
    bool flag1 = Game.IsActive() && Game.instance.isCareer && App.instance.saveSystem.NeedSaveConfirmation();
    bool flag2 = !flag1 && Game.IsActive() && !Game.instance.isCareer;
    Action toMainMenuAction = MMDropdown.GetQuitToMainMenuAction();
    if (flag1 || flag2)
    {
      GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
      string inTitle = !flag1 ? Localisation.LocaliseID("PSG_10009094", (GameObject) null) : Localisation.LocaliseID("PSG_10009093", (GameObject) null);
      string inText = !flag1 ? Localisation.LocaliseID("PSG_10009096", (GameObject) null) : Localisation.LocaliseID("PSG_10009095", (GameObject) null);
      string inCancelString = Localisation.LocaliseID("PSG_10009077", (GameObject) null);
      string inConfirmString = Localisation.LocaliseID("PSG_10009078", (GameObject) null);
      dialog.Show(MMDropdown.GetCancelQuitToMainMenuAction(), inCancelString, toMainMenuAction, inConfirmString, inText, inTitle);
      scSoundManager.Instance.Pause(true, false, false);
    }
    else
      toMainMenuAction();
  }

  private void OnExitButton()
  {
    this.gameObject.SetActive(false);
    bool flag = Game.IsActive() && Game.instance.isCareer && App.instance.saveSystem.NeedSaveConfirmation();
    Action inConfirmAction = (Action) (() =>
    {
      scMusicController.Stop();
      scSoundManager.Instance.SessionEnd(false);
      Application.Quit();
    });
    Action inCancelAction = (Action) (() => scSoundManager.Instance.UnPause(false));
    GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
    string inTitle = !flag ? Localisation.LocaliseID("PSG_10009094", (GameObject) null) : Localisation.LocaliseID("PSG_10009093", (GameObject) null);
    string inText = !flag ? Localisation.LocaliseID("PSG_10009096", (GameObject) null) : Localisation.LocaliseID("PSG_10009095", (GameObject) null);
    string inCancelString = Localisation.LocaliseID("PSG_10009077", (GameObject) null);
    string inConfirmString = Localisation.LocaliseID("PSG_10009078", (GameObject) null);
    scSoundManager.Instance.Pause(true, false, false);
    dialog.Show(inCancelAction, inCancelString, inConfirmAction, inConfirmString, inText, inTitle);
  }

  private void OnSkipSessionButton()
  {
    Action inConfirmAction = (Action) (() =>
    {
      (App.instance.gameStateManager.GetState(GameState.Type.SkipSession) as SkipSessionState).sessionStarted = true;
      App.instance.gameStateManager.SetState(GameState.Type.SkipSession, GameStateManager.StateChangeType.CheckForFadedScreenChange, false);
      scSoundManager.Instance.SessionEnd(false);
    });
    scSoundManager.Instance.Pause(true, false, false);
    UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>().Show((Action) null, Localisation.LocaliseID("PSG_10003115", (GameObject) null), inConfirmAction, Localisation.LocaliseID("PSG_10003949", (GameObject) null), Localisation.LocaliseID("PSG_10009102", (GameObject) null), Localisation.LocaliseID("PSG_10009103", (GameObject) null));
  }

  private void OnCancelTutorialButton()
  {
    Game.instance.tutorialSystem.CancelAndResetTutorial(true);
  }

  private void Update()
  {
    this.UpdateButtonStatus();
    this.CheckForMouseAwayFromPanel();
  }

  public static bool ShouldShowSaveButton()
  {
    bool flag = Game.IsActive() && Game.instance.isCareer && !(App.instance.gameStateManager.currentState is SkipSessionState);
    if (flag)
    {
      Team team = Game.instance.player.team;
      if (team != null)
      {
        PreSeasonTesting preSeasonTesting = team.championship.preSeasonTesting;
        if (preSeasonTesting != null && preSeasonTesting.state == PreSeasonTesting.State.Running)
          flag = false;
      }
    }
    return flag;
  }

  public static bool ShouldShowLoadButton()
  {
    return !(UIManager.instance.currentScreen is PreferencesScreen);
  }

  private void UpdateButtonStatus()
  {
    bool inIsActive = !UIManager.instance.currentScreen.hideMMDropdownButtons;
    bool flag1 = MMDropdown.ShouldShowSaveButton();
    bool flag2 = MMDropdown.ShouldShowLoadButton();
    GameUtility.SetActive(this.saveButton.gameObject, flag1 && inIsActive);
    GameUtility.SetActive(this.saveAsButton.gameObject, flag1 && inIsActive);
    GameUtility.SetActive(this.loadButton.gameObject, flag2 && inIsActive);
    GameUtility.SetActive(this.preferencesButton.gameObject, UIManager.instance.currentScreen.canEnterPreferencesScreen && inIsActive);
    GameUtility.SetActive(this.quitToMainMenuButton.gameObject, inIsActive);
    GameUtility.SetActive(this.exitButton.gameObject, true);
    GameUtility.SetActive(this.skipSessionButton.gameObject, App.instance.gameStateManager.currentState.IsSimulation() && Game.instance.sessionManager.eventDetails.currentSession.sessionType != SessionDetails.SessionType.Race && !Game.instance.sessionManager.IsSessionEnding() && !Game.instance.sessionManager.isSkippingSession && inIsActive && !(UIManager.instance.currentScreen is PreferencesScreen));
    GameUtility.SetActive(this.cancelTutorialButton.gameObject, Game.IsActive() && Game.instance.tutorialSystem.isTutorialActive && inIsActive);
  }

  private void CheckForMouseAwayFromPanel()
  {
    if (this.autoHideTransforms == null || this.autoHideTransforms.Length <= 0)
      return;
    bool flag = false;
    List<RaycastResult> objectsAtMousePosition = UIManager.instance.UIObjectsAtMousePosition;
    for (int index1 = 0; index1 < objectsAtMousePosition.Count && !flag; ++index1)
    {
      for (int index2 = 0; index2 < this.autoHideTransforms.Length && !flag; ++index2)
      {
        if (GameUtility.IsParentInHierarchy(objectsAtMousePosition[index1].gameObject.transform, (Transform) this.autoHideTransforms[index2]))
          flag = true;
      }
    }
    if (!this.mHasMouseEnteredRect && flag)
      this.mHasMouseEnteredRect = true;
    if (!this.mHasMouseEnteredRect || flag)
      return;
    this.gameObject.SetActive(false);
  }

  public static Action GetQuitToMainMenuAction()
  {
    return (Action) (() =>
    {
      UIManager.instance.dialogBoxManager.OnQuitToMainMenu();
      App.instance.gameStateManager.SetState(GameState.Type.Null, GameStateManager.StateChangeType.ChangeInstantly, false);
      UIManager.instance.LeaveScreenEarlyReadyForNextScreen();
      UIManager.instance.navigationBars.ShowNavigationBars(false);
      scMusicController.Stop();
      scSoundManager.Instance.SessionEnd(false);
      if (Game.instance != null)
      {
        Game.instance.Destroy();
        Game.instance = (Game) null;
        if (Game.OnGameDataChanged != null)
          Game.OnGameDataChanged();
      }
      App.instance.gameStateManager.LoadToTitleScreen(GameStateManager.StateChangeType.CheckForFadedScreenChange);
    });
  }

  public static Action GetCancelQuitToMainMenuAction()
  {
    return (Action) (() => scSoundManager.Instance.UnPause(false));
  }
}
