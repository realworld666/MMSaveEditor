// Decompiled with JetBrains decompiler
// Type: UIMessagePersonalityBody
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class UIMessagePersonalityBody : UIMailMessageBody
{
  [Header("Derived Class Settings")]
  public TextMeshProUGUI driverStatus;
  public TextMeshProUGUI driverName;
  public Flag driverNationality;
  public UICharacterPortrait portrait;
  public UIAbilityStars stars;
  public TextMeshProUGUI traitHeader;
  public UIDriverScreenTraitEntry trait;

  public override void SetupSpecialObjectText()
  {
    PersonalityTrait inPersonalityTrait = this.message.specialObject[0] as PersonalityTrait;
    if (inPersonalityTrait == null)
      return;
    Driver ownerDriver = inPersonalityTrait.ownerDriver;
    this.trait.SetupPersonalityTrait(inPersonalityTrait);
    this.driverStatus.text = Localisation.LocaliseEnum((Enum) ownerDriver.contract.proposedStatus);
    this.driverName.text = ownerDriver.name;
    this.driverNationality.SetNationality(ownerDriver.nationality);
    this.portrait.SetPortrait((Person) ownerDriver);
    this.stars.SetAbilityStarsData(ownerDriver);
  }
}
