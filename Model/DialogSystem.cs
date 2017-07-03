// Decompiled with JetBrains decompiler
// Type: DialogSystem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

public class DialogSystem
{
  private HashSet<DialogRule> mRulesInspectedForButtons = new HashSet<DialogRule>();
  private bool mResetStrings = true;
  public Action OnGameStart;
  public Action OnMonthEnd;
  public Action OnEventStartInThreeDays;
  public Action OnEventStart;
  public Action OnChampionshipEnd;
  public Action OnChampionshipStart;
  private bool tutRaceMessage;

  public void OnStart()
  {
    if (!GameUtility.MethodPresentInAction(Game.OnNewGame, "OnGameStartEvent"))
      Game.OnNewGame += new Action(this.OnGameStartEvent);
    Game.instance.time.OnDayEnd -= new Action(this.OnDayEndMessages);
    Game.instance.time.OnDayEnd += new Action(this.OnDayEndMessages);
    if (this.OnGameStart != null)
      return;
    this.OnEventStart += new Action(this.OnEventStartMessages);
    this.OnGameStart += new Action(this.OnStartGameMessages);
  }

  public void OnLoad()
  {
    Game.instance.time.OnDayEnd -= new Action(this.OnDayEndMessages);
    Game.instance.time.OnDayEnd += new Action(this.OnDayEndMessages);
  }

  public void OnDestroy()
  {
    Game.OnNewGame -= new Action(this.OnGameStartEvent);
  }

  public void ResetStrings(bool inBool)
  {
    this.mResetStrings = inBool;
  }

  private void OnGameStartEvent()
  {
    if (this.OnGameStart == null)
      return;
    this.OnGameStart();
  }

  public Message SendMail(params DialogCriteria[] inDialogCriteria)
  {
    return this.SendMail((Person) null, inDialogCriteria);
  }

  public Message SendMail(Person inSender, params DialogCriteria[] inDialogCriteria)
  {
    List<DialogCriteria> dialogCriteriaList = new List<DialogCriteria>((IEnumerable<DialogCriteria>) inDialogCriteria);
    Person inSender1 = inSender;
    DialogQuery inQuery = new DialogQuery();
    for (int index = 0; index < dialogCriteriaList.Count; ++index)
    {
      if (dialogCriteriaList[index].mType == "Who")
        inQuery.who = dialogCriteriaList[index];
      else
        inQuery.AddCriteria(dialogCriteriaList[index].mType, dialogCriteriaList[index].mCriteriaInfo);
    }
    return this.SendMail(inSender1, inQuery, false);
  }

  public Message SendMail(Person inSender, DialogQuery inQuery, bool inAddToIgnoreStack = false)
  {
    Person person = inSender;
    DialogQuery inQuery1 = inQuery;
    if (person == null && inQuery1.who != null)
      person = this.FindPerson(inQuery1.who.mCriteriaInfo);
    StringVariableParser.SetStaticData(person);
    DialogRule rule;
    if (person != null)
    {
      rule = person.dialogQuery.ProcessQueryWithOwnCriteria(inQuery1, inAddToIgnoreStack);
    }
    else
    {
      DialogQueryCreator.AddWorldCriteria(inQuery1);
      rule = App.instance.dialogRulesManager.ProcessQuery(inQuery1, inAddToIgnoreStack);
    }
    if (rule != null)
      return this.PostMessage(rule, person);
    string str = "Message parameters not met, Criteria: ";
    for (int index = 0; index < inQuery1.criteriaList.Count; ++index)
      str = str + "Who: " + inQuery1.who.mCriteriaInfo + "  " + inQuery1.criteriaList[index].mType + " = " + inQuery1.criteriaList[index].mCriteriaInfo + ";";
    Debug.Log((object) str, (UnityEngine.Object) null);
    return (Message) null;
  }

  public Message PostMessage(DialogRule rule, Person sender)
  {
    StringVariableParser.useLinkData = true;
    if (sender == null && rule.who != null)
    {
      sender = this.FindPerson(rule.who.mCriteriaInfo);
      StringVariableParser.sender = sender;
    }
    List<string> inResult = new List<string>();
    DialogRule inRuleForButtons = (DialogRule) null;
    this.mRulesInspectedForButtons.Clear();
    this.GetMailButtons(rule, ref inRuleForButtons, sender);
    this.GetMailBody(ref inResult, rule, sender);
    MessageManager messageManager = Game.instance.messageManager;
    Message message = MessageManager.CreateMessage();
    message.buttonsRule = inRuleForButtons;
    message.SetMessageTextFields(inResult, rule.localisationID);
    message.showNotification = true;
    message.portraitId = 0;
    message.SetSender(sender);
    message.deliverStateGroup = GameState.Group.Frontend;
    this.SetUserDataOptions(rule.userData, message);
    messageManager.PostMessage(message);
    StringVariableParser.useLinkData = false;
    if (this.mResetStrings)
      StringVariableParser.ResetAllStaticReferences();
    this.mResetStrings = true;
    return message;
  }

  private void GetMailBody(ref List<string> inResult, DialogRule inRule, Person inPerson = null)
  {
    if (inResult.Count > 0 && inResult[inResult.Count - 1].Length > 10000)
    {
      Debug.Log((object) ("Message too long, might be a loop: " + (object) inResult), (UnityEngine.Object) null);
    }
    else
    {
      if (inRule.triggerQuery == null)
        return;
      Person inPerson1 = inPerson ?? this.FindPerson(this.GetPersonThatNeedsToRespond(inRule));
      if (inPerson1 != null)
      {
        DialogRule inRule1 = inPerson1.dialogQuery.ProcessQueryWithOwnCriteria(inRule.triggerQuery, false);
        if (inRule1 == null)
          return;
        inResult.Add(inRule1.localisationID);
        DialogSystem.GetMessageTextRelatedUserData(ref inResult, inRule1, (DialogQuery) null);
        if (this.HasStartOfInterviewBlock(inRule1))
          return;
        this.GetMailBody(ref inResult, inRule1, inPerson1);
      }
      else
      {
        DialogRule inRule1 = App.instance.dialogRulesManager.ProcessQuery(inRule.triggerQuery, false);
        if (inRule1 == null)
          return;
        Person person = this.FindPerson(this.GetPersonThatNeedsToRespond(inRule1));
        if (person == null)
        {
          Debug.LogErrorFormat("Could not find person with string ({0}), could just be a reference that is not set", (object) this.GetPersonThatNeedsToRespond(inRule1));
        }
        else
        {
          DialogRule inRule2 = person.dialogQuery.ProcessQueryWithOwnCriteria(inRule.triggerQuery, false);
          if (inRule2 != null && person != null)
          {
            inResult.Add(inRule2.localisationID);
            DialogSystem.GetMessageTextRelatedUserData(ref inResult, inRule2, (DialogQuery) null);
            if (this.HasStartOfInterviewBlock(inRule2))
              return;
            this.GetMailBody(ref inResult, inRule2, person);
          }
          else if (inRule2 != null)
            Debug.LogError((object) ("Not able to find a sender for rule ID: " + inRule2.localisationID), (UnityEngine.Object) null);
          else
            Debug.LogError((object) ("Did not find rule for Query: " + inRule.localisationID + " Criteria :" + this.GetQueryCriteriaString(inRule.triggerQuery)), (UnityEngine.Object) null);
        }
      }
    }
  }

  private void GetMailButtons(DialogRule inRule, ref DialogRule inRuleForButtons, Person inPerson = null)
  {
    if (this.mRulesInspectedForButtons.Contains(inRule))
    {
      Debug.LogWarning((object) (inRule.localisationID + " rule, seems to be in a circular loop in GetMailButtons(), check the rule trigger."), (UnityEngine.Object) null);
    }
    else
    {
      this.mRulesInspectedForButtons.Add(inRule);
      if (inRule.triggerQuery == null)
        return;
      Person inPerson1 = inPerson ?? this.FindPerson(this.GetPersonThatNeedsToRespond(inRule));
      if (inPerson1 != null)
      {
        DialogRule inRule1 = inPerson1.dialogQuery.ProcessQueryWithOwnCriteria(inRule.triggerQuery, true);
        if (inRule1 == null)
          return;
        if (inRule1.HasMailButtons())
          inRuleForButtons = inRule1;
        else
          this.GetMailButtons(inRule1, ref inRuleForButtons, inPerson1);
      }
      else
      {
        DialogRule inRule1 = App.instance.dialogRulesManager.ProcessQuery(inRule.triggerQuery, true);
        if (inRule1 == null)
          return;
        Person person = this.FindPerson(this.GetPersonThatNeedsToRespond(inRule1));
        if (person == null)
        {
          Debug.LogErrorFormat("Could not find person with string ({0}), could just be a reference that is not set", (object) this.GetPersonThatNeedsToRespond(inRule1));
        }
        else
        {
          DialogRule inRule2 = person.dialogQuery.ProcessQueryWithOwnCriteria(inRule.triggerQuery, true);
          if (inRule2 == null || person == null)
            return;
          if (inRule2.HasMailButtons())
            inRuleForButtons = inRule2;
          else
            this.GetMailButtons(inRule2, ref inRuleForButtons, person);
        }
      }
    }
  }

  private string GetPersonThatNeedsToRespond(DialogRule inRule)
  {
    string empty = string.Empty;
    if (inRule.triggerQuery != null)
      return !(inRule.triggerQuery.GetWho() != "Error") ? inRule.who.mCriteriaInfo : inRule.triggerQuery.GetWho();
    Debug.LogError((object) string.Format("Rule with ID:{0} has a null reference to its trigger query.", (object) inRule.localisationID), (UnityEngine.Object) null);
    return string.Empty;
  }

  private string GetQueryCriteriaString(DialogQuery inQuery)
  {
    string str = string.Empty;
    for (int index = 0; index < inQuery.criteriaList.Count; ++index)
      str = str + inQuery.criteriaList[index].mType + " = " + inQuery.criteriaList[index].mCriteriaInfo + " ; ";
    return str;
  }

  public static void GetMessageTextRelatedUserData(ref List<string> result, DialogRule inRule, DialogQuery inQuery = null)
  {
    for (int index1 = 0; index1 < inRule.userData.Count; ++index1)
    {
      DialogCriteria dialogCriteria = inRule.userData[index1];
      if (dialogCriteria.mType == "AddLineBreaks")
      {
        for (int index2 = 0; index2 < int.Parse(inRule.userData[index1].mCriteriaInfo); ++index2)
          result.Add("\n");
      }
      if (dialogCriteria.mType == "AddSpaces")
      {
        for (int index2 = 0; index2 < int.Parse(inRule.userData[index1].mCriteriaInfo); ++index2)
          result.Add(" ");
      }
      if (dialogCriteria.mType == "SetSubject")
      {
        result.Add(string.Format("{0}={1}", (object) dialogCriteria.mType, (object) dialogCriteria.mCriteriaInfo));
        StringVariableParser.subject = (Person) StringVariableParser.GetObject(dialogCriteria.mCriteriaInfo);
      }
      if (dialogCriteria.mType == "InsertText")
      {
        string mCriteriaInfo = dialogCriteria.mCriteriaInfo;
        if (mCriteriaInfo.Contains("PSG_"))
        {
          DialogRule ruleById = App.instance.dialogRulesManager.GetRuleByID(mCriteriaInfo);
          if (inQuery == null)
          {
            DialogQuery inQuery1 = new DialogQuery();
            inQuery1.AddCriteria("Source", ruleById.GetSource());
            inQuery1.AddCriteria("InsertText", "True");
            inQuery1.AddCriteria(ruleById.who);
            DialogQueryCreator.AddWorldCriteria(inQuery1);
            if (!App.instance.dialogRulesManager.GetValidRules(inQuery1).Contains(ruleById))
              continue;
          }
          if (inQuery == null || App.instance.dialogRulesManager.GetValidRules(inQuery).Contains(ruleById))
          {
            List<string> inResult = new List<string>();
            Person person = Game.instance.dialogSystem.FindPerson(ruleById.who.mCriteriaInfo);
            Game.instance.dialogSystem.GetMailBody(ref inResult, ruleById, person);
            result.Add(ruleById.localisationID);
            DialogSystem.GetMessageTextRelatedUserData(ref result, ruleById, (DialogQuery) null);
            for (int index2 = 0; index2 < inResult.Count; ++index2)
              result.Add(inResult[index2]);
          }
        }
      }
    }
  }

  public void SetUserDataOptions(List<DialogCriteria> inUserData, Message inMessage)
  {
    DateTime dateTime = Game.instance.time.now;
    for (int index = 0; index < inUserData.Count; ++index)
    {
      int num;
      if (inUserData[index].mType == "MailType")
      {
        string mCriteriaInfo = inUserData[index].mCriteriaInfo;
        if (mCriteriaInfo != null)
        {
          // ISSUE: reference to a compiler-generated field
          if (DialogSystem.\u003C\u003Ef__switch\u0024map3 == null)
          {
            // ISSUE: reference to a compiler-generated field
            DialogSystem.\u003C\u003Ef__switch\u0024map3 = new Dictionary<string, int>(5)
            {
              {
                "RequiresResponse",
                0
              },
              {
                "Dilemma",
                1
              },
              {
                "PartDilemma",
                1
              },
              {
                "GTSeries",
                2
              },
              {
                "PauseGame",
                3
              }
            };
          }
          // ISSUE: reference to a compiler-generated field
          if (DialogSystem.\u003C\u003Ef__switch\u0024map3.TryGetValue(mCriteriaInfo, out num))
          {
            switch (num)
            {
              case 0:
                inMessage.interruptGameTime = true;
                inMessage.mustRespond = true;
                inMessage.SetPriorityType(Message.Priority.Urgent);
                break;
              case 1:
                inMessage.interruptGameTime = true;
                inMessage.mustRespond = true;
                inMessage.SetPriorityType(Message.Priority.Urgent);
                inMessage.SetBodyType(Message.BodyType.Dilemma);
                inMessage.SetHeaderType(Message.HeaderType.Dilemma);
                break;
              case 2:
                inMessage.SetBodyType(Message.BodyType.GTSeries);
                inMessage.SetHeaderType(Message.HeaderType.Standard);
                break;
              case 3:
                inMessage.interruptGameTime = true;
                break;
            }
          }
        }
      }
      if (inUserData[index].mType == "MailGroup")
        inMessage.SetGroupType((Message.Group) Enum.Parse(typeof (Message.Group), inUserData[index].mCriteriaInfo));
      if (inUserData[index].mType == "AddHours")
        dateTime = dateTime.AddHours((double) int.Parse(inUserData[index].mCriteriaInfo));
      else if (inUserData[index].mType == "AddDays")
        dateTime = dateTime.AddDays((double) int.Parse(inUserData[index].mCriteriaInfo));
      else if (inUserData[index].mType == "AddRandomTime")
      {
        string mCriteriaInfo = inUserData[index].mCriteriaInfo;
        if (mCriteriaInfo != null)
        {
          // ISSUE: reference to a compiler-generated field
          if (DialogSystem.\u003C\u003Ef__switch\u0024map4 == null)
          {
            // ISSUE: reference to a compiler-generated field
            DialogSystem.\u003C\u003Ef__switch\u0024map4 = new Dictionary<string, int>(3)
            {
              {
                "Days",
                0
              },
              {
                "Hours",
                1
              },
              {
                "Minutes",
                2
              }
            };
          }
          // ISSUE: reference to a compiler-generated field
          if (DialogSystem.\u003C\u003Ef__switch\u0024map4.TryGetValue(mCriteriaInfo, out num))
          {
            switch (num)
            {
              case 0:
                dateTime = dateTime.AddDays((double) UnityEngine.Random.Range(0, 2));
                continue;
              case 1:
                dateTime = dateTime.AddHours((double) UnityEngine.Random.Range(0, 24));
                continue;
              case 2:
                dateTime = dateTime.AddMinutes((double) UnityEngine.Random.Range(0, 120));
                continue;
              default:
                continue;
            }
          }
        }
      }
    }
    inMessage.deliverDate = dateTime;
  }

  public bool HasStartOfInterviewBlock(DialogRule inRule)
  {
    bool flag = false;
    for (int index = 0; index < inRule.userData.Count; ++index)
    {
      if (inRule.userData[index].mType == "InterviewStart")
        flag = true;
    }
    return flag;
  }

  public Person FindPerson(string inPerson)
  {
    Person person = StringVariableParser.GetObject(inPerson) as Person;
    if (person != null)
      return person;
    Debug.LogError((object) ("Could not get reference for Who: " + inPerson), (UnityEngine.Object) null);
    return (Person) null;
  }

  public void OnTestGenderMessage()
  {
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "Engineer"),
      new DialogCriteria("Source", "GenderTestMessage"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnPartDilemma(CarPart.PartType inType)
  {
    StringVariableParser.partFrontendUI = inType;
    Message message = Game.instance.dialogSystem.SendMail(new DialogCriteria[3]{ new DialogCriteria("Who", "Engineer"), new DialogCriteria("Source", "PartAdaptationDilemma"), new DialogCriteria("Type", "Header") });
    message.SetBodyType(Message.BodyType.Dilemma);
    message.SetHeaderType(Message.HeaderType.Dilemma);
  }

  public void OnContractSignedMessages(Person inSender)
  {
    if (inSender.IsReplacementPerson())
      return;
    StringVariableParser.subject = inSender;
    Person personReplaced = StringVariableParser.personReplaced;
    if (inSender is Driver)
    {
      this.SendMail(inSender, new DialogCriteria("Who", "AnyDriver"), new DialogCriteria("Source", "ContractSigned"), new DialogCriteria("Type", "Header"));
      StringVariableParser.subject = inSender;
      StringVariableParser.personReplaced = personReplaced;
      this.SendMail(new DialogCriteria[3]
      {
        new DialogCriteria("Who", "MediaPerson"),
        new DialogCriteria("Source", "DriverContractSigned"),
        new DialogCriteria("Type", "Header")
      });
    }
    else if (inSender is Mechanic)
    {
      this.SendMail(inSender, new DialogCriteria("Who", "Mechanic"), new DialogCriteria("Source", "MechanicContractSigned"), new DialogCriteria("Type", "Header"));
      StringVariableParser.subject = inSender;
      StringVariableParser.personReplaced = personReplaced;
      this.SendMail(new DialogCriteria[3]
      {
        new DialogCriteria("Who", "MediaPerson"),
        new DialogCriteria("Source", "MechanicSignedReport"),
        new DialogCriteria("Type", "Header")
      });
    }
    else
    {
      if (!(inSender is Engineer))
        return;
      this.SendMail(inSender, new DialogCriteria("Who", "Engineer"), new DialogCriteria("Source", "EngineerContractSigned"), new DialogCriteria("Type", "Header"));
      StringVariableParser.subject = inSender;
      StringVariableParser.personReplaced = personReplaced;
      this.SendMail(new DialogCriteria[3]
      {
        new DialogCriteria("Who", "MediaPerson"),
        new DialogCriteria("Source", "EngineerSignedReport"),
        new DialogCriteria("Type", "Header")
      });
    }
  }

  public void OnContractAgreedMessages(Person inSender)
  {
    StringVariableParser.subject = inSender;
    if (inSender is Driver)
      this.SendMail(inSender, new DialogCriteria[2]
      {
        new DialogCriteria("Source", "ContractAgreed"),
        new DialogCriteria("Type", "Header")
      }).SetGroupType(Message.Group.Contracts);
    else if (inSender is Mechanic)
    {
      this.SendMail(inSender, new DialogCriteria[2]
      {
        new DialogCriteria("Source", "StaffContractAgreed"),
        new DialogCriteria("Type", "Header")
      }).SetGroupType(Message.Group.Contracts);
    }
    else
    {
      if (!(inSender is Engineer))
        return;
      this.SendMail(inSender, new DialogCriteria[2]
      {
        new DialogCriteria("Source", "StaffContractAgreed"),
        new DialogCriteria("Type", "Header")
      }).SetGroupType(Message.Group.Contracts);
    }
  }

  public void OnContractRenewedMessages(Person inSender)
  {
    StringVariableParser.subject = inSender;
    if (inSender is Driver)
      this.SendMail(inSender, new DialogCriteria[2]
      {
        new DialogCriteria("Source", "ContractRenewed"),
        new DialogCriteria("Type", "Header")
      }).SetGroupType(Message.Group.Contracts);
    else if (inSender is Mechanic)
    {
      this.SendMail(inSender, new DialogCriteria[2]
      {
        new DialogCriteria("Source", "ContractRenewed"),
        new DialogCriteria("Type", "Header")
      }).SetGroupType(Message.Group.Contracts);
    }
    else
    {
      if (!(inSender is Engineer))
        return;
      this.SendMail(inSender, new DialogCriteria[2]
      {
        new DialogCriteria("Source", "ContractRenewed"),
        new DialogCriteria("Type", "Header")
      }).SetGroupType(Message.Group.Contracts);
    }
  }

  public void OnContractPromotionMessages(Person inSender)
  {
    StringVariableParser.subject = inSender;
    if (!(inSender is Driver) || inSender.IsReplacementPerson())
      return;
    this.SendMail(inSender, new DialogCriteria("Who", "AnyDriver"), new DialogCriteria("Source", "DriverPromoted"), new DialogCriteria("Type", "Header"));
  }

  public void OnContractDemotionMessages(Person inSender)
  {
    StringVariableParser.subject = inSender;
    if (!(inSender is Driver) || inSender.IsReplacementPerson())
      return;
    this.SendMail(inSender, new DialogCriteria("Who", "AnyDriver"), new DialogCriteria("Source", "DriverDemoted"), new DialogCriteria("Type", "Header"));
  }

  public void OnContractEndedMessages(Person inSender)
  {
    StringVariableParser.subject = inSender;
    if (inSender is Driver)
      this.SendMail(inSender, new DialogCriteria[2]
      {
        new DialogCriteria("Source", "ContractEnded"),
        new DialogCriteria("Type", "Header")
      }).SetGroupType(Message.Group.Contracts);
    else if (inSender is Mechanic)
    {
      this.SendMail(inSender, new DialogCriteria[2]
      {
        new DialogCriteria("Source", "ContractEnded"),
        new DialogCriteria("Type", "Header")
      }).SetGroupType(Message.Group.Contracts);
    }
    else
    {
      if (!(inSender is Engineer))
        return;
      this.SendMail(inSender, new DialogCriteria[2]
      {
        new DialogCriteria("Source", "ContractEnded"),
        new DialogCriteria("Type", "Header")
      }).SetGroupType(Message.Group.Contracts);
    }
  }

  public void OnDriverPoached(Person inSender)
  {
    StringVariableParser.subject = inSender;
    if (!(inSender is Driver))
      return;
    this.SendMail(inSender, new DialogCriteria("Who", "AnyDriver"), new DialogCriteria("Source", "DriverPoached"), new DialogCriteria("Type", "Header"));
  }

  public void OnDriverRetiringMessage(Person inSender)
  {
    if (!(inSender is Driver))
      return;
    StringVariableParser.rumourDriver = inSender as Driver;
    StringVariableParser.rumourTeam = inSender.contract.GetTeam();
    this.SendMail(inSender, new DialogCriteria("Who", "AnyDriver"), new DialogCriteria("Source", "DriverRetirementMessage"), new DialogCriteria("Type", "Header"));
  }

  public void OnContractTerminatedMessages(Person inSender)
  {
    StringVariableParser.subject = inSender;
    if (inSender is Driver)
      this.SendMail(inSender, new DialogCriteria[2]
      {
        new DialogCriteria("Source", "ContractTerminated"),
        new DialogCriteria("Type", "Header")
      }).SetGroupType(Message.Group.Contracts);
    else if (inSender is Mechanic)
    {
      this.SendMail(inSender, new DialogCriteria[2]
      {
        new DialogCriteria("Source", "ContractTerminated"),
        new DialogCriteria("Type", "Header")
      }).SetGroupType(Message.Group.Contracts);
    }
    else
    {
      if (!(inSender is Engineer))
        return;
      this.SendMail(inSender, new DialogCriteria[2]
      {
        new DialogCriteria("Source", "ContractTerminated"),
        new DialogCriteria("Type", "Header")
      }).SetGroupType(Message.Group.Contracts);
    }
  }

  public void OnContractsEndingMessages()
  {
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "Assistant"),
      new DialogCriteria("Source", "ContractsEnding"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnContractNegotiationExpiring(Person inSender)
  {
    if (inSender is Driver)
      this.SendMail(inSender, new DialogCriteria("Who", "AnyDriver"), new DialogCriteria("Source", "DealTimerRunningDownDriver"), new DialogCriteria("Type", "Header")).SetGroupType(Message.Group.Contracts);
    else if (inSender is Mechanic)
    {
      this.SendMail(inSender, new DialogCriteria[2]
      {
        new DialogCriteria("Source", "DealTimerRunningDown"),
        new DialogCriteria("Type", "Header")
      }).SetGroupType(Message.Group.Contracts);
    }
    else
    {
      if (!(inSender is Engineer))
        return;
      this.SendMail(inSender, new DialogCriteria[2]
      {
        new DialogCriteria("Source", "DealTimerRunningDown"),
        new DialogCriteria("Type", "Header")
      }).SetGroupType(Message.Group.Contracts);
    }
  }

  public void OnContractNegotiationExpired(Person inSender)
  {
    if (inSender is Driver)
      this.SendMail(inSender, new DialogCriteria[2]
      {
        new DialogCriteria("Source", "DealTimerRunningDown"),
        new DialogCriteria("Type", "Header")
      }).SetGroupType(Message.Group.Contracts);
    else if (inSender is Mechanic)
    {
      this.SendMail(inSender, new DialogCriteria[2]
      {
        new DialogCriteria("Source", "DealTimerFinished"),
        new DialogCriteria("Type", "Header")
      }).SetGroupType(Message.Group.Contracts);
    }
    else
    {
      if (!(inSender is Engineer))
        return;
      this.SendMail(inSender, new DialogCriteria[2]
      {
        new DialogCriteria("Source", "DealTimerFinished"),
        new DialogCriteria("Type", "Header")
      }).SetGroupType(Message.Group.Contracts);
    }
  }

  public void OnContractElapsedOrFiredWhileRenewing(Person inSender)
  {
    StringVariableParser.subject = inSender;
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "Assistant"),
      new DialogCriteria("Source", "ContractElapsedOrFiredWhileRenewing"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnNewPersonalityTrait(Driver inDriverWhoGotTheTrait, PersonalityTrait inNewTrait)
  {
    if (inNewTrait.data.triggerCriteria.Count <= 0)
      return;
    StringVariableParser.subject = (Person) inDriverWhoGotTheTrait;
    StringVariableParser.anyDriver = inDriverWhoGotTheTrait;
    StringVariableParser.rumourTeam = inDriverWhoGotTheTrait.contract.GetTeam();
    StringVariableParser.SetStaticData((Person) inDriverWhoGotTheTrait);
    Message message = (Message) null;
    try
    {
      message = this.SendMail(inNewTrait.data.triggerCriteria.ToArray());
    }
    catch (Exception ex)
    {
      Debug.LogError((object) (inNewTrait.name + " - Mail Error - " + ex.ToString()), (UnityEngine.Object) null);
    }
    message.specialObject[0] = (object) inNewTrait;
    if (message.sender.contract.GetMediaOutlet() != null)
      message.SetHeaderType(Message.HeaderType.Media);
    else
      message.SetHeaderType(Message.HeaderType.PersonalityTrait);
    message.SetBodyType(Message.BodyType.PersonalityTrait);
  }

  public void OnAIContractSignedMessages(Person inSender)
  {
    if (inSender.IsReplacementPerson() || !Game.instance.player.IsUnemployed() && inSender.contract.GetTeam().championship != Game.instance.player.team.championship)
      return;
    StringVariableParser.subject = inSender;
    if (inSender is Driver)
      this.SendMail(new DialogCriteria[3]
      {
        new DialogCriteria("Who", "MediaPerson"),
        new DialogCriteria("Source", "DriverContractSigned"),
        new DialogCriteria("Type", "Header")
      });
    else if (inSender is Mechanic)
    {
      this.SendMail(new DialogCriteria[3]
      {
        new DialogCriteria("Who", "MediaPerson"),
        new DialogCriteria("Source", "MechanicSignedReport"),
        new DialogCriteria("Type", "Header")
      });
    }
    else
    {
      if (!(inSender is Engineer))
        return;
      this.SendMail(new DialogCriteria[3]
      {
        new DialogCriteria("Who", "MediaPerson"),
        new DialogCriteria("Source", "EngineerSignedReport"),
        new DialogCriteria("Type", "Header")
      });
    }
  }

  public void OnRetiredMessages(Person inSender)
  {
    StringVariableParser.rumourTeam = inSender.contract.GetTeam();
    if (inSender is Driver)
    {
      StringVariableParser.rumourDriver = inSender as Driver;
      this.SendMail(new DialogCriteria[3]
      {
        new DialogCriteria("Who", "MediaPerson"),
        new DialogCriteria("Source", "Retiring"),
        new DialogCriteria("Type", "Header")
      });
    }
    if (!(inSender is Chairman))
      return;
    StringVariableParser.subject = (Person) (inSender as Chairman);
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "MediaPerson"),
      new DialogCriteria("Source", "ChairmanRetired"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnRetiringMessages(Person inSender)
  {
    StringVariableParser.rumourTeam = inSender.contract.GetTeam();
    if (!(inSender is Driver))
      return;
    StringVariableParser.rumourDriver = inSender as Driver;
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "MediaPerson"),
      new DialogCriteria("Source", "RumoursOfRetirement"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnWantsToLeaveMessages(Person inSender)
  {
    StringVariableParser.rumourTeam = inSender.contract.GetTeam();
    if (!(inSender is Driver) || !Game.instance.player.IsInSameChampionship(inSender))
      return;
    StringVariableParser.rumourDriver = inSender as Driver;
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "MediaPerson"),
      new DialogCriteria("Source", "WantsToLeave"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnUnhappyWithTeammateMessages(Person inSender)
  {
    StringVariableParser.rumourTeam = inSender.contract.GetTeam();
    if (!(inSender is Mechanic) || !Game.instance.player.IsInSameChampionship(inSender))
      return;
    StringVariableParser.rumourDriver = inSender.contract.GetTeam().GetDriver((inSender as Mechanic).driver);
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "MediaPerson"),
      new DialogCriteria("Source", "MechanicsDislikeDriver"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnOpenToOffersMessages(Person inSender)
  {
    if (!(inSender is Driver) || ((Driver) inSender).IsReplacementPerson())
      return;
    StringVariableParser.rumourDriver = inSender as Driver;
    this.SendMail(inSender, new DialogCriteria[2]
    {
      new DialogCriteria("Source", "DriverOpenToOffers"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnStopListeningToOffersMessages(Person inSender)
  {
    if (!(inSender is Driver) || ((Driver) inSender).IsReplacementPerson())
      return;
    StringVariableParser.rumourDriver = inSender as Driver;
    this.SendMail(inSender, new DialogCriteria[2]
    {
      new DialogCriteria("Source", "DriverStopsListeningToOffers"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnSponsorOfferMade(Sponsor inSponsor)
  {
    this.SendMail(inSponsor.liaison, new DialogCriteria("Who", "SponsorLiaison"), new DialogCriteria("Source", "SponsorOfferMade"), new DialogCriteria("Type", "Header"));
  }

  public void OnSponsorDealSigned(Sponsor inSponsor)
  {
    this.SendMail(inSponsor.liaison, new DialogCriteria("Who", "SponsorLiaison"), new DialogCriteria("Source", "SponsorDealSigned"), new DialogCriteria("Type", "Header"));
  }

  public void OnSponsorDealEnded(Sponsor inSponsor)
  {
    this.SendMail(inSponsor.liaison, new DialogCriteria("Who", "SponsorLiaison"), new DialogCriteria("Source", "SponsorDealEnded"), new DialogCriteria("Type", "Header"));
  }

  public void OnSponsorClauseFailedMessages(Sponsor inSponsor)
  {
    this.SendMail(inSponsor.liaison, new DialogCriteria("Who", "SponsorLiaison"), new DialogCriteria("Source", "SponsorClauseFailed"), new DialogCriteria("Type", "Header"));
  }

  public void OnSponsorClauseCompletedMessages(Sponsor inSponsor)
  {
    this.SendMail(inSponsor.liaison, new DialogCriteria("Who", "SponsorLiaison"), new DialogCriteria("Source", "SponsorClauseCompleted"), new DialogCriteria("Type", "Header"));
  }

  public void OnSponsorHapinessLowMessages(Sponsor inSponsor)
  {
    this.SendMail(inSponsor.liaison, new DialogCriteria("Who", "SponsorLiaison"), new DialogCriteria("Source", "SponsorHappinessLow"), new DialogCriteria("Type", "Header"));
  }

  public void OnPoliticsNewYearRulesApplied(Person inSender)
  {
    this.SendMail(inSender, new DialogCriteria("Who", "IMAPresident"), new DialogCriteria("Source", "NewYearNewRules"), new DialogCriteria("Type", "Header"));
  }

  public void OnPoliticVotesDecided(Person inSender)
  {
    this.SendMail(inSender, new DialogCriteria("Who", "IMAPresident"), new DialogCriteria("Source", "NewVotesDecided"), new DialogCriteria("Type", "Header"));
  }

  public void OnPoliticsVoteStartedMessages(Person inSender)
  {
    this.SendMail(inSender, new DialogCriteria("Who", "Assistant"), new DialogCriteria("Source", "PoliticsVoteStarted"), new DialogCriteria("Type", "Header")).mustRespond = true;
  }

  public void OnPoliticsVoteEndedMessages(Person inSender)
  {
    this.SendMail(inSender, new DialogCriteria("Who", "IMAPresident"), new DialogCriteria("Source", "PoliticsVoteFinished"), new DialogCriteria("Type", "Header"));
  }

  public void OnScoutingCompleted(Person inSender)
  {
    this.SendMail(inSender, new DialogCriteria("Who", "Scout"), new DialogCriteria("Source", "ScoutReportFinished"), new DialogCriteria("Type", "Header"));
  }

  public void OnPartBuilt(CarPart inPart)
  {
    StringVariableParser.partFrontendUI = inPart.GetPartType();
    Message message = (Message) null;
    switch (inPart.GetPartType())
    {
      case CarPart.PartType.Brakes:
      case CarPart.PartType.BrakesGT:
        message = this.SendMail(new DialogCriteria[3]
        {
          new DialogCriteria("Who", "Engineer"),
          new DialogCriteria("Source", "PartBuilt"),
          new DialogCriteria("Type", "Header")
        });
        break;
      case CarPart.PartType.Engine:
      case CarPart.PartType.EngineGT:
        message = this.SendMail(new DialogCriteria[3]
        {
          new DialogCriteria("Who", "Engineer"),
          new DialogCriteria("Source", "PartBuilt"),
          new DialogCriteria("Type", "Header")
        });
        break;
      case CarPart.PartType.FrontWing:
        message = this.SendMail(new DialogCriteria[3]
        {
          new DialogCriteria("Who", "Engineer"),
          new DialogCriteria("Source", "PartBuilt"),
          new DialogCriteria("Type", "Header")
        });
        break;
      case CarPart.PartType.Gearbox:
      case CarPart.PartType.GearboxGT:
        message = this.SendMail(new DialogCriteria[3]
        {
          new DialogCriteria("Who", "Engineer"),
          new DialogCriteria("Source", "PartBuilt"),
          new DialogCriteria("Type", "Header")
        });
        break;
      case CarPart.PartType.RearWing:
      case CarPart.PartType.RearWingGT:
        message = this.SendMail(new DialogCriteria[3]
        {
          new DialogCriteria("Who", "Engineer"),
          new DialogCriteria("Source", "PartBuilt"),
          new DialogCriteria("Type", "Header")
        });
        break;
      case CarPart.PartType.Suspension:
      case CarPart.PartType.SuspensionGT:
        message = this.SendMail(new DialogCriteria[3]
        {
          new DialogCriteria("Who", "Engineer"),
          new DialogCriteria("Source", "PartBuilt"),
          new DialogCriteria("Type", "Header")
        });
        break;
    }
    message.specialObject = new object[2];
    message.specialObject[0] = (object) inPart;
    message.specialObject[1] = (object) Game.instance.player.team.carManager.partInventory.GetMostRecentParts(2, inPart.GetPartType())[1];
    message.SetBodyType(Message.BodyType.Part);
    message.SetHeaderType(Message.HeaderType.Part);
  }

  public void OnAIPartBuilt(CarPart inPart, Team inTeam)
  {
    StringVariableParser.rumourTeam = inTeam;
    StringVariableParser.partFrontendUI = inPart.GetPartType();
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "MediaPerson"),
      new DialogCriteria("Source", "AIPartBuilt"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnPlayerTeamPromotable()
  {
    StringVariableParser.playerTeamPromoted = true;
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "IMAPresident"),
      new DialogCriteria("Source", "PromotionOpportunity"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnPlayerTeamRelegatable(bool inWasRelegated, int inChampionshipBelowID)
  {
    StringVariableParser.playerTeamRelegated = inWasRelegated;
    Championship championshipById = Game.instance.championshipManager.GetChampionshipByID(inChampionshipBelowID);
    StringVariableParser.promotedTeam = championshipById == null ? (Team) null : championshipById.championshipPromotions.champion;
    this.SendMail(new DialogCriteria("Who", "IMAPresident"), new DialogCriteria("Source", "RelegationMessage"), new DialogCriteria("Type", "Header"), new DialogCriteria("Relegateable", "True"));
  }

  public void OnTeamsPromoted()
  {
    if (Game.instance.player.team.championship.allowPromotions)
    {
      if (Game.instance.player.team.championship.series == Championship.Series.SingleSeaterSeries)
        this.SendMail(new DialogCriteria[3]
        {
          new DialogCriteria("Who", "IMAPresident"),
          new DialogCriteria("Source", "PromotionRoundup"),
          new DialogCriteria("Type", "Header")
        });
      if (Game.instance.player.team.championship.series == Championship.Series.GTSeries)
        this.SendMail(new DialogCriteria[3]
        {
          new DialogCriteria("Who", "IMAPresident"),
          new DialogCriteria("Source", "PromotionRoundupGT"),
          new DialogCriteria("Type", "Header")
        });
    }
    this.SendPlayerRuleDependentMails();
  }

  public void SendPlayerRuleDependentMails()
  {
    if (Game.instance.player.IsUnemployed())
      return;
    int inID1 = 79;
    int inID2 = 77;
    int inID3 = 78;
    if (this.ShouldSendMessageAboutRule(inID1) || this.ShouldSendMessageAboutRule(inID2) || this.ShouldSendMessageAboutRule(inID3))
      this.SendMail(new DialogCriteria[3]
      {
        new DialogCriteria("Who", "IMAPresident"),
        new DialogCriteria("Source", "HeadsUpERS"),
        new DialogCriteria("Type", "Header")
      });
    if (this.ShouldSendMessageAboutRule(inID2) || this.ShouldSendMessageAboutRule(inID3))
    {
      Game.instance.player.playerGameData.AddRule(inID2);
      Game.instance.player.playerGameData.AddRule(inID3);
    }
    if (this.ShouldSendMessageAboutRule(70))
      this.SendMail(new DialogCriteria[3]
      {
        new DialogCriteria("Who", "IMAPresident"),
        new DialogCriteria("Source", "HeadsUp3StageQuali"),
        new DialogCriteria("Type", "Header")
      });
    Game.instance.player.playerGameData.UpdateData(Game.instance.player.team.championship);
  }

  private bool ShouldSendMessageAboutRule(int inID)
  {
    if (!Game.instance.player.playerGameData.HasBeenExposedToRule(inID))
      return Game.instance.player.team.championship.rules.HasActiveRule(inID);
    return false;
  }

  public void OnSeasonEndMessages()
  {
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "IMAPresident"),
      new DialogCriteria("Source", "PrizesGivenOut"),
      new DialogCriteria("Type", "Header")
    });
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "IMAPresident"),
      new DialogCriteria("Source", "ManagerOfTheSeason"),
      new DialogCriteria("Type", "Header")
    });
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "IMAPresident"),
      new DialogCriteria("Source", "DriverOfTheSeason"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnPreSeasonStartMessages()
  {
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "Engineer"),
      new DialogCriteria("Source", "CarDesignUnlocked"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnLiveryEditPrompt()
  {
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "Engineer"),
      new DialogCriteria("Source", "ChooseLivery"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnPreSeasonTestingEndingMessages()
  {
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "Engineer"),
      new DialogCriteria("Source", "PreSeasonTestingSoon"),
      new DialogCriteria("Type", "Header")
    });
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "MediaPerson"),
      new DialogCriteria("Source", "PreSeasonTrailer"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnPreSeasonEndMessages()
  {
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "Assistant"),
      new DialogCriteria("Source", "PreSeasonTest"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnPreSeasonTestingEndMessages()
  {
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "MediaPerson"),
      new DialogCriteria("Source", "PreSeasonMediaReport"),
      new DialogCriteria("Type", "Header")
    });
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "Engineer"),
      new DialogCriteria("Source", "PreSeasonTestingReport"),
      new DialogCriteria("Type", "Header")
    });
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "AnyDriver"),
      new DialogCriteria("Source", "DriverPreSeasonReaction"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnMechanicsIdle(Person inSender)
  {
    this.SendMail(inSender, new DialogCriteria("Who", "Mechanic"), new DialogCriteria("Source", "FactoryStaffIdle"), new DialogCriteria("Type", "Header"));
  }

  public void OnReliabilityStackEmpty(Person inSender)
  {
    this.SendMail(inSender, new DialogCriteria("Who", "Mechanic"), new DialogCriteria("Source", "ReliabilityWorkFinished"), new DialogCriteria("Type", "Header"));
  }

  public void OnPerformanceStackEmpty(Person inSender)
  {
    this.SendMail(inSender, new DialogCriteria("Who", "Mechanic"), new DialogCriteria("Source", "PerformanceWorkFinished"), new DialogCriteria("Type", "Header"));
  }

  public void OnConditionStackEmpty(Person inSender)
  {
    this.SendMail(inSender, new DialogCriteria("Who", "Mechanic"), new DialogCriteria("Source", "CarRepairFinished"), new DialogCriteria("Type", "Header"));
  }

  public void OnNewCarDesignComplete(Person inSender)
  {
    this.SendMail(inSender, new DialogCriteria("Who", "Engineer"), new DialogCriteria("Source", "ContractEnded"), new DialogCriteria("Type", "Header"));
  }

  public void OnNewCarDesignIterationComplete(Person inSender)
  {
    this.SendMail(inSender, new DialogCriteria("Who", "Engineer"), new DialogCriteria("Source", "ContractEnded"), new DialogCriteria("Type", "Header"));
  }

  public void OnNewCarInitialDesignComplete(Person inSender)
  {
    this.SendMail(inSender, new DialogCriteria("Who", "Engineer"), new DialogCriteria("Source", "ContractEnded"), new DialogCriteria("Type", "Header"));
  }

  public void OnNewCarDesignRulesBroken(Person inSender)
  {
    this.SendMail(inSender, new DialogCriteria("Who", "Engineer"), new DialogCriteria("Source", "ContractEnded"), new DialogCriteria("Type", "Header"));
  }

  public void OnHQBuildingComplete(HQsBuilding_v1 inBuilding)
  {
    Person personOnJob = Game.instance.player.team.contractManager.GetPersonOnJob(Contract.Job.TeamAssistant);
    StringVariableParser.building = inBuilding;
    this.SendMail(personOnJob, new DialogCriteria("Who", "Assistant"), new DialogCriteria("Source", "NewBuildingComplete"), new DialogCriteria("Type", "Header"));
  }

  public void OnHQBuildingUpgraded(HQsBuilding_v1 inBuilding)
  {
    Person personOnJob = Game.instance.player.team.contractManager.GetPersonOnJob(Contract.Job.TeamAssistant);
    StringVariableParser.building = inBuilding;
    this.SendMail(personOnJob, new DialogCriteria("Who", "Assistant"), new DialogCriteria("Source", "HQUpgradeComplete"), new DialogCriteria("Type", "Header"));
  }

  public void OnAIHQBuildingComplete(HQsBuilding_v1 inBuilding)
  {
    if ((double) RandomUtility.GetRandom01() <= 0.5)
      return;
    StringVariableParser.rumourTeam = inBuilding.team;
    StringVariableParser.building = inBuilding;
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "MediaPerson"),
      new DialogCriteria("Source", "NewBuildingComplete"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnAIHQBuildingUpgraded(HQsBuilding_v1 inBuilding)
  {
    if ((double) RandomUtility.GetRandom01() <= 0.5)
      return;
    StringVariableParser.rumourTeam = inBuilding.team;
    StringVariableParser.building = inBuilding;
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "MediaPerson"),
      new DialogCriteria("Source", "NewUpgradeComplete"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnUltimatum(Person inSender)
  {
    this.SendMail(inSender, new DialogCriteria("Who", "Chairman"), new DialogCriteria("Source", "UltimatumIssued"), new DialogCriteria("Type", "Header"));
  }

  public void OnUltimatumPlayerFired(Person inSender)
  {
    this.SendMail(inSender, new DialogCriteria("Who", "Chairman"), new DialogCriteria("Source", "PlayerFired"), new DialogCriteria("Type", "Header"));
  }

  public void OnUltimatumPlayerSafe(Person inSender)
  {
    this.SendMail(inSender, new DialogCriteria("Who", "Chairman"), new DialogCriteria("Source", "PlayerJobSafe"), new DialogCriteria("Type", "Header"));
  }

  public void OnAIUltimatum(Person inSender)
  {
    StringVariableParser.rumourTeam = inSender.contract.GetTeam();
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "MediaPerson"),
      new DialogCriteria("Source", "ChairmanDislikesManager"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnUltimatumAIFired(Person inSender)
  {
    StringVariableParser.subject = inSender;
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "MediaPerson"),
      new DialogCriteria("Source", "ManagerFired"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnPartReset()
  {
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "IMAPresident"),
      new DialogCriteria("Source", "CarReset"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnPartRuleBroken(Person inSender, CarPart inPart)
  {
    StringVariableParser.partBanned = inPart;
    this.SendMail(inSender, new DialogCriteria("Who", "IMAPresident"), new DialogCriteria("Source", "PartBanned"), new DialogCriteria("Type", "Header"));
  }

  public Message OnDilemmaTriggered(DialogRule inDialogRule)
  {
    this.mResetStrings = false;
    return this.PostMessage(inDialogRule, (Person) null);
  }

  public Message OnDilemmaTriggered(DialogCriteria[] inDialogCriteria)
  {
    this.mResetStrings = false;
    return this.SendMail(inDialogCriteria);
  }

  public Message OnDilemmaPromiseTriggered(Person inSender, DialogCriteria[] inDialogCriteria)
  {
    this.mResetStrings = false;
    DialogQuery inQuery = new DialogQuery();
    for (int index = 0; index < inDialogCriteria.Length; ++index)
    {
      if (inDialogCriteria[index].mType == "Who")
        inQuery.who = inDialogCriteria[index];
      else
        inQuery.AddCriteria(inDialogCriteria[index].mType, inDialogCriteria[index].mCriteriaInfo);
    }
    return this.SendMail(inSender, inQuery, true);
  }

  public void OnThreeDaysToEvent()
  {
    Message message = this.SendMail(new DialogCriteria[3]{ new DialogCriteria("Who", "MediaPerson"), new DialogCriteria("Source", "RacePreview"), new DialogCriteria("Type", "Header") });
    if (message != null)
      message.SetGroupType(Message.Group.Media);
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "Chairman"),
      new DialogCriteria("Source", "FinancialManagementConcerns"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnSevenDaysToEvent()
  {
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "Assistant"),
      new DialogCriteria("Source", "PreRaceInformation"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnPlayerJobApplicationReceived(Person inSender)
  {
    this.SendMail(inSender, new DialogCriteria[2]
    {
      new DialogCriteria("Source", "PlayerJobApplicationReceived"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnPlayerJobApplicationPreSeasonReceived(Person inSender)
  {
    this.SendMail(inSender, new DialogCriteria[2]
    {
      new DialogCriteria("Source", "PlayerJobApplicationReceivedPreSeason"),
      new DialogCriteria("Type", "Header")
    });
  }

  public Message OnPlayerJobApplicationDecision(Person inSender, bool inSuccessfull)
  {
    StringVariableParser.jobAccepted = inSuccessfull;
    Message message = this.SendMail(inSender, new DialogCriteria("Who", "Chairman"), new DialogCriteria("Source", "PlayerJobApplicationDecision"), new DialogCriteria("Type", "Header"));
    this.SendPlayerRuleDependentMails();
    return message;
  }

  public Message OnPlayerJobApplicationPreSeasonDecision(Person inSender, bool inSuccessfull)
  {
    StringVariableParser.jobAccepted = inSuccessfull;
    return this.SendMail(inSender, new DialogCriteria("Who", "Chairman"), new DialogCriteria("Source", "PlayerJobApplicationDecisionPreSeason"), new DialogCriteria("Type", "Header"));
  }

  public void OnTeamFeedback()
  {
    StringVariableParser.SetStaticData((Person) null);
    this.SendMail(new DialogCriteria[2]
    {
      new DialogCriteria("Source", "TeamFeedback"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnEventStartMessages()
  {
  }

  public void OnChairmanPerformanceUpdate()
  {
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "Chairman"),
      new DialogCriteria("Source", "ChairmanTargetsUpdate"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnEventEndMessages()
  {
    if (Game.instance.tutorialSystem.isTutorialActive)
      return;
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "Mechanic"),
      new DialogCriteria("Source", "CarConditionUpdate"),
      new DialogCriteria("Type", "Header")
    });
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "MediaPerson"),
      new DialogCriteria("Source", "PostRaceInterviewRequest"),
      new DialogCriteria("Type", "Header")
    });
    if (Game.instance.player.IsUnemployed() || !Game.instance.player.team.championship.HasSeasonEnded())
      return;
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "Chairman"),
      new DialogCriteria("Source", "EndSeasonTargetReview"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void OnDayEndMessages()
  {
    if (Game.instance.player.IsUnemployed())
      return;
    int days = (Game.instance.player.team.championship.GetCurrentEventDetails().eventDate - Game.instance.time.now).Days;
    if (days == 3)
      this.OnThreeDaysToEvent();
    if (days == 6)
      this.OnTeamFeedback();
    if (days == 7)
      this.OnSevenDaysToEvent();
    if (days != 8 || Game.instance.player.team.championship.eventNumber <= 4)
      return;
    this.OnChairmanPerformanceUpdate();
  }

  public void OnBudgetNegative(Finance inFinance)
  {
  }

  public void OnTutorialRaceFinished()
  {
    if (this.tutRaceMessage)
      return;
    this.tutRaceMessage = true;
  }

  public void OnStartGameMessages()
  {
    if (!Game.instance.player.team.isCreatedAndManagedByPlayer())
    {
      this.SendMail(new DialogCriteria[3]
      {
        new DialogCriteria("Who", "Assistant"),
        new DialogCriteria("Source", "WelcomeFromAssistant"),
        new DialogCriteria("Type", "Header")
      });
      StringVariableParser.anyDriver = (Driver) this.FindPerson("Driver1");
      this.SendMail(new DialogCriteria[3]
      {
        new DialogCriteria("Who", "AnyDriver"),
        new DialogCriteria("Source", "WelcomeFromDriver"),
        new DialogCriteria("Type", "Header")
      });
      StringVariableParser.anyDriver = (Driver) this.FindPerson("Driver2");
      this.SendMail(new DialogCriteria[3]
      {
        new DialogCriteria("Who", "AnyDriver"),
        new DialogCriteria("Source", "WelcomeFromDriver"),
        new DialogCriteria("Type", "Header")
      });
      this.SendMail(new DialogCriteria[3]
      {
        new DialogCriteria("Who", "Engineer"),
        new DialogCriteria("Source", "WelcomeFromEngineer"),
        new DialogCriteria("Type", "Header")
      });
      this.SendMail(new DialogCriteria[3]
      {
        new DialogCriteria("Who", "MediaPerson"),
        new DialogCriteria("Source", "PlayerJobAppointment"),
        new DialogCriteria("Type", "Header")
      });
      this.SendMail(new DialogCriteria[3]
      {
        new DialogCriteria("Who", "MediaPerson"),
        new DialogCriteria("Source", "PlayerJobAppointmentReaction"),
        new DialogCriteria("Type", "Header")
      });
      this.SendMail(new DialogCriteria[3]
      {
        new DialogCriteria("Who", "Scout"),
        new DialogCriteria("Source", "ScoutReportOnJoiningTeam"),
        new DialogCriteria("Type", "Header")
      });
    }
    if (Game.instance.player.team.isCreatedAndManagedByPlayer())
    {
      this.SendMail(new DialogCriteria[3]
      {
        new DialogCriteria("Who", "Chairman"),
        new DialogCriteria("Source", "CreateTeamChairman"),
        new DialogCriteria("Type", "Header")
      });
      this.SendMail(new DialogCriteria[3]
      {
        new DialogCriteria("Who", "Assistant"),
        new DialogCriteria("Source", "CreateTeamAssistant"),
        new DialogCriteria("Type", "Header")
      });
      this.SendMail(new DialogCriteria[3]
      {
        new DialogCriteria("Who", "Chairman"),
        new DialogCriteria("Source", "CreateTeamExpectations"),
        new DialogCriteria("Type", "Header")
      });
      this.SendMail(new DialogCriteria[3]
      {
        new DialogCriteria("Who", "Scout"),
        new DialogCriteria("Source", "CreateTeamScout"),
        new DialogCriteria("Type", "Header")
      });
    }
    if (!Game.instance.tutorialSystem.isTutorialActive)
    {
      if (!Game.instance.player.team.isCreatedAndManagedByPlayer())
        this.SendMail(new DialogCriteria[3]
        {
          new DialogCriteria("Who", "Chairman"),
          new DialogCriteria("Source", "ChairmanExpectations"),
          new DialogCriteria("Type", "Header")
        });
      Message message = this.SendMail(new DialogCriteria[3]{ new DialogCriteria("Who", "MediaPerson"), new DialogCriteria("Source", "GameStartInterviewRequest"), new DialogCriteria("Type", "Header") });
      if (message != null)
        message.SetGroupType(Message.Group.Media);
    }
    this.SendPlayerRuleDependentMails();
  }

  public void SendFirstMessageOfPoliticalSeason()
  {
    if (Game.instance.player.careerHistory.GetLatestFinishedEntry() == null)
      this.SendMail(new DialogCriteria[3]
      {
        new DialogCriteria("Who", "IMAPresident"),
        new DialogCriteria("Source", "GMAInduction"),
        new DialogCriteria("Type", "Header")
      });
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "MediaPerson"),
      new DialogCriteria("Source", "FirstGMAMeetingOfSeason"),
      new DialogCriteria("Type", "Header")
    });
  }

  public void PlayerVoteChoiceMessage()
  {
    this.SendMail(new DialogCriteria[3]
    {
      new DialogCriteria("Who", "IMAPresident"),
      new DialogCriteria("Source", "PlayerVoteChoice"),
      new DialogCriteria("Type", "Header")
    });
  }
}
