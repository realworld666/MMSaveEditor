// Decompiled with JetBrains decompiler
// Type: UITopBar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITopBar : MonoBehaviour
{
  public TextMeshProUGUI screenNameLabel;
  public TextMeshProUGUI screenSubtitleLabel;
  public UITopBarFinance financeWidget;
  public UITopBarNextEvent nextEventWidget;
  public UITopBarPreseasonStageWidget preSeasonStageWidget;
  public UITopBarCarDesignStages carDesignStagesWidget;
  public GameObject politicsWidgetContainer;
  public UITopBarTeam teamWidget;
  public UITopBarCurrentObjective currentObjectivesWidget;
  public UITopBarChampionship championshipWidget;
  public UIBaseSessionTopBarWidget[] topBarSessionWidgets;
  public GameObject topBarSessionStandingsContainer;
  [SerializeField]
  private GameObject frontend;
  [SerializeField]
  private GameObject session;
  [SerializeField]
  private GameObject info;
  [SerializeField]
  private UIPlaybackSpeedWidget playBackWidget;
  [SerializeField]
  private Button infoButton;
  private UITopBar.Mode mMode;
  private Canvas mCanvas;

  public UITopBar.Mode mode
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

  private void Awake()
  {
    GameUtility.SetActive(this.financeWidget.gameObject, false);
    GameUtility.SetActive(this.nextEventWidget.gameObject, false);
    GameUtility.SetActive(this.preSeasonStageWidget.gameObject, false);
    GameUtility.SetActive(this.carDesignStagesWidget.gameObject, false);
    GameUtility.SetActive(this.championshipWidget.gameObject, false);
    this.nextEventWidget.SetListener();
    this.preSeasonStageWidget.SetListener();
    this.carDesignStagesWidget.SetListener();
    this.championshipWidget.SetListener();
    this.financeWidget.SetListener();
    this.currentObjectivesWidget.SetListener();
    this.mCanvas = this.transform.parent.gameObject.GetComponent<Canvas>();
    UIManager.instance.SetupCanvasCamera(this.mCanvas);
    this.SetMode(this.mMode);
    GameStateManager.OnStateChange += new Action(this.OnStateChange);
    UIManager.OnScreenChange += new Action(this.OnScreenChange);
    Animator[] componentsInChildren = this.GetComponentsInChildren<Animator>(true);
    if (componentsInChildren == null)
      return;
    for (int index = 0; index < componentsInChildren.Length; ++index)
      componentsInChildren[index].updateMode = AnimatorUpdateMode.UnscaledTime;
  }

  private void Start()
  {
    Game.OnNewGame += new Action(this.OnNewGame);
  }

  private void OnNewGame()
  {
    this.infoButton.onClick.AddListener(new UnityAction(Game.instance.helpSystem.OnInfoHelp));
    Game.instance.OnAboutToBeDestroyed += (Action) (() => this.infoButton.onClick.RemoveListener(new UnityAction(Game.instance.helpSystem.OnInfoHelp)));
  }

  public void SetMode(UITopBar.Mode inMode)
  {
    this.mMode = inMode;
    GameUtility.SetActive(this.playBackWidget.gameObject, false);
    GameUtility.SetActive(this.info, false);
    GameState currentState = App.instance.gameStateManager.currentState;
    GameUtility.SetActive(this.politicsWidgetContainer, (currentState is FrontendState || currentState is PostEventFrontendState) && !Game.instance.player.IsUnemployed());
    switch (this.mMode)
    {
      case UITopBar.Mode.Nothing:
        GameUtility.SetActive(this.session, false);
        GameUtility.SetActive(this.frontend, false);
        break;
      case UITopBar.Mode.Core:
        GameUtility.SetActive(this.session, false);
        GameUtility.SetActive(this.frontend, true);
        break;
      case UITopBar.Mode.Frontend:
        GameUtility.SetActive(this.session, false);
        GameUtility.SetActive(this.info, true);
        GameUtility.SetActive(this.playBackWidget.gameObject, true);
        GameUtility.SetActive(this.frontend, true);
        break;
      case UITopBar.Mode.Session:
        GameUtility.SetActive(this.session, true);
        GameUtility.SetActive(this.frontend, false);
        this.ActivateSessionTopBarWidgets();
        break;
      case UITopBar.Mode.DataCenter:
        GameUtility.SetActive(this.session, true);
        GameUtility.SetActive(this.frontend, false);
        this.ActivateSessionTopBarWidgets();
        break;
      case UITopBar.Mode.Hidden:
        GameUtility.SetActive(this.session, false);
        GameUtility.SetActive(this.frontend, false);
        break;
    }
  }

  private void ChangeScreen(string inScreenName)
  {
    if (!UIManager.InstanceExists)
      return;
    UIManager.instance.ChangeScreen(inScreenName, UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  public void SetActive(bool inActive)
  {
    GameUtility.SetActive(this.transform.parent.gameObject, inActive);
  }

  private void ActivateSessionTopBarWidgets()
  {
    bool isTutorialActive = Game.instance.tutorialSystem.isTutorialActive;
    UITutorial navigationBarsTutorial = UIManager.instance.navigationBars.navigationBarsTutorial;
    for (int index = 0; index < this.topBarSessionWidgets.Length; ++index)
    {
      bool inIsActive = this.topBarSessionWidgets[index].ShouldBeEnabled();
      if (!isTutorialActive || !navigationBarsTutorial.IsTutorialHidingGameObject(this.topBarSessionWidgets[index].gameObject))
        GameUtility.SetActive(this.topBarSessionWidgets[index].gameObject, inIsActive);
      if (inIsActive)
        this.topBarSessionWidgets[index].OnEnter();
    }
  }

  private void DisableSessionTopBarWidgetsDropdowns()
  {
    for (int index = 0; index < this.topBarSessionWidgets.Length; ++index)
    {
      if (this.topBarSessionWidgets[index].isDropdownActive)
        this.topBarSessionWidgets[index].ToggleDropdown(false);
    }
  }

  private void OnStateChange()
  {
    if (!Game.IsActive() || this.mMode != UITopBar.Mode.Session)
      return;
    this.ActivateSessionTopBarWidgets();
  }

  private void OnScreenChange()
  {
    if (!Game.IsActive() || this.mMode != UITopBar.Mode.Session)
      return;
    this.DisableSessionTopBarWidgetsDropdowns();
  }

  public void OnUnload()
  {
    this.preSeasonStageWidget.OnUnload();
    this.carDesignStagesWidget.OnUnload();
    this.nextEventWidget.OnUnload();
    this.financeWidget.OnUnload();
    this.championshipWidget.OnUnload();
    this.playBackWidget.OnUnload();
  }

  private void Update()
  {
  }

  public enum Mode
  {
    Nothing,
    Core,
    Frontend,
    Session,
    DataCenter,
    Hidden,
  }
}
