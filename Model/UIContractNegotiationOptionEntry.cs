// Decompiled with JetBrains decompiler
// Type: UIContractNegotiationOptionEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIContractNegotiationOptionEntry : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI contractTerm;
  [SerializeField]
  private TextMeshProUGUI offer;
  [SerializeField]
  private TextMeshProUGUI subTextOffer;
  [SerializeField]
  private TextMeshProUGUI existingContract;
  [SerializeField]
  private TextMeshProUGUI subTextExistingContract;
  [SerializeField]
  private TextMeshProUGUI reactionText;
  [SerializeField]
  private Image reactionSmiley;

  public void Setup(UIContractOptionEntry.OptionType inOptionType, ContractManagerPerson inContractManager)
  {
    GameUtility.SetActive(this.gameObject, inContractManager != null && inContractManager.draftProposalContract != null);
    if (inContractManager == null || inContractManager.draftProposalContract == null)
      return;
    this.contractTerm.text = Localisation.LocaliseEnum((Enum) inOptionType);
    if (inContractManager.isConsideringProposal)
    {
      this.SetupContractTerm(this.offer, this.subTextOffer, inOptionType, inContractManager.draftProposalContract);
      this.SetThinkingSmileyReaction();
    }
    else if (inContractManager.noContractProposed)
    {
      GameUtility.SetActive(this.reactionSmiley.gameObject, false);
      GameUtility.SetActive(this.reactionText.gameObject, false);
      GameUtility.SetActive(this.reactionText.gameObject, false);
      this.offer.text = "-";
      GameUtility.SetActive(this.subTextOffer.gameObject, false);
    }
    else
    {
      this.SetupContractTerm(this.offer, this.subTextOffer, inOptionType, inContractManager.draftProposalContract);
      this.SetReaction(inOptionType, inContractManager);
    }
    if (inOptionType != UIContractOptionEntry.OptionType.QualifyingBonus)
      return;
    Team team = inContractManager.draftProposalContract.GetTeam();
    GameUtility.SetActive(this.gameObject, !(team is NullTeam) && team.championship.rules.qualifyingBasedActive);
  }

  public void SetupWithDraftContract(UIContractOptionEntry inOptionEntry, ContractPerson inDraftContractPerson, ContractManagerPerson inContractManager)
  {
    this.contractTerm.text = Localisation.LocaliseEnum((Enum) inOptionEntry.optionType);
    this.UpdateContractTerm(inOptionEntry, inDraftContractPerson);
    if (inContractManager.noContractProposed)
    {
      GameUtility.SetActive(this.reactionSmiley.gameObject, false);
      GameUtility.SetActive(this.reactionText.gameObject, false);
      GameUtility.SetActive(this.reactionText.gameObject, false);
    }
    else if (inContractManager.isConsideringProposal)
      this.SetThinkingSmileyReaction();
    else
      this.SetReaction(inOptionEntry.optionType, inContractManager);
    if (inOptionEntry.optionType != UIContractOptionEntry.OptionType.QualifyingBonus)
      return;
    Team team = inDraftContractPerson.GetTeam();
    GameUtility.SetActive(this.gameObject, !(team is NullTeam) && team.championship.rules.qualifyingBasedActive);
  }

  private void SetThinkingSmileyReaction()
  {
    GameUtility.SetActive(this.reactionSmiley.gameObject, true);
    GameUtility.SetActive(this.reactionText.gameObject, true);
    GameUtility.SetActive(this.reactionText.gameObject, true);
    this.reactionSmiley.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-ThinkingSmiley");
    this.reactionText.text = "?";
    this.reactionText.color = Color.grey;
  }

  public void UpdateContractTerm(UIContractOptionEntry inOptionEntry, ContractPerson inDraftContractPerson)
  {
    if (inOptionEntry.activeOption.AreSettingsChosen())
    {
      this.SetupContractTerm(this.offer, this.subTextOffer, inOptionEntry.optionType, inDraftContractPerson);
    }
    else
    {
      this.offer.text = "-";
      GameUtility.SetActive(this.subTextOffer.gameObject, false);
    }
  }

  private void SetupContractTerm(TextMeshProUGUI inText, TextMeshProUGUI inSubText, UIContractOptionEntry.OptionType inOptionType, ContractPerson inContractPerson)
  {
    GameUtility.SetActive(inSubText.gameObject, false);
    switch (inOptionType)
    {
      case UIContractOptionEntry.OptionType.Status:
        inText.text = inContractPerson.GetStatusText();
        break;
      case UIContractOptionEntry.OptionType.WagesPerRace:
        long inValue = GameUtility.RoundCurrency(inContractPerson.perRaceCost);
        inText.text = GameUtility.GetCurrencyString(inValue, 0);
        break;
      case UIContractOptionEntry.OptionType.ContractLength:
        int monthsRemaining = inContractPerson.GetMonthsRemaining();
        StringVariableParser.ordinalNumberString = monthsRemaining.ToString();
        inText.text = Localisation.LocaliseID(monthsRemaining != 1 ? "PSG_10010608" : "PSG_10010609", (GameObject) null);
        break;
      case UIContractOptionEntry.OptionType.SignOnFee:
        inText.text = GameUtility.GetCurrencyString((long) inContractPerson.signOnFee, 0);
        break;
      case UIContractOptionEntry.OptionType.QualifyingBonus:
        inText.text = GameUtility.GetCurrencyString((long) inContractPerson.qualifyingBonus, 0);
        GameUtility.SetActive(inSubText.gameObject, true);
        if (inContractPerson.hasQualifyingBonus)
        {
          inSubText.text = GameUtility.FormatForPositionOrAbove(inContractPerson.qualifyingBonusTargetPosition, (string) null);
          break;
        }
        inSubText.text = "-";
        break;
      case UIContractOptionEntry.OptionType.RaceBonus:
        inText.text = GameUtility.GetCurrencyString((long) inContractPerson.raceBonus, 0);
        GameUtility.SetActive(inSubText.gameObject, true);
        if (inContractPerson.hasRaceBonus)
        {
          inSubText.text = GameUtility.FormatForPositionOrAbove(inContractPerson.raceBonusTargetPosition, (string) null);
          break;
        }
        inSubText.text = "-";
        break;
      case UIContractOptionEntry.OptionType.BuyOutClause:
        inText.text = GameUtility.GetCurrencyString((long) inContractPerson.amountForContractorToPay, 0);
        break;
    }
  }

  public void SetupCurrentContract(UIContractOptionEntry.OptionType inOptionType, ContractPerson inContractPerson)
  {
    if ((UnityEngine.Object) this.existingContract == (UnityEngine.Object) null)
      return;
    if (inContractPerson.job == Contract.Job.Unemployed)
    {
      this.existingContract.text = Localisation.LocaliseEnum((Enum) Contract.Job.Unemployed);
      GameUtility.SetActive(this.subTextExistingContract.gameObject, false);
    }
    else if (inContractPerson.person is Driver && !((Driver) inContractPerson.person).CanShowStats())
    {
      this.existingContract.text = Localisation.LocaliseID("PSG_10010341", (GameObject) null);
      GameUtility.SetActive(this.subTextExistingContract.gameObject, false);
    }
    else
      this.SetupContractTerm(this.existingContract, this.subTextExistingContract, inOptionType, inContractPerson);
  }

  private void SetReaction(UIContractOptionEntry.OptionType inOptionType, ContractManagerPerson inContractManager)
  {
    GameUtility.SetActive(this.reactionSmiley.gameObject, true);
    GameUtility.SetActive(this.reactionText.gameObject, true);
    GameUtility.SetActive(this.reactionText.gameObject, true);
    ContractEvaluationPerson.ReactionType reactionType = ContractEvaluationPerson.ReactionType.Insulted;
    switch (inOptionType)
    {
      case UIContractOptionEntry.OptionType.Status:
        reactionType = inContractManager.contractEvaluation.GetContractStatusReaction();
        break;
      case UIContractOptionEntry.OptionType.WagesPerRace:
        reactionType = inContractManager.contractEvaluation.GetContractWageReaction();
        break;
      case UIContractOptionEntry.OptionType.ContractLength:
        reactionType = inContractManager.contractEvaluation.GetContractLengthReaction();
        break;
      case UIContractOptionEntry.OptionType.SignOnFee:
        reactionType = inContractManager.contractEvaluation.GetContractSignOnFeeReaction();
        break;
      case UIContractOptionEntry.OptionType.QualifyingBonus:
        reactionType = inContractManager.contractEvaluation.GetContractQualifyingBonusReaction();
        break;
      case UIContractOptionEntry.OptionType.RaceBonus:
        reactionType = inContractManager.contractEvaluation.GetContractRaceBonusReaction();
        break;
      case UIContractOptionEntry.OptionType.BuyOutClause:
        reactionType = inContractManager.contractEvaluation.GetContractBuyOutClauseReaction();
        break;
    }
    string empty = string.Empty;
    Color color1 = new Color();
    Sprite sprite;
    Color color2;
    string str;
    if (reactionType == ContractEvaluationPerson.ReactionType.Delighted)
    {
      sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-HappySmiley");
      color2 = UIConstants.colorBandGreen;
      str = Localisation.LocaliseID("PSG_10010104", (GameObject) null);
    }
    else if (reactionType == ContractEvaluationPerson.ReactionType.Neutral)
    {
      sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-AverageSmiley");
      color2 = UIConstants.colorBandYellow;
      str = Localisation.LocaliseID("PSG_10010146", (GameObject) null);
    }
    else if (reactionType == ContractEvaluationPerson.ReactionType.UnHappy)
    {
      sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-UnhappySmiley");
      color2 = UIConstants.colorBandRed;
      str = Localisation.LocaliseID("PSG_10001551", (GameObject) null);
    }
    else
    {
      sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-AngrySmiley");
      color2 = UIConstants.colorBandRed;
      str = Localisation.LocaliseID("PSG_10010103", (GameObject) null);
    }
    this.reactionSmiley.sprite = sprite;
    this.reactionText.text = str;
    this.reactionText.color = color2;
  }
}
