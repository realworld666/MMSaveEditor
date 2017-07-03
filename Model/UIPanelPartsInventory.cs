// Decompiled with JetBrains decompiler
// Type: UIPanelPartsInventory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIPanelPartsInventory : MonoBehaviour
{
  public UIPanelInventoryPartEntry[] partSlots = new UIPanelInventoryPartEntry[0];
  private Car mCar;

  public void Setup(Car inCar)
  {
    this.mCar = inCar;
    for (int index = 0; index < this.partSlots.Length; ++index)
    {
      CarPart.PartType inType = this.mCar.carManager.team.championship.series != Championship.Series.SingleSeaterSeries ? this.partSlots[index].slotTypeGT : this.partSlots[index].slotType;
      if (inType == CarPart.PartType.None)
      {
        GameUtility.SetActive(this.partSlots[index].gameObject, false);
      }
      else
      {
        CarPart part = this.mCar.GetPart(inType);
        this.partSlots[index].Setup(part);
        GameUtility.SetActive(this.partSlots[index].gameObject, true);
      }
    }
  }

  public void Refresh()
  {
    for (int index = 0; index < this.partSlots.Length; ++index)
    {
      CarPart.PartType inType = this.mCar.carManager.team.championship.series != Championship.Series.SingleSeaterSeries ? this.partSlots[index].slotTypeGT : this.partSlots[index].slotType;
      if (inType != CarPart.PartType.None)
      {
        CarPart part = this.mCar.GetPart(inType);
        this.partSlots[index].Refresh(part);
      }
    }
  }

  public void HighLight(CarPart.PartType inPartType)
  {
    for (int index = 0; index < this.partSlots.Length; ++index)
    {
      CarPart.PartType partType = this.mCar.carManager.team.championship.series != Championship.Series.SingleSeaterSeries ? this.partSlots[index].slotTypeGT : this.partSlots[index].slotType;
      if (partType != CarPart.PartType.None)
      {
        if (partType == inPartType)
        {
          this.partSlots[index].animator.SetBool("SlotFocused", true);
          this.partSlots[index].animator.SetBool("SlotUnfocused", false);
        }
        else
        {
          this.partSlots[index].animator.SetBool("SlotUnfocused", true);
          this.partSlots[index].animator.SetBool("SlotFocused", false);
        }
      }
    }
  }
}
