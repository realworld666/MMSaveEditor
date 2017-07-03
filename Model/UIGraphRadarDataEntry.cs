// Decompiled with JetBrains decompiler
// Type: UIGraphRadarDataEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class UIGraphRadarDataEntry
{
  public List<float> graphData = new List<float>();
  public List<string> graphLabels = new List<string>();
  public EasingUtility.Easing easingCurve = EasingUtility.Easing.InBack;
  public Color graphColor = Color.white;
  public float animSpeed = 2f;
  public UIPolygon polygon;
  public float maxValue;
  public int dotSize;
  public int lineThickness;
  private float mTimer;

  public void Update()
  {
    if ((double) this.mTimer >= 1.0)
      return;
    this.mTimer += GameTimer.deltaTime * this.animSpeed;
    for (int index = 0; index < this.graphData.Count; ++index)
      this.polygon.VerticesDistances[index] = EasingUtility.EaseByType(this.easingCurve, 0.0f, this.graphData[index] / this.maxValue, this.mTimer);
    this.polygon.SetVerticesDirty();
  }

  public void Delete()
  {
    Object.Destroy((Object) this.polygon.gameObject);
  }
}
