// Decompiled with JetBrains decompiler
// Type: UIPracticeStintTypeWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine.UI;

public class UIPracticeStintTypeWidget : UIBaseTimerWidget
{
  private static int idealQualifyingTrimMultiplier = 1;
  private static int idealRaceTrimMultiplier = 4;
  private static int numDropdownentries = 3;
  private List<string> mDropDownData = new List<string>();
  private bool mAllowQualifyingTrim = true;
  private int mQualTrimMultiplier = UIPracticeStintTypeWidget.idealQualifyingTrimMultiplier;
  private int mRaceTrimMultiplier = UIPracticeStintTypeWidget.idealRaceTrimMultiplier;
  public Toggle qualifyingTrimToggle;
  public Dropdown qualifyingDropdown;
  public UISessionKnowledgeProgressBar qualifyingTrimBar;
  public Toggle raceTrimToggle;
  public Dropdown raceTrimDropdown;
  public UISessionKnowledgeProgressBar raceTrimBar;
  private RacingVehicle mVehicle;
  private UIPracticeStintTypeWidget.StintSelection mStintSelection;
  private UIPracticeStintTypeWidget.StintSelection defaultSelection;

  public int raceTrimMultiplier
  {
    get
    {
      return this.mRaceTrimMultiplier;
    }
  }

  public int qualTrimMultiplier
  {
    get
    {
      return this.mQualTrimMultiplier;
    }
  }

  public void SetVehicle(RacingVehicle inVehicle)
  {
    this.mVehicle = inVehicle;
    this.mQualTrimMultiplier = UIPracticeStintTypeWidget.idealQualifyingTrimMultiplier;
    this.mRaceTrimMultiplier = UIPracticeStintTypeWidget.idealRaceTrimMultiplier;
    while (this.mVehicle.performance.fuel.fuelTankLapCountCapacity - 2 < this.mQualTrimMultiplier * UIPracticeStintTypeWidget.numDropdownentries && this.mQualTrimMultiplier > 1)
      --this.mQualTrimMultiplier;
    while (this.mVehicle.performance.fuel.fuelTankLapCountCapacity - 2 < this.mRaceTrimMultiplier * UIPracticeStintTypeWidget.numDropdownentries && this.mRaceTrimMultiplier > 1)
      --this.mRaceTrimMultiplier;
    this.mAllowQualifyingTrim = Game.instance.player.team.championship.rules.qualifyingBasedActive;
    this.SetQualifyingGameObjects(this.mAllowQualifyingTrim);
    if (this.mAllowQualifyingTrim)
    {
      this.mDropDownData.Clear();
      for (int index = 1; index <= UIPracticeStintTypeWidget.numDropdownentries; ++index)
        this.mDropDownData.Add(GameUtility.FormatForLaps(index * this.mQualTrimMultiplier));
      this.qualifyingDropdown.ClearOptions();
      this.qualifyingDropdown.AddOptions(this.mDropDownData);
      this.qualifyingTrimBar.Setup(PracticeReportSessionData.KnowledgeType.QualifyingTrim);
    }
    this.mDropDownData.Clear();
    for (int index = 1; index <= UIPracticeStintTypeWidget.numDropdownentries; ++index)
      this.mDropDownData.Add(GameUtility.FormatForLaps(index * this.mRaceTrimMultiplier));
    this.raceTrimDropdown.ClearOptions();
    this.raceTrimDropdown.AddOptions(this.mDropDownData);
    this.raceTrimBar.Setup(PracticeReportSessionData.KnowledgeType.RaceTrim);
    this.mDropDownData.Clear();
    this.SetupSelectedTrim();
  }

  private void SetupSelectedTrim()
  {
    if (this.mVehicle.setup.currentSetup.trim == SessionSetup.Trim.Race || !this.mAllowQualifyingTrim)
    {
      this.defaultSelection = UIPracticeStintTypeWidget.StintSelection.RaceTrim;
      this.SetStintSelection(UIPracticeStintTypeWidget.StintSelection.RaceTrim, true);
    }
    else
    {
      if (this.mVehicle.setup.currentSetup.trim != SessionSetup.Trim.Qualifying)
        return;
      this.defaultSelection = UIPracticeStintTypeWidget.StintSelection.QualifyingTrim;
      this.SetStintSelection(UIPracticeStintTypeWidget.StintSelection.QualifyingTrim, true);
    }
  }

  public void OnQualifyingTrimSelected()
  {
    this.SetStintSelection(UIPracticeStintTypeWidget.StintSelection.QualifyingTrim, false);
  }

  public void OnRaceTrimSelected()
  {
    this.SetStintSelection(UIPracticeStintTypeWidget.StintSelection.RaceTrim, false);
  }

  private void SetStintSelection(UIPracticeStintTypeWidget.StintSelection inStintSelection, bool inForceChange)
  {
    if (this.mStintSelection != inStintSelection || inForceChange)
    {
      UIManager.instance.GetScreen<PitScreen>().OnStintChanged(this.defaultSelection != inStintSelection);
      this.mStintSelection = inStintSelection;
      switch (this.mStintSelection)
      {
        case UIPracticeStintTypeWidget.StintSelection.QualifyingTrim:
          this.qualifyingTrimToggle.isOn = true;
          this.raceTrimToggle.isOn = false;
          GameUtility.SetActive(this.qualifyingDropdown.transform.parent.gameObject, true);
          GameUtility.SetActive(this.raceTrimDropdown.transform.parent.gameObject, false);
          this.qualifyingDropdown.value = 0;
          this.mVehicle.setup.SetTargetTrim(SessionSetup.Trim.Qualifying);
          this.mVehicle.practiceKnowledge.knowledgeType = PracticeReportSessionData.KnowledgeType.QualifyingTrim;
          break;
        case UIPracticeStintTypeWidget.StintSelection.RaceTrim:
          this.raceTrimToggle.isOn = true;
          this.qualifyingTrimToggle.isOn = false;
          GameUtility.SetActive(this.qualifyingDropdown.transform.parent.gameObject, false);
          GameUtility.SetActive(this.raceTrimDropdown.transform.parent.gameObject, true);
          this.raceTrimDropdown.value = 0;
          this.mVehicle.setup.SetTargetTrim(SessionSetup.Trim.Race);
          this.mVehicle.practiceKnowledge.knowledgeType = PracticeReportSessionData.KnowledgeType.RaceTrim;
          break;
      }
      this.RefreshTime();
    }
    this.qualifyingTrimBar.SetCanvasAlpha();
    this.raceTrimBar.SetCanvasAlpha();
  }

  public UIPracticeStintTypeWidget.StintSelection GetSelectedStint()
  {
    return this.mStintSelection;
  }

  public int GetSelectedDropdownValue()
  {
    int num = 0;
    switch (this.mStintSelection)
    {
      case UIPracticeStintTypeWidget.StintSelection.QualifyingTrim:
        num = this.qualifyingDropdown.value;
        break;
      case UIPracticeStintTypeWidget.StintSelection.RaceTrim:
        num = this.raceTrimDropdown.value;
        break;
    }
    return num;
  }

  public void SetQualifyingGameObjects(bool inQualAllowed)
  {
    GameUtility.SetActive(this.qualifyingDropdown.transform.parent.gameObject, inQualAllowed);
    GameUtility.SetActive(this.qualifyingTrimToggle.gameObject, inQualAllowed);
    GameUtility.SetActive(this.qualifyingTrimBar.gameObject, inQualAllowed);
  }

  public override void RefreshTime()
  {
    this.SetTimeEstimate(this.mVehicle.setup.GetTrimTimeImpact(), this.mVehicle.setup.IsOnTheCriticalPath(SessionSetup.PitCrewSizeDependentSteps.Trim));
  }

  public enum StintSelection
  {
    QualifyingTrim,
    RaceTrim,
  }
}
