// Decompiled with JetBrains decompiler
// Type: ERSBatteryTabWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ERSBatteryTabWidget : MonoBehaviour
{
  public GameObject[] icons = new GameObject[0];
  public GameObject[] ERSIcons = new GameObject[0];
  public DriverActionButtons driverActionButtons;
  public Toggle selectionToggle;
  public ERSBatteryPanel ersBatteryPanel;
  public TextMeshProUGUI capacityLabel;
  public TextMeshProUGUI modeLabel;
  private RacingVehicle mVehicle;

  private void Awake()
  {
    this.selectionToggle.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnTogglePressed(value)));
  }

  public void SetVehicle(RacingVehicle inVehicle)
  {
    this.mVehicle = inVehicle;
  }

  private void DisableToggle()
  {
    this.selectionToggle.isOn = false;
  }

  private void OnTogglePressed(bool inValue)
  {
  }

  private void Update()
  {
    if (this.mVehicle == null)
      return;
    this.capacityLabel.text = string.Format("{0}% ", (object) Mathf.RoundToInt(this.mVehicle.ERSController.normalizedCharge * 100f));
    this.modeLabel.text = this.mVehicle.ERSController.mode.ToString();
    GameUtility.SetActive(this.icons[1], this.mVehicle.ERSController.mode == ERSController.Mode.Harvest && (double) this.mVehicle.ERSController.normalizedCharge >= 1.0);
    GameUtility.SetActive(this.icons[0], this.mVehicle.ERSController.mode == ERSController.Mode.Harvest && !this.mVehicle.HasStopped() && (double) this.mVehicle.ERSController.normalizedCharge < 1.0);
    GameUtility.SetActive(this.icons[2], this.mVehicle.ERSController.mode != ERSController.Mode.Harvest);
    for (ERSController.Mode mode = ERSController.Mode.Harvest; mode < ERSController.Mode.Count; ++mode)
      GameUtility.SetActive(this.ERSIcons[(int) mode], this.mVehicle.ERSController.mode == mode);
  }

  public enum IconType
  {
    FillingUp,
    Full,
    NotFull,
  }
}
