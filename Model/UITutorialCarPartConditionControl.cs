// Decompiled with JetBrains decompiler
// Type: UITutorialCarPartConditionControl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UITutorialCarPartConditionControl : UITutorialBespokeScript
{
  public CarPart.PartType partType = CarPart.PartType.Gearbox;
  [Range(0.0f, 1f)]
  public float condition = 1f;
  [Range(0.0f, 1f)]
  public int driverVehicle;

  protected override void Activate()
  {
    RacingVehicle[] playerVehicles = Game.instance.vehicleManager.GetPlayerVehicles();
    Team team = Game.instance.player.team;
    for (int index = 0; index < playerVehicles.Length; ++index)
    {
      if (team.GetDriverIndex(playerVehicles[index].driver) == this.driverVehicle)
      {
        this.condition = Mathf.Min(this.condition, playerVehicles[index].car.GetPart(this.partType).partCondition.condition / 2f);
        playerVehicles[index].car.GetPart(this.partType).partCondition.SetCondition(this.condition);
      }
    }
  }
}
