// Decompiled with JetBrains decompiler
// Type: CarPartFittingScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CarPartFittingScreen : UIScreen
{
  public UIPartFittingPanelWidget driver1Panel;
  public UIPartFittingPanelWidget driver2Panel;
  public UIPartFittingItemListWidget itemListWidget;
  public UIRadarGraphWidget graph;
  public UIRadarGraphWidget graphGT;
  public Toggle graphPerformanceToggle;
  public Toggle graphReliabilityToggle;
  public GameObject headerPerformance;
  public GameObject headerReliability;
  public Button fitPartsButton;
  public Animator panelsAnimator;
  public TextMeshProUGUI driver1LegendLabel;
  public TextMeshProUGUI driver2LegendLabel;

  public override void OnStart()
  {
    base.OnStart();
    this.fitPartsButton.onClick.AddListener(new UnityAction(this.OnFitPartsButton));
    this.graphPerformanceToggle.isOn = true;
    this.graphPerformanceToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnPerformanceToggle));
    this.graphReliabilityToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnReliabilityToggle));
    this.PreloadScene();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    Driver driver1 = Game.instance.player.team.GetDriver(0);
    Driver driver2 = Game.instance.player.team.GetDriver(1);
    this.driver1Panel.Setup(driver1);
    this.driver2Panel.Setup(driver2);
    this.showNavigationBars = true;
    this.itemListWidget.Setup();
    this.driver1LegendLabel.text = driver1.name;
    this.driver2LegendLabel.text = driver2.name;
    this.SetGraph();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionFactory, 0.0f);
  }

  private void PreloadScene()
  {
    if (!Game.IsActive() || Game.instance.player.IsUnemployed())
      return;
    Driver driver1 = Game.instance.player.team.GetDriver(0);
    Driver driver2 = Game.instance.player.team.GetDriver(1);
    this.driver1Panel.Setup(driver1);
    this.driver2Panel.Setup(driver2);
    this.itemListWidget.Setup();
    this.SetGraph();
  }

  public void RefreshCarInventoryWidgets()
  {
    this.driver1Panel.partsInventory.Refresh();
    this.driver2Panel.partsInventory.Refresh();
    this.driver1Panel.driverInfo.UpdateData();
    this.driver2Panel.driverInfo.UpdateData();
    this.itemListWidget.UpdateUnfitedParts();
    this.SetGraph();
  }

  public void Update()
  {
    if (!(App.instance.gameStateManager.currentState is TravelArrangementsState))
    {
      this.needsPlayerConfirmation = false;
    }
    else
    {
      this.needsPlayerConfirmation = !Game.instance.player.team.carManager.BothCarsReadyForEvent();
      if (App.instance.gameStateManager.currentState is TravelArrangementsState && this.needsPlayerConfirmation)
      {
        this.continueButtonLabel = Localisation.LocaliseID("PSG_10002122", (GameObject) null);
        this.continueButtonLowerLabel = Localisation.LocaliseID("PSG_10010987", (GameObject) null);
      }
      else
      {
        if (!(App.instance.gameStateManager.currentState is TravelArrangementsState))
          return;
        this.continueButtonLabel = Localisation.LocaliseID("PSG_10002122", (GameObject) null);
        this.continueButtonLowerLabel = string.Empty;
      }
    }
  }

  private void OnFitPartsButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.panelsAnimator.SetTrigger(AnimationHashes.ShowPartFittingPanel);
    this.itemListWidget.Open(CarPart.PartType.Brakes);
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    return base.OnContinueButton();
  }

  public override void OpenConfirmDialogBox(Action inAction)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CarPartFittingScreen.\u003COpenConfirmDialogBox\u003Ec__AnonStorey6B boxCAnonStorey6B = new CarPartFittingScreen.\u003COpenConfirmDialogBox\u003Ec__AnonStorey6B();
    // ISSUE: reference to a compiler-generated field
    boxCAnonStorey6B.inAction = inAction;
    // ISSUE: reference to a compiler-generated field
    boxCAnonStorey6B.\u003C\u003Ef__this = this;
    GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
    Action inCancelAction = (Action) (() => {});
    // ISSUE: reference to a compiler-generated method
    Action inConfirmAction = new Action(boxCAnonStorey6B.\u003C\u003Em__D0);
    string inTitle = Localisation.LocaliseID("PSG_10003942", (GameObject) null);
    string inText = Localisation.LocaliseID("PSG_10003943", (GameObject) null);
    string inCancelString = Localisation.LocaliseID("PSG_10003944", (GameObject) null);
    string inConfirmString = Localisation.LocaliseID("PSG_10003945", (GameObject) null);
    dialog.Show(inCancelAction, inCancelString, inConfirmAction, inConfirmString, inText, inTitle);
  }

  private void SetGraph()
  {
    if (this.graphGT.gameObject.activeSelf)
    {
      this.graphGT.graphStat = !this.graphPerformanceToggle.isOn ? UIRadarGraphWidget.Stat.Reliability : UIRadarGraphWidget.Stat.Performance;
      this.graphGT.UpdateGraphData();
    }
    if (this.graph.gameObject.activeSelf)
    {
      this.graph.graphStat = !this.graphPerformanceToggle.isOn ? UIRadarGraphWidget.Stat.Reliability : UIRadarGraphWidget.Stat.Performance;
      this.graph.UpdateGraphData();
    }
    GameUtility.SetActive(this.headerPerformance, this.graphPerformanceToggle.isOn);
    GameUtility.SetActive(this.headerReliability, this.graphReliabilityToggle.isOn);
  }

  private void OnPerformanceToggle(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!inValue)
      return;
    this.SetGraph();
  }

  private void OnReliabilityToggle(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!inValue)
      return;
    this.SetGraph();
  }
}
