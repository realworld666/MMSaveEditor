// Decompiled with JetBrains decompiler
// Type: UITravelSelectedSponsorWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UITravelSelectedSponsorWidget : MonoBehaviour
{
  public UISponsorLogo sponsorLogo;
  public TextMeshProUGUI qualifyingBonus;
  public TextMeshProUGUI qualifyingTarget;
  public TextMeshProUGUI raceBonus;
  public TextMeshProUGUI raceTarget;
  public TextMeshProUGUI combinedBonus;
  private SponsorshipDeal mSponsorshipDeal;

  public void OnStart()
  {
    this.Close();
  }

  public void Setup(SponsorshipDeal inSponsorship)
  {
    if (inSponsorship == null)
      return;
    this.mSponsorshipDeal = inSponsorship;
    this.gameObject.SetActive(true);
    this.SetDetails();
  }

  private void SetDetails()
  {
    this.sponsorLogo.SetSponsor(this.mSponsorshipDeal.sponsor);
    this.qualifyingBonus.text = GameUtility.GetCurrencyString((long) this.mSponsorshipDeal.qualifyingObjective.financialReward, 0);
    this.qualifyingTarget.text = this.mSponsorshipDeal.qualifyingObjective.objectiveText;
    this.raceBonus.text = GameUtility.GetCurrencyString((long) this.mSponsorshipDeal.raceObjective.financialReward, 0);
    this.raceTarget.text = this.mSponsorshipDeal.raceObjective.objectiveText;
    this.combinedBonus.text = GameUtility.GetCurrencyString((long) this.mSponsorshipDeal.GetObjectivesTotalBonus(), 0);
  }

  public void Close()
  {
    this.gameObject.SetActive(false);
  }
}
