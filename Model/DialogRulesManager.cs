// Decompiled with JetBrains decompiler
// Type: DialogRulesManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

public class DialogRulesManager
{
  public Dictionary<string, DialogRule> rulesByID = new Dictionary<string, DialogRule>();
  private List<DialogRule> rulesList = new List<DialogRule>();
  private Dictionary<string, List<DialogRule>> rules = new Dictionary<string, List<DialogRule>>();
  private Dictionary<string, List<DialogRule>> rulesGameArea = new Dictionary<string, List<DialogRule>>();

  public void CreateDictionary(List<DialogRule> inList)
  {
    for (int index = 0; index < inList.Count; ++index)
    {
      string source = inList[index].GetSource();
      if (!this.rules.ContainsKey(source))
      {
        this.rules[source] = new List<DialogRule>();
        this.rules[source].Add(inList[index]);
      }
      else
        this.rules[source].Add(inList[index]);
      string gameArea = inList[index].gameArea;
      if (!this.rulesGameArea.ContainsKey(gameArea))
      {
        this.rulesGameArea[gameArea] = new List<DialogRule>();
        this.rulesGameArea[gameArea].Add(inList[index]);
      }
      else
        this.rulesGameArea[gameArea].Add(inList[index]);
      string localisationId = inList[index].localisationID;
      if (!this.rulesByID.ContainsKey(localisationId))
        this.rulesByID[localisationId] = inList[index];
      else
        Debug.LogError((object) string.Format("Duplicate ID Found: {0}", (object) localisationId), (UnityEngine.Object) null);
    }
    this.rulesList = new List<DialogRule>((IEnumerable<DialogRule>) inList);
  }

  public string[] GetMailHeadersIDS()
  {
    List<string> stringList = new List<string>();
    for (int index = 0; index < this.rulesList.Count; ++index)
    {
      DialogRule rules = this.rulesList[index];
      if (rules.HasCriteria("Type", "Header"))
        stringList.Add(rules.localisationID);
    }
    return stringList.ToArray();
  }

  public string[] GetMediaStoriesHeaderIDs()
  {
    List<string> stringList = new List<string>();
    for (int index = 0; index < this.rulesList.Count; ++index)
    {
      DialogRule rules = this.rulesList[index];
      if ((rules.GetSource().Contains("PostRace") || rules.GetSource().Contains("PostQualifying") || rules.GetSource().Contains("PostPractice")) && rules.HasCriteria("Type", "Header"))
        stringList.Add(rules.localisationID);
    }
    return stringList.ToArray();
  }

  public string[] GetDilemmaHeaderIDS()
  {
    List<string> stringList = new List<string>();
    for (int index = 0; index < this.rulesList.Count; ++index)
    {
      DialogRule rules = this.rulesList[index];
      if (rules.GetSource().Equals("dilemma", StringComparison.OrdinalIgnoreCase) && rules.HasCriteria("Type", "Header"))
        stringList.Add(rules.localisationID);
    }
    return stringList.ToArray();
  }

  public string[] GetTweetIDS()
  {
    List<string> stringList = new List<string>();
    for (int index = 0; index < this.rulesList.Count; ++index)
    {
      DialogRule rules = this.rulesList[index];
      if (rules.GetSource().Contains("Tweet"))
        stringList.Add(rules.localisationID);
    }
    return stringList.ToArray();
  }

  public DialogRule ProcessQuery(DialogQuery inQuery, bool inIgnoreRules)
  {
    List<DialogRule> outRulesIgnored = new List<DialogRule>();
    DialogRule rule = this.GetRule(out outRulesIgnored, inQuery);
    if (rule == null)
    {
      if (Game.IsActive())
      {
        for (int index = 0; index < outRulesIgnored.Count; ++index)
          Game.instance.stateInfo.rulesToIgnore.Remove(outRulesIgnored[index]);
      }
      rule = this.GetRule(out outRulesIgnored, inQuery);
    }
    if (Game.IsActive() && rule != null && !inIgnoreRules)
      Game.instance.stateInfo.rulesToIgnore.Add(rule);
    return rule;
  }

  private DialogRule GetRule(out List<DialogRule> outRulesIgnored, DialogQuery inQuery)
  {
    DialogRule dialogRule = (DialogRule) null;
    List<DialogRule> dialogRuleList = new List<DialogRule>();
    List<DialogRule> validRules = this.GetValidRules(inQuery, out outRulesIgnored);
    if (validRules.Count > 0)
      dialogRule = validRules[UnityEngine.Random.Range(0, validRules.Count)];
    for (int index = 0; index < validRules.Count; ++index)
    {
      if (dialogRule == null || validRules[index].criteriaList.Count > dialogRule.criteriaList.Count)
        dialogRule = validRules[index];
    }
    return dialogRule;
  }

  public List<DialogRule> GetValidRulesForInterviewAnswers(DialogQuery inQuery)
  {
    List<DialogRule> dialogRuleList = new List<DialogRule>();
    string source = inQuery.GetSource();
    if (this.rules.ContainsKey(source))
    {
      for (int index = 0; index < this.rules[source].Count; ++index)
      {
        if (this.rules[source][index].MatchsQuery(inQuery))
          dialogRuleList.Add(this.rules[source][index]);
      }
    }
    else
      Debug.LogError((object) ("Source not in the rules dictionary: " + source), (UnityEngine.Object) null);
    return dialogRuleList;
  }

  public List<DialogRule> GetValidRules(DialogQuery inQuery)
  {
    List<DialogRule> outRulesIgnored = new List<DialogRule>();
    return this.GetValidRules(inQuery, out outRulesIgnored);
  }

  public List<DialogRule> GetValidRules(DialogQuery inQuery, out List<DialogRule> outRulesIgnored)
  {
    List<DialogRule> dialogRuleList1 = new List<DialogRule>();
    List<DialogRule> dialogRuleList2 = new List<DialogRule>();
    string source = inQuery.GetSource();
    if (this.rules.ContainsKey(source))
    {
      for (int index = 0; index < this.rules[source].Count; ++index)
      {
        DialogRule dialogRule = this.rules[source][index];
        if (dialogRule.MatchsQuery(inQuery))
        {
          if (Game.IsActive() && Game.instance.stateInfo.rulesToIgnore.Contains(dialogRule))
            dialogRuleList2.Add(dialogRule);
          else
            dialogRuleList1.Add(this.rules[source][index]);
        }
      }
    }
    else
      Debug.LogError((object) ("Source not in the rules dictionary: {" + source + "}, check the excel file for a rule with the source."), (UnityEngine.Object) null);
    outRulesIgnored = dialogRuleList2;
    return dialogRuleList1;
  }

  public DialogRule GetRuleByID(string inRuleID)
  {
    for (int index = 0; index < this.rulesList.Count; ++index)
    {
      if (this.rulesList[index].localisationID == inRuleID)
        return this.rulesList[index];
    }
    return (DialogRule) null;
  }

  public DialogRule GetRuleByGameArea(string inGameArea, DialogQuery inQuery, List<DialogRule> inRulesToIgnore)
  {
    string key = inGameArea;
    if (this.rulesGameArea.ContainsKey(key))
    {
      for (int index = 0; index < this.rulesGameArea[key].Count; ++index)
      {
        DialogRule dialogRule = this.rulesGameArea[key][index];
        if (dialogRule.MatchsQuery(inQuery) && !inRulesToIgnore.Contains(dialogRule))
          return dialogRule;
      }
    }
    else
      Debug.LogError((object) ("Source not in the rules dictionary: " + key), (UnityEngine.Object) null);
    return (DialogRule) null;
  }

  public List<DialogRule> GetRulesByGameArea(string inGameArea, DialogQuery inQuery, List<DialogRule> inRulesToIgnore)
  {
    List<DialogRule> dialogRuleList = new List<DialogRule>();
    string key = inGameArea;
    if (this.rulesGameArea.ContainsKey(key))
    {
      for (int index = 0; index < this.rulesGameArea[key].Count; ++index)
      {
        DialogRule dialogRule = this.rulesGameArea[key][index];
        if (dialogRule.PartialMatch(inQuery, "Source") && !inRulesToIgnore.Contains(dialogRule))
          dialogRuleList.Add(dialogRule);
      }
    }
    else
      Debug.LogError((object) ("Source not in the rules dictionary: " + key), (UnityEngine.Object) null);
    return dialogRuleList;
  }

  public List<DialogRule> GetAllRulesByGameArea(string inGameArea)
  {
    List<DialogRule> dialogRuleList = new List<DialogRule>();
    string key = inGameArea;
    if (this.rulesGameArea.ContainsKey(key))
      dialogRuleList = this.rulesGameArea[key];
    else
      Debug.LogError((object) ("GameArea Rules not Found: " + key), (UnityEngine.Object) null);
    return dialogRuleList;
  }

  public bool IsTutorialLoaded(string inTutorial)
  {
    return this.rulesGameArea.ContainsKey(inTutorial) && this.rulesGameArea[inTutorial].Count >= 2;
  }
}
