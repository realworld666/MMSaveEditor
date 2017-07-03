// Decompiled with JetBrains decompiler
// Type: UITravelCarFittingSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITravelCarFittingSelect : UITravelStepOption
{
  public ActivateForSeries.GameObjectData[] seriesSpecificData = new ActivateForSeries.GameObjectData[0];
  public UIPanelDriverInfo[] driverOne;
  public UIPanelDriverInfo[] driverTwo;
  public UITravelCarFittingStatus[] carOne;
  public UITravelCarFittingStatus[] carTwo;
  public Button fitPartsButton;
  public Animator[] highlightAnimators;
  private Car mVehicleCarOne;
  private Car mVehicleCarTwo;

  public override void OnStart()
  {
    this.fitPartsButton.onClick.AddListener(new UnityAction(this.OnFitPartsButton));
  }

  public override void Setup()
  {
    Driver driver1 = Game.instance.player.team.GetDriver(0);
    Driver driver2 = Game.instance.player.team.GetDriver(1);
    GameUtility.SetActiveForSeries(Game.instance.player.team.championship, this.seriesSpecificData);
    for (int index = 0; index < this.driverOne.Length; ++index)
    {
      if (this.driverOne[index].gameObject.activeSelf)
        this.driverOne[index].Setup(driver1);
    }
    for (int index = 0; index < this.driverTwo.Length; ++index)
    {
      if (this.driverTwo[index].gameObject.activeSelf)
        this.driverTwo[index].Setup(driver2);
    }
    this.mVehicleCarOne = Game.instance.player.team.carManager.GetCarForDriver(driver1);
    this.mVehicleCarTwo = Game.instance.player.team.carManager.GetCarForDriver(driver2);
    for (int index = 0; index < this.carOne.Length; ++index)
    {
      if (this.carOne[index].gameObject.activeSelf)
        this.carOne[index].SetupCarPartStatus(this.mVehicleCarOne);
    }
    for (int index = 0; index < this.carTwo.Length; ++index)
    {
      if (this.carTwo[index].gameObject.activeSelf)
        this.carTwo[index].SetupCarPartStatus(this.mVehicleCarTwo);
    }
  }

  public override bool IsReady()
  {
    if (this.mVehicleCarOne.HasAllPartSlotsFitted())
      return this.mVehicleCarTwo.HasAllPartSlotsFitted();
    return false;
  }

  private void OnFitPartsButton()
  {
    UIManager.instance.ChangeScreen("CarPartFittingScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }
}
