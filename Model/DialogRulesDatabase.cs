// Decompiled with JetBrains decompiler
// Type: DialogRulesDatabase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

public class DialogRulesDatabase
{
  public List<DialogRule> rulesList = new List<DialogRule>();

  public void LoadGameDialogRules(AssetManager inAssetManager)
  {
    List<DatabaseEntry> inDataList1 = inAssetManager.ReadDatabase(Database.DatabaseType.FrontEnd);
    List<DatabaseEntry> inDataList2 = inAssetManager.ReadDatabase(Database.DatabaseType.MediaStories);
    List<DatabaseEntry> inDataList3 = inAssetManager.ReadDatabase(Database.DatabaseType.MediaInterviews);
    List<DatabaseEntry> inDataList4 = inAssetManager.ReadDatabase(Database.DatabaseType.MediaTweets);
    List<DatabaseEntry> inDataList5 = inAssetManager.ReadDatabase(Database.DatabaseType.MessageDialogs);
    List<DatabaseEntry> inDataList6 = inAssetManager.ReadDatabase(Database.DatabaseType.RaceEventDialogs);
    List<DatabaseEntry> inDataList7 = inAssetManager.ReadDatabase(Database.DatabaseType.PreRaceTalkDialogs);
    List<DatabaseEntry> inDataList8 = inAssetManager.ReadDatabase(Database.DatabaseType.SimulationDialogs);
    List<DatabaseEntry> inDataList9 = inAssetManager.ReadDatabase(Database.DatabaseType.TeamRadioDialogs);
    List<DatabaseEntry> inDataList10 = inAssetManager.ReadDatabase(Database.DatabaseType.DilemmaDialogs);
    List<DatabaseEntry> inDataList11 = inAssetManager.ReadDatabase(Database.DatabaseType.TutorialDialogs);
    DialogRulesDatabase.LoadRules(inDataList1, ref this.rulesList);
    DialogRulesDatabase.LoadRules(inDataList2, ref this.rulesList);
    DialogRulesDatabase.LoadRules(inDataList3, ref this.rulesList);
    DialogRulesDatabase.LoadRules(inDataList4, ref this.rulesList);
    DialogRulesDatabase.LoadRules(inDataList5, ref this.rulesList);
    DialogRulesDatabase.LoadRules(inDataList6, ref this.rulesList);
    DialogRulesDatabase.LoadRules(inDataList7, ref this.rulesList);
    DialogRulesDatabase.LoadRules(inDataList8, ref this.rulesList);
    DialogRulesDatabase.LoadRules(inDataList9, ref this.rulesList);
    DialogRulesDatabase.LoadRules(inDataList10, ref this.rulesList);
    DialogRulesDatabase.LoadRules(inDataList11, ref this.rulesList);
  }

  public static void LoadRules(List<DatabaseEntry> inDataList, ref List<DialogRule> resultList)
  {
    for (int index = 0; index < inDataList.Count; ++index)
    {
      DatabaseEntry inData = inDataList[index];
      DialogRule inRule = new DialogRule();
      inRule.databaseIndex = index.ToString();
      inRule.localisationID = inData.GetStringValue("ID");
      if (inRule.localisationID.Contains("PSG_"))
      {
        inRule.gameArea = inData.GetStringValue("Game Area");
        if (inData.GetStringValue("Memory") != "0")
          DialogRulesDatabase.AddMemory(inRule, inData.GetStringValue("Memory"));
        inRule.criteriaList.Add(new DialogCriteria("Source", inData.GetStringValue("Source")));
        inRule.who = new DialogCriteria("Who", inData.GetStringValue("Who"));
        if (inData.GetStringValue("Criteria") != "0")
          inRule.criteriaList.AddRange((IEnumerable<DialogCriteria>) DialogRulesDatabase.GetCriteriaList(inData.GetStringValue("Criteria")));
        if (inData.GetStringValue("User Data") != "0")
          DialogRulesDatabase.AddUserData(inRule, inData.GetStringValue("User Data"));
        if (inData.GetStringValue("Trigger") != "0")
          DialogRulesDatabase.AddTrigger(inRule, inData.GetStringValue("Trigger"));
        resultList.Add(inRule);
      }
    }
  }

  public static void AddMemory(DialogRule inRule, string inCriteria)
  {
    inCriteria = inCriteria.Trim();
    string[] strArray = inCriteria.Split(';');
    for (int index = 0; index < strArray.Length; ++index)
    {
      if (!(strArray[index] == string.Empty))
      {
        DialogCriteria criteria = DialogRulesDatabase.GetCriteria(strArray[index]);
        inRule.remember.Add(criteria);
      }
    }
  }

  public static void AddUserData(DialogRule inRule, string inCriteria)
  {
    inCriteria = inCriteria.Trim();
    string[] strArray = inCriteria.Split(';');
    for (int index = 0; index < strArray.Length; ++index)
    {
      if (!(strArray[index] == string.Empty))
      {
        DialogCriteria criteria = DialogRulesDatabase.GetCriteria(strArray[index]);
        inRule.userData.Add(criteria);
      }
    }
  }

  public static void AddTrigger(DialogRule inRule, string inCriteria)
  {
    inRule.triggerQuery = new DialogQuery();
    inCriteria = inCriteria.Trim();
    string[] strArray = inCriteria.Split(';');
    for (int index = 0; index < strArray.Length; ++index)
    {
      if (!(strArray[index] == string.Empty))
      {
        DialogCriteria criteria = DialogRulesDatabase.GetCriteria(strArray[index]);
        if (criteria.mType == "Who")
          inRule.triggerQuery.who = criteria;
        else
          inRule.triggerQuery.criteriaList.Add(criteria);
      }
    }
    if (!(inRule.triggerQuery.GetSource() == "Error"))
      return;
    inRule.triggerQuery.AddCriteria("Source", inRule.GetSource());
  }

  public static List<DialogCriteria> GetCriteriaList(string inCriteria)
  {
    List<DialogCriteria> dialogCriteriaList = new List<DialogCriteria>();
    inCriteria = inCriteria.Trim();
    string[] strArray = inCriteria.Split(';');
    for (int index = 0; index < strArray.Length; ++index)
    {
      if (!(strArray[index] == string.Empty))
      {
        DialogCriteria criteria = DialogRulesDatabase.GetCriteria(strArray[index]);
        if (criteria.mCriteriaInfo != string.Empty)
          dialogCriteriaList.Add(criteria);
      }
    }
    return dialogCriteriaList;
  }

  private static DialogCriteria GetCriteria(string inString)
  {
    DialogCriteria dialogCriteria = new DialogCriteria();
    dialogCriteria.criteriaOperator = DialogRulesDatabase.SetOperator(inString);
    string[] separator = new string[6]{ "=", ">", ">=", "<", "<=", "!=" };
    string[] strArray = inString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
    for (int index = 0; index < strArray.Length; ++index)
    {
      strArray[index] = strArray[index].Trim();
      if (strArray.Length >= 2)
      {
        if (index == 0)
          dialogCriteria.mType = strArray[index];
        else if (index == 1)
        {
          dialogCriteria.mCriteriaInfo = strArray[index];
          dialogCriteria.SetParsedData();
        }
        else
          Debug.Log((object) "Error, criteria split in more than 2 strings, database entry badly formated", (UnityEngine.Object) null);
      }
    }
    return dialogCriteria;
  }

  public static DialogCriteria.CriteriaOperator SetOperator(string inString)
  {
    if (inString.Contains("!="))
      return DialogCriteria.CriteriaOperator.Different;
    if (inString.Contains(">="))
      return DialogCriteria.CriteriaOperator.GreaterOrEquals;
    if (inString.Contains("<="))
      return DialogCriteria.CriteriaOperator.SmallerOrEquals;
    if (inString.Contains("<"))
      return DialogCriteria.CriteriaOperator.Smaller;
    return inString.Contains(">") ? DialogCriteria.CriteriaOperator.Greater : DialogCriteria.CriteriaOperator.Equals;
  }
}
