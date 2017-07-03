// Decompiled with JetBrains decompiler
// Type: MinimapOptions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class MinimapOptions : MonoBehaviour
{
  public MinimapOptions.ChangeColor driverColors = MinimapOptions.ChangeColor.All;
  public bool displayPlayerDrivers = true;
  public bool displayOtherDrivers = true;
  public bool displayGameCamera = true;
  public bool displaySectors = true;
  public bool displayFinishLine = true;
  private Dictionary<Vehicle, bool> vehicleData = new Dictionary<Vehicle, bool>();
  public bool displayDriverNamesAlways;
  public bool rotateVehicleLabelsWithCamera;

  public void DisplayVehicle(Vehicle inVehicle, bool inDisplay)
  {
    if (inVehicle == null)
      return;
    if (inDisplay)
    {
      if (!this.vehicleData.ContainsKey(inVehicle))
        return;
      this.vehicleData.Remove(inVehicle);
    }
    else
    {
      if (this.vehicleData.ContainsKey(inVehicle))
        return;
      this.vehicleData.Add(inVehicle, true);
    }
  }

  public bool IsVehicleDisplayed(Vehicle inVehicle)
  {
    return inVehicle == null || !this.vehicleData.ContainsKey(inVehicle);
  }

  public enum ChangeColor
  {
    None,
    MyDriversOnly,
    RivalsOnly,
    All,
  }
}
