// Decompiled with JetBrains decompiler
// Type: TutorialSimulation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

public class TutorialSimulation
{
  public void Start()
  {
  }

  public void OnTutorialSimulationExample(Person inSender)
  {
    this.AddTutorial(inSender, new DialogCriteria("Who", "Assistant"), new DialogCriteria("Source", "PitStops"), new DialogCriteria("Type", "Header"));
  }

  public void OnTutorialSimulationExample2(Person inSender)
  {
    this.AddTutorial(inSender, new DialogCriteria("Who", "Assistant"), new DialogCriteria("Source", "Reliability"), new DialogCriteria("Type", "Header"));
  }

  public void AddTutorial(params DialogCriteria[] inDialogCriteria)
  {
    this.AddTutorial((Person) null, inDialogCriteria);
  }

  public void AddTutorial(Person inSender, params DialogCriteria[] inDialogCriteria)
  {
    DialogRule rule = Game.instance.helpSystem.GetRule(inSender, inDialogCriteria);
    if (rule == null)
      return;
    this.AddTutorial(rule);
  }

  public void AddTutorial(DialogRule inRule)
  {
    if (inRule == null || Game.instance == null || Game.instance.tutorialInfo == null)
      return;
    Game.instance.tutorialInfo.AddSimulationTutorial(inRule, TutorialInfo.RuleStatus.Triggered);
  }

  public void SendTutorial(params DialogCriteria[] inDialogCriteria)
  {
    this.SendTutorial((Person) null, inDialogCriteria);
  }

  public void SendTutorial(Person inSender, params DialogCriteria[] inDialogCriteria)
  {
    DialogRule rule = Game.instance.helpSystem.GetRule(inSender, inDialogCriteria);
    if (rule == null)
      return;
    this.SendTutorial(rule, false);
  }

  public void SendTutorial(DialogRule inRule, bool inUnviewed)
  {
    if (inRule == null)
      return;
    TutorialPopupDialog dialog = UIManager.instance.dialogBoxManager.GetDialog<TutorialPopupDialog>();
    DialogRule inRule1 = inRule;
    if (inUnviewed)
      inRule1 = Game.instance.tutorialInfo.GetFirstUnviewedRule(inRule, true, false) ?? inRule;
    string criteriaValue1 = inRule1.GetCriteriaValue("Source");
    if (inRule1.gameArea != dialog.lastGameArea || inRule1.gameArea == dialog.lastGameArea && criteriaValue1 != dialog.lastSource)
      dialog.ClearRulesInList();
    if (inRule1.triggerQuery != null && inRule1.GetCriteriaValue("Type") == "Header")
      inRule1 = Game.instance.helpSystem.GetRule((Person) null, inRule1.triggerQuery.criteriaList.ToArray()) ?? inRule;
    if (inRule1.userData != null && !string.IsNullOrEmpty(inRule1.GetUserDataValue("Header")))
    {
      string criteriaValue2 = inRule1.GetCriteriaValue("Source");
      DialogQuery inQuery = new DialogQuery();
      inQuery.AddCriteria("Source", criteriaValue2);
      inQuery.AddCriteria("Type", "Header");
      DialogRule ruleByGameArea = App.instance.dialogRulesManager.GetRuleByGameArea(inRule1.gameArea, inQuery, new List<DialogRule>());
      if (ruleByGameArea != null)
        dialog.AddRuleInList(ruleByGameArea);
    }
    Person person = Game.instance.dialogSystem.FindPerson(inRule1.who.mCriteriaInfo);
    DialogQuery inQuery1 = new DialogQuery();
    inQuery1.AddCriteria("Type", "Header");
    DialogQuery inQuery2 = new DialogQuery();
    inQuery2.AddCriteria("Type", "Button");
    List<DialogRule> rulesByGameArea1 = App.instance.dialogRulesManager.GetRulesByGameArea(inRule1.gameArea, inQuery1, new List<DialogRule>());
    List<DialogRule> rulesByGameArea2 = App.instance.dialogRulesManager.GetRulesByGameArea("Tutorial", inQuery2, new List<DialogRule>());
    dialog.OpenTutorial(person, rulesByGameArea1, rulesByGameArea2, inRule1, true);
  }
}
