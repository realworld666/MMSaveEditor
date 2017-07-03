// Decompiled with JetBrains decompiler
// Type: UIQualifyPracticeTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIQualifyPracticeTable : MonoBehaviour
{
  public UIGridList gridList;
  public GameObject singleQualifyingHeader;
  public GameObject multipleQualifyingHeader;
  public GameObject singleQualifyingEntryPrefab;
  public GameObject multipleQualifyingEntryPrefab;
  public UIQualifyPracticeTable.EventType eventType;

  public void CreateTable()
  {
    this.gridList.DestroyListItems();
    RaceEventResults results = Game.instance.sessionManager.eventDetails.results;
    RaceEventResults.ResultData inFirstPlaceEntry = new RaceEventResults.ResultData();
    bool inIsActive = Game.instance.sessionManager.eventDetails.hasSeveralQualifyingSessions && this.eventType == UIQualifyPracticeTable.EventType.Qualifying;
    if (this.eventType == UIQualifyPracticeTable.EventType.Qualifying)
    {
      GameUtility.SetActive(this.multipleQualifyingHeader, inIsActive);
      GameUtility.SetActive(this.singleQualifyingHeader, !inIsActive);
    }
    if (this.eventType == UIQualifyPracticeTable.EventType.Qualifying)
      this.gridList.itemPrefab = !inIsActive ? this.singleQualifyingEntryPrefab : this.multipleQualifyingEntryPrefab;
    int num = 0;
    if (this.eventType == UIQualifyPracticeTable.EventType.Practice)
    {
      num = results.GetResultsForSession(SessionDetails.SessionType.Practice).resultData.Count;
      inFirstPlaceEntry = results.GetResultsForSession(SessionDetails.SessionType.Practice).resultData[0];
    }
    else if (this.eventType == UIQualifyPracticeTable.EventType.Qualifying)
    {
      num = results.GetResultsForSession(SessionDetails.SessionType.Qualifying).resultData.Count;
      inFirstPlaceEntry = results.GetResultsForSession(SessionDetails.SessionType.Qualifying).resultData[0];
    }
    for (int inPosition = 0; inPosition < num; ++inPosition)
    {
      RaceEventResults.ResultData inResultData = new RaceEventResults.ResultData();
      SessionDetails.SessionType inSessionType = SessionDetails.SessionType.Race;
      if (this.eventType == UIQualifyPracticeTable.EventType.Practice)
      {
        inResultData = results.GetResultsForSession(SessionDetails.SessionType.Practice).resultData[inPosition];
        inSessionType = SessionDetails.SessionType.Practice;
      }
      else if (this.eventType == UIQualifyPracticeTable.EventType.Qualifying)
      {
        inResultData = results.GetResultsForSession(SessionDetails.SessionType.Qualifying).resultData[inPosition];
        inSessionType = SessionDetails.SessionType.Qualifying;
      }
      if (inIsActive)
      {
        UIMultipleQualifyingEntry listItem = this.gridList.CreateListItem<UIMultipleQualifyingEntry>();
        listItem.barType = inPosition % 2 != 0 ? UIMultipleQualifyingEntry.BarType.Darker : UIMultipleQualifyingEntry.BarType.Lighter;
        listItem.SetInfo(inResultData, inFirstPlaceEntry, inPosition, inSessionType);
      }
      else
      {
        UIQualifyPracticeEntry listItem = this.gridList.CreateListItem<UIQualifyPracticeEntry>();
        listItem.barType = inPosition % 2 != 0 ? UIQualifyPracticeEntry.BarType.Darker : UIQualifyPracticeEntry.BarType.Lighter;
        listItem.SetInfo(inResultData, inFirstPlaceEntry, inPosition, inSessionType);
      }
    }
  }

  public enum EventType
  {
    Qualifying,
    Practice,
  }
}
