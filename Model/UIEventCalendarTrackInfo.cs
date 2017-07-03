// Decompiled with JetBrains decompiler
// Type: UIEventCalendarTrackInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIEventCalendarTrackInfo : MonoBehaviour
{
  public Button trackVariationsButton;
  public GameObject weatherForecast;
  public GameObject weatherUnknown;
  public UITravelCircuitStatWidget[] trackStats;
  public UITravelWeatherWidget[] weather;
  public TextMeshProUGUI fuelBurnRate;
  public TextMeshProUGUI tyreWearRate;
  public TextMeshProUGUI laps;
  public TextMeshProUGUI rainChance;
  public UITyreIcon[] tyresAvailable;
  public EventCalendarScreen screen;
  private RaceEventDetails mRaceEvent;
  private int mEventNumber;
  private Circuit mCircuit;

  public void OnStart()
  {
    this.trackVariationsButton.onClick.AddListener(new UnityAction(this.OnButton));
  }

  public void Setup(RaceEventDetails inRaceEvent, int inEventNumber)
  {
    if (inRaceEvent == null)
      return;
    this.mRaceEvent = inRaceEvent;
    this.mEventNumber = inEventNumber;
    this.mCircuit = this.mRaceEvent.circuit;
    this.SetDetails();
  }

  private void SetDetails()
  {
    this.SetupCircuitStats();
    this.SetupWeatherDetails();
    this.fuelBurnRate.text = this.mCircuit.GetFuelBurnLocalised();
    this.tyreWearRate.text = this.mCircuit.GetTyreWearLocalised();
    StringVariableParser.intValue1 = DesignDataManager.CalculateRaceLapCount(this.screen.championship, this.mCircuit.trackLengthMiles, false);
    StringVariableParser.floatValue1 = this.mCircuit.trackLengthMiles;
    this.laps.text = Localisation.LocaliseID("PSG_10010243", (GameObject) null);
    this.tyresAvailable[0].SetTyreIcon(this.mCircuit.firstTyreOption);
    this.tyresAvailable[1].SetTyreIcon(this.mCircuit.secondTyreOption);
    this.tyresAvailable[2].SetTyreIcon(this.mCircuit.thirdTyreOption);
    GameUtility.SetActive(this.tyresAvailable[2].gameObject, this.screen.championship.rules.compoundsAvailable > 2);
    this.tyresAvailable[3].SetTyreIcon(TyreSet.Compound.Intermediate);
    this.tyresAvailable[4].SetTyreIcon(TyreSet.Compound.Wet);
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
    if (count >= this.trackStats.Length)
      return;
    for (int index = 0; index < 6; ++index)
    {
      CarStats.StatType statType = (CarStats.StatType) index;
      if (CarPart.GetPartForStatType(statType, Game.instance.player.team.championship.series) != CarPart.PartType.None && CarStats.GetRelevancy(Mathf.RoundToInt(this.mCircuit.trackStatsCharacteristics.GetStat(statType))) == inRelevancy)
      {
        if (count < this.trackStats.Length)
          this.trackStats[count].SetupCircuitStat(this.mCircuit, statType);
        count = count + 1;
      }
    }
  }

  private void SetupWeatherDetails()
  {
    bool inIsActive = Game.instance.player.CanSeeEventWeatherForecast(this.screen.championship, this.mEventNumber);
    GameUtility.SetActive(this.weatherForecast, inIsActive);
    GameUtility.SetActive(this.weatherUnknown, !inIsActive);
    if (this.weatherForecast.activeSelf)
    {
      this.weather[0].SetupWeatherWidget(this.mRaceEvent.practiceSessions[0]);
      this.weather[2].SetupWeatherWidget(this.mRaceEvent.raceSessions[0]);
      GameUtility.SetActive(this.weather[1].gameObject, this.screen.championship.rules.qualifyingBasedActive);
      if (!this.weather[1].gameObject.activeSelf)
        return;
      this.weather[1].SetupWeatherWidget(this.mRaceEvent.qualifyingSessions[0]);
    }
    else
      this.rainChance.text = string.Format("{0} %", (object) Mathf.RoundToInt(this.mRaceEvent.circuit.climate.GetRainChance(this.mRaceEvent.eventDate.Month - 1) * 100f));
  }

  private void OnButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    UIEventCalendarVariationsPopup.ShowAllTrackLayouts(this.mCircuit, (Action) null);
  }
}
