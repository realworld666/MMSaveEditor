// Decompiled with JetBrains decompiler
// Type: UIPracticeResultsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPracticeResultsWidget : UIResultsWidget
{
  public override void PopulateList(List<RaceEventResults.SessonResultData> inList)
  {
    this.resultsList.DestroyListItems();
    RaceEventResults.ResultData resultData = new RaceEventResults.ResultData();
    if (inList.Count <= 0)
      return;
    int count = inList[0].resultData.Count;
    RaceEventResults.ResultData inFirstPlaceEntry = inList[0].resultData[0];
    for (int inPosition = 0; inPosition < count; ++inPosition)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UIPracticeResultsWidget.\u003CPopulateList\u003Ec__AnonStoreyA3 listCAnonStoreyA3 = new UIPracticeResultsWidget.\u003CPopulateList\u003Ec__AnonStoreyA3();
      // ISSUE: reference to a compiler-generated field
      listCAnonStoreyA3.driverData = new RaceEventResults.ResultData();
      // ISSUE: reference to a compiler-generated field
      listCAnonStoreyA3.driverData = inList[0].resultData[inPosition];
      UIQualifyPracticeEntry listItem = this.resultsList.CreateListItem<UIQualifyPracticeEntry>();
      listItem.barType = inPosition % 2 != 0 ? UIQualifyPracticeEntry.BarType.Darker : UIQualifyPracticeEntry.BarType.Lighter;
      // ISSUE: reference to a compiler-generated field
      listItem.SetInfo(listCAnonStoreyA3.driverData, inFirstPlaceEntry, inPosition, SessionDetails.SessionType.Practice);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      listCAnonStoreyA3.goToPlayerScreen = new Action(listCAnonStoreyA3.\u003C\u003Em__20D);
      // ISSUE: reference to a compiler-generated method
      listItem.GetComponent<Button>().onClick.AddListener(new UnityAction(listCAnonStoreyA3.\u003C\u003Em__20E));
    }
  }
}
