// Decompiled with JetBrains decompiler
// Type: SupplierRolloverWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SupplierRolloverWidget : UIDialogBox
{
  public SupplierRolloverEntry engineSupplier;
  public SupplierRolloverEntry fuelSupplier;
  public SupplierRolloverEntry batterySupplier;
  public SupplierRolloverEntry brakesSupplier;
  public SupplierRolloverEntry materialsSupplier;
  public GameObject batteryGridContainer;
  public TextMeshProUGUI totalCostLabel;
  private Team mTeam;
  private RectTransform mRectTransform;

  protected override void Awake()
  {
    base.Awake();
    this.mRectTransform = this.gameObject.GetComponent<RectTransform>();
  }

  public void Open(Team inTeam, bool inForceCurrentSuppliers = false)
  {
    this.gameObject.SetActive(true);
    this.mTeam = inTeam;
    CarChassisStats chassisStats = this.mTeam.carManager.GetCar(0).chassisStats;
    if (!inForceCurrentSuppliers && App.instance.gameStateManager.currentState is PreSeasonState && (this.mTeam.IsPlayersTeam() && (App.instance.gameStateManager.currentState as PreSeasonState).stage >= PreSeasonState.PreSeasonStage.DesigningCar) && this.mTeam.carManager.nextYearCarDesign.chassisStats != null)
      chassisStats = this.mTeam.carManager.nextYearCarDesign.chassisStats;
    this.engineSupplier.Setup(chassisStats.supplierEngine, this.mTeam);
    this.fuelSupplier.Setup(chassisStats.supplierFuel, this.mTeam);
    this.brakesSupplier.Setup(chassisStats.supplierBrakes, this.mTeam);
    this.materialsSupplier.Setup(chassisStats.supplierMaterials, this.mTeam);
    if (this.mTeam.championship.rules.isEnergySystemActive && chassisStats.supplierBattery != null && chassisStats.supplierBattery.supplierType == Supplier.SupplierType.Battery)
    {
      this.batterySupplier.Setup(chassisStats.supplierBattery, this.mTeam);
      GameUtility.SetActive(this.batteryGridContainer, true);
    }
    else
      GameUtility.SetActive(this.batteryGridContainer, false);
    List<Supplier> suppliers = chassisStats.GetSuppliers();
    long inValue = 0;
    for (int index = 0; index < suppliers.Count; ++index)
    {
      if (suppliers[index].supplierType != Supplier.SupplierType.Battery || this.mTeam.championship.rules.isEnergySystemActive)
        inValue += (long) suppliers[index].GetPrice(this.mTeam);
    }
    this.totalCostLabel.text = GameUtility.GetCurrencyString(inValue, 0);
  }

  private void Update()
  {
    GameUtility.SetTooltipTransformInsideScreen(this.mRectTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
  }

  public void Close()
  {
    this.gameObject.SetActive(false);
  }
}
