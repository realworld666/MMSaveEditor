using System;
using FullSerializer;

[fsObject("v1", new System.Type[] { typeof(CalendarEvent) }, MemberSerialization = fsMemberSerialization.OptOut)]
public class CalendarEvent_v1
{
    public DateTime triggerDate;
    public DateTime triggerCacheDayDate;
    public CalendarEvent.UIState uiState;
    public GameState.Type triggerState;
    public MMAction OnButtonClick;
    public MMAction OnEventTrigger;
    public Message message;
    public EventEffect effect;
    public DisplayEffect displayEffect;
    public CalendarEventCategory category;
    private TextDynamicData mDynamicDescription = new TextDynamicData();
    private string mCachedDynamicDescriptionTextID;
    public bool mustRespond;
    public bool interruptGameTime;
    public bool showOnCalendar;


    public CalendarEvent_v1()
    {
        this.triggerDate = new DateTime();
        this.uiState = CalendarEvent.UIState.None;
        this.triggerState = GameState.Type.None;
        this.OnButtonClick = (MMAction)null;
        this.OnEventTrigger = (MMAction)null;
        this.message = (Message)null;
        this.effect = (EventEffect)null;
        this.displayEffect = (DisplayEffect)null;
        this.category = CalendarEventCategory.Unknown;
        this.mustRespond = false;
        this.interruptGameTime = false;
        this.showOnCalendar = false;
        this.mCachedDynamicDescriptionTextID = (string)null;
    }

    public CalendarEvent_v1(CalendarEvent v0)
    {
        this.triggerDate = v0.triggerDate;
        this.uiState = v0.uiState;
        this.triggerState = v0.triggerState;
        this.OnButtonClick = v0.OnButtonClick;
        this.OnEventTrigger = v0.OnEventTrigger;
        this.message = v0.message;
        this.effect = v0.effect;
        this.displayEffect = v0.displayEffect;
        this.category = v0.category;
        this.mCachedDynamicDescriptionTextID = v0.description;
        this.mustRespond = v0.mustRespond;
        this.interruptGameTime = v0.interruptGameTime;
        this.showOnCalendar = v0.showOnCalendar;
    }
}
