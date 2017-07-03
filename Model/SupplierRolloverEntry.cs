// Decompiled with JetBrains decompiler
// Type: SupplierRolloverEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class SupplierRolloverEntry : MonoBehaviour
{
  public TextMeshProUGUI supplierCategoryLabel;
  public TextMeshProUGUI statOneLabel;
  public TextMeshProUGUI statTwoLabel;
  public TextMeshProUGUI statThreeLabel;
  public TextMeshProUGUI costLabel;
  public GameObject supplierThreeContainer;
  public UISupplierLogoWidget supplierLogo;
  private Supplier mSupplier;

  public void Setup(Supplier inSupplier, Team inTeam)
  {
    this.mSupplier = inSupplier;
    UISupplierOption.SetupSupplierDetails(this.mSupplier, this.supplierLogo, this.supplierThreeContainer, this.statOneLabel, this.statTwoLabel, this.statThreeLabel);
    this.supplierCategoryLabel.text = Localisation.LocaliseEnum((Enum) this.mSupplier.supplierType);
    this.costLabel.text = GameUtility.GetCurrencyString((long) this.mSupplier.GetPrice(inTeam), 0);
  }
}
