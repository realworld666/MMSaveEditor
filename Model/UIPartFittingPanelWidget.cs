// Decompiled with JetBrains decompiler
// Type: UIPartFittingPanelWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPartFittingPanelWidget : MonoBehaviour
{
  public UIPanelDriverInfo driverInfo;
  public UIPanelPartsInventory partsInventory;
  public Button autoPickButton;
  private Driver mDriver;
  private Car mCar;

  private void Start()
  {
    this.autoPickButton.onClick.AddListener(new UnityAction(this.OnDropdownValueChanged));
  }

  private void OnDropdownValueChanged()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mCar.carManager.AutoFit(this.mCar, CarManager.AutofitOptions.Performance, CarManager.AutofitAvailabilityOption.UnfitedParts);
    this.RefreshScreen();
  }

  private void RefreshScreen()
  {
    UIManager.instance.GetScreen<CarPartFittingScreen>().RefreshCarInventoryWidgets();
    this.driverInfo.SetHappinessData(this.mDriver, false);
  }

  public void Setup(Driver inDriver)
  {
    this.mDriver = inDriver;
    this.mCar = Game.instance.player.team.carManager.GetCarForDriver(this.mDriver);
    this.driverInfo.Setup(this.mDriver);
    this.partsInventory.Setup(this.mCar);
  }
}
