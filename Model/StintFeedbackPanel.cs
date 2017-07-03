// Decompiled with JetBrains decompiler
// Type: StintFeedbackPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StintFeedbackPanel : HUDPanel
{
  public TextMeshProUGUI lapTimeLabel;
  public GameObject upArrow;
  public GameObject downArrow;
  public SetupKnowledgeFeedbackEntry aerodynamicsFeedback;
  public SetupKnowledgeFeedbackEntry speedBalanceFeedback;
  public SetupKnowledgeFeedbackEntry handlingFeedback;
  public GameObject timeSetGroup;
  public GameObject noTimeSetGroup;

  private void OnEnable()
  {
    if (this.vehicle == null)
      return;
    List<SetupStintData> setupStintData = Game.instance.persistentEventData.GetSetupStintData(this.vehicle);
    if (setupStintData.Count > 0)
    {
      int index1 = setupStintData.Count - 1;
      this.lapTimeLabel.text = GameUtility.GetLapTimeText(setupStintData[index1].averageLapTime, false);
      GameUtility.SetActive(this.timeSetGroup, !MathsUtility.ApproximatelyZero(setupStintData[index1].averageLapTime));
      GameUtility.SetActive(this.noTimeSetGroup, !this.timeSetGroup.activeSelf);
      if (setupStintData.Count == 1)
      {
        GameUtility.SetActive(this.upArrow, true);
        GameUtility.SetActive(this.downArrow, false);
      }
      else
      {
        int index2 = index1 - 1;
        if ((double) setupStintData[index1].averageLapTime < (double) setupStintData[index2].averageLapTime)
        {
          GameUtility.SetActive(this.upArrow, true);
          GameUtility.SetActive(this.downArrow, false);
        }
        else
        {
          GameUtility.SetActive(this.upArrow, false);
          GameUtility.SetActive(this.downArrow, true);
        }
      }
      this.aerodynamicsFeedback.SetOpinion(setupStintData[index1].aerodynamicsOpinion);
      this.speedBalanceFeedback.SetOpinion(setupStintData[index1].speedBalanceOpinion);
      this.handlingFeedback.SetOpinion(setupStintData[index1].handlingOpinion);
    }
    else
    {
      this.lapTimeLabel.text = 0.0f.ToString("0.0", (IFormatProvider) Localisation.numberFormatter);
      GameUtility.SetActive(this.upArrow, false);
      GameUtility.SetActive(this.downArrow, false);
      GameUtility.SetActive(this.timeSetGroup, false);
      GameUtility.SetActive(this.noTimeSetGroup, true);
      this.aerodynamicsFeedback.SetOpinion(SessionSetup.SetupOpinion.None);
      this.speedBalanceFeedback.SetOpinion(SessionSetup.SetupOpinion.None);
      this.handlingFeedback.SetOpinion(SessionSetup.SetupOpinion.None);
    }
  }
}
