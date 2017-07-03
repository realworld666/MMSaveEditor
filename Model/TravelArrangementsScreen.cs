// Decompiled with JetBrains decompiler
// Type: TravelArrangementsScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TravelArrangementsScreen : UIScreen
{
  public Transform[] tyresAvailable = new Transform[0];
  public TextMeshProUGUI nextEventDateLabel;
  public TextMeshProUGUI raceNameLabel;
  public TextMeshProUGUI circuitLayoutLabel;
  public TextMeshProUGUI championshipRoundLabel;
  public Flag flag;
  public UICircuitImage circuitImage;
  public UITrackLayout trackLayout;
  public SelectedSponsorInfo selectedSponsorInfo;
  public GameObject roundPips;
  public UITravelSelectionWidget selectionWidget;
  public UITravelWeatherWidget[] sessionWeather;
  public UITravelCircuitStatWidget[] circuitStats;
  public TextMeshProUGUI projectedFinanceHeader;
  public TextMeshProUGUI financeWithoutBonusLabel;
  public TextMeshProUGUI financeIncludingBonusLabel;
  public GameObject projectedFinanceIncludingBonus;
  public TextMeshProUGUI tyreWearRate;
  public TextMeshProUGUI fuelBurnRate;
  public TextMeshProUGUI laps;
  public GameObject warningContainer;
  public TextMeshProUGUI warningText;
  private Championship mChampionship;
  private SponsorshipDeal mSelectedSponsorship;
  private RoundInfoPip[] mRoundInfoPips;

  public SponsorshipDeal selectedSponsorshipDeal
  {
    get
    {
      return this.mSelectedSponsorship;
    }
  }

  public override void OnStart()
  {
    base.OnStart();
    this.mRoundInfoPips = this.roundPips.GetComponentsInChildren<RoundInfoPip>(true);
    this.selectionWidget.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.mChampionship = Game.instance.player.team.championship;
    UIManager.instance.navigationBars.bottomBar.subMenu.SetBool("Active", false);
    this.SetupRoundPips();
    RaceEventDetails currentEventDetails = this.mChampionship.GetCurrentEventDetails();
    this.trackLayout.SetCircuitIcon(currentEventDetails.circuit);
    this.circuitImage.SetCircuitIcon(currentEventDetails.circuit);
    this.nextEventDateLabel.text = GameUtility.FormatDateTimeToLongDateString(currentEventDetails.eventDate, string.Empty);
    this.raceNameLabel.text = Localisation.LocaliseID(currentEventDetails.circuit.locationNameID, (GameObject) null);
    this.fuelBurnRate.text = currentEventDetails.circuit.GetFuelBurnLocalised();
    this.tyreWearRate.text = currentEventDetails.circuit.GetTyreWearLocalised();
    this.laps.text = DesignDataManager.CalculateRaceLapCount(Game.instance.player.team.championship, currentEventDetails.circuit.trackLengthMiles, true).ToString() + " (" + GameUtility.GetDistanceTextFromMiles(currentEventDetails.circuit.trackLengthMiles, 1f) + ")";
    this.circuitLayoutLabel.text = Localisation.LocaliseEnum((Enum) currentEventDetails.circuit.trackLayout);
    StringVariableParser.intValue1 = this.mChampionship.eventNumberForUI;
    StringVariableParser.intValue2 = this.mChampionship.eventCount;
    this.championshipRoundLabel.text = Localisation.LocaliseID("PSG_10002217", (GameObject) null);
    this.SelectSponsor(Game.instance.player.team.sponsorController.weekendSponsorshipDeal);
    this.flag.SetNationality(currentEventDetails.circuit.nationality);
    this.SetTyre(this.tyresAvailable[0], currentEventDetails.circuit.firstTyreOption);
    this.SetTyre(this.tyresAvailable[1], currentEventDetails.circuit.secondTyreOption);
    ChampionshipRules rules = Game.instance.player.team.championship.rules;
    this.SetTyre(this.tyresAvailable[2], currentEventDetails.circuit.thirdTyreOption);
    this.tyresAvailable[2].gameObject.SetActive(rules.compoundsAvailable > 2);
    this.SetTyre(this.tyresAvailable[3], TyreSet.Compound.Intermediate);
    this.SetTyre(this.tyresAvailable[4], TyreSet.Compound.Wet);
    this.SetupCircuitStats();
    this.sessionWeather[0].SetupWeatherWidget(currentEventDetails.practiceSessions[0]);
    this.sessionWeather[2].SetupWeatherWidget(currentEventDetails.raceSessions[0]);
    GameUtility.SetActive(this.sessionWeather[1].gameObject, this.mChampionship.rules.qualifyingBasedActive);
    if (this.sessionWeather[1].gameObject.activeSelf)
      this.sessionWeather[1].SetupWeatherWidget(currentEventDetails.qualifyingSessions[0]);
    this.selectionWidget.Setup(this);
    this.SetupFinanceBreakdown();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionScreen, 0.0f);
  }

  private void SetupRoundPips()
  {
    int eventCount = this.mChampionship.eventCount;
    for (int index = 0; index < this.mRoundInfoPips.Length; ++index)
    {
      this.mRoundInfoPips[index].gameObject.SetActive(index < eventCount);
      if (index < eventCount)
        this.mRoundInfoPips[index].SetRaceEventDetails(this.mChampionship, this.mChampionship.calendar[index]);
    }
  }

  private void SetTyre(Transform inIconParent, TyreSet.Compound inCompound)
  {
    for (int index = 0; index < inIconParent.childCount; ++index)
    {
      if (inCompound == (TyreSet.Compound) index)
        inIconParent.GetChild(index).gameObject.SetActive(true);
      else
        inIconParent.GetChild(index).gameObject.SetActive(false);
    }
  }

  private void SetupCircuitStats()
  {
    int count = 0;
    this.SetStatForRelevancy(CarStats.RelevantToCircuit.VeryImportant, ref count);
    this.SetStatForRelevancy(CarStats.RelevantToCircuit.VeryUseful, ref count);
    this.SetStatForRelevancy(CarStats.RelevantToCircuit.Useful, ref count);
    this.SetStatForRelevancy(CarStats.RelevantToCircuit.No, ref count);
  }

  private void SetStatForRelevancy(CarStats.RelevantToCircuit inRelevancy, ref int count)
  {
    if (count >= this.circuitStats.Length)
      return;
    RaceEventDetails currentEventDetails = this.mChampionship.GetCurrentEventDetails();
    for (int index = 0; index < 6; ++index)
    {
      CarStats.StatType statType = (CarStats.StatType) index;
      if (CarPart.GetPartForStatType(statType, Game.instance.player.team.championship.series) != CarPart.PartType.None && CarStats.GetRelevancy(Mathf.RoundToInt(currentEventDetails.circuit.trackStatsCharacteristics.GetStat(statType))) == inRelevancy)
      {
        if (count < this.circuitStats.Length)
          this.circuitStats[count].SetupCircuitStat(currentEventDetails.circuit, statType);
        count = count + 1;
      }
    }
  }

  public override void OnExit()
  {
    base.OnExit();
  }

  private void Update()
  {
    if (this.selectionWidget.IsComplete() && this.requiredContinueButton != UIScreen.RequiredContinueButton.StartRace)
    {
      this.SetRequiredContinueButton(UIScreen.RequiredContinueButton.StartRace);
    }
    else
    {
      if (this.selectionWidget.IsComplete() || this.requiredContinueButton == UIScreen.RequiredContinueButton.ContinueOrMustRespond)
        return;
      this.SetRequiredContinueButton(UIScreen.RequiredContinueButton.ContinueOrMustRespond);
    }
  }

  public void RefreshWarnings()
  {
    switch (this.selectionWidget.currentWarning)
    {
      case UITravelSelectionWidget.warningStep.sponsors:
        this.warningText.text = Localisation.LocaliseID("PSG_10010622", (GameObject) null);
        break;
      case UITravelSelectionWidget.warningStep.tyres:
        this.warningText.text = Localisation.LocaliseID("PSG_10010623", (GameObject) null);
        break;
      case UITravelSelectionWidget.warningStep.partFitting:
        this.warningText.text = Localisation.LocaliseID("PSG_10010621", (GameObject) null);
        break;
    }
    GameUtility.SetActive(this.warningContainer, this.selectionWidget.currentWarning != UITravelSelectionWidget.warningStep.none);
  }

  public void SelectSponsor(SponsorshipDeal inSponsorshipDeal)
  {
    if (inSponsorshipDeal != null)
      this.mSelectedSponsorship = inSponsorshipDeal;
    this.selectedSponsorInfo.UpdateSelectedSponsor(inSponsorshipDeal);
    Game.instance.player.team.sponsorController.SetWeekendSponsor(this.mSelectedSponsorship);
    this.SetupFinanceBreakdown();
  }

  public void ResetSponsorshipDeal()
  {
    this.mSelectedSponsorship = (SponsorshipDeal) null;
    this.selectedSponsorInfo.UpdateSelectedSponsor((SponsorshipDeal) null);
    Game.instance.player.team.sponsorController.SetWeekendSponsor(this.mSelectedSponsorship);
    this.SetupFinanceBreakdown();
  }

  private void SetupFinanceBreakdown()
  {
    bool inIsActive = this.mSelectedSponsorship != null;
    GameUtility.SetActive(this.projectedFinanceHeader.gameObject, false);
    GameUtility.SetActive(this.projectedFinanceIncludingBonus, inIsActive);
    TeamFinanceController financeController = Game.instance.player.team.financeController;
    long totalCostPerRace = financeController.GetTotalCostPerRace();
    this.financeWithoutBonusLabel.text = GameUtility.GetCurrencyString(totalCostPerRace, 0);
    this.financeWithoutBonusLabel.color = totalCostPerRace < 0L ? UIConstants.colorBandRed : UIConstants.colorBandGreen;
    if (!inIsActive)
      return;
    long inValue = totalCostPerRace + financeController.GetTotalBonusCostPerRace(this.mSelectedSponsorship.sponsor.bonusTarget);
    this.financeIncludingBonusLabel.text = GameUtility.GetCurrencyString(inValue, 0);
    this.financeIncludingBonusLabel.color = inValue < 0L ? UIConstants.colorBandRed : UIConstants.colorBandGreen;
  }

  public void ShowFinanceRolloverBreakdown(bool inConsiderBonuses)
  {
    TeamFinanceController financeController = Game.instance.player.team.financeController;
    List<Transaction> inList = new List<Transaction>();
    for (Transaction.Group group = Transaction.Group.CarParts; group < Transaction.Group.Count; ++group)
    {
      long costPerRace = financeController.GetCostPerRace(group);
      if (costPerRace != 0L)
      {
        Transaction transaction = new Transaction(group, Transaction.Type.Debit, costPerRace, Localisation.LocaliseEnum((Enum) group));
        if (costPerRace > 0L)
          transaction.transactionType = Transaction.Type.Credit;
        inList.Add(transaction);
      }
    }
    if (inConsiderBonuses && this.mSelectedSponsorship != null)
    {
      int bonusTarget = this.mSelectedSponsorship.contract.sponsor.bonusTarget;
      inList.AddRange((IEnumerable<Transaction>) financeController.GetAllEventBonusTransactions(bonusTarget));
      UIManager.instance.dialogBoxManager.GetDialog<FinanceBreakdownDialogBox>().ShowRollover(inList, Localisation.LocaliseID("PSG_10010881", (GameObject) null));
    }
    else
      UIManager.instance.dialogBoxManager.GetDialog<FinanceBreakdownDialogBox>().ShowRollover(inList, Localisation.LocaliseID("PSG_10010882", (GameObject) null));
  }

  public void HideFinanceRolloverBreakdown()
  {
    UIManager.instance.dialogBoxManager.GetDialog<FinanceBreakdownDialogBox>().Hide();
  }

  private void SetSponsorWeekened()
  {
    if (this.mSelectedSponsorship == null)
      return;
    this.mSelectedSponsorship.contract.SetRaceAttended(this.mChampionship.GetCurrentEventDetails().circuit);
    this.mSelectedSponsorship.contract.lattestRaceAttendedDate = this.mChampionship.GetCurrentEventDetails().eventDate;
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    if (!this.selectionWidget.IsComplete())
    {
      this.selectionWidget.GoToNextStep();
    }
    else
    {
      this.selectionWidget.DisableButtons();
      this.SetSponsorWeekened();
      Game.instance.player.team.GetDriver(0).carOpinion.ApplyMoraleHit();
      Game.instance.player.team.GetDriver(1).carOpinion.ApplyMoraleHit();
      if (Game.instance.stateInfo.isReadyToGoToRace)
        Game.instance.stateInfo.GoToNextState();
    }
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }
}
