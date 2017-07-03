// Decompiled with JetBrains decompiler
// Type: UIMediaInterviewAnswerOutcome
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMediaInterviewAnswerOutcome : MonoBehaviour
{
  private List<MechanicBonus> mMechanicBonusUnlocked = new List<MechanicBonus>();
  private List<PersonalityTrait> mTraitAdded = new List<PersonalityTrait>();
  private UIMediaInterviewAnswerOutcome.FloatData mMoraleGained = new UIMediaInterviewAnswerOutcome.FloatData();
  private UIMediaInterviewAnswerOutcome.FloatData mHappinessGained = new UIMediaInterviewAnswerOutcome.FloatData();
  private UIMediaInterviewAnswerOutcome.FloatData mMarketabilityGained = new UIMediaInterviewAnswerOutcome.FloatData();
  private UIMediaInterviewAnswerOutcome.FloatData mRelationshipGained = new UIMediaInterviewAnswerOutcome.FloatData();
  public GameObject teamLogoContainer;
  public UITeamLogo teamLogo;
  public GameObject personDataContainer;
  public Flag flag;
  public UICharacterPortrait driverPortrait;
  public UICharacterPortrait staffPortrait;
  public TextMeshProUGUI personName;
  public TextMeshProUGUI personJobTitle;
  public UIAbilityStars abilityStars;
  public GameObject moraleContainer;
  public TextMeshProUGUI moraleHeader;
  public TextMeshProUGUI preInterviewMorale;
  public TextMeshProUGUI currentMorale;
  public TextMeshProUGUI moraleChange;
  public GameObject mechanicRelationshipContainer;
  public TextMeshProUGUI preInterviewMechanicRelationship;
  public TextMeshProUGUI currentMechanicRelationship;
  public TextMeshProUGUI mechanicRelationshipChange;
  public UIGridList mechanicBonusList;
  public GameObject traitContainer;
  public UIGridList personalityTraitList;
  public GameObject marketabilityContainer;
  public TextMeshProUGUI preInterviewMarketability;
  public TextMeshProUGUI currentMarketability;
  public TextMeshProUGUI marketabilityChange;
  public Animator animator;
  private DialogCriteria mUserData;
  private object mTarget;
  private float mTimer;

  public object target
  {
    get
    {
      return this.mTarget;
    }
  }

  public void Setup(DialogCriteria inUserData, bool inSetupImpacts)
  {
    if (!this.gameObject.activeSelf)
      this.mTarget = (object) null;
    GameUtility.SetActive(this.gameObject, true);
    this.mUserData = inUserData;
    this.mTimer = -1f;
    this.mMoraleGained.ResetValues(true);
    this.mHappinessGained.ResetValues(true);
    this.mMarketabilityGained.ResetValues(true);
    this.mRelationshipGained.ResetValues(true);
    this.SetupUserData(this.mUserData, inSetupImpacts);
  }

  public void SetAlphaState(bool inStatChanged)
  {
    if (inStatChanged)
      this.animator.SetTrigger("OnFocus");
    else
      this.animator.SetTrigger("Unfocus");
  }

  private void SetupUserData(DialogCriteria inCriteria, bool inSetupImpacts)
  {
    try
    {
      string[] strArray = inCriteria.mType.Split(':');
      if (strArray.Length <= 1)
        return;
      object obj = StringVariableParser.GetObject(strArray[0].Trim());
      Person inPerson = obj as Person;
      Team team = obj as Team;
      Driver inDriver = inPerson as Driver;
      if (inPerson == null && team == null)
      {
        Debug.LogErrorFormat("Could not find target for media answer impact. {0}", (object) strArray[0]);
      }
      else
      {
        if (obj != this.mTarget)
        {
          this.mTarget = obj;
          this.Reset();
          this.mMoraleGained.ResetValues(false);
          this.mHappinessGained.ResetValues(false);
          this.mMarketabilityGained.ResetValues(false);
          this.mRelationshipGained.ResetValues(false);
          this.SetTargetData(this.mTarget);
        }
        if (!inSetupImpacts)
          return;
        this.SetAlphaState(true);
        float result = 0.0f;
        float.TryParse(inCriteria.mCriteriaInfo, out result);
        string lower1 = strArray[1].Trim().ToLower();
        if (lower1 != null)
        {
          // ISSUE: reference to a compiler-generated field
          if (UIMediaInterviewAnswerOutcome.\u003C\u003Ef__switch\u0024map3B == null)
          {
            // ISSUE: reference to a compiler-generated field
            UIMediaInterviewAnswerOutcome.\u003C\u003Ef__switch\u0024map3B = new Dictionary<string, int>(6)
            {
              {
                "morale",
                0
              },
              {
                "mechanicrelationship",
                1
              },
              {
                "happiness",
                2
              },
              {
                "marketability",
                3
              },
              {
                "addtrait",
                4
              },
              {
                "reactiontoperformance",
                5
              }
            };
          }
          int num1;
          // ISSUE: reference to a compiler-generated field
          if (UIMediaInterviewAnswerOutcome.\u003C\u003Ef__switch\u0024map3B.TryGetValue(lower1, out num1))
          {
            switch (num1)
            {
              case 0:
                this.SetupMoraleChange(inPerson, result);
                return;
              case 1:
                // ISSUE: object of a compiler-generated type is created
                // ISSUE: variable of a compiler-generated type
                UIMediaInterviewAnswerOutcome.\u003CSetupUserData\u003Ec__AnonStorey65 dataCAnonStorey65 = new UIMediaInterviewAnswerOutcome.\u003CSetupUserData\u003Ec__AnonStorey65();
                // ISSUE: reference to a compiler-generated field
                dataCAnonStorey65.\u003C\u003Ef__this = this;
                if (inDriver == null)
                {
                  Debug.LogError((object) "Tried to access mechanic relationship for a non driver entity", (UnityEngine.Object) null);
                  return;
                }
                Mechanic mechanicOfDriver = inDriver.contract.GetTeam().GetMechanicOfDriver(inDriver);
                GameUtility.SetActive(this.mechanicRelationshipContainer, true);
                float relationshipAmount = mechanicOfDriver.CalculateCurrentRelationshipWithDriverForUI().relationshipAmount;
                this.mRelationshipGained.initialValue = relationshipAmount / 100f;
                this.mRelationshipGained.extraValue = result / 100f;
                if ((double) this.mRelationshipGained.preInterviewValue == 0.0)
                  this.mRelationshipGained.preInterviewValue = relationshipAmount / 100f;
                this.preInterviewMechanicRelationship.text = GameUtility.GetPercentageText(this.mRelationshipGained.preInterviewValue, 1f, false, true);
                this.currentMechanicRelationship.text = GameUtility.GetPercentageText(relationshipAmount / 100f, 1f, false, true);
                this.mechanicRelationshipChange.text = GameUtility.GetPercentageText(result / 100f, 1f, true, true);
                this.mechanicRelationshipChange.color = (double) result < 0.0 ? UIConstants.negativeColor : UIConstants.positiveColor;
                bool flag1 = mechanicOfDriver.IsBonusLevelUnlocked(0);
                bool flag2 = mechanicOfDriver.IsBonusLevelUnlocked(1);
                mechanicOfDriver.ModifyCurrentDriverRelationship(result, "PSG_10011066");
                bool flag3 = mechanicOfDriver.IsBonusLevelUnlocked(0);
                bool flag4 = mechanicOfDriver.IsBonusLevelUnlocked(1);
                if (flag1 != flag3)
                  this.mMechanicBonusUnlocked.Add(mechanicOfDriver.bonusOne);
                else if (flag2 != flag4)
                  this.mMechanicBonusUnlocked.Add(mechanicOfDriver.bonusTwo);
                // ISSUE: reference to a compiler-generated field
                dataCAnonStorey65.bonusIndex = this.mMechanicBonusUnlocked.Count - 1;
                // ISSUE: reference to a compiler-generated field
                if (dataCAnonStorey65.bonusIndex >= 0)
                {
                  // ISSUE: reference to a compiler-generated field
                  // ISSUE: reference to a compiler-generated method
                  // ISSUE: reference to a compiler-generated method
                  this.SetupToolTipEntry(this.mechanicBonusList.GetOrCreateItem<Transform>(dataCAnonStorey65.bonusIndex), new Action(dataCAnonStorey65.\u003C\u003Em__AB), new Action(dataCAnonStorey65.\u003C\u003Em__AC));
                }
                this.mRelationshipGained.minimumValue = mechanicOfDriver.CalculateCurrentRelationshipWithDriverForUI().relationshipAmount / 100f;
                if ((double) this.mRelationshipGained.minimumValue != (double) this.mRelationshipGained.initialValue)
                  return;
                this.mRelationshipGained.extraValue = 0.0f;
                this.mechanicRelationshipChange.text = "-";
                return;
              case 2:
                GameUtility.SetActive(this.moraleContainer, true);
                if ((double) this.mHappinessGained.preInterviewValue == 0.0)
                  this.mHappinessGained.preInterviewValue = (float) inPerson.GetHappiness() / 100f;
                this.preInterviewMorale.text = GameUtility.GetPercentageText(this.mHappinessGained.preInterviewValue, 1f, false, true);
                this.moraleHeader.text = Localisation.LocaliseID("PSG_10001550", (GameObject) null);
                this.currentMorale.text = GameUtility.GetPercentageText((float) inPerson.GetHappiness() / 100f, 1f, false, true);
                this.moraleChange.text = GameUtility.GetPercentageText(result / 100f, 1f, true, true);
                this.moraleChange.color = (double) result < 0.0 ? UIConstants.negativeColor : UIConstants.positiveColor;
                this.mHappinessGained.initialValue = (float) inPerson.GetHappiness() / 100f;
                this.mHappinessGained.extraValue = result / 100f;
                inPerson.ModifyHappiness(result, "PSG_10011066");
                this.mHappinessGained.minimumValue = (float) inPerson.GetHappiness() / 100f;
                if ((double) this.mHappinessGained.minimumValue != (double) this.mHappinessGained.initialValue)
                  return;
                this.mHappinessGained.extraValue = 0.0f;
                this.moraleChange.text = "-";
                return;
              case 3:
                GameUtility.SetActive(this.marketabilityContainer, true);
                if ((double) this.mMarketabilityGained.preInterviewValue == 0.0)
                  this.mMarketabilityGained.preInterviewValue = team.GetMarketability();
                this.preInterviewMarketability.text = GameUtility.GetPercentageText(this.mMarketabilityGained.preInterviewValue, 1f, false, true);
                this.currentMarketability.text = GameUtility.GetPercentageText(team.GetMarketability(), 1f, false, true);
                this.marketabilityChange.text = GameUtility.GetPercentageText(result / 100f, 1f, true, true);
                this.marketabilityChange.color = (double) result < 0.0 ? UIConstants.negativeColor : UIConstants.positiveColor;
                this.mMarketabilityGained.initialValue = team.GetMarketability();
                this.mMarketabilityGained.extraValue = result / 100f;
                team.AddToMarketebility(result / 100f);
                this.mMarketabilityGained.minimumValue = team.GetMarketability();
                if ((double) this.mMarketabilityGained.minimumValue != (double) this.mMarketabilityGained.initialValue)
                  return;
                this.mMarketabilityGained.extraValue = 0.0f;
                this.marketabilityChange.text = "-";
                return;
              case 4:
                if (inDriver == null)
                  return;
                GameUtility.SetActive(this.traitContainer, true);
                if (Game.instance.personalityTraitManager.personalityTraits.ContainsKey((int) result))
                {
                  // ISSUE: object of a compiler-generated type is created
                  // ISSUE: variable of a compiler-generated type
                  UIMediaInterviewAnswerOutcome.\u003CSetupUserData\u003Ec__AnonStorey66 dataCAnonStorey66 = new UIMediaInterviewAnswerOutcome.\u003CSetupUserData\u003Ec__AnonStorey66();
                  // ISSUE: reference to a compiler-generated field
                  dataCAnonStorey66.\u003C\u003Ef__this = this;
                  PersonalityTraitData personalityTrait = Game.instance.personalityTraitManager.personalityTraits[(int) result];
                  if (personalityTrait != null)
                    this.mTraitAdded.Add(inDriver.personalityTraitController.AddPersonalityTrait(personalityTrait, false));
                  // ISSUE: reference to a compiler-generated field
                  dataCAnonStorey66.traitIndex = this.mTraitAdded.Count - 1;
                  // ISSUE: reference to a compiler-generated field
                  // ISSUE: reference to a compiler-generated method
                  // ISSUE: reference to a compiler-generated method
                  this.SetupToolTipEntry(this.personalityTraitList.GetOrCreateItem<Transform>(dataCAnonStorey66.traitIndex), new Action(dataCAnonStorey66.\u003C\u003Em__AD), new Action(dataCAnonStorey66.\u003C\u003Em__AE));
                  return;
                }
                Debug.LogErrorFormat("Trait with ID: {0} , does not exist in the database.", (object) (int) result);
                return;
              case 5:
                if (inDriver == null)
                  return;
                RaceEventResults.ResultData resultForDriver = inDriver.contract.GetTeam().championship.GetPreviousEventDetails().results.GetResultsForSession(SessionDetails.SessionType.Race).GetResultForDriver(inDriver);
                bool flag5 = inDriver.IsAmbitiousDriver();
                bool flag6 = inDriver.IsSlackerDriver();
                bool flag7 = inDriver.IsUnpredictableDriver();
                bool flag8 = resultForDriver.position <= resultForDriver.expectedPosition;
                int num2 = Mathf.Abs(resultForDriver.expectedPosition - resultForDriver.position);
                float inMoraleChangeValue = 0.0f;
                float num3 = (float) (num2 * 2);
                string lower2 = inCriteria.mCriteriaInfo.ToLower();
                if (lower2 != null)
                {
                  // ISSUE: reference to a compiler-generated field
                  if (UIMediaInterviewAnswerOutcome.\u003C\u003Ef__switch\u0024map3A == null)
                  {
                    // ISSUE: reference to a compiler-generated field
                    UIMediaInterviewAnswerOutcome.\u003C\u003Ef__switch\u0024map3A = new Dictionary<string, int>(3)
                    {
                      {
                        "happy",
                        0
                      },
                      {
                        "average",
                        1
                      },
                      {
                        "unhappy",
                        2
                      }
                    };
                  }
                  int num4;
                  // ISSUE: reference to a compiler-generated field
                  if (UIMediaInterviewAnswerOutcome.\u003C\u003Ef__switch\u0024map3A.TryGetValue(lower2, out num4))
                  {
                    switch (num4)
                    {
                      case 0:
                        inMoraleChangeValue = !flag7 ? (!flag5 ? (!flag6 ? (!flag8 ? (float) (-(double) num3 * 0.5) : num3 * 0.5f) : (!flag8 ? num3 * 1.5f : num3)) : (!flag8 ? -num3 : num3)) : (float) RandomUtility.GetRandom(-10, 20);
                        break;
                      case 1:
                        inMoraleChangeValue = !flag7 ? (!flag5 ? (!flag6 ? (float) RandomUtility.GetRandom(-5, 5) : (!flag8 ? 0.0f : num3 * 0.5f)) : (!flag8 ? (float) (-(double) num3 * 0.5) : num3 * 0.5f)) : (float) RandomUtility.GetRandom(-15, 15);
                        break;
                      case 2:
                        inMoraleChangeValue = !flag7 ? (!flag5 ? (!flag6 ? (!flag8 ? (float) RandomUtility.GetRandom(-5, 5) : (float) RandomUtility.GetRandom(-10, -5)) : (!flag8 ? 0.0f : -num3)) : (!flag8 ? num3 * 2f : -num3)) : (float) RandomUtility.GetRandom(-20, 10);
                        break;
                    }
                  }
                }
                this.SetupMoraleChange((Person) inDriver, inMoraleChangeValue);
                return;
            }
          }
        }
        Debug.LogErrorFormat("{0} does not have a switch chase for the outcome", (object) strArray[1].Trim().ToLower());
      }
    }
    catch (Exception ex)
    {
      Debug.LogErrorFormat("Error with interview outcome {0} {1} \n{2}", (object) inCriteria.mType, (object) inCriteria.mCriteriaInfo, (object) ex.Message);
    }
  }

  private void SetupMoraleChange(Person inPerson, float inMoraleChangeValue)
  {
    GameUtility.SetActive(this.moraleContainer, true);
    if ((double) this.mMoraleGained.preInterviewValue == 0.0)
      this.mMoraleGained.preInterviewValue = inPerson.GetMorale();
    this.preInterviewMorale.text = GameUtility.GetPercentageText(this.mMoraleGained.preInterviewValue, 1f, false, true);
    this.moraleHeader.text = Localisation.LocaliseID("PSG_10001625", (GameObject) null);
    this.currentMorale.text = GameUtility.GetPercentageText(inPerson.GetMorale(), 1f, false, true);
    this.moraleChange.text = GameUtility.GetPercentageText(inMoraleChangeValue / 100f, 1f, true, true);
    this.moraleChange.color = (double) inMoraleChangeValue < 0.0 ? UIConstants.negativeColor : UIConstants.positiveColor;
    this.mMoraleGained.initialValue = inPerson.GetMorale();
    this.mMoraleGained.extraValue = inMoraleChangeValue / 100f;
    inPerson.ModifyMorale(inMoraleChangeValue / 100f, "PSG_10011066", false);
    this.mMoraleGained.minimumValue = inPerson.GetMorale();
    if ((double) this.mMoraleGained.minimumValue != (double) this.mMoraleGained.initialValue)
      return;
    this.mMoraleGained.extraValue = 0.0f;
    this.moraleChange.text = "-";
  }

  private void Update()
  {
    if ((double) this.mTimer <= 1.0)
    {
      this.mTimer += Time.deltaTime;
      if ((double) this.mMoraleGained.extraValue != 0.0)
      {
        this.currentMorale.text = GameUtility.GetPercentageText(this.mMoraleGained.GetLerpedInitialValue(this.mTimer), 1f, false, true);
        this.moraleChange.text = GameUtility.GetPercentageText(this.mMoraleGained.GetLerpedExtraValue(this.mTimer), 1f, true, true);
      }
      if ((double) this.mHappinessGained.extraValue != 0.0)
      {
        this.currentMorale.text = GameUtility.GetPercentageText(this.mHappinessGained.GetLerpedInitialValue(this.mTimer), 1f, false, true);
        this.moraleChange.text = GameUtility.GetPercentageText(this.mHappinessGained.GetLerpedExtraValue(this.mTimer), 1f, true, true);
      }
      if ((double) this.mMarketabilityGained.extraValue != 0.0)
      {
        this.currentMarketability.text = GameUtility.GetPercentageText(this.mMarketabilityGained.GetLerpedInitialValue(this.mTimer), 1f, false, true);
        this.marketabilityChange.text = GameUtility.GetPercentageText(this.mMarketabilityGained.GetLerpedExtraValue(this.mTimer), 1f, true, true);
      }
      if ((double) this.mRelationshipGained.extraValue == 0.0)
        return;
      this.currentMechanicRelationship.text = GameUtility.GetPercentageText(this.mRelationshipGained.GetLerpedInitialValue(this.mTimer), 1f, false, true);
      this.mechanicRelationshipChange.text = GameUtility.GetPercentageText(this.mRelationshipGained.GetLerpedExtraValue(this.mTimer), 1f, true, true);
    }
    else
    {
      this.moraleChange.text = string.Empty;
      this.marketabilityChange.text = string.Empty;
      this.mechanicRelationshipChange.text = string.Empty;
    }
  }

  private void SetupToolTipEntry(Transform inContainer, Action inMouseEnter, Action inMouseExit)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UIMediaInterviewAnswerOutcome.\u003CSetupToolTipEntry\u003Ec__AnonStorey67 entryCAnonStorey67 = new UIMediaInterviewAnswerOutcome.\u003CSetupToolTipEntry\u003Ec__AnonStorey67();
    // ISSUE: reference to a compiler-generated field
    entryCAnonStorey67.inMouseEnter = inMouseEnter;
    // ISSUE: reference to a compiler-generated field
    entryCAnonStorey67.inMouseExit = inMouseExit;
    GameUtility.SetActive(inContainer.gameObject, true);
    Button componentInChildren = inContainer.gameObject.GetComponentInChildren<Button>();
    EventTrigger eventTrigger = componentInChildren.GetComponent<EventTrigger>();
    if ((UnityEngine.Object) eventTrigger == (UnityEngine.Object) null)
      eventTrigger = componentInChildren.gameObject.AddComponent<EventTrigger>();
    EventTrigger.Entry entry1 = new EventTrigger.Entry();
    entry1.eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter;
    // ISSUE: reference to a compiler-generated method
    entry1.callback.AddListener(new UnityAction<BaseEventData>(entryCAnonStorey67.\u003C\u003Em__AF));
    eventTrigger.get_triggers().Add(entry1);
    EventTrigger.Entry entry2 = new EventTrigger.Entry();
    entry2.eventID = UnityEngine.EventSystems.EventTriggerType.PointerExit;
    // ISSUE: reference to a compiler-generated method
    entry2.callback.AddListener(new UnityAction<BaseEventData>(entryCAnonStorey67.\u003C\u003Em__B0));
    eventTrigger.get_triggers().Add(entry2);
  }

  private void Reset()
  {
    this.mTraitAdded.Clear();
    this.mMechanicBonusUnlocked.Clear();
    this.personalityTraitList.HideListItems();
    this.mechanicBonusList.HideListItems();
    GameUtility.SetActive(this.traitContainer, false);
    GameUtility.SetActive(this.moraleContainer, false);
    GameUtility.SetActive(this.mechanicRelationshipContainer, false);
    GameUtility.SetActive(this.marketabilityContainer, false);
  }

  private void SetTargetData(object inObject)
  {
    Person mTarget1 = this.mTarget as Person;
    Team mTarget2 = this.mTarget as Team;
    GameUtility.SetActive(this.personDataContainer, mTarget1 != null);
    GameUtility.SetActive(this.teamLogoContainer, mTarget2 != null);
    if (mTarget1 != null)
    {
      Driver inDriver = mTarget1 as Driver;
      GameUtility.SetActive(this.driverPortrait.gameObject, inDriver != null);
      GameUtility.SetActive(this.staffPortrait.gameObject, inDriver == null);
      this.flag.SetNationality(mTarget1.nationality);
      this.personName.text = mTarget1.name;
      GameUtility.SetActive(this.abilityStars.gameObject, false);
      if (inDriver != null)
      {
        this.driverPortrait.SetPortrait((Person) inDriver);
        this.abilityStars.SetAbilityStarsData(inDriver);
        this.personJobTitle.text = Localisation.LocaliseEnum((Enum) mTarget1.contract.proposedStatus) + " " + Localisation.LocaliseEnum((Enum) mTarget1.contract.job);
      }
      else
      {
        this.staffPortrait.SetPortrait(mTarget1);
        this.abilityStars.SetAbilityStarsData(mTarget1);
        this.personJobTitle.text = Localisation.LocaliseEnum((Enum) mTarget1.contract.job);
      }
    }
    else
    {
      if (mTarget2 == null)
        return;
      this.teamLogo.SetTeam(mTarget2);
    }
  }

  public void OpenMechanicBonusToolTip(int inID)
  {
    UIManager.instance.dialogBoxManager.GetDialog<UIDialogBoxMechanicBonusTooltip>().ShowTooltip(true, this.mMechanicBonusUnlocked[inID]);
  }

  public void CloseMechanicBonusToolTip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<UIDialogBoxMechanicBonusTooltip>().HideTooltip();
  }

  public void OpenTraitRollover(int inID)
  {
    UIManager.instance.dialogBoxManager.GetDialog<DriverTraitsRollover>().ShowRollover(this.mTraitAdded[inID]);
  }

  public void CloseTraitRollover()
  {
    UIManager.instance.dialogBoxManager.GetDialog<DriverTraitsRollover>().Hide();
  }

  private struct FloatData
  {
    public float preInterviewValue;
    public float minimumValue;
    public float initialValue;
    public float extraValue;

    public float GetLerpedInitialValue(float inTime)
    {
      float num = Mathf.Lerp(this.initialValue, this.initialValue + this.extraValue, inTime);
      if ((double) this.extraValue < 0.0)
        return Mathf.Clamp(num, this.minimumValue, 1f);
      return Mathf.Clamp01(num);
    }

    public float GetLerpedExtraValue(float inTime)
    {
      return Mathf.Lerp(this.extraValue, 0.0f, inTime);
    }

    public void ResetValues(bool inPartial)
    {
      if (!inPartial)
        this.preInterviewValue = 0.0f;
      this.minimumValue = 0.0f;
      this.minimumValue = 0.0f;
      this.extraValue = 0.0f;
    }
  }
}
