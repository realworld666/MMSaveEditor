// Decompiled with JetBrains decompiler
// Type: UILineGraph
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILineGraph : MonoBehaviour
{
  public List<UILineGraphDataEntry> graphData = new List<UILineGraphDataEntry>();
  public EasingUtility.Easing animationCurve = EasingUtility.Easing.InBack;
  private List<TextMeshProUGUI> mVerticalLabels = new List<TextMeshProUGUI>();
  private List<TextMeshProUGUI> mHorizontalLabels = new List<TextMeshProUGUI>();
  private List<GameObject> mVerticalGridLines = new List<GameObject>();
  private List<GameObject> mHorizontalGridLines = new List<GameObject>();
  public float verticalScaling;
  public float horizontalScaling;
  public float verticalOffset;
  public float horizontalOffset;
  public float zoom;
  public float lineThickness;
  public float dotThickness;
  public int numberHorizontalLabels;
  public int numberVerticalLabels;
  public float verticalLabelOffset;
  public float horizontalLabelOffset;
  public bool fitToGraphArea;
  public bool showCurrentBar;
  public bool showTooltip;
  public bool showHorizontalGridLines;
  public bool showVerticalGridLines;
  public bool showBoundariesGradients;
  public int horizontalLabelScale;
  public int verticalLabelScale;
  public UIGraphLineConnector linePrefab;
  public UILineGraphDot dotPrefab;
  private float mHorizontalScaling;
  private float mVerticalScaling;
  private float mYLabelFactor;
  public int xStartPointOffset;
  public int yStartPointOffset;
  private Transform mGraphHolder;
  private Transform mLabelHorizontal;
  private Transform mLabelVertical;
  private Transform mHorizontalGrid;
  private Transform mVerticalGrid;
  private ScrollRect mScrollRect;
  private Slider mZoomSlider;
  private RectTransform mGraphRectTransform;
  private RectTransform mGraphSlideAreaRectTransform;
  private RectTransform mCurrentBar;
  private UIGraphTooltip mToolTip;
  private Animator mGradientLeft;
  private Animator mGradientRight;
  private Animator mGradientTop;
  private Animator mGradientBottom;

  public void Setup()
  {
    this.mCurrentBar = this.transform.FindChild("GraphArea").FindChild("CurrentBar").gameObject.GetComponent<RectTransform>();
    this.mGradientLeft = this.transform.FindChild("Gradients").FindChild("LeftGradient").gameObject.GetComponent<Animator>();
    this.mGradientRight = this.transform.FindChild("Gradients").FindChild("RightGradient").gameObject.GetComponent<Animator>();
    this.mGradientTop = this.transform.FindChild("Gradients").FindChild("TopGradient").gameObject.GetComponent<Animator>();
    this.mGradientBottom = this.transform.FindChild("Gradients").FindChild("BottomGradient").gameObject.GetComponent<Animator>();
    this.mScrollRect = this.transform.FindChild("GraphArea").gameObject.GetComponent<ScrollRect>();
    this.mToolTip = this.transform.FindChild("TextTooltip").gameObject.GetComponent<UIGraphTooltip>();
    this.mGraphSlideAreaRectTransform = this.transform.FindChild("GraphSlidingArea").gameObject.GetComponent<RectTransform>();
    this.mGraphRectTransform = this.gameObject.GetComponent<RectTransform>();
    this.mGraphHolder = this.transform.FindChild("GraphArea");
    this.mLabelHorizontal = this.transform.FindChild("LabelsHorizontalParent").FindChild("LabelHorizontal");
    this.mLabelVertical = this.transform.FindChild("LabelsVerticalParent").FindChild("LabelVertical");
    this.mZoomSlider = this.transform.FindChild("ZoomBar").GetComponent<Slider>();
    this.mHorizontalGrid = this.transform.FindChild("GridHorizontalParent");
    this.mVerticalGrid = this.transform.FindChild("GridVerticalParent");
    this.FitToArea();
    this.mHorizontalScaling = this.horizontalScaling;
    this.mVerticalScaling = this.verticalScaling;
    this.HideTooltip();
    for (int index = 0; index < this.graphData.Count; ++index)
      this.CreateGraphLine(this.graphData[index]);
    this.CreateLabels();
  }

  public void FitToArea()
  {
    int num1 = 0;
    int num2 = 0;
    for (int index1 = 0; index1 < this.graphData.Count; ++index1)
    {
      for (int index2 = 0; index2 < this.graphData[index1].data.Count; ++index2)
      {
        if (num2 < (int) this.graphData[index1].data[index2].y)
          num2 = (int) this.graphData[index1].data[index2].y;
        if (num1 < (int) this.graphData[index1].data[index2].x)
          num1 = (int) this.graphData[index1].data[index2].x;
      }
    }
    if (!this.fitToGraphArea)
      return;
    this.mYLabelFactor = num2 <= 1000000 ? (num2 <= 1000 ? 1f : 1000f) : 1000000f;
    if (num2 != 0)
    {
      this.verticalScaling = (double) this.verticalScaling <= 0.0 ? (float) (-1.0 * ((double) this.mGraphRectTransform.rect.height / (double) num2)) : this.mGraphRectTransform.rect.height / (float) num2;
      this.verticalLabelScale = Mathf.RoundToInt((float) num2 / 20f);
      this.verticalLabelScale = Mathf.Clamp(this.verticalLabelScale, 1, 1000000);
    }
    else
      this.verticalLabelScale = 1;
    if (num1 != 0)
    {
      this.horizontalScaling = (double) this.horizontalScaling <= 0.0 ? (float) (-1 * Mathf.RoundToInt(this.mGraphRectTransform.rect.width / (float) num1)) : (float) Mathf.RoundToInt(this.mGraphRectTransform.rect.width / (float) num1);
      this.horizontalLabelScale = num1 / 10;
    }
    else
      this.horizontalLabelScale = 1;
    this.numberHorizontalLabels = num1;
    this.numberVerticalLabels = num2;
    this.numberHorizontalLabels = Mathf.Clamp(this.numberHorizontalLabels, 0, 100);
    this.numberVerticalLabels = Mathf.Clamp(this.numberVerticalLabels, 0, 100);
  }

  public void SetGraphVisibility(int inIndex, bool inValue)
  {
    if (!inValue)
      this.graphData[inIndex].Hide();
    else
      this.graphData[inIndex].Show();
  }

  public void ClearGraph()
  {
    for (int index = 0; index < this.graphData.Count; ++index)
      this.graphData[index].RemoveObject();
    for (int index = 1; index < this.mVerticalLabels.Count; ++index)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mVerticalLabels[index].gameObject);
    for (int index = 1; index < this.mVerticalGridLines.Count; ++index)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mVerticalGridLines[index].gameObject);
    for (int index = 1; index < this.mHorizontalLabels.Count; ++index)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mHorizontalLabels[index].gameObject);
    for (int index = 1; index < this.mHorizontalGridLines.Count; ++index)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mHorizontalGridLines[index].gameObject);
    this.mVerticalLabels.Clear();
    this.mHorizontalLabels.Clear();
    this.mVerticalGridLines.Clear();
    this.mHorizontalGridLines.Clear();
    this.graphData.Clear();
  }

  public void UpdateAll()
  {
    this.Zoom();
    for (int index = 0; index < this.graphData.Count; ++index)
      this.UpdateDotPosition(this.graphData[index]);
    this.UpdateOffset();
    this.UpdateLabelPosition();
    this.UpdateLabelText();
    this.UpdateInteractiveCanvasSize();
    this.UpdateGridPosition();
    this.ShowCurrentBar();
    this.TriggerGradientsAnimations();
  }

  public void ShowCurrentBar()
  {
    if (this.showCurrentBar)
    {
      this.mCurrentBar.gameObject.SetActive(true);
      float num = 0.0f;
      for (int index1 = 0; index1 < this.graphData.Count; ++index1)
      {
        UILineGraphDataEntry lineGraphDataEntry = this.graphData[index1];
        for (int index2 = 0; index2 < lineGraphDataEntry.dots.Count; ++index2)
        {
          if ((double) num < (double) lineGraphDataEntry.dots[index2].transform.localPosition.x)
            num = lineGraphDataEntry.dots[index2].transform.localPosition.x;
        }
      }
      Vector2 localPosition = (Vector2) this.mCurrentBar.localPosition;
      Vector2 sizeDelta = this.mCurrentBar.sizeDelta;
      sizeDelta.x = this.horizontalScaling;
      localPosition.x = num + sizeDelta.x / 2f;
      this.mCurrentBar.sizeDelta = sizeDelta;
      this.mCurrentBar.localPosition = (Vector3) localPosition;
    }
    else
      this.mCurrentBar.gameObject.SetActive(false);
  }

  public void Zoom()
  {
    if ((double) this.zoom == (double) this.mZoomSlider.value)
      return;
    this.zoom = this.mZoomSlider.value;
    float num = this.mZoomSlider.value * 5f;
    this.mZoomSlider.handleRect.sizeDelta = new Vector2(num, num);
    this.horizontalScaling = this.mHorizontalScaling * this.zoom;
    this.verticalScaling = this.mVerticalScaling * this.zoom;
    this.mScrollRect.Rebuild(CanvasUpdate.Layout);
    Canvas.ForceUpdateCanvases();
  }

  public void ShowTooltip(UILineGraphDot inDot)
  {
    if (!this.showTooltip)
      return;
    this.mToolTip.isOn = true;
    this.mToolTip.SetText(inDot.tooltipText);
    this.mToolTip.transform.localPosition = inDot.transform.localPosition + new Vector3(0.0f, 35f, 0.0f);
  }

  public void HideTooltip()
  {
    if (!this.showTooltip)
      return;
    this.mToolTip.isOn = false;
  }

  public void UpdateOffset()
  {
    this.verticalOffset = this.mGraphSlideAreaRectTransform.localPosition.y;
    this.horizontalOffset = this.mGraphSlideAreaRectTransform.localPosition.x;
  }

  public void UpdateDotPosition(UILineGraphDataEntry inData)
  {
    for (int index = 0; index < inData.dots.Count; ++index)
    {
      Vector2 inSpacing = new Vector2();
      inSpacing = new Vector2((float) ((double) inData.data[index].x * (double) this.horizontalScaling + (double) this.horizontalOffset - (double) this.xStartPointOffset * (double) this.horizontalScaling), (float) ((double) inData.data[index].y * (double) this.verticalScaling + (double) this.verticalOffset - (double) this.yStartPointOffset * (double) this.verticalScaling));
      if ((double) this.horizontalScaling < 0.0)
        inSpacing.x += this.mGraphRectTransform.rect.width;
      if ((double) this.verticalScaling < 0.0)
        inSpacing.y += this.mGraphRectTransform.rect.height;
      inData.dots[index].SetPosition(inSpacing);
    }
  }

  public void UpdateInteractiveCanvasSize()
  {
    float f1 = 0.0f;
    float f2 = 0.0f;
    for (int index1 = 0; index1 < this.graphData.Count; ++index1)
    {
      UILineGraphDataEntry lineGraphDataEntry = this.graphData[index1];
      for (int index2 = 0; index2 < lineGraphDataEntry.dots.Count; ++index2)
      {
        Vector2 vector2 = new Vector2();
        vector2 = new Vector2(lineGraphDataEntry.data[index2].x * Mathf.Abs(this.horizontalScaling), lineGraphDataEntry.data[index2].y * Mathf.Abs(this.verticalScaling));
        if ((double) Mathf.Abs(f1) < (double) Mathf.Abs(vector2.y))
          f1 = vector2.y;
        if ((double) Mathf.Abs(f2) < (double) Mathf.Abs(vector2.x))
          f2 = vector2.x;
      }
    }
    Vector2 vector2_1 = new Vector2();
    Vector2 vector2_2 = new Vector2();
    vector2_1.x = Mathf.Abs(f2);
    vector2_1.y = Mathf.Abs(f1);
    if ((double) f1 != 0.0)
    {
      if ((double) this.verticalScaling > 0.0)
        vector2_2.y = 0.0f;
      else if ((double) this.verticalScaling < 0.0)
        vector2_2.y = (vector2_1.y - this.mGraphRectTransform.rect.height) / vector2_1.y;
    }
    if ((double) f2 != 0.0)
    {
      if ((double) this.horizontalScaling > 0.0)
        vector2_2.x = 0.0f;
      else if ((double) this.horizontalScaling < 0.0)
        vector2_2.x = (vector2_1.x - this.mGraphRectTransform.rect.width) / vector2_1.x;
    }
    this.mGraphSlideAreaRectTransform.pivot = vector2_2;
    this.mGraphSlideAreaRectTransform.sizeDelta = vector2_1;
  }

  public void CreateLabels()
  {
    this.mVerticalLabels.Add(this.mLabelVertical.GetComponent<TextMeshProUGUI>());
    this.mHorizontalLabels.Add(this.mLabelHorizontal.GetComponent<TextMeshProUGUI>());
    this.mVerticalGridLines.Add(this.mVerticalGrid.GetChild(0).gameObject);
    this.mHorizontalGridLines.Add(this.mHorizontalGrid.GetChild(0).gameObject);
    for (int index = 1; index < this.numberVerticalLabels; ++index)
    {
      this.mVerticalLabels.Add(UnityEngine.Object.Instantiate<TextMeshProUGUI>(this.mVerticalLabels[0]));
      this.mHorizontalGridLines.Add(UnityEngine.Object.Instantiate<GameObject>(this.mHorizontalGridLines[0]));
      this.mVerticalLabels[index].transform.SetParent(this.mLabelVertical.parent, false);
      this.mHorizontalGridLines[index].transform.SetParent(this.mVerticalGrid, false);
    }
    for (int index = 1; index < this.numberHorizontalLabels; ++index)
    {
      this.mHorizontalLabels.Add(UnityEngine.Object.Instantiate<TextMeshProUGUI>(this.mHorizontalLabels[0]));
      this.mVerticalGridLines.Add(UnityEngine.Object.Instantiate<GameObject>(this.mVerticalGridLines[0]));
      this.mHorizontalLabels[index].transform.SetParent(this.mLabelHorizontal.parent, false);
      this.mVerticalGridLines[index].transform.SetParent(this.mHorizontalGrid, false);
    }
  }

  public void UpdateLabelText()
  {
    for (int index = 0; index < this.mVerticalLabels.Count; ++index)
    {
      this.mVerticalLabels[index].text = Mathf.Round((float) ((index + this.yStartPointOffset) * this.verticalLabelScale) / this.mYLabelFactor).ToString((IFormatProvider) Localisation.numberFormatter);
      if ((double) this.mYLabelFactor >= 1000000.0)
        this.mVerticalLabels[index].text += "M";
      else if ((double) this.mYLabelFactor >= 1000.0)
        this.mVerticalLabels[index].text += "K";
    }
    for (int index = 0; index < this.mHorizontalLabels.Count; ++index)
      this.mHorizontalLabels[index].text = ((index + this.xStartPointOffset) * this.horizontalLabelScale).ToString();
  }

  public void UpdateGridPosition()
  {
    if (this.showHorizontalGridLines)
    {
      for (int index = 0; index < this.mHorizontalGridLines.Count; ++index)
      {
        Vector3 localPosition = this.mHorizontalGridLines[index].transform.localPosition;
        localPosition.y = this.verticalScaling * (float) index * (float) this.verticalLabelScale + this.verticalOffset;
        if ((double) localPosition.y > (double) this.mGraphRectTransform.rect.height + 5.0 || (double) localPosition.y < -5.0 && (double) this.verticalScaling > 0.0 || ((double) localPosition.y < -(double) this.mGraphRectTransform.rect.height && (double) this.verticalScaling < 0.0 || (double) localPosition.y > 5.0 && (double) this.verticalScaling < 0.0))
          this.mHorizontalGridLines[index].gameObject.SetActive(false);
        else
          this.mHorizontalGridLines[index].gameObject.SetActive(true);
        if ((double) this.verticalScaling < 0.0)
          localPosition.y += this.mGraphRectTransform.rect.height;
        this.mHorizontalGridLines[index].transform.localPosition = localPosition;
      }
    }
    else
    {
      for (int index = 0; index < this.mHorizontalGridLines.Count; ++index)
        this.mHorizontalGridLines[index].gameObject.SetActive(false);
    }
    if (this.showVerticalGridLines)
    {
      for (int index = 0; index < this.mVerticalGridLines.Count; ++index)
      {
        Vector3 localPosition = this.mVerticalGridLines[index].transform.localPosition;
        localPosition.x = this.horizontalScaling * (float) index * (float) this.horizontalLabelScale + this.horizontalOffset;
        if ((double) localPosition.x > (double) this.mGraphRectTransform.rect.width + 5.0 || (double) localPosition.x < -5.0 && (double) this.horizontalScaling > 0.0 || ((double) localPosition.x < -(double) this.mGraphRectTransform.rect.width && (double) this.horizontalScaling < 0.0 || (double) localPosition.x > 5.0 && (double) this.horizontalScaling < 0.0))
          this.mVerticalGridLines[index].gameObject.SetActive(false);
        else
          this.mVerticalGridLines[index].gameObject.SetActive(true);
        if ((double) this.horizontalScaling < 0.0)
          localPosition.x += this.mGraphRectTransform.rect.width;
        this.mVerticalGridLines[index].transform.localPosition = localPosition;
      }
    }
    else
    {
      for (int index = 0; index < this.mVerticalGridLines.Count; ++index)
        this.mVerticalGridLines[index].gameObject.SetActive(false);
    }
  }

  public void UpdateLabelPosition()
  {
    for (int index = 0; index < this.mVerticalLabels.Count; ++index)
    {
      Vector3 localPosition = this.mVerticalLabels[index].transform.localPosition;
      localPosition.y = this.verticalScaling * (float) index * (float) this.verticalLabelScale + this.verticalOffset;
      if ((double) localPosition.y > (double) this.mGraphRectTransform.rect.height + 5.0 || (double) localPosition.y < -5.0 && (double) this.verticalScaling > 0.0 || ((double) localPosition.y < -(double) this.mGraphRectTransform.rect.height && (double) this.verticalScaling < 0.0 || (double) localPosition.y > 5.0 && (double) this.verticalScaling < 0.0))
        this.mVerticalLabels[index].gameObject.SetActive(false);
      else
        this.mVerticalLabels[index].gameObject.SetActive(true);
      if ((double) this.verticalScaling < 0.0)
        localPosition.y += this.mGraphRectTransform.rect.height;
      localPosition.x = this.verticalLabelOffset;
      this.mVerticalLabels[index].transform.localPosition = localPosition;
    }
    for (int index = 0; index < this.mHorizontalLabels.Count; ++index)
    {
      Vector3 localPosition = this.mHorizontalLabels[index].transform.localPosition;
      localPosition.x = this.horizontalScaling * (float) index * (float) this.horizontalLabelScale + this.horizontalOffset;
      if ((double) localPosition.x > (double) this.mGraphRectTransform.rect.width + 5.0 || (double) localPosition.x < -5.0 && (double) this.horizontalScaling > 0.0 || ((double) localPosition.x < -(double) this.mGraphRectTransform.rect.width && (double) this.horizontalScaling < 0.0 || (double) localPosition.x > 5.0 && (double) this.horizontalScaling < 0.0))
        this.mHorizontalLabels[index].gameObject.SetActive(false);
      else
        this.mHorizontalLabels[index].gameObject.SetActive(true);
      if ((double) this.horizontalScaling < 0.0)
        localPosition.x += this.mGraphRectTransform.rect.width;
      localPosition.y = this.horizontalLabelOffset;
      this.mHorizontalLabels[index].transform.localPosition = localPosition;
    }
  }

  public void CreateGraphLine(UILineGraphDataEntry inData)
  {
    for (int index = 0; index < inData.data.Count; ++index)
    {
      UILineGraphDot uiLineGraphDot = UnityEngine.Object.Instantiate<UILineGraphDot>(this.dotPrefab);
      uiLineGraphDot.dotData = inData.data[index];
      uiLineGraphDot.tooltipText = inData.tooltipText[index];
      uiLineGraphDot.SetSize(new Vector2(this.dotThickness, this.dotThickness));
      uiLineGraphDot.graph = this;
      uiLineGraphDot.SetColor(inData.color);
      inData.dots.Add(uiLineGraphDot);
    }
    for (int index = 0; index < inData.dots.Count; ++index)
    {
      UIGraphLineConnector graphLineConnector = UnityEngine.Object.Instantiate<UIGraphLineConnector>(this.linePrefab);
      graphLineConnector.transform.SetParent(this.mGraphHolder, false);
      graphLineConnector.lineWidth = this.lineThickness;
      graphLineConnector.SetColor(inData.color);
      graphLineConnector.startPoint = inData.dots[index].transform;
      graphLineConnector.endPoint = index + 1 == inData.dots.Count ? inData.dots[index].transform : inData.dots[index + 1].transform;
      inData.lines.Add(graphLineConnector);
    }
    for (int index = 0; index < inData.dots.Count; ++index)
      inData.dots[index].transform.SetParent(this.mGraphHolder, false);
  }

  private void TriggerGradientsAnimations()
  {
    if (this.showBoundariesGradients)
    {
      if ((double) this.mScrollRect.verticalScrollbar.value == 1.0)
        this.mGradientTop.SetBool("Play", true);
      else
        this.mGradientTop.SetBool("Play", false);
      if ((double) this.mScrollRect.verticalScrollbar.value == 0.0)
        this.mGradientBottom.SetBool("Play", true);
      else
        this.mGradientBottom.SetBool("Play", false);
      if ((double) this.mScrollRect.horizontalScrollbar.value == 1.0)
        this.mGradientRight.SetBool("Play", true);
      else
        this.mGradientRight.SetBool("Play", false);
      if ((double) this.mScrollRect.horizontalScrollbar.value == 0.0)
        this.mGradientLeft.SetBool("Play", true);
      else
        this.mGradientLeft.SetBool("Play", false);
    }
    else
      this.mGradientTop.transform.parent.gameObject.SetActive(false);
  }
}
