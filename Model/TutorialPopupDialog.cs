// Decompiled with JetBrains decompiler
// Type: TutorialPopupDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialPopupDialog : UIDialogBox
{
  private string mLastGameArea = string.Empty;
  private string mLastSource = string.Empty;
  private List<DialogRule> mRulesList = new List<DialogRule>();
  public RectTransform rectTransform;
  public TextMeshProUGUI name;
  public TextMeshProUGUI jobTitle;
  public UICharacterPortrait portrait;
  public TextMeshProUGUI sceneName;
  public TextMeshProUGUI mainText;
  public Button closeButton;
  public ToggleGroup toggleGroup;
  public UIGridList buttons;
  public UIGridList topics;
  private bool mSimulation;
  private DialogRule mRule;
  private DialogRule mSelectedTopic;

  public string lastGameArea
  {
    get
    {
      return this.mLastGameArea;
    }
  }

  public string lastSource
  {
    get
    {
      return this.mLastSource;
    }
  }

  protected override void Awake()
  {
    base.Awake();
    this.closeButton.onClick.AddListener(new UnityAction(this.OnClose));
  }

  public void Update()
  {
    if (!this.HasUserClickedAwayFromObject())
      return;
    this.OnEndTutorial(true, false);
  }

  public void OpenTutorial(Person inPerson, List<DialogRule> inTopicRules, List<DialogRule> inButtonRules, DialogRule inRule, bool inSimulation)
  {
    if (!this.gameObject.activeSelf)
    {
      Game.instance.time.Pause(GameTimer.PauseType.Tutorial);
      this.gameObject.SetActive(true);
    }
    this.mSimulation = inSimulation;
    this.mLastSource = inRule.GetCriteriaValue("Source");
    this.mLastGameArea = inRule.gameArea;
    if (inPerson == null)
      ;
    this.mRule = inRule;
    this.mSelectedTopic = this.GetTopicRule(inRule, inTopicRules);
    this.sceneName.text = Localisation.LocaliseID(this.mSelectedTopic.localisationID, (GameObject) null);
    this.mainText.text = Localisation.LocaliseID(inRule.localisationID, (GameObject) null);
    if (inTopicRules.Count > 0)
      this.SetTopics(inTopicRules);
    if (inButtonRules.Count <= 0)
      return;
    this.SetButtons(inButtonRules);
  }

  public override void Hide()
  {
    Game.instance.time.UnPause(GameTimer.PauseType.Tutorial);
    base.Hide();
  }

  public void OnClose()
  {
    this.OnEndTutorial(true, false);
  }

  public void AddRuleInList(DialogRule inRule)
  {
    if (this.GetRuleInList(inRule))
      return;
    this.mRulesList.Add(inRule);
  }

  public void AddRulesInList(params DialogRule[] inRules)
  {
    for (int index = 0; index < inRules.Length; ++index)
    {
      if (!this.GetRuleInList(inRules[index]))
        this.mRulesList.Add(inRules[index]);
    }
  }

  public bool GetRuleInList(DialogRule inRule)
  {
    for (int index = 0; index < this.mRulesList.Count; ++index)
    {
      if (this.mRulesList[index] == inRule)
        return true;
    }
    return false;
  }

  public void TriggerViewRulesInList()
  {
    if (this.mRulesList.Count > 0)
    {
      for (int index = 0; index < this.mRulesList.Count; ++index)
      {
        if (!this.mSimulation)
          Game.instance.tutorialInfo.AddRule(this.mRulesList[index]);
        else
          Game.instance.tutorialInfo.AddSimulationRule(this.mRulesList[index]);
      }
    }
    this.ClearRulesInList();
  }

  public void ClearRulesInList()
  {
    this.mRulesList.Clear();
  }

  public void SetButtons(List<DialogRule> inButtonRules)
  {
    this.buttons.DestroyListItems();
    this.buttons.itemPrefab.SetActive(true);
    for (int index1 = 0; index1 < inButtonRules.Count; ++index1)
    {
      for (int index2 = 0; index2 < inButtonRules[index1].userData.Count; ++index2)
      {
        if (inButtonRules[index1].userData[index2].mType == "Order" && inButtonRules[index1].userData[index2].mCriteriaInfo == index1.ToString())
        {
          TutorialButtonWidget listItem = this.buttons.CreateListItem<TutorialButtonWidget>();
          listItem.Setup(Localisation.LocaliseID(inButtonRules[index1].localisationID, (GameObject) null));
          this.SetButton(listItem.button, inButtonRules[index1]);
        }
      }
    }
    this.buttons.itemPrefab.SetActive(false);
  }

  public void SetTopics(List<DialogRule> inTopicRules)
  {
    if (inTopicRules.Count <= 0)
      return;
    List<DialogRule> dialogRuleList = new List<DialogRule>();
    int inIndex1 = 0;
    this.topics.DestroyListItems();
    this.topics.itemPrefab.SetActive(true);
    for (int index = 0; index < inTopicRules.Count; ++index)
    {
      TutorialToggleWidget listItem = this.topics.CreateListItem<TutorialToggleWidget>();
      listItem.toggle.isOn = false;
      this.toggleGroup.RegisterToggle(listItem.toggle);
      listItem.toggle.group = this.toggleGroup;
    }
    this.topics.itemPrefab.SetActive(false);
    if (inTopicRules.Count == 1)
      this.AssignTopic(0, inTopicRules[0]);
    else if (inTopicRules.Count > 1)
    {
      for (int inIndex2 = 0; inIndex2 < inTopicRules.Count; ++inIndex2)
      {
        bool flag = false;
        for (int index = 0; index < inTopicRules[inIndex2].userData.Count; ++index)
        {
          if (inTopicRules[inIndex2].userData[index].mType == "Order" && inTopicRules[inIndex2].userData[index].mCriteriaInfo == inIndex2.ToString())
          {
            this.AssignTopic(inIndex2, inTopicRules[inIndex2]);
            ++inIndex1;
            flag = true;
          }
        }
        if (!flag)
          dialogRuleList.Add(inTopicRules[inIndex2]);
      }
    }
    if (dialogRuleList.Count <= 0)
      return;
    for (int index = 0; index < dialogRuleList.Count; ++index)
    {
      this.AssignTopic(inIndex1, dialogRuleList[index]);
      ++inIndex1;
    }
  }

  public void AssignTopic(int inIndex, DialogRule inRule)
  {
    TutorialToggleWidget tutorialToggleWidget = this.topics.GetItem<TutorialToggleWidget>(inIndex);
    tutorialToggleWidget.Setup(Localisation.LocaliseID(inRule.localisationID, (GameObject) null));
    this.SetTopic(tutorialToggleWidget.toggle, inRule);
    if (!(Game.instance != null & Game.instance.tutorialInfo != null))
      return;
    bool inTickValue = this.mSimulation ? Game.instance.tutorialInfo.CheckSimulationRule(inRule) : Game.instance.tutorialInfo.CheckRule(inRule);
    tutorialToggleWidget.Tick(inTickValue);
  }

  public void SetTopic(Toggle inToggle, DialogRule inRule)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    TutorialPopupDialog.\u003CSetTopic\u003Ec__AnonStorey64 topicCAnonStorey64 = new TutorialPopupDialog.\u003CSetTopic\u003Ec__AnonStorey64();
    // ISSUE: reference to a compiler-generated field
    topicCAnonStorey64.inToggle = inToggle;
    // ISSUE: reference to a compiler-generated field
    topicCAnonStorey64.inRule = inRule;
    // ISSUE: reference to a compiler-generated field
    topicCAnonStorey64.\u003C\u003Ef__this = this;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (!((Object) topicCAnonStorey64.inToggle != (Object) null) || topicCAnonStorey64.inRule == null || topicCAnonStorey64.inRule.triggerQuery == null)
      return;
    // ISSUE: reference to a compiler-generated field
    topicCAnonStorey64.inToggle.onValueChanged.RemoveAllListeners();
    // ISSUE: reference to a compiler-generated field
    if (topicCAnonStorey64.inRule == this.mSelectedTopic)
    {
      // ISSUE: reference to a compiler-generated field
      topicCAnonStorey64.inToggle.isOn = true;
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated method
    topicCAnonStorey64.inToggle.onValueChanged.AddListener(new UnityAction<bool>(topicCAnonStorey64.\u003C\u003Em__99));
  }

  public void SetButton(Button inButton, DialogRule inRule)
  {
    if (!((Object) inButton != (Object) null) || inRule == null)
      return;
    inButton.onClick.RemoveAllListeners();
    string control = this.GetControl(inRule);
    if (inRule.triggerQuery != null && (inRule.triggerQuery == null || inRule.triggerQuery.criteriaList.Count != 0))
      return;
    if (this.mRule.triggerQuery == null || this.mRule.triggerQuery != null && this.mRule.triggerQuery.criteriaList.Count == 0)
    {
      if (control == "End")
      {
        inButton.gameObject.SetActive(true);
        inButton.onClick.AddListener((UnityAction) (() => this.OnEndTutorial(true, true)));
      }
      else
        inButton.gameObject.SetActive(false);
    }
    else if (control == "Continue")
    {
      inButton.gameObject.SetActive(true);
      inButton.onClick.AddListener((UnityAction) (() => this.OnNextTutorial((Toggle) null, this.mRule.triggerQuery.criteriaList.ToArray(), true)));
    }
    else
      inButton.gameObject.SetActive(false);
  }

  public void OnNextTutorial(Toggle inToggle, DialogCriteria[] inCriteria, bool inSaveViewed)
  {
    if (!((Object) inToggle == (Object) null) && (!((Object) inToggle != (Object) null) || !inToggle.isOn))
      return;
    if (inSaveViewed && Game.instance != null && (Game.instance.tutorialInfo != null && this.mRule != null))
    {
      if (!this.mSimulation)
        Game.instance.tutorialInfo.AddRule(this.mRule);
      else
        Game.instance.tutorialInfo.AddSimulationRule(this.mRule);
      string criteriaValue1 = this.GetCriteriaValue("Source", inCriteria);
      string criteriaValue2 = this.mRule.GetCriteriaValue("Source");
      if (this.mRule.GetCriteriaValue("Type") != "Header" && !string.IsNullOrEmpty(criteriaValue2) && (!string.IsNullOrEmpty(criteriaValue1) && criteriaValue2 != criteriaValue1))
        this.TriggerViewRulesInList();
    }
    if (inCriteria != null && inCriteria.Length > 0)
    {
      if (!this.mSimulation)
        Game.instance.helpSystem.SendTutorial(true, inCriteria);
      else
        App.instance.tutorialSimulation.SendTutorial(inCriteria);
    }
    else
      this.Hide();
  }

  public void OnEndTutorial(bool inSaveViewed, bool inTriggerRules)
  {
    if (inSaveViewed && Game.instance != null && (Game.instance.tutorialInfo != null && this.mRule != null))
    {
      if (!this.mSimulation)
      {
        if (inTriggerRules)
          Game.instance.tutorialInfo.AddRule(this.mRule);
        Game.instance.tutorialInfo.AddTutorial(this.mRule.gameArea);
      }
      else
      {
        if (inTriggerRules)
          Game.instance.tutorialInfo.AddSimulationRule(this.mRule);
        Game.instance.tutorialInfo.AddSimulationTutorial(this.mRule, TutorialInfo.RuleStatus.Viewed);
      }
      if (inTriggerRules)
        this.TriggerViewRulesInList();
    }
    this.Hide();
  }

  private string GetCriteriaValue(string inCriteria, DialogCriteria[] inCriterias)
  {
    for (int index = 0; index < inCriterias.Length; ++index)
    {
      if (inCriterias[index].mType == inCriteria)
        return inCriterias[index].mCriteriaInfo;
    }
    return string.Empty;
  }

  private string GetControl(DialogRule inRule)
  {
    for (int index = 0; index < inRule.userData.Count; ++index)
    {
      if (inRule.userData[index].mType == "Control")
        return inRule.userData[index].mCriteriaInfo;
    }
    return string.Empty;
  }

  private DialogRule GetTopicRule(DialogRule inRule, List<DialogRule> inTopicRules)
  {
    string str = string.Empty;
    for (int index = 0; index < inRule.userData.Count; ++index)
    {
      if (inRule.userData[index].mType == "Header")
        str = inRule.userData[index].mCriteriaInfo;
    }
    if (string.IsNullOrEmpty(str))
    {
      str = inRule.GetSource();
      if (str == "Error")
        return inRule;
    }
    if (string.IsNullOrEmpty(str))
      return inRule;
    for (int index = 0; index < inTopicRules.Count; ++index)
    {
      if (inTopicRules[index].GetSource() == str)
        return inTopicRules[index];
    }
    return (DialogRule) null;
  }

  private bool HasUserClickedAwayFromObject()
  {
    if (InputManager.instance.GetKeyDown(KeyBinding.Name.MouseLeft))
      return !this.rectTransform.rect.Contains((Vector2) this.rectTransform.InverseTransformPoint(Input.mousePosition));
    return false;
  }
}
