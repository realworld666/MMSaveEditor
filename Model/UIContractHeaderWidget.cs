// Decompiled with JetBrains decompiler
// Type: UIContractHeaderWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIContractHeaderWidget : MonoBehaviour
{
  public List<Toggle> strikes = new List<Toggle>();
  private float mRefreshFrequency = 1f;
  private float mCurrentRefresh = 1f;
  public UICharacterPortrait targetPortrait;
  public UICharacterPortrait targetPortraitStaff;
  public TextMeshProUGUI targetName;
  public UITeamLogo targetTeamLogo;
  public TextMeshProUGUI targetTeamName;
  public Flag targetNationality;
  public TextMeshProUGUI targetAge;
  public TextMeshProUGUI dealCostLabel;
  public UIAbilityStars abilityStars;
  public UIGridList contractOptionEntries;
  public GameObject lastChanceObject;
  private ContractNegotiationScreen.NegotatiationType mType;
  private ContractNegotiationScreen mContractNegotiationScreen;
  private Person mTarget;
  private ContractPerson mDraftContractPerson;

  private void Start()
  {
  }

  public void Setup(Person inTarget, ContractPerson inDraftContract, ContractNegotiationScreen.NegotatiationType inType, ContractManagerPerson.ContractProposalState inProposalState)
  {
    this.mType = inType;
    this.mTarget = inTarget;
    this.mDraftContractPerson = inDraftContract;
    if ((Object) this.mContractNegotiationScreen == (Object) null)
      this.mContractNegotiationScreen = UIManager.instance.GetScreen<ContractNegotiationScreen>();
    if (this.mType == ContractNegotiationScreen.NegotatiationType.NewStaffEmployed || this.mType == ContractNegotiationScreen.NegotatiationType.NewStaffUnemployed || this.mType == ContractNegotiationScreen.NegotatiationType.RenewStaff)
    {
      GameUtility.SetActive(this.targetPortrait.gameObject, false);
      GameUtility.SetActive(this.targetPortraitStaff.gameObject, true);
      this.targetPortraitStaff.SetPortrait(this.mTarget);
      this.abilityStars.SetAbilityStarsData(this.mTarget);
    }
    else
    {
      GameUtility.SetActive(this.targetPortrait.gameObject, true);
      GameUtility.SetActive(this.targetPortraitStaff.gameObject, false);
      this.targetPortrait.SetPortrait(this.mTarget);
      if (this.mTarget is Driver)
        this.abilityStars.SetAbilityStarsData(this.mTarget as Driver);
    }
    this.SetOldContractDetails();
    this.targetName.text = this.mTarget.name;
    StringVariableParser.intValue1 = this.mTarget.GetAge();
    this.targetAge.text = Localisation.LocaliseID("PSG_10010748", (GameObject) null);
    this.targetNationality.SetNationality(this.mTarget.nationality);
    this.SetupPatience();
    this.SetStrikes(this.mTarget);
    this.UpdateStrikes(this.mTarget);
    GameUtility.SetActive(this.lastChanceObject, this.mTarget.contractManager.isLastChance);
    this.PopulateNegotationOptionEntries(this.mContractNegotiationScreen.optionsWidget.contractOptions);
    this.SetupNegotiationOptionEntries();
  }

  private void SetOldContractDetails()
  {
    this.targetTeamLogo.SetTeam(this.mTarget.contract.GetTeam());
    this.targetTeamName.text = this.mTarget.IsFreeAgent() ? string.Empty : this.mTarget.contract.GetTeam().name;
  }

  private void SetStrikes(Person inPerson)
  {
    for (int index = 0; index < this.strikes.Count; ++index)
    {
      if (index < inPerson.contractManager.contractPatienceAvailable)
      {
        this.strikes[index].gameObject.SetActive(true);
        this.strikes[index].isOn = false;
      }
      else
      {
        this.strikes[index].gameObject.SetActive(false);
        this.strikes[index].isOn = false;
      }
    }
  }

  private void UpdateStrikes(Person inPerson)
  {
    for (int index = 0; index < this.strikes.Count; ++index)
      this.strikes[index].isOn = index < inPerson.contractManager.contractPatienceUsed;
  }

  private void Update()
  {
    this.mCurrentRefresh -= GameTimer.deltaTime;
    if ((double) this.mCurrentRefresh < 0.0)
    {
      this.UpdateOptionEntriesTerms();
      this.mCurrentRefresh = this.mRefreshFrequency;
    }
    this.SetCostLabel();
  }

  public void ActivateDoneDealGFX(bool inBool)
  {
  }

  public void SetCostLabel()
  {
    UIContractOptionEntry contractOptionEntry = (UIContractOptionEntry) null;
    for (int index = 0; index < this.mContractNegotiationScreen.optionsWidget.contractOptions.Count; ++index)
    {
      if (this.mContractNegotiationScreen.optionsWidget.contractOptions[index].optionType == UIContractOptionEntry.OptionType.ContractLength)
      {
        contractOptionEntry = this.mContractNegotiationScreen.optionsWidget.contractOptions[index];
        break;
      }
    }
    long inValue = 0;
    if ((Object) contractOptionEntry != (Object) null)
    {
      if (contractOptionEntry.activeOption.AreSettingsChosen())
      {
        long contractForThisYear = this.mDraftContractPerson.GetTotalCostToContractForThisYear();
        inValue = contractForThisYear >= 0L ? contractForThisYear : 0L;
      }
      else
        inValue = 0L;
    }
    this.dealCostLabel.text = GameUtility.GetCurrencyString(inValue, 0);
  }

  private void PopulateNegotationOptionEntries(List<UIContractOptionEntry> inOptionEntries)
  {
    this.contractOptionEntries.DestroyListItems();
    this.contractOptionEntries.itemPrefab.SetActive(true);
    for (int index = 0; index < inOptionEntries.Count; ++index)
      this.contractOptionEntries.CreateListItem<UIContractNegotiationOptionEntry>();
    this.contractOptionEntries.itemPrefab.SetActive(false);
  }

  public void SetupNegotiationOptionEntries()
  {
    List<UIContractOptionEntry> contractOptions = this.mContractNegotiationScreen.optionsWidget.contractOptions;
    for (int inIndex = 0; inIndex < this.contractOptionEntries.itemCount; ++inIndex)
    {
      UIContractNegotiationOptionEntry negotiationOptionEntry = this.contractOptionEntries.GetItem<UIContractNegotiationOptionEntry>(inIndex);
      negotiationOptionEntry.SetupWithDraftContract(contractOptions[inIndex], this.mDraftContractPerson, this.mDraftContractPerson.person.contractManager);
      negotiationOptionEntry.SetupCurrentContract(contractOptions[inIndex].optionType, this.mTarget.contract);
    }
  }

  public void UpdateOptionEntriesTerms()
  {
    List<UIContractOptionEntry> contractOptions = this.mContractNegotiationScreen.optionsWidget.contractOptions;
    for (int inIndex = 0; inIndex < this.contractOptionEntries.itemCount; ++inIndex)
      this.contractOptionEntries.GetItem<UIContractNegotiationOptionEntry>(inIndex).UpdateContractTerm(contractOptions[inIndex], this.mDraftContractPerson);
  }

  private void SetupPatience()
  {
  }
}
