// Decompiled with JetBrains decompiler
// Type: UIMailEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMailEntry : MonoBehaviour
{
  private int mNumDaysAgoLastChangedString = -1;
  public Action<Message, bool> OpenMessage;
  public GameObject container;
  public Toggle toggle;
  public TextMeshProUGUI dateLabel;
  public TextMeshProUGUI personNameLabel;
  public TextMeshProUGUI titleLabel;
  public TextMeshProUGUI descriptionLabel;
  public UICharacterPortrait staffPortrait;
  public UICharacterPortrait driverPortrait;
  public UISponsorLogo sponsorLogo;
  public UITeamLogo teamLogo;
  public UIPressLogo mediaLogo;
  public GameObject politicalLogo;
  public Image mailGroupColor;
  public GameObject readGFX;
  public GameObject mustRespondGFX;
  public GameObject dilemmaGFX;
  private Person mPerson;
  private Message mMessage;
  private string mDateFormatString;
  private string mDateString;
  private string mDaysAgoString;
  private MailScreen mMailScreen;

  public Message message
  {
    get
    {
      return this.mMessage;
    }
    set
    {
      this.mMessage = value;
    }
  }

  private void Awake()
  {
    this.toggle.isOn = false;
  }

  private void Start()
  {
    scSoundManager.BlockSoundEvents = true;
    this.mMailScreen = UIManager.instance.GetScreen<MailScreen>();
    this.SetupListeners();
    Localisation.OnLanguageChange += new Action(this.Refresh);
    scSoundManager.BlockSoundEvents = false;
  }

  public void SetupListeners()
  {
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnToggleValueChanged(value)));
  }

  private void OnDestroy()
  {
    Localisation.OnLanguageChange -= new Action(this.Refresh);
  }

  private void Refresh()
  {
    if (this.mMessage == null)
      return;
    this.Setup(this.mMessage);
  }

  private void SetLogo()
  {
    this.politicalLogo.SetActive(false);
    this.teamLogo.gameObject.SetActive(false);
    this.sponsorLogo.gameObject.SetActive(false);
    this.mediaLogo.gameObject.SetActive(false);
    if (this.mPerson.contract.GetTeam() != Game.instance.teamManager.nullTeam)
    {
      this.teamLogo.SetTeam(this.mPerson.contract.GetTeam());
      this.teamLogo.gameObject.SetActive(this.mPerson.contract.GetTeam() != Game.instance.player.team);
    }
    else if (this.mPerson.contract.GetSponsor() != null)
    {
      this.sponsorLogo.SetSponsor(this.mPerson.contract.GetSponsor());
      this.sponsorLogo.gameObject.SetActive(true);
    }
    else if (this.mPerson.contract.GetMediaOutlet() != null)
    {
      this.mediaLogo.SetMediaOutlet(this.mPerson.contract.GetMediaOutlet());
      this.mediaLogo.gameObject.SetActive(true);
    }
    else
    {
      if (this.mPerson != Game.instance.championshipManager.GetChampionshipByID(0).politicalSystem.president && !(this.mPerson.name == Game.instance.championshipManager.GetChampionshipByID(0).politicalSystem.president.name))
        return;
      this.politicalLogo.SetActive(true);
    }
  }

  public void Setup(Message inMessage)
  {
    this.mDateString = string.Empty;
    this.mMessage = inMessage;
    this.mPerson = inMessage.sender;
    this.mailGroupColor.color = MailScreen.GetBackingColor(this.mMessage.group);
    this.SetLogo();
    if (this.mPerson != null)
    {
      this.personNameLabel.text = this.mPerson.name;
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
    this.titleLabel.text = GameUtility.RemoveLinks(this.mMessage.localisedTitle);
    this.descriptionLabel.text = GameUtility.PrepareStringForMailEntry(this.mMessage.localisedDescription, 50);
    GameUtility.SetActive(this.mustRespondGFX, this.mMessage.mustRespond && !this.mMessage.responded && this.mMessage.headerType != Message.HeaderType.Dilemma);
    GameUtility.SetActive(this.dilemmaGFX, this.mMessage.mustRespond && !this.mMessage.responded && this.mMessage.headerType == Message.HeaderType.Dilemma);
    this.UpdateMessageStatus();
    this.RefreshDateLabel();
  }

  private void Update()
  {
    if (!this.toggle.isOn)
      return;
    this.UpdateMessageStatus();
  }

  public void RefreshDateLabel()
  {
    this.dateLabel.text = this.GetTimeString();
  }

  private string GetTimeString()
  {
    DateTime now = Game.instance.time.now;
    TimeSpan timeSpan = now - this.mMessage.deliverDate;
    if (timeSpan.TotalHours < 2.0 && now.Date == this.mMessage.deliverDate.Date)
      return Localisation.LocaliseID("PSG_10010366", (GameObject) null);
    if (now.Date == this.mMessage.deliverDate.Date)
      return Localisation.LocaliseID("PSG_10009321", (GameObject) null);
    if (now.AddDays(-1.0).Date == this.mMessage.deliverDate.Date)
      return Localisation.LocaliseID("PSG_10010367", (GameObject) null);
    if (timeSpan.TotalDays < 31.0)
    {
      int num = Mathf.RoundToInt((float) timeSpan.TotalDays);
      if (this.mNumDaysAgoLastChangedString != num)
      {
        this.mNumDaysAgoLastChangedString = num;
        StringVariableParser.intValue1 = num;
        this.mDaysAgoString = Localisation.LocaliseID("PSG_10010368", (GameObject) null);
      }
      return this.mDaysAgoString;
    }
    if (this.mDateString == string.Empty || App.instance.preferencesManager.gamePreferences.GetCurrentDateFormat() != this.mDateFormatString)
    {
      this.mDateFormatString = App.instance.preferencesManager.gamePreferences.GetCurrentDateFormat();
      this.mDateString = GameUtility.FormatDateTimeToShortDateString(this.mMessage.deliverDate);
    }
    return this.mDateString;
  }

  private void OnToggleValueChanged(bool inValue)
  {
    bool flag = !this.mMessage.hasBeenRead;
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (inValue && this.OpenMessage != null)
      this.OpenMessage(this.mMessage, false);
    else
      this.mMailScreen.refreshMailListSelectionToggle = true;
    if (!flag)
      return;
    this.UpdateMessageStatus();
  }

  private void UpdateMessageStatus()
  {
    GameUtility.SetActive(this.readGFX, !this.mMessage.hasBeenRead);
    GameUtility.SetActive(this.mustRespondGFX, this.mMessage.mustRespond && !this.mMessage.responded && this.mMessage.headerType != Message.HeaderType.Dilemma);
    GameUtility.SetActive(this.dilemmaGFX, this.mMessage.mustRespond && !this.mMessage.responded && this.mMessage.headerType == Message.HeaderType.Dilemma);
  }
}
