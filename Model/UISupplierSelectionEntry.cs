// Decompiled with JetBrains decompiler
// Type: UISupplierSelectionEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISupplierSelectionEntry : MonoBehaviour
{
  private Supplier.SupplierType mSupplierType = Supplier.SupplierType.Brakes;
  public UIGridList supplierOptionsEntries;
  public TextMeshProUGUI statOne;
  public TextMeshProUGUI statTwo;
  public TextMeshProUGUI statThree;
  public GameObject thirdStatOption;
  private UISupplierSelectionStep mSelectionStep;
  private Supplier mSelectedSupplier;

  public Supplier selectedSupplier
  {
    get
    {
      return this.mSelectedSupplier;
    }
  }

  public void OnStart(Supplier.SupplierType inSupplierType, UISupplierSelectionStep inStep)
  {
    this.mSupplierType = inSupplierType;
    this.mSelectionStep = inStep;
  }

  public void OnEnter()
  {
    List<Supplier> suppliersForTeam = Game.instance.supplierManager.GetSuppliersForTeam(this.mSupplierType, Game.instance.player.team, false);
    this.supplierOptionsEntries.HideListItems();
    ToggleGroup component = this.supplierOptionsEntries.grid.GetComponent<ToggleGroup>();
    for (int inIndex = 0; inIndex < suppliersForTeam.Count; ++inIndex)
      this.supplierOptionsEntries.GetOrCreateItem<UISupplierOption>(inIndex).Setup(this, suppliersForTeam[inIndex], component);
    this.SetupHeaderLabels();
    this.mSelectedSupplier = (Supplier) null;
  }

  private void SetupHeaderLabels()
  {
    switch (this.mSupplierType)
    {
      case Supplier.SupplierType.Engine:
      case Supplier.SupplierType.Fuel:
        this.statOne.text = Localisation.LocaliseID("PSG_10004256", (GameObject) null);
        this.statTwo.text = Localisation.LocaliseID("PSG_10004258", (GameObject) null);
        if (this.mSupplierType == Supplier.SupplierType.Engine)
        {
          GameUtility.SetActive(this.thirdStatOption, true);
          this.statThree.text = Localisation.LocaliseID("PSG_10011045", (GameObject) null);
          break;
        }
        GameUtility.SetActive(this.thirdStatOption, false);
        break;
      case Supplier.SupplierType.Brakes:
      case Supplier.SupplierType.Materials:
        this.statOne.text = Localisation.LocaliseID("PSG_10004259", (GameObject) null);
        this.statTwo.text = Localisation.LocaliseID("PSG_10004260", (GameObject) null);
        GameUtility.SetActive(this.thirdStatOption, false);
        break;
      case Supplier.SupplierType.Battery:
        GameUtility.SetActive(this.thirdStatOption, false);
        this.statOne.text = Localisation.LocaliseID("PSG_10011518", (GameObject) null);
        this.statTwo.text = Localisation.LocaliseID("PSG_10011520", (GameObject) null);
        break;
    }
  }

  public void SupplierSelected(Supplier inSelectedSupplier)
  {
    this.mSelectedSupplier = inSelectedSupplier;
    this.mSelectionStep.SetComplete(true);
  }
}
