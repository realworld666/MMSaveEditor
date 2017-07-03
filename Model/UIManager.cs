// Decompiled with JetBrains decompiler
// Type: UIManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  public List<RaycastResult> UIObjectsAtMousePosition = new List<RaycastResult>();
  private List<GameObject> cacheObjectsAtMousePosition = new List<GameObject>();
  public string currentScreen_name = string.Empty;
  public string currentScreen_screenName = string.Empty;
  private CursorManager mCursorManager = new CursorManager();
  private Stack mBackStack = new Stack();
  private Stack mForwardStack = new Stack();
  public Camera UICamera;
  public UIBlur blur;
  public UIDialogBoxManager dialogBoxManager;
  public UITeamCustomLogoManager teamCustomLogoManager;
  private PointerEventData pointerCache;
  public TextAsset[] screenData;
  private string[][] screenNames;
  private UIScreen[][] uiScreens;
  private int[] screenCounts;
  private int[] screenLoadedCounts;
  [SerializeField]
  private Image screenFade;
  [SerializeField]
  private UIBackground background;
  private UINavigationBars mNavigationBars;
  private UIScreen mCurrentScreen;
  private UIScreenNavData mDesiredNavigation;
  private UIManager.NavigationType mNavigationType;
  private int mIsLoadingScreens;
  private static UIManager sInstance;
  private bool mHasDeferredCommand;

  public static UIManager instance
  {
    get
    {
      UIManager.EnsureInstanceExists();
      return UIManager.sInstance;
    }
  }

  public static bool InstanceExists
  {
    get
    {
      return (UnityEngine.Object) UIManager.sInstance != (UnityEngine.Object) null;
    }
  }

  public UIScreen currentScreen
  {
    get
    {
      return this.mCurrentScreen;
    }
  }

  public int backStackLength
  {
    get
    {
      return this.mBackStack.Count;
    }
  }

  public int forwardStackLength
  {
    get
    {
      return this.mForwardStack.Count;
    }
  }

  public UINavigationBars navigationBars
  {
    get
    {
      return this.mNavigationBars;
    }
  }

  public CursorManager cursorManager
  {
    get
    {
      return this.mCursorManager;
    }
  }

  public bool isScreenFadeActive
  {
    get
    {
      return this.screenFade.gameObject.activeSelf;
    }
  }

  public bool isLoadingScreens
  {
    get
    {
      return this.mIsLoadingScreens != 0;
    }
  }

  public UIManager.NavigationType navigationType
  {
    get
    {
      return this.mNavigationType;
    }
  }

  public bool canChangeScreen
  {
    get
    {
      return !this.mHasDeferredCommand;
    }
  }

  public UIBackground UIBackground
  {
    get
    {
      return this.background;
    }
  }

  public static event Action OnScreenChange;

  public static void EnsureInstanceExists()
  {
    if (!((UnityEngine.Object) UIManager.sInstance == (UnityEngine.Object) null) || !Application.isPlaying)
      return;
    GameObject go = UnityEngine.Object.Instantiate<GameObject>((GameObject) UnityEngine.Resources.Load("Prefabs/UIManager"));
    go.name = go.name.Replace("(Clone)", string.Empty);
    UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(go, FirstActiveSceneHolder.firstActiveScene);
    UIManager.sInstance = go.GetComponent<UIManager>();
  }

  private void Awake()
  {
    this.screenNames = new string[this.screenData.Length][];
    this.uiScreens = new UIScreen[this.screenData.Length][];
    this.screenCounts = new int[this.screenData.Length];
    this.screenLoadedCounts = new int[this.screenData.Length];
    for (int index = 0; index < this.screenData.Length; ++index)
    {
      string[] strArray = this.screenData[index].text.Split(new string[3]
      {
        "\r\n",
        "\r",
        "\n"
      }, StringSplitOptions.None);
      this.screenNames[index] = strArray.Length != 1 || !(strArray[0] == string.Empty) ? strArray : new string[0];
      this.screenCounts[index] = this.screenNames[index].Length;
      this.screenLoadedCounts[index] = 0;
      this.uiScreens[index] = new UIScreen[this.screenNames[index].Length];
    }
    this.screenFade.gameObject.SetActive(false);
    if ((UnityEngine.Object) App.instance != (UnityEngine.Object) null)
      App.instance.StartCoroutine(this.LoadNavigationBars());
    this.dialogBoxManager = new UIDialogBoxManager();
    this.mCursorManager.Start();
    this.teamCustomLogoManager.OnStart();
  }

  private void Update()
  {
    if (this.dialogBoxManager != null)
      this.dialogBoxManager.Update();
    this.UpdateUIObjectsAtMousePosition();
    this.mCursorManager.Update();
  }

  private void LateUpdate()
  {
    this.CheckHotkeys();
  }

  public void SetMouseCursorState(CursorManager.State inState)
  {
    this.mCursorManager.SetState(inState);
  }

  [DebuggerHidden]
  private IEnumerator LoadNavigationBars()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new UIManager.\u003CLoadNavigationBars\u003Ec__Iterator15()
    {
      \u003C\u003Ef__this = this
    };
  }

  [DebuggerHidden]
  public IEnumerator LoadScreens(UIManager.ScreenSet inScreenSet)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new UIManager.\u003CLoadScreens\u003Ec__Iterator16()
    {
      inScreenSet = inScreenSet,
      \u003C\u0024\u003EinScreenSet = inScreenSet,
      \u003C\u003Ef__this = this
    };
  }

  [DebuggerHidden]
  private IEnumerator DoScreenLoading(string[] inScreens, UIManager.ScreenSet inScreenSet)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new UIManager.\u003CDoScreenLoading\u003Ec__Iterator17()
    {
      inScreens = inScreens,
      inScreenSet = inScreenSet,
      \u003C\u0024\u003EinScreens = inScreens,
      \u003C\u0024\u003EinScreenSet = inScreenSet,
      \u003C\u003Ef__this = this
    };
  }

  private void OnScreenLoaded(string sceneName, UIManager.ScreenSet inScreenSet, int indexWithinSet)
  {
    scSoundManager.BlockSoundEvents = true;
    Scene sceneByName = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);
    GameUtility.Assert(sceneByName.isLoaded, "Scene finished loading, but scene with the same name is reported as not currently loaded", (UnityEngine.Object) null);
    GameObject[] rootGameObjects = sceneByName.GetRootGameObjects();
    for (int index = 0; index < rootGameObjects.Length; ++index)
    {
      UIScreen component = rootGameObjects[index].GetComponent<UIScreen>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null && component.screenSet == UIManager.ScreenSet.NotSet)
      {
        GameUtility.Assert((UnityEngine.Object) this.uiScreens[(int) inScreenSet][indexWithinSet] == (UnityEngine.Object) null, "Screen " + sceneName + " has more than one UIScreen object in it!", (UnityEngine.Object) null);
        this.uiScreens[(int) inScreenSet][indexWithinSet] = component;
        rootGameObjects[index].GetComponent<CanvasScaler>().matchWidthOrHeight = 0.0f;
        component.screenSet = inScreenSet;
        component.OnStart();
        component.gameObject.SetActive(false);
      }
    }
    ++this.screenLoadedCounts[(int) inScreenSet];
    GameUtility.Assert(this.screenLoadedCounts[(int) inScreenSet] <= this.screenCounts[(int) inScreenSet]);
    scSoundManager.BlockSoundEvents = false;
  }

  [DebuggerHidden]
  public IEnumerator ReloadNavigationAndDialogBoxes()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new UIManager.\u003CReloadNavigationAndDialogBoxes\u003Ec__Iterator18()
    {
      \u003C\u003Ef__this = this
    };
  }

  [DebuggerHidden]
  public IEnumerator UnloadScreenSet(UIManager.ScreenSet inScreenSet)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new UIManager.\u003CUnloadScreenSet\u003Ec__Iterator19()
    {
      inScreenSet = inScreenSet,
      \u003C\u0024\u003EinScreenSet = inScreenSet,
      \u003C\u003Ef__this = this
    };
  }

  public bool IsScreenLoaded(string inScreenName)
  {
    for (int index1 = 0; index1 < this.screenNames.Length; ++index1)
    {
      for (int index2 = 0; index2 < this.screenNames[index1].Length; ++index2)
      {
        if (this.screenNames[index1][index2] == inScreenName && (UnityEngine.Object) this.uiScreens[index1][index2] != (UnityEngine.Object) null)
          return true;
      }
    }
    return false;
  }

  public bool IsScreenSetLoaded(UIManager.ScreenSet inScreenSet)
  {
    return this.screenLoadedCounts[(int) inScreenSet] == this.screenCounts[(int) inScreenSet];
  }

  public float GetScreenSetLoadingProgress(UIManager.ScreenSet inScreenSet)
  {
    return Mathf.Clamp01((float) this.screenCounts[(int) inScreenSet] / (float) this.screenLoadedCounts[(int) inScreenSet]);
  }

  private void CheckHotkeys()
  {
    if (!Game.IsActive() || (!(this.mCurrentScreen.screenName != "Preferences") || !this.mCurrentScreen.canUseScreenHotkeys || Game.instance.tutorialSystem.isTutorialActive && !Game.instance.tutorialSystem.IsTutorialSectionComplete(TutorialSystem_v1.TutorialSection.ScreenHotkeysCheck)))
      return;
    if (InputManager.instance.GetKeyDown(KeyBinding.Name.Home))
      this.TryAccessScreenUsingHotkey("HomeScreen");
    else if (InputManager.instance.GetKeyDown(KeyBinding.Name.Profile))
      this.TryAccessScreenUsingHotkey("PlayerScreen");
    else if (InputManager.instance.GetKeyDown(KeyBinding.Name.Factory))
      this.TryAccessScreenUsingHotkey("AllDriversScreen");
    else if (InputManager.instance.GetKeyDown(KeyBinding.Name.Car))
      this.TryAccessScreenUsingHotkey("CarScreen");
    else if (InputManager.instance.GetKeyDown(KeyBinding.Name.Standings))
    {
      this.TryAccessScreenUsingHotkey("StandingsScreen");
    }
    else
    {
      if (!InputManager.instance.GetKeyDown(KeyBinding.Name.Headquarters))
        return;
      this.TryAccessScreenUsingHotkey("HeadquartersScreen");
    }
  }

  public void GamepadQuickNavigation(string inScreen)
  {
    if (!Game.IsActive() || (!(this.mCurrentScreen.screenName != "Preferences") || !this.mCurrentScreen.canUseScreenHotkeys || Game.instance.tutorialSystem.isTutorialActive && !Game.instance.tutorialSystem.IsTutorialSectionComplete(TutorialSystem_v1.TutorialSection.ScreenHotkeysCheck)))
      return;
    this.TryAccessScreenUsingHotkey(inScreen);
  }

  private void TryAccessScreenUsingHotkey(string inScreen)
  {
    if (!this.CanAccessScreenUsingHotkey(inScreen))
      return;
    this.dialogBoxManager.HideAll();
    this.ChangeScreen(inScreen, UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  private bool CanAccessScreenUsingHotkey(string inScreen)
  {
    GameState currentState = App.instance.gameStateManager.currentState;
    if (currentState is FrontendState)
      return FrontendState.PlayerCanAccessScreen(inScreen);
    if (currentState is PreSeasonState)
      return PreSeasonState.IsAtCorrectPreSeasonScreenForMailLink(inScreen);
    return false;
  }

  public bool OnContinueButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Continue, 0.0f);
    if ((UnityEngine.Object) this.mCurrentScreen != (UnityEngine.Object) null && this.mCurrentScreen.hasFocus)
      return this.mCurrentScreen.OnContinueButton() == UIScreen.NavigationButtonEvent.HandledByScreen;
    return false;
  }

  public bool OnCancelButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Back, 0.0f);
    if ((UnityEngine.Object) this.mCurrentScreen != (UnityEngine.Object) null && this.mCurrentScreen.hasFocus)
      return this.mCurrentScreen.OnCancelButton() == UIScreen.NavigationButtonEvent.HandledByScreen;
    return false;
  }

  public void OnBackButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Back, 0.0f);
    if ((UnityEngine.Object) this.mCurrentScreen != (UnityEngine.Object) null && this.mCurrentScreen.hasFocus && this.mCurrentScreen.OnBackButton() == UIScreen.NavigationButtonEvent.HandledByScreen)
      return;
    Action inAction = (Action) (() =>
    {
      if (this.mBackStack.Count <= 0)
        return;
      this.mForwardStack.Push((object) UIManager.CreateNavigationData(this.mCurrentScreen));
      this.ChangeScreen((UIScreenNavData) this.mBackStack.Pop(), UIManager.NavigationType.Back);
    });
    if ((UnityEngine.Object) this.mCurrentScreen == (UnityEngine.Object) null || !this.mCurrentScreen.needsPlayerConfirmation)
    {
      inAction();
    }
    else
    {
      if (!this.mCurrentScreen.needsPlayerConfirmation)
        return;
      this.mCurrentScreen.OpenConfirmDialogBox(inAction);
    }
  }

  public void OnForwardButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    Action inAction = (Action) (() =>
    {
      if (this.mForwardStack.Count <= 0)
        return;
      this.ChangeScreen((UIScreenNavData) this.mForwardStack.Pop(), UIManager.NavigationType.Forward);
    });
    if ((UnityEngine.Object) this.mCurrentScreen == (UnityEngine.Object) null || !this.mCurrentScreen.needsPlayerConfirmation)
    {
      inAction();
    }
    else
    {
      if (!this.mCurrentScreen.needsPlayerConfirmation)
        return;
      this.mCurrentScreen.OpenConfirmDialogBox(inAction);
    }
  }

  public void LeaveScreenEarlyReadyForNextScreen()
  {
    this.mCurrentScreen.OnExit();
    this.mCurrentScreen = (UIScreen) null;
    this.currentScreen_name = string.Empty;
    this.currentScreen_screenName = string.Empty;
  }

  public void ChangeScreen(UIScreenNavData inNavigationData, UIManager.NavigationType inNavigationType = UIManager.NavigationType.Normal)
  {
    this.ChangeScreen(inNavigationData, UIManager.ScreenTransition.None, 0.0f, (Action) null, inNavigationType);
  }

  public void ChangeScreen(string inScreenName, Entity focusEntity, UIManager.ScreenTransition inTransition = UIManager.ScreenTransition.None, float inDuration = 0.0f, UIManager.NavigationType inNavigationType = UIManager.NavigationType.Normal)
  {
    if (this.mHasDeferredCommand)
      Debug.LogErrorFormat("Prevented ChangeScreen from {0} to {1} during screen Fade", new object[2]
      {
        (object) this.currentScreen_screenName,
        (object) inScreenName
      });
    else
      this.ChangeScreen(UIManager.CreateNavigationData(this.GetScreen(inScreenName), focusEntity), inTransition, inDuration, (Action) null, inNavigationType);
  }

  public void ChangeScreen(string inScreenName, UIManager.ScreenTransition inTransition = UIManager.ScreenTransition.None, float inDuration = 0.0f, Action onScreenChangingOrAlreadyChanged = null, UIManager.NavigationType inNavigationType = UIManager.NavigationType.Normal)
  {
    if (this.mHasDeferredCommand)
      Debug.LogErrorFormat("Prevented ChangeScreen from {0} to {1} during screen Fade", new object[2]
      {
        (object) this.currentScreen_screenName,
        (object) inScreenName
      });
    else
      this.ChangeScreen(UIManager.CreateNavigationData(this.GetScreen(inScreenName)), inTransition, inDuration, onScreenChangingOrAlreadyChanged, inNavigationType);
  }

  public void ChangeScreen(UIScreenNavData inNavigationData, UIManager.ScreenTransition inTransition = UIManager.ScreenTransition.None, float inDuration = 0.0f, Action onScreenChangingOrAlreadyChanged = null, UIManager.NavigationType inNavigationType = UIManager.NavigationType.Normal)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UIManager.\u003CChangeScreen\u003Ec__AnonStorey74 screenCAnonStorey74 = new UIManager.\u003CChangeScreen\u003Ec__AnonStorey74();
    // ISSUE: reference to a compiler-generated field
    screenCAnonStorey74.inNavigationData = inNavigationData;
    // ISSUE: reference to a compiler-generated field
    screenCAnonStorey74.inNavigationType = inNavigationType;
    // ISSUE: reference to a compiler-generated field
    screenCAnonStorey74.inTransition = inTransition;
    // ISSUE: reference to a compiler-generated field
    screenCAnonStorey74.inDuration = inDuration;
    // ISSUE: reference to a compiler-generated field
    screenCAnonStorey74.onScreenChangingOrAlreadyChanged = onScreenChangingOrAlreadyChanged;
    // ISSUE: reference to a compiler-generated field
    screenCAnonStorey74.\u003C\u003Ef__this = this;
    // ISSUE: reference to a compiler-generated field
    GameUtility.Assert(screenCAnonStorey74.inNavigationData != null, "UIManager.ChangeScreen: Trying to change screen with a null UINavigationData", (UnityEngine.Object) null);
    // ISSUE: reference to a compiler-generated field
    GameUtility.Assert((UnityEngine.Object) screenCAnonStorey74.inNavigationData.screen != (UnityEngine.Object) null, "UIManager.ChangeScreen: Trying to change screen with a null UIScreen", (UnityEngine.Object) null);
    // ISSUE: reference to a compiler-generated field
    if (screenCAnonStorey74.inNavigationData.IsEqual(this.mCurrentScreen))
    {
      // ISSUE: reference to a compiler-generated field
      if (screenCAnonStorey74.onScreenChangingOrAlreadyChanged == null)
        return;
      // ISSUE: reference to a compiler-generated field
      screenCAnonStorey74.onScreenChangingOrAlreadyChanged();
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      Action inAction = new Action(screenCAnonStorey74.\u003C\u003Em__FE);
      // ISSUE: reference to a compiler-generated field
      if ((UnityEngine.Object) this.mCurrentScreen == (UnityEngine.Object) null || !this.mCurrentScreen.needsPlayerConfirmation || screenCAnonStorey74.inNavigationData.screen is PreferencesScreen)
        inAction();
      else
        this.mCurrentScreen.OpenConfirmDialogBox(inAction);
    }
  }

  [DebuggerHidden]
  private IEnumerator DoScreenTransition(UIManager.ScreenTransition inTransition, float inDuration, Action onScreenChanging)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new UIManager.\u003CDoScreenTransition\u003Ec__Iterator1A()
    {
      inTransition = inTransition,
      inDuration = inDuration,
      onScreenChanging = onScreenChanging,
      \u003C\u0024\u003EinTransition = inTransition,
      \u003C\u0024\u003EinDuration = inDuration,
      \u003C\u0024\u003EonScreenChanging = onScreenChanging,
      \u003C\u003Ef__this = this
    };
  }

  private void PerformChangeScreen(Action onScreenChanging)
  {
    this.UpdateNavigationStacks();
    if ((UnityEngine.Object) this.mCurrentScreen != (UnityEngine.Object) null)
    {
      this.mCurrentScreen.OnExit();
      this.mCurrentScreen.OnExit(this.mDesiredNavigation.screen);
    }
    this.mCurrentScreen = this.mDesiredNavigation.screen;
    this.currentScreen_name = this.mCurrentScreen.name;
    this.currentScreen_screenName = this.mCurrentScreen.screenName;
    this.mCurrentScreen.data = this.mDesiredNavigation.data;
    if (onScreenChanging != null)
      onScreenChanging();
    switch (App.instance.gameStateManager.currentState.group)
    {
      case GameState.Group.Frontend:
        this.background.SetBackground(this.mCurrentScreen.frontendBackground);
        break;
      case GameState.Group.Event:
        this.background.SetBackground(this.mCurrentScreen.eventBackground);
        break;
      case GameState.Group.Simulation:
        this.background.SetBackground(this.mCurrentScreen.simulationBackground);
        break;
      default:
        this.background.SetBackground(UIBackground.Type.None);
        break;
    }
    this.mCurrentScreen.OnEnter();
    this.mCurrentScreen.SendMessage("Update");
    if (UIManager.OnScreenChange != null)
      UIManager.OnScreenChange();
    this.mCurrentScreen.hasFocus = true;
    this.mDesiredNavigation = (UIScreenNavData) null;
  }

  private void UpdateNavigationStacks()
  {
    if ((UnityEngine.Object) this.mCurrentScreen != (UnityEngine.Object) null && this.mCurrentScreen.CanAddToBackStack())
    {
      bool flag = true;
      if (this.mForwardStack.Count > 0 && ((UIScreenNavData) this.mForwardStack.Peek()).IsEqual(this.mCurrentScreen))
        flag = false;
      if (flag)
        this.mBackStack.Push((object) UIManager.CreateNavigationData(this.mCurrentScreen));
    }
    if (this.mForwardStack.Count <= 0 || this.mNavigationType == UIManager.NavigationType.Back || !((UIScreenNavData) this.mForwardStack.Peek()).IsEqual(this.mDesiredNavigation))
      return;
    this.mForwardStack.Pop();
  }

  public void ClearBackStack()
  {
    this.mBackStack.Clear();
  }

  public void ClearForwardStack()
  {
    this.mForwardStack.Clear();
  }

  public void ClearNavigationStacks()
  {
    this.ClearBackStack();
    this.ClearForwardStack();
  }

  public void SetDefaultNavigationState()
  {
    GameState currentState = App.instance.gameStateManager.currentState;
    if (currentState is FrontendState || currentState is PreSeasonState)
    {
      this.mCurrentScreen.SetTopBarMode(UITopBar.Mode.Frontend);
      this.mCurrentScreen.SetBottomBarMode(UIBottomBar.Mode.Frontend);
    }
    else if (currentState is TravelArrangementsState)
    {
      this.mCurrentScreen.SetTopBarMode(UITopBar.Mode.Frontend);
      this.mCurrentScreen.SetBottomBarMode(UIBottomBar.Mode.Core);
    }
    else if (currentState.IsEvent() || currentState.IsSimulation())
    {
      if (currentState is PreSessionState)
      {
        this.mCurrentScreen.SetTopBarMode(UITopBar.Mode.Frontend);
        this.mCurrentScreen.SetBottomBarMode(UIBottomBar.Mode.PreSession);
      }
      else if (currentState is PostSessionState || currentState is RaceGridState)
      {
        this.mCurrentScreen.SetTopBarMode(UITopBar.Mode.Frontend);
        this.mCurrentScreen.SetBottomBarMode(UIBottomBar.Mode.Core);
      }
      else if (currentState is PreSessionHUBState)
      {
        this.mCurrentScreen.SetTopBarMode(UITopBar.Mode.Session);
        this.mCurrentScreen.SetBottomBarMode(UIBottomBar.Mode.PreSession);
      }
      else if (currentState is SessionState || currentState is SessionWinnerState)
      {
        bool flag = !(UIManager.instance.currentScreen is SessionHUD);
        this.mCurrentScreen.SetTopBarMode(!flag ? UITopBar.Mode.Session : UITopBar.Mode.DataCenter);
        this.mCurrentScreen.SetBottomBarMode(!flag ? UIBottomBar.Mode.Session : UIBottomBar.Mode.DataCenter);
      }
      else if (currentState is PostSessionDataCenterState)
      {
        this.mCurrentScreen.SetTopBarMode(UITopBar.Mode.Frontend);
        this.mCurrentScreen.SetBottomBarMode(UIBottomBar.Mode.DataCenter);
      }
      else
      {
        this.mCurrentScreen.SetTopBarMode(UITopBar.Mode.Core);
        this.mCurrentScreen.SetBottomBarMode(UIBottomBar.Mode.Core);
      }
    }
    else
    {
      this.mCurrentScreen.SetTopBarMode(UITopBar.Mode.Core);
      this.mCurrentScreen.SetBottomBarMode(UIBottomBar.Mode.Core);
    }
  }

  public bool TryGetScreen(string inScreenName, out UIScreen inUIScreen)
  {
    GameUtility.Assert(!string.IsNullOrEmpty(inScreenName));
    for (int index1 = 0; index1 < this.screenNames.Length; ++index1)
    {
      for (int index2 = 0; index2 < this.screenNames[index1].Length; ++index2)
      {
        if (this.screenNames[index1][index2] == inScreenName && (UnityEngine.Object) this.uiScreens[index1][index2] != (UnityEngine.Object) null)
        {
          inUIScreen = this.uiScreens[index1][index2];
          return true;
        }
      }
    }
    inUIScreen = (UIScreen) null;
    return false;
  }

  public UIScreen GetScreen(string inScreenName)
  {
    GameUtility.Assert(!string.IsNullOrEmpty(inScreenName));
    for (int index1 = 0; index1 < this.screenNames.Length; ++index1)
    {
      for (int index2 = 0; index2 < this.screenNames[index1].Length; ++index2)
      {
        if (this.screenNames[index1][index2] == inScreenName && (UnityEngine.Object) this.uiScreens[index1][index2] != (UnityEngine.Object) null)
          return this.uiScreens[index1][index2];
      }
    }
    Debug.LogError((object) ("Failed to find UIScreen with name " + inScreenName), (UnityEngine.Object) null);
    return (UIScreen) null;
  }

  public T GetScreen<T>() where T : UIScreen
  {
    for (int index1 = 0; index1 < this.uiScreens.Length; ++index1)
    {
      for (int index2 = 0; index2 < this.uiScreens[index1].Length; ++index2)
      {
        T obj = this.uiScreens[index1][index2] as T;
        if ((UnityEngine.Object) obj != (UnityEngine.Object) null)
          return obj;
      }
    }
    Debug.LogError((object) ("Requested UIScreen of type \"" + typeof (T).ToString() + "\" but it doesn't exist in the UIManager"), (UnityEngine.Object) null);
    return (T) null;
  }

  public bool IsScreenOpen(string inScreenName)
  {
    bool flag = false;
    if ((UnityEngine.Object) this.mCurrentScreen != (UnityEngine.Object) null)
      flag = ((object) this.mCurrentScreen).GetType().ToString() == inScreenName;
    if (!flag && this.mDesiredNavigation != null)
      flag = ((object) this.mDesiredNavigation.screen).GetType().ToString() == inScreenName;
    return flag;
  }

  public bool IsScreenOpen(UIScreen inScreen)
  {
    bool flag = false;
    if ((UnityEngine.Object) this.mCurrentScreen != (UnityEngine.Object) null)
      flag = ((object) this.mCurrentScreen).GetType() == ((object) inScreen).GetType();
    if (!flag && this.mDesiredNavigation != null)
      flag = ((object) this.mDesiredNavigation.screen).GetType() == ((object) inScreen).GetType();
    return flag;
  }

  private T LoadElement<T>(string inPrefabPath, bool inStartActive) where T : MonoBehaviour
  {
    GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate(UnityEngine.Resources.Load(inPrefabPath));
    gameObject.transform.SetParent(this.transform);
    T componentInChildren = gameObject.GetComponentInChildren<T>();
    gameObject.name = gameObject.name.Replace("(Clone)", string.Empty);
    gameObject.SetActive(inStartActive);
    return componentInChildren;
  }

  public void OnScreenRegainingFocus()
  {
    if (!((UnityEngine.Object) this.mCurrentScreen != (UnityEngine.Object) null))
      return;
    this.mCurrentScreen.OnFocus();
  }

  public void RefreshCurrentPage()
  {
    if (!((UnityEngine.Object) this.mCurrentScreen != (UnityEngine.Object) null))
      return;
    this.mCurrentScreen.OnExit();
    this.mCurrentScreen.OnEnter();
  }

  public void RefreshCurrentPage(Entity inData)
  {
    if (!((UnityEngine.Object) this.mCurrentScreen != (UnityEngine.Object) null))
      return;
    this.mCurrentScreen.OnExit();
    this.mCurrentScreen.data = inData;
    this.mCurrentScreen.OnEnter();
  }

  public static UIScreenNavData CreateNavigationData(UIScreen inScreen)
  {
    return UIManager.CreateNavigationData(inScreen, inScreen.data);
  }

  public static UIScreenNavData CreateNavigationData(UIScreen inScreen, Entity inData)
  {
    return new UIScreenNavData()
    {
      screen = inScreen,
      data = inData
    };
  }

  public void UpdateUIObjectsAtMousePosition()
  {
    if (this.pointerCache == null)
      this.pointerCache = new PointerEventData(EventSystem.current);
    this.pointerCache.Reset();
    this.pointerCache.position = (Vector2) Input.mousePosition;
    this.UIObjectsAtMousePosition.Clear();
    EventSystem.current.RaycastAll(this.pointerCache, this.UIObjectsAtMousePosition);
    this.cacheObjectsAtMousePosition.Clear();
    this.cacheObjectsAtMousePosition.Capacity = this.UIObjectsAtMousePosition.Count;
    for (int index = 0; index < this.UIObjectsAtMousePosition.Count; ++index)
      this.cacheObjectsAtMousePosition.Add(this.UIObjectsAtMousePosition[index].gameObject);
  }

  public bool IsObjectAtMousePosition(GameObject inObject)
  {
    return this.cacheObjectsAtMousePosition.Contains(inObject);
  }

  public bool IsNavigationLoaded()
  {
    return (UnityEngine.Object) this.mNavigationBars != (UnityEngine.Object) null;
  }

  public void SetupCanvasCamera(Canvas inCanvas)
  {
    if ((UnityEngine.Object) inCanvas == (UnityEngine.Object) null)
      return;
    inCanvas.renderMode = UnityEngine.RenderMode.ScreenSpaceCamera;
    inCanvas.worldCamera = this.UICamera;
  }

  [Conditional("UIMANAGER_LOGGING_ENABLED")]
  private void StateChangeLog(string message)
  {
    Debug.Log((object) ("Frame " + (object) Time.frameCount + ": " + message), (UnityEngine.Object) null);
  }

  public enum ScreenSet
  {
    Core,
    Title,
    Shared,
    Frontend,
    RaceEvent,
    Count,
    NotSet,
  }

  public enum NavigationType
  {
    Normal,
    Back,
    Forward,
  }

  public enum ScreenTransition
  {
    None,
    Fade,
    FadeFrom,
  }
}
