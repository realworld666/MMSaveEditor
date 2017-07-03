// Decompiled with JetBrains decompiler
// Type: QualifyingEliminationScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QualifyingEliminationScreen : UIScreen
{
  public TextMeshProUGUI driversEliminatedText;
  public TextMeshProUGUI driversSurvivedText;
  public UIGridList eliminatedDriverList;
  public UIGridList survivedDriverList;

  public override void OnEnter()
  {
    base.OnEnter();
    UIManager.instance.ClearBackStack();
    App.instance.cameraManager.ActivateMode(CameraManager.CameraMode.PostSession);
    this.showNavigationBars = true;
    Game.instance.sessionManager.SetCircuitActive(true);
    this.SetupDriverPortraits();
    StringVariableParser.intValue1 = Game.instance.sessionManager.eventDetails.currentSession.sessionNumberForUI;
    this.driversEliminatedText.text = Localisation.LocaliseID("PSG_10011496", (GameObject) null);
    StringVariableParser.intValue1 = Game.instance.sessionManager.eventDetails.currentSession.sessionNumberForUI + 1;
    this.driversSurvivedText.text = Localisation.LocaliseID("PSG_10011497", (GameObject) null);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionResults, 0.0f);
    scMusicController.PostRaceLoop2();
  }

  private void SetupDriverPortraits()
  {
    SessionManager sessionManager = Game.instance.sessionManager;
    RaceEventResults results = sessionManager.eventDetails.results;
    List<RaceEventResults.ResultData> resultData = results.GetAllResultsForSession(SessionDetails.SessionType.Qualifying)[sessionManager.eventDetails.currentSession.sessionNumber].resultData;
    this.survivedDriverList.DestroyListItems();
    this.eliminatedDriverList.DestroyListItems();
    int sessionNumber = sessionManager.eventDetails.currentSession.sessionNumber;
    int count = sessionManager.eventDetails.qualifyingSessions.Count;
    bool flag1 = sessionNumber == count - 1;
    int num = !flag1 || sessionNumber <= 0 ? RaceEventResults.GetPositionThreshold(sessionNumber) : RaceEventResults.GetPositionThreshold(sessionNumber - 1);
    int inIndex1 = 0;
    int inIndex2 = 0;
    float bestLapTime = resultData[num - 1].bestLapTime;
    for (int index = 0; index < resultData.Count; ++index)
    {
      Driver driver = resultData[index].driver;
      int inPosition = index + 1;
      bool flag2 = results.GetSessionIdDriverElimination(driver) == sessionNumber && !flag1;
      bool flag3 = inPosition > num;
      if (flag2)
      {
        UIEliminatedDriverIcon eliminatedDriverIcon = this.eliminatedDriverList.GetOrCreateItem<UIEliminatedDriverIcon>(inIndex1);
        float inTimeDelta = resultData[index].bestLapTime - bestLapTime;
        eliminatedDriverIcon.Setup(driver, inPosition, sessionManager.GetDriversCar(driver).setup.tyreSet, inTimeDelta);
        ++inIndex1;
      }
      else if (!flag3)
      {
        this.survivedDriverList.GetOrCreateItem<UIQualifyEliminationEntry>(inIndex2).Setup(driver, inPosition);
        ++inIndex2;
      }
    }
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    UIManager.instance.ChangeScreen("QualifyingResultsScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }
}
