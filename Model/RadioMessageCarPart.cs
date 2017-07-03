// Decompiled with JetBrains decompiler
// Type: RadioMessageCarPart
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RadioMessageCarPart : RadioMessage
{
  private bool[] dilemmaFlags = new bool[2];
  private bool[] issuesFlags = new bool[3];
  private List<CarPart> mCarPartsWithIssues = new List<CarPart>();
  private List<CarPart> mAllCarPartsWithIssues = new List<CarPart>();
  private bool[] mHasFailureBeenTriggered;
  private bool[] mRedZoneBeenTriggered;

  public RadioMessageCarPart(RacingVehicle inVehicle, TeamRadio inTeamRadio)
    : base(inVehicle, inTeamRadio)
  {
    this.mHasPlayerBeenInformedAlready = new bool[11];
    this.mHasFailureBeenTriggered = new bool[11];
    this.mRedZoneBeenTriggered = new bool[11];
    inVehicle.pathState.OnPitlaneExit += new Action(this.UpdateCarPartConditions);
    this.dilemmaType = RadioMessage.DilemmaType.CarPartCondition;
  }

  public override void OnLoad()
  {
    this.mVehicle.pathState.OnPitlaneExit -= new Action(this.UpdateCarPartConditions);
    this.mVehicle.pathState.OnPitlaneExit += new Action(this.UpdateCarPartConditions);
  }

  public override bool AllowDuplicateDilemma()
  {
    return true;
  }

  private void UpdateCarPartConditions()
  {
    CarPart.PartType[] partType = CarPart.GetPartType(this.mVehicle.driver.contract.GetTeam().championship.series, false);
    int length = partType.Length;
    Car car = this.mVehicle.driver.contract.GetTeam().carManager.GetCar(this.mVehicle.driver.contract.GetTeam().GetDriverIndex(this.mVehicle.driver));
    for (int index = 0; index < length; ++index)
    {
      CarPart part = car.GetPart(partType[index]);
      if ((double) part.partCondition.normalizedCondition > (double) this.GetCarPartConditionTrigger(part, RadioMessageCarPart.CarPartCondition.Low))
        this.ResetAddressedPartFlag(part, RadioMessageCarPart.CarPartCondition.Low);
      if ((double) part.partCondition.normalizedCondition > (double) this.GetCarPartConditionTrigger(part, RadioMessageCarPart.CarPartCondition.RedZone))
        this.ResetAddressedPartFlag(part, RadioMessageCarPart.CarPartCondition.RedZone);
      if ((double) part.partCondition.normalizedCondition > (double) this.GetCarPartConditionTrigger(part, RadioMessageCarPart.CarPartCondition.Failed))
        this.ResetAddressedPartFlag(part, RadioMessageCarPart.CarPartCondition.Failed);
    }
    this.mAllCarPartsWithIssues.Clear();
    this.GetCarPartIssues(car, RadioMessageCarPart.CarPartCondition.RedZone, true, ref this.mAllCarPartsWithIssues);
    if (this.mAllCarPartsWithIssues.Count == 0)
    {
      this.dilemmaFlags[0] = false;
      this.dilemmaFlags[1] = false;
    }
    else if (this.mAllCarPartsWithIssues.Count == 1)
      this.dilemmaFlags[1] = false;
    this.mAllCarPartsWithIssues.Clear();
    this.GetCarPartIssues(car, RadioMessageCarPart.CarPartCondition.Low, true, ref this.mAllCarPartsWithIssues);
    if (this.mAllCarPartsWithIssues.Count == 0)
    {
      this.issuesFlags[0] = false;
      this.issuesFlags[1] = false;
      this.issuesFlags[2] = false;
    }
    else if (this.mAllCarPartsWithIssues.Count == 1)
    {
      this.issuesFlags[1] = false;
      this.issuesFlags[2] = false;
    }
    else if (this.mAllCarPartsWithIssues.Count == 2)
      this.issuesFlags[2] = false;
    this.mAllCarPartsWithIssues.Clear();
  }

  protected override void OnSimulationUpdate()
  {
    if (this.mVehicle.behaviourManager.isOutOfRace || this.mVehicle.timer.hasSeenChequeredFlag || (!this.isRaceSession || this.mVehicle.pathState.IsInPitlaneArea()))
      return;
    this.CheckVehicleCarParts();
  }

  protected override bool CheckForDilemmaAndSendToPlayer()
  {
    if (!this.mVehicle.timer.hasSeenChequeredFlag && this.isRaceSession && !this.mVehicle.pathState.IsInPitlaneArea())
    {
      Car car = this.mVehicle.driver.contract.GetTeam().carManager.GetCar(this.mVehicle.driver.contract.GetTeam().GetDriverIndex(this.mVehicle.driver));
      this.mCarPartsWithIssues.Clear();
      this.GetCarPartIssues(car, RadioMessageCarPart.CarPartCondition.RedZone, false, ref this.mCarPartsWithIssues);
      if (this.mCarPartsWithIssues.Count > 0)
        return this.TriggerRedZoneParts(this.mCarPartsWithIssues, car);
      this.mCarPartsWithIssues.Clear();
    }
    return false;
  }

  private void CheckVehicleCarParts()
  {
    Car car = this.mVehicle.driver.contract.GetTeam().carManager.GetCar(this.mVehicle.driver.contract.GetTeam().GetDriverIndex(this.mVehicle.driver));
    this.mCarPartsWithIssues.Clear();
    this.GetCarPartIssues(car, RadioMessageCarPart.CarPartCondition.Failed, false, ref this.mCarPartsWithIssues);
    if (this.mCarPartsWithIssues.Count > 0)
    {
      this.TriggerFailedPart(this.mCarPartsWithIssues);
    }
    else
    {
      this.mCarPartsWithIssues.Clear();
      this.GetCarPartIssues(car, RadioMessageCarPart.CarPartCondition.Low, false, ref this.mCarPartsWithIssues);
      if (this.mCarPartsWithIssues.Count > 0)
        this.TriggerIssueParts(this.mCarPartsWithIssues, car);
    }
    this.mCarPartsWithIssues.Clear();
  }

  private void GetCarPartIssues(Car inCar, RadioMessageCarPart.CarPartCondition inCondition, bool inAllParts, ref List<CarPart> carPartIssues)
  {
    CarPart.PartType[] partType = CarPart.GetPartType(inCar.carManager.team.championship.series, false);
    int length = partType.Length;
    for (int index = 0; index < length; ++index)
    {
      CarPart part = inCar.GetPart(partType[index]);
      float conditionTrigger = this.GetCarPartConditionTrigger(part, inCondition);
      bool flag = inAllParts || !this.HasPartBeenAddressed(part, inCondition);
      if (!this.IsPartAboutToBeRepaired(part) && flag)
      {
        if (inCondition != RadioMessageCarPart.CarPartCondition.Failed && (double) part.partCondition.condition <= (double) conditionTrigger)
          carPartIssues.Add(part);
        else if (inCondition == RadioMessageCarPart.CarPartCondition.Failed && part.partCondition.partState == global::CarPartCondition.PartState.CatastrophicFailure)
          carPartIssues.Add(part);
      }
    }
  }

  private void TriggerFailedPart(List<CarPart> inFailedCarParts)
  {
    this.MarkPartAsAddressed(inFailedCarParts[0], RadioMessageCarPart.CarPartCondition.Failed);
    this.CreateDialogQuery(inFailedCarParts[0], inFailedCarParts.Count, RadioMessageCarPart.CarPartCondition.Failed);
  }

  private bool TriggerRedZoneParts(List<CarPart> inRedZoneCarParts, Car inCar)
  {
    for (int index = 0; index < inRedZoneCarParts.Count; ++index)
      this.MarkPartAsAddressed(inRedZoneCarParts[index], RadioMessageCarPart.CarPartCondition.RedZone);
    this.mAllCarPartsWithIssues.Clear();
    this.GetCarPartIssues(inCar, RadioMessageCarPart.CarPartCondition.RedZone, true, ref this.mAllCarPartsWithIssues);
    if (this.mAllCarPartsWithIssues.Count == 1 && !this.dilemmaFlags[0])
    {
      this.CreateDialogQuery(inRedZoneCarParts[0], this.mAllCarPartsWithIssues.Count, RadioMessageCarPart.CarPartCondition.RedZone);
      this.dilemmaFlags[0] = true;
      this.mAllCarPartsWithIssues.Clear();
      return true;
    }
    if (this.mAllCarPartsWithIssues.Count > 1 && !this.dilemmaFlags[1])
    {
      this.CreateDialogQuery(inRedZoneCarParts[0], this.mAllCarPartsWithIssues.Count, RadioMessageCarPart.CarPartCondition.RedZone);
      this.dilemmaFlags[0] = true;
      this.dilemmaFlags[1] = true;
      this.mAllCarPartsWithIssues.Clear();
      return true;
    }
    this.mAllCarPartsWithIssues.Clear();
    return false;
  }

  private void TriggerIssueParts(List<CarPart> inIssueCarParts, Car inCar)
  {
    for (int index = 0; index < inIssueCarParts.Count; ++index)
      this.MarkPartAsAddressed(inIssueCarParts[index], RadioMessageCarPart.CarPartCondition.Low);
    this.mAllCarPartsWithIssues.Clear();
    this.GetCarPartIssues(inCar, RadioMessageCarPart.CarPartCondition.Low, true, ref this.mAllCarPartsWithIssues);
    if (this.mAllCarPartsWithIssues.Count == 1 && !this.issuesFlags[0])
    {
      this.CreateDialogQuery(inIssueCarParts[0], this.mAllCarPartsWithIssues.Count, RadioMessageCarPart.CarPartCondition.Low);
      this.issuesFlags[0] = true;
    }
    else if (this.mAllCarPartsWithIssues.Count == 2 && !this.issuesFlags[1])
    {
      this.CreateDialogQuery(inIssueCarParts[0], this.mAllCarPartsWithIssues.Count, RadioMessageCarPart.CarPartCondition.Low);
      this.issuesFlags[0] = true;
      this.issuesFlags[1] = true;
    }
    else if (this.mAllCarPartsWithIssues.Count >= 3 && !this.issuesFlags[2])
    {
      this.CreateDialogQuery(inIssueCarParts[0], this.mAllCarPartsWithIssues.Count, RadioMessageCarPart.CarPartCondition.Low);
      this.issuesFlags[0] = true;
      this.issuesFlags[1] = true;
      this.issuesFlags[2] = true;
    }
    this.mAllCarPartsWithIssues.Clear();
  }

  private bool IsPartAboutToBeRepaired(CarPart inPart)
  {
    if (this.mVehicle.setup.repairParts.Count == 0 || this.mVehicle.setup.GetRepairPart(inPart) == null)
      return false;
    return this.mVehicle.strategy.IsGoingToPit();
  }

  private void CreateDialogQuery(CarPart inPart, int inProblemsCount, RadioMessageCarPart.CarPartCondition inCarPartCondition)
  {
    StringVariableParser.partForUI = inPart;
    DialogQuery inQuery = new DialogQuery();
    this.AddPersonCriteria(inQuery, (Person) this.mVehicle.driver);
    inQuery.AddCriteria("Source", "PartProblem");
    this.AddPartNameCriteria(inQuery, inPart, inProblemsCount);
    this.AddPartCondition(inQuery, inCarPartCondition);
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(inQuery, false);
    if (this.dialogRule != null)
    {
      this.personWhoSpeaks = (Person) this.mVehicle.driver;
      if (inCarPartCondition == RadioMessageCarPart.CarPartCondition.RedZone)
        this.SendDilemma();
      else
        this.SendRadioMessage();
    }
    StringVariableParser.partForUI = (CarPart) null;
  }

  private void AddPartNameCriteria(DialogQuery inQuery, CarPart inPart, int inProblemsCount)
  {
    if (inProblemsCount == 1)
    {
      CarPart.PartType partType = inPart.GetPartType();
      switch (partType)
      {
        case CarPart.PartType.BrakesGT:
          partType = CarPart.PartType.Brakes;
          break;
        case CarPart.PartType.EngineGT:
          partType = CarPart.PartType.Engine;
          break;
        case CarPart.PartType.GearboxGT:
          partType = CarPart.PartType.Gearbox;
          break;
        case CarPart.PartType.SuspensionGT:
          partType = CarPart.PartType.Suspension;
          break;
      }
      inQuery.AddCriteria("Part", partType.ToString());
    }
    else if (inProblemsCount == 2)
      inQuery.AddCriteria("Part", "Some");
    else
      inQuery.AddCriteria("Part", "Lots");
  }

  private void AddPartCondition(DialogQuery inQuery, RadioMessageCarPart.CarPartCondition inPartCondition)
  {
    switch (inPartCondition)
    {
      case RadioMessageCarPart.CarPartCondition.Low:
        inQuery.AddCriteria("PartCondition", "Low");
        break;
      case RadioMessageCarPart.CarPartCondition.RedZone:
        inQuery.AddCriteria("PartCondition", "Low");
        break;
      case RadioMessageCarPart.CarPartCondition.Failed:
        inQuery.AddCriteria("PartCondition", "Failed");
        break;
    }
  }

  private bool HasPartBeenAddressed(CarPart inPart, RadioMessageCarPart.CarPartCondition inConditionType)
  {
    int partType = (int) inPart.GetPartType();
    switch (inConditionType)
    {
      case RadioMessageCarPart.CarPartCondition.Low:
        return this.mHasPlayerBeenInformedAlready[partType];
      case RadioMessageCarPart.CarPartCondition.RedZone:
        return this.mRedZoneBeenTriggered[partType];
      case RadioMessageCarPart.CarPartCondition.Failed:
        return this.mHasFailureBeenTriggered[partType];
      default:
        return false;
    }
  }

  private void MarkPartAsAddressed(CarPart inPart, RadioMessageCarPart.CarPartCondition inConditionType)
  {
    int partType = (int) inPart.GetPartType();
    switch (inConditionType)
    {
      case RadioMessageCarPart.CarPartCondition.Low:
        this.mHasPlayerBeenInformedAlready[partType] = true;
        break;
      case RadioMessageCarPart.CarPartCondition.RedZone:
        this.mRedZoneBeenTriggered[partType] = true;
        break;
      case RadioMessageCarPart.CarPartCondition.Failed:
        this.mHasFailureBeenTriggered[partType] = true;
        break;
    }
  }

  private void ResetAddressedPartFlag(CarPart inPart, RadioMessageCarPart.CarPartCondition inConditionType)
  {
    int partType = (int) inPart.GetPartType();
    switch (inConditionType)
    {
      case RadioMessageCarPart.CarPartCondition.Low:
        this.mHasPlayerBeenInformedAlready[partType] = false;
        break;
      case RadioMessageCarPart.CarPartCondition.RedZone:
        this.mRedZoneBeenTriggered[partType] = false;
        break;
      case RadioMessageCarPart.CarPartCondition.Failed:
        this.mHasFailureBeenTriggered[partType] = false;
        break;
    }
  }

  private float GetCarPartConditionTrigger(CarPart inCarPart, RadioMessageCarPart.CarPartCondition inCondition)
  {
    switch (inCondition)
    {
      case RadioMessageCarPart.CarPartCondition.Low:
        return 0.3f;
      case RadioMessageCarPart.CarPartCondition.RedZone:
        return inCarPart.partCondition.redZone;
      case RadioMessageCarPart.CarPartCondition.Failed:
        return 0.0f;
      default:
        return 0.0f;
    }
  }

  private enum CarPartCondition
  {
    Low,
    RedZone,
    Failed,
  }
}
