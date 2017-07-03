// Decompiled with JetBrains decompiler
// Type: UIFinanceGraphWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFinanceGraphWidget : MonoBehaviour
{
  private List<Vector2> mSeriesData = new List<Vector2>();
  private List<int> mPointIndexToIgnore = new List<int>();
  private DateTime mStartDate = new DateTime();
  private int mLocaleID = -1;
  public const int weeksDisplayed = 12;
  private const int million = 1000000;
  public TextMeshProUGUI balanceText;
  private WMG_Axis_Graph mGraph;
  private Finance mFinance;
  private WMG_Series mBalanceSeries;
  private int mNowDateDay;
  private int mForceRefresh;

  public void Awake()
  {
    this.mGraph = this.gameObject.GetComponent<WMG_Axis_Graph>();
  }

  public void OnEnter()
  {
    this.mFinance = Game.instance.player.team.financeController.finance;
    this.CreateSeries();
    this.SetText();
  }

  public void ShowPrediction(long inValue)
  {
    this.balanceText.text = GameUtility.GetCurrencyString(this.mFinance.currentBudget, 0) + "  [ " + ((inValue <= 0L ? GameUtility.ColorToRichTextHex(UIConstants.negativeColor) : GameUtility.ColorToRichTextHex(UIConstants.positiveColor)) + GameUtility.GetCurrencyString(inValue, 0) + "</color>") + " ]";
    this.balanceText.color = this.mFinance.currentBudget >= 0L ? Color.white : UIConstants.negativeColor;
  }

  private void SetText()
  {
    this.balanceText.text = GameUtility.GetCurrencyString(this.mFinance.currentBudget, 0);
    this.balanceText.color = this.mFinance.currentBudget >= 0L ? Color.white : UIConstants.negativeColor;
  }

  private void CreateSeries()
  {
    this.mGraph.Init();
    this.mPointIndexToIgnore.Clear();
    this.mSeriesData.Clear();
    this.mBalanceSeries = this.mGraph.lineSeries[0].GetComponent<WMG_Series>();
    this.mGraph.xAxis.AxisMinValue = 0.0f;
    this.mGraph.xAxis.AxisMaxValue = 1f;
    this.mGraph.yAxis.AxisMinValue = 0.0f;
    this.mGraph.yAxis.AxisMaxValue = 1f;
    DateTime lastSunday = this.GetLastSunday();
    this.mStartDate = Game.instance.time.now.AddDays(-84.0);
    this.mBalanceSeries.pointColors.Clear();
    for (int index = 0; index < 12; ++index)
    {
      DateTime inDate = lastSunday.AddDays((double) (-index * 7));
      int count = this.mFinance.transactionHistory.transactions.Count;
      if (count > 0)
      {
        float num = (float) (index != 0 ? this.mFinance.transactionHistory.GetLastTransactionBeforeDate(inDate) ?? this.mFinance.transactionHistory.transactions[0] : this.mFinance.transactionHistory.transactions[count - 1]).fundsAfterTransaction / 1000000f;
        Vector2 inPoint = new Vector2(this.GetNormalizedXPosition(index), num);
        if (this.AddPointToBudgetStateTransition(index, inPoint, ref this.mSeriesData))
          this.mBalanceSeries.pointColors.Add(UIConstants.positiveColor);
        GameUtility.Assert(!float.IsNaN(inPoint.x), "Transaction point is NAN, X:" + (object) inPoint.x, (UnityEngine.Object) null);
        GameUtility.Assert(!float.IsNaN(inPoint.y), "Transaction point is NAN, Y:" + (object) inPoint.y, (UnityEngine.Object) null);
        this.mSeriesData.Add(inPoint);
        this.mBalanceSeries.pointColors.Add((double) num >= 0.0 ? UIConstants.positiveColor : UIConstants.negativeColor);
        if ((double) this.mGraph.yAxis.AxisMaxValue < (double) num)
          this.mGraph.yAxis.AxisMaxValue = Mathf.Ceil(num);
      }
      else
        break;
    }
    this.mSeriesData.Reverse();
    this.mBalanceSeries.pointColors.Reverse();
    this.mBalanceSeries.pointValues.SetList((IEnumerable<Vector2>) this.mSeriesData);
    this.mGraph.Refresh();
    this.mGraph.GraphChanged();
    this.mGraph.SeriesChanged(true, true);
    this.mForceRefresh = 0;
  }

  private void LateUpdate()
  {
    if (this.mForceRefresh >= 2)
      return;
    this.SetAxisLabelsData();
    ++this.mForceRefresh;
  }

  private bool AddPointToBudgetStateTransition(int index, Vector2 inPoint, ref List<Vector2> inPoints)
  {
    if (inPoints.Count <= 0)
      return false;
    Vector2 inFirstPoint = inPoints[inPoints.Count - 1];
    if (((double) inFirstPoint.y >= 0.0 || (double) inPoint.y < 0.0) && ((double) inPoint.y >= 0.0 || (double) inFirstPoint.y < 0.0))
      return false;
    inPoints.Add(new Vector2(this.GetXPositionForGraphFunc(inPoint, inFirstPoint), 0.0f));
    return true;
  }

  private float GetXPositionForGraphFunc(Vector2 inSecondPoint, Vector2 inFirstPoint)
  {
    float num1 = inFirstPoint.x - inSecondPoint.x;
    if ((double) num1 == 0.0)
      return inFirstPoint.x;
    float a = (inFirstPoint.y - inSecondPoint.y) / num1;
    float num2 = inFirstPoint.y - a * inFirstPoint.x;
    if (Mathf.Approximately(a, 0.0f))
      return 0.0f;
    return -num2 / a;
  }

  private void SetLineColors()
  {
    List<GameObject> lines = this.mBalanceSeries.getLines();
    for (int index = 0; index < lines.Count; ++index)
    {
      WMG_Link component1 = lines[index].GetComponent<WMG_Link>();
      Image component2 = component1.fromNode.GetComponent<Image>();
      component1.GetComponent<Image>().color = component2.color;
    }
  }

  private void SetAxisLabelsData()
  {
    List<WMG_Node> axisLabelNodes1 = this.mGraph.xAxis.GetAxisLabelNodes();
    List<WMG_Node> axisLabelNodes2 = this.mGraph.yAxis.GetAxisLabelNodes();
    float num = (float) (((double) this.mGraph.yAxis.AxisMaxValue - (double) this.mGraph.yAxis.AxisMinValue) / 3.0);
    int lcid = App.instance.preferencesManager.gamePreferences.GetCurrencyCultureFormat().LCID;
    int result = 0;
    for (int index = 0; index < axisLabelNodes2.Count; ++index)
    {
      TextMeshProUGUI componentInChildren = axisLabelNodes2[index].GetComponentInChildren<TextMeshProUGUI>();
      if (this.mNowDateDay == Game.instance.time.now.Day && !int.TryParse(componentInChildren.text, out result) && this.mLocaleID == lcid)
        return;
      float inValue = this.mGraph.yAxis.AxisMinValue + num * (float) index;
      componentInChildren.text = GameUtility.GetCurrencyStringMillions(inValue, 1);
    }
    this.mLocaleID = lcid;
    TimeSpan timeSpan = Game.instance.time.now - this.mStartDate;
    for (int index = 0; index < axisLabelNodes1.Count; ++index)
    {
      TextMeshProUGUI componentInChildren = axisLabelNodes1[index].GetComponentInChildren<TextMeshProUGUI>();
      if (index == axisLabelNodes1.Count - 1)
      {
        this.mNowDateDay = Game.instance.time.now.Day;
        componentInChildren.text = Localisation.LocaliseID("PSG_10000391", (GameObject) null);
      }
      else
      {
        DateTime inDate = this.mStartDate.AddHours((double) ((float) index / (float) (this.mGraph.xAxis.axisLabels.Count - 1)) * timeSpan.TotalHours);
        componentInChildren.text = GameUtility.FormatDateTimeToAbbrevMonthNoYear(inDate, string.Empty);
      }
    }
    this.SetLineColors();
  }

  private void UpdateGraphPointsTimeScale()
  {
    WMG_List<Vector2> pointValues = this.mBalanceSeries.pointValues;
    for (int index = 0; index < pointValues.Count; ++index)
    {
      if (!this.mPointIndexToIgnore.Contains(index))
        pointValues[index] = new Vector2(1f - this.GetNormalizedXPosition(index), pointValues[index].y);
      else if (index > 0)
        pointValues[index] = new Vector2(this.GetXPositionForGraphFunc(pointValues[index - 1], pointValues[index + 1]), 0.0f);
    }
  }

  private float GetNormalizedXPosition(int index)
  {
    return 1f - Mathf.Clamp01((float) index / 11f);
  }

  private DateTime GetLastSunday()
  {
    DateTime now = Game.instance.time.now;
    int num = (int) (now.DayOfWeek - 1 + 1);
    DateTime dateTime = now.AddDays((double) -num);
    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
  }
}
