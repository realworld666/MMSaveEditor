// Decompiled with JetBrains decompiler
// Type: ERSPowerIndicatorsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class ERSPowerIndicatorsWidget : MonoBehaviour
{
  private List<RacingVehicle> mVehicles = new List<RacingVehicle>();
  public UIGridList list;

  public void OnStart()
  {
    this.list.itemPrefab.SetActive(false);
  }

  private void OnEnable()
  {
    this.mVehicles.Clear();
    for (int inIndex = 0; inIndex < Game.instance.sessionManager.standings.Count; ++inIndex)
    {
      this.mVehicles.Add(Game.instance.sessionManager.standings[inIndex]);
      this.list.GetOrCreateItem<UIVehicleInfo>(inIndex).Hide();
    }
    this.Update();
  }

  private void Update()
  {
    for (int inIndex = 0; inIndex < this.mVehicles.Count; ++inIndex)
    {
      RacingVehicle mVehicle = this.mVehicles[inIndex];
      if (!mVehicle.driver.IsPlayersDriver())
      {
        UIVehicleInfo uiVehicleInfo = this.list.GetOrCreateItem<UIVehicleInfo>(inIndex);
        if (mVehicle.ERSController.mode == ERSController.Mode.Power)
        {
          if (!uiVehicleInfo.isShowing)
            uiVehicleInfo.Show((Vehicle) mVehicle);
          uiVehicleInfo.UpdatePosition();
        }
        else if (uiVehicleInfo.isShowing)
          uiVehicleInfo.HideAndClearVehicle();
      }
    }
  }
}
