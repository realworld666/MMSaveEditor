// Decompiled with JetBrains decompiler
// Type: UIDriverFormEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIDriverFormEntry : MonoBehaviour
{
  public Flag eventFlag;
  public TextMeshProUGUI eventLocationLabel;
  public TextMeshProUGUI qualifyingPositionLabel;
  public TextMeshProUGUI racePositionLabel;
  public TextMeshProUGUI pointsLabel;
  public Button button;
  public GameObject statNumberUp;
  public GameObject statNumberDown;

  public void Setup(Driver inDriver, int inEventIndex)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UIDriverFormEntry.\u003CSetup\u003Ec__AnonStorey80 setupCAnonStorey80 = new UIDriverFormEntry.\u003CSetup\u003Ec__AnonStorey80();
    // ISSUE: reference to a compiler-generated field
    setupCAnonStorey80.inEventIndex = inEventIndex;
    // ISSUE: reference to a compiler-generated field
    setupCAnonStorey80.driverEntry = inDriver.GetChampionshipEntry();
    // ISSUE: reference to a compiler-generated field
    bool qualifyingBasedActive = setupCAnonStorey80.driverEntry.championship.rules.qualifyingBasedActive;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    RaceEventDetails raceEventDetails = setupCAnonStorey80.driverEntry.championship.calendar[setupCAnonStorey80.inEventIndex];
    Nationality nationality = new Nationality();
    this.eventFlag.SetNationality(Nationality.GetNationalityByName(raceEventDetails.circuit.nationalityKey));
    this.eventLocationLabel.text = Localisation.LocaliseID(raceEventDetails.circuit.locationNameID, (GameObject) null);
    if (raceEventDetails.hasEventEnded)
    {
      RaceEventResults.ResultData resultForDriver1 = raceEventDetails.results.GetResultsForSession(SessionDetails.SessionType.Race).GetResultForDriver(inDriver);
      int inPosition1 = resultForDriver1 != null ? resultForDriver1.position : 0;
      RaceEventResults.ResultData resultForDriver2 = raceEventDetails.results.GetResultsForSession(SessionDetails.SessionType.Qualifying).GetResultForDriver(inDriver);
      int inPosition2 = resultForDriver2 != null ? resultForDriver2.position : 0;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      int pointsOnEvent = setupCAnonStorey80.driverEntry.GetPointsOnEvent(setupCAnonStorey80.inEventIndex);
      if (inPosition1 == 0)
      {
        this.qualifyingPositionLabel.text = !qualifyingBasedActive ? string.Empty : "-";
        this.racePositionLabel.text = "-";
        this.pointsLabel.text = "-";
      }
      else
      {
        this.qualifyingPositionLabel.text = !qualifyingBasedActive ? string.Empty : GameUtility.FormatForPosition(inPosition2, (string) null);
        this.racePositionLabel.text = GameUtility.FormatForPosition(inPosition1, (string) null);
        this.pointsLabel.text = pointsOnEvent.ToString();
      }
      if (inPosition1 < inPosition2)
      {
        GameUtility.SetActive(this.statNumberUp, true);
        GameUtility.SetActive(this.statNumberDown, false);
      }
      else if (inPosition1 > inPosition2)
      {
        GameUtility.SetActive(this.statNumberUp, false);
        GameUtility.SetActive(this.statNumberDown, true);
      }
      else
      {
        GameUtility.SetActive(this.statNumberUp, false);
        GameUtility.SetActive(this.statNumberDown, false);
      }
    }
    else
    {
      this.qualifyingPositionLabel.text = !qualifyingBasedActive ? string.Empty : "-";
      this.racePositionLabel.text = "-";
      this.pointsLabel.text = "-";
      GameUtility.SetActive(this.statNumberUp, false);
      GameUtility.SetActive(this.statNumberDown, false);
    }
    this.button.onClick.RemoveAllListeners();
    // ISSUE: reference to a compiler-generated method
    this.button.onClick.AddListener(new UnityAction(setupCAnonStorey80.\u003C\u003Em__16A));
  }
}
