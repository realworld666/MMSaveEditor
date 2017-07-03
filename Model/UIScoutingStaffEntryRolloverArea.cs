// Decompiled with JetBrains decompiler
// Type: UIScoutingStaffEntryRolloverArea
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIScoutingStaffEntryRolloverArea : MonoBehaviour
{
  private static float mHoverDelay = 0.25f;
  public RectTransform rectTransform;
  public UIScoutingStaffEntry scoutingEntry;
  private bool mActivated;
  private bool mDisplay;
  private float mTimer;

  private void Update()
  {
    this.mActivated = UIManager.instance.IsObjectAtMousePosition(this.gameObject);
    if (this.mActivated && !this.mDisplay)
    {
      this.mTimer += GameTimer.deltaTime;
      if ((double) this.mTimer < (double) UIScoutingStaffEntryRolloverArea.mHoverDelay)
        return;
      this.ShowRollover();
    }
    else
    {
      if (this.mActivated || !this.mDisplay)
        return;
      this.HideRollover();
      this.mTimer = 0.0f;
    }
  }

  private void ShowRollover()
  {
    if (this.scoutingEntry.person != null)
    {
      if (this.scoutingEntry.person is Driver)
        DriverInfoRollover.ShowTooltip(this.scoutingEntry.person as Driver, this.rectTransform, false, true);
      else
        StaffInfoRollover.ShowTooltip(this.scoutingEntry.person, this.rectTransform, false, true);
    }
    this.mDisplay = true;
  }

  private void HideRollover()
  {
    if (this.scoutingEntry.person != null)
    {
      if (this.scoutingEntry.person is Driver)
        DriverInfoRollover.HideTooltip();
      else
        StaffInfoRollover.HideTooltip();
    }
    this.mDisplay = false;
  }
}
