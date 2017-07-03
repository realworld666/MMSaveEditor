// Decompiled with JetBrains decompiler
// Type: PartDesignScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class PartDesignScreen : UIScreen
{
  private CarPart.PartType mPartType = CarPart.PartType.None;
  private ColorImageWithCurrentPartLevel[] mColorComponents = new ColorImageWithCurrentPartLevel[0];
  public UISelectPartStateOptionsWidget selectStageOptionsWidget;
  public UIBestBuiltPartStatsWidget statsWidget;
  public GameObject[] stageOneObjects;
  public GameObject[] stageTwoObjects;
  public UIComponentsChoiceWidget componentsWidget;
  public UIComponentsDetailsWidget componentsDetailWidget;
  public UIGenericCarBackground carBackground;
  public GameObject carBackgroundParent;
  public GameObject carCamera;
  private int mEngineerAccuracy;
  private PartDesignScreen.UIStage uiStage;
  private CarPartDesign mCarPartDesign;
  private StudioScene mStudioScene;

  public bool isPartSelected
  {
    get
    {
      return this.mPartType != CarPart.PartType.None;
    }
  }

  public bool allComponentsChosen
  {
    get
    {
      return !this.mCarPartDesign.part.hasComponentSlotsAvailable;
    }
  }

  public CarPart.PartType partType
  {
    get
    {
      return this.mPartType;
    }
  }

  public int engineerAccuracy
  {
    get
    {
      return this.mEngineerAccuracy;
    }
    set
    {
      this.mEngineerAccuracy = value;
    }
  }

  public override void OnStart()
  {
    base.OnStart();
    this.dontAddToBackStack = true;
    this.mColorComponents = this.gameObject.GetComponentsInChildren<ColorImageWithCurrentPartLevel>(true);
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.selectStageOptionsWidget.OnEnter();
    this.statsWidget.Setup();
    this.showNavigationBars = true;
    UIManager.instance.ClearForwardStack();
    this.mCarPartDesign = Game.instance.player.team.carManager.carPartDesign;
    this.DefaultDesign();
    this.UpdateColors();
    this.LoadScene();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionFactory, 0.0f);
  }

  public override void OnExit()
  {
    base.OnExit();
    this.UnloadScene();
  }

  private void SetupSettingOptionsWidgets()
  {
    this.componentsWidget.Setup();
    this.componentsDetailWidget.Setup();
  }

  public void UpdateColors()
  {
    for (int index = 0; index < this.mColorComponents.Length; ++index)
      this.mColorComponents[index].SetColor();
  }

  public void CancelDesign()
  {
    this.mCarPartDesign.Reset();
    this.DefaultDesign();
    this.SetPartType(CarPart.PartType.None);
    UIManager.instance.RefreshCurrentPage();
  }

  public void SetPartType(CarPart.PartType inType)
  {
    if (inType == CarPart.PartType.None)
      return;
    this.mPartType = inType;
    this.ActivateStage(PartDesignScreen.UIStage.SettingPartOptions);
  }

  public void DefaultDesign()
  {
    this.ActivateStage(PartDesignScreen.UIStage.ChoosingPartType);
    this.componentsWidget.ResetChoices();
  }

  public void StartDesign()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: reference to a compiler-generated method
    Action inConfirmAction = new Action(new PartDesignScreen.\u003CStartDesign\u003Ec__AnonStorey6F()
    {
      \u003C\u003Ef__this = this,
      partForUI = this.mCarPartDesign.part
    }.\u003C\u003Em__E7);
    if (!this.allComponentsChosen)
    {
      GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
      Action inCancelAction = (Action) (() => {});
      string inTitle = Localisation.LocaliseID("PSG_10009106", (GameObject) null);
      string inText = Localisation.LocaliseID("PSG_10005675", (GameObject) null);
      string inCancelString = Localisation.LocaliseID("PSG_10003115", (GameObject) null);
      string inConfirmString = Localisation.LocaliseID("PSG_10003949", (GameObject) null);
      UIManager.instance.dialogBoxManager.Show("GenericConfirmation");
      dialog.Show(inCancelAction, inCancelString, inConfirmAction, inConfirmString, inText, inTitle);
    }
    else
      inConfirmAction();
  }

  private void OnSucessfulTransaction()
  {
    this.mCarPartDesign.StartDesigning();
    this.DefaultDesign();
    UIManager.instance.ChangeScreen("CarScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  public override UIScreen.NavigationButtonEvent OnBackButton()
  {
    if (this.uiStage != PartDesignScreen.UIStage.SettingPartOptions)
      return UIScreen.NavigationButtonEvent.LetGameStateHandle;
    this.CancelDesign();
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    if (this.uiStage == PartDesignScreen.UIStage.ChoosingPartType)
      return UIScreen.NavigationButtonEvent.LetGameStateHandle;
    this.StartDesign();
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }

  public override UIScreen.NavigationButtonEvent OnCancelButton()
  {
    this.CancelDesign();
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }

  public void ActivateStage(PartDesignScreen.UIStage inStage)
  {
    if (inStage == PartDesignScreen.UIStage.ChoosingPartType)
    {
      this.ActivateObjects(this.stageOneObjects, true);
      this.ActivateObjects(this.stageTwoObjects, false);
      UIManager.instance.SetDefaultNavigationState();
    }
    else
    {
      this.mCarPartDesign.InitializeNewPart(this.partType);
      this.continueButtonLabel = Localisation.LocaliseID("PSG_10010945", (GameObject) null);
      this.SetBottomBarMode(UIBottomBar.Mode.PlayerAction);
      UIManager.instance.navigationBars.bottomBar.MarkContinueButtonForUpdate();
      this.ActivateObjects(this.stageOneObjects, false);
      this.ActivateObjects(this.stageTwoObjects, true);
      if (this.uiStage != inStage)
        this.SetupSettingOptionsWidgets();
    }
    this.uiStage = inStage;
  }

  private void ActivateObjects(GameObject[] inObjects, bool inValue)
  {
    for (int index = 0; index < inObjects.Length; ++index)
    {
      if (inObjects[index].activeSelf != inValue)
        inObjects[index].SetActive(inValue);
    }
  }

  private void LoadScene()
  {
    GameUtility.SetActive(this.carCamera, this.screenMode == UIScreen.ScreenMode.Mode3D);
    GameUtility.SetActive(this.carBackgroundParent, this.screenMode == UIScreen.ScreenMode.Mode2D);
    if (Game.instance.player.IsUnemployed())
      return;
    if (this.screenMode == UIScreen.ScreenMode.Mode3D)
    {
      if (SceneManager.instance.GetSceneGameObject("TrackFrontEnd").activeSelf)
        return;
      SceneManager.instance.SwitchScene("TrackFrontEnd");
      this.mStudioScene = SceneManager.instance.GetSceneGameObject("TrackFrontEnd").GetComponent<StudioScene>();
      this.mStudioScene.SetSeries(Game.instance.player.team.championship.series);
      this.mStudioScene.SetCarType(StudioScene.Car.CurrentCar);
      this.mStudioScene.SetCarVisualsToCurrentGame();
      this.mStudioScene.EnableCamera("ToTextureProfileCamera");
    }
    else
      this.carBackground.SetCar(Game.instance.player.team);
  }

  private void UnloadScene()
  {
    if (this.screenMode != UIScreen.ScreenMode.Mode3D)
      return;
    SceneManager.instance.LeaveCurrentScene();
  }

  public enum UIStage
  {
    ChoosingPartType,
    SettingPartOptions,
  }
}
