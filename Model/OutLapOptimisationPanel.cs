// Decompiled with JetBrains decompiler
// Type: OutLapOptimisationPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OutLapOptimisationPanel : MonoBehaviour
{
  public Color statusBackingColor = Color.white;
  private TemperatureOptimisation.Status mCachedTyreStatus = TemperatureOptimisation.Status.Perfect;
  private TemperatureOptimisation.Status mCachedBrakeStatus = TemperatureOptimisation.Status.Perfect;
  private float minPinRotation = -48f;
  private float maxPinRotation = 48f;
  public Image outLapDistanceSlider;
  public Gradient temperatureGradient;
  public Toggle autoManageToggle;
  public CanvasGroup toggleCanvas;
  public GameObject singleSeaterCar;
  public GameObject gtCar;
  public UICarSetupTyreIcon tyreIcon;
  public RectTransform tyreTemperaturePin;
  public GameObject[] tyreSweetSpots;
  public GameObject tyreStatus;
  public TextMeshProUGUI tyreTemperatureStatusLabel;
  public Image tyreTemperatureStatusBacking;
  public Image gtTyreRepresentation;
  public Image singleSeaterTyreRepresentation;
  public RectTransform brakeTemperaturePin;
  public GameObject[] brakeSweetSpots;
  public GameObject brakeStatus;
  public TextMeshProUGUI brakeTemperatureStatusLabel;
  public Image brakeTemperatureStatusBacking;
  public Image singleSeaterBrakeRepresentation;
  public Image gtBrakeRepresentation;
  public GameObject sliderContainer;
  public Slider speedSlider;
  public Image speedSliderFill;
  public GameObject[] drivingStyleIcons;
  public Color[] drivingStyleColors;
  public Color[] statusColors;
  private RacingVehicle mVehicle;
  private TemperatureOptimisation mTemperatureOptimisation;

  public void Awake()
  {
  }

  public void SetVehicle(RacingVehicle inVehicle)
  {
    this.mVehicle = inVehicle;
    this.mTemperatureOptimisation = this.mVehicle.performance.temperatureOptimisation;
    GameUtility.SetActive(this.singleSeaterCar, Game.instance.sessionManager.championship.series == Championship.Series.SingleSeaterSeries);
    GameUtility.SetActive(this.gtCar, Game.instance.sessionManager.championship.series == Championship.Series.GTSeries);
    this.autoManageToggle.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnAutoManageChanged(value)));
    this.autoManageToggle.isOn = this.mTemperatureOptimisation.autoManage;
  }

  private void OnDisable()
  {
    this.sliderContainer.SetActive(false);
  }

  private void OnEnable()
  {
    if (this.mTemperatureOptimisation == null)
      return;
    this.sliderContainer.SetActive(true);
    if (this.mTemperatureOptimisation != null)
    {
      GameUtility.SetSliderAmountIfDifferent(this.speedSlider, Mathf.Clamp01(this.mTemperatureOptimisation.speed01), 1000f);
      this.SetStatusDisplay(this.mTemperatureOptimisation.tyreStatus, this.mTemperatureOptimisation.tyreTemperature, this.tyreStatus, this.tyreTemperatureStatusLabel, this.tyreTemperatureStatusBacking, false, true);
      this.SetStatusDisplay(this.mTemperatureOptimisation.brakeStatus, this.mTemperatureOptimisation.brakeTemperature, this.brakeStatus, this.brakeTemperatureStatusLabel, this.brakeTemperatureStatusBacking, false, true);
    }
    if (this.mVehicle != null)
    {
      this.tyreIcon.SetTyre(this.mVehicle.setup.tyreSet);
      int num = 0;
      if (this.mVehicle.bonuses.IsBonusActive(MechanicBonus.Trait.SweeterSpots))
        num = 1;
      else if (this.mVehicle.bonuses.IsBonusActive(MechanicBonus.Trait.TheSweetestSpots))
        num = 2;
      for (int index = 0; index < this.tyreSweetSpots.Length; ++index)
      {
        GameUtility.SetActive(this.tyreSweetSpots[index], index == num);
        GameUtility.SetActive(this.brakeSweetSpots[index], index == num);
      }
    }
    this.Update();
  }

  private void Update()
  {
    if (this.mVehicle == null)
      return;
    if (this.mTemperatureOptimisation.autoManage)
      this.speedSlider.value = this.mTemperatureOptimisation.speed01;
    this.UpdateTyreTemperature();
    this.UpdateBrakeTemperature();
    this.UpdateSpeed();
    GameUtility.SetImageFillAmountIfDifferent(this.outLapDistanceSlider, this.mTemperatureOptimisation.GetOutLapRemaining01(), 1f / 512f);
    GameUtility.SetInteractable((Selectable) this.speedSlider, this.mTemperatureOptimisation.mode != TemperatureOptimisation.Mode.Complete && !this.mVehicle.strategy.IsGoingToPit() && !this.mTemperatureOptimisation.autoManage);
    GameUtility.SetAlpha(this.toggleCanvas, !this.speedSlider.interactable ? 0.3f : 1f);
  }

  private void UpdateTyreTemperature()
  {
    bool flag = this.mTemperatureOptimisation.IsInSweetSpot(this.mTemperatureOptimisation.tyreTemperature, this.mTemperatureOptimisation.tyreSweetSpot);
    this.tyreTemperaturePin.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(this.minPinRotation, this.maxPinRotation, this.mTemperatureOptimisation.tyreTemperature));
    if (Game.instance.sessionManager.championship.series == Championship.Series.GTSeries)
      this.gtTyreRepresentation.color = this.CalculateTemperatureColor(this.mTemperatureOptimisation.tyreTemperature, this.mTemperatureOptimisation.tyreSweetSpot);
    else
      this.singleSeaterTyreRepresentation.color = this.CalculateTemperatureColor(this.mTemperatureOptimisation.tyreTemperature, this.mTemperatureOptimisation.tyreSweetSpot);
    bool inIsFinal = this.mTemperatureOptimisation.mode == TemperatureOptimisation.Mode.Complete;
    bool inRefreshText = this.mCachedTyreStatus != this.mTemperatureOptimisation.tyreStatus;
    this.mCachedTyreStatus = this.mTemperatureOptimisation.tyreStatus;
    this.SetStatusDisplay(this.mTemperatureOptimisation.tyreStatus, this.mTemperatureOptimisation.tyreTemperature, this.tyreStatus, this.tyreTemperatureStatusLabel, this.tyreTemperatureStatusBacking, inIsFinal, inRefreshText);
    if (inIsFinal)
      return;
    GameUtility.SetActive(this.tyreStatus, !flag);
  }

  private void UpdateBrakeTemperature()
  {
    bool flag = this.mTemperatureOptimisation.IsInSweetSpot(this.mTemperatureOptimisation.brakeTemperature, this.mTemperatureOptimisation.brakeSweetSpot);
    this.brakeTemperaturePin.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(this.minPinRotation, this.maxPinRotation, this.mTemperatureOptimisation.brakeTemperature));
    if (Game.instance.sessionManager.championship.series == Championship.Series.GTSeries)
      this.gtBrakeRepresentation.color = this.CalculateTemperatureColor(this.mTemperatureOptimisation.brakeTemperature, this.mTemperatureOptimisation.brakeSweetSpot);
    else
      this.singleSeaterBrakeRepresentation.color = this.CalculateTemperatureColor(this.mTemperatureOptimisation.brakeTemperature, this.mTemperatureOptimisation.brakeSweetSpot);
    bool inIsFinal = this.mTemperatureOptimisation.mode == TemperatureOptimisation.Mode.Complete;
    bool inRefreshText = this.mCachedBrakeStatus != this.mTemperatureOptimisation.brakeStatus;
    this.mCachedBrakeStatus = this.mTemperatureOptimisation.brakeStatus;
    this.SetStatusDisplay(this.mTemperatureOptimisation.brakeStatus, this.mTemperatureOptimisation.brakeTemperature, this.brakeStatus, this.brakeTemperatureStatusLabel, this.brakeTemperatureStatusBacking, inIsFinal, inRefreshText);
    if (inIsFinal)
      return;
    GameUtility.SetActive(this.brakeStatus, !flag);
  }

  private void UpdateSpeed()
  {
    if (this.mVehicle.strategy.IsGoingToPit())
    {
      int index1 = 4;
      this.mVehicle.performance.drivingStyle.SetDrivingStyle(DrivingStyle.Mode.BackUp);
      this.mTemperatureOptimisation.SetSpeed01(0.0f);
      this.speedSlider.value = 0.0f;
      this.speedSliderFill.color = this.drivingStyleColors[index1];
      for (int index2 = 0; index2 < this.drivingStyleIcons.Length; ++index2)
        this.drivingStyleIcons[index2].SetActive(index2 == index1);
    }
    else
    {
      int index1 = Mathf.Clamp(Mathf.RoundToInt((float) (5.0 - (double) this.speedSlider.value / 0.200000002980232)), 0, this.drivingStyleIcons.Length - 1);
      DrivingStyle.Mode inDrivingStyle = (DrivingStyle.Mode) index1;
      this.speedSliderFill.color = this.drivingStyleColors[index1];
      for (int index2 = 0; index2 < this.drivingStyleIcons.Length; ++index2)
        this.drivingStyleIcons[index2].SetActive(index2 == index1);
      if (this.mTemperatureOptimisation.mode != TemperatureOptimisation.Mode.Complete)
        this.mVehicle.performance.drivingStyle.SetDrivingStyle(inDrivingStyle);
      this.mTemperatureOptimisation.SetSpeed01(this.speedSlider.value);
    }
  }

  private void OnAutoManageChanged(bool inAutoManage)
  {
    if (this.mTemperatureOptimisation.autoManage == inAutoManage)
      return;
    this.mTemperatureOptimisation.SetSpeedAutoManage(inAutoManage);
    if (!Game.instance.sessionManager.isSessionActive)
      return;
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  private Color CalculateTemperatureColor(float inValue, TemperatureOptimisation.SweetSpot inSweetSpot)
  {
    Color white = Color.white;
    float num = this.mTemperatureOptimisation.GetMinSweetSpotDelta(inSweetSpot) * 0.5f;
    return (double) Mathf.Abs(inValue - 0.5f) >= (double) num ? this.temperatureGradient.Evaluate(inValue) : this.temperatureGradient.Evaluate(0.5f);
  }

  private void SetStatusDisplay(TemperatureOptimisation.Status inStatus, float inValue, GameObject inStatusObject, TextMeshProUGUI inStatusLabel, Image inStatusBacking, bool inIsFinal, bool inRefreshText)
  {
    string inID = "PSG_10007964";
    if (inIsFinal)
    {
      GameUtility.SetActive(inStatusObject, true);
      inStatusLabel.color = this.statusBackingColor;
      switch (inStatus)
      {
        case TemperatureOptimisation.Status.Overheated:
          inID = "PSG_10010803";
          inStatusBacking.color = this.statusColors[0];
          break;
        case TemperatureOptimisation.Status.Cold:
          inID = "PSG_10011039";
          inStatusBacking.color = this.statusColors[1];
          break;
        case TemperatureOptimisation.Status.Good:
          inID = "PSG_10007964";
          inStatusBacking.color = this.statusColors[2];
          break;
        case TemperatureOptimisation.Status.Great:
          inID = "PSG_10010180";
          inStatusBacking.color = this.statusColors[3];
          break;
        case TemperatureOptimisation.Status.Perfect:
          inID = "PSG_10010804";
          inStatusBacking.color = this.statusColors[4];
          break;
      }
    }
    else
    {
      inStatusBacking.color = this.statusBackingColor;
      switch (inStatus)
      {
        case TemperatureOptimisation.Status.Overheated:
          inID = "PSG_10010803";
          inStatusLabel.color = this.statusColors[0];
          break;
        case TemperatureOptimisation.Status.Cold:
          inID = "PSG_10011039";
          inStatusLabel.color = this.statusColors[1];
          break;
        case TemperatureOptimisation.Status.Good:
          inID = "PSG_10007964";
          inStatusLabel.color = this.statusColors[2];
          break;
        case TemperatureOptimisation.Status.Great:
          inID = "PSG_10010180";
          inStatusLabel.color = this.statusColors[3];
          break;
        case TemperatureOptimisation.Status.Perfect:
          inID = "PSG_10010804";
          inStatusLabel.color = this.statusColors[4];
          break;
      }
    }
    if (!inRefreshText)
      return;
    inStatusLabel.text = Localisation.LocaliseID(inID, (GameObject) null);
  }
}
