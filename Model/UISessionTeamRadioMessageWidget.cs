// Decompiled with JetBrains decompiler
// Type: UISessionTeamRadioMessageWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISessionTeamRadioMessageWidget : MonoBehaviour
{
  [HideInInspector]
  public bool isFinished = true;
  private float mCountDown = 5f;
  private float mCurrentCountDown = 5f;
  public UICharacterPortrait staffPortrait;
  public UICharacterPortrait driverPortrait;
  public Image countDownImage;
  public TextMeshProUGUI radioMessage;
  public GameObject buttons;
  public Button ignoreButton;
  public Button takeActionButton;
  private scSoundContainer mSoundRadioMessage;

  public void OnStart()
  {
    this.ignoreButton.onClick.AddListener(new UnityAction(this.IgnoreButtonOnClick));
  }

  private void IgnoreButtonOnClick()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.EndRadioMessage();
  }

  public void OnEnter(RadioMessage inRadioMessage)
  {
    if (inRadioMessage != null && inRadioMessage.dialogRule != null)
    {
      this.ShowTeamRadioMessage(inRadioMessage);
    }
    else
    {
      this.Show(false);
      this.isFinished = true;
    }
  }

  public void OnExit()
  {
  }

  public void ShowTeamRadioMessage(RadioMessage inRadioMessage)
  {
    if (inRadioMessage.dialogRule == null)
      return;
    this.Show(true);
    scSoundManager.CheckPlaySound(SoundID.Sfx_RadioMessage, ref this.mSoundRadioMessage, 0.0f);
    if (inRadioMessage.personWhoSpeaks.contract.job == Contract.Job.Driver)
    {
      this.staffPortrait.gameObject.SetActive(false);
      this.driverPortrait.gameObject.SetActive(true);
      this.driverPortrait.SetPortrait(inRadioMessage.personWhoSpeaks);
    }
    else
    {
      this.staffPortrait.gameObject.SetActive(true);
      this.driverPortrait.gameObject.SetActive(false);
      this.staffPortrait.SetPortrait(inRadioMessage.personWhoSpeaks);
    }
    this.radioMessage.text = inRadioMessage.text.GetText();
    this.SetupButtons(inRadioMessage);
    this.isFinished = false;
  }

  private void SetupButtons(RadioMessage inRadioMessage)
  {
    List<DialogCriteria> userData = inRadioMessage.dialogRule.userData;
    GameUtility.SetActive(this.takeActionButton.gameObject, false);
    for (int index = 0; index < userData.Count; ++index)
    {
      if (userData[index].mType == "Button")
      {
        this.SetupActionButton(userData[index], inRadioMessage.personWhoSpeaks);
        GameUtility.SetActive(this.takeActionButton.gameObject, true);
      }
    }
  }

  private void SetupActionButton(DialogCriteria inDialogcriteria, Person inPersonWhoSpeaks)
  {
    if (!(inDialogcriteria.mCriteriaInfo == "Pit"))
      return;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UISessionTeamRadioMessageWidget.\u003CSetupActionButton\u003Ec__AnonStoreyAA buttonCAnonStoreyAa = new UISessionTeamRadioMessageWidget.\u003CSetupActionButton\u003Ec__AnonStoreyAA();
    // ISSUE: reference to a compiler-generated field
    buttonCAnonStoreyAa.\u003C\u003Ef__this = this;
    // ISSUE: reference to a compiler-generated field
    buttonCAnonStoreyAa.personWhoTalk = inPersonWhoSpeaks as Driver;
    // ISSUE: reference to a compiler-generated field
    if (buttonCAnonStoreyAa.personWhoTalk == null)
      return;
    this.takeActionButton.onClick.RemoveAllListeners();
    // ISSUE: reference to a compiler-generated method
    this.takeActionButton.onClick.AddListener(new UnityAction(buttonCAnonStoreyAa.\u003C\u003Em__21F));
  }

  private void Show(bool inShow)
  {
    this.gameObject.SetActive(inShow);
    this.buttons.SetActive(inShow);
  }

  private void Update()
  {
    if (Game.instance.time.isPaused)
      return;
    this.mCurrentCountDown -= GameTimer.deltaTime;
    this.countDownImage.fillAmount = this.mCurrentCountDown / this.mCountDown;
    if ((double) this.mCurrentCountDown >= 0.0)
      return;
    this.EndRadioMessage();
  }

  private void EndRadioMessage()
  {
    scSoundManager.CheckStopSound(ref this.mSoundRadioMessage);
    this.isFinished = true;
    this.mCurrentCountDown = this.mCountDown;
    this.Show(false);
  }

  private void GoToPitScreen(Driver inDriver)
  {
    RacingVehicle vehicle = Game.instance.vehicleManager.GetVehicle(inDriver);
    if (Game.instance.sessionManager.eventDetails.currentSession.sessionType == SessionDetails.SessionType.Race)
      UIManager.instance.GetScreen<PitScreen>().Setup(vehicle, PitScreen.Mode.Pitting);
    else
      UIManager.instance.GetScreen<PitScreen>().Setup(vehicle, PitScreen.Mode.SendOut);
    UIManager.instance.ChangeScreen("PitScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }
}
