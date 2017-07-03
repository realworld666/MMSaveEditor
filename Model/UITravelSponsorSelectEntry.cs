// Decompiled with JetBrains decompiler
// Type: UITravelSponsorSelectEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITravelSponsorSelectEntry : MonoBehaviour
{
  public Toggle toggle;
  public UISponsorLogo sponsorLogo;
  public TextMeshProUGUI sponsorObjective;
  public TextMeshProUGUI sponsorTotalBonus;
  public TravelArrangementsScreen screen;
  public Animator highlightAnimator;
  private SponsorshipDeal mSponsorshipDeal;

  public void OnStart()
  {
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle()));
  }

  public void Setup(SponsorshipDeal inSponsorshipDeal)
  {
    if (inSponsorshipDeal == null)
      return;
    this.mSponsorshipDeal = inSponsorshipDeal;
    this.sponsorLogo.SetSponsor(this.mSponsorshipDeal.sponsor);
    this.sponsorTotalBonus.text = GameUtility.GetCurrencyString((long) this.mSponsorshipDeal.GetObjectivesTotalBonus(), 0);
    this.sponsorObjective.text = this.mSponsorshipDeal.raceObjective.objectiveText;
  }

  private void OnToggle()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!this.toggle.isOn)
      return;
    this.screen.SelectSponsor(this.mSponsorshipDeal);
  }
}
