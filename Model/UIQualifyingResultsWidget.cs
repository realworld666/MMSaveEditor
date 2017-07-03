// Decompiled with JetBrains decompiler
// Type: UIQualifyingResultsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIQualifyingResultsWidget : UIResultsWidget
{
  public GameObject singleQualifyingHeader;
  public GameObject multipleQualifyingHeader;
  public GameObject singleQualifyingEntryPrefab;
  public GameObject multipleQualifyingEntryPrefab;

  public override void PopulateList(List<RaceEventResults.SessonResultData> inList)
  {
    bool qualifyingSessions = Game.instance.sessionManager.eventDetails.hasSeveralQualifyingSessions;
    GameUtility.SetActive(this.multipleQualifyingHeader, qualifyingSessions);
    GameUtility.SetActive(this.singleQualifyingHeader, !qualifyingSessions);
    this.resultsList.itemPrefab = !qualifyingSessions ? this.singleQualifyingEntryPrefab : this.multipleQualifyingEntryPrefab;
    this.resultsList.DestroyListItems();
    RaceEventResults.ResultData resultData = new RaceEventResults.ResultData();
    if (inList.Count <= 0)
      return;
    int index = inList.Count - 1;
    int count = inList[index].resultData.Count;
    RaceEventResults.ResultData inFirstPlaceEntry = inList[index].resultData[0];
    for (int inPosition = 0; inPosition < count; ++inPosition)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UIQualifyingResultsWidget.\u003CPopulateList\u003Ec__AnonStoreyA4 listCAnonStoreyA4 = new UIQualifyingResultsWidget.\u003CPopulateList\u003Ec__AnonStoreyA4();
      // ISSUE: reference to a compiler-generated field
      listCAnonStoreyA4.driverData = new RaceEventResults.ResultData();
      // ISSUE: reference to a compiler-generated field
      listCAnonStoreyA4.driverData = inList[index].resultData[inPosition];
      if (qualifyingSessions)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        UIQualifyingResultsWidget.\u003CPopulateList\u003Ec__AnonStoreyA5 listCAnonStoreyA5 = new UIQualifyingResultsWidget.\u003CPopulateList\u003Ec__AnonStoreyA5();
        // ISSUE: reference to a compiler-generated field
        listCAnonStoreyA5.\u003C\u003Ef__ref\u0024164 = listCAnonStoreyA4;
        UIMultipleQualifyingEntry listItem = this.resultsList.CreateListItem<UIMultipleQualifyingEntry>();
        listItem.barType = inPosition % 2 != 0 ? UIMultipleQualifyingEntry.BarType.Darker : UIMultipleQualifyingEntry.BarType.Lighter;
        // ISSUE: reference to a compiler-generated field
        listItem.SetInfo(listCAnonStoreyA4.driverData, inFirstPlaceEntry, inPosition, SessionDetails.SessionType.Qualifying);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated method
        listCAnonStoreyA5.goToPlayerScreen = new Action(listCAnonStoreyA5.\u003C\u003Em__20F);
        // ISSUE: reference to a compiler-generated method
        listItem.GetComponent<Button>().onClick.AddListener(new UnityAction(listCAnonStoreyA5.\u003C\u003Em__210));
      }
      else
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        UIQualifyingResultsWidget.\u003CPopulateList\u003Ec__AnonStoreyA6 listCAnonStoreyA6 = new UIQualifyingResultsWidget.\u003CPopulateList\u003Ec__AnonStoreyA6();
        // ISSUE: reference to a compiler-generated field
        listCAnonStoreyA6.\u003C\u003Ef__ref\u0024164 = listCAnonStoreyA4;
        UIQualifyPracticeEntry listItem = this.resultsList.CreateListItem<UIQualifyPracticeEntry>();
        listItem.barType = inPosition % 2 != 0 ? UIQualifyPracticeEntry.BarType.Darker : UIQualifyPracticeEntry.BarType.Lighter;
        // ISSUE: reference to a compiler-generated field
        listItem.SetInfo(listCAnonStoreyA4.driverData, inFirstPlaceEntry, inPosition, SessionDetails.SessionType.Qualifying);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated method
        listCAnonStoreyA6.goToPlayerScreen = new Action(listCAnonStoreyA6.\u003C\u003Em__211);
        // ISSUE: reference to a compiler-generated method
        listItem.GetComponent<Button>().onClick.AddListener(new UnityAction(listCAnonStoreyA6.\u003C\u003Em__212));
      }
    }
  }
}
