// Decompiled with JetBrains decompiler
// Type: UIContractOptionsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIContractOptionsWidget : MonoBehaviour
{
  public List<UIContractOptionEntry> newContractOptions = new List<UIContractOptionEntry>();
  public List<UIContractOptionEntry> renewContractOptions = new List<UIContractOptionEntry>();
  public List<UIContractOptionEntry> newDriverUnemployedContractOptions = new List<UIContractOptionEntry>();
  public List<UIContractOptionEntry> staffContractOptionsBreakClause = new List<UIContractOptionEntry>();
  public List<UIContractOptionEntry> renewStaffContractOptions = new List<UIContractOptionEntry>();
  public List<UIContractOptionEntry> staffContractOptionsNoBreakClause = new List<UIContractOptionEntry>();
  private int mMandatoryContractOptions = 3;
  private List<UIContractOptionEntry> mContractOptions = new List<UIContractOptionEntry>();
  public TextMeshProUGUI importantsLabel;
  private ContractNegotiationScreen.NegotatiationType mType;
  private ContractNegotiationScreen.ContractYear mPersonContractYear;
  private ContractPerson mDraftContractPerson;
  private ContractPerson mCurrentContract;
  private ContractManagerPerson.ContractProposalState mContractProposalState;

  public List<UIContractOptionEntry> contractOptions
  {
    get
    {
      return this.mContractOptions;
    }
  }

  public bool canSendProposal
  {
    get
    {
      if (!this.AreAllSettingsDone() || this.mContractProposalState != ContractManagerPerson.ContractProposalState.NoContractProposed)
        return this.mContractProposalState == ContractManagerPerson.ContractProposalState.ProposalRejected;
      return true;
    }
  }

  private void Start()
  {
  }

  private void OnShakeOnItButton()
  {
    switch (this.mType)
    {
      case ContractNegotiationScreen.NegotatiationType.NewDriver:
      case ContractNegotiationScreen.NegotatiationType.NewDriverUnemployed:
        HirePopup dialog1 = UIManager.instance.dialogBoxManager.GetDialog<HirePopup>();
        UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog1);
        dialog1.Setup(this.mDraftContractPerson, this.mCurrentContract.person, this.mType);
        break;
      case ContractNegotiationScreen.NegotatiationType.RenewDriver:
        this.mCurrentContract.GetTeam().contractManager.RenewContractForPerson(this.mDraftContractPerson.person, this.mDraftContractPerson);
        UIManager.instance.ChangeScreen("AllDriversScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
        break;
      case ContractNegotiationScreen.NegotatiationType.NewStaffEmployed:
      case ContractNegotiationScreen.NegotatiationType.NewStaffUnemployed:
        HirePopup dialog2 = UIManager.instance.dialogBoxManager.GetDialog<HirePopup>();
        UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog2);
        dialog2.Setup(this.mDraftContractPerson, this.mCurrentContract.person, this.mType);
        break;
      case ContractNegotiationScreen.NegotatiationType.RenewStaff:
        this.mCurrentContract.GetTeam().contractManager.RenewContractForPerson(this.mDraftContractPerson.person, this.mDraftContractPerson);
        UIManager.instance.ChangeScreen("StaffDetailsScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
        break;
    }
  }

  public void OnRetractOffer()
  {
    if (UIManager.instance.backStackLength > 0)
      UIManager.instance.OnBackButton();
    else
      UIManager.instance.ChangeScreen("HomeScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  public void OnSendProposalButton()
  {
    Game.instance.player.team.contractManager.ProposeNewDraftContract(this.mDraftContractPerson, this.mCurrentContract.person, this.mType, this.mPersonContractYear);
    StringVariableParser.subject = this.mCurrentContract.person;
    FeedbackPopup.Open(Localisation.LocaliseID("PSG_10010874", (GameObject) null), string.Format("{0}'s contract proposal sent", (object) this.mCurrentContract.person.name));
    if (UIManager.instance.backStackLength > 0)
      UIManager.instance.OnBackButton();
    else
      UIManager.instance.ChangeScreen("HomeScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  public void Setup(ContractPerson inTargetContract, ContractPerson inDraftContract, ContractNegotiationScreen.NegotatiationType inType, ContractNegotiationScreen.ContractYear inContractYear, ContractManagerPerson.ContractProposalState inProposalState)
  {
    this.TurnOffPreviousOptions();
    this.mDraftContractPerson = inDraftContract;
    this.mCurrentContract = inTargetContract;
    this.mType = inType;
    this.mPersonContractYear = inContractYear;
    this.mContractProposalState = inProposalState;
    this.SetMandatoryContractOptionCount();
    switch (this.mType)
    {
      case ContractNegotiationScreen.NegotatiationType.NewDriver:
        this.mContractOptions = this.newContractOptions;
        break;
      case ContractNegotiationScreen.NegotatiationType.NewDriverUnemployed:
        this.mContractOptions = this.newDriverUnemployedContractOptions;
        break;
      case ContractNegotiationScreen.NegotatiationType.RenewDriver:
        this.mContractOptions = this.renewContractOptions;
        break;
      case ContractNegotiationScreen.NegotatiationType.NewStaffEmployed:
        this.mContractOptions = this.staffContractOptionsBreakClause;
        break;
      case ContractNegotiationScreen.NegotatiationType.NewStaffUnemployed:
        this.mContractOptions = this.staffContractOptionsNoBreakClause;
        break;
      case ContractNegotiationScreen.NegotatiationType.RenewStaff:
        this.mContractOptions = this.renewStaffContractOptions;
        break;
    }
    StringVariableParser.contractNegotiationPersonLastName = inDraftContract.person.lastName;
    this.importantsLabel.text = Localisation.LocaliseID("PSG_10010472", (GameObject) null);
    this.SetupEntries();
  }

  private void SetMandatoryContractOptionCount()
  {
    if (this.mType == ContractNegotiationScreen.NegotatiationType.RenewDriver || this.mType == ContractNegotiationScreen.NegotatiationType.RenewStaff)
      this.mMandatoryContractOptions = 2;
    else
      this.mMandatoryContractOptions = 3;
  }

  private void TurnOffPreviousOptions()
  {
    for (int index = 0; index < this.newContractOptions.Count; ++index)
      this.newContractOptions[index].gameObject.SetActive(false);
  }

  private void SetupEntries()
  {
    for (int inEntryIndex = 0; inEntryIndex < this.mContractOptions.Count; ++inEntryIndex)
    {
      this.mContractOptions[inEntryIndex].gameObject.SetActive(true);
      this.SetupEntryforPerson(inEntryIndex, this.mContractOptions[inEntryIndex].optionType);
      this.mContractOptions[inEntryIndex].ID = inEntryIndex;
    }
  }

  private void SetupEntryforPerson(int inEntryIndex, UIContractOptionEntry.OptionType inOptionType)
  {
    UIContractOptionEntry mContractOption = this.mContractOptions[inEntryIndex];
    mContractOption.Setup(inOptionType, this.mDraftContractPerson, this.mCurrentContract, this.mContractProposalState);
    mContractOption.activeOption.Reset(this.mDraftContractPerson);
    switch (this.mContractProposalState)
    {
      case ContractManagerPerson.ContractProposalState.NoContractProposed:
        mContractOption.SetState(UIContractOptionEntry.State.Active);
        mContractOption.activeOption.UpdateContractInfo((Contract) this.mDraftContractPerson);
        break;
      case ContractManagerPerson.ContractProposalState.ConsideringProposal:
        mContractOption.SetState(UIContractOptionEntry.State.Considering);
        mContractOption.activeOption.PopulateWithPreviousProposal((Contract) this.mDraftContractPerson);
        break;
      case ContractManagerPerson.ContractProposalState.ProposalRejected:
        mContractOption.SetState(UIContractOptionEntry.State.Rejected);
        mContractOption.activeOption.PopulateWithPreviousProposal((Contract) this.mDraftContractPerson);
        break;
      case ContractManagerPerson.ContractProposalState.ProposalAccepted:
        mContractOption.SetState(UIContractOptionEntry.State.Accepted);
        break;
    }
  }

  private void Update()
  {
    ContractNegotiationScreen currentScreen = (ContractNegotiationScreen) UIManager.instance.currentScreen;
    bool flag1 = this.AreAllSettingsDone();
    bool flag2 = this.mContractProposalState == ContractManagerPerson.ContractProposalState.NoContractProposed || this.mContractProposalState == ContractManagerPerson.ContractProposalState.ProposalRejected;
    if (flag1 && this.mContractProposalState == ContractManagerPerson.ContractProposalState.ProposalAccepted)
      currentScreen.header.ActivateDoneDealGFX(true);
    else if (flag1 && flag2)
      currentScreen.header.ActivateDoneDealGFX(false);
    else
      currentScreen.header.ActivateDoneDealGFX(false);
  }

  private bool AreAllSettingsDone()
  {
    bool flag = true;
    for (int index = 0; index < this.mContractOptions.Count; ++index)
    {
      if ((index < this.mContractOptions.Count - this.mMandatoryContractOptions || this.mContractOptions[index].activeOption.HaveTheSettingsChanged()) && !this.mContractOptions[index].activeOption.AreSettingsChosen())
      {
        flag = false;
        break;
      }
    }
    return flag;
  }
}
