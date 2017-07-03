// Decompiled with JetBrains decompiler
// Type: UISponsorsOverview
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISponsorsOverview : UIDialogBox
{
  private Vector3 mMouseOffset = new Vector3(30f, 30f, 0.0f);
  public RectTransform rectTransform;
  public Button backgroundButton;
  public Button closeButton;
  public Flag flag;
  public UISponsorLogo logo;
  public TextMeshProUGUI sponsorName;
  public TextMeshProUGUI sponsorLocation;
  public TextMeshProUGUI homeBonus;
  public TextMeshProUGUI raceBonus;
  public TextMeshProUGUI upfront;
  public TextMeshProUGUI remainingLength;
  public TextMeshProUGUI incomePerRace;
  public UISponsorDifficulty difficulty;
  public TextMeshProUGUI totalAwarded;
  public TextMeshProUGUI bonusPoolLabel;
  public UIGridList slotsGrid;
  public GameObject incomeContainer;
  public GameObject bonusDifficultyContainer;
  public GameObject homeBonusContainer;
  public GameObject raceBonusContainer;
  public GameObject[] prestigeStars;
  private SponsorsScreen mScreen;
  private SponsorshipDeal mSponsorshipDeal;

  public void OnStart()
  {
    this.backgroundButton.onClick.AddListener(new UnityAction(this.OnCloseButton));
    this.closeButton.onClick.AddListener(new UnityAction(this.OnCloseButton));
    this.slotsGrid.OnStart();
    this.mScreen = UIManager.instance.GetScreen<SponsorsScreen>();
  }

  public void Open(SponsorshipDeal inSponsorshipDeal)
  {
    if (inSponsorshipDeal == null)
      return;
    scSoundManager.BlockSoundEvents = true;
    this.mSponsorshipDeal = inSponsorshipDeal;
    this.SetSponsorSlots();
    this.SetDetails();
    this.SetPrestige();
    this.UpdatePosition();
    GameUtility.SetActive(this.gameObject, true);
    scSoundManager.BlockSoundEvents = false;
  }

  private void Update()
  {
    this.UpdatePosition();
  }

  public void SetDetails()
  {
    this.logo.SetSponsor(this.mSponsorshipDeal.sponsor);
    this.sponsorName.text = this.mSponsorshipDeal.sponsor.name;
    this.sponsorLocation.text = this.mSponsorshipDeal.sponsor.nationality.localisedCountry;
    this.flag.SetNationality(this.mSponsorshipDeal.sponsor.nationality);
    bool hasPerRacePayment = this.mSponsorshipDeal.hasPerRacePayment;
    GameUtility.SetActive(this.incomeContainer.gameObject, hasPerRacePayment);
    GameUtility.SetActive(this.homeBonusContainer.gameObject, !hasPerRacePayment);
    GameUtility.SetActive(this.raceBonusContainer.gameObject, !hasPerRacePayment);
    GameUtility.SetActive(this.bonusDifficultyContainer.gameObject, !hasPerRacePayment);
    this.upfront.text = GameUtility.GetCurrencyString((long) this.mSponsorshipDeal.contract.upfrontValue, 0);
    StringVariableParser.intValue1 = this.mSponsorshipDeal.contract.contractRacesLeft;
    this.remainingLength.text = this.mSponsorshipDeal.contract.contractRacesLeft <= 1 ? Localisation.LocaliseID("PSG_10010929", (GameObject) null) : Localisation.LocaliseID("PSG_10010469", (GameObject) null);
    this.totalAwarded.text = GameUtility.GetCurrencyString((long) this.mSponsorshipDeal.contract.historyTotalBonus, 0);
    if (hasPerRacePayment)
    {
      this.incomePerRace.text = GameUtility.GetCurrencyString((long) this.mSponsorshipDeal.contract.perRacePayment, 0);
    }
    else
    {
      this.homeBonus.text = !this.mSponsorshipDeal.sponsor.hasHomeBonus ? Localisation.LocaliseID("PSG_10005815", (GameObject) null) : this.mSponsorshipDeal.sponsor.homeBonusMultiplier.ToString((IFormatProvider) Localisation.numberFormatter) + "X";
      this.raceBonus.text = GameUtility.GetCurrencyString((long) this.mSponsorshipDeal.contract.bonusValuePerRace, 0);
      this.difficulty.SetDifficulty(this.mSponsorshipDeal.sponsor.bonusTarget);
    }
    this.bonusPoolLabel.text = Localisation.LocaliseID(!Game.instance.player.team.championship.rules.qualifyingBasedActive ? "PSG_10011042" : "PSG_10009856", (GameObject) null);
  }

  public void SetSponsorSlots()
  {
    this.slotsGrid.DestroyListItems();
    UISponsorsPanel.SponsorData sponsorData = this.mScreen.panelWidget.GetSponsorData(this.mSponsorshipDeal);
    if (sponsorData != null)
    {
      UISponsorsPanel.SponsorSlotData slot = sponsorData.slot;
      UISponsorsSlotEntry listItem = this.slotsGrid.CreateListItem<UISponsorsSlotEntry>();
      GameUtility.SetActive(listItem.gameObject, true);
      listItem.Setup(slot.number, slot.index);
    }
    GameUtility.SetActive(this.slotsGrid.itemPrefab, false);
  }

  public void SetPrestige()
  {
    for (int index = 0; index < this.prestigeStars.Length; ++index)
      GameUtility.SetActive(this.prestigeStars[index], index < this.mSponsorshipDeal.sponsor.prestigeLevel);
  }

  private void UpdatePosition()
  {
    GameUtility.SetTooltipTransformInsideScreen(this.rectTransform, (RectTransform) null, this.mMouseOffset, false, (RectTransform) null);
  }

  public void Close()
  {
    GameUtility.SetActive(this.gameObject, false);
  }

  public void OnCloseButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.Close();
  }
}
