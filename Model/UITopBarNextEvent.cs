// Decompiled with JetBrains decompiler
// Type: UITopBarNextEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITopBarNextEvent : MonoBehaviour
{
  private int mDaysToRace = int.MaxValue;
  private int mCircuitID = int.MaxValue;
  public Button button;
  public GameObject panel;
  public GameObject dropdownContainer;
  public GameObject[] dropdownMouseContainers;
  public Flag flag;
  public Image flagImage;
  public TextMeshProUGUI eventDate;
  public TextMeshProUGUI location;
  public UIWeatherIcon weather;
  public UITopBarTrackDetailsWidget detailsWidget;
  private RaceEventDetails mNextRaceDetails;
  private RaceEventDetails mCurrentDetails;
  private Championship mChampionship;

  private void Awake()
  {
    if (!((UnityEngine.Object) this.button != (UnityEngine.Object) null))
      return;
    this.button.onClick.AddListener(new UnityAction(this.OnButtonPressed));
  }

  public void SetListener()
  {
    UIManager.OnScreenChange += new Action(this.UpdateVisibility);
  }

  public void OnDestroy()
  {
    UIManager.OnScreenChange -= new Action(this.UpdateVisibility);
  }

  public void OnUnload()
  {
    UIManager.OnScreenChange -= new Action(this.UpdateVisibility);
  }

  private void UpdateVisibility()
  {
    if (!Game.IsActive() || !Game.instance.isCareer || Game.instance.player.IsUnemployed())
      return;
    GameUtility.SetActive(this.gameObject, !Game.instance.player.team.championship.GetFinalEventDetails().hasEventEnded && !(App.instance.gameStateManager.currentState is PreSeasonState));
  }

  public void OpenDropDown()
  {
    this.detailsWidget.RefreshLabelsText();
    GameUtility.SetActive(this.dropdownContainer, true);
  }

  public void Update()
  {
    if ((UnityEngine.Object) this.dropdownContainer != (UnityEngine.Object) null)
      GameUtility.DisableIfMouseExit(this.dropdownContainer, this.dropdownMouseContainers);
    bool inIsActive = true;
    if (Game.IsActive() && Game.instance.isCareer && !Game.instance.player.IsUnemployed())
    {
      if ((UnityEngine.Object) this.detailsWidget != (UnityEngine.Object) null && this.detailsWidget.gameObject.activeSelf)
        return;
      this.mChampionship = Game.instance.player.team.championship;
      this.mNextRaceDetails = this.mChampionship.GetCurrentEventDetails();
      int days = (this.mNextRaceDetails.eventDate - Game.instance.time.now).Days;
      if (days != this.mDaysToRace)
      {
        this.mDaysToRace = days;
        if (days <= 0)
        {
          this.eventDate.text = Localisation.LocaliseID("PSG_10009316", (GameObject) null);
        }
        else
        {
          StringVariableParser.intValue1 = days;
          this.eventDate.text = days != 1 ? Localisation.LocaliseID("PSG_10010420", (GameObject) null) : Localisation.LocaliseID("PSG_10010421", (GameObject) null);
        }
      }
      if (this.mCurrentDetails != this.mNextRaceDetails || this.mCurrentDetails.hasEventEnded)
      {
        this.mCurrentDetails = this.mNextRaceDetails;
        if (!this.mCurrentDetails.hasEventEnded)
        {
          this.flag.SetNationality(this.mCurrentDetails.circuit.nationality);
          if (this.mCircuitID != this.mCurrentDetails.circuit.circuitID)
          {
            this.mCircuitID = this.mCurrentDetails.circuit.circuitID;
            StringVariableParser.randomCircuit = this.mCurrentDetails.circuit;
            this.location.text = Localisation.LocaliseID("PSG_10010221", (GameObject) null);
          }
          if ((UnityEngine.Object) this.weather != (UnityEngine.Object) null)
          {
            GameUtility.SetActive(this.weather.gameObject, Game.instance.player.CanSeeEventWeatherForecast(this.mChampionship, this.mChampionship.eventNumber));
            if (this.weather.gameObject.activeSelf)
              this.weather.SetIcon(this.mCurrentDetails.raceSessions[0].sessionWeather.forecastWeather);
          }
          if ((UnityEngine.Object) this.detailsWidget != (UnityEngine.Object) null)
            this.detailsWidget.SetData(this.mCurrentDetails.circuit);
        }
        else
          inIsActive = false;
      }
    }
    else
      inIsActive = false;
    if (!((UnityEngine.Object) this.panel != (UnityEngine.Object) null))
      return;
    GameUtility.SetActive(this.panel, inIsActive);
  }

  public void SetEvent(int inEventNumber)
  {
    this.mCurrentDetails = Game.instance.player.team.championship.calendar[inEventNumber];
    this.mCircuitID = this.mCurrentDetails.circuit.circuitID;
    this.flag.SetNationality(this.mCurrentDetails.circuit.nationality);
    bool flag = inEventNumber == Game.instance.player.team.championship.eventNumber;
    int days = (this.mCurrentDetails.eventDate - Game.instance.time.now).Days;
    this.mDaysToRace = days;
    if (days <= 0)
    {
      this.eventDate.text = Localisation.LocaliseID("PSG_10009316", (GameObject) null);
    }
    else
    {
      StringVariableParser.intValue1 = days;
      if (flag)
        this.eventDate.text = days != 1 ? Localisation.LocaliseID("PSG_10010420", (GameObject) null) : Localisation.LocaliseID("PSG_10010421", (GameObject) null);
      else
        this.eventDate.text = days != 1 ? Localisation.LocaliseID("PSG_10010422", (GameObject) null) : Localisation.LocaliseID("PSG_10010423", (GameObject) null);
    }
    StringVariableParser.randomCircuit = this.mCurrentDetails.circuit;
    this.location.text = Localisation.LocaliseID("PSG_10010221", (GameObject) null);
    if (!((UnityEngine.Object) this.weather != (UnityEngine.Object) null))
      return;
    GameUtility.SetActive(this.weather.gameObject, Game.instance.player.CanSeeEventWeatherForecast(this.mChampionship, inEventNumber));
    if (!this.weather.gameObject.activeSelf)
      return;
    this.weather.SetIcon(this.mCurrentDetails.raceSessions[0].sessionWeather.forecastWeather);
  }

  public void OnButtonPressed()
  {
    if (UIManager.instance.IsScreenOpen("EventCalendarScreen") || App.instance.gameStateManager.currentState.group != GameState.Group.Frontend)
      return;
    UIManager.instance.ChangeScreen("EventCalendarScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    GameUtility.SetActive(this.dropdownContainer, false);
  }
}
