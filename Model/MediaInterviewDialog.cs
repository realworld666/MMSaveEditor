// Decompiled with JetBrains decompiler
// Type: MediaInterviewDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MediaInterviewDialog : UIDialogBox
{
  public float questionAnimationDuration = 1f;
  public List<UIMediaOutletColor> mediaColours = new List<UIMediaOutletColor>();
  public List<UIMediaAnswer> answers = new List<UIMediaAnswer>();
  private List<object> mAnswerTargets = new List<object>();
  public Button endInterviewButton;
  public GameObject answersContainer;
  public Animator panelAnimator;
  public UIPressLogo logo;
  public UICharacterPortrait journalistPortrait;
  public Flag journalistNationalityFlag;
  public TextMeshProUGUI mediaOutletName;
  public TextMeshProUGUI questionNumber;
  public TextMeshProUGUI journalistName;
  public TextMeshProUGUI currentQuestion;
  public UIGridList pipList;
  public UIGridList outcomesList;
  private Person mJournalist;
  private DialogRule mNextStep;
  private DialogRule mCurrentStep;
  private int mQuestionIndex;
  private bool mAnimateQuestion;
  private bool mSkipToNextStep;
  private float mSkipTimer;
  private float mVisibleCharacters;
  private InterviewData mInterviewQueryData;

  public Person journalist
  {
    get
    {
      return this.mJournalist;
    }
  }

  protected override void Awake()
  {
    base.Awake();
    this.endInterviewButton.onClick.AddListener(new UnityAction(this.OnEndInterviewButton));
    this.pipList.OnStart();
  }

  private void OnEndInterviewButton()
  {
    this.Hide();
  }

  public void Setup(DialogRule inStartingQuestion, Person inInterviewer)
  {
    StringVariableParser.SetStaticData((Person) null);
    this.outcomesList.HideListItems();
    this.mAnswerTargets.Clear();
    this.mInterviewQueryData = new InterviewData();
    this.mInterviewQueryData.GatherData();
    this.mSkipToNextStep = false;
    this.mNextStep = inStartingQuestion;
    this.mQuestionIndex = 0;
    this.mSkipTimer = 0.0f;
    Person inPerson = inInterviewer;
    this.mediaOutletName.text = inPerson.contract.GetMediaOutlet().name;
    this.mJournalist = inPerson;
    this.journalistPortrait.SetPortrait(inPerson);
    this.journalistNationalityFlag.SetNationality(inPerson.nationality);
    this.journalistName.text = inPerson.name;
    this.logo.SetMediaOutlet(inPerson.contract.GetMediaOutlet());
    this.GoToNextStep();
    for (int index = 0; index < this.mediaColours.Count; ++index)
      this.mediaColours[index].SetColor(inPerson.contract.GetMediaOutlet());
  }

  public void PlayButtonAnimations(UIMediaAnswer inChosenAnswer)
  {
    for (int index = 0; index < this.answers.Count; ++index)
    {
      UIMediaAnswer answer = this.answers[index];
      if (answer.gameObject.activeSelf)
      {
        if ((Object) answer == (Object) inChosenAnswer)
          answer.animator.SetTrigger(AnimationHashes.Accepted);
        else
          answer.animator.SetTrigger(AnimationHashes.Rejected);
      }
    }
  }

  public void SetNextStep(DialogRule inCurrentStep)
  {
    this.mJournalist.dialogQuery.AddNewMemory(inCurrentStep);
    this.mNextStep = inCurrentStep;
    this.panelAnimator.Play(AnimationHashes.PanelOutro);
  }

  private void GoToNextStep()
  {
    this.currentQuestion.maxVisibleCharacters = 0;
    this.mVisibleCharacters = 0.0f;
    this.panelAnimator.Play(AnimationHashes.PanelIntro);
    this.mAnimateQuestion = true;
    DialogRule outNextStep = (DialogRule) null;
    List<DialogRule> outNextStepAnswers = (List<DialogRule>) null;
    this.GetNextStep(this.mNextStep, out outNextStep, out outNextStepAnswers);
    bool inIsActive = this.IsInterviewOver(outNextStep);
    this.mCurrentStep = outNextStep;
    if (outNextStep != null)
    {
      ++this.mQuestionIndex;
      GameUtility.SetActive(this.endInterviewButton.gameObject, inIsActive);
      GameUtility.SetActive(this.answersContainer, !inIsActive);
      if (!inIsActive)
      {
        this.mJournalist.dialogQuery.AddNewMemory(outNextStep);
        this.SetupNextQuestion(outNextStepAnswers, outNextStep);
      }
      else
      {
        this.SetupInterviewEnd(outNextStep);
        if (outNextStep.triggerQuery != null && outNextStep.SendTriggerAsMailMessage())
          Game.instance.dialogSystem.SendMail(this.mJournalist, outNextStep.triggerQuery, false);
      }
    }
    else
    {
      this.OnEndInterviewButton();
      Debug.LogError((object) ("Interview ended unexpectadly at: " + Localisation.LocaliseID(outNextStep.localisationID, (GameObject) null)), (Object) null);
    }
    this.UpdateQuestionNumber();
  }

  private void UpdateQuestionNumber()
  {
    int b = this.mQuestionIndex - 1 + this.GetMaxInterviewDepth(this.mCurrentStep) - 1;
    if (this.mQuestionIndex == b + 1)
    {
      this.questionNumber.text = Localisation.LocaliseID("PSG_10008896", (GameObject) null);
    }
    else
    {
      StringVariableParser.intValue1 = this.mQuestionIndex;
      StringVariableParser.intValue2 = b;
      this.questionNumber.text = Localisation.LocaliseID("PSG_10011051", (GameObject) null);
    }
    for (int inIndex = 0; inIndex < Mathf.Max(this.pipList.itemCount, b); ++inIndex)
    {
      UIMediaProgressPip mediaProgressPip = this.pipList.GetOrCreateItem<UIMediaProgressPip>(inIndex);
      if (inIndex + 1 == this.mQuestionIndex)
        mediaProgressPip.SetState(UIMediaProgressPip.State.Current);
      else if (inIndex + 1 < this.mQuestionIndex)
        mediaProgressPip.SetState(UIMediaProgressPip.State.Filled);
      else
        mediaProgressPip.SetState(UIMediaProgressPip.State.Empty);
      GameUtility.SetActive(mediaProgressPip.gameObject, inIndex < b);
    }
  }

  private void GetNextStep(DialogRule inCurrentStep, out DialogRule outNextStep, out List<DialogRule> outNextStepAnswers)
  {
    DialogRule dialogRule = (DialogRule) null;
    List<DialogRule> dialogRuleList = (List<DialogRule>) null;
    if (inCurrentStep.triggerQuery != null)
    {
      dialogRule = this.mJournalist.dialogQuery.ProcessQueryForInterview(inCurrentStep.triggerQuery, this.mInterviewQueryData);
      if (dialogRule != null && dialogRule.triggerQuery != null)
      {
        DialogQuery dialogQuery = new DialogQuery();
        DialogQuery inQuery = Game.instance.player.dialogQuery.AddMemoryAndInterviewData(this.mInterviewQueryData, dialogRule.triggerQuery);
        dialogRuleList = App.instance.dialogRulesManager.GetValidRulesForInterviewAnswers(inQuery);
      }
      else if (dialogRule == null && !this.mSkipToNextStep)
        Debug.LogErrorFormat("Could not find a rule for the next interview step. Criteria that did not get a result are: {0}", (object) string.Join(string.Empty, inCurrentStep.triggerQuery.GetCriteriaToStringArray()));
    }
    outNextStep = dialogRule;
    outNextStepAnswers = dialogRuleList;
  }

  private int GetMaxInterviewDepth(DialogRule inCurrentStep)
  {
    int num = 1;
    DialogRule outNextStep = (DialogRule) null;
    List<DialogRule> outNextStepAnswers = (List<DialogRule>) null;
    this.GetNextStep(inCurrentStep, out outNextStep, out outNextStepAnswers);
    if (outNextStepAnswers != null)
    {
      int a = 0;
      for (int index = 0; index < outNextStepAnswers.Count; ++index)
      {
        int maxInterviewDepth = this.GetMaxInterviewDepth(outNextStepAnswers[index]);
        a = Mathf.Max(a, maxInterviewDepth);
      }
      num += a;
    }
    else if (outNextStep != null)
      num += this.GetMaxInterviewDepth(outNextStep);
    return num;
  }

  private void SetupInterviewEnd(DialogRule inLastStatement)
  {
    this.SetupNextQuestion((List<DialogRule>) null, inLastStatement);
  }

  private void SetupNextQuestion(List<DialogRule> inAnswers, DialogRule inQuestion)
  {
    this.currentQuestion.text = Localisation.LocaliseID(inQuestion.localisationID, (GameObject) null);
    if (inAnswers == null)
    {
      this.mSkipToNextStep = true;
    }
    else
    {
      for (int index = 0; index < this.answers.Count; ++index)
      {
        bool inIsActive = inAnswers != null && index < inAnswers.Count;
        if (inIsActive)
          this.answers[index].Setup(inAnswers[index]);
        GameUtility.SetActive(this.answers[index].gameObject, inIsActive);
      }
    }
  }

  public void SetOutcomeTargets(DialogRule inRule, bool inSetupImpacts)
  {
    for (int inIndex = 0; inIndex < this.outcomesList.itemCount; ++inIndex)
      this.outcomesList.GetOrCreateItem<UIMediaInterviewAnswerOutcome>(inIndex).SetAlphaState(false);
    for (int index = 0; index < inRule.userData.Count; ++index)
    {
      object obj = StringVariableParser.GetObject(inRule.userData[index].mType.Split(':')[0].Trim());
      int inIndex;
      if (!this.mAnswerTargets.Contains(obj))
      {
        this.mAnswerTargets.Add(obj);
        inIndex = this.mAnswerTargets.Count - 1;
      }
      else
        inIndex = this.mAnswerTargets.IndexOf(obj);
      DialogQueryCreator.SetSubject(this.mCurrentStep);
      this.outcomesList.GetOrCreateItem<UIMediaInterviewAnswerOutcome>(inIndex).Setup(inRule.userData[index], inSetupImpacts);
    }
  }

  private void Update()
  {
    if (this.mSkipToNextStep)
    {
      this.mSkipTimer += GameTimer.deltaTime;
      if ((double) this.mSkipTimer >= 3.0)
      {
        this.mSkipToNextStep = false;
        this.mSkipTimer = 0.0f;
        if (!this.IsInterviewOver(this.mCurrentStep))
        {
          List<DialogRule> outNextStepAnswers = (List<DialogRule>) null;
          this.GetNextStep(this.mCurrentStep, out this.mNextStep, out outNextStepAnswers);
          this.SetNextStep(this.mNextStep);
        }
      }
    }
    AnimatorStateInfo animatorStateInfo = this.panelAnimator.GetCurrentAnimatorStateInfo(0);
    if (animatorStateInfo.shortNameHash == AnimationHashes.PanelOutro && (double) animatorStateInfo.normalizedTime >= 1.0)
      this.GoToNextStep();
    if (!this.mAnimateQuestion)
      return;
    this.mVisibleCharacters += GameTimer.deltaTime * (float) this.currentQuestion.text.Length;
    this.currentQuestion.maxVisibleCharacters = Mathf.RoundToInt(this.mVisibleCharacters);
    if (this.currentQuestion.maxVisibleCharacters <= this.currentQuestion.text.Length)
      return;
    this.mAnimateQuestion = false;
  }

  private float GetAtributeChange(string inValue)
  {
    string str = inValue[0].ToString();
    float num = float.Parse(inValue.Remove(0, 1));
    if (str == "-")
      num = -num;
    return num;
  }

  public bool IsInterviewOver(DialogRule inRule)
  {
    if (inRule == null)
      return true;
    for (int index = 0; index < inRule.userData.Count; ++index)
    {
      if (inRule.userData[index].mType == "InterviewEnd" && inRule.userData[index].mCriteriaInfo == "True")
        return true;
    }
    return false;
  }
}
