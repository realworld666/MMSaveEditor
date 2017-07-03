// Decompiled with JetBrains decompiler
// Type: TeamWipEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TeamWipEntry : MonoBehaviour
{
  private CarPartStats.CarPartStat mCarPartStat = CarPartStats.CarPartStat.Reliability;
  private TimeSpan mTimeRemaining = TimeSpan.Zero;
  public TeamWipEntry.Type type;
  public TextMeshProUGUI titleLabel;
  public Button cancelButton;
  public TextMeshProUGUI cancelButtonLabel;
  public Button detailsButton;
  public TextMeshProUGUI detailsButtonLabel;
  public GameObject flagParent;
  public Flag flag;
  public Slider progressBar;
  public TextMeshProUGUI timeLeftLabel;
  public HomeScreenWipWidget widget;
  private Driver mDriver;
  private HQsBuilding_v1 mBuilding;
  private CarPartDesign mCarPartDesign;
  private PartImprovement mPartImprovement;

  public TimeSpan timeRemaining
  {
    get
    {
      return this.mTimeRemaining;
    }
  }

  public void Setup(Driver inDriver)
  {
    this.mDriver = inDriver;
    this.Set();
  }

  public void Setup(HQsBuilding_v1 inBuilding)
  {
    this.mBuilding = inBuilding;
    this.Set();
  }

  public void Setup(CarPartStats.CarPartStat inStat)
  {
    this.mCarPartStat = inStat;
    this.Set();
  }

  public void Set()
  {
    this.detailsButton.onClick.RemoveAllListeners();
    this.detailsButton.onClick.AddListener(new UnityAction(this.OnDetailsButton));
    this.cancelButton.onClick.RemoveAllListeners();
    this.cancelButton.onClick.AddListener(new UnityAction(this.OnCancelButton));
    this.SetDetails();
    this.UpdateProgress();
  }

  private void Update()
  {
    this.UpdateProgress();
    this.UpdateInteractability();
  }

  private void UpdateInteractability()
  {
    if (this.type == TeamWipEntry.Type.PartImprovements)
      GameUtility.SetInteractable(this.detailsButton, !Game.instance.player.team.carManager.partImprovement.FixingCondition());
    else
      GameUtility.SetInteractable(this.detailsButton, true);
  }

  public void SetDetails()
  {
    switch (this.type)
    {
      case TeamWipEntry.Type.Scouting:
        this.titleLabel.text = this.mDriver.name;
        this.flag.SetNationality(this.mDriver.nationality);
        this.detailsButtonLabel.text = Localisation.LocaliseID("PSG_10002307", (GameObject) null);
        break;
      case TeamWipEntry.Type.HQBuilding:
        this.titleLabel.text = this.mBuilding.GetBuildingStateString();
        this.detailsButtonLabel.text = Localisation.LocaliseID("PSG_10002250", (GameObject) null);
        break;
      case TeamWipEntry.Type.PartBuilding:
        this.mCarPartDesign = Game.instance.player.team.carManager.carPartDesign;
        this.titleLabel.text = Localisation.LocaliseEnum((Enum) this.mCarPartDesign.part.GetPartType());
        this.detailsButtonLabel.text = Localisation.LocaliseID("PSG_10004593", (GameObject) null);
        break;
      case TeamWipEntry.Type.PartImprovements:
        this.mPartImprovement = Game.instance.player.team.carManager.partImprovement;
        StringVariableParser.partStat = this.mCarPartStat;
        this.titleLabel.text = Localisation.LocaliseID("PSG_10010939", (GameObject) null);
        this.detailsButtonLabel.text = Localisation.LocaliseID("PSG_10002283", (GameObject) null);
        break;
    }
    GameUtility.SetActive(this.cancelButton.gameObject, this.type == TeamWipEntry.Type.Scouting);
    GameUtility.SetActive(this.flagParent, this.type == TeamWipEntry.Type.Scouting);
  }

  public void UpdateProgress()
  {
    if (!this.isActive())
    {
      GameUtility.SetActive(this.gameObject, false);
      this.widget.UpdateHeader(this.type);
      this.widget.UpdateEntries(this.type);
    }
    else
    {
      switch (this.type)
      {
        case TeamWipEntry.Type.Scouting:
          TimeSpan forScoutingDriver = this.widget.scoutingManager.GetTimeLeftForScoutingDriver(this.mDriver);
          if (!(forScoutingDriver != this.mTimeRemaining))
            break;
          this.mTimeRemaining = forScoutingDriver;
          this.progressBar.value = this.widget.scoutingManager.GetTimeLeftForScoutingDriverNormalized(this.mDriver);
          this.timeLeftLabel.text = GameUtility.FormatTimeSpanDays(this.mTimeRemaining);
          break;
        case TeamWipEntry.Type.HQBuilding:
          TimeSpan timeRemaining = this.mBuilding.timeRemaining;
          if (!(timeRemaining != this.mTimeRemaining))
            break;
          this.mTimeRemaining = timeRemaining;
          this.progressBar.value = this.mBuilding.normalizedProgressUI;
          this.timeLeftLabel.text = GameUtility.FormatTimeSpanWeeks(this.mTimeRemaining);
          break;
        case TeamWipEntry.Type.PartBuilding:
          TimeSpan remainingTime = this.mCarPartDesign.remainingTime;
          if (!(remainingTime != this.mTimeRemaining))
            break;
          this.mTimeRemaining = remainingTime;
          this.progressBar.value = this.mCarPartDesign.GetCreationTimeElapsedNormalised();
          this.timeLeftLabel.text = GameUtility.FormatTimeSpanDays(this.mTimeRemaining);
          break;
        case TeamWipEntry.Type.PartImprovements:
          TimeSpan timeToFinishWork = this.mPartImprovement.GetTimeToFinishWork(this.mCarPartStat);
          if (!(timeToFinishWork != this.mTimeRemaining))
            break;
          this.mTimeRemaining = timeToFinishWork;
          this.progressBar.value = this.mPartImprovement.GetNormalizedTimeToFinishWork(this.mCarPartStat);
          this.timeLeftLabel.text = GameUtility.FormatTimeSpanDays(this.mTimeRemaining);
          break;
      }
    }
  }

  private void OnDetailsButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    switch (this.type)
    {
      case TeamWipEntry.Type.Scouting:
        UIManager.instance.ChangeScreen("DriverScreen", (Entity) this.mDriver, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
        break;
      case TeamWipEntry.Type.HQBuilding:
        UIManager.instance.ChangeScreen("HeadquartersScreen", (Entity) this.mBuilding, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
        break;
      case TeamWipEntry.Type.PartBuilding:
        UIManager.instance.ChangeScreen("CarScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
        break;
      case TeamWipEntry.Type.PartImprovements:
        UIManager.instance.ChangeScreen("FactoryPartDevelopmentScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
        break;
    }
  }

  private void OnCancelButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.type == TeamWipEntry.Type.Scouting)
      Game.instance.scoutingManager.RemoveDriverFromScoutingJobs(this.mDriver);
    this.widget.UpdateHeader(this.type);
    this.widget.UpdateEntries(this.type);
  }

  private bool isActive()
  {
    switch (this.type)
    {
      case TeamWipEntry.Type.Scouting:
        return this.widget.scoutingManager.IsDriverCurrentlyScouted(this.mDriver);
      case TeamWipEntry.Type.HQBuilding:
        return (double) this.mBuilding.normalizedProgressUI < 1.0;
      case TeamWipEntry.Type.PartBuilding:
        return this.mCarPartDesign.stage == CarPartDesign.Stage.Designing;
      case TeamWipEntry.Type.PartImprovements:
        return this.mPartImprovement.WorkOnStatActive(this.mCarPartStat);
      default:
        return false;
    }
  }

  public enum Type
  {
    Scouting,
    HQBuilding,
    PartBuilding,
    PartImprovements,
  }
}
