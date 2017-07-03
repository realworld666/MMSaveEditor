// Decompiled with JetBrains decompiler
// Type: UIMailMessageBody
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMailMessageBody : MonoBehaviour
{
  public List<Button> buttons = new List<Button>();
  [Header("General Settings (Null Safe)")]
  public Message.BodyType bodyType;
  public TextMeshProUGUI description;
  public TextMeshProUGUI buttonActionText;
  public TextMeshProUGUI titleLabel;
  public TextMeshProUGUI senderName;
  public TextMeshProUGUI senderTitle;
  public UICharacterPortrait staffPortrait;
  public UICharacterPortrait driverPortrait;
  public GameObject buttonsContainer;
  private Message mMessage;
  private Person mPerson;

  public Message message
  {
    get
    {
      return this.mMessage;
    }
  }

  public virtual void OpenMail(Message inMessage)
  {
    this.mMessage = inMessage;
    this.mPerson = inMessage.sender;
    if (this.mPerson != null)
    {
      if ((UnityEngine.Object) this.senderName != (UnityEngine.Object) null)
      {
        StringVariableParser.subject = this.mPerson;
        this.senderName.text = Localisation.LocaliseID("PSG_10008697", (GameObject) null);
      }
      if ((UnityEngine.Object) this.senderTitle != (UnityEngine.Object) null)
      {
        StringVariableParser.subject = this.mPerson;
        this.senderTitle.text = Localisation.LocaliseEnum((Enum) this.mPerson.contract.job);
      }
      if ((UnityEngine.Object) this.driverPortrait != (UnityEngine.Object) null && (UnityEngine.Object) this.staffPortrait != (UnityEngine.Object) null)
      {
        if (this.mPerson is Driver)
        {
          this.driverPortrait.gameObject.SetActive(true);
          this.driverPortrait.SetPortrait(this.mPerson);
          this.staffPortrait.gameObject.SetActive(false);
        }
        else
        {
          this.driverPortrait.gameObject.SetActive(false);
          this.staffPortrait.SetPortrait(this.mPerson);
          this.staffPortrait.gameObject.SetActive(true);
        }
      }
    }
    if ((UnityEngine.Object) this.titleLabel != (UnityEngine.Object) null)
      this.titleLabel.text = this.mMessage.localisedTitle;
    if ((UnityEngine.Object) this.description != (UnityEngine.Object) null)
      this.description.text = this.mMessage.localisedDescription;
    if ((UnityEngine.Object) this.buttonActionText != (UnityEngine.Object) null)
    {
      if (this.mMessage.messageResponseData == null)
        this.buttonActionText.text = string.Empty;
      else
        this.buttonActionText.text = this.mMessage.messageResponseData.textTranslated;
    }
    this.gameObject.SetActive(true);
    this.SetupButtons();
    this.SetupSpecialObjectText();
  }

  protected virtual void SetupButtons()
  {
    GameUtility.SetActiveAndCheckNull(this.buttonsContainer, this.mMessage.buttonsRule != null);
    if (this.mMessage.buttonsRule == null)
      return;
    int num = 0;
    for (int index1 = 0; index1 < this.buttons.Count; ++index1)
    {
      Button button = this.buttons[index1];
      bool flag = false;
      if (num < this.mMessage.buttonsRule.userData.Count)
      {
        for (int index2 = num; index2 < this.mMessage.buttonsRule.userData.Count; ++index2)
        {
          DialogCriteria dialogCriteria = this.mMessage.buttonsRule.userData[index2];
          if (!this.mMessage.responded && dialogCriteria.mType.Equals("mailbutton", StringComparison.OrdinalIgnoreCase) && this.ShowToPlayer(dialogCriteria.mCriteriaInfo))
          {
            button.interactable = true;
            this.AssignButton(this.mMessage, button, dialogCriteria.mCriteriaInfo);
            flag = true;
            num = index2 + 1;
            break;
          }
        }
      }
      button.gameObject.SetActive(flag);
    }
  }

  public virtual void SetupSpecialObjectText()
  {
  }

  private void Refresh()
  {
    this.OpenMail(this.mMessage);
  }

  public bool ShowToPlayer(string inButtonType)
  {
    if (Game.instance.player.IsUnemployed())
    {
      string key = inButtonType;
      if (key != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (UIMailMessageBody.\u003C\u003Ef__switch\u0024map3E == null)
        {
          // ISSUE: reference to a compiler-generated field
          UIMailMessageBody.\u003C\u003Ef__switch\u0024map3E = new Dictionary<string, int>(4)
          {
            {
              "StartInterview",
              0
            },
            {
              "RejectInterview",
              0
            },
            {
              "PSG_10006712",
              0
            },
            {
              "PSG_10006713",
              0
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (UIMailMessageBody.\u003C\u003Ef__switch\u0024map3E.TryGetValue(key, out num) && num == 0)
          return true;
      }
      return false;
    }
    string key1 = inButtonType;
    if (key1 != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (UIMailMessageBody.\u003C\u003Ef__switch\u0024map3F == null)
      {
        // ISSUE: reference to a compiler-generated field
        UIMailMessageBody.\u003C\u003Ef__switch\u0024map3F = new Dictionary<string, int>(1)
        {
          {
            "PartDevelopment",
            0
          }
        };
      }
      int num;
      // ISSUE: reference to a compiler-generated field
      if (UIMailMessageBody.\u003C\u003Ef__switch\u0024map3F.TryGetValue(key1, out num) && num == 0)
      {
        if (!Game.instance.player.team.carManager.partImprovement.FixingCondition())
          return !Game.instance.player.team.championship.InPreseason();
        return false;
      }
    }
    return true;
  }

  private void RespondedToMessage()
  {
    this.mMessage.SetResponded();
    this.mMessage.SetPriorityType(Message.Priority.Normal);
    UIManager.instance.GetScreen<MailScreen>().UpdateToggleText();
    UIManager.instance.navigationBars.bottomBar.MarkContinueButtonForUpdate();
    for (int index = 0; index < this.buttons.Count; ++index)
      this.buttons[index].gameObject.SetActive(false);
  }

  private void CheckUserdataRequirements(string inButtonType)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UIMailMessageBody.\u003CCheckUserdataRequirements\u003Ec__AnonStorey84 requirementsCAnonStorey84 = new UIMailMessageBody.\u003CCheckUserdataRequirements\u003Ec__AnonStorey84();
    // ISSUE: reference to a compiler-generated field
    requirementsCAnonStorey84.\u003C\u003Ef__this = this;
    DialogRule ruleById = App.instance.dialogRulesManager.GetRuleByID(inButtonType);
    // ISSUE: reference to a compiler-generated field
    requirementsCAnonStorey84.newData = new TextDynamicData();
    // ISSUE: reference to a compiler-generated field
    requirementsCAnonStorey84.newData.SetMessageTextFields(inButtonType);
    switch (this.mMessage.headerType)
    {
      case Message.HeaderType.Dilemma:
        // ISSUE: reference to a compiler-generated method
        Action inRespondedDilemmaAction = new Action(requirementsCAnonStorey84.\u003C\u003Em__17F);
        Game.instance.dilemmaSystem.TriggerDilemmaChoice(this.mMessage, ruleById, inRespondedDilemmaAction);
        break;
      case Message.HeaderType.PersonalityTrait:
        PersonalityTrait personalityTrait = this.mMessage.specialObject[0] as PersonalityTrait;
        if (personalityTrait != null)
          personalityTrait.OnTriggerTrait();
        this.RespondedToMessage();
        // ISSUE: reference to a compiler-generated field
        this.mMessage.messageResponseData = requirementsCAnonStorey84.newData;
        this.Refresh();
        break;
      default:
        List<DialogCriteria> userData = ruleById.userData;
        for (int index = 0; index < userData.Count; ++index)
        {
          if (userData[index].mType == "ChangeScreen")
          {
            UIManager.instance.ChangeScreen(userData[index].mCriteriaInfo, UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
            break;
          }
        }
        this.RespondedToMessage();
        // ISSUE: reference to a compiler-generated field
        this.mMessage.messageResponseData = requirementsCAnonStorey84.newData;
        this.Refresh();
        break;
    }
  }

  public void Close()
  {
    GameUtility.SetActive(this.gameObject, false);
  }

  public void AssignButton(Message inMessage, Button inButton, string inButtonType)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey9D buttonCAnonStorey9D = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey9D();
    // ISSUE: reference to a compiler-generated field
    buttonCAnonStorey9D.inButtonType = inButtonType;
    // ISSUE: reference to a compiler-generated field
    buttonCAnonStorey9D.\u003C\u003Ef__this = this;
    inButton.onClick.RemoveAllListeners();
    TextMeshProUGUI componentInChildren = inButton.GetComponentInChildren<TextMeshProUGUI>();
    // ISSUE: reference to a compiler-generated field
    string inButtonType1 = buttonCAnonStorey9D.inButtonType;
    if (inButtonType1 != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (UIMailMessageBody.\u003C\u003Ef__switch\u0024map40 == null)
      {
        // ISSUE: reference to a compiler-generated field
        UIMailMessageBody.\u003C\u003Ef__switch\u0024map40 = new Dictionary<string, int>(50)
        {
          {
            "StartInterview",
            0
          },
          {
            "RejectInterview",
            1
          },
          {
            "Profile",
            2
          },
          {
            "Finance",
            3
          },
          {
            "Team",
            4
          },
          {
            "OnContractNegotiated",
            5
          },
          {
            "Staff",
            6
          },
          {
            "StaffScreen",
            6
          },
          {
            "Drivers",
            7
          },
          {
            "Scouting",
            8
          },
          {
            "HeadquartersScreen",
            9
          },
          {
            "HQ",
            9
          },
          {
            "CarPartFittingScreen",
            10
          },
          {
            "Design",
            11
          },
          {
            "PartDesign",
            12
          },
          {
            "NewCarDesign",
            13
          },
          {
            "PartDevelopment",
            14
          },
          {
            "CarScreen",
            15
          },
          {
            "Factory",
            15
          },
          {
            "Car1",
            16
          },
          {
            "Car2",
            17
          },
          {
            "Sponsors",
            18
          },
          {
            "Politics",
            19
          },
          {
            "Standings",
            20
          },
          {
            "RulesScreen",
            21
          },
          {
            "CarDesignScreen",
            22
          },
          {
            "PreSeasonTesting",
            23
          },
          {
            "AllDriversScreen",
            24
          },
          {
            "ContractNegotiationScreen",
            25
          },
          {
            "DriverPoached",
            26
          },
          {
            "PrizesScreen",
            27
          },
          {
            "LiveryScreen",
            28
          },
          {
            "EventCalendarScreen",
            29
          },
          {
            "RejectPromotion",
            30
          },
          {
            "AcceptPromotion",
            31
          },
          {
            "PlayerVote",
            32
          },
          {
            "RejectPlayerVote",
            33
          },
          {
            "GotIt",
            34
          },
          {
            "PSG_10004782",
            35
          },
          {
            "PSG_10004783",
            36
          },
          {
            "PSG_10004784",
            37
          },
          {
            "PSG_10004861",
            38
          },
          {
            "PSG_10004862",
            39
          },
          {
            "PSG_10004863",
            40
          },
          {
            "PSG_10006712",
            41
          },
          {
            "PSG_10006713",
            42
          },
          {
            "PSG_10004901",
            43
          },
          {
            "PSG_10011756",
            44
          },
          {
            "PSG_10011757",
            45
          },
          {
            "PSG_10011758",
            46
          }
        };
      }
      int num;
      // ISSUE: reference to a compiler-generated field
      if (UIMailMessageBody.\u003C\u003Ef__switch\u0024map40.TryGetValue(inButtonType1, out num))
      {
        switch (num)
        {
          case 0:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey85 buttonCAnonStorey85 = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey85();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey85.\u003C\u003Ef__this = this;
            componentInChildren.text = Localisation.LocaliseID("PSG_10000627", (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey85.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey85.newData.SetMessageTextFields("PSG_10000629");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey85.\u003C\u003Em__180));
            return;
          case 1:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey86 buttonCAnonStorey86 = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey86();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey86.\u003C\u003Ef__this = this;
            componentInChildren.text = Localisation.LocaliseID("PSG_10000628", (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey86.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey86.newData.SetMessageTextFields("PSG_10000630");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey86.\u003C\u003Em__181));
            return;
          case 2:
            componentInChildren.text = Localisation.LocaliseID("PSG_10001277", (GameObject) null);
            inButton.onClick.AddListener((UnityAction) (() => UIManager.instance.ChangeScreen("ProfileScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal)));
            return;
          case 3:
            componentInChildren.text = Localisation.LocaliseID("PSG_10000006", (GameObject) null);
            inButton.onClick.AddListener((UnityAction) (() => UIManager.instance.ChangeScreen("FinanceScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal)));
            return;
          case 4:
            componentInChildren.text = Localisation.LocaliseID("PSG_10000005", (GameObject) null);
            inButton.onClick.AddListener((UnityAction) (() => UIManager.instance.ChangeScreen("TeamScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal)));
            return;
          case 5:
            if (inMessage.sender is Driver)
            {
              componentInChildren.text = Localisation.LocaliseID("PSG_10007591", (GameObject) null);
              inButton.onClick.AddListener((UnityAction) (() => UIManager.instance.ChangeScreen("AllDriversScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal)));
              return;
            }
            componentInChildren.text = Localisation.LocaliseID("PSG_10003781", (GameObject) null);
            inButton.onClick.AddListener((UnityAction) (() => UIManager.instance.ChangeScreen("StaffScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal)));
            return;
          case 6:
            componentInChildren.text = Localisation.LocaliseID("PSG_10003781", (GameObject) null);
            inButton.onClick.AddListener((UnityAction) (() => UIManager.instance.ChangeScreen("StaffScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal)));
            return;
          case 7:
            componentInChildren.text = Localisation.LocaliseID("PSG_10007591", (GameObject) null);
            inButton.onClick.AddListener((UnityAction) (() => UIManager.instance.ChangeScreen("DriversScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal)));
            return;
          case 8:
            componentInChildren.text = Localisation.LocaliseID("PSG_10007590", (GameObject) null);
            inButton.onClick.AddListener((UnityAction) (() => UIManager.instance.ChangeScreen("ScoutingScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal)));
            return;
          case 9:
            componentInChildren.text = Localisation.LocaliseID("PSG_10007595", (GameObject) null);
            inButton.onClick.AddListener((UnityAction) (() => UIManager.instance.ChangeScreen("HeadquartersScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal)));
            return;
          case 10:
            componentInChildren.text = Localisation.LocaliseID("PSG_10007592", (GameObject) null);
            inButton.onClick.AddListener((UnityAction) (() => UIManager.instance.ChangeScreen("CarPartFittingScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal)));
            return;
          case 11:
            componentInChildren.text = Localisation.LocaliseID("PSG_10003780", (GameObject) null);
            inButton.onClick.AddListener((UnityAction) (() => UIManager.instance.ChangeScreen("DesignScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal)));
            return;
          case 12:
            componentInChildren.text = Localisation.LocaliseID("PSG_10007596", (GameObject) null);
            inButton.onClick.AddListener((UnityAction) (() => UIManager.instance.ChangeScreen("PartDesignScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal)));
            return;
          case 13:
            componentInChildren.text = Localisation.LocaliseID("PSG_10001275", (GameObject) null);
            inButton.onClick.AddListener((UnityAction) (() =>
            {
              UIManager.instance.ChangeScreen("CarDesignScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
              ((PreSeasonState) App.instance.gameStateManager.currentState).SetStage(PreSeasonState.PreSeasonStage.DesigningCar);
            }));
            return;
          case 14:
            componentInChildren.text = Localisation.LocaliseID("PSG_10007593", (GameObject) null);
            inButton.onClick.AddListener((UnityAction) (() => UIManager.instance.ChangeScreen("FactoryPartDevelopmentScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal)));
            return;
          case 15:
            componentInChildren.text = Localisation.LocaliseID("PSG_10007589", (GameObject) null);
            inButton.onClick.AddListener((UnityAction) (() => UIManager.instance.ChangeScreen("CarScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal)));
            return;
          case 16:
            componentInChildren.text = Localisation.LocaliseID("Car1 (notLocalised)", (GameObject) null);
            inButton.onClick.AddListener((UnityAction) (() => UIManager.instance.ChangeScreen("CarDevelopmentScreen", (Entity) Game.instance.player.team.GetDriver(0), UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal)));
            return;
          case 17:
            componentInChildren.text = Localisation.LocaliseID("Car2 (notLocalised)", (GameObject) null);
            inButton.onClick.AddListener((UnityAction) (() => UIManager.instance.ChangeScreen("CarDevelopmentScreen", (Entity) Game.instance.player.team.GetDriver(1), UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal)));
            return;
          case 18:
            componentInChildren.text = Localisation.LocaliseID("PSG_10003782", (GameObject) null);
            inButton.onClick.AddListener((UnityAction) (() => UIManager.instance.ChangeScreen("SponsorsScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal)));
            return;
          case 19:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey87 buttonCAnonStorey87 = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey87();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey87.\u003C\u003Ef__this = this;
            componentInChildren.text = Localisation.LocaliseID("PSG_10003783", (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey87.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey87.newData.SetMessageTextFields("PSG_10004853");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey87.\u003C\u003Em__194));
            return;
          case 20:
            componentInChildren.text = Localisation.LocaliseID("PSG_10007594", (GameObject) null);
            inButton.onClick.AddListener((UnityAction) (() => UIManager.instance.ChangeScreen("StandingsScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal)));
            return;
          case 21:
            componentInChildren.text = Localisation.LocaliseID("PSG_10010398", (GameObject) null);
            inButton.onClick.AddListener((UnityAction) (() =>
            {
              UIManager.instance.ChangeScreen("StandingsScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
              UIManager.instance.GetScreen<StandingsScreen>().overviewWidget.currentRulesButton.onClick.Invoke();
            }));
            return;
          case 22:
            componentInChildren.text = Localisation.LocaliseID("PSG_10010397", (GameObject) null);
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey9D.\u003C\u003Em__197));
            return;
          case 23:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey88 buttonCAnonStorey88 = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey88();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey88.\u003C\u003Ef__this = this;
            componentInChildren.text = Localisation.LocaliseID("PSG_10010396", (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey88.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey88.newData.SetMessageTextFields("PSG_10004840");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey88.\u003C\u003Em__198));
            return;
          case 24:
            componentInChildren.text = Localisation.LocaliseID("PSG_10010395", (GameObject) null);
            inButton.onClick.AddListener((UnityAction) (() => UIManager.instance.ChangeScreen("AllDriversScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal)));
            return;
          case 25:
            componentInChildren.text = Localisation.LocaliseID("PSG_10004761", (GameObject) null);
            ContractManagerPerson contractManager = this.mMessage.sender.contractManager;
            if (!Game.instance.player.IsUnemployed() && contractManager.isNegotiating && (!contractManager.isConsideringProposal && contractManager.draftProposalContract.GetTeam().IsPlayersTeam()))
            {
              // ISSUE: reference to a compiler-generated method
              inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey9D.\u003C\u003Em__19A));
              return;
            }
            inButton.interactable = false;
            return;
          case 26:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey89 buttonCAnonStorey89 = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey89();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey89.\u003C\u003Ef__this = this;
            componentInChildren.text = Localisation.LocaliseID("PSG_10011111", (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey89.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey89.newData.SetMessageTextFields("PSG_10011111");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey89.\u003C\u003Em__19B));
            return;
          case 27:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey8A buttonCAnonStorey8A = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey8A();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey8A.\u003C\u003Ef__this = this;
            componentInChildren.text = Localisation.LocaliseID("PSG_10010390", (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey8A.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey8A.newData.SetMessageTextFields("PSG_10004839");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey8A.\u003C\u003Em__19C));
            return;
          case 28:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey8B buttonCAnonStorey8B = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey8B();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey8B.\u003C\u003Ef__this = this;
            componentInChildren.text = Localisation.LocaliseID("PSG_10010391", (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey8B.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey8B.newData.SetMessageTextFields("PSG_10004835");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey8B.\u003C\u003Em__19D));
            return;
          case 29:
            componentInChildren.text = Localisation.LocaliseID("PSG_10010392", (GameObject) null);
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey9D.\u003C\u003Em__19E));
            return;
          case 30:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey8C buttonCAnonStorey8C = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey8C();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey8C.\u003C\u003Ef__this = this;
            componentInChildren.text = Localisation.LocaliseID("PSG_10010393", (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey8C.team = Game.instance.player.team;
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey8C.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey8C.newData.SetMessageTextFields("PSG_10004832");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey8C.\u003C\u003Em__19F));
            return;
          case 31:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey8D buttonCAnonStorey8D = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey8D();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey8D.\u003C\u003Ef__this = this;
            componentInChildren.text = Localisation.LocaliseID("PSG_10010394", (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey8D.team = Game.instance.player.team;
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey8D.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey8D.newData.SetMessageTextFields("PSG_10004831");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey8D.\u003C\u003Em__1A0));
            return;
          case 32:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey8E buttonCAnonStorey8E = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey8E();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey8E.\u003C\u003Ef__this = this;
            componentInChildren.text = Localisation.LocaliseID("PSG_10011910", (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey8E.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey8E.newData.SetMessageTextFields("PSG_10011908");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey8E.\u003C\u003Em__1A1));
            return;
          case 33:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey8F buttonCAnonStorey8F = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey8F();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey8F.\u003C\u003Ef__this = this;
            componentInChildren.text = Localisation.LocaliseID("PSG_10011911", (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey8F.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey8F.newData.SetMessageTextFields("PSG_10011909");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey8F.\u003C\u003Em__1A2));
            return;
          case 34:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey90 buttonCAnonStorey90 = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey90();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey90.\u003C\u003Ef__this = this;
            componentInChildren.text = Localisation.LocaliseID("PSG_10003346", (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey90.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey90.newData.SetMessageTextFields("PSG_10003346");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey90.\u003C\u003Em__1A3));
            return;
          case 35:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey91 buttonCAnonStorey91 = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey91();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey91.\u003C\u003Ef__this = this;
            // ISSUE: reference to a compiler-generated field
            componentInChildren.text = Localisation.LocaliseID(buttonCAnonStorey9D.inButtonType, (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey91.team = Game.instance.player.team;
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey91.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey91.newData.SetMessageTextFields("PSG_10004836");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey91.\u003C\u003Em__1A4));
            return;
          case 36:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey92 buttonCAnonStorey92 = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey92();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey92.\u003C\u003Ef__this = this;
            // ISSUE: reference to a compiler-generated field
            componentInChildren.text = Localisation.LocaliseID(buttonCAnonStorey9D.inButtonType, (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey92.team = Game.instance.player.team;
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey92.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey92.newData.SetMessageTextFields("PSG_10004837");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey92.\u003C\u003Em__1A5));
            return;
          case 37:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey93 buttonCAnonStorey93 = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey93();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey93.\u003C\u003Ef__this = this;
            // ISSUE: reference to a compiler-generated field
            componentInChildren.text = Localisation.LocaliseID(buttonCAnonStorey9D.inButtonType, (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey93.team = Game.instance.player.team;
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey93.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey93.newData.SetMessageTextFields("PSG_10004838");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey93.\u003C\u003Em__1A6));
            return;
          case 38:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey94 buttonCAnonStorey94 = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey94();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey94.\u003C\u003Ef__this = this;
            // ISSUE: reference to a compiler-generated field
            componentInChildren.text = Localisation.LocaliseID(buttonCAnonStorey9D.inButtonType, (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey94.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey94.newData.SetMessageTextFields("PSG_10004861");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey94.\u003C\u003Em__1A7));
            return;
          case 39:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey95 buttonCAnonStorey95 = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey95();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey95.\u003C\u003Ef__this = this;
            // ISSUE: reference to a compiler-generated field
            componentInChildren.text = Localisation.LocaliseID(buttonCAnonStorey9D.inButtonType, (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey95.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey95.newData.SetMessageTextFields("PSG_10004862");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey95.\u003C\u003Em__1A8));
            return;
          case 40:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey96 buttonCAnonStorey96 = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey96();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey96.\u003C\u003Ef__this = this;
            // ISSUE: reference to a compiler-generated field
            componentInChildren.text = Localisation.LocaliseID(buttonCAnonStorey9D.inButtonType, (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey96.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey96.newData.SetMessageTextFields("PSG_10004863");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey96.\u003C\u003Em__1A9));
            return;
          case 41:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey97 buttonCAnonStorey97 = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey97();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey97.\u003C\u003Ef__this = this;
            // ISSUE: reference to a compiler-generated field
            componentInChildren.text = Localisation.LocaliseID(buttonCAnonStorey9D.inButtonType, (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey97.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey97.newData.SetMessageTextFields("PSG_10006969");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey97.\u003C\u003Em__1AA));
            return;
          case 42:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey98 buttonCAnonStorey98 = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey98();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey98.\u003C\u003Ef__this = this;
            // ISSUE: reference to a compiler-generated field
            componentInChildren.text = Localisation.LocaliseID(buttonCAnonStorey9D.inButtonType, (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey98.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey98.newData.SetMessageTextFields("PSG_10006970");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey98.\u003C\u003Em__1AB));
            return;
          case 43:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey99 buttonCAnonStorey99 = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey99();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey99.\u003C\u003Ef__this = this;
            // ISSUE: reference to a compiler-generated field
            componentInChildren.text = Localisation.LocaliseID(buttonCAnonStorey9D.inButtonType, (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey99.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey99.newData.SetMessageTextFields("PSG_10004901");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey99.\u003C\u003Em__1AC));
            return;
          case 44:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey9A buttonCAnonStorey9A = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey9A();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey9A.\u003C\u003Ef__this = this;
            // ISSUE: reference to a compiler-generated field
            componentInChildren.text = Localisation.LocaliseID(buttonCAnonStorey9D.inButtonType, (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey9A.team = Game.instance.player.team;
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey9A.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey9A.newData.SetMessageTextFields("PSG_10011759");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey9A.\u003C\u003Em__1AD));
            return;
          case 45:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey9B buttonCAnonStorey9B = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey9B();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey9B.\u003C\u003Ef__this = this;
            // ISSUE: reference to a compiler-generated field
            componentInChildren.text = Localisation.LocaliseID(buttonCAnonStorey9D.inButtonType, (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey9B.team = Game.instance.player.team;
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey9B.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey9B.newData.SetMessageTextFields("PSG_10011760");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey9B.\u003C\u003Em__1AE));
            return;
          case 46:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey9C buttonCAnonStorey9C = new UIMailMessageBody.\u003CAssignButton\u003Ec__AnonStorey9C();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey9C.\u003C\u003Ef__this = this;
            // ISSUE: reference to a compiler-generated field
            componentInChildren.text = Localisation.LocaliseID(buttonCAnonStorey9D.inButtonType, (GameObject) null);
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey9C.team = Game.instance.player.team;
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey9C.newData = new TextDynamicData();
            // ISSUE: reference to a compiler-generated field
            buttonCAnonStorey9C.newData.SetMessageTextFields("PSG_10011761");
            // ISSUE: reference to a compiler-generated method
            inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey9C.\u003C\u003Em__1AF));
            return;
        }
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (!buttonCAnonStorey9D.inButtonType.Contains("PSG"))
      return;
    // ISSUE: reference to a compiler-generated field
    componentInChildren.text = Localisation.LocaliseID(buttonCAnonStorey9D.inButtonType, (GameObject) null);
    // ISSUE: reference to a compiler-generated method
    inButton.onClick.AddListener(new UnityAction(buttonCAnonStorey9D.\u003C\u003Em__1B0));
  }

  private void StartInterview()
  {
    if (Game.instance.player.team.championship.politicalSystem.activeVote != null)
      Game.instance.player.team.championship.politicalSystem.activeVote.ApplyImpactSpecialStrings(false);
    MediaInterviewDialog dialog = UIManager.instance.dialogBoxManager.GetDialog<MediaInterviewDialog>();
    Person sender = this.mMessage.sender;
    if (sender != null && sender.dialogQuery.ProcessQueryWithOwnCriteria(this.mMessage.buttonsRule.triggerQuery, false) != null)
    {
      UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
      dialog.Setup(this.mMessage.buttonsRule, this.mMessage.sender);
    }
    this.RespondedToMessage();
  }
}
