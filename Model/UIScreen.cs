// Decompiled with JetBrains decompiler
// Type: UIScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

public class UIScreen : MonoBehaviour
{
  public string screenName = string.Empty;
  public string screenSubtitle = string.Empty;
  public string movieFallBack2DMode = string.Empty;
  public string movieFallBack2DModeGT = string.Empty;
  private bool mShowCoreNavigation = true;
  private bool mCanEnterPreferencesScreen = true;
  private bool mCanUseScreenHotkeys = true;
  private UIScreen.ScreenMode mScreenMode = UIScreen.ScreenMode.Mode3D;
  private UIManager.ScreenSet mScreenSet = UIManager.ScreenSet.NotSet;
  public UIBackground.Type frontendBackground;
  public UIBackground.Type eventBackground;
  public UIBackground.Type simulationBackground;
  protected bool dontAddToBackStack;
  private Entity mData;
  private bool mHasFocus;
  private bool mNeedsPlayerConfirmation;
  private bool mCanUseEscapeButton;
  private bool mHideMMDropdownButtons;
  private UIWidget[] mWidgets;
  private Canvas[] mCanvases;
  private UIScreen.RequiredContinueButton mRequiredContinueButton;

  public bool showCoreNavigation
  {
    get
    {
      return this.mShowCoreNavigation;
    }
    protected set
    {
      this.mShowCoreNavigation = value;
    }
  }

  public bool canEnterPreferencesScreen
  {
    get
    {
      return this.mCanEnterPreferencesScreen;
    }
    set
    {
      this.mCanEnterPreferencesScreen = value;
    }
  }

  public bool hasFocus
  {
    get
    {
      return this.mHasFocus;
    }
    set
    {
      this.mHasFocus = value;
    }
  }

  public bool needsPlayerConfirmation
  {
    get
    {
      return this.mNeedsPlayerConfirmation;
    }
    set
    {
      this.mNeedsPlayerConfirmation = value;
    }
  }

  public bool canUseEscapeButton
  {
    get
    {
      return this.mCanUseEscapeButton;
    }
    set
    {
      this.mCanUseEscapeButton = value;
    }
  }

  public bool canUseScreenHotkeys
  {
    get
    {
      return this.mCanUseScreenHotkeys;
    }
    set
    {
      this.mCanUseScreenHotkeys = value;
    }
  }

  public bool hideMMDropdownButtons
  {
    get
    {
      return this.mHideMMDropdownButtons;
    }
    set
    {
      this.mHideMMDropdownButtons = value;
    }
  }

  public bool showNavigationBars
  {
    set
    {
      UIManager.instance.navigationBars.ShowNavigationBars(value);
    }
  }

  public UIScreen.ScreenMode screenMode
  {
    get
    {
      return this.mScreenMode;
    }
  }

  public UIManager.ScreenSet screenSet
  {
    get
    {
      return this.mScreenSet;
    }
    set
    {
      this.mScreenSet = value;
    }
  }

  public string continueButtonLabel
  {
    set
    {
      UIManager.instance.navigationBars.bottomBar.continueButtonText.text = value;
    }
  }

  public string continueButtonUpperLabel
  {
    set
    {
      UIManager.instance.navigationBars.bottomBar.continueButtonUpperText.text = value;
    }
  }

  public string continueButtonLowerLabel
  {
    set
    {
      UIManager.instance.navigationBars.bottomBar.continueButtonLowerText.text = value;
    }
  }

  public bool continueButtonInteractable
  {
    set
    {
      UIManager.instance.navigationBars.SetContinueInteractable(value);
    }
  }

  public bool playerActionButtonInteractable
  {
    set
    {
      UIManager.instance.navigationBars.SetPlayerActionContinueInteractable(value);
    }
  }

  public string screenNameLabel
  {
    set
    {
      UIManager.instance.navigationBars.topBar.screenNameLabel.text = value;
    }
  }

  public string screenSubtitleLabel
  {
    set
    {
      UIManager.instance.navigationBars.topBar.screenSubtitleLabel.text = value;
    }
  }

  public Entity data
  {
    get
    {
      return this.mData;
    }
    set
    {
      this.mData = value;
    }
  }

  public Canvas[] canvases
  {
    get
    {
      return this.mCanvases;
    }
  }

  public UIScreen.RequiredContinueButton requiredContinueButton
  {
    get
    {
      return this.mRequiredContinueButton;
    }
  }

  private void Awake()
  {
    this.mCanvases = this.GetComponentsInChildren<Canvas>();
    CanvasScaler component = this.gameObject.GetComponent<CanvasScaler>();
    if ((UnityEngine.Object) component != (UnityEngine.Object) null)
      component.matchWidthOrHeight = 0.5f;
    this.mWidgets = this.GetComponentsInChildren<UIWidget>(true);
    if (this.mWidgets == null)
      return;
    for (int index = 0; index < this.mWidgets.Length; ++index)
      this.mWidgets[index].OnStart(this);
  }

  public virtual void OnStart()
  {
    this.mScreenMode = !App.instance.preferencesManager.videoPreferences.isRunning2DMode ? UIScreen.ScreenMode.Mode3D : UIScreen.ScreenMode.Mode2D;
    if (this.mCanvases != null && this.mCanvases.Length > 0)
    {
      for (int index = 0; index < this.mCanvases.Length; ++index)
      {
        if (this.mCanvases[index].renderMode == UnityEngine.RenderMode.ScreenSpaceOverlay)
          UIManager.instance.SetupCanvasCamera(this.mCanvases[index]);
      }
    }
    ActivateForGameState[] componentsInChildren1 = this.GetComponentsInChildren<ActivateForGameState>(true);
    if (componentsInChildren1 != null)
    {
      for (int index = 0; index < componentsInChildren1.Length; ++index)
        componentsInChildren1[index].OnStart();
    }
    ActivateForSession[] componentsInChildren2 = this.GetComponentsInChildren<ActivateForSession>(true);
    if (componentsInChildren2 != null)
    {
      for (int index = 0; index < componentsInChildren2.Length; ++index)
        componentsInChildren2[index].OnStart();
    }
    UIGridList[] componentsInChildren3 = this.GetComponentsInChildren<UIGridList>(true);
    if (componentsInChildren3 != null)
    {
      for (int index = 0; index < componentsInChildren3.Length; ++index)
        componentsInChildren3[index].OnStart();
    }
    UITutorial[] componentsInChildren4 = this.GetComponentsInChildren<UITutorial>(true);
    if (componentsInChildren4 != null)
    {
      for (int index = 0; index < componentsInChildren4.Length; ++index)
      {
        if (!componentsInChildren4[index].gameObject.activeSelf)
          ;
      }
    }
    if (this.mCanvases == null || this.mCanvases.Length <= 0)
      return;
    for (int index = 0; index < this.mCanvases.Length; ++index)
    {
      foreach (Selectable componentsInChild in this.mCanvases[index].GetComponentsInChildren<Selectable>(true))
      {
        Navigation navigation = componentsInChild.navigation;
        navigation.mode = Navigation.Mode.None;
        componentsInChild.navigation = navigation;
      }
    }
  }

  public virtual void OnUnload()
  {
    UILocaliseLabel[] componentsInChildren1 = this.GetComponentsInChildren<UILocaliseLabel>(true);
    if (componentsInChildren1 != null)
    {
      for (int index = 0; index < componentsInChildren1.Length; ++index)
        componentsInChildren1[index].OnUnload();
    }
    ActivateForGameState[] componentsInChildren2 = this.GetComponentsInChildren<ActivateForGameState>(true);
    if (componentsInChildren2 != null)
    {
      for (int index = 0; index < componentsInChildren2.Length; ++index)
        componentsInChildren2[index].OnUnload();
    }
    ActivateForSession[] componentsInChildren3 = this.GetComponentsInChildren<ActivateForSession>(true);
    if (componentsInChildren3 == null)
      return;
    for (int index = 0; index < componentsInChildren3.Length; ++index)
      componentsInChildren3[index].OnUnload();
  }

  public virtual void OnEnter()
  {
    this.mScreenMode = !App.instance.preferencesManager.videoPreferences.isRunning2DMode ? UIScreen.ScreenMode.Mode3D : UIScreen.ScreenMode.Mode2D;
    Animator[] componentsInChildren = this.GetComponentsInChildren<Animator>(true);
    if (componentsInChildren != null && App.instance.gameStateManager.currentState.IsSimulation())
    {
      for (int index = 0; index < componentsInChildren.Length; ++index)
        componentsInChildren[index].updateMode = AnimatorUpdateMode.UnscaledTime;
    }
    GameUtility.SetActive(this.gameObject, true);
    this.continueButtonInteractable = true;
    this.playerActionButtonInteractable = true;
    this.canUseEscapeButton = true;
    this.continueButtonUpperLabel = string.Empty;
    this.continueButtonLowerLabel = string.Empty;
    this.continueButtonLabel = Localisation.LocaliseID("PSG_10002713", (GameObject) null);
    UIManager.instance.SetDefaultNavigationState();
    this.screenNameLabel = Localisation.LocaliseID(this.screenName, (GameObject) null);
    this.screenSubtitleLabel = !string.IsNullOrEmpty(this.screenSubtitle) ? Localisation.LocaliseID(this.screenSubtitle, (GameObject) null) : string.Empty;
    this.ShowCancelButton(false);
    this.SetRequiredContinueButton(UIScreen.RequiredContinueButton.ContinueOrMustRespond);
    UILocaliseLabel.ForceRefreshOnLateUpdate();
  }

  public virtual void OnExit()
  {
    this.mData = (Entity) null;
    GameUtility.SetActive(this.gameObject, false);
  }

  public virtual void OnExit(UIScreen inNextScreen)
  {
  }

  public virtual void OnFocus()
  {
  }

  public virtual UIScreen.NavigationButtonEvent OnContinueButton()
  {
    return UIScreen.NavigationButtonEvent.LetGameStateHandle;
  }

  public virtual UIScreen.NavigationButtonEvent OnCancelButton()
  {
    return UIScreen.NavigationButtonEvent.LetGameStateHandle;
  }

  public virtual UIScreen.NavigationButtonEvent OnBackButton()
  {
    return UIScreen.NavigationButtonEvent.LetGameStateHandle;
  }

  public void SetTopBarMode(UITopBar.Mode inMode)
  {
    UIManager.instance.navigationBars.topBar.SetMode(inMode);
  }

  public void SetBottomBarMode(UIBottomBar.Mode inMode)
  {
    UIManager.instance.navigationBars.bottomBar.SetMode(inMode);
  }

  public bool CanAddToBackStack()
  {
    return !this.dontAddToBackStack;
  }

  public virtual void OpenConfirmDialogBox(Action inAction)
  {
  }

  public void ShowCancelButton(bool inShow)
  {
    UIManager.instance.navigationBars.bottomBar.ShowCancelButton(inShow);
  }

  protected void SetRequiredContinueButton(UIScreen.RequiredContinueButton inRequiredContinueButton)
  {
    this.mRequiredContinueButton = inRequiredContinueButton;
    UIManager.instance.navigationBars.bottomBar.MarkContinueButtonForUpdate();
  }

  public enum NavigationButtonEvent
  {
    LetGameStateHandle,
    HandledByScreen,
  }

  public enum RequiredContinueButton
  {
    ContinueOrMustRespond,
    StartRace,
  }

  public enum ScreenMode
  {
    Mode2D,
    Mode3D,
  }
}
