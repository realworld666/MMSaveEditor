// Decompiled with JetBrains decompiler
// Type: UISponsorsOfferEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISponsorsOfferEntry : MonoBehaviour
{
  public Button acceptButton;
  public UISponsorLogo logo;
  public TextMeshProUGUI upfrontPayment;
  public TextMeshProUGUI raceBonus;
  public TextMeshProUGUI perRacePayment;
  public TextMeshProUGUI dealLength;
  public UISponsorDifficulty bonusObjective;
  public GameObject bonusDifficultyObject;
  public GameObject homeRaceBonusObject;
  public GameObject raceBonusObject;
  public GameObject perRacePaymentObject;
  public GameObject notEnoughSlots;
  public GameObject[] bonusStars;
  public GameObject homeParent;
  public TextMeshProUGUI homeBonusNone;
  public TextMeshProUGUI homeBonus;
  public Flag homeFlag;
  public TextMeshProUGUI nationality;
  public TextMeshProUGUI offerExpires;
  public TextMeshProUGUI bonusPaymentLabel;
  private ContractSponsor mSponsorContract;

  public void Setup(ContractSponsor inContract)
  {
    this.acceptButton.onClick.RemoveAllListeners();
    this.acceptButton.onClick.AddListener(new UnityAction(this.OnAcceptButton));
    if (inContract == null)
      return;
    this.mSponsorContract = inContract;
    this.SetDetails();
  }

  private void SetDetails()
  {
    this.logo.SetSponsor(this.mSponsorContract.sponsor);
    this.upfrontPayment.text = GameUtility.GetCurrencyString((long) this.mSponsorContract.upfrontValue, 0);
    this.raceBonus.text = GameUtility.GetCurrencyString((long) this.mSponsorContract.bonusValuePerRace, 0);
    this.perRacePayment.text = GameUtility.GetCurrencyString((long) this.mSponsorContract.perRacePayment, 0);
    StringVariableParser.intValue1 = this.mSponsorContract.contractTotalRaces;
    this.dealLength.text = this.mSponsorContract.contractTotalRaces <= 1 ? Localisation.LocaliseID("PSG_10010929", (GameObject) null) : Localisation.LocaliseID("PSG_10010469", (GameObject) null);
    GameUtility.SetActive(this.raceBonusObject, this.mSponsorContract.bonusValuePerRace > 0);
    GameUtility.SetActive(this.bonusDifficultyObject, this.mSponsorContract.bonusValuePerRace > 0);
    GameUtility.SetActive(this.perRacePaymentObject, this.mSponsorContract.perRacePayment > 0);
    this.bonusObjective.SetDifficulty(this.mSponsorContract.sponsor.bonusTarget);
    this.SetStars();
    GameUtility.SetActive(this.homeRaceBonusObject, this.mSponsorContract.bonusValuePerRace > 0);
    if (this.homeRaceBonusObject.activeSelf)
    {
      GameUtility.SetActive(this.homeParent, this.mSponsorContract.sponsor.hasHomeBonus);
      GameUtility.SetActive(this.homeBonusNone.gameObject, !this.mSponsorContract.sponsor.hasHomeBonus);
      if (this.homeParent.activeSelf)
      {
        this.homeFlag.SetNationality(this.mSponsorContract.sponsor.nationality);
        this.nationality.text = this.mSponsorContract.sponsor.nationality.localisedCountry;
        this.homeBonus.text = this.mSponsorContract.sponsor.homeBonusMultiplier.ToString((IFormatProvider) Localisation.numberFormatter) + "X";
      }
    }
    StringVariableParser.contractSponsor = this.mSponsorContract;
    this.offerExpires.text = Localisation.LocaliseID("PSG_10010174", (GameObject) null);
    this.acceptButton.interactable = this.mSponsorContract.team.sponsorController.GetFreeSponsorSlots() > 0;
    GameUtility.SetActive(this.acceptButton.gameObject, this.acceptButton.interactable);
    GameUtility.SetActive(this.notEnoughSlots, !this.acceptButton.interactable);
    this.bonusPaymentLabel.text = Localisation.LocaliseID(!Game.instance.player.team.championship.rules.qualifyingBasedActive ? "PSG_10011043" : "PSG_10007931", (GameObject) null);
  }

  private void SetStars()
  {
    for (int index = 0; index < this.bonusStars.Length; ++index)
      GameUtility.SetActive(this.bonusStars[index], index < this.mSponsorContract.sponsor.prestigeLevel);
  }

  private void OnAcceptButton()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UISponsorsOfferEntry.\u003COnAcceptButton\u003Ec__AnonStorey9E buttonCAnonStorey9E = new UISponsorsOfferEntry.\u003COnAcceptButton\u003Ec__AnonStorey9E();
    // ISSUE: reference to a compiler-generated field
    buttonCAnonStorey9E.\u003C\u003Ef__this = this;
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    // ISSUE: reference to a compiler-generated field
    buttonCAnonStorey9E.widget = UIManager.instance.dialogBoxManager.GetDialog<UISponsorsOffers>();
    // ISSUE: reference to a compiler-generated method
    Action inActionSuccess = new Action(buttonCAnonStorey9E.\u003C\u003Em__1CE);
    // ISSUE: reference to a compiler-generated field
    buttonCAnonStorey9E.widget.screen.ProcessTransaction(this.mSponsorContract, inActionSuccess);
    // ISSUE: reference to a compiler-generated field
    buttonCAnonStorey9E.widget.Close();
  }
}
