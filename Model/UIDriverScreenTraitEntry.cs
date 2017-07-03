// Decompiled with JetBrains decompiler
// Type: UIDriverScreenTraitEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class UIDriverScreenTraitEntry : MonoBehaviour
{
  public GameObject popupTarget;
  public TextMeshProUGUI personalityTraitName;
  public GameObject temporaryIcon;
  public GameObject permanentIcon;
  private PersonalityTrait mPersonalityTrait;
  private bool mPopupOpen;

  public void SetupPersonalityTrait(PersonalityTrait inPersonalityTrait)
  {
    this.mPersonalityTrait = inPersonalityTrait;
    this.personalityTraitName.text = inPersonalityTrait.name;
    GameUtility.SetActive(this.temporaryIcon, inPersonalityTrait.data.type == PersonalityTraitData.TraitType.Temporary);
    GameUtility.SetActive(this.permanentIcon, inPersonalityTrait.data.type == PersonalityTraitData.TraitType.Permanent);
  }

  private void Update()
  {
    GameUtility.HandlePopup(ref this.mPopupOpen, this.popupTarget, new Action(this.OnMouseEnter), new Action(this.OnMouseExit));
  }

  public void OnMouseEnter()
  {
    UIManager.instance.dialogBoxManager.GetDialog<DriverTraitsRollover>().ShowRollover(this.mPersonalityTrait);
  }

  public void OnMouseExit()
  {
    DriverTraitsRollover dialog = UIManager.instance.dialogBoxManager.GetDialog<DriverTraitsRollover>();
    if (dialog.currentPersonalityTraitDisplay != this.mPersonalityTrait)
      return;
    dialog.Hide();
  }
}
