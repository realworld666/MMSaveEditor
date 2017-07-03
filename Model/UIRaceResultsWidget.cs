// Decompiled with JetBrains decompiler
// Type: UIRaceResultsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIRaceResultsWidget : UIResultsWidget
{
  public override void PopulateList(List<RaceEventResults.SessonResultData> inList)
  {
    this.resultsList.DestroyListItems();
    RaceEventResults.ResultData resultData1 = new RaceEventResults.ResultData();
    if (inList.Count <= 0)
      return;
    int count = inList[0].resultData.Count;
    RaceEventResults.ResultData inFirstPlaceEntry = inList[0].resultData[0];
    for (int index1 = 0; index1 < count; ++index1)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UIRaceResultsWidget.\u003CPopulateList\u003Ec__AnonStoreyA7 listCAnonStoreyA7 = new UIRaceResultsWidget.\u003CPopulateList\u003Ec__AnonStoreyA7();
      // ISSUE: reference to a compiler-generated field
      listCAnonStoreyA7.driverData = new RaceEventResults.ResultData();
      // ISSUE: reference to a compiler-generated field
      listCAnonStoreyA7.driverData = inList[0].resultData[index1];
      RaceEventResults.ResultData resultData2 = (RaceEventResults.ResultData) null;
      RaceEventResults.SessonResultData resultsForSession = Game.instance.sessionManager.eventDetails.results.GetResultsForSession(SessionDetails.SessionType.Qualifying);
      for (int index2 = 0; index2 < resultsForSession.resultData.Count; ++index2)
      {
        // ISSUE: reference to a compiler-generated field
        if (resultsForSession.resultData[index2].driver == listCAnonStoreyA7.driverData.driver)
        {
          resultData2 = resultsForSession.resultData[index2];
          break;
        }
      }
      UITimedRaceEntry listItem = this.resultsList.CreateListItem<UITimedRaceEntry>();
      listItem.barType = index1 % 2 != 0 ? UITimedRaceEntry.BarType.Darker : UITimedRaceEntry.BarType.Lighter;
      // ISSUE: reference to a compiler-generated field
      listItem.SetInfo(listCAnonStoreyA7.driverData, inFirstPlaceEntry, index1 + 1, resultData2.gridPosition);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      listCAnonStoreyA7.goToPlayerScreen = new Action(listCAnonStoreyA7.\u003C\u003Em__213);
      // ISSUE: reference to a compiler-generated method
      listItem.GetComponent<Button>().onClick.AddListener(new UnityAction(listCAnonStoreyA7.\u003C\u003Em__214));
    }
  }
}
