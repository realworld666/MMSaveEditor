// Decompiled with JetBrains decompiler
// Type: UIPastDriverChemistryWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class UIPastDriverChemistryWidget : MonoBehaviour
{
  public UIGridList pastChemistryList;
  [SerializeField]
  private GameObject mNoData;

  public void Setup(Mechanic inMechanic)
  {
    this.ClearList();
    List<Driver> entityList = Game.instance.driverManager.GetEntityList();
    Dictionary<string, Mechanic.DriverRelationship>.Enumerator enumerator = inMechanic.allDriverRelationships.GetEnumerator();
    while (enumerator.MoveNext())
    {
      string key = enumerator.Current.Key;
      for (int index = 0; index < entityList.Count; ++index)
      {
        Driver inDriver = entityList[index];
        if (inDriver.name.Equals(key, StringComparison.OrdinalIgnoreCase))
        {
          int numberOfUnlocks = this.CalculateNumberOfUnlocks(enumerator.Current.Value.relationshipAmount, inMechanic);
          this.pastChemistryList.CreateListItem<UIPastChemistryEntry>().Setup(inDriver, enumerator.Current.Value.numberOfWeeks, numberOfUnlocks);
        }
      }
    }
    GameUtility.SetActive(this.mNoData, inMechanic.allDriverRelationships.Count <= 0);
  }

  private int CalculateNumberOfUnlocks(float inRelationshipAmount, Mechanic inMechanic)
  {
    int num = 0;
    if ((double) inMechanic.bonusOne.bonusUnlockAt <= (double) inRelationshipAmount)
      ++num;
    if ((double) inMechanic.bonusTwo.bonusUnlockAt <= (double) inRelationshipAmount)
      ++num;
    return num;
  }

  private void ClearList()
  {
    this.pastChemistryList.DestroyListItems();
  }
}
