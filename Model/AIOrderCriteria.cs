// Decompiled with JetBrains decompiler
// Type: AIOrderCriteria
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

public class AIOrderCriteria
{
  public AIOrderCriteria.Type mType = AIOrderCriteria.Type.Count;
  public DialogCriteria.CriteriaOperator criteriaOperator;
  private int mCriteriaInfoEnum;
  private float mCriteriaInfoFloat;
  private bool mCriteriaInfoBool;
  private AIOrderCriteria.ValueType mSetValueType;
  private float mOutputData;

  public float outputData
  {
    get
    {
      return this.mOutputData;
    }
  }

  public void ClearData()
  {
    this.mType = AIOrderCriteria.Type.Count;
    this.mCriteriaInfoEnum = 0;
    this.mCriteriaInfoFloat = 0.0f;
    this.mCriteriaInfoBool = false;
    this.mSetValueType = AIOrderCriteria.ValueType.None;
  }

  public void SetupDataEnum(AIOrderCriteria.Type inType, int inInfo)
  {
    this.mType = inType;
    this.mCriteriaInfoEnum = inInfo;
    this.mSetValueType = AIOrderCriteria.ValueType.Enum;
  }

  public void SetupData(AIOrderCriteria.Type inType, float inInfo)
  {
    this.mType = inType;
    this.mCriteriaInfoFloat = inInfo;
    this.mSetValueType = AIOrderCriteria.ValueType.Float;
  }

  public void SetupData(AIOrderCriteria.Type inType, bool inInfo)
  {
    this.mType = inType;
    this.mCriteriaInfoBool = inInfo;
    this.mSetValueType = AIOrderCriteria.ValueType.Bool;
  }

  public void SetupDataEnum(int inInfo)
  {
    this.mCriteriaInfoEnum = inInfo;
    this.mSetValueType = AIOrderCriteria.ValueType.Enum;
  }

  public void SetupData(float inInfo)
  {
    this.mCriteriaInfoFloat = inInfo;
    this.mSetValueType = AIOrderCriteria.ValueType.Float;
  }

  public void SetupData(bool inInfo)
  {
    this.mCriteriaInfoBool = inInfo;
    this.mSetValueType = AIOrderCriteria.ValueType.Bool;
  }

  public bool HasValue()
  {
    return this.mSetValueType != AIOrderCriteria.ValueType.None;
  }

  public void SetOutputData(string inString)
  {
    this.mOutputData = Convert.ToSingle(inString);
  }

  public int GetCriteriaInfoEnum()
  {
    return this.mCriteriaInfoEnum;
  }

  public float GetCriteriaInfoFloat()
  {
    return this.mCriteriaInfoFloat;
  }

  public bool GetCriteriaInfoBool()
  {
    return this.mCriteriaInfoBool;
  }

  public object GetCriteriaInfo()
  {
    return (object) null;
  }

  public bool CriteriaMatch(AIOrderCriteria inCriteria)
  {
    if (this.mType == inCriteria.mType && this.HasValue() && inCriteria.HasValue())
    {
      switch (this.mSetValueType)
      {
        case AIOrderCriteria.ValueType.Enum:
          return this.CriteriaMatch(inCriteria.GetCriteriaInfoEnum());
        case AIOrderCriteria.ValueType.Float:
          return this.CriteriaMatch(inCriteria.GetCriteriaInfoFloat());
        case AIOrderCriteria.ValueType.Bool:
          return this.CriteriaMatch(inCriteria.GetCriteriaInfoBool());
      }
    }
    return false;
  }

  private bool CriteriaMatch(int inValue)
  {
    switch (this.criteriaOperator)
    {
      case DialogCriteria.CriteriaOperator.Equals:
        return this.mCriteriaInfoEnum == inValue;
      case DialogCriteria.CriteriaOperator.Greater:
        return inValue > this.mCriteriaInfoEnum;
      case DialogCriteria.CriteriaOperator.GreaterOrEquals:
        return inValue >= this.mCriteriaInfoEnum;
      case DialogCriteria.CriteriaOperator.Smaller:
        return inValue < this.mCriteriaInfoEnum;
      case DialogCriteria.CriteriaOperator.SmallerOrEquals:
        return inValue <= this.mCriteriaInfoEnum;
      case DialogCriteria.CriteriaOperator.Different:
        return this.mCriteriaInfoEnum != inValue;
      default:
        return false;
    }
  }

  private bool CriteriaMatch(float inValue)
  {
    switch (this.criteriaOperator)
    {
      case DialogCriteria.CriteriaOperator.Equals:
        return (double) this.mCriteriaInfoFloat == (double) inValue;
      case DialogCriteria.CriteriaOperator.Greater:
        return (double) inValue > (double) this.mCriteriaInfoFloat;
      case DialogCriteria.CriteriaOperator.GreaterOrEquals:
        return (double) inValue >= (double) this.mCriteriaInfoFloat;
      case DialogCriteria.CriteriaOperator.Smaller:
        return (double) inValue < (double) this.mCriteriaInfoFloat;
      case DialogCriteria.CriteriaOperator.SmallerOrEquals:
        return (double) inValue <= (double) this.mCriteriaInfoFloat;
      case DialogCriteria.CriteriaOperator.Different:
        return (double) this.mCriteriaInfoFloat != (double) inValue;
      default:
        return false;
    }
  }

  private bool CriteriaMatch(bool inValue)
  {
    switch (this.criteriaOperator)
    {
      case DialogCriteria.CriteriaOperator.Equals:
        return this.mCriteriaInfoBool == inValue;
      case DialogCriteria.CriteriaOperator.Different:
        return this.mCriteriaInfoBool != inValue;
      default:
        return false;
    }
  }

  public enum Type
  {
    SessionType,
    SessionCompletion,
    TyreCompound,
    IdealTyreTread,
    TyreWear,
    FuelDelta,
    TyreTemperature,
    LapsToGo,
    TeamMateStatus,
    GapToTeamMate,
    GapToAhead,
    GapToBehind,
    GapToLeader,
    SessionPosition,
    DriverChampPosition,
    TeamChampPosition,
    LowestCondition,
    WaterLevel,
    TeamAggression,
    SessionFlag,
    IsPitting,
    Fuel,
    DiceRoll,
    TeamMateChampPosition,
    CarAheadTyreWear,
    CarBehindTyreWear,
    CarAheadEngineMode,
    CarBehindEngineMode,
    CarAheadDrivingStyle,
    CarBehindDrivingStyle,
    CarAheadOrderedPit,
    CarBehindOrderedPit,
    LastRace,
    FirstRace,
    Outlap,
    InLap,
    TeamMateRetired,
    CanRefuel,
    IsInGarage,
    PredictedPositionDelta,
    GapToPredictedPosition,
    CloseToPitlaneEntry,
    TeamMateTyreWear,
    TeamMateFuelDelta,
    TeamMateIsPitting,
    TeamCompoundDelta,
    EngineCondition,
    AirTempTyreHeatingRate,
    FuelToTankCapacityPercentage,
    ERSBatteryCharge,
    IsERSActive,
    IsHybridModeActive,
    TeamOrder,
    DrivingStyle,
    EngineMode,
    PitOrder,
    SetStrategy,
    SetKnowledgeType,
    ERSOrder,
    Count,
  }

  public class TypeComparer : IEqualityComparer<AIOrderCriteria.Type>
  {
    public bool Equals(AIOrderCriteria.Type x, AIOrderCriteria.Type y)
    {
      return x == y;
    }

    public int GetHashCode(AIOrderCriteria.Type codeh)
    {
      return (int) codeh;
    }
  }

  private enum ValueType
  {
    None,
    Enum,
    Float,
    Bool,
  }
}
