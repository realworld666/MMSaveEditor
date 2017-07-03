// Decompiled with JetBrains decompiler
// Type: SessionAIOrdersDatabase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

public class SessionAIOrdersDatabase
{
  private List<SessionAIOrder> mOrders = new List<SessionAIOrder>();
  private HashSet<AIOrderCriteria.Type> outcomes = new HashSet<AIOrderCriteria.Type>((IEqualityComparer<AIOrderCriteria.Type>) new AIOrderCriteria.TypeComparer());
  private int mOrdersCount;

  public List<SessionAIOrder> orders
  {
    get
    {
      return this.mOrders;
    }
  }

  public void LoadAIOrdersData(AssetManager inAssetManager)
  {
    List<DatabaseEntry> databaseEntryList = inAssetManager.ReadDatabase(Database.DatabaseType.SessionAIOrders);
    for (int index = 0; index < databaseEntryList.Count; ++index)
    {
      DatabaseEntry databaseEntry = databaseEntryList[index];
      int intValue = databaseEntry.GetIntValue("ID");
      SessionAIOrder.Category inCategory = (SessionAIOrder.Category) Enum.Parse(typeof (SessionAIOrder.Category), databaseEntry.GetStringValue("Category"));
      List<AIOrderCriteria> criteriaList1 = SessionAIOrdersDatabase.GetCriteriaList(databaseEntry.GetStringValue("Output"));
      List<AIOrderCriteria> criteriaList2 = SessionAIOrdersDatabase.GetCriteriaList(databaseEntry.GetStringValue("Source"));
      criteriaList2.AddRange((IEnumerable<AIOrderCriteria>) SessionAIOrdersDatabase.GetCriteriaList(databaseEntry.GetStringValue("Input")));
      SessionAIOrder sessionAiOrder = new SessionAIOrder(intValue, inCategory, criteriaList2, criteriaList1);
      this.mOrders.Add(sessionAiOrder);
      sessionAiOrder.debugThisOrder = bool.Parse(databaseEntry.GetStringValue("Debug"));
    }
    this.mOrders.Sort((Comparison<SessionAIOrder>) ((x, y) => y.inputs.Count.CompareTo(x.inputs.Count)));
    this.mOrdersCount = this.mOrders.Count;
  }

  public void GetRelevantOrders(RacingVehicle inVehicle, AIOrderCriteria[] inInputs, ref List<SessionAIOrder> selectedOrders)
  {
    for (int index1 = 0; index1 < this.mOrdersCount; ++index1)
    {
      SessionAIOrder mOrder = this.mOrders[index1];
      if (mOrder.IsValidForInputs(inVehicle, inInputs))
      {
        bool flag = true;
        for (int index2 = 0; index2 < mOrder.outputTypes.Count; ++index2)
        {
          if (this.outcomes.Contains(mOrder.outputTypes[index2]))
          {
            flag = false;
            break;
          }
        }
        if (flag)
        {
          for (int index2 = 0; index2 < mOrder.outputTypes.Count; ++index2)
            this.outcomes.Add(mOrder.outputTypes[index2]);
          selectedOrders.Add(mOrder);
        }
      }
    }
    this.outcomes.Clear();
  }

  public static List<AIOrderCriteria> GetCriteriaList(string inCriteria)
  {
    List<AIOrderCriteria> aiOrderCriteriaList = new List<AIOrderCriteria>();
    inCriteria = inCriteria.Trim();
    string[] strArray = inCriteria.Split(';');
    for (int index = 0; index < strArray.Length; ++index)
    {
      if (!(strArray[index] == string.Empty))
      {
        AIOrderCriteria criteria = SessionAIOrdersDatabase.GetCriteria(strArray[index]);
        if (criteria.HasValue())
          aiOrderCriteriaList.Add(criteria);
      }
    }
    return aiOrderCriteriaList;
  }

  private static AIOrderCriteria GetCriteria(string inString)
  {
    AIOrderCriteria inCriteria = new AIOrderCriteria();
    inCriteria.criteriaOperator = DialogRulesDatabase.SetOperator(inString);
    string[] separator = new string[6]{ "=", ">", ">=", "<", "<=", "!=" };
    string[] strArray = inString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
    for (int index1 = 0; index1 < strArray.Length; ++index1)
    {
      strArray[index1] = strArray[index1].Trim();
      if (strArray.Length >= 2)
      {
        if (index1 == 0)
          inCriteria.mType = (AIOrderCriteria.Type) Enum.Parse(typeof (AIOrderCriteria.Type), strArray[index1]);
        else if (index1 == 1)
        {
          string inData = strArray[index1];
          bool flag = false;
          string str = string.Empty;
          for (int index2 = 0; index2 < inData.Length; ++index2)
          {
            if ((int) inData[index2] == 40)
              flag = true;
            if (flag)
              str += (string) (object) inData[index2];
            if ((int) inData[index2] == 41)
              flag = false;
          }
          if (str != string.Empty)
          {
            inData = inData.Replace(str, string.Empty);
            str = str.Trim('(', ')');
          }
          SessionAIOrdersDatabase.SetCorrectObjecType(inCriteria, inData, str);
        }
        else
          Debug.Log((object) "Error, criteria split in more than 2 strings, database entry badly formated", (UnityEngine.Object) null);
      }
    }
    return inCriteria;
  }

  private static void SetCorrectObjecType(AIOrderCriteria inCriteria, string inData, string inExtraParameter)
  {
    switch (inCriteria.mType)
    {
      case AIOrderCriteria.Type.SessionType:
        inCriteria.SetupDataEnum((int) Enum.Parse(typeof (SessionDetails.SessionType), inData));
        break;
      case AIOrderCriteria.Type.TyreCompound:
        inCriteria.SetupDataEnum((int) Enum.Parse(typeof (TyreSet.Compound), inData));
        break;
      case AIOrderCriteria.Type.IdealTyreTread:
      case AIOrderCriteria.Type.IsPitting:
      case AIOrderCriteria.Type.CarAheadOrderedPit:
      case AIOrderCriteria.Type.CarBehindOrderedPit:
      case AIOrderCriteria.Type.LastRace:
      case AIOrderCriteria.Type.FirstRace:
      case AIOrderCriteria.Type.Outlap:
      case AIOrderCriteria.Type.InLap:
      case AIOrderCriteria.Type.TeamMateRetired:
      case AIOrderCriteria.Type.CanRefuel:
      case AIOrderCriteria.Type.IsInGarage:
      case AIOrderCriteria.Type.CloseToPitlaneEntry:
      case AIOrderCriteria.Type.TeamMateIsPitting:
      case AIOrderCriteria.Type.IsERSActive:
      case AIOrderCriteria.Type.IsHybridModeActive:
        inCriteria.SetupData(bool.Parse(inData));
        break;
      case AIOrderCriteria.Type.TeamMateStatus:
        inCriteria.SetupDataEnum((int) Enum.Parse(typeof (ContractPerson.Status), inData));
        break;
      case AIOrderCriteria.Type.SessionFlag:
        inCriteria.SetupDataEnum((int) Enum.Parse(typeof (SessionManager.Flag), inData));
        break;
      case AIOrderCriteria.Type.CarAheadEngineMode:
      case AIOrderCriteria.Type.CarBehindEngineMode:
      case AIOrderCriteria.Type.EngineMode:
        inCriteria.SetupDataEnum((int) Enum.Parse(typeof (Fuel.EngineMode), inData));
        break;
      case AIOrderCriteria.Type.CarAheadDrivingStyle:
      case AIOrderCriteria.Type.CarBehindDrivingStyle:
      case AIOrderCriteria.Type.DrivingStyle:
        inCriteria.SetupDataEnum((int) Enum.Parse(typeof (DrivingStyle.Mode), inData));
        break;
      case AIOrderCriteria.Type.TeamOrder:
        inCriteria.SetupDataEnum((int) Enum.Parse(typeof (SessionStrategy.TeamOrders), inData));
        break;
      case AIOrderCriteria.Type.PitOrder:
        inCriteria.SetupDataEnum((int) Enum.Parse(typeof (SessionStrategy.PitOrder), inData));
        break;
      case AIOrderCriteria.Type.SetStrategy:
        inCriteria.SetupDataEnum((int) Enum.Parse(typeof (SessionStrategy.PitStrategy), inData));
        break;
      case AIOrderCriteria.Type.SetKnowledgeType:
        inCriteria.SetupDataEnum((int) Enum.Parse(typeof (PracticeReportSessionData.KnowledgeType), inData));
        break;
      case AIOrderCriteria.Type.ERSOrder:
        inCriteria.SetupDataEnum((int) Enum.Parse(typeof (ERSController.Mode), inData));
        break;
      default:
        inCriteria.SetupData(float.Parse(inData));
        break;
    }
    if (!(inExtraParameter != string.Empty))
      return;
    inCriteria.SetOutputData(inExtraParameter);
  }
}
