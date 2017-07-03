// Decompiled with JetBrains decompiler
// Type: UIPositionTrackerPlace
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPositionTrackerPlace : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
  public RectTransform rectTransform;
  public Image selected;
  public Image dot;
  public Image retired;
  public Image pitstop;
  public Image positionCircle;
  public TextMeshProUGUI position;
  private int mLapIndex;
  private UIPositionTrackerGraph mGraph;
  private UIPositionTrackerGraph.TrackerData mTrackData;

  public void SetLapIndex(int inLapIndex)
  {
    this.mLapIndex = inLapIndex;
    this.mTrackData = (UIPositionTrackerGraph.TrackerData) null;
    this.Disable();
  }

  public void Setup(UIPositionTrackerGraph inGraph, UIPositionTrackerGraph.TrackerData inData, int inPosition, Color inColor, bool inLastLap)
  {
    this.mGraph = inGraph;
    this.mTrackData = inData;
    UIPositionTrackerGraph.LapData lapData = this.mTrackData.lapsData[this.mLapIndex];
    bool isSelected = this.mTrackData.isSelected;
    bool flag = lapData.retired || lapData.crashed;
    GameUtility.SetActive(this.retired.gameObject, isSelected && inLastLap && flag);
    GameUtility.SetActive(this.pitstop.gameObject, isSelected && !inLastLap && lapData.pitted);
    GameUtility.SetActive(this.positionCircle.gameObject, inLastLap && !flag);
    GameUtility.SetActive(this.position.gameObject, inLastLap && !flag);
    GameUtility.SetActive(this.dot.gameObject, !inLastLap && !lapData.pitted);
    GameUtility.SetActive(this.selected.gameObject, false);
    if (this.positionCircle.gameObject.activeSelf)
    {
      this.position.text = (inPosition + 1).ToString();
      this.positionCircle.color = inColor;
    }
    if (!this.dot.gameObject.activeSelf)
      return;
    this.dot.color = inColor;
  }

  public void Disable()
  {
    GameUtility.SetActive(this.dot.gameObject, false);
    GameUtility.SetActive(this.retired.gameObject, false);
    GameUtility.SetActive(this.pitstop.gameObject, false);
    GameUtility.SetActive(this.positionCircle.gameObject, false);
    GameUtility.SetActive(this.position.gameObject, false);
    GameUtility.SetActive(this.selected.gameObject, false);
  }

  public void OnPointerEnter(PointerEventData inEventData)
  {
    if (!((Object) this.mGraph != (Object) null) || this.mTrackData == null)
      return;
    scSoundManager.BlockSoundEvents = true;
    UIPositionTrackerRollover.ShowRollover(this.mLapIndex, this.mTrackData, this.rectTransform);
    GameUtility.SetActive(this.selected.gameObject, true);
    scSoundManager.BlockSoundEvents = false;
  }

  public void OnPointerExit(PointerEventData inEventData)
  {
    if (!((Object) this.mGraph != (Object) null))
      return;
    UIPositionTrackerRollover.HideRollover();
    GameUtility.SetActive(this.selected.gameObject, false);
  }
}
