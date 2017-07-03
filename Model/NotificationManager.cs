
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class NotificationManager : GenericManager<Notification>
{
  private List<Notification> mNotificationsDispachtList = new List<Notification>();
  public Action OnNotificationsChanged;
  private int mVisibleNotificationsCount;
    
}
