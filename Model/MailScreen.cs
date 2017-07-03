// Decompiled with JetBrains decompiler
// Type: MailScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MailScreen : UIScreen
{
  public GameUtility.AnchorLocation inboxListSnapMode = GameUtility.AnchorLocation.Center;
  private Message.Group mMessageGroupFilter = Message.Group.Count;
  private Dictionary<MailScreen.TimeSeparator, UIMailDateSeparator> mDateSeparators = new Dictionary<MailScreen.TimeSeparator, UIMailDateSeparator>();
  private List<UIMailEntry> mMailEntries = new List<UIMailEntry>();
  public UIGridList inboxList;
  public UIGridList toggleList;
  public UIGridList datesToggleList;
  public ScrollRect inboxScrollRect;
  public GameObject messageBodyContainer;
  public GameObject messageHeaderContainer;
  public ToggleGroup toggleGroup;
  public Toggle urgentToggle;
  public TextMeshProUGUI urgentTextLabel;
  public Toggle noFilterToggle;
  public TextMeshProUGUI noFilterTextLabel;
  public GameObject noEntriesLabel;
  public Button nextMessageButton;
  public Button previousMessageButton;
  public Animator highlightAnimation;
  private bool mRefreshFlag;
  private bool mRefreshMailListSelectionToggle;
  private bool mRecreateListFlag;
  private int mDaysInCurrentYear;
  private MessageManager mMessageManager;
  private Message mOpenMessage;
  private Message mPreviousMessage;
  private bool mHasActiveGroupFilter;
  private UIMailMessageBody[] messageBody;
  private UIMailMessageHeader[] messageHeader;

  public bool refreshMailListSelectionToggle
  {
    set
    {
      this.mRefreshMailListSelectionToggle = value;
    }
  }

  public UIMailMessageBody currentMailBody
  {
    get
    {
      if (this.mOpenMessage != null)
        return this.GetBody(this.mOpenMessage.bodyType);
      return (UIMailMessageBody) null;
    }
  }

  public override void OnStart()
  {
    base.OnStart();
    this.inboxList.OnStart();
    MessageManager.NewMessage -= new Action<Message[]>(this.AddMessage);
    MessageManager.NewMessage += new Action<Message[]>(this.AddMessage);
    MessageManager.OnOldMessagesRemoved -= new Action(this.SetRecreateList);
    MessageManager.OnOldMessagesRemoved += new Action(this.SetRecreateList);
    Game.OnNewGame -= new Action(this.RecreateMailEntries);
    Game.OnNewGame += new Action(this.RecreateMailEntries);
    Game.OnGameDataChanged -= new Action(this.RecreateMailEntries);
    Game.OnGameDataChanged += new Action(this.RecreateMailEntries);
    this.CreateToggleList();
    if (Game.IsActive())
      this.RefreshMailListEntries();
    this.urgentToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleChange));
    this.noFilterToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleChange));
    this.nextMessageButton.onClick.AddListener(new UnityAction(this.OnPreviousMessageButton));
    this.previousMessageButton.onClick.AddListener(new UnityAction(this.OnNextMessageButton));
    this.noFilterToggle.isOn = true;
    this.messageBody = this.messageBodyContainer.GetComponentsInChildren<UIMailMessageBody>(true);
    this.messageHeader = this.messageHeaderContainer.GetComponentsInChildren<UIMailMessageHeader>(true);
  }

  private void SetRecreateList()
  {
    this.mRecreateListFlag = true;
  }

  private void RecreateMailEntries()
  {
    this.mRefreshFlag = true;
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.mRefreshFlag = true;
    this.mRefreshMailListSelectionToggle = true;
    this.mMessageManager = Game.instance.messageManager;
    this.showNavigationBars = true;
    this.CloseAllMessageBodies();
    this.UpdateMailFilter();
    this.UpdateToggleText();
    if (UIManager.instance.navigationType != UIManager.NavigationType.Normal)
      this.data = (Entity) this.mPreviousMessage;
    if (this.data != null)
      this.OpenMail((Message) this.data, true);
    else if (this.mMessageManager.count > 0 && !this.SelectNextMessage())
      this.OpenMail(this.mMessageManager.GetEntity(this.mMessageManager.count - 1), true);
    this.mPreviousMessage = (Message) null;
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
    Game.instance.time.OnDayEnd += new Action(this.RefreshDateLabel);
  }

  public override void OnExit()
  {
    base.OnExit();
    this.mPreviousMessage = this.mOpenMessage;
    this.mOpenMessage = (Message) null;
    this.CloseAllMessageBodies();
    Game.instance.time.OnDayEnd -= new Action(this.RefreshDateLabel);
  }

  private void RefreshDateLabel()
  {
    for (int index = 0; index < this.mMailEntries.Count; ++index)
      this.mMailEntries[index].RefreshDateLabel();
  }

  private bool SelectNextMessage()
  {
    bool flag = this.mOpenMessage == null;
    if (!flag)
      flag = !this.mOpenMessage.mustRespond || this.mOpenMessage.mustRespond && this.mOpenMessage.responded;
    if (!flag)
      return false;
    if (!this.SelectNextUnreadMessage())
      return this.SelectNextUnrespondendMessage();
    return true;
  }

  private bool SelectNextUnrespondendMessage()
  {
    for (int inIndex = 0; inIndex < this.mMessageManager.count; ++inIndex)
    {
      Message entity = this.mMessageManager.GetEntity(inIndex);
      if (!entity.responded && entity.mustRespond)
      {
        this.OpenMail(entity, true);
        this.highlightAnimation.SetTrigger(AnimationHashes.Play);
        return true;
      }
    }
    return false;
  }

  private void OnNextMessageButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    for (int inIndex = 0; inIndex < this.mMailEntries.Count; ++inIndex)
    {
      if (this.mMailEntries[inIndex].message == this.mOpenMessage)
      {
        this.OpenNextActiveMessage(inIndex, -1);
        break;
      }
    }
  }

  private void OnPreviousMessageButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    for (int inIndex = 0; inIndex < this.mMailEntries.Count; ++inIndex)
    {
      if (this.mMailEntries[inIndex].message == this.mOpenMessage)
      {
        this.OpenNextActiveMessage(inIndex, 1);
        break;
      }
    }
  }

  private void OpenNextActiveMessage(int inIndex, int inOffset)
  {
    inIndex = this.AddAndLoopIndex(inIndex, inOffset);
    int num = 0;
    while (!this.mMailEntries[inIndex].gameObject.activeSelf)
    {
      inIndex = this.AddAndLoopIndex(inIndex, inOffset);
      ++num;
      if (num > this.mMailEntries.Count)
        return;
    }
    this.OpenMail(this.mMailEntries[inIndex].message, true);
  }

  private int AddAndLoopIndex(int inIndex, int inOffset)
  {
    inIndex += inOffset;
    if (inIndex < 0)
      inIndex = this.mMailEntries.Count - 1;
    else if (inIndex >= this.mMailEntries.Count)
      inIndex = 0;
    return inIndex;
  }

  private void CreateToggleList()
  {
    for (Message.Group inGroup = Message.Group.Media; inGroup < Message.Group.Count; ++inGroup)
    {
      UIMailToggleEntry listItem = this.toggleList.CreateListItem<UIMailToggleEntry>();
      listItem.Setup(inGroup);
      listItem.toggle.group = this.toggleGroup;
      listItem.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleChange));
    }
    for (MailScreen.TimeSeparator timeSeparator = MailScreen.TimeSeparator.All; timeSeparator < MailScreen.TimeSeparator.Count; ++timeSeparator)
    {
      UIMailDateSeparator listItem = this.datesToggleList.CreateListItem<UIMailDateSeparator>();
      listItem.Setup(timeSeparator);
      listItem.button.group = this.datesToggleList.transform.parent.GetComponent<ToggleGroup>();
      this.mDateSeparators.Add(timeSeparator, listItem);
      if (timeSeparator == MailScreen.TimeSeparator.All)
        listItem.button.isOn = true;
    }
  }

  public void UpdateToggleText()
  {
    for (int inIndex = 0; inIndex < 14; ++inIndex)
    {
      UIMailToggleEntry uiMailToggleEntry = this.toggleList.GetItem<UIMailToggleEntry>(inIndex);
      if ((UnityEngine.Object) uiMailToggleEntry != (UnityEngine.Object) null)
        uiMailToggleEntry.UpdateText();
    }
    int num1 = 0;
    int num2 = 0;
    for (int inIndex = 0; inIndex < this.mMessageManager.count; ++inIndex)
    {
      Message entity = this.mMessageManager.GetEntity(inIndex);
      if (entity.priority == Message.Priority.Urgent && !entity.responded)
        ++num1;
      if (!entity.hasBeenRead)
        ++num2;
    }
    if (!((UnityEngine.Object) this.urgentTextLabel != (UnityEngine.Object) null) || !((UnityEngine.Object) this.noFilterTextLabel != (UnityEngine.Object) null))
      return;
    if (num1 != 0)
      this.urgentTextLabel.text = Localisation.LocaliseID("PSG_10010369", (GameObject) null) + " (" + (object) num1 + ")";
    else
      this.urgentTextLabel.text = Localisation.LocaliseID("PSG_10010369", (GameObject) null);
    if (num2 != 0)
      this.noFilterTextLabel.text = Localisation.LocaliseID("PSG_10009357", (GameObject) null) + " (" + (object) num2 + ")";
    else
      this.noFilterTextLabel.text = Localisation.LocaliseID("PSG_10009357", (GameObject) null);
  }

  private void OnToggleChange(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!inValue || !this.hasFocus)
      return;
    this.UpdateMailFilter();
  }

  private void SetDefaultFilters()
  {
    for (int inIndex = 0; inIndex < 14; ++inIndex)
    {
      UIMailToggleEntry uiMailToggleEntry = this.toggleList.GetItem<UIMailToggleEntry>(inIndex);
      if ((UnityEngine.Object) uiMailToggleEntry != (UnityEngine.Object) null)
        uiMailToggleEntry.toggle.isOn = false;
    }
    for (MailScreen.TimeSeparator index = MailScreen.TimeSeparator.All; index < MailScreen.TimeSeparator.Count; ++index)
      this.mDateSeparators[index].button.isOn = index == MailScreen.TimeSeparator.All;
    this.urgentToggle.isOn = false;
    this.noFilterToggle.isOn = true;
  }

  private void RefreshGroupFilter()
  {
    this.mHasActiveGroupFilter = false;
    for (int inIndex = 0; inIndex < 14; ++inIndex)
    {
      UIMailToggleEntry uiMailToggleEntry = this.toggleList.GetItem<UIMailToggleEntry>(inIndex);
      if ((UnityEngine.Object) uiMailToggleEntry != (UnityEngine.Object) null && uiMailToggleEntry.toggle.isOn)
      {
        this.mMessageGroupFilter = (Message.Group) inIndex;
        this.mHasActiveGroupFilter = true;
      }
    }
  }

  public void UpdateMailFilter()
  {
    this.RefreshGroupFilter();
    bool flag1 = this.noFilterToggle.isOn && !this.urgentToggle.isOn && !this.mHasActiveGroupFilter;
    bool isOn = this.urgentToggle.isOn;
    MailScreen.TimeSeparator timeSeparator = MailScreen.TimeSeparator.Count;
    for (MailScreen.TimeSeparator index = MailScreen.TimeSeparator.All; index < MailScreen.TimeSeparator.Count; ++index)
    {
      if (this.mDateSeparators[index].button.isOn)
      {
        timeSeparator = index;
        break;
      }
    }
    Dictionary<MailScreen.TimeSeparator, int> inDictionary = new Dictionary<MailScreen.TimeSeparator, int>();
    for (int index = 0; index < this.mMailEntries.Count; ++index)
    {
      UIMailEntry mMailEntry = this.mMailEntries[index];
      MailScreen.TimeSeparator timeSperatorType = this.GetTimeSperatorType(mMailEntry.message.deliverDate);
      bool flag2 = timeSeparator == MailScreen.TimeSeparator.ThisMonth && (timeSperatorType == MailScreen.TimeSeparator.ThisWeek || timeSperatorType == MailScreen.TimeSeparator.PreviousWeek);
      this.CountTimeSeparatorsForToggleActivation(inDictionary, timeSperatorType);
      if (!flag2 && timeSeparator != MailScreen.TimeSeparator.Count && (timeSeparator != MailScreen.TimeSeparator.All && timeSperatorType != timeSeparator))
      {
        GameUtility.SetActive(mMailEntry.gameObject, false);
      }
      else
      {
        bool flag3 = mMailEntry.message.priority == Message.Priority.Urgent;
        bool flag4 = this.mHasActiveGroupFilter && mMailEntry.message.group == this.mMessageGroupFilter;
        bool inIsActive = flag1 || isOn && flag3 || flag4;
        GameUtility.SetActive(mMailEntry.gameObject, inIsActive);
      }
    }
    for (MailScreen.TimeSeparator key = MailScreen.TimeSeparator.All; key < MailScreen.TimeSeparator.Count; ++key)
    {
      bool inValue = false;
      if (inDictionary.ContainsKey(key))
        inValue = inDictionary[key] > 0;
      if (key == MailScreen.TimeSeparator.All)
        inValue = true;
      this.mDateSeparators[key].SetActive(inValue);
    }
    this.nextMessageButton.interactable = this.HasMessageActive();
    this.previousMessageButton.interactable = this.HasMessageActive();
    this.noEntriesLabel.SetActive(!this.HasMessageActive());
  }

  private void CountTimeSeparatorsForToggleActivation(Dictionary<MailScreen.TimeSeparator, int> inDictionary, MailScreen.TimeSeparator inTimeFilter)
  {
    if (inTimeFilter == MailScreen.TimeSeparator.ThisWeek || inTimeFilter == MailScreen.TimeSeparator.PreviousWeek)
    {
      if (!inDictionary.ContainsKey(MailScreen.TimeSeparator.ThisMonth))
        inDictionary.Add(MailScreen.TimeSeparator.ThisMonth, 0);
      Dictionary<MailScreen.TimeSeparator, int> dictionary;
      MailScreen.TimeSeparator index;
      (dictionary = inDictionary)[index = MailScreen.TimeSeparator.ThisMonth] = dictionary[index] + 1;
    }
    if (!inDictionary.ContainsKey(inTimeFilter))
      inDictionary.Add(inTimeFilter, 0);
    Dictionary<MailScreen.TimeSeparator, int> dictionary1;
    MailScreen.TimeSeparator index1;
    (dictionary1 = inDictionary)[index1 = inTimeFilter] = dictionary1[index1] + 1;
  }

  private bool HasMessageActive()
  {
    for (int index = 0; index < this.mMailEntries.Count; ++index)
    {
      if (this.mMailEntries[index].gameObject.activeSelf)
        return true;
    }
    return false;
  }

  private void AddMessage(params Message[] inMessages)
  {
    this.mRefreshFlag = true;
    this.mRefreshMailListSelectionToggle = true;
  }

  private void LateUpdate()
  {
    this.RefreshListIfNeeded();
  }

  private void RefreshListIfNeeded()
  {
    if (!this.mRecreateListFlag)
      ;
    if (this.mRefreshFlag)
    {
      this.mRefreshFlag = false;
      this.RefreshMailListEntries();
      if (this.hasFocus)
      {
        this.UpdateMailFilter();
        this.UpdateToggleText();
      }
      UIManager.instance.navigationBars.bottomBar.MarkContinueButtonForUpdate();
    }
    if (!this.mRefreshMailListSelectionToggle)
      return;
    this.mRefreshMailListSelectionToggle = false;
    this.RefreshMailListSelectionToggles();
  }

  private void RefreshMailListSelectionToggles()
  {
    for (int index = 0; index < this.mMailEntries.Count; ++index)
    {
      UIMailEntry mMailEntry = this.mMailEntries[index];
      mMailEntry.toggle.onValueChanged.RemoveAllListeners();
      mMailEntry.toggle.isOn = mMailEntry.message != null && mMailEntry.message == this.mOpenMessage;
      mMailEntry.SetupListeners();
    }
  }

  private void OpenMail(Message inMessage, bool inSnapToScrollPosition)
  {
    this.mRefreshMailListSelectionToggle = true;
    this.mOpenMessage = inMessage;
    if (!this.mOpenMessage.hasBeenRead)
      Game.instance.notificationManager.GetNotification("UnreadMessages").DecrementCount();
    this.mOpenMessage.hasBeenRead = true;
    this.mOpenMessage.showNotification = false;
    this.ChooseMailFormat();
    this.UpdateToggleText();
    for (int index = 0; index < this.mMailEntries.Count; ++index)
    {
      UIMailEntry mMailEntry = this.mMailEntries[index];
      if ((UnityEngine.Object) mMailEntry != (UnityEngine.Object) null && mMailEntry.message == this.mOpenMessage && inSnapToScrollPosition)
      {
        if (!mMailEntry.gameObject.activeSelf)
          this.SetDefaultFilters();
        GameUtility.SnapScrollRectTo(mMailEntry.GetComponent<RectTransform>(), this.inboxScrollRect, GameUtility.AnchorType.Y, this.inboxListSnapMode);
      }
    }
    UIManager.instance.navigationBars.bottomBar.MarkContinueButtonForUpdate();
  }

  private void ChooseMailFormat()
  {
    this.CloseAllMessageBodies();
    UIMailMessageBody body = this.GetBody(this.mOpenMessage.bodyType);
    if ((UnityEngine.Object) body == (UnityEngine.Object) null)
      body = this.GetBody(Message.BodyType.Standard);
    body.OpenMail(this.mOpenMessage);
    UIMailMessageHeader header = this.GetHeader(this.mOpenMessage.headerType);
    if ((UnityEngine.Object) header == (UnityEngine.Object) null)
      header = this.GetHeader(Message.HeaderType.Standard);
    header.OpenMail(this.mOpenMessage);
  }

  private UIMailMessageBody GetBody(Message.BodyType inType)
  {
    for (int index = 0; index < this.messageBody.Length; ++index)
    {
      if (this.messageBody[index].bodyType == inType)
        return this.messageBody[index];
    }
    return (UIMailMessageBody) null;
  }

  private UIMailMessageHeader GetHeader(Message.HeaderType inType)
  {
    for (int index = 0; index < this.messageHeader.Length; ++index)
    {
      if (this.messageHeader[index].headerType == inType)
        return this.messageHeader[index];
    }
    return (UIMailMessageHeader) null;
  }

  private MailScreen.TimeSeparator GetTimeSperatorType(DateTime inMessageDate)
  {
    float num1 = 52f;
    int num2 = Mathf.FloorToInt((float) inMessageDate.DayOfYear / (float) this.mDaysInCurrentYear * num1);
    int num3 = Mathf.FloorToInt((float) Game.instance.time.now.DayOfYear / (float) this.mDaysInCurrentYear * num1);
    if (num2 == num3)
      return MailScreen.TimeSeparator.ThisWeek;
    if (num2 == num3 - 1)
      return MailScreen.TimeSeparator.PreviousWeek;
    if (inMessageDate.Year == Game.instance.time.now.Year && inMessageDate.Month == Game.instance.time.now.Month)
      return MailScreen.TimeSeparator.ThisMonth;
    return inMessageDate.Year == Game.instance.time.now.Year && inMessageDate.Month == Game.instance.time.now.Month - 1 ? MailScreen.TimeSeparator.PreviousMonth : MailScreen.TimeSeparator.All;
  }

  private int GetTotalDaysInCurrentYear(DateTime inDate)
  {
    DateTime dateTime = new DateTime(inDate.Year, 1, 1);
    return (int) (new DateTime(inDate.Year, 12, 31) - dateTime).TotalDays;
  }

  private void RefreshMailListEntries()
  {
    for (MailScreen.TimeSeparator index = MailScreen.TimeSeparator.All; index < MailScreen.TimeSeparator.Count; ++index)
      this.mDateSeparators[index].messageEntries.Clear();
    this.mMailEntries.Clear();
    this.inboxList.HideListItems();
    this.mDaysInCurrentYear = this.GetTotalDaysInCurrentYear(Game.instance.time.now);
    List<Message> entityList = Game.instance.messageManager.GetEntityList();
    int inItemCount = 0;
    for (int index = entityList.Count - 1; index >= 0; --index)
    {
      Message inMessage = entityList[index];
      if (inMessage.deliverDate <= Game.instance.time.now)
      {
        this.SetMessageEntry(inMessage, inItemCount);
        ++inItemCount;
      }
    }
  }

  private void SetMessageEntry(Message inMessage, int inItemCount)
  {
    Message inMessage1 = inMessage;
    UIMailEntry uiMailEntry = this.inboxList.GetOrCreateItem<UIMailEntry>(inItemCount);
    uiMailEntry.Setup(inMessage1);
    uiMailEntry.OpenMessage += new Action<Message, bool>(this.OpenMail);
    this.mDateSeparators[this.GetTimeSperatorType(inMessage1.deliverDate)].messageEntries.Add(uiMailEntry);
    this.mDateSeparators[MailScreen.TimeSeparator.All].messageEntries.Add(uiMailEntry);
    this.mMailEntries.Insert(0, uiMailEntry);
    GameUtility.SetActive(uiMailEntry.gameObject, true);
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    if (this.SelectNextMessage())
      return UIScreen.NavigationButtonEvent.HandledByScreen;
    if (this.mOpenMessage != null && this.mOpenMessage.mustRespond && !this.mOpenMessage.responded)
      this.highlightAnimation.SetTrigger(AnimationHashes.Play);
    return base.OnContinueButton();
  }

  private bool SelectNextUnreadMessage()
  {
    if (Game.instance.notificationManager.GetNotification("UnreadMessages").count > 0)
    {
      for (int inIndex = 0; inIndex < this.mMessageManager.count; ++inIndex)
      {
        Message entity = this.mMessageManager.GetEntity(inIndex);
        if (!entity.hasBeenRead)
        {
          this.OpenMail(entity, true);
          return true;
        }
      }
    }
    return false;
  }

  private void CloseAllMessageBodies()
  {
    for (int index = 0; index < this.messageHeader.Length; ++index)
      this.messageHeader[index].Close();
    for (int index = 0; index < this.messageBody.Length; ++index)
      this.messageBody[index].Close();
  }

  public bool IsOpenMessageMustRespond()
  {
    if (this.mOpenMessage != null && this.mOpenMessage.mustRespond)
      return !this.mOpenMessage.responded;
    return false;
  }

  private void OnDestroy()
  {
    Game.OnNewGame -= new Action(this.RecreateMailEntries);
    Game.OnGameDataChanged -= new Action(this.RecreateMailEntries);
    MessageManager.NewMessage -= new Action<Message[]>(this.AddMessage);
    MessageManager.OnOldMessagesRemoved -= new Action(this.SetRecreateList);
    if (!Game.IsActive())
      return;
    Game.instance.time.OnDayEnd -= new Action(this.RefreshDateLabel);
  }

  public void SetMediaGroup(Message.Group inMessageGroupFilter)
  {
    this.mMessageGroupFilter = inMessageGroupFilter;
  }

  public static Color GetBackingColor(Message.Group inGroup)
  {
    Color color = new Color();
    switch (inGroup)
    {
      case Message.Group.Media:
        color = UIConstants.mailMedia;
        break;
      case Message.Group.Gossip:
        color = UIConstants.mailGossip;
        break;
      case Message.Group.Politics:
        color = UIConstants.mailPolitics;
        break;
      case Message.Group.Assistant:
      case Message.Group.Scout:
      case Message.Group.Drivers:
      case Message.Group.Mechanics:
      case Message.Group.LeadDesigner:
      case Message.Group.Chairman:
      case Message.Group.Staff:
      case Message.Group.Team:
        color = UIConstants.mailAssistant;
        break;
      case Message.Group.Championship:
        color = UIConstants.mailChampionship;
        break;
      case Message.Group.Contracts:
        color = UIConstants.mailContracts;
        break;
      case Message.Group.Other:
        color = UIConstants.mailOther;
        break;
    }
    return color;
  }

  public enum TimeSeparator
  {
    [LocalisationID("PSG_10007759")] All,
    [LocalisationID("PSG_10008934")] ThisWeek,
    [LocalisationID("PSG_10008935")] PreviousWeek,
    [LocalisationID("PSG_10008936")] ThisMonth,
    [LocalisationID("PSG_10008937")] PreviousMonth,
    Count,
  }
}
