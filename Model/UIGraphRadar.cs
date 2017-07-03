// Decompiled with JetBrains decompiler
// Type: UIGraphRadar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class UIGraphRadar : MonoBehaviour
{
  public List<UIGraphRadarDataEntry> graphData = new List<UIGraphRadarDataEntry>();
  public EasingUtility.Easing animationCurve = EasingUtility.Easing.InBack;
  private List<GameObject> mGfxObjects = new List<GameObject>();
  public int numberOfLines;
  public int minValue;
  public int maxValue;
  public bool displayLabel;
  public GameObject gfxPanel;
  public UIGraphRadarLabel radarLabel;
  public UIPolygon polygonPrefab;

  public void GenerateGFX()
  {
    if (this.mGfxObjects.Count >= this.numberOfLines)
      return;
    --this.numberOfLines;
    this.CreateGraphGFX();
  }

  public void DestroyGFX()
  {
    for (int index = 1; index < this.mGfxObjects.Count; ++index)
      Object.Destroy((Object) this.mGfxObjects[index]);
    this.mGfxObjects.Clear();
    this.numberOfLines = 0;
  }

  private void Update()
  {
    for (int index = 0; index < this.graphData.Count; ++index)
      this.graphData[index].Update();
  }

  public void Highlight(int index, bool state)
  {
    if (!state)
      return;
    this.mGfxObjects[index].transform.FindChild("Circle").gameObject.GetComponent<Button>().Select();
  }

  public GameObject GetHighlightObject(int index)
  {
    if (this.mGfxObjects.Count > index)
      return this.mGfxObjects[index].transform.FindChild("Circle").gameObject;
    return (GameObject) null;
  }

  private void CreateGraphGFX()
  {
    if (!this.displayLabel)
      this.radarLabel.canvasGroup.alpha = 0.0f;
    this.mGfxObjects.Add(this.gfxPanel.transform.Find("GFX").gameObject);
    for (int index = 0; index < this.numberOfLines; ++index)
    {
      GameObject gameObject = Object.Instantiate<GameObject>(this.gfxPanel.transform.FindChild("GFX").gameObject);
      if (this.displayLabel)
      {
        UIGraphRadarCircle component = gameObject.transform.FindChild("Circle").GetComponent<UIGraphRadarCircle>();
        if ((Object) component != (Object) null)
          component.Setup(index + 1);
      }
      gameObject.transform.SetParent(this.gfxPanel.transform, false);
      this.mGfxObjects.Add(gameObject);
    }
    for (int index = 0; index < this.mGfxObjects.Count; ++index)
      this.mGfxObjects[index].transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f)
      {
        z = (float) (360 / this.mGfxObjects.Count * index)
      };
  }

  public void ClearGraphs()
  {
    for (int index = 0; index < this.graphData.Count; ++index)
      this.graphData[index].Delete();
    this.graphData.Clear();
  }

  public void CreateGraph(UIGraphRadarDataEntry inDataEntry)
  {
    this.polygonPrefab.gameObject.SetActive(false);
    inDataEntry.maxValue = (float) this.maxValue;
    inDataEntry.polygon = Object.Instantiate<UIPolygon>(this.polygonPrefab);
    inDataEntry.polygon.sides = inDataEntry.graphData.Count;
    inDataEntry.polygon.transform.SetParent(this.polygonPrefab.transform.parent, false);
    inDataEntry.polygon.color = inDataEntry.graphColor;
    inDataEntry.polygon.DrawPolygon(inDataEntry.polygon.sides);
    inDataEntry.polygon.rotation = 180f;
    for (int index = 0; index < inDataEntry.graphData.Count; ++index)
      inDataEntry.polygon.VerticesDistances[index] = 0.0f;
    inDataEntry.easingCurve = this.animationCurve;
    inDataEntry.polygon.gameObject.SetActive(true);
  }
}
