// Decompiled with JetBrains decompiler
// Type: UIStaffDetailsScreenPersonInfoWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIStaffDetailsScreenPersonInfoWidget : MonoBehaviour
{
  public TextMeshProUGUI jobTitleFor;
  public TextMeshProUGUI team;
  public UICharacterPortrait portrait;
  public UITeamLogo teamLogo;
  public Flag flag;
  public TextMeshProUGUI jobTitle;
  public TextMeshProUGUI name;
  public TextMeshProUGUI age;
  public Toggle favouriteToggle;
  public UIStaffDetailsScreenDriverDetailsWidget driverWidget;
  public UIPersonContractDetailsWidget personContractDetails;
  public TextMeshProUGUI contractJobTitle;
  public TextMeshProUGUI contractButtonLabel;
  public Button teamButton;
  public Button negotiateContract;
  public Button compare;
  public Button fire;
  public Button leftButton;
  public Button rightButton;
  public UIAbilityStars abilityStars;
  private Person mPerson;
  private int mBrowsingStaffTeamIndex;

  private void Awake()
  {
    this.negotiateContract.onClick.AddListener(new UnityAction(this.NegotiateContract));
    this.compare.onClick.AddListener(new UnityAction(this.Compare));
    this.fire.onClick.AddListener(new UnityAction(this.OnFire));
    this.teamButton.onClick.AddListener(new UnityAction(this.OnTeamButton));
  }

  private void NegotiateContract()
  {
    if (!this.mPerson.isNegotiatingContract)
    {
      bool transferWindowPreseason = Game.instance.player.team.championship.rules.staffTransferWindowPreseason;
      bool flag = !transferWindowPreseason || transferWindowPreseason && App.instance.gameStateManager.currentState is PreSeasonState;
      bool negotiateContract = this.mPerson.canNegotiateContract;
      if (flag && negotiateContract)
        UIManager.instance.dialogBoxManager.GetDialog<ApproachDialogBox>().Show(this.mPerson, ApproachDialogBox.ApproachType.SignNewContract);
      else if (!negotiateContract)
      {
        UIManager.instance.dialogBoxManager.GetDialog<ApproachDialogBox>().Show(this.mPerson, ApproachDialogBox.ApproachType.RenewContract);
      }
      else
      {
        GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
        string inTitle = Localisation.LocaliseID("PSG_10010996", (GameObject) null);
        string inText = Localisation.LocaliseID("PSG_10010997", (GameObject) null);
        string inCancelString = Localisation.LocaliseID("PSG_10009081", (GameObject) null);
        string empty = string.Empty;
        dialog.Show((Action) null, inCancelString, (Action) null, empty, inText, inTitle);
      }
    }
    else if (this.mPerson is Driver)
      UIManager.instance.ChangeScreen("AllDriversScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    else
      UIManager.instance.ChangeScreen("StaffScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  private void Compare()
  {
    if (!(this.mPerson is Engineer) && !(this.mPerson is Mechanic))
      return;
    UIManager.instance.ChangeScreen("CompareStaffScreen", (Entity) this.mPerson, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }

  private void OnFire()
  {
    FirePopup dialog = UIManager.instance.dialogBoxManager.GetDialog<FirePopup>();
    dialog.Setup(this.mPerson);
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
  }

  public void Setup(Person inPerson)
  {
    if (inPerson == null)
      return;
    this.mPerson = inPerson;
    this.portrait.SetPortrait(this.mPerson);
    this.flag.SetNationality(this.mPerson.nationality);
    this.name.text = this.mPerson.name;
    this.age.text = this.mPerson.GetAge().ToString();
    this.teamLogo.SetTeam(this.mPerson.contract.GetTeam());
    this.favouriteToggle.onValueChanged.RemoveAllListeners();
    this.favouriteToggle.onValueChanged.AddListener(new UnityAction<bool>(inPerson.ToggleShortlisted));
    this.favouriteToggle.isOn = inPerson.isShortlisted;
    this.driverWidget.gameObject.SetActive(false);
    bool isSession = App.instance.gameStateManager.currentState.group != GameState.Group.Frontend;
    bool isPlayerEmployed = !Game.instance.player.IsUnemployed();
    this.teamButton.interactable = !this.mPerson.IsFreeAgent();
    this.SetNegotiateButton(isSession, isPlayerEmployed);
    this.SetCompareButton(isSession, isPlayerEmployed);
    this.SetFireButton(isSession);
    this.abilityStars.SetAbilityStarsData(this.mPerson);
    this.personContractDetails.Setup(this.mPerson);
    StringVariableParser.subject = this.mPerson;
    if (!this.mPerson.IsFreeAgent())
    {
      this.team.text = this.mPerson.contract.GetTeam().name;
      StringVariableParser.contractJob = this.mPerson.contract.job;
      this.jobTitleFor.text = Localisation.LocaliseID("PSG_10010604", (GameObject) null);
      this.jobTitle.text = !this.mPerson.IsReplacementPerson() ? Localisation.LocaliseEnum((Enum) this.mPerson.contract.job) : Localisation.LocaliseID("PSG_10010605", (GameObject) null);
      this.contractJobTitle.text = Localisation.LocaliseEnum((Enum) this.mPerson.contract.job);
      GameUtility.SetActive(this.leftButton.gameObject, true);
      GameUtility.SetActive(this.rightButton.gameObject, true);
      this.leftButton.onClick.RemoveAllListeners();
      this.rightButton.onClick.RemoveAllListeners();
      this.leftButton.onClick.AddListener(new UnityAction(this.OnLeftButton));
      this.rightButton.onClick.AddListener(new UnityAction(this.OnRightButton));
      this.SetupBrowsingStaffTeamIndex();
    }
    else
    {
      this.team.text = Localisation.LocaliseID("PSG_10005815", (GameObject) null);
      this.jobTitleFor.text = Localisation.LocaliseID("PSG_10010606", (GameObject) null);
      this.jobTitle.text = Localisation.LocaliseID("PSG_10009320", (GameObject) null);
      this.contractJobTitle.text = Localisation.LocaliseID("PSG_10009320", (GameObject) null);
      GameUtility.SetActive(this.leftButton.gameObject, false);
      GameUtility.SetActive(this.rightButton.gameObject, false);
    }
    StringVariableParser.subject = (Person) null;
  }

  private void SetNegotiateButton(bool isSession, bool isPlayerEmployed)
  {
    GameUtility.SetActive(this.negotiateContract.gameObject, isPlayerEmployed && (this.mPerson.canNegotiateContract || this.mPerson.isNegotiatingContract));
    if (!this.negotiateContract.gameObject.activeSelf)
      return;
    this.negotiateContract.interactable = !isSession && !this.mPerson.isNegotiatingContract;
    bool flag = !this.mPerson.IsFreeAgent() && this.mPerson.contract.GetTeam().IsPlayersTeam();
    StringVariableParser.subject = this.mPerson;
    if (this.mPerson.isNegotiatingContract)
      this.contractButtonLabel.text = Localisation.LocaliseID("PSG_10010603", (GameObject) null);
    else if (this.mPerson is Driver)
      this.contractButtonLabel.text = !flag ? Localisation.LocaliseID("PSG_10007753", (GameObject) null) : Localisation.LocaliseID("PSG_10006855", (GameObject) null);
    else if (this.mPerson is Engineer)
      this.contractButtonLabel.text = !flag ? Localisation.LocaliseID("PSG_10007755", (GameObject) null) : Localisation.LocaliseID("PSG_10006855", (GameObject) null);
    else if (this.mPerson is Mechanic)
      this.contractButtonLabel.text = !flag ? Localisation.LocaliseID("PSG_10007754", (GameObject) null) : Localisation.LocaliseID("PSG_10006855", (GameObject) null);
    StringVariableParser.subject = (Person) null;
  }

  private void SetCompareButton(bool isSession, bool isPlayerEmployed)
  {
    GameUtility.SetActive(this.compare.gameObject, isPlayerEmployed && !this.mPerson.isTeamPrincipal);
    if (!this.compare.gameObject.activeSelf)
      return;
    this.compare.interactable = !isSession;
  }

  private void SetFireButton(bool isSession)
  {
    GameUtility.SetActive(this.fire.gameObject, this.mPerson.canBeFired);
    if (!this.fire.gameObject.activeSelf)
      return;
    this.fire.interactable = !isSession;
  }

  private void SetupBrowsingStaffTeamIndex()
  {
    if (this.mPerson is Engineer)
    {
      this.mBrowsingStaffTeamIndex = 0;
    }
    else
    {
      if (!(this.mPerson is Mechanic))
        return;
      List<Person> allPeopleOnJob = this.mPerson.contract.GetTeam().contractManager.GetAllPeopleOnJob(Contract.Job.Mechanic);
      for (int index = 0; index < allPeopleOnJob.Count; ++index)
      {
        if (this.mPerson == allPeopleOnJob[index])
        {
          this.mBrowsingStaffTeamIndex = index + 1;
          break;
        }
      }
    }
  }

  private void OnLeftButton()
  {
    --this.mBrowsingStaffTeamIndex;
    if (this.mBrowsingStaffTeamIndex < 0)
      this.mBrowsingStaffTeamIndex = 2;
    this.ChangeScreen();
  }

  private void OnRightButton()
  {
    this.mBrowsingStaffTeamIndex = (this.mBrowsingStaffTeamIndex + 1) % 3;
    this.ChangeScreen();
  }

  private void OnTeamButton()
  {
    if (this.mPerson.IsFreeAgent())
      return;
    UIManager.instance.ChangeScreen("TeamScreen", (Entity) this.mPerson.contract.GetTeam(), UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }

  private void ChangeScreen()
  {
    if (this.mBrowsingStaffTeamIndex == 0)
    {
      UIManager.instance.ChangeScreen("StaffDetailsScreen", (Entity) this.mPerson.contract.GetTeam().contractManager.GetPersonOnJob(Contract.Job.EngineerLead), UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
    }
    else
    {
      if (this.mBrowsingStaffTeamIndex < 1)
        return;
      UIManager.instance.ChangeScreen("StaffDetailsScreen", (Entity) this.mPerson.contract.GetTeam().contractManager.GetAllPeopleOnJob(Contract.Job.Mechanic)[this.mBrowsingStaffTeamIndex - 1], UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
    }
  }
}
