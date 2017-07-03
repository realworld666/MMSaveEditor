// Decompiled with JetBrains decompiler
// Type: MM2.UI.CalendarDayEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MM2.UI
{
  internal class CalendarDayEvent : MonoBehaviour
  {
    [SerializeField]
    private GameObject regularContainer;
    [SerializeField]
    private GameObject smallContainer;
    [SerializeField]
    private TextMeshProUGUI smallDescriptionText;
    [SerializeField]
    private Button button;
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI descriptionText;
    [SerializeField]
    private RectTransform pausingIcon;
    [SerializeField]
    private RectTransform mustRespond;
    [SerializeField]
    private CanvasGroup iconCanvasGroup;
    [SerializeField]
    private CalendarEventTypeIconContainer iconContainer;
    private CalendarEvent_v1 calendarEvent;

    public void Reset()
    {
      this.calendarEvent = (CalendarEvent_v1) null;
    }

    public void Setup(CalendarEvent_v1 inCalendarEvent, bool useSmallVersion)
    {
      this.calendarEvent = inCalendarEvent;
      GameUtility.SetActive(this.regularContainer, !useSmallVersion);
      GameUtility.SetActive(this.smallContainer, useSmallVersion);
      this.button.onClick.RemoveAllListeners();
      this.button.onClick.AddListener(new UnityAction(this.OnButton));
      this.button.enabled = false;
      this.button.enabled = true;
      if (!useSmallVersion)
      {
        this.timeText.text = this.calendarEvent.triggerDate.ToString("hh:mm tt");
        this.descriptionText.text = this.calendarEvent.GetDescription();
        GameUtility.SetActive(this.pausingIcon.gameObject, this.calendarEvent.interruptGameTime && !this.calendarEvent.mustRespond);
        GameUtility.SetActive(this.mustRespond.gameObject, this.calendarEvent.interruptGameTime && this.calendarEvent.mustRespond);
        this.iconCanvasGroup.alpha = this.calendarEvent.category != CalendarEventCategory.Mail || this.calendarEvent.message == null || !this.calendarEvent.message.hasBeenRead ? 1f : 0.5f;
        this.iconContainer.SetIcon(CalendarEventTypeIconContainer.IconForEventCategory(this.calendarEvent.category));
      }
      else
        this.smallDescriptionText.text = this.calendarEvent.GetDescription();
    }

    public void UpdatePausingIcon()
    {
      GameUtility.SetActive(this.pausingIcon.gameObject, this.calendarEvent.interruptGameTime || Game.instance.calendar.ShouldPauseOnEventCategory(this.calendarEvent.category));
    }

    private void OnButton()
    {
      scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
      if (this.calendarEvent == null || this.calendarEvent.OnButtonClick == null || Game.instance.time.timeState == GameTimer.TimeState.Skip)
        return;
      Game.instance.time.Pause(GameTimer.PauseType.Game);
      UIManager.instance.navigationBars.bottomBar.calendarButton.SetCalendarVisibility(false);
      this.calendarEvent.OnButtonClick.Invoke();
    }
  }
}
