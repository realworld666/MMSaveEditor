// Decompiled with JetBrains decompiler
// Type: UIBottomBar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIBottomBar : MonoBehaviour
{
  private bool mContinueButtonInteractable = true;
  private bool mPlayerActionContinueButtonInteractable = true;
  public GameObject core;
  public GameObject frontend;
  public GameObject preSession;
  public GameObject session;
  public GameObject dataCenter;
  public GameObject preferences;
  public GameObject playerAction;
  public CalendarButton calendarButton;
  public TextMeshProUGUI gameDateLabel;
  public Animator subMenu;
  [SerializeField]
  private Button backButton;
  [SerializeField]
  private Button forwardButton;
  [SerializeField]
  private Button continueButton;
  [SerializeField]
  public TextMeshProUGUI continueButtonText;
  [SerializeField]
  public TextMeshProUGUI continueButtonUpperText;
  [SerializeField]
  public TextMeshProUGUI continueButtonLowerText;
  [SerializeField]
  private Button cancelButton;
  [SerializeField]
  private Button mustRespondButton;
  [SerializeField]
  private Button raceButton;
  [SerializeField]
  private Button pauseButton;
  [SerializeField]
  private GameObject continueButtons;
  [SerializeField]
  private GameObject coreButtons;
  private UIBottomBar.Mode mMode;
  private Canvas mCanvas;
  private bool mUpdateContinueButtonState;
  private DateTime mDateLastUpdatedText;
  private string mDateFormatString;
  private string mLanguageString;

  public static bool ResumeAudioOnContinue { get; set; }

  public UIBottomBar.Mode mode
  {
    get
    {
      return this.mMode;
    }
  }

  public Canvas canvas
  {
    get
    {
      return this.mCanvas;
    }
  }

  public bool isCalendarActive
  {
    get
    {
      return this.calendarButton.isCalendarOpen;
    }
  }

  private void Awake()
  {
    this.mCanvas = this.transform.parent.gameObject.GetComponent<Canvas>();
    UIManager.instance.SetupCanvasCamera(this.mCanvas);
    this.frontend.SetActive(false);
    this.preSession.SetActive(false);
    this.dataCenter.SetActive(false);
    this.core.SetActive(false);
    this.session.SetActive(false);
    this.preferences.SetActive(false);
    this.ShowCancelButton(false);
    Animator[] componentsInChildren = this.GetComponentsInChildren<Animator>(true);
    if (componentsInChildren != null)
    {
      for (int index = 0; index < componentsInChildren.Length; ++index)
        componentsInChildren[index].updateMode = AnimatorUpdateMode.UnscaledTime;
    }
    this.SetMode(this.mMode);
  }

  private void Start()
  {
    this.backButton.onClick.AddListener(new UnityAction(UIManager.instance.OnBackButton));
    this.forwardButton.onClick.AddListener(new UnityAction(UIManager.instance.OnForwardButton));
    this.continueButton.onClick.AddListener(new UnityAction(this.OnContinueButton));
    this.mustRespondButton.onClick.AddListener(new UnityAction(this.OnReadMessagesButton));
    this.raceButton.onClick.AddListener(new UnityAction(this.OnContinueButton));
    this.pauseButton.onClick.AddListener(new UnityAction(this.OnPauseButton));
    this.cancelButton.onClick.AddListener(new UnityAction(this.OnCancelButton));
  }

  public void SetMode(UIBottomBar.Mode inMode)
  {
    this.mMode = inMode;
    this.SetCoreButtonsActive(true);
    this.SetContinueActive(true);
    switch (this.mMode)
    {
      case UIBottomBar.Mode.Nothing:
        this.frontend.SetActive(false);
        this.preSession.SetActive(false);
        this.dataCenter.SetActive(false);
        this.core.SetActive(false);
        this.session.SetActive(false);
        this.preferences.SetActive(false);
        this.playerAction.SetActive(false);
        this.ShowCancelButton(false);
        this.SetContinueActive(false);
        break;
      case UIBottomBar.Mode.Core:
        this.frontend.SetActive(false);
        this.preSession.SetActive(false);
        this.dataCenter.SetActive(false);
        this.core.SetActive(true);
        this.session.SetActive(false);
        this.preferences.SetActive(false);
        this.playerAction.SetActive(false);
        break;
      case UIBottomBar.Mode.PlayerAction:
        this.frontend.SetActive(false);
        this.preSession.SetActive(false);
        this.dataCenter.SetActive(false);
        this.core.SetActive(false);
        this.session.SetActive(false);
        this.preferences.SetActive(false);
        this.playerAction.SetActive(true);
        this.ShowCancelButton(true);
        break;
      case UIBottomBar.Mode.Frontend:
        this.frontend.SetActive(true);
        this.preSession.SetActive(false);
        this.dataCenter.SetActive(false);
        this.core.SetActive(false);
        this.session.SetActive(false);
        this.preferences.SetActive(false);
        this.playerAction.SetActive(false);
        break;
      case UIBottomBar.Mode.PreSession:
        this.frontend.SetActive(false);
        this.preSession.SetActive(true);
        this.dataCenter.SetActive(false);
        this.core.SetActive(false);
        this.session.SetActive(false);
        this.preferences.SetActive(false);
        this.playerAction.SetActive(false);
        break;
      case UIBottomBar.Mode.Session:
        this.frontend.SetActive(false);
        this.preSession.SetActive(false);
        this.dataCenter.SetActive(false);
        this.core.SetActive(false);
        this.session.SetActive(true);
        this.preferences.SetActive(false);
        this.playerAction.SetActive(false);
        this.SetCoreButtonsActive(false);
        this.SetContinueActive(false);
        break;
      case UIBottomBar.Mode.DataCenter:
        this.frontend.SetActive(false);
        this.preSession.SetActive(false);
        this.dataCenter.SetActive(true);
        this.core.SetActive(false);
        this.session.SetActive(false);
        this.preferences.SetActive(false);
        this.playerAction.SetActive(false);
        break;
      case UIBottomBar.Mode.Preferences:
        this.frontend.SetActive(false);
        this.preSession.SetActive(false);
        this.dataCenter.SetActive(false);
        this.core.SetActive(false);
        this.session.SetActive(false);
        this.preferences.SetActive(true);
        this.playerAction.SetActive(false);
        break;
    }
  }

  public void SetActive(bool inActive)
  {
    this.transform.parent.gameObject.SetActive(inActive);
  }

  public void MarkContinueButtonForUpdate()
  {
    this.mUpdateContinueButtonState = true;
  }

  private void Update()
  {
    if (!Game.IsActive() || !(Game.instance.time.now.Date != this.mDateLastUpdatedText) && !(this.mDateFormatString != App.instance.preferencesManager.gamePreferences.GetCurrentLongDateFormat()) && !(this.mLanguageString != Localisation.currentLanguage))
      return;
    this.mDateLastUpdatedText = Game.instance.time.now.Date;
    this.mDateFormatString = App.instance.preferencesManager.gamePreferences.GetCurrentLongDateFormat();
    this.mLanguageString = Localisation.currentLanguage;
    this.gameDateLabel.text = GameUtility.FormatDateTimeToLongDateString(Game.instance.time.now, string.Empty);
  }

  private void LateUpdate()
  {
    UIScreen currentScreen = UIManager.instance.currentScreen;
    GameUtility.SetInteractable(this.backButton, UIManager.instance.backStackLength > 0 && currentScreen.showCoreNavigation);
    GameUtility.SetInteractable(this.forwardButton, UIManager.instance.forwardStackLength > 0 && currentScreen.showCoreNavigation);
    if (!this.mUpdateContinueButtonState)
      return;
    this.continueButton.onClick.RemoveAllListeners();
    GameUtility.SetActive(this.continueButtonLowerText.gameObject, false);
    this.continueButtonLowerText.text = string.Empty;
    if (Game.IsActive())
    {
      GameState currentState = App.instance.gameStateManager.currentState;
      int count = Game.instance.notificationManager.GetNotification("UnreadMessages").count;
      bool inIsActive1 = currentScreen is MailScreen && (currentScreen as MailScreen).IsOpenMessageMustRespond();
      bool flag1 = Game.instance.messageManager.HasMessageWithMustRespond();
      bool flag2 = currentScreen is PreferencesScreen;
      PreSeasonState preSeasonState = !(App.instance.gameStateManager.currentState is PreSeasonState) ? (PreSeasonState) null : (PreSeasonState) App.instance.gameStateManager.currentState;
      if (this.mMode == UIBottomBar.Mode.PlayerAction)
      {
        GameUtility.SetActive(this.continueButton.gameObject, true);
        GameUtility.SetActive(this.mustRespondButton.gameObject, false);
        GameUtility.SetActive(this.pauseButton.gameObject, false);
        GameUtility.SetActive(this.raceButton.gameObject, false);
        this.continueButton.interactable = this.mPlayerActionContinueButtonInteractable;
        this.continueButton.onClick.AddListener(new UnityAction(this.OnContinueButton));
      }
      else if (currentState is PostEventFrontendState || flag2 || currentScreen is PitScreen)
      {
        GameUtility.SetActive(this.continueButton.gameObject, true);
        GameUtility.SetActive(this.mustRespondButton.gameObject, false);
        GameUtility.SetActive(this.pauseButton.gameObject, false);
        GameUtility.SetActive(this.raceButton.gameObject, false);
        this.continueButton.interactable = flag2 || this.mContinueButtonInteractable;
        this.continueButton.onClick.AddListener(new UnityAction(this.OnContinueButton));
      }
      else if (currentState is FrontendState && (Game.instance.time.timeState != GameTimer.TimeState.Skip && count > 0 || (inIsActive1 || flag1)))
      {
        GameUtility.SetActive(this.continueButton.gameObject, !inIsActive1);
        GameUtility.SetActive(this.mustRespondButton.gameObject, inIsActive1);
        GameUtility.SetActive(this.raceButton.gameObject, false);
        GameUtility.SetActive(this.pauseButton.gameObject, false);
        if (count > 0)
          this.continueButtonLowerText.text = Localisation.LocaliseID("PSG_10011047", (GameObject) null);
        else if (inIsActive1 || flag1)
          this.continueButtonLowerText.text = Localisation.LocaliseID("PSG_10011048", (GameObject) null);
        this.continueButton.interactable = true;
        this.continueButton.onClick.AddListener(new UnityAction(this.OnReadMessagesButton));
      }
      else if (preSeasonState != null && Game.instance.time.timeState != GameTimer.TimeState.Skip && PreSeasonState.IsAtCorrectPreSeasonScreenForMailLink(currentScreen) && (count > 0 || inIsActive1 || flag1))
      {
        GameUtility.SetActive(this.continueButton.gameObject, !inIsActive1);
        GameUtility.SetActive(this.mustRespondButton.gameObject, inIsActive1);
        GameUtility.SetActive(this.raceButton.gameObject, false);
        GameUtility.SetActive(this.pauseButton.gameObject, false);
        GameUtility.SetActive(this.continueButtonLowerText.gameObject, true);
        if (count > 0)
          this.continueButtonLowerText.text = Localisation.LocaliseID("PSG_10011047", (GameObject) null);
        else if (inIsActive1 || flag1)
          this.continueButtonLowerText.text = Localisation.LocaliseID("PSG_10011048", (GameObject) null);
        this.continueButton.interactable = Game.instance.time.timeState != GameTimer.TimeState.Skip;
        this.continueButton.onClick.AddListener(new UnityAction(this.OnReadMessagesButton));
      }
      else
      {
        bool inIsActive2 = false;
        bool inIsActive3 = false;
        bool inIsActive4 = false;
        bool inIsActive5 = false;
        if (preSeasonState != null)
        {
          bool flag3 = Game.instance.time.timeState == GameTimer.TimeState.Skip;
          inIsActive2 = !flag3;
          inIsActive5 = flag3;
          GameUtility.SetActive(this.continueButtonLowerText.gameObject, false);
          switch (preSeasonState.stage)
          {
            case PreSeasonState.PreSeasonStage.DesigningCar:
            case PreSeasonState.PreSeasonStage.ChoosingLivery:
            case PreSeasonState.PreSeasonStage.InPreSeasonTest:
            case PreSeasonState.PreSeasonStage.Finished:
              this.continueButton.onClick.AddListener(new UnityAction(this.OnContinueButton));
              break;
            default:
              this.continueButton.onClick.AddListener(new UnityAction(this.OnSkipButton));
              break;
          }
          if (this.continueButton.gameObject.activeSelf || inIsActive2)
            GameUtility.SetInteractable(this.continueButton, this.mContinueButtonInteractable && !flag3);
        }
        else if (this.mContinueButtonInteractable)
        {
          UIScreen.RequiredContinueButton requiredContinueButton = currentScreen.requiredContinueButton;
          if (Game.instance.stateInfo.isReadyToGoToRace && requiredContinueButton == UIScreen.RequiredContinueButton.StartRace)
          {
            inIsActive4 = true;
          }
          else
          {
            inIsActive2 = true;
            GameUtility.SetInteractable(this.continueButton, this.mContinueButtonInteractable);
            this.continueButton.onClick.AddListener(new UnityAction(this.OnContinueButton));
          }
        }
        else if (!this.mContinueButtonInteractable && (currentState is FrontendState || currentState is PreSeasonState))
        {
          bool flag3 = Game.instance.time.timeState == GameTimer.TimeState.Skip;
          inIsActive2 = !flag3;
          inIsActive5 = flag3;
          if (this.continueButton.gameObject.activeSelf || inIsActive2)
          {
            GameUtility.SetInteractable(this.continueButton, !flag3);
            this.continueButton.onClick.AddListener(new UnityAction(this.OnSkipButton));
          }
        }
        GameUtility.SetActive(this.continueButton.gameObject, inIsActive2);
        GameUtility.SetActive(this.mustRespondButton.gameObject, inIsActive3);
        GameUtility.SetActive(this.raceButton.gameObject, inIsActive4);
        GameUtility.SetActive(this.pauseButton.gameObject, inIsActive5);
      }
      this.continueButtonText.ForceMeshUpdate();
    }
    else
    {
      GameUtility.SetActive(this.continueButton.gameObject, true);
      GameUtility.SetActive(this.mustRespondButton.gameObject, false);
      GameUtility.SetActive(this.raceButton.gameObject, false);
      GameUtility.SetActive(this.pauseButton.gameObject, false);
      this.continueButton.interactable = this.mContinueButtonInteractable;
      this.continueButton.onClick.AddListener(new UnityAction(this.OnContinueButton));
    }
    this.mUpdateContinueButtonState = false;
    this.continueButton.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = !this.continueButton.interactable ? 0.33f : 1f;
  }

  public void OnContinueButton()
  {
    this.calendarButton.SetCalendarVisibility(false);
    if (!(App.instance.gameStateManager.currentState is PostEventFrontendState) && UIManager.instance.OnContinueButton())
      return;
    App.instance.gameStateManager.OnContinueButton();
    scSoundManager.Instance.EndTutorialPaused();
    if (!UIBottomBar.ResumeAudioOnContinue)
      return;
    scSoundManager.Instance.UnPause(false);
    UIBottomBar.ResumeAudioOnContinue = false;
  }

  public void OnCancelButton()
  {
    if (UIManager.instance.OnCancelButton())
      return;
    App.instance.gameStateManager.OnCancelButton();
  }

  public void OnPauseButton()
  {
    if (!Game.IsActive())
      return;
    scSoundManager.Instance.PlaySound(SoundID.Button_Pause, 0.0f);
    Game.instance.time.Pause(GameTimer.PauseType.Game);
  }

  public void OnSkipButton()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UIBottomBar.\u003COnSkipButton\u003Ec__AnonStorey68 buttonCAnonStorey68 = new UIBottomBar.\u003COnSkipButton\u003Ec__AnonStorey68();
    this.calendarButton.SetCalendarVisibility(false);
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    // ISSUE: reference to a compiler-generated field
    buttonCAnonStorey68.game = Game.instance;
    // ISSUE: reference to a compiler-generated method
    Action inAction = new Action(buttonCAnonStorey68.\u003C\u003Em__C7);
    if (App.instance.gameStateManager.currentState is PreSeasonState && UIManager.instance.currentScreen.needsPlayerConfirmation)
    {
      UIManager.instance.currentScreen.OpenConfirmDialogBox(inAction);
    }
    else
    {
      if (!Game.IsActive())
        return;
      inAction();
    }
  }

  public void OnReadMessagesButton()
  {
    this.calendarButton.SetCalendarVisibility(false);
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    MailScreen screen = UIManager.instance.GetScreen<MailScreen>();
    if ((UnityEngine.Object) UIManager.instance.currentScreen != (UnityEngine.Object) screen)
    {
      UIManager.instance.ChangeScreen("MailScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    }
    else
    {
      int num = (int) screen.OnContinueButton();
    }
  }

  public void SetCoreButtonsActive(bool inValue)
  {
    if (this.coreButtons.activeSelf == inValue)
      return;
    this.coreButtons.SetActive(inValue);
  }

  public void SetContinueActive(bool inValue)
  {
    if (this.continueButtons.activeSelf == inValue)
      return;
    this.continueButtons.SetActive(inValue);
  }

  public void SetContinueInteractable(bool inValue)
  {
    if (this.mContinueButtonInteractable != inValue)
      this.MarkContinueButtonForUpdate();
    this.mContinueButtonInteractable = inValue;
  }

  public void SetPlayerActionContinueInteractable(bool inValue)
  {
    if (this.mPlayerActionContinueButtonInteractable != inValue)
      this.MarkContinueButtonForUpdate();
    this.mPlayerActionContinueButtonInteractable = inValue;
  }

  public void ShowCancelButton(bool inShow)
  {
    this.cancelButton.transform.parent.gameObject.SetActive(inShow);
  }

  public void PressActionButton()
  {
    if (this.raceButton.IsActive() && this.raceButton.IsInteractable())
      this.raceButton.onClick.Invoke();
    else if (this.pauseButton.IsActive() && this.pauseButton.IsInteractable())
      this.pauseButton.onClick.Invoke();
    else if (this.mustRespondButton.IsActive() && this.mustRespondButton.IsInteractable())
    {
      this.mustRespondButton.onClick.Invoke();
    }
    else
    {
      if (!this.continueButton.IsActive() || !this.continueButton.IsInteractable())
        return;
      this.continueButton.onClick.Invoke();
    }
  }

  public enum Mode
  {
    Nothing,
    Core,
    PlayerAction,
    Frontend,
    PreSession,
    Session,
    DataCenter,
    Preferences,
  }
}
