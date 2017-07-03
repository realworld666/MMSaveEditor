// Decompiled with JetBrains decompiler
// Type: UIBonusIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class UIBonusIcon : MonoBehaviour
{
  private int mCarID = -1;
  private List<PersonalityTrait> mTraits = new List<PersonalityTrait>();
  public SessionDriverBonusRollover popup;

  public void Setup(RacingVehicle inVehicle)
  {
    this.mCarID = inVehicle.carID;
    this.mTraits = inVehicle.driver.personalityTraitController.GetAllPersonalityTraits();
    this.popup.Setup(this.mCarID, inVehicle.bonuses.bonusDisplayInfo, this.mTraits);
  }

  private void OnDisable()
  {
    this.HideRollover();
  }

  private void OnMouseEnter()
  {
    this.popup.Show(this.mCarID);
  }

  private void OnMouseExit()
  {
    this.HideRollover();
  }

  private void HideRollover()
  {
    this.popup.Hide();
  }
}
