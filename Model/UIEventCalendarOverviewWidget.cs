// Decompiled with JetBrains decompiler
// Type: UIEventCalendarOverviewWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIEventCalendarOverviewWidget : MonoBehaviour
{
  public UIEventCalendarLayout layoutWidget;
  public UIEventCalendarTrackInfo trackInfoWidget;
  public UIEventCalendarResults resultsWidget;
  private RaceEventDetails mRaceEvent;
  private int mEventNumber;

  public void OnStart()
  {
    this.trackInfoWidget.OnStart();
    this.resultsWidget.OnStart();
  }

  public void Setup(RaceEventDetails inRaceEvent, int inEventNumber)
  {
    if (inRaceEvent == null)
      return;
    this.mRaceEvent = inRaceEvent;
    this.mEventNumber = inEventNumber;
    this.layoutWidget.Setup(this.mRaceEvent, this.mEventNumber);
    this.trackInfoWidget.Setup(this.mRaceEvent, this.mEventNumber);
    this.resultsWidget.Setup(this.mRaceEvent);
  }
}
