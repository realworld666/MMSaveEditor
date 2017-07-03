// Decompiled with JetBrains decompiler
// Type: UIHUBSetup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHUBSetup : UIHUBStepOption
{
  public UIHUBSelection widget;
  public UITyreWearIcon[] tyreIcon;
  public TextMeshProUGUI[] tyreCompoundLabel;
  public TextMeshProUGUI[] fastestLapTimeLabel;
  public GameObject[] fuel;
  public TextMeshProUGUI[] fuelLabel;
  public Button[] setupButton;
  public UISetupBalanceSliderEntry[] driver1SetupSliders;
  public UISetupBalanceSliderEntry[] driver2SetupSliders;
  private RacingVehicle[] mVehicle;

  public override void OnStart()
  {
    this.setupButton[0].onClick.AddListener(new UnityAction(this.OnFirstVehicleSetupButton));
    this.setupButton[1].onClick.AddListener(new UnityAction(this.OnSecondVehicleSetupButton));
  }

  public override void Setup()
  {
    this.mVehicle = Game.instance.vehicleManager.GetPlayerVehicles();
    this.SetupBalanceSliders(this.driver1SetupSliders, this.mVehicle[0]);
    this.SetupBalanceSliders(this.driver2SetupSliders, this.mVehicle[1]);
    for (int index = 0; index < this.mVehicle.Length; ++index)
    {
      RacingVehicle inVehicle = this.mVehicle[index];
      this.tyreIcon[index].SetTyreSet(inVehicle.setup.tyreSet, this.mVehicle[index].bonuses.bonusDisplayInfo);
      this.tyreIcon[index].UpdateTyreLocking(this.mVehicle[index], true);
      this.tyreCompoundLabel[index].text = inVehicle.setup.tyreSet.GetName();
      if (Game.instance.persistentEventData.HasSetLapTime(inVehicle))
        this.fastestLapTimeLabel[index].text = GameUtility.GetLapTimeText(Game.instance.persistentEventData.GetFastestLapOfEvent(inVehicle), false);
      else
        this.fastestLapTimeLabel[index].text = Localisation.LocaliseID("PSG_10008596", (GameObject) null);
      if (Game.instance.sessionManager.sessionType == SessionDetails.SessionType.Race)
      {
        GameUtility.SetActive(this.fuel[index], true);
        int fuelLapsRemaining = inVehicle.performance.fuel.GetFuelLapsRemaining();
        StringVariableParser.intValue1 = fuelLapsRemaining;
        if (fuelLapsRemaining == 1)
          this.fuelLabel[index].text = Localisation.LocaliseID("PSG_10010645", (GameObject) null);
        else
          this.fuelLabel[index].text = Localisation.LocaliseID("PSG_10010646", (GameObject) null);
      }
      else
        GameUtility.SetActive(this.fuel[index], false);
    }
  }

  private void SetupBalanceSliders(UISetupBalanceSliderEntry[] inBalanceSliders, RacingVehicle inVehicle)
  {
    SessionSetup.SetupOutput outSetupOutput = new SessionSetup.SetupOutput();
    inVehicle.setup.GetSetupOutput(ref outSetupOutput);
    inVehicle.performance.setupPerformance.UpdateVisualKnowledgeRange();
    SetupPerformance.OptimalSetup optimalSetup = inVehicle.performance.setupPerformance.GetOptimalSetup();
    SessionSetup.SetupOpinion[] opinionOnPreviousSetup = Game.instance.persistentEventData.GetOpinionOnPreviousSetup(inVehicle);
    for (int index = 0; index < inBalanceSliders.Length; ++index)
    {
      switch (inBalanceSliders[index].setupBalanceEntry)
      {
        case UISetupBalanceSliderEntry.balanceEntryType.Aero:
          inBalanceSliders[index].SetupSlider(inVehicle, optimalSetup.setupOutput.aerodynamics, optimalSetup.minVisualRangeOffset[0], optimalSetup.maxVisualRangeOffset[0], opinionOnPreviousSetup[0], 0.0f, false);
          inBalanceSliders[index].SetSetupOutput(outSetupOutput.aerodynamics, outSetupOutput.aerodynamics);
          break;
        case UISetupBalanceSliderEntry.balanceEntryType.Speed:
          inBalanceSliders[index].SetupSlider(inVehicle, optimalSetup.setupOutput.speedBalance, optimalSetup.minVisualRangeOffset[1], optimalSetup.maxVisualRangeOffset[1], opinionOnPreviousSetup[1], 0.0f, false);
          inBalanceSliders[index].SetSetupOutput(outSetupOutput.speedBalance, outSetupOutput.speedBalance);
          break;
        case UISetupBalanceSliderEntry.balanceEntryType.Handling:
          inBalanceSliders[index].SetupSlider(inVehicle, optimalSetup.setupOutput.handling, optimalSetup.minVisualRangeOffset[2], optimalSetup.maxVisualRangeOffset[2], opinionOnPreviousSetup[2], 0.0f, false);
          inBalanceSliders[index].SetSetupOutput(outSetupOutput.handling, outSetupOutput.handling);
          break;
      }
      inBalanceSliders[index].SetupEntryOnCurrentKnowledge();
    }
  }

  public override bool IsReady()
  {
    return true;
  }

  public void OnFirstVehicleSetupButton()
  {
    UIManager.instance.GetScreen<PitScreen>().Setup(this.mVehicle[0], PitScreen.Mode.PreSession);
    UIManager.instance.ChangeScreen("PitScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  public void OnSecondVehicleSetupButton()
  {
    UIManager.instance.GetScreen<PitScreen>().Setup(this.mVehicle[1], PitScreen.Mode.PreSession);
    UIManager.instance.ChangeScreen("PitScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }
}
