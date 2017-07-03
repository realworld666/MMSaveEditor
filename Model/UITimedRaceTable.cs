// Decompiled with JetBrains decompiler
// Type: UITimedRaceTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UITimedRaceTable : MonoBehaviour
{
  public UIGridList gridList;
  public TextMeshProUGUI fastestLapLabel;

  public void SetFastestLapLabel(RaceEventResults.ResultData inData)
  {
    int num = 1;
    this.fastestLapLabel.text = "Fastest Lap: " + inData.driver.name + " " + GameUtility.GetLapTimeText(inData.bestLapTime, false) + " LAP(" + num.ToString() + ")";
  }

  public void CreateTable()
  {
    this.gridList.HideListItems();
    RaceEventResults results = Game.instance.sessionManager.eventDetails.results;
    RaceEventResults.ResultData resultData1 = new RaceEventResults.ResultData();
    int count = results.GetResultsForSession(SessionDetails.SessionType.Race).resultData.Count;
    RaceEventResults.ResultData inFirstPlaceEntry = results.GetResultsForSession(SessionDetails.SessionType.Race).resultData[0];
    for (int inIndex = 0; inIndex < count; ++inIndex)
    {
      RaceEventResults.ResultData resultData2 = results.GetResultsForSession(SessionDetails.SessionType.Race).resultData[inIndex];
      RaceEventResults.ResultData resultData3 = (RaceEventResults.ResultData) null;
      RaceEventResults.SessonResultData resultsForSession = results.GetResultsForSession(SessionDetails.SessionType.Qualifying);
      for (int index = 0; index < resultsForSession.resultData.Count; ++index)
      {
        if (resultsForSession.resultData[index].driver == resultData2.driver)
          resultData3 = resultsForSession.resultData[index];
      }
      if (resultData3 == null)
        resultData3 = resultsForSession.resultData[0];
      UITimedRaceEntry uiTimedRaceEntry = this.gridList.GetOrCreateItem<UITimedRaceEntry>(inIndex);
      uiTimedRaceEntry.barType = inIndex % 2 != 0 ? UITimedRaceEntry.BarType.Darker : UITimedRaceEntry.BarType.Lighter;
      if (!Game.instance.player.team.championship.rules.qualifyingBasedActive)
        uiTimedRaceEntry.SetInfo(resultData2, inFirstPlaceEntry, inIndex + 1, resultData3.gridPosition);
      else
        uiTimedRaceEntry.SetInfo(resultData2, inFirstPlaceEntry, inIndex + 1, resultData3.position);
      GameUtility.SetActive(uiTimedRaceEntry.gameObject, true);
      if (resultData2.sessionFastestLap)
        this.SetFastestLapLabel(resultData2);
    }
  }
}
