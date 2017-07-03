// Decompiled with JetBrains decompiler
// Type: UIBaseTimerWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIBaseTimerWidget : MonoBehaviour
{
  private int mValue = int.MinValue;
  public GameObject timeWidget;
  public TextMeshProUGUI timeLabel;

  public bool isShowingTime
  {
    get
    {
      return this.timeWidget.activeSelf;
    }
  }

  private void OnEnable()
  {
    if (!this.isShowingTime)
      return;
    this.RefreshTime();
  }

  public void ShowTimeWidget(bool inShow)
  {
    GameUtility.SetActive(this.timeWidget, inShow);
  }

  public void SetTimeEstimate(float inTime, bool inIsTimeAdded)
  {
    inIsTimeAdded |= Mathf.Approximately(inTime, 0.0f);
    int inTime1 = Mathf.RoundToInt(inTime);
    Color color = !inIsTimeAdded ? UIConstants.pitStopChangedOrange : UIConstants.negativeColor;
    if (inTime1 == this.mValue && !(this.timeLabel.color != color))
      return;
    this.SetTimeEstimateText(inTime1, inIsTimeAdded);
    this.timeLabel.color = color;
  }

  private void SetTimeEstimateText(int inTime, bool inIncludeSign)
  {
    this.mValue = inTime;
    StringVariableParser.intValue1 = Mathf.RoundToInt((float) inTime);
    if (inIncludeSign)
      this.timeLabel.text = Localisation.LocaliseID("PSG_10010812", (GameObject) null);
    else
      this.timeLabel.text = GameUtility.FormatSecondsToString((float) inTime, 1);
  }

  public virtual void RefreshTime()
  {
  }
}
