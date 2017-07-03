// Decompiled with JetBrains decompiler
// Type: UIContractOptionEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIContractOptionEntry : MonoBehaviour
{
  public UIContractOptionEntry.State state = UIContractOptionEntry.State.Active;
  public List<UIContractSettingsEntry> optionContents = new List<UIContractSettingsEntry>();
  private ContractEvaluationPerson.ReactionType mReactionType = ContractEvaluationPerson.ReactionType.Neutral;
  public Action<int, bool> GoToNextContractOption;
  public UIContractOptionEntry.OptionType optionType;
  public CanvasGroup canvasGroup;
  public CanvasGroup canvasGroupContent;
  public GameObject reactionObject;
  public GameObject highlightObject;
  public GameObject importanceObject;
  public Toggle turnOnToggle;
  public int ID;
  public bool hasProposed;
  public Image reactionEmoji;
  public TextMeshProUGUI reaction;
  public TextMeshProUGUI importanceLabel;
  private ContractManagerPerson.ContractProposalState mContractProposalState;
  private ContractPerson mDraftContract;
  private ContractPerson mTargetContract;

  public UIContractSettingsEntry activeOption
  {
    get
    {
      return this.optionContents[(int) this.optionType];
    }
  }

  private void Start()
  {
    if (!((UnityEngine.Object) this.turnOnToggle != (UnityEngine.Object) null))
      return;
    this.turnOnToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnTurnOnToggle));
  }

  private void SharedSetup(UIContractOptionEntry.OptionType inOptionType)
  {
    this.hasProposed = false;
    this.optionType = inOptionType;
  }

  public void Setup(UIContractOptionEntry.OptionType inOptionType, ContractPerson inDraftContract, ContractPerson inTargetContract, ContractManagerPerson.ContractProposalState inContractProposalState)
  {
    this.SharedSetup(inOptionType);
    this.mDraftContract = inDraftContract;
    this.mTargetContract = inTargetContract;
    this.mContractProposalState = inContractProposalState;
    if (inOptionType == UIContractOptionEntry.OptionType.QualifyingBonus)
      GameUtility.SetActive(this.gameObject, inDraftContract.GetTeam().championship.rules.qualifyingBasedActive);
    else
      GameUtility.SetActive(this.gameObject, true);
    for (int index = 0; index < this.optionContents.Count; ++index)
    {
      if ((UIContractOptionEntry.OptionType) index == this.optionType)
        GameUtility.SetActive(this.optionContents[index].gameObject, true);
      else
        GameUtility.SetActive(this.optionContents[index].gameObject, false);
    }
    this.optionContents[(int) this.optionType].Setup((Contract) this.mTargetContract, (Contract) this.mDraftContract);
    if (!((UnityEngine.Object) this.turnOnToggle != (UnityEngine.Object) null))
      return;
    this.SetupTurnOnToggle();
  }

  private void SetupTurnOnToggle()
  {
    bool flag = false;
    switch (this.optionType)
    {
      case UIContractOptionEntry.OptionType.SignOnFee:
        flag = this.mDraftContract.signOnFee > 0;
        break;
      case UIContractOptionEntry.OptionType.QualifyingBonus:
        flag = this.mDraftContract.qualifyingBonus > 0;
        break;
      case UIContractOptionEntry.OptionType.RaceBonus:
        flag = this.mDraftContract.raceBonus > 0;
        break;
    }
    CanvasGroup component = this.activeOption.GetComponent<CanvasGroup>();
    component.alpha = !flag ? 0.2f : 1f;
    component.interactable = flag;
    this.turnOnToggle.isOn = flag;
  }

  public void SetState(UIContractOptionEntry.State inState)
  {
    this.state = inState;
    this.StateChangeInitializations();
  }

  private void OnAdjustButton()
  {
    if (this.GoToNextContractOption == null)
      return;
    this.GoToNextContractOption(this.ID, false);
  }

  private void OnConfirmButton()
  {
    this.hasProposed = true;
    this.SetState(UIContractOptionEntry.State.Active);
    if (this.GoToNextContractOption == null)
      return;
    this.GoToNextContractOption(this.ID + 1, true);
  }

  private void OnRevertButton()
  {
    this.optionContents[(int) this.optionType].Revert();
  }

  private void OnTurnOnToggle(bool inToggle)
  {
    CanvasGroup component = this.activeOption.GetComponent<CanvasGroup>();
    component.alpha = !inToggle ? 0.2f : 1f;
    component.interactable = inToggle;
    if (!inToggle)
    {
      this.activeOption.Reset(this.mDraftContract);
      this.activeOption.UpdateContractInfo((Contract) this.mDraftContract);
    }
    else
    {
      this.activeOption.SetDefaultValue(this.mDraftContract);
      this.activeOption.UpdateContractInfo((Contract) this.mDraftContract);
    }
  }

  private void StateChangeInitializations()
  {
    switch (this.state)
    {
      case UIContractOptionEntry.State.Considering:
        this.hasProposed = true;
        this.canvasGroup.interactable = false;
        this.canvasGroupContent.interactable = false;
        this.canvasGroup.alpha = 0.5f;
        this.canvasGroupContent.alpha = 0.5f;
        this.highlightObject.SetActive(false);
        this.reactionEmoji.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-ThinkingSmiley");
        this.reaction.text = "?";
        this.reaction.color = Color.grey;
        GameUtility.SetActive(this.reactionObject, true);
        GameUtility.SetActive(this.importanceObject, true);
        break;
      case UIContractOptionEntry.State.Active:
        this.activeOption.SetPrevious();
        this.canvasGroup.interactable = true;
        this.canvasGroupContent.interactable = true;
        this.highlightObject.SetActive(false);
        this.canvasGroup.alpha = 1f;
        this.canvasGroupContent.alpha = 1f;
        GameUtility.SetActive(this.reactionObject, false);
        GameUtility.SetActive(this.importanceObject, true);
        break;
      case UIContractOptionEntry.State.Rejected:
      case UIContractOptionEntry.State.Accepted:
        this.hasProposed = true;
        this.canvasGroup.interactable = this.mContractProposalState != ContractManagerPerson.ContractProposalState.ProposalAccepted;
        this.canvasGroupContent.interactable = this.mContractProposalState != ContractManagerPerson.ContractProposalState.ProposalAccepted;
        this.canvasGroup.alpha = 1f;
        this.canvasGroupContent.alpha = 1f;
        this.highlightObject.SetActive(false);
        this.mReactionType = this.activeOption.GetScore((Contract) this.mDraftContract);
        this.GetReaction(this.mReactionType);
        this.activeOption.SetPrevious();
        GameUtility.SetActive(this.reactionObject, true);
        GameUtility.SetActive(this.importanceObject, true);
        break;
    }
    this.activeOption.SetupImportanceLabel(this.mDraftContract, this.importanceLabel);
  }

  public bool SettingsChangedAndProposed()
  {
    if (this.optionContents[(int) this.optionType].HaveTheSettingsChanged())
      return this.hasProposed;
    return false;
  }

  private void GetReaction(ContractEvaluationPerson.ReactionType inValue)
  {
    string empty = string.Empty;
    Color color1 = new Color();
    Sprite sprite;
    Color color2;
    string str;
    if (inValue == ContractEvaluationPerson.ReactionType.Delighted)
    {
      sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-HappySmileyLarge");
      color2 = UIConstants.colorBandGreen;
      str = "DELIGHTED";
    }
    else if (inValue == ContractEvaluationPerson.ReactionType.Neutral)
    {
      sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-AverageSmileyLarge");
      color2 = UIConstants.colorBandYellow;
      str = "NEUTRAL";
    }
    else if (inValue == ContractEvaluationPerson.ReactionType.UnHappy)
    {
      sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-UnhappySmileyLarge");
      color2 = UIConstants.colorBandRed;
      str = "UNHAPPY";
    }
    else
    {
      sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-AngrySmileyLarge");
      color2 = UIConstants.colorBandRed;
      str = "INSULTED";
    }
    this.reactionEmoji.sprite = sprite;
    this.reaction.text = str;
    this.reaction.color = color2;
  }

  public void OnReactionMouseEnter()
  {
    if (this.state == UIContractOptionEntry.State.Considering)
      return;
    UIManager.instance.dialogBoxManager.GetDialog<ContractReactionRollover>().ShowRollover(this.mReactionType);
  }

  public void OnReactionMouseExit()
  {
    UIManager.instance.dialogBoxManager.GetDialog<ContractReactionRollover>().Hide();
  }

  public enum State
  {
    [LocalisationID("PSG_10005745")] Considering,
    [LocalisationID("PSG_10005746")] Active,
    [LocalisationID("PSG_10005747")] Rejected,
    [LocalisationID("PSG_10005748")] Accepted,
  }

  public enum OptionType
  {
    [LocalisationID("PSG_10005749")] Status,
    [LocalisationID("PSG_10006844")] WagesPerRace,
    [LocalisationID("PSG_10005751")] ContractLength,
    [LocalisationID("PSG_10005752")] SignOnFee,
    [LocalisationID("PSG_10005753")] QualifyingBonus,
    [LocalisationID("PSG_10005754")] RaceBonus,
    [LocalisationID("PSG_10005755")] BuyOutClause,
    Count,
  }
}
