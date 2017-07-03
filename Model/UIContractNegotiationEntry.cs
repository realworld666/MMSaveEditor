// Decompiled with JetBrains decompiler
// Type: UIContractNegotiationEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIContractNegotiationEntry : MonoBehaviour
{
  public List<Toggle> strikes = new List<Toggle>();
  public TextMeshProUGUI personName;
  public TextMeshProUGUI otherTeamName;
  public TextMeshProUGUI age;
  public TextMeshProUGUI notScoutedLabel;
  public TextMeshProUGUI agreedTimeRemaining;
  public TextMeshProUGUI wagesProposalLabel;
  public TextMeshProUGUI detailsButtonLabel;
  public Flag flag;
  public UIAbilityStars abilityStars;
  public Button cancelButton;
  public Button adjustButton;
  public Button signButton;
  public Button detailsButton;
  public GameObject consideringOffer;
  public GameObject termsNotMet;
  public GameObject agreedOffer;
  public GameObject insulted;
  public GameObject lastChance;
  private Person mDraftPerson;

  private void Start()
  {
    this.cancelButton.onClick.AddListener(new UnityAction(this.OnCancelButton));
    this.adjustButton.onClick.AddListener(new UnityAction(this.OnAdjustButton));
    this.signButton.onClick.AddListener(new UnityAction(this.OnSignButton));
    this.detailsButton.onClick.AddListener(new UnityAction(this.OnDetailsButton));
  }

  public void SetupEntryForDraft(Person inDraftPerson)
  {
    this.mDraftPerson = inDraftPerson;
    this.personName.text = inDraftPerson.name;
    if (!inDraftPerson.IsFreeAgent())
      this.otherTeamName.text = inDraftPerson.contract.GetTeam().name;
    else
      this.otherTeamName.text = "-";
    this.flag.SetNationality(inDraftPerson.nationality);
    StringVariableParser.intValue1 = inDraftPerson.GetAge();
    this.age.text = Localisation.LocaliseID("PSG_10010748", (GameObject) null);
    this.wagesProposalLabel.text = GameUtility.GetCurrencyString(inDraftPerson.contractManager.draftProposalContract.GetTotalCostToContractForThisYear(), 0);
    Driver inDriver = inDraftPerson as Driver;
    GameUtility.SetActive(this.notScoutedLabel.gameObject, false);
    if (inDriver != null)
    {
      if (inDriver.CanShowStats())
      {
        GameUtility.SetActive(this.abilityStars.gameObject, true);
        this.abilityStars.SetAbilityStarsData(inDriver);
      }
      else
      {
        GameUtility.SetActive(this.abilityStars.gameObject, false);
        GameUtility.SetActive(this.notScoutedLabel.gameObject, true);
      }
    }
    else
      this.abilityStars.SetAbilityStarsData(inDraftPerson);
    this.SetupProposalState();
    this.SetStrikes(this.mDraftPerson);
    this.UpdateStrikes(this.mDraftPerson);
    this.detailsButtonLabel.text = !(this.mDraftPerson is Driver) ? Localisation.LocaliseID("PSG_10002283", (GameObject) null) : Localisation.LocaliseID("PSG_10002307", (GameObject) null);
  }

  private void Update()
  {
    this.SetupProposalState();
    if (!this.mDraftPerson.contractManager.isProposalAccepted)
      return;
    this.agreedTimeRemaining.text = this.mDraftPerson.contractManager.contractAgreedTimeRemaining;
  }

  private void SetupProposalState()
  {
    bool flag = !this.mDraftPerson.contractManager.hasPatience;
    bool isLastChance = this.mDraftPerson.contractManager.isLastChance;
    GameUtility.SetActive(this.consideringOffer, this.mDraftPerson.contractManager.isConsideringProposal);
    GameUtility.SetActive(this.termsNotMet, this.mDraftPerson.contractManager.isProposalRejected && !flag && !isLastChance);
    GameUtility.SetActive(this.lastChance, this.mDraftPerson.contractManager.isProposalRejected && !flag && isLastChance);
    GameUtility.SetActive(this.insulted, this.mDraftPerson.contractManager.isProposalRejected && flag);
    GameUtility.SetActive(this.agreedOffer, this.mDraftPerson.contractManager.isProposalAccepted);
    GameUtility.SetActive(this.signButton.gameObject, this.mDraftPerson.contractManager.isProposalAccepted);
    GameUtility.SetActive(this.adjustButton.gameObject, this.mDraftPerson.contractManager.isProposalRejected && !flag);
  }

  private void SetStrikes(Person inPerson)
  {
    for (int index = 0; index < this.strikes.Count; ++index)
    {
      if (index < inPerson.contractManager.contractPatienceAvailable)
        GameUtility.SetActive(this.strikes[index].gameObject, true);
      else
        GameUtility.SetActive(this.strikes[index].gameObject, false);
      this.strikes[index].isOn = false;
      this.strikes[index].interactable = false;
    }
  }

  private void UpdateStrikes(Person inPerson)
  {
    for (int index = 0; index < this.strikes.Count; ++index)
      this.strikes[index].isOn = index < inPerson.contractManager.contractPatienceUsed;
  }

  private void OnCancelButton()
  {
    GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
    Action inCancelAction = (Action) (() => {});
    string inTitle = Localisation.LocaliseID("PSG_10009117", (GameObject) null);
    string inCancelString = Localisation.LocaliseID("PSG_10009077", (GameObject) null);
    string inConfirmString = Localisation.LocaliseID("PSG_10009078", (GameObject) null);
    string inText = Localisation.LocaliseID("PSG_10009118", (GameObject) null);
    Action inConfirmAction = (Action) (() =>
    {
      Team team = Game.instance.player.team;
      this.mDraftPerson.contractManager.AddCancelledNegotiationCooldown();
      if (this.mDraftPerson.contractManager.isConsideringProposal)
        team.contractManager.CancelDraftProposal(this.mDraftPerson);
      else
        team.contractManager.RemoveDraftProposal(this.mDraftPerson);
      UIManager.instance.RefreshCurrentPage();
    });
    dialog.Show(inCancelAction, inCancelString, inConfirmAction, inConfirmString, inText, inTitle);
    this.OnMouseExit();
  }

  private void OnAdjustButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    UIManager.instance.ChangeScreen("ContractNegotiationScreen", (Entity) this.mDraftPerson, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
    this.OnMouseExit();
  }

  private void OnDetailsButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mDraftPerson == null)
      return;
    if (this.mDraftPerson is Driver)
      UIManager.instance.ChangeScreen("DriverScreen", (Entity) (this.mDraftPerson as Driver), UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
    else
      UIManager.instance.ChangeScreen("StaffDetailsScreen", (Entity) this.mDraftPerson, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
    this.OnMouseExit();
  }

  private void OnSignButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mDraftPerson.contractManager.isRenewProposal)
    {
      this.mDraftPerson.contract.GetTeam().contractManager.RenewContractForPerson(this.mDraftPerson, this.mDraftPerson.contractManager.draftProposalContract);
      UIManager.instance.RefreshCurrentPage();
      StringVariableParser.contractJob = this.mDraftPerson.contract.job;
      StringVariableParser.subject = this.mDraftPerson;
      FeedbackPopup.Open(Localisation.LocaliseID("PSG_10010873", (GameObject) null), string.Format("{0} got renewed!", (object) this.mDraftPerson.name));
    }
    else if (this.mDraftPerson.contractManager.negotiationType == ContractNegotiationScreen.NegotatiationType.PromoteReserveDriver)
    {
      PromotePopup dialog = UIManager.instance.dialogBoxManager.GetDialog<PromotePopup>();
      UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
      dialog.Setup(this.mDraftPerson);
    }
    else
    {
      HirePopup dialog = UIManager.instance.dialogBoxManager.GetDialog<HirePopup>();
      UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
      dialog.Setup(this.mDraftPerson.contractManager.draftProposalContract, this.mDraftPerson, this.mDraftPerson.contractManager.negotiationType);
    }
    this.OnMouseExit();
  }

  private void OnMouseEnter()
  {
    UIManager.instance.dialogBoxManager.GetDialog<ContractNegotiationRollover>().ShowRollover(this.mDraftPerson);
  }

  private void OnMouseExit()
  {
    UIManager.instance.dialogBoxManager.GetDialog<ContractNegotiationRollover>().HideRollover();
  }
}
