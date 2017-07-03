using FullSerializer;
using System;
using System.Collections.Generic;
using MM2.UI;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Notification : Entity
{
  public string localisationID = string.Empty;
  public string screenReference = string.Empty;
  private List<Notification> mChildren = new List<Notification>();
  public CalendarEventTypeIconContainer.IconType iconType;
  private int mCount;
  private float mProgress;
  private bool mDisplay;
  private Notification mParent;
}
