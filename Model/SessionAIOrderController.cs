// Decompiled with JetBrains decompiler
// Type: SessionAIOrderController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class SessionAIOrderController
{
  private AIOrderCriteria[] mInputs = new AIOrderCriteria[52];
  private List<SessionAIOrder> mOrders = new List<SessionAIOrder>();
  private RacingVehicle mVehicle;
  private bool mUsesAIForStrategy;

  public List<SessionAIOrder> orders
  {
    get
    {
      return this.mOrders;
    }
  }

  public void Start(RacingVehicle inVehicle)
  {
    this.mVehicle = inVehicle;
    for (int index = 0; index < this.mInputs.Length; ++index)
      this.mInputs[index] = new AIOrderCriteria();
    this.mUsesAIForStrategy = this.mVehicle.driver.personalityTraitController.HasSpecialCase(PersonalityTrait.SpecialCaseType.TurnOffStrategy);
  }

  public void UpdateAIOrders(bool inIsCloseToPitlaneEntry = false)
  {
    if (this.mVehicle.timer.hasSeenChequeredFlag || this.mVehicle.behaviourManager.isOutOfRace || !this.mUsesAIForStrategy && this.mVehicle.driver.contract.GetTeam().IsPlayersTeam() && (!false && !Game.instance.sessionManager.isSkippingSession))
      return;
    this.SetInputsForVehicle(this.mVehicle, inIsCloseToPitlaneEntry);
    this.mOrders.Clear();
    App.instance.sessionAIOrdersDatabase.GetRelevantOrders(this.mVehicle, this.mInputs, ref this.mOrders);
    for (int index = 0; index < this.mOrders.Count; ++index)
    {
      SessionAIOrder mOrder = this.mOrders[index];
      if (mOrder.AreOutputsRelevant(this.mVehicle) && (!this.mUsesAIForStrategy || !this.mVehicle.driver.IsPlayersDriver() || this.mUsesAIForStrategy && mOrder.category == SessionAIOrder.Category.Strategy || Game.instance.sessionManager.isSkippingSession))
      {
        mOrder.ApplyOutputs(this.mVehicle);
        if (mOrder.debugThisOrder)
          Debug.LogFormat("{0} driver has executed order ({1})", new object[2]
          {
            (object) this.mVehicle.driver.name,
            (object) mOrder.ID
          });
      }
    }
  }

  public bool IsDriverOnIdealTyreTread()
  {
    float num = this.mVehicle.performance.tyrePerformance.TimeCostForLaps(this.mVehicle.setup.tyreSet.GetTread(), 1, 0);
    return (double) this.mVehicle.performance.tyrePerformance.TimeCostForLaps(this.mVehicle.setup.tyreSet.GetTread(), 1, 1) - (double) num <= 5.0 && (double) num <= 5.0 || this.mVehicle.performance.tyrePerformance.GetTreadWithLeastTimeCost(0.0f) == this.mVehicle.setup.tyreSet.GetTread();
  }

  private void SetInputsForVehicle(RacingVehicle inVehicle, bool inIsCloseToPitlaneEntry)
  {
    SessionManager sessionManager = Game.instance.sessionManager;
    RacingVehicle behind = inVehicle.behind;
    RacingVehicle ahead = inVehicle.ahead;
    Team team = inVehicle.driver.contract.GetTeam();
    Championship championship = team.championship;
    this.mInputs[0].SetupDataEnum(AIOrderCriteria.Type.SessionType, (int) sessionManager.eventDetails.currentSession.sessionType);
    this.mInputs[1].SetupData(AIOrderCriteria.Type.SessionCompletion, sessionManager.GetNormalizedSessionTime());
    this.mInputs[2].SetupDataEnum(AIOrderCriteria.Type.TyreCompound, (int) inVehicle.setup.tyreSet.GetCompound());
    this.mInputs[3].SetupData(AIOrderCriteria.Type.TyreWear, inVehicle.setup.tyreSet.GetCondition());
    float targetFuelLapDelta = inVehicle.performance.fuel.GetTargetFuelLapDelta();
    this.mInputs[4].SetupData(AIOrderCriteria.Type.FuelDelta, targetFuelLapDelta);
    this.mInputs[5].SetupData(AIOrderCriteria.Type.TyreTemperature, inVehicle.setup.tyreSet.GetTemperature());
    if (sessionManager.eventDetails.currentSession.sessionType == SessionDetails.SessionType.Race)
      this.mInputs[6].SetupData(AIOrderCriteria.Type.LapsToGo, (float) sessionManager.GetLapsRemaining());
    RacingVehicle teamMateVehicle = SessionAIOrderController.GetTeamMateVehicle(inVehicle);
    this.mInputs[7].SetupDataEnum(AIOrderCriteria.Type.TeamMateStatus, (int) teamMateVehicle.driver.contract.currentStatus);
    this.mInputs[8].SetupData(AIOrderCriteria.Type.GapToTeamMate, teamMateVehicle.timer.gapToLeader - inVehicle.timer.gapToLeader);
    this.mInputs[9].SetupData(AIOrderCriteria.Type.GapToAhead, inVehicle.timer.gapToAhead);
    this.mInputs[10].SetupData(AIOrderCriteria.Type.GapToBehind, inVehicle.timer.gapToBehind);
    this.mInputs[11].SetupData(AIOrderCriteria.Type.GapToLeader, inVehicle.timer.gapToLeader);
    this.mInputs[12].SetupData(AIOrderCriteria.Type.SessionPosition, (float) inVehicle.standingsPosition);
    ChampionshipEntry_v1 entry1 = championship.standings.GetEntry((Entity) inVehicle.driver);
    if (entry1 != null)
      this.mInputs[13].SetupData(AIOrderCriteria.Type.DriverChampPosition, (float) entry1.GetCurrentChampionshipPosition());
    else
      this.mInputs[13].ClearData();
    this.mInputs[14].SetupData(AIOrderCriteria.Type.TeamChampPosition, (float) championship.standings.GetEntry((Entity) team).GetCurrentChampionshipPosition());
    this.mInputs[15].SetupData(AIOrderCriteria.Type.LowestCondition, SessionAIOrderController.GetLowestConditionValue(inVehicle, CarPart.PartType.None));
    this.mInputs[16].SetupData(AIOrderCriteria.Type.WaterLevel, sessionManager.currentSessionWeather.GetNormalizedTrackWater());
    this.mInputs[17].SetupDataEnum(AIOrderCriteria.Type.SessionFlag, (int) sessionManager.flag);
    this.mInputs[18].SetupData(AIOrderCriteria.Type.IsPitting, inVehicle.strategy.IsGoingToPit());
    this.mInputs[19].SetupData(AIOrderCriteria.Type.Fuel, (float) inVehicle.performance.fuel.GetFuelLapsRemaining() + targetFuelLapDelta);
    this.mInputs[20].SetupData(AIOrderCriteria.Type.TeamAggression, team.aggression);
    this.mInputs[21].SetupData(AIOrderCriteria.Type.DiceRoll, RandomUtility.GetRandom01());
    ChampionshipEntry_v1 entry2 = championship.standings.GetEntry((Entity) teamMateVehicle.driver);
    if (entry2 != null)
      this.mInputs[22].SetupData(AIOrderCriteria.Type.TeamMateChampPosition, (float) entry2.GetCurrentChampionshipPosition());
    else
      this.mInputs[22].ClearData();
    if (ahead != null)
    {
      this.mInputs[23].SetupData(AIOrderCriteria.Type.CarAheadTyreWear, ahead.setup.tyreSet.GetCondition());
      this.mInputs[24].SetupDataEnum(AIOrderCriteria.Type.CarAheadEngineMode, (int) ahead.performance.fuel.engineMode);
      this.mInputs[25].SetupDataEnum(AIOrderCriteria.Type.CarAheadDrivingStyle, (int) ahead.performance.drivingStyleMode);
      this.mInputs[26].SetupData(AIOrderCriteria.Type.CarAheadOrderedPit, ahead.strategy.IsGoingToPit());
    }
    else
    {
      this.mInputs[23].ClearData();
      this.mInputs[24].ClearData();
      this.mInputs[25].ClearData();
      this.mInputs[26].ClearData();
    }
    if (behind != null)
    {
      this.mInputs[27].SetupData(AIOrderCriteria.Type.CarBehindTyreWear, behind.setup.tyreSet.GetCondition());
      this.mInputs[28].SetupDataEnum(AIOrderCriteria.Type.CarBehindEngineMode, (int) behind.performance.fuel.engineMode);
      this.mInputs[29].SetupDataEnum(AIOrderCriteria.Type.CarBehindDrivingStyle, (int) behind.performance.drivingStyleMode);
      this.mInputs[30].SetupData(AIOrderCriteria.Type.CarBehindOrderedPit, behind.strategy.IsGoingToPit());
    }
    else
    {
      this.mInputs[27].ClearData();
      this.mInputs[28].ClearData();
      this.mInputs[29].ClearData();
      this.mInputs[30].ClearData();
    }
    this.mInputs[31].SetupData(AIOrderCriteria.Type.FirstRace, championship.GetFirstEventDetails() == Game.instance.sessionManager.eventDetails);
    this.mInputs[32].SetupData(AIOrderCriteria.Type.LastRace, championship.GetFinalEventDetails() == Game.instance.sessionManager.eventDetails);
    this.mInputs[33].SetupData(AIOrderCriteria.Type.TeamMateRetired, teamMateVehicle.behaviourManager.isOutOfRace);
    this.mInputs[34].SetupData(AIOrderCriteria.Type.Outlap, inVehicle.timer.currentLap.isOutLap);
    this.mInputs[35].SetupData(AIOrderCriteria.Type.InLap, inVehicle.timer.currentLap.isInLap);
    this.mInputs[36].SetupData(AIOrderCriteria.Type.CanRefuel, championship.rules.isRefuelingOn);
    this.mInputs[37].SetupData(AIOrderCriteria.Type.TeamMateIsPitting, teamMateVehicle.strategy.IsGoingToPit());
    this.mInputs[38].SetupData(AIOrderCriteria.Type.TeamMateTyreWear, teamMateVehicle.setup.tyreSet.GetCondition());
    this.mInputs[39].SetupData(AIOrderCriteria.Type.TeamMateFuelDelta, teamMateVehicle.performance.fuel.GetTargetFuelLapDelta());
    this.mInputs[40].SetupData(AIOrderCriteria.Type.TeamCompoundDelta, this.GetCompoundDelta(inVehicle.setup.tyreSet.GetCompound(), teamMateVehicle.setup.tyreSet.GetCompound()));
    this.mInputs[41].SetupData(AIOrderCriteria.Type.IsInGarage, inVehicle.pathState.currentState.stateType == PathStateManager.StateType.Garage);
    int num = inVehicle.driver.expectedRacePosition - inVehicle.standingsPosition;
    this.mInputs[42].SetupData(AIOrderCriteria.Type.PredictedPositionDelta, (float) num);
    float inInfo = 0.0f;
    int index = Mathf.Clamp(inVehicle.driver.expectedRacePosition - 1, 0, Game.instance.sessionManager.standings.Count);
    if (num != 0 && index < Game.instance.sessionManager.standings.Count)
    {
      RacingVehicle standing = Game.instance.sessionManager.standings[index];
      inInfo = inVehicle.timer.gapToLeader - standing.timer.gapToLeader;
    }
    this.mInputs[43].SetupData(AIOrderCriteria.Type.GapToPredictedPosition, inInfo);
    this.mInputs[44].SetupData(AIOrderCriteria.Type.CloseToPitlaneEntry, inIsCloseToPitlaneEntry);
    this.mInputs[45].SetupData(AIOrderCriteria.Type.EngineCondition, SessionAIOrderController.GetLowestConditionValue(inVehicle, CarPart.PartType.Engine));
    if (inIsCloseToPitlaneEntry)
      this.mInputs[46].ClearData();
    else
      this.mInputs[46].ClearData();
    this.mInputs[47].SetupData(AIOrderCriteria.Type.AirTempTyreHeatingRate, inVehicle.setup.tyreSet.airTempRateChange);
    this.mInputs[48].SetupData(AIOrderCriteria.Type.FuelToTankCapacityPercentage, inVehicle.performance.fuel.GetFuelLapsRemainingDecimal() / (float) inVehicle.performance.fuel.fuelTankLapCountCapacity);
    this.mInputs[49].SetupData(AIOrderCriteria.Type.ERSBatteryCharge, inVehicle.ERSController.normalizedCharge);
    this.mInputs[50].SetupData(AIOrderCriteria.Type.IsERSActive, championship.rules.isEnergySystemActive);
    this.mInputs[51].SetupData(AIOrderCriteria.Type.IsHybridModeActive, championship.rules.isEnergySystemActive && championship.rules.isHybridModeActive);
  }

  private float GetCompoundDelta(TyreSet.Compound inCompound1, TyreSet.Compound inCompound2)
  {
    float num1 = (float) inCompound1;
    if (inCompound1 == TyreSet.Compound.UltraSoft)
      num1 = -1f;
    float num2 = (float) inCompound2;
    if (inCompound2 == TyreSet.Compound.UltraSoft)
      num2 = -1f;
    return num1 - num2;
  }

  private static float GetLowestConditionValue(RacingVehicle inVehicle, CarPart.PartType partType = CarPart.PartType.None)
  {
    float b = float.MaxValue;
    for (int index = 0; index < inVehicle.car.seriesCurrentParts.Length; ++index)
    {
      CarPart seriesCurrentPart = inVehicle.car.seriesCurrentParts[index];
      if (partType == CarPart.PartType.None || partType == seriesCurrentPart.GetPartType())
        b = Mathf.Min(seriesCurrentPart.partCondition.condition, b);
    }
    return b;
  }

  private static RacingVehicle GetTeamMateVehicle(RacingVehicle inVehicle)
  {
    Driver driver = inVehicle.driver;
    Team team = driver.contract.GetTeam();
    for (int inIndex = 0; inIndex < Game.instance.vehicleManager.vehicleCount; ++inIndex)
    {
      RacingVehicle vehicle = Game.instance.vehicleManager.GetVehicle(inIndex);
      if (driver != vehicle.driver && vehicle.driver.contract.GetTeam() == team)
        return vehicle;
    }
    return (RacingVehicle) null;
  }
}
