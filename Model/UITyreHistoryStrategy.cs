// Decompiled with JetBrains decompiler
// Type: UITyreHistoryStrategy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class UITyreHistoryStrategy : MonoBehaviour
{
  public RectTransform rectTransform;
  public UIGridList compoundGrid;
  private RacingVehicle mVehicle;
  private float mVehicleLaps;
  private float mTotalWidth;
  private float mCircuitLaps;
  private float mLapWidth;
  private float mSize;
  private float mLastPosition;
  private float mWidth;
  private int mLaps;
  private int mTotalLaps;

  public void Setup(RacingVehicle inVehicle)
  {
    if (inVehicle == null)
      return;
    if (this.mVehicle != inVehicle)
    {
      this.mVehicle = inVehicle;
      this.compoundGrid.DestroyListItems();
    }
    this.UpdateStrategy();
  }

  public void UpdateStrategy()
  {
    List<SessionStints.SessionStintData> stintsForSession = this.mVehicle.stints.GetStintsForSession(Game.instance.sessionManager.eventDetails.currentSession.sessionType);
    int itemCount = this.compoundGrid.itemCount;
    int count = stintsForSession.Count;
    this.mTotalWidth = this.rectTransform.sizeDelta.x;
    this.mCircuitLaps = (float) Game.instance.sessionManager.lapCount;
    this.mLapWidth = this.mTotalWidth / this.mCircuitLaps;
    this.mLastPosition = 0.0f;
    this.mSize = 0.0f;
    this.mTotalLaps = 0;
    for (int inIndex = 0; inIndex < count; ++inIndex)
    {
      SessionStints.SessionStintData inStintData = stintsForSession[inIndex];
      this.mVehicleLaps = Mathf.Clamp((float) this.mVehicle.timer.lap + 1f, 1f, this.mCircuitLaps);
      this.mWidth = Mathf.Clamp(this.mLapWidth * this.mVehicleLaps, 60f, this.mTotalWidth);
      this.mLaps = inStintData.lapCount;
      if (inStintData.changedTyres || inIndex <= 0)
      {
        for (int index = inIndex + 1; index < count; ++index)
        {
          SessionStints.SessionStintData sessionStintData = stintsForSession[index];
          if (!sessionStintData.changedTyres)
            this.mLaps += sessionStintData.lapCount;
          else
            break;
        }
        if ((double) this.mTotalLaps < (double) this.mCircuitLaps && (double) (this.mTotalLaps + this.mLaps) > (double) this.mCircuitLaps)
        {
          this.mLaps = (int) this.mCircuitLaps - this.mTotalLaps;
          this.mTotalLaps = (int) this.mCircuitLaps;
        }
        else if ((double) (this.mTotalLaps + this.mLaps) < (double) this.mCircuitLaps || (double) (this.mTotalLaps + this.mLaps) == (double) this.mCircuitLaps && this.mLaps > 0)
          this.mTotalLaps += this.mLaps;
        else
          continue;
        this.mLastPosition += this.mSize;
        this.mSize = this.mLaps > 0 ? Mathf.Max((float) (60.0 + (double) this.mLaps * 10.0), (float) this.mLaps / this.mVehicleLaps * this.mWidth) : 38f;
        if ((double) this.mLastPosition + (double) this.mSize > (double) this.mWidth)
          this.mSize = this.mWidth - this.mLastPosition;
        UITyreHistoryStrategyEntry listItem;
        if (inIndex < itemCount)
        {
          listItem = this.compoundGrid.GetItem<UITyreHistoryStrategyEntry>(inIndex);
        }
        else
        {
          listItem = this.compoundGrid.CreateListItem<UITyreHistoryStrategyEntry>();
          GameUtility.SetActive(listItem.gameObject, true);
        }
        listItem.Setup(inStintData, this.mLastPosition, this.mSize, this.mLaps);
      }
    }
    GameUtility.SetActive(this.compoundGrid.itemPrefab, false);
  }
}
