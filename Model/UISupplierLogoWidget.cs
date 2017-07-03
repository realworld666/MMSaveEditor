// Decompiled with JetBrains decompiler
// Type: UISupplierLogoWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UISupplierLogoWidget : MonoBehaviour
{
  [SerializeField]
  private Image mLogoImage;

  public void SetLogo(Supplier inSupplier)
  {
    using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
    {
      switch (inSupplier.supplierType)
      {
        case Supplier.SupplierType.Engine:
          builderSafe.stringBuilder.Append("SupplierLogos-EngineManufacturerLogos-EngineLogo");
          break;
        case Supplier.SupplierType.Brakes:
          builderSafe.stringBuilder.Append("SupplierLogos-EcuLogos-ECULogo");
          break;
        case Supplier.SupplierType.Fuel:
          builderSafe.stringBuilder.Append("SupplierLogos-FuelSupplierLogos-FuelLogo");
          break;
        case Supplier.SupplierType.Materials:
          builderSafe.stringBuilder.Append("SupplierLogos-ChassisMaterialsLogos-ChassisLogo");
          break;
        case Supplier.SupplierType.Battery:
          builderSafe.stringBuilder.Append("SupplierLogos-BatteryLogos-BatteryLogo");
          break;
      }
      builderSafe.stringBuilder.Append(inSupplier.logoIndex);
      this.mLogoImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Logos1, builderSafe.stringBuilder.ToString());
    }
  }
}
