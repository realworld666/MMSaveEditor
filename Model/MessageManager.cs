
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class MessageManager : GenericManager<Message>
{
  private List<Message> mDelayedMessages = new List<Message>();
  private List<Message> mMessagesToDeliver = new List<Message>();
  public static Action<Message[]> NewMessage;
  public static Action OnOldMessagesRemoved;
  public bool skipedToEvent;
  [NonSerialized]
  private Notification mNotification;
    
}
