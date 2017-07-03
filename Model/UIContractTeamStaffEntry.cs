// Decompiled with JetBrains decompiler
// Type: UIContractTeamStaffEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class UIContractTeamStaffEntry : MonoBehaviour
{
  public UICharacterPortrait driverPortrait;
  public UICharacterPortrait staffPortrait;
  public TextMeshProUGUI label;

  public void Setup(Person inPerson, Person inTarget)
  {
    GameUtility.SetActive(this.gameObject, inPerson != inTarget);
    if (!this.gameObject.activeSelf)
      return;
    bool inIsActive = inPerson is Driver;
    GameUtility.SetActive(this.driverPortrait.gameObject, inIsActive);
    GameUtility.SetActive(this.staffPortrait.gameObject, !inIsActive);
    if (inIsActive)
    {
      this.driverPortrait.SetPortrait(inPerson);
      this.label.text = Localisation.LocaliseEnum((Enum) inPerson.contract.currentStatus);
    }
    else
    {
      this.staffPortrait.SetPortrait(inPerson);
      this.label.text = Localisation.LocaliseEnum((Enum) inPerson.contract.job);
    }
  }
}
