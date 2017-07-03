// Decompiled with JetBrains decompiler
// Type: UITutorialCompoundControl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class UITutorialCompoundControl : UITutorialBespokeScript
{
  public SessionStrategy.TyreOption vehicleOneTyreOption;
  public SessionStrategy.TyreOption vehicleTwoTyreOption;

  protected override void Activate()
  {
    RacingVehicle[] playerVehicles = Game.instance.vehicleManager.GetPlayerVehicles();
    Team team = Game.instance.player.team;
    for (int index = 0; index < playerVehicles.Length; ++index)
    {
      int driverIndex = team.GetDriverIndex(playerVehicles[index].driver);
      TyreSet inTyres = (TyreSet) null;
      if (driverIndex == 0)
        inTyres = playerVehicles[index].strategy.GetTyre(this.vehicleOneTyreOption, 0);
      else if (driverIndex == 1)
        inTyres = playerVehicles[index].strategy.GetTyre(this.vehicleTwoTyreOption, 0);
      playerVehicles[index].setup.SetTargetTyres(inTyres);
      playerVehicles[index].setup.InstantlyChangeTyres();
    }
  }
}
