// Decompiled with JetBrains decompiler
// Type: UIPositionTrackerRollover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIPositionTrackerRollover : UIDialogBox
{
  public RectTransform rectTransform;
  public Flag flag;
  public TextMeshProUGUI driverName;
  public TextMeshProUGUI driverAge;
  public UITeamLogo logo;
  public UIDriverHelmet driverHelmet;
  public GameObject totalTimePanel;
  public GameObject lapTimePanel;
  public TextMeshProUGUI lapHeader;
  public TextMeshProUGUI position;
  public TextMeshProUGUI lapTime;
  public TextMeshProUGUI totalTime;
  public GameObject pitPanel;
  public TextMeshProUGUI pitTime;
  public TextMeshProUGUI pitEstimated;
  public TextMeshProUGUI pitLaneTime;
  public TextMeshProUGUI pitEntrance;
  public TextMeshProUGUI pitExit;
  private UIPositionTrackerGraph.TrackerData mData;
  private Driver mDriver;
  private int mLapNumber;

  public static void ShowRollover(int inLapNumber, UIPositionTrackerGraph.TrackerData inTrackData, RectTransform inRectTransform)
  {
    UIManager.instance.dialogBoxManager.GetDialog<UIPositionTrackerRollover>().Setup(inLapNumber, inTrackData, inRectTransform);
  }

  public static void HideRollover()
  {
    UIManager.instance.dialogBoxManager.GetDialog<UIPositionTrackerRollover>().Close();
  }

  public void Setup(int inLapNumber, UIPositionTrackerGraph.TrackerData inTrackData, RectTransform inRectTransform)
  {
    this.mData = inTrackData;
    this.mDriver = this.mData.driver;
    this.mLapNumber = inLapNumber;
    this.flag.SetNationality(this.mDriver.nationality);
    this.driverName.text = this.mDriver.name;
    this.driverAge.text = this.mDriver.GetAge().ToString();
    this.logo.SetTeam(this.mDriver.contract.GetTeam());
    this.driverHelmet.SetHelmet(this.mDriver);
    UIPositionTrackerGraph.LapData lapData = this.mData.lapsData[this.mLapNumber];
    StringVariableParser.intValue1 = this.mLapNumber;
    this.lapHeader.text = this.mLapNumber != 0 ? Localisation.LocaliseID("PSG_10010418", (GameObject) null) : Localisation.LocaliseID("PSG_10010879", (GameObject) null);
    this.position.text = GameUtility.FormatForPosition(lapData.position + 1, (string) null);
    this.lapTime.text = GameUtility.GetLapTimeText(lapData.lapTime, false);
    this.totalTime.text = GameUtility.GetLapTimeText(lapData.totalTime, false);
    GameUtility.SetActive(this.totalTimePanel, this.mLapNumber > 0);
    GameUtility.SetActive(this.lapTimePanel, this.mLapNumber > 0);
    GameUtility.SetActive(this.pitPanel, lapData.pitted);
    if (this.pitPanel.activeSelf)
    {
      this.pitTime.text = GameUtility.GetLapTimeText(lapData.stopTime, false);
      this.pitEstimated.text = GameUtility.GetLapTimeText(lapData.estimatedPitStopTime, false);
      this.pitLaneTime.text = GameUtility.GetLapTimeText(lapData.pitlaneTime, false);
      this.pitEntrance.text = GameUtility.FormatForPosition(lapData.entrancePosition, (string) null);
      this.pitExit.text = GameUtility.FormatForPosition(lapData.exitPosition, (string) null);
    }
    this.UpdateTooltipPosition(inRectTransform);
    GameUtility.SetActive(this.gameObject, true);
  }

  public void Close()
  {
    GameUtility.SetActive(this.gameObject, false);
  }

  private void UpdateTooltipPosition(RectTransform inRectTransform)
  {
    GameUtility.SetTooltipTransformInsideScreen(this.rectTransform, inRectTransform, new Vector3(), false, (RectTransform) null);
  }
}
