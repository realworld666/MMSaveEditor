// Decompiled with JetBrains decompiler
// Type: DilemmaWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DilemmaWidget : MonoBehaviour
{
  public Button actionButton;
  public Button ignoreButton;
  public UICharacterPortrait portrait;
  public Flag flag;
  public TextMeshProUGUI driverNameLabel;
  public TextMeshProUGUI positionLabel;
  public TextMeshProUGUI lapsLabel;
  public TextMeshProUGUI dilemmaLabel;
  public GameObject tyresWidget;
  public TextMeshProUGUI tyreCompoundLabel;
  public TextMeshProUGUI tyrePercentageLabel;
  public UITyreWearIcon tyreWear;
  public GameObject fuelWidget;
  public Slider fuelSlider;
  public TextMeshProUGUI fuelLapsLabel;
  public UIGridList carConditionGrid;
  private RadioMessage mRadioMessage;
  private RacingVehicle mVehicle;
  private scSoundContainer mSoundRadioMessage;

  private void Start()
  {
    this.actionButton.onClick.AddListener(new UnityAction(this.OnActionButton));
    this.ignoreButton.onClick.AddListener(new UnityAction(this.OnIgnoreButton));
  }

  private void Update()
  {
    if (Game.instance.time.isPaused)
      return;
    this.OnExit();
  }

  public void OnEnter(RadioMessage inRadioMessageBase)
  {
    GameUtility.SetActive(this.gameObject, true);
    Game.instance.time.Pause(GameTimer.PauseType.Dilemma);
    this.SetupWithRadioMessage(inRadioMessageBase);
    scSoundManager.CheckPlaySound(SoundID.Sfx_RadioMessage, ref this.mSoundRadioMessage, 0.0f);
  }

  private void OnExit()
  {
    GameUtility.SetActive(this.gameObject, false);
    scSoundManager.CheckStopSound(ref this.mSoundRadioMessage);
    Game.instance.time.UnPause(GameTimer.PauseType.Dilemma);
  }

  private void SetupWithRadioMessage(RadioMessage inRadioMessage)
  {
    Driver personWhoSpeaks = inRadioMessage.personWhoSpeaks as Driver;
    if (personWhoSpeaks != null)
    {
      this.mVehicle = Game.instance.vehicleManager.GetVehicle(personWhoSpeaks);
      this.SetupDriver();
    }
    this.mRadioMessage = inRadioMessage;
    this.dilemmaLabel.text = this.mRadioMessage.text.GetText();
    GameUtility.SetActive(this.tyresWidget, false);
    GameUtility.SetActive(this.fuelWidget, false);
    GameUtility.SetActive(this.carConditionGrid.gameObject, false);
    switch (inRadioMessage.dilemmaType)
    {
      case RadioMessage.DilemmaType.Fuel:
        GameUtility.SetSliderAmountIfDifferent(this.fuelSlider, this.mVehicle.performance.fuel.GetNormalisedFuelLevel(), 1000f);
        int fuelLapsRemaining = this.mVehicle.performance.fuel.GetFuelLapsRemaining();
        StringVariableParser.intValue1 = fuelLapsRemaining;
        this.fuelLapsLabel.text = fuelLapsRemaining != 1 ? Localisation.LocaliseID("PSG_10010646", (GameObject) null) : Localisation.LocaliseID("PSG_10010645", (GameObject) null);
        GameUtility.SetActive(this.fuelWidget, true);
        break;
      case RadioMessage.DilemmaType.Tyres:
        TyreSet tyreSet = this.mVehicle.setup.tyreSet;
        this.tyreCompoundLabel.text = tyreSet.GetName();
        this.tyrePercentageLabel.text = tyreSet.GetConditionText();
        this.tyreWear.SetTyreSet(tyreSet, this.mVehicle.bonuses.bonusDisplayInfo);
        GameUtility.SetActive(this.tyresWidget, true);
        break;
      case RadioMessage.DilemmaType.CarPartCondition:
        GameUtility.SetActive(this.carConditionGrid.gameObject, true);
        this.carConditionGrid.DestroyListItems();
        Car car = this.mVehicle.car;
        this.carConditionGrid.itemPrefab.SetActive(true);
        foreach (CarPart.PartType inType in CarPart.GetPartType(car.carManager.team.championship.series, false))
        {
          CarPart part = car.GetPart(inType);
          if ((double) part.partCondition.condition <= (double) part.partCondition.redZone)
            this.carConditionGrid.CreateListItem<CarConditionEntry>().SetPart(part);
        }
        this.carConditionGrid.itemPrefab.SetActive(false);
        break;
    }
  }

  private void SetupDriver()
  {
    if (this.mVehicle == null)
      return;
    this.portrait.SetPortrait((Person) this.mVehicle.driver);
    this.flag.SetNationality(this.mVehicle.driver.nationality);
    this.driverNameLabel.text = this.mVehicle.driver.name;
    this.positionLabel.text = GameUtility.FormatForPosition(this.mVehicle.standingsPosition, (string) null);
    if (Game.instance.sessionManager.endCondition == SessionManager.EndCondition.LapCount)
    {
      int lapCount = Game.instance.sessionManager.lapCount;
      StringVariableParser.intValue1 = Mathf.Min(Game.instance.sessionManager.GetLeader().timer.lap + 1, lapCount);
      StringVariableParser.intValue2 = lapCount;
      this.lapsLabel.text = Localisation.LocaliseID("PSG_10010651", (GameObject) null);
    }
    else
    {
      if (Game.instance.sessionManager.endCondition != SessionManager.EndCondition.Time)
        return;
      this.lapsLabel.text = GameUtility.GetSessionTimeText(Mathf.Round(Game.instance.sessionManager.time));
    }
  }

  private void OnActionButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.OnExit();
    this.mRadioMessage.DoDilemmaAction();
  }

  private void OnIgnoreButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.OnExit();
  }
}
