// Decompiled with JetBrains decompiler
// Type: UIDesignPartsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIDesignPartsWidget : MonoBehaviour
{
  public Button designButton;
  public TextMeshProUGUI designDescription;
  public GameObject noWorkContainer;
  public GameObject buildingPartContainer;
  public Slider progressSlider;
  public Button cancelDesignButton;
  public TextMeshProUGUI designPartName;
  public TextMeshProUGUI designPartBuildTime;

  public void OnStart()
  {
    this.designButton.onClick.RemoveAllListeners();
    this.designButton.onClick.AddListener(new UnityAction(this.OnDesignButton));
    this.cancelDesignButton.onClick.RemoveAllListeners();
    this.cancelDesignButton.onClick.AddListener(new UnityAction(this.OnCancelDesignButton));
  }

  public void OnEnter()
  {
    CarPartDesign carPartDesign = Game.instance.player.team.carManager.carPartDesign;
    carPartDesign.OnPartBuilt -= new Action(this.UpdateDesignWidget);
    carPartDesign.OnPartBuilt += new Action(this.UpdateDesignWidget);
    this.UpdateDesignWidget();
  }

  public void OnExit()
  {
    Game.instance.player.team.carManager.carPartDesign.OnPartBuilt -= new Action(this.UpdateDesignWidget);
  }

  private void OnDesignButton()
  {
    UIManager.instance.ChangeScreen("PartDesignScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  private void OnCancelDesignButton()
  {
    Action inConfirmAction = (Action) (() =>
    {
      Game.instance.player.team.carManager.carPartDesign.Cancel();
      this.UpdateDesignWidget();
    });
    GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
    string inTitle = Localisation.LocaliseID("PSG_10010372", (GameObject) null);
    string inText = Localisation.LocaliseID("PSG_10010373", (GameObject) null);
    string inCancelString = Localisation.LocaliseID("PSG_10009077", (GameObject) null);
    string inConfirmString = Localisation.LocaliseID("PSG_10009078", (GameObject) null);
    dialog.Show((Action) null, inCancelString, inConfirmAction, inConfirmString, inText, inTitle);
  }

  private void Update()
  {
    CarPartDesign carPartDesign = Game.instance.player.team.carManager.carPartDesign;
    if (carPartDesign.stage != CarPartDesign.Stage.Designing)
      return;
    StringVariableParser.partFrontendUI = carPartDesign.part.GetPartType();
    this.designDescription.text = Localisation.LocaliseID("PSG_10010371", (GameObject) null);
    this.designPartName.text = Localisation.LocaliseEnum((Enum) carPartDesign.part.GetPartType());
    this.designPartBuildTime.text = carPartDesign.GetTimeRemainingString();
    this.progressSlider.normalizedValue = carPartDesign.GetCreationTimeElapsedNormalised();
  }

  private void UpdateDesignWidget()
  {
    CarPartDesign carPartDesign = Game.instance.player.team.carManager.carPartDesign;
    switch (carPartDesign.stage)
    {
      case CarPartDesign.Stage.Idle:
        this.designDescription.text = Localisation.LocaliseID("PSG_10010370", (GameObject) null);
        break;
      case CarPartDesign.Stage.Designing:
        StringVariableParser.partFrontendUI = carPartDesign.part.GetPartType();
        this.designDescription.text = Localisation.LocaliseID("PSG_10010371", (GameObject) null);
        this.designPartName.text = Localisation.LocaliseEnum((Enum) carPartDesign.part.GetPartType());
        this.designPartBuildTime.text = carPartDesign.GetTimeRemainingString();
        break;
    }
    this.noWorkContainer.SetActive(carPartDesign.stage == CarPartDesign.Stage.Idle);
    this.buildingPartContainer.SetActive(carPartDesign.stage == CarPartDesign.Stage.Designing);
    this.designButton.interactable = carPartDesign.stage == CarPartDesign.Stage.Idle;
  }
}
