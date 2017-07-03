// Decompiled with JetBrains decompiler
// Type: SelectedSponsorInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class SelectedSponsorInfo : MonoBehaviour
{
  public GameObject empty;
  public GameObject selected;
  public UISponsorLogo sponsorLogo;
  public TextMeshProUGUI objectiveLabel;
  public TextMeshProUGUI objectiveValueLabel;

  public void UpdateSelectedSponsor(SponsorshipDeal inSponsorshipDeal)
  {
    if (inSponsorshipDeal != null)
    {
      GameUtility.SetActive(this.empty, false);
      GameUtility.SetActive(this.selected, true);
      this.sponsorLogo.SetSponsor(inSponsorshipDeal.sponsor);
      int objectivesTotalBonus = inSponsorshipDeal.GetObjectivesTotalBonus();
      this.objectiveLabel.text = inSponsorshipDeal.raceObjective.objectiveText;
      this.objectiveValueLabel.text = GameUtility.GetCurrencyString((long) objectivesTotalBonus, 0);
    }
    else
    {
      GameUtility.SetActive(this.empty, true);
      GameUtility.SetActive(this.selected, false);
    }
  }
}
