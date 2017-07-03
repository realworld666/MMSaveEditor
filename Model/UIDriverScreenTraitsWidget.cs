// Decompiled with JetBrains decompiler
// Type: UIDriverScreenTraitsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class UIDriverScreenTraitsWidget : MonoBehaviour
{
  public UIGridList traitGridList;
  public GameObject detailsObject;
  public GameObject noDetailsObject;

  public void SetupDriverTraits(Driver inDriver)
  {
    if (!inDriver.CanShowStats())
    {
      GameUtility.SetActive(this.detailsObject, false);
      if (!((Object) this.noDetailsObject != (Object) null))
        return;
      GameUtility.SetActive(this.noDetailsObject, true);
    }
    else
    {
      GameUtility.SetActive(this.detailsObject, true);
      if ((Object) this.noDetailsObject != (Object) null)
        GameUtility.SetActive(this.noDetailsObject, false);
      this.traitGridList.DestroyListItems();
      List<PersonalityTrait> personalityTraits = inDriver.personalityTraitController.GetAllPersonalityTraits();
      GameUtility.SetActive(this.traitGridList.itemPrefab, true);
      for (int index = 0; index < personalityTraits.Count; ++index)
        this.traitGridList.CreateListItem<UIDriverScreenTraitEntry>().SetupPersonalityTrait(personalityTraits[index]);
      GameUtility.SetActive(this.traitGridList.itemPrefab, false);
    }
  }
}
