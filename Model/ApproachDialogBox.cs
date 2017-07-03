// Decompiled with JetBrains decompiler
// Type: ApproachDialogBox
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ApproachDialogBox : UIDialogBox
{
  [SerializeField]
  private TextMeshProUGUI popupHeader;
  [SerializeField]
  private UICharacterPortrait personPortrait;
  [SerializeField]
  private UICharacterPortrait driverPortrait;
  [SerializeField]
  private UITeamLogo personTeamLogo;
  [SerializeField]
  private TextMeshProUGUI personName;
  [SerializeField]
  private TextMeshProUGUI personTeamName;
  [SerializeField]
  private TextMeshProUGUI personPosition;
  [SerializeField]
  private UIAbilityStars personAbilityStars;
  [SerializeField]
  private Flag personNationality;
  [SerializeField]
  private GameObject teamObject;
  [SerializeField]
  private TextMeshProUGUI approachChat;
  [SerializeField]
  private TextMeshProUGUI approachStatus;
  [SerializeField]
  private Image approachHighlight;
  [SerializeField]
  private Button retractOfferButton;
  [SerializeField]
  private Button startNegotiationsButton;
  [SerializeField]
  private TextMeshProUGUI negotiationButtonLabel;
  [SerializeField]
  private TextMeshProUGUI retractButtonLabel;
  private ApproachDialogBox.ApproachEntity mApproachEntity;
  private ApproachDialogBox.ApproachType mApproachType;
  private Person mPerson;

  private void Start()
  {
    this.startNegotiationsButton.onClick.AddListener(new UnityAction(this.OnStartNegotiationButton));
    this.retractOfferButton.onClick.AddListener(new UnityAction(((UIDialogBox) this).OnCancelButtonClicked));
  }

  public void Show(Person inPerson, ApproachDialogBox.ApproachType inType)
  {
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) this);
    this.mApproachType = inType;
    this.mPerson = inPerson;
    this.SetApproachType();
    this.SetupDialogBox();
  }

  private void SetApproachType()
  {
    if (this.mPerson is Driver)
      this.mApproachEntity = ApproachDialogBox.ApproachEntity.Driver;
    else if (this.mPerson is Chairman)
      this.mApproachEntity = ApproachDialogBox.ApproachEntity.Team;
    else
      this.mApproachEntity = ApproachDialogBox.ApproachEntity.Staff;
  }

  private void SetupDialogBox()
  {
    this.SetupHeader();
    this.SetupButtonLabels();
    this.SetupPortrait();
    this.SetupAbilityStars();
    this.SetupTeamDetails();
    StringVariableParser.subject = this.mPerson;
    switch (this.mApproachType)
    {
      case ApproachDialogBox.ApproachType.SignNewContract:
        this.SetupApproachStatus();
        break;
      case ApproachDialogBox.ApproachType.RenewContract:
        this.SetupRenewStatus();
        break;
    }
    this.personPosition.text = Localisation.LocaliseEnum((Enum) this.mPerson.contract.job);
    this.personNationality.SetNationality(this.mPerson.nationality);
    this.personName.text = this.mPerson.name;
    StringVariableParser.subject = (Person) null;
  }

  private void SetupHeader()
  {
    switch (this.mApproachType)
    {
      case ApproachDialogBox.ApproachType.SignNewContract:
        switch (this.mApproachEntity)
        {
          case ApproachDialogBox.ApproachEntity.Driver:
            this.popupHeader.text = Localisation.LocaliseID("PSG_10007753", (GameObject) null);
            return;
          case ApproachDialogBox.ApproachEntity.Staff:
            if (this.mPerson is Engineer)
            {
              this.popupHeader.text = Localisation.LocaliseID("PSG_10007755", (GameObject) null);
              return;
            }
            this.popupHeader.text = Localisation.LocaliseID("PSG_10007754", (GameObject) null);
            return;
          case ApproachDialogBox.ApproachEntity.Team:
            this.popupHeader.text = Localisation.LocaliseID("PSG_10009239", (GameObject) null);
            return;
          default:
            return;
        }
      case ApproachDialogBox.ApproachType.RenewContract:
        this.popupHeader.text = Localisation.LocaliseID("PSG_10006855", (GameObject) null);
        break;
    }
  }

  private void SetupButtonLabels()
  {
    switch (this.mApproachType)
    {
      case ApproachDialogBox.ApproachType.SignNewContract:
        switch (this.mApproachEntity)
        {
          case ApproachDialogBox.ApproachEntity.Driver:
          case ApproachDialogBox.ApproachEntity.Staff:
            this.negotiationButtonLabel.text = Localisation.LocaliseID("PSG_10009241", (GameObject) null);
            this.retractButtonLabel.text = Localisation.LocaliseID("PSG_10009242", (GameObject) null);
            return;
          case ApproachDialogBox.ApproachEntity.Team:
            this.negotiationButtonLabel.text = Localisation.LocaliseID("PSG_10009243", (GameObject) null);
            this.retractButtonLabel.text = Localisation.LocaliseID("PSG_10009244", (GameObject) null);
            return;
          default:
            return;
        }
      case ApproachDialogBox.ApproachType.RenewContract:
        this.negotiationButtonLabel.text = Localisation.LocaliseID("PSG_10009241", (GameObject) null);
        this.retractButtonLabel.text = Localisation.LocaliseID("PSG_10009242", (GameObject) null);
        break;
    }
  }

  private void SetupPortrait()
  {
    switch (this.mApproachEntity)
    {
      case ApproachDialogBox.ApproachEntity.Driver:
        GameUtility.SetActive(this.personPortrait.gameObject, false);
        GameUtility.SetActive(this.driverPortrait.gameObject, true);
        this.driverPortrait.SetPortrait(this.mPerson);
        break;
      case ApproachDialogBox.ApproachEntity.Staff:
      case ApproachDialogBox.ApproachEntity.Team:
        GameUtility.SetActive(this.personPortrait.gameObject, true);
        GameUtility.SetActive(this.driverPortrait.gameObject, false);
        this.personPortrait.SetPortrait(this.mPerson);
        break;
    }
  }

  private void SetupAbilityStars()
  {
    switch (this.mApproachEntity)
    {
      case ApproachDialogBox.ApproachEntity.Driver:
        this.personAbilityStars.SetAbilityStarsData(this.mPerson as Driver);
        break;
      case ApproachDialogBox.ApproachEntity.Staff:
        this.personAbilityStars.SetAbilityStarsData(this.mPerson);
        break;
      case ApproachDialogBox.ApproachEntity.Team:
        GameUtility.SetActive(this.personAbilityStars.gameObject, false);
        break;
    }
  }

  private void SetupTeamDetails()
  {
    switch (this.mApproachEntity)
    {
      case ApproachDialogBox.ApproachEntity.Driver:
      case ApproachDialogBox.ApproachEntity.Staff:
        if (this.mPerson.IsFreeAgent())
        {
          GameUtility.SetActive(this.teamObject, false);
          this.personTeamLogo.SetTeam(this.mPerson.contract.GetTeam());
          break;
        }
        GameUtility.SetActive(this.teamObject, true);
        this.personTeamName.text = this.mPerson.contract.GetTeam().name;
        this.personTeamLogo.SetTeam(this.mPerson.contract.GetTeam());
        break;
      case ApproachDialogBox.ApproachEntity.Team:
        GameUtility.SetActive(this.teamObject, true);
        this.personTeamName.text = this.mPerson.contract.GetTeam().name;
        this.personTeamLogo.SetTeam(this.mPerson.contract.GetTeam());
        break;
    }
  }

  private void SetupRenewStatus()
  {
    switch (this.mApproachEntity)
    {
      case ApproachDialogBox.ApproachEntity.Driver:
      case ApproachDialogBox.ApproachEntity.Staff:
        Person.InterestedToTalkResponseType interestedToTalkReaction = this.mPerson.GetInterestedToTalkReaction(Game.instance.player.team);
        bool inIsActive = interestedToTalkReaction == Person.InterestedToTalkResponseType.InterestedToTalk;
        GameUtility.SetActive(this.startNegotiationsButton.gameObject, inIsActive);
        this.approachChat.text = this.GetInterestedToTalkText(interestedToTalkReaction);
        if (inIsActive)
        {
          this.approachStatus.text = Localisation.LocaliseID("PSG_10009245", (GameObject) null);
          this.approachStatus.color = UIConstants.approachDialogBoxGreen;
          this.approachHighlight.color = UIConstants.approachDialogBoxGreen;
          break;
        }
        this.approachStatus.text = Localisation.LocaliseID("PSG_10009246", (GameObject) null);
        this.approachStatus.color = UIConstants.approachDialogBoxRed;
        this.approachHighlight.color = UIConstants.approachDialogBoxRed;
        this.retractButtonLabel.text = Localisation.LocaliseID("PSG_10009081", (GameObject) null);
        break;
    }
  }

  private void SetupApproachStatus()
  {
    switch (this.mApproachEntity)
    {
      case ApproachDialogBox.ApproachEntity.Driver:
      case ApproachDialogBox.ApproachEntity.Staff:
        Person.InterestedToTalkResponseType interestedToTalkReaction = this.mPerson.GetInterestedToTalkReaction(Game.instance.player.team);
        bool inIsActive = interestedToTalkReaction == Person.InterestedToTalkResponseType.InterestedToTalk;
        GameUtility.SetActive(this.startNegotiationsButton.gameObject, inIsActive);
        this.approachChat.text = this.GetInterestedToTalkText(interestedToTalkReaction);
        if (inIsActive)
        {
          this.approachStatus.text = Localisation.LocaliseID("PSG_10009245", (GameObject) null);
          this.approachStatus.color = UIConstants.approachDialogBoxGreen;
          this.approachHighlight.color = UIConstants.approachDialogBoxGreen;
          break;
        }
        this.approachStatus.text = Localisation.LocaliseID("PSG_10009246", (GameObject) null);
        this.approachStatus.color = UIConstants.approachDialogBoxRed;
        this.approachHighlight.color = UIConstants.approachDialogBoxRed;
        break;
      case ApproachDialogBox.ApproachEntity.Team:
        Team team = this.mPerson.contract.GetTeam();
        if (Game.instance.player.CanApproachTeam(team))
        {
          GameUtility.SetActive(this.startNegotiationsButton.gameObject, true);
          this.approachChat.text = Localisation.LocaliseID("PSG_10009247", (GameObject) null);
          this.approachStatus.text = Localisation.LocaliseID("PSG_10009248", (GameObject) null);
          this.approachStatus.color = UIConstants.approachDialogBoxGreen;
          this.approachHighlight.color = UIConstants.approachDialogBoxGreen;
          break;
        }
        GameUtility.SetActive(this.startNegotiationsButton.gameObject, false);
        bool flag1 = Game.instance.player.isTeamHistoryCooldownReady(team);
        bool joinedTeamRecently = Game.instance.player.hasJoinedTeamRecently;
        bool flag2 = Game.instance.time.now.Subtract(team.teamPrincipal.contract.startDate).TotalDays < 180.0;
        if (!flag1)
          this.approachChat.text = Localisation.LocaliseID("PSG_10009249", (GameObject) null);
        else if (joinedTeamRecently)
          this.approachChat.text = Localisation.LocaliseID("PSG_10009250", (GameObject) null);
        else if (flag2)
          this.approachChat.text = Localisation.LocaliseID("PSG_10009251", (GameObject) null);
        else
          this.approachChat.text = Localisation.LocaliseID("PSG_10009252", (GameObject) null);
        this.approachStatus.text = Localisation.LocaliseID("PSG_10009253", (GameObject) null);
        this.approachStatus.color = UIConstants.approachDialogBoxRed;
        this.approachHighlight.color = UIConstants.approachDialogBoxRed;
        break;
    }
  }

  private string GetInterestedToTalkText(Person.InterestedToTalkResponseType inInterestedToTalk)
  {
    switch (inInterestedToTalk)
    {
      case Person.InterestedToTalkResponseType.InterestedToTalk:
        return Localisation.LocaliseID("PSG_10009258", (GameObject) null);
      case Person.InterestedToTalkResponseType.NotJoiningLowerChampionship:
        return Localisation.LocaliseID("PSG_10011117", (GameObject) null);
      case Person.InterestedToTalkResponseType.WantToJoinHigherChampionship:
        return Localisation.LocaliseID("PSG_10011119", (GameObject) null);
      case Person.InterestedToTalkResponseType.JustBeenFiredByPlayer:
        return Localisation.LocaliseID("PSG_10009254", (GameObject) null);
      case Person.InterestedToTalkResponseType.InsultedByLastProposal:
        return Localisation.LocaliseID("PSG_10009257", (GameObject) null);
      case Person.InterestedToTalkResponseType.WantsToRetire:
        return Localisation.LocaliseID("PSG_10009261", (GameObject) null);
      case Person.InterestedToTalkResponseType.WontRenewContract:
        return Localisation.LocaliseID("PSG_10009262", (GameObject) null);
      case Person.InterestedToTalkResponseType.WontJoinRival:
        return Localisation.LocaliseID("PSG_10009256", (GameObject) null);
      case Person.InterestedToTalkResponseType.JustStartedANewContract:
        return Localisation.LocaliseID("PSG_10009255", (GameObject) null);
      case Person.InterestedToTalkResponseType.TooEarlyToRenew:
        return Localisation.LocaliseID("PSG_10009260", (GameObject) null);
      case Person.InterestedToTalkResponseType.MoraleTooLow:
        return Localisation.LocaliseID("PSG_10009259", (GameObject) null);
      case Person.InterestedToTalkResponseType.LetNegotiationExpire:
        return Localisation.LocaliseID("PSG_10011091", (GameObject) null);
      case Person.InterestedToTalkResponseType.CanceledNegotiation:
        return Localisation.LocaliseID("PSG_10011022", (GameObject) null);
      case Person.InterestedToTalkResponseType.WontDriveForThatSeries:
        Driver mPerson = this.mPerson as Driver;
        if (mPerson == null)
          return "'Won't driver for series' being used for a non driver entity";
        if (mPerson.preferedSeries == Championship.Series.GTSeries)
          return Localisation.LocaliseID("PSG_10011986", (GameObject) null);
        return Localisation.LocaliseID("PSG_10011985", (GameObject) null);
      case Person.InterestedToTalkResponseType.OffendedByInterview:
        return Localisation.LocaliseID("PSG_10012169", (GameObject) null);
      default:
        return Localisation.LocaliseID("PSG_10009263", (GameObject) null);
    }
  }

  private void OnStartNegotiationButton()
  {
    if (this.mApproachEntity != ApproachDialogBox.ApproachEntity.Team)
    {
      UIManager.instance.ChangeScreen("ContractNegotiationScreen", (Entity) this.mPerson, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
      this.Hide();
    }
    else
    {
      Game.instance.player.SendJobApplication(this.mPerson.contract.GetTeam());
      this.Hide();
    }
  }

  public enum ApproachType
  {
    SignNewContract,
    RenewContract,
  }

  private enum ApproachEntity
  {
    Driver,
    Staff,
    Team,
  }
}
