// Decompiled with JetBrains decompiler
// Type: UISessionTeamRadioWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class UISessionTeamRadioWidget : MonoBehaviour
{
  private List<RadioMessage> mTeamRadioMessagesQueue = new List<RadioMessage>();
  private string mLastWeatherMessageSource = string.Empty;
  public UISessionTeamRadioMessageWidget[] teamRadioMessageWidgets;
  public DriverActionButtons[] driverActionButtons;
  public DilemmaWidget[] dilemmaWidgets;
  private RadioMessage[] mCurrentTeamRadioMessages;
  private SessionDetails.SessionType mPreviousSessionType;

  public void OnStart()
  {
    for (int index = 0; index < this.teamRadioMessageWidgets.Length; ++index)
      this.teamRadioMessageWidgets[index].OnStart();
    this.mCurrentTeamRadioMessages = new RadioMessage[this.teamRadioMessageWidgets.Length];
  }

  public void OnEnter()
  {
    if (this.mPreviousSessionType != Game.instance.sessionManager.eventDetails.currentSession.sessionType)
      this.ResetCurrentMessagesAndQueue();
    for (int index = 0; index < this.teamRadioMessageWidgets.Length; ++index)
    {
      if (this.teamRadioMessageWidgets[index].isFinished)
      {
        this.mTeamRadioMessagesQueue.Remove(this.mCurrentTeamRadioMessages[index]);
        this.mCurrentTeamRadioMessages[index] = (RadioMessage) null;
      }
      this.teamRadioMessageWidgets[index].OnEnter(this.mCurrentTeamRadioMessages[index]);
    }
    this.CheckIfNeedToHideStandings();
    this.RegisterRadioMessageEvents();
  }

  private void CheckIfNeedToHideStandings()
  {
  }

  public void OnExit()
  {
    for (int index = 0; index < this.teamRadioMessageWidgets.Length; ++index)
      this.teamRadioMessageWidgets[index].OnExit();
    this.mPreviousSessionType = Game.instance.sessionManager.eventDetails.currentSession.sessionType;
  }

  private void ResetCurrentMessagesAndQueue()
  {
    this.mTeamRadioMessagesQueue.Clear();
    for (int index = 0; index < this.mCurrentTeamRadioMessages.Length; ++index)
      this.mCurrentTeamRadioMessages[index] = (RadioMessage) null;
  }

  public void RegisterRadioMessageEvents()
  {
    TeamRadioManager teamRadioManager = Game.instance.sessionManager.teamRadioManager;
    teamRadioManager.OnRadioMessage = (Action<RadioMessage>) null;
    teamRadioManager.OnRadioMessage += new Action<RadioMessage>(this.OnRadioMessage);
    teamRadioManager.OnDilemma = (Action<RadioMessage>) null;
    teamRadioManager.OnDilemma += new Action<RadioMessage>(this.OnDilemmaTriggered);
  }

  private void OnRadioMessage(RadioMessage inRadioMessage)
  {
    this.mTeamRadioMessagesQueue.Add(inRadioMessage);
  }

  private void OnDilemmaTriggered(RadioMessage inRadioMessage)
  {
    Driver personWhoSpeaks = inRadioMessage.personWhoSpeaks as Driver;
    if (personWhoSpeaks == null)
      return;
    this.dilemmaWidgets[Game.instance.player.team.GetDriverIndex(personWhoSpeaks)].OnEnter(inRadioMessage);
  }

  private void Update()
  {
    for (int index = 0; index < this.teamRadioMessageWidgets.Length; ++index)
    {
      if (this.teamRadioMessageWidgets[index].isFinished && this.mCurrentTeamRadioMessages[index] != null)
      {
        this.mTeamRadioMessagesQueue.Remove(this.mCurrentTeamRadioMessages[index]);
        this.mCurrentTeamRadioMessages[index] = (RadioMessage) null;
        this.CheckIfNeedToHideStandings();
      }
    }
    for (int teamRadioWidgetIndex = 0; teamRadioWidgetIndex < this.mCurrentTeamRadioMessages.Length; ++teamRadioWidgetIndex)
    {
      if (this.mCurrentTeamRadioMessages[teamRadioWidgetIndex] == null)
        this.GetNextMessage(teamRadioWidgetIndex);
    }
  }

  private void GetNextMessage(int teamRadioWidgetIndex)
  {
    if (this.mTeamRadioMessagesQueue.Count <= 0)
      return;
    Driver selectedDriver = Game.instance.player.team.GetSelectedDriver(teamRadioWidgetIndex);
    for (int index = 0; index < this.mTeamRadioMessagesQueue.Count; ++index)
    {
      RadioMessage teamRadioMessages = this.mTeamRadioMessagesQueue[index];
      if (teamRadioMessages.personWhoSpeaks == selectedDriver)
      {
        if (this.HaveShownWeatherMessageOfSameSource(teamRadioMessages))
        {
          this.mTeamRadioMessagesQueue.RemoveAt(index);
          break;
        }
        this.mCurrentTeamRadioMessages[teamRadioWidgetIndex] = teamRadioMessages;
        this.teamRadioMessageWidgets[teamRadioWidgetIndex].ShowTeamRadioMessage(teamRadioMessages);
        if (teamRadioMessages.messageCategory == RadioMessage.MessageCategory.WeatherChange)
          this.mLastWeatherMessageSource = teamRadioMessages.dialogRule.GetSource();
        this.CheckIfNeedToHideStandings();
        break;
      }
    }
  }

  private bool HaveShownWeatherMessageOfSameSource(RadioMessage inRadioMessage)
  {
    if (inRadioMessage.messageCategory == RadioMessage.MessageCategory.WeatherChange)
      return inRadioMessage.dialogRule.GetSource() == this.mLastWeatherMessageSource;
    return false;
  }
}
