// Decompiled with JetBrains decompiler
// Type: UIDriverSeriesTooltip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class UIDriverSeriesTooltip : MonoBehaviour
{
  private static float mHoverDelay = 0.25f;
  public Championship.Series series;
  private bool mActivated;
  private bool mDisplay;
  private float mTimer;

  private void Update()
  {
    this.mActivated = UIManager.instance.IsObjectAtMousePosition(this.gameObject);
    if (this.mActivated && !this.mDisplay)
    {
      this.mTimer += GameTimer.deltaTime;
      if ((double) this.mTimer < (double) UIDriverSeriesTooltip.mHoverDelay)
        return;
      this.ShowRollover();
    }
    else
    {
      if (this.mActivated || !this.mDisplay)
        return;
      this.HideRollover();
    }
  }

  private void OnDisable()
  {
    if (!this.mDisplay)
      return;
    this.HideRollover();
  }

  private void ShowRollover()
  {
    UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Open(Localisation.LocaliseEnum((Enum) this.series), Localisation.LocaliseID("PSG_10011505", (GameObject) null));
    this.mDisplay = true;
  }

  private void HideRollover()
  {
    this.mTimer = 0.0f;
    this.mDisplay = false;
    UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Close();
  }
}
