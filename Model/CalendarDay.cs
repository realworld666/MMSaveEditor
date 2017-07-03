// Decompiled with JetBrains decompiler
// Type: MM2.UI.CalendarDay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MM2.UI
{
  internal class CalendarDay : MonoBehaviour
  {
    private const float mTolerance = 0.01f;
    [SerializeField]
    private RectTransform backingImageNotCurrentDay;
    [SerializeField]
    private RectTransform backingImageCurrentDay;
    [SerializeField]
    private Image dayCycleBarImage;
    [SerializeField]
    private TextMeshProUGUI dateText;
    [SerializeField]
    private TextMeshProUGUI dayOfWeekText;
    [SerializeField]
    private TextMeshProUGUI monthText;
    [SerializeField]
    private UIGridList eventsList;
    private bool mIsToday;

    public DateTime date { get; private set; }

    public void Reset()
    {
      this.eventsList.HideListItems();
    }

    public void Setup(DateTime inDate)
    {
      this.date = inDate;
      this.mIsToday = this.date.Date == Game.instance.time.now.Date;
      GameUtility.SetActive(this.dayCycleBarImage.gameObject, this.mIsToday);
      GameUtility.SetImageFillAmountIfDifferent(this.dayCycleBarImage, !this.mIsToday ? 0.0f : CalendarDay.ProportionThroughDay01(Game.instance.time.now), 1f / 512f);
      GameUtility.SetActive(this.backingImageCurrentDay.gameObject, this.mIsToday);
      GameUtility.SetActive(this.backingImageNotCurrentDay.gameObject, !this.mIsToday);
      this.dateText.text = this.date.Day.ToString();
      this.dayOfWeekText.text = GameUtility.GetLocalisedDay(this.date);
      using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
      {
        StringBuilder stringBuilder = builderSafe.stringBuilder;
        stringBuilder.Append(GameUtility.GetLocalisedMonth(this.date));
        stringBuilder.Append(" ");
        stringBuilder.Append(this.date.Year.ToString("0000"));
        this.monthText.text = stringBuilder.ToString();
      }
      this.AddEventsFromCalendar(this.date);
    }

    public void UpdatePausingIcon()
    {
      int itemCount = this.eventsList.itemCount;
      for (int inIndex = 0; inIndex < itemCount; ++inIndex)
        this.eventsList.GetItem<CalendarDayEvent>(inIndex).UpdatePausingIcon();
    }

    private void AddEventsFromCalendar(DateTime inDate)
    {
      List<CalendarEvent_v1> eventsOnDay = Game.instance.calendar.GetEventsOnDay(inDate, true);
      eventsOnDay.Sort();
      int count = eventsOnDay.Count;
      int num = Mathf.Max(this.eventsList.itemCount, count);
      for (int inIndex = 0; inIndex < num; ++inIndex)
      {
        CalendarEvent_v1 inCalendarEvent = inIndex >= count ? (CalendarEvent_v1) null : eventsOnDay[inIndex];
        CalendarDayEvent calendarDayEvent = this.eventsList.GetOrCreateItem<CalendarDayEvent>(inIndex);
        if (inCalendarEvent != null)
        {
          bool useSmallVersion = inCalendarEvent.category == CalendarEventCategory.Race && (inCalendarEvent.displayEffect == null || !((ChampionshipDisplayEffect) inCalendarEvent.displayEffect).championship.isPlayerChampionship);
          calendarDayEvent.Setup(inCalendarEvent, useSmallVersion);
        }
        GameUtility.SetActive(calendarDayEvent.gameObject, inCalendarEvent != null);
      }
    }

    private void Update()
    {
      if (!this.mIsToday)
        return;
      GameUtility.SetImageFillAmountIfDifferent(this.dayCycleBarImage, CalendarDay.ProportionThroughDay01(Game.instance.time.now), 0.01f);
    }

    public static float ProportionThroughDay01(DateTime now)
    {
      return (float) (now.TimeOfDay.TotalSeconds / 86400.0);
    }
  }
}
