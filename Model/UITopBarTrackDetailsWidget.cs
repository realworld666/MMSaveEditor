// Decompiled with JetBrains decompiler
// Type: UITopBarTrackDetailsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITopBarTrackDetailsWidget : MonoBehaviour
{
  public Transform[] tyresAvailable = new Transform[0];
  public Button leftButton;
  public Button rightButton;
  public GameObject weatherForecast;
  public GameObject weatherUnknown;
  public UICircuitImage circuitImage;
  public TextMeshProUGUI fuelBurnRate;
  public TextMeshProUGUI tyreWearRate;
  public TextMeshProUGUI laps;
  public TextMeshProUGUI trackGuide;
  public TextMeshProUGUI rainChance;
  public UITravelWeatherWidget[] sessionWeather;
  public UITravelCircuitStatWidget[] circuitStats;
  public UITopBarNextEvent widget;
  private int mEvent;
  private int mMinEvent;
  private int mMaxEvent;
  private RaceEventDetails mRaceEvent;
  private Circuit mCircuit;

  public void SetData(Circuit inCircuit)
  {
    if (inCircuit == null)
      return;
    this.mEvent = Game.instance.player.team.championship.eventNumber;
    this.mMinEvent = this.mEvent;
    this.mMaxEvent = Game.instance.player.team.championship.eventCount - 1;
    this.mRaceEvent = Game.instance.player.team.championship.calendar[this.mEvent];
    this.mCircuit = inCircuit;
    this.Refresh();
  }

  private void Refresh()
  {
    Championship championship = Game.instance.player.team.championship;
    this.leftButton.onClick.RemoveAllListeners();
    this.rightButton.onClick.RemoveAllListeners();
    this.leftButton.onClick.AddListener(new UnityAction(this.OnLeftButton));
    this.rightButton.onClick.AddListener(new UnityAction(this.OnRightButton));
    this.circuitImage.SetCircuitIcon(this.mCircuit);
    this.fuelBurnRate.text = this.mCircuit.GetFuelBurnLocalised();
    this.tyreWearRate.text = this.mCircuit.GetTyreWearLocalised();
    this.SetTyre(this.tyresAvailable[0], this.mCircuit.firstTyreOption);
    this.SetTyre(this.tyresAvailable[1], this.mCircuit.secondTyreOption);
    this.SetTyre(this.tyresAvailable[2], this.mCircuit.thirdTyreOption);
    GameUtility.SetActive(this.tyresAvailable[2].gameObject, championship.rules.compoundsAvailable > 2);
    this.SetTyre(this.tyresAvailable[3], TyreSet.Compound.Intermediate);
    this.SetTyre(this.tyresAvailable[4], TyreSet.Compound.Wet);
    this.mRaceEvent = championship.calendar[this.mEvent];
    bool inIsActive = Game.instance.player.CanSeeEventWeatherForecast(championship, this.mEvent);
    GameUtility.SetActive(this.weatherForecast, inIsActive);
    GameUtility.SetActive(this.weatherUnknown, !inIsActive);
    if (this.weatherForecast.activeSelf)
    {
      this.sessionWeather[0].SetupWeatherWidget(this.mRaceEvent.practiceSessions[0]);
      this.sessionWeather[2].SetupWeatherWidget(this.mRaceEvent.raceSessions[0]);
      GameUtility.SetActive(this.sessionWeather[1].gameObject, championship.rules.qualifyingBasedActive);
      if (this.sessionWeather[1].gameObject.activeSelf)
        this.sessionWeather[1].SetupWeatherWidget(this.mRaceEvent.qualifyingSessions[0]);
    }
    else
      this.rainChance.text = string.Format("{0} %", (object) (float) (Mathf.RoundToInt(Mathf.Max(1f, this.mRaceEvent.circuit.climate.GetRainChance(this.mRaceEvent.eventDate.Month - 1) * 10f)) * 10));
    this.RefreshLabelsText();
    this.SetupCircuitStats();
    this.UpdateButtonsState();
  }

  public void RefreshLabelsText()
  {
    StringVariableParser.randomCircuit = this.mCircuit;
    this.trackGuide.text = Localisation.LocaliseID("PSG_10010409", (GameObject) null);
    StringVariableParser.intValue1 = DesignDataManager.CalculateRaceLapCount(Game.instance.player.team.championship, this.mCircuit.trackLengthMiles, false);
    StringVariableParser.floatValue1 = this.mCircuit.trackLengthMiles;
    this.laps.text = Localisation.LocaliseID("PSG_10010243", (GameObject) null);
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
    for (int index = 0; index < 6; ++index)
    {
      CarStats.StatType statType = (CarStats.StatType) index;
      if (CarPart.GetPartForStatType(statType, Game.instance.player.team.championship.series) != CarPart.PartType.None && CarStats.GetRelevancy(Mathf.RoundToInt(this.mCircuit.trackStatsCharacteristics.GetStat(statType))) == inRelevancy)
      {
        if (count < this.circuitStats.Length)
          this.circuitStats[count].SetupCircuitStat(this.mCircuit, statType);
        count = count + 1;
      }
    }
  }

  private void OnLeftButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mEvent = this.mEvent - 1 >= this.mMinEvent ? this.mEvent - 1 : this.mMaxEvent;
    this.mRaceEvent = Game.instance.player.team.championship.calendar[this.mEvent];
    this.mCircuit = this.mRaceEvent.circuit;
    this.widget.SetEvent(this.mEvent);
    this.Refresh();
  }

  private void OnRightButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mEvent = this.mEvent + 1 <= this.mMaxEvent ? this.mEvent + 1 : this.mMinEvent;
    this.mRaceEvent = Game.instance.player.team.championship.calendar[this.mEvent];
    this.mCircuit = this.mRaceEvent.circuit;
    this.widget.SetEvent(this.mEvent);
    this.Refresh();
  }

  private void UpdateButtonsState()
  {
    this.leftButton.interactable = this.mEvent - 1 >= this.mMinEvent;
    this.rightButton.interactable = this.mEvent + 1 <= this.mMaxEvent;
  }
}
