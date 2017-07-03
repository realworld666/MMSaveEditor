// Decompiled with JetBrains decompiler
// Type: UIPositionTrackerLine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class UIPositionTrackerLine : MonoBehaviour
{
  private List<Vector2> mPoints = new List<Vector2>();
  private Color mColor = new Color();
  public UILineTextureRenderer driverLine;
  private UIPositionTrackerGraph.TrackerData mData;
  private UIPositionTrackerGraph mGraph;

  public void Setup(UIPositionTrackerGraph inGraph, UIPositionTrackerGraph.TrackerData inData, int inFirstLap, int inLastLap)
  {
    this.mGraph = inGraph;
    this.mData = inData;
    this.mPoints.Clear();
    this.mColor = this.mData.driver.GetTeamColor().primaryUIColour.normal;
    this.mColor.a = !this.mData.isSelected ? 0.2f : 1f;
    this.driverLine.color = this.mColor;
    this.driverLine.Points = (Vector2[]) null;
    int length1 = this.mGraph.laps.Length;
    int index1 = inFirstLap;
    for (int index2 = 0; index2 < length1; ++index2)
    {
      int count = this.mData.lapsData.Count;
      if (index1 < count && index1 <= inLastLap)
      {
        int position = this.mData.lapsData[index1].position;
        int length2 = this.mGraph.laps[index2].places.Length;
        if (position >= 0 && length2 > 0 && position < length2)
        {
          UIPositionTrackerPlace place = this.mGraph.laps[index2].places[position];
          float width = place.rectTransform.rect.width;
          float height = place.rectTransform.rect.height;
          this.mPoints.Add(new Vector2((float) ((double) index2 * (double) width + (double) width / 2.0), (float) ((double) this.mGraph.laps[index2].places.Length * (double) height - ((double) position * (double) height + (double) height / 2.0))));
          place.Setup(this.mGraph, this.mData, position, this.mColor, count - 1 == index1);
        }
      }
      ++index1;
    }
    GameUtility.SetActive(this.gameObject, this.mPoints.Count > 1);
    if (!this.gameObject.activeSelf)
      return;
    this.driverLine.Points = this.mPoints.ToArray();
  }
}
