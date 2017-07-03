// Decompiled with JetBrains decompiler
// Type: GridDriverWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GridDriverWidget : MonoBehaviour
{
  public Button carSetupButton;
  public GameObject notification;
  public TextMeshProUGUI notificationCountLabel;
  public Flag flag;
  public Smiley smiley;
  public UITyre tyre;
  public UICharacterPortrait driverPortrait;
  public TextMeshProUGUI driverNameLabel;
  public TextMeshProUGUI driverMentalStateLabel;
  public TextMeshProUGUI driverPositionLabel;
  public TextMeshProUGUI driverDeltaToPoleLabel;
  public TextMeshProUGUI teamNameLabel;
  private Driver mDriver;

  public Driver driver
  {
    get
    {
      return this.mDriver;
    }
  }

  private void Start()
  {
    this.carSetupButton.onClick.AddListener(new UnityAction(this.OnCarSetupButton));
  }

  private void Update()
  {
    this.smiley.SetType(this.mDriver.mentalState.smileyType);
    this.driverMentalStateLabel.text = this.mDriver.mentalState.GetText();
  }

  public void SetDriver(Driver inDriver)
  {
    this.mDriver = inDriver;
    RacingVehicle vehicle = Game.instance.vehicleManager.GetVehicle(this.mDriver);
    this.flag.SetNationality(this.mDriver.nationality);
    this.driverPortrait.SetPortrait((Person) this.mDriver);
    this.driverNameLabel.text = this.mDriver.shortName;
    this.driverPositionLabel.text = vehicle.standingsPosition.ToString();
    this.driverDeltaToPoleLabel.text = "+" + Localisation.GetLapTimeFormatting(true);
    this.teamNameLabel.text = this.mDriver.contract.GetTeam().name;
    this.tyre.SetTyre(Game.instance.vehicleManager.GetVehicle(this.mDriver).setup.tyreSet);
    this.notification.SetActive(true);
    this.notificationCountLabel.text = "1";
    this.Update();
  }

  public void OnCarSetupButton()
  {
    UIManager.instance.ChangeScreen("CarSetupScreen", (Entity) this.mDriver, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }
}
