// Decompiled with JetBrains decompiler
// Type: SessionAIOrder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

public class SessionAIOrder
{
  private List<AIOrderCriteria> mInputs = new List<AIOrderCriteria>();
  private List<AIOrderCriteria> mOutputs = new List<AIOrderCriteria>();
  private List<AIOrderCriteria.Type> mOutputTypes = new List<AIOrderCriteria.Type>();
  public bool debugThisOrder;
  private int mID;
  private SessionAIOrder.Category mCategory;

  public List<AIOrderCriteria> inputs
  {
    get
    {
      return this.mInputs;
    }
  }

  public List<AIOrderCriteria.Type> outputTypes
  {
    get
    {
      return this.mOutputTypes;
    }
  }

  public int ID
  {
    get
    {
      return this.mID;
    }
  }

  public SessionAIOrder.Category category
  {
    get
    {
      return this.mCategory;
    }
  }

  public SessionAIOrder()
  {
  }

  public SessionAIOrder(int inID, SessionAIOrder.Category inCategory, List<AIOrderCriteria> inInputs, List<AIOrderCriteria> inOutputs)
  {
    this.mID = inID;
    this.mCategory = inCategory;
    this.mInputs = inInputs;
    this.mOutputs = inOutputs;
    for (int index = 0; index < this.mOutputs.Count; ++index)
      this.mOutputTypes.Add(this.mOutputs[index].mType);
  }

  public bool IsValidForInputs(RacingVehicle inVehicle, AIOrderCriteria[] inInputs)
  {
    bool flag = true;
    for (int index1 = 0; index1 < this.mInputs.Count; ++index1)
    {
      AIOrderCriteria mInput = this.mInputs[index1];
      for (int index2 = 0; index2 < inInputs.Length; ++index2)
      {
        AIOrderCriteria inInput = inInputs[index2];
        if (mInput.CriteriaMatch(inInput))
        {
          flag = true;
          break;
        }
        flag = false;
      }
      if (!flag)
        break;
    }
    return flag;
  }

  public void ApplyOutputs(RacingVehicle inVehicle)
  {
    for (int index = 0; index < this.mOutputs.Count; ++index)
      this.ApplyOutputs(this.mOutputs[index], inVehicle);
  }

  public bool AreOutputsRelevant(RacingVehicle inVehicle)
  {
    bool flag = false;
    for (int index = 0; index < this.mOutputs.Count; ++index)
    {
      if (!this.IsOutputActive(this.mOutputs[index], inVehicle))
      {
        flag = true;
        break;
      }
    }
    return flag;
  }

  private bool IsOutputActive(AIOrderCriteria inOutput, RacingVehicle inVehicle)
  {
    switch (inOutput.mType)
    {
      case AIOrderCriteria.Type.TeamOrder:
        SessionStrategy.TeamOrders criteriaInfoEnum1 = (SessionStrategy.TeamOrders) inOutput.GetCriteriaInfoEnum();
        return inVehicle.strategy.teamOrders == criteriaInfoEnum1;
      case AIOrderCriteria.Type.DrivingStyle:
        DrivingStyle.Mode criteriaInfoEnum2 = (DrivingStyle.Mode) inOutput.GetCriteriaInfoEnum();
        return inVehicle.performance.drivingStyleMode == criteriaInfoEnum2;
      case AIOrderCriteria.Type.EngineMode:
        Fuel.EngineMode criteriaInfoEnum3 = (Fuel.EngineMode) inOutput.GetCriteriaInfoEnum();
        return inVehicle.performance.fuel.engineMode == criteriaInfoEnum3;
      case AIOrderCriteria.Type.PitOrder:
        SessionStrategy.PitOrder criteriaInfoEnum4 = (SessionStrategy.PitOrder) inOutput.GetCriteriaInfoEnum();
        if (inVehicle.strategy.IsGoingToPit())
          return inVehicle.strategy.HasPitOrder(criteriaInfoEnum4);
        return false;
      case AIOrderCriteria.Type.SetStrategy:
        SessionStrategy.PitStrategy criteriaInfoEnum5 = (SessionStrategy.PitStrategy) inOutput.GetCriteriaInfoEnum();
        return inVehicle.strategy.pitStrategy == criteriaInfoEnum5;
      case AIOrderCriteria.Type.SetKnowledgeType:
        PracticeReportSessionData.KnowledgeType criteriaInfoEnum6 = (PracticeReportSessionData.KnowledgeType) inOutput.GetCriteriaInfoEnum();
        return inVehicle.practiceKnowledge.knowledgeType == criteriaInfoEnum6;
      case AIOrderCriteria.Type.ERSOrder:
        ERSController.Mode criteriaInfoEnum7 = (ERSController.Mode) inOutput.GetCriteriaInfoEnum();
        return inVehicle.ERSController.mode == criteriaInfoEnum7;
      default:
        return false;
    }
  }

  private void ApplyOutputs(AIOrderCriteria inOutput, RacingVehicle inVehicle)
  {
    switch (inOutput.mType)
    {
      case AIOrderCriteria.Type.TeamOrder:
        this.ApplyTeamOrder(inVehicle, inOutput);
        break;
      case AIOrderCriteria.Type.SetKnowledgeType:
        this.ApplyKnowledgeType(inVehicle, inOutput);
        break;
    }
  }

  private void ApplyKnowledgeType(RacingVehicle inVehicle, AIOrderCriteria inOrder)
  {
    PracticeReportSessionData.KnowledgeType criteriaInfoEnum = (PracticeReportSessionData.KnowledgeType) inOrder.GetCriteriaInfoEnum();
    inVehicle.practiceKnowledge.knowledgeType = criteriaInfoEnum;
    switch (criteriaInfoEnum)
    {
      case PracticeReportSessionData.KnowledgeType.QualifyingTrim:
        inVehicle.setup.SetTargetTrim(SessionSetup.Trim.Qualifying);
        break;
      case PracticeReportSessionData.KnowledgeType.RaceTrim:
        inVehicle.setup.SetTargetTrim(SessionSetup.Trim.Race);
        break;
    }
  }

  private void ApplyPitOrder(RacingVehicle inVehicle, AIOrderCriteria inOrder)
  {
    if (Game.instance.sessionManager.sessionType == SessionDetails.SessionType.Race)
    {
      SessionStrategy.PitOrder criteriaInfoEnum = (SessionStrategy.PitOrder) inOrder.GetCriteriaInfoEnum();
      if (criteriaInfoEnum != SessionStrategy.PitOrder.CancelPit)
        return;
      inVehicle.strategy.SetAIPitOrder(criteriaInfoEnum);
    }
    else
    {
      if (!inVehicle.pathState.IsInPitlaneArea() && !inVehicle.strategy.HasCompletedOrderedLapCount())
        return;
      SessionStrategy.PitOrder criteriaInfoEnum = (SessionStrategy.PitOrder) inOrder.GetCriteriaInfoEnum();
      if (!inVehicle.pathState.IsInPitlaneArea() && !inVehicle.strategy.IsGoingToPit())
        inVehicle.strategy.ReturnToGarage();
      inVehicle.strategy.SetAIPitOrder(criteriaInfoEnum);
    }
  }

  private void ApplyTeamOrder(RacingVehicle inVehicle, AIOrderCriteria inOrder)
  {
    SessionStrategy.TeamOrders criteriaInfoEnum = (SessionStrategy.TeamOrders) inOrder.GetCriteriaInfoEnum();
    inVehicle.strategy.SetTeamOrders(criteriaInfoEnum);
  }

  public string GetDebugInfo()
  {
    string str = string.Empty;
    for (int index = 0; index < this.mOutputTypes.Count; ++index)
      str = str + (object) this.mOutputTypes[index] + " ";
    return string.Format("ID: {0}, {1}, {2},", (object) this.mID, (object) this.mCategory.ToString(), (object) str);
  }

  public enum Category
  {
    None,
    PitStop,
    Strategy,
    TeamOrders,
    PracticeStint,
    ERSOrder,
  }
}
