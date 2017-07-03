// Decompiled with JetBrains decompiler
// Type: UITutorialTyreWearControl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UITutorialTyreWearControl : UITutorialBespokeScript
{
  [Range(0.0f, 1f)]
  public float vehicleOneTyreWear = 1f;
  [Range(0.0f, 1f)]
  public float vehicleTwoTyreWear = 1f;

  protected override void Activate()
  {
    RacingVehicle[] playerVehicles = Game.instance.vehicleManager.GetPlayerVehicles();
    Team team = Game.instance.player.team;
    for (int index = 0; index < playerVehicles.Length; ++index)
    {
      switch (team.GetDriverIndex(playerVehicles[index].driver))
      {
        case 0:
          playerVehicles[index].setup.tyreSet.SetCondition(this.vehicleOneTyreWear);
          break;
        case 1:
          playerVehicles[index].setup.tyreSet.SetCondition(this.vehicleTwoTyreWear);
          break;
      }
    }
  }
}
