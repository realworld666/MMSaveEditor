using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Message : Entity
{
    public bool showNotification;
    public bool hasBeenRead;
    public bool mustRespond;
    public bool interruptGameTime;
    public int portraitId;
    public DateTime deliverDate;
    public GameState.Group deliverStateGroup;
    public MessageAction messageAction;
    public DialogRule buttonsRule;
    public TimeSpan reponseDelay = TimeSpan.Zero;
    public bool autoRespond;
    public bool responded;
    public List<TextDynamicData> textSpecialData = new List<TextDynamicData>();
    public TextDynamicData messageResponseData;
    public object[] specialObject = new object[1];
    private DilemmaSystem.DilemmaMessageData mDilemmaMessageData;
    private List<Message> mResponses = new List<Message>();
    private CalendarEvent_v1 mCalendarEvent;
    private Person mSender;
    private Person mReceiver;
    private string mDescription = string.Empty;
    private string mTitle = string.Empty;
    private Message.Group mGroup = Message.Group.Other;
    private Message.Priority mPriority;
    private Message.HeaderType mHeaderType;
    private Message.BodyType mBodyType;


    public enum Group
    {
        [LocalisationID("PSG_10001563")] Media,
        [LocalisationID("PSG_10009359")] Gossip,
        [LocalisationID("PSG_10010235")] Politics,
        [LocalisationID("PSG_10009360")] Assistant,
        [LocalisationID("PSG_10001580")] Scout,
        [LocalisationID("PSG_10000002")] Drivers,
        [LocalisationID("PSG_10001632")] Mechanics,
        [LocalisationID("PSG_10004602")] LeadDesigner,
        [LocalisationID("PSG_10002201")] Chairman,
        [LocalisationID("PSG_10003781")] Staff,
        [LocalisationID("PSG_10000005")] Team,
        [LocalisationID("PSG_10006838")] Championship,
        [LocalisationID("PSG_10009361")] Contracts,
        [LocalisationID("PSG_10010236")] Other,
        Count,
    }

    public enum Priority
    {
        Normal,
        Urgent,
    }

    public enum HeaderType
    {
        Standard,
        Media,
        Dilemma,
        PersonalityTrait,
        Part,
    }

    public enum BodyType
    {
        Standard,
        Dilemma,
        Media,
        PersonalityTrait,
        Part,
        GTSeries,
    }
}
