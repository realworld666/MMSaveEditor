// Decompiled with JetBrains decompiler
// Type: UISponsorsOffers
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISponsorsOffers : UIDialogBox
{
  private UISponsorsOffers.SponsorFilter mFilter = UISponsorsOffers.SponsorFilter.BonusFund;
  private List<ContractSponsor> mOffers = new List<ContractSponsor>();
  private SponsorSlot.SlotType mSlotType = SponsorSlot.SlotType.AirIntake;
  public UISponsorsFilter[] filters;
  public UIGridList grid;
  public Button backingCloseButton;
  public Button closeButton;
  public GameObject noOffers;
  private SponsorsScreen mScreen;
  private Team mTeam;
  private bool mFilterASC;

  public bool filterASC
  {
    get
    {
      return this.mFilterASC;
    }
  }

  public SponsorsScreen screen
  {
    get
    {
      return this.mScreen;
    }
  }

  public void OnStart()
  {
    this.mScreen = UIManager.instance.GetScreen<SponsorsScreen>();
    this.grid.OnStart();
    this.backingCloseButton.onClick.AddListener(new UnityAction(this.Close));
    this.closeButton.onClick.AddListener(new UnityAction(this.Close));
    for (int index = 0; index < this.filters.Length; ++index)
      this.filters[index].OnStart();
  }

  public void Open(SponsorSlot.SlotType inSlotType)
  {
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) this);
    this.mSlotType = inSlotType;
    this.mTeam = Game.instance.player.team;
    this.Filter(this.mFilter, false);
    Game.instance.notificationManager.GetNotification("NewSponsorOffer" + this.mSlotType.ToString()).ResetCount();
  }

  public void Filter(UISponsorsOffers.SponsorFilter inFilter, bool inCanReverse)
  {
    this.mOffers = new List<ContractSponsor>((IEnumerable<ContractSponsor>) this.mTeam.sponsorController.sponsorOffers.GetMap(this.mSlotType));
    this.mFilterASC = this.mFilter == inFilter && inCanReverse && !this.mFilterASC;
    this.mFilter = inFilter;
    switch (inFilter)
    {
      case UISponsorsOffers.SponsorFilter.Company:
        this.mOffers.Sort((Comparison<ContractSponsor>) ((x, y) => x.sponsor.name.CompareTo(y.sponsor.name)));
        break;
      case UISponsorsOffers.SponsorFilter.Slots:
        this.mOffers.Sort((Comparison<ContractSponsor>) ((x, y) => x.slotSponsoringType.CompareTo((object) y.slotSponsoringType)));
        break;
      case UISponsorsOffers.SponsorFilter.UpfrontPayment:
        this.mOffers.Sort((Comparison<ContractSponsor>) ((x, y) => x.upfrontValue.CompareTo(y.upfrontValue)));
        break;
      case UISponsorsOffers.SponsorFilter.RaceBonus:
        this.mOffers.Sort((Comparison<ContractSponsor>) ((x, y) => x.bonusValuePerRace.CompareTo(y.bonusValuePerRace)));
        break;
      case UISponsorsOffers.SponsorFilter.DealLength:
        this.mOffers.Sort((Comparison<ContractSponsor>) ((x, y) => x.contractTotalRaces.CompareTo(y.contractTotalRaces)));
        break;
      case UISponsorsOffers.SponsorFilter.Difficulty:
        this.mOffers.Sort((Comparison<ContractSponsor>) ((x, y) => x.sponsor.difficulty.CompareTo((object) y.sponsor.difficulty)));
        break;
      case UISponsorsOffers.SponsorFilter.BonusFund:
        this.mOffers.Sort((Comparison<ContractSponsor>) ((x, y) => x.sponsor.prestigeLevel.CompareTo(y.sponsor.prestigeLevel)));
        break;
      case UISponsorsOffers.SponsorFilter.HomeBonus:
        this.mOffers.Sort((Comparison<ContractSponsor>) ((x, y) => x.sponsor.homeBonusMultiplier.CompareTo(y.sponsor.homeBonusMultiplier)));
        break;
      case UISponsorsOffers.SponsorFilter.OfferExpire:
        this.mOffers.Sort((Comparison<ContractSponsor>) ((x, y) => x.offerRacesLeft.CompareTo(y.offerRacesLeft)));
        break;
    }
    if (!this.mFilterASC)
      this.mOffers.Reverse();
    for (int index = 0; index < this.filters.Length; ++index)
    {
      UISponsorsFilter filter = this.filters[index];
      if (filter.filter != this.mFilter)
        filter.DisableArrows();
    }
    this.SetGrid();
  }

  private void SetGrid()
  {
    int itemCount = this.grid.itemCount;
    int count = this.mOffers.Count;
    int num = 0;
    for (int inIndex = 0; inIndex < Mathf.Max(itemCount, count); ++inIndex)
    {
      UISponsorsOfferEntry sponsorsOfferEntry = this.grid.GetOrCreateItem<UISponsorsOfferEntry>(inIndex);
      GameUtility.SetActive(sponsorsOfferEntry.gameObject, inIndex < count);
      if (sponsorsOfferEntry.gameObject.activeSelf)
      {
        ContractSponsor mOffer = this.mOffers[inIndex];
        sponsorsOfferEntry.Setup(mOffer);
        ++num;
      }
    }
    GameUtility.SetActive(this.grid.itemPrefab, false);
    GameUtility.SetActive(this.noOffers, num == 0);
  }

  public void Close()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    UIManager.instance.dialogBoxManager.GetDialog<UISponsorsOffers>().Hide();
  }

  public enum SponsorFilter
  {
    Company,
    Slots,
    UpfrontPayment,
    RaceBonus,
    DealLength,
    Difficulty,
    BonusFund,
    HomeBonus,
    OfferExpire,
  }
}
