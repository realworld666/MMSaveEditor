// Decompiled with JetBrains decompiler
// Type: GridScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GridScreen : UIScreen
{
  public GameObject gridData;
  public GameObject noData;
  public UIGridList gridRowList;
  public ScrollRect scrollRect;
  public Button showButtonOne;
  public Button showButtonTwo;

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.scrollRect.verticalNormalizedPosition = 1f;
    if (App.instance.gameStateManager.currentState.type == GameState.Type.RaceGrid)
    {
      this.SetTopBarMode(UITopBar.Mode.Frontend);
      this.SetBottomBarMode(UIBottomBar.Mode.Core);
      App.instance.cameraManager.ActivateMode(CameraManager.CameraMode.PostSession);
      App.instance.cameraManager.gameCamera.SetBlurActive(true);
    }
    if (Game.instance.sessionManager.eventDetails.results.GetAllResultsForSession(SessionDetails.SessionType.Qualifying).Count > 0)
    {
      GameUtility.SetActive(this.gridData, true);
      GameUtility.SetActive(this.noData, false);
      this.SetDriversButton();
      this.SetGridWidgets();
    }
    else
    {
      GameUtility.SetActive(this.gridData, false);
      GameUtility.SetActive(this.noData, true);
    }
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionDrivers, 0.0f);
  }

  public override void OnExit()
  {
    UIManager.instance.dialogBoxManager.HideAll();
    base.OnExit();
  }

  private void SetDriversButton()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    GridScreen.\u003CSetDriversButton\u003Ec__AnonStorey71 buttonCAnonStorey71 = new GridScreen.\u003CSetDriversButton\u003Ec__AnonStorey71();
    // ISSUE: reference to a compiler-generated field
    buttonCAnonStorey71.\u003C\u003Ef__this = this;
    this.showButtonOne.onClick.RemoveAllListeners();
    this.showButtonTwo.onClick.RemoveAllListeners();
    // ISSUE: reference to a compiler-generated field
    buttonCAnonStorey71.driverOne = Game.instance.player.team.GetDriver(0);
    // ISSUE: reference to a compiler-generated field
    buttonCAnonStorey71.driverTwo = Game.instance.player.team.GetDriver(1);
    // ISSUE: reference to a compiler-generated field
    this.showButtonOne.GetComponentInChildren<TextMeshProUGUI>().text = "show " + buttonCAnonStorey71.driverOne.name;
    // ISSUE: reference to a compiler-generated method
    this.showButtonOne.onClick.AddListener(new UnityAction(buttonCAnonStorey71.\u003C\u003Em__EE));
    // ISSUE: reference to a compiler-generated field
    this.showButtonTwo.GetComponentInChildren<TextMeshProUGUI>().text = "show " + buttonCAnonStorey71.driverTwo.name;
    // ISSUE: reference to a compiler-generated method
    this.showButtonTwo.onClick.AddListener(new UnityAction(buttonCAnonStorey71.\u003C\u003Em__EF));
  }

  private void CenterOnDriver(Driver inDriver)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    float teamEntryCount = (float) Game.instance.player.team.championship.standings.teamEntryCount;
    float num = 0.0f;
    for (int inIndex = 0; inIndex < this.gridRowList.itemCount; ++inIndex)
    {
      if (this.gridRowList.GetItem<UIGridRowWidget>(inIndex).HasDriver(inDriver))
      {
        num = (float) inIndex;
        break;
      }
    }
    this.scrollRect.horizontalNormalizedPosition = num / teamEntryCount;
  }

  private void SetGridWidgets()
  {
    List<RaceEventResults.ResultData> resultData1 = Game.instance.sessionManager.eventDetails.results.GetResultsForSession(SessionDetails.SessionType.Qualifying).resultData;
    RaceEventResults.ResultData resultData2 = new RaceEventResults.ResultData();
    RaceEventResults.ResultData resultData3 = resultData1[0];
    bool flag = Game.instance.sessionManager.championship.rules.gridSetup == ChampionshipRules.GridSetup.QualifyingBased3Sessions;
    RaceEventResults.ResultData driverQualifyingData1 = Game.instance.sessionManager.eventDetails.results.GetDriverQualifyingData(resultData1[GameStatsConstants.qualifyingThresholdForQ2].driver);
    RaceEventResults.ResultData driverQualifyingData2 = Game.instance.sessionManager.eventDetails.results.GetDriverQualifyingData(resultData1[GameStatsConstants.qualifyingThresholdForQ3].driver);
    int teamEntryCount = Game.instance.player.team.championship.standings.teamEntryCount;
    for (int index = 0; index < teamEntryCount; ++index)
    {
      UIGridRowWidget uiGridRowWidget = this.gridRowList.GetOrCreateItem<UIGridRowWidget>(index);
      int driverOnePosition = index * 2;
      RaceEventResults.ResultData driverQualifyingData3 = Game.instance.sessionManager.eventDetails.results.GetDriverQualifyingData(resultData1[driverOnePosition].driver);
      RaceEventResults.ResultData driverQualifyingData4 = Game.instance.sessionManager.eventDetails.results.GetDriverQualifyingData(resultData1[driverOnePosition + 1].driver);
      if (driverQualifyingData3 == null)
        driverQualifyingData3 = resultData1[driverOnePosition];
      if (driverQualifyingData4 == null)
        driverQualifyingData4 = resultData1[driverOnePosition + 1];
      RaceEventResults.ResultData inReferenceDriverForOne = resultData3;
      RaceEventResults.ResultData inReferenceDriverForTwo = resultData3;
      if (flag && driverOnePosition >= GameStatsConstants.qualifyingThresholdForQ3)
      {
        if (driverOnePosition >= GameStatsConstants.qualifyingThresholdForQ2)
        {
          inReferenceDriverForOne = driverQualifyingData1;
          inReferenceDriverForTwo = driverQualifyingData1;
        }
        else
        {
          inReferenceDriverForOne = driverQualifyingData2;
          inReferenceDriverForTwo = driverOnePosition + 1 != GameStatsConstants.qualifyingThresholdForQ2 ? driverQualifyingData2 : driverQualifyingData1;
        }
      }
      uiGridRowWidget.SetupRow(index, driverOnePosition, inReferenceDriverForOne, inReferenceDriverForTwo, driverQualifyingData3, driverQualifyingData4);
    }
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    if (App.instance.gameStateManager.currentState.type != GameState.Type.RaceGrid)
      return base.OnContinueButton();
    UIManager.instance.ChangeScreen("SessionHUD", UIManager.ScreenTransition.Fade, 1.5f, (Action) null, UIManager.NavigationType.Normal);
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }
}
