// Decompiled with JetBrains decompiler
// Type: HelpSystem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class HelpSystem
{
  public void OnTutorialExample(Person inSender)
  {
    this.SendTutorial(false, inSender, new DialogCriteria("Who", "Assistant"), new DialogCriteria("Source", "HQMain"), new DialogCriteria("Type", "Header"));
  }

  public void OnTutorialExample2(Person inSender)
  {
    this.SendTutorial(false, inSender, new DialogCriteria("Who", "Assistant"), new DialogCriteria("Source", "ProfileMain"), new DialogCriteria("Type", "Body0"));
  }

  public void SendTutorial(bool inForceOpen, params DialogCriteria[] inDialogCriteria)
  {
    this.SendTutorial(inForceOpen, (Person) null, inDialogCriteria);
  }

  public void SendTutorial(bool inForceOpen, Person inSender, params DialogCriteria[] inDialogCriteria)
  {
    DialogRule rule = this.GetRule(inSender, inDialogCriteria);
    if (rule == null)
      return;
    this.SendTutorial(inForceOpen, rule);
  }

  public void SendTutorial(bool inForceOpen, DialogRule inRule)
  {
    if (inRule == null)
      return;
    TutorialPopupDialog dialog = UIManager.instance.dialogBoxManager.GetDialog<TutorialPopupDialog>();
    DialogRule inRule1 = inRule;
    string criteriaValue1 = inRule1.GetCriteriaValue("Source");
    bool flag = Game.instance.tutorialInfo.CheckTutorial(inRule1.gameArea);
    if (!inForceOpen && flag)
      return;
    if (inRule1.gameArea != dialog.lastGameArea || inRule1.gameArea == dialog.lastGameArea && criteriaValue1 != dialog.lastSource)
      dialog.ClearRulesInList();
    if (inRule1.triggerQuery != null && inRule1.GetCriteriaValue("Type") == "Header")
      inRule1 = this.GetRule((Person) null, inRule1.triggerQuery.criteriaList.ToArray()) ?? inRule;
    if (inRule1.userData != null)
    {
      string str = inRule1.GetUserDataValue("Header");
      if (string.IsNullOrEmpty(str))
        str = inRule1.GetSource();
      if (!string.IsNullOrEmpty(str))
      {
        string criteriaValue2 = inRule1.GetCriteriaValue("Source");
        DialogQuery inQuery = new DialogQuery();
        inQuery.AddCriteria("Source", criteriaValue2);
        inQuery.AddCriteria("Type", "Header");
        DialogRule ruleByGameArea = App.instance.dialogRulesManager.GetRuleByGameArea(inRule1.gameArea, inQuery, new List<DialogRule>());
        if (ruleByGameArea != null)
          dialog.AddRuleInList(ruleByGameArea);
      }
    }
    Person person = Game.instance.dialogSystem.FindPerson(inRule1.who.mCriteriaInfo);
    DialogQuery inQuery1 = new DialogQuery();
    inQuery1.AddCriteria("Type", "Header");
    DialogQuery inQuery2 = new DialogQuery();
    inQuery2.AddCriteria("Type", "Button");
    List<DialogRule> rulesByGameArea1 = App.instance.dialogRulesManager.GetRulesByGameArea(inRule1.gameArea, inQuery1, new List<DialogRule>());
    List<DialogRule> rulesByGameArea2 = App.instance.dialogRulesManager.GetRulesByGameArea("Tutorial", inQuery2, new List<DialogRule>());
    dialog.OpenTutorial(person, rulesByGameArea1, rulesByGameArea2, inRule1, false);
  }

  public void OnInfoHelp()
  {
    if (!((Object) UIManager.instance != (Object) null) || !((Object) UIManager.instance.currentScreen != (Object) null))
      return;
    this.ShowTutorial("Tutorial - " + UIManager.instance.currentScreen.name);
  }

  private void ShowTutorial(string inTutorialName)
  {
    if (!this.CheckTutorialLoaded(inTutorialName) || Game.instance.tutorialInfo.CheckTutorial(inTutorialName))
      return;
    DialogQuery inQuery = new DialogQuery();
    inQuery.AddCriteria("Type", "Header");
    List<DialogRule> rulesByGameArea = App.instance.dialogRulesManager.GetRulesByGameArea(inTutorialName, inQuery, new List<DialogRule>());
    if (rulesByGameArea.Count <= 0)
      return;
    DialogRule firstItemByOrder = this.GetFirstItemByOrder(rulesByGameArea);
    if (firstItemByOrder == null || !(firstItemByOrder.GetCriteriaValue("Source") != "Error"))
      return;
    DialogRule firstUnviewedRule = Game.instance.tutorialInfo.GetFirstUnviewedRule(firstItemByOrder, false, false);
    if (firstUnviewedRule != null)
    {
      this.SendTutorial(true, firstUnviewedRule);
    }
    else
    {
      DialogRule rule = this.GetRule((Person) null, firstItemByOrder.triggerQuery.criteriaList.ToArray());
      if (rule == null)
        return;
      this.SendTutorial(true, rule);
    }
  }

  public bool CheckTutorialLoaded(string inTutorial)
  {
    return App.instance.dialogRulesManager.IsTutorialLoaded(inTutorial);
  }

  public DialogRule GetRule(Person inSender, params DialogCriteria[] inDialogCriteria)
  {
    DialogQuery inQuery = new DialogQuery();
    List<DialogCriteria> dialogCriteriaList = new List<DialogCriteria>((IEnumerable<DialogCriteria>) inDialogCriteria);
    if (dialogCriteriaList.Count > 0)
    {
      for (int index = 0; index < dialogCriteriaList.Count; ++index)
      {
        if (dialogCriteriaList[index].mType == "Who")
          inQuery.who = dialogCriteriaList[index];
        else
          inQuery.AddCriteria(dialogCriteriaList[index].mType, dialogCriteriaList[index].mCriteriaInfo);
      }
      if (inQuery.GetCriteriaValue("Type") == "Error")
        inQuery.AddCriteria("Type", "Header");
      DialogRule dialogRule = App.instance.dialogRulesManager.ProcessQuery(inQuery, true);
      if (dialogRule != null)
        return dialogRule;
      string str = "Message parameters not met, Criteria: ";
      for (int index = 0; index < dialogCriteriaList.Count; ++index)
        str = str + "  " + dialogCriteriaList[index].mType + " = " + dialogCriteriaList[index].mCriteriaInfo + ";";
      Debug.Log((object) str, (Object) null);
    }
    return (DialogRule) null;
  }

  public DialogRule GetFirstItemByOrder(List<DialogRule> inList)
  {
    int num = 100;
    DialogRule dialogRule = (DialogRule) null;
    for (int index1 = 0; index1 < inList.Count; ++index1)
    {
      if (index1 == 0)
        dialogRule = inList[index1];
      for (int index2 = 0; index2 < inList[index1].userData.Count; ++index2)
      {
        if (inList[index1].userData[index2].mType == "Order" && int.Parse(inList[index1].userData[index2].mCriteriaInfo) < num)
        {
          num = int.Parse(inList[index1].userData[index2].mCriteriaInfo);
          dialogRule = inList[index1];
        }
      }
    }
    return dialogRule;
  }
}
