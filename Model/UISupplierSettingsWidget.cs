// Decompiled with JetBrains decompiler
// Type: UISupplierSettingsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UISupplierSettingsWidget : MonoBehaviour
{
  public UISupplierButtonEntry[] supplierButtons = new UISupplierButtonEntry[0];
  public TextMeshProUGUI totalCostLabel;

  public void OnEnter()
  {
    for (int index = 0; index < this.supplierButtons.Length; ++index)
      this.supplierButtons[index].OnEnter();
    this.UpdateTotalCost();
    this.Open();
  }

  public void UpdateTotalCost()
  {
    int num = 0;
    Team team = Game.instance.player.team;
    for (int index = 0; index < this.supplierButtons.Length; ++index)
    {
      if (this.supplierButtons[index].supplier != null)
        num += this.supplierButtons[index].supplier.GetPrice(team);
    }
    this.totalCostLabel.text = GameUtility.GetCurrencyString((long) num, 0);
  }

  public void Open()
  {
    this.gameObject.SetActive(true);
  }

  public void Close()
  {
    this.gameObject.SetActive(false);
  }
}
