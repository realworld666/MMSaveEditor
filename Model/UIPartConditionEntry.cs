// Decompiled with JetBrains decompiler
// Type: UIPartConditionEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPartConditionEntry : MonoBehaviour
{
  public Button removeButton;
  public Slider conditionDangerZone;
  public Slider conditionValueSlider;
  public Image conditionBacking;
  public Transform iconParent;
  public GameObject fixButton;
  public GameObject maxLabel;
  public TextMeshProUGUI partNameLabel;
  public TextMeshProUGUI conditionLabel;
  public TextMeshProUGUI timeToFixLabel;
  public Toggle fixPartToggle;
  public GameObject criticalStatusContainer;
  public Image carPartGFX;
  private CarPart mPart;
  private RacingVehicle mVehicle;

  public void Start()
  {
    this.fixPartToggle.onValueChanged.AddListener((UnityAction<bool>) (value => this.SetPartRepairStatus(value)));
  }

  private void SetPartRepairStatus(bool inRepairPart)
  {
    if (!inRepairPart)
    {
      scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    }
    else
    {
      scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
      if (!this.mPart.partCondition.CanRepairInPit())
        Debug.LogError((object) "Managed to toggle a max part for repair!", (UnityEngine.Object) null);
    }
    this.mPart.partCondition.SetRepairInPit(inRepairPart);
    this.mVehicle.setup.SetRepair();
    this.UpdateTimeToFixLabel();
    this.UpdateConditionStats();
  }

  public void SetPart(CarPart inPart, RacingVehicle inVehicle)
  {
    if (inPart == null)
    {
      GameUtility.SetActive(this.gameObject, false);
    }
    else
    {
      GameUtility.SetActive(this.gameObject, true);
      this.mVehicle = inVehicle;
      this.mPart = inPart;
      this.partNameLabel.text = this.mPart.GetPartName();
      this.removeButton.onClick.Invoke();
      this.UpdateConditionStats();
      for (int index = 0; index < 11; ++index)
      {
        CarPart.PartType partType = (CarPart.PartType) index;
        GameUtility.SetActive(this.iconParent.GetChild(index).gameObject, this.mPart.GetPartType() == partType);
      }
      if (this.mPart.partCondition.IsOnRed())
        this.carPartGFX.color = UIConstants.conditionCarPartRed;
      else if ((double) this.mPart.partCondition.normalizedCondition < 0.5)
        this.carPartGFX.color = UIConstants.conditionCarPartYellow;
      else
        this.carPartGFX.color = UIConstants.whiteColor;
      if (this.mPart.partCondition.CanRepairInPit())
      {
        GameUtility.SetActive(this.fixButton, true);
        GameUtility.SetActive(this.maxLabel, false);
      }
      else
      {
        GameUtility.SetActive(this.fixButton, false);
        GameUtility.SetActive(this.maxLabel, true);
        this.fixPartToggle.interactable = this.mPart.partCondition.CanRepairInPit();
        this.fixPartToggle.isOn = false;
      }
    }
  }

  private void OnEnable()
  {
    this.UpdateConditionStats();
  }

  private void UpdateConditionStats()
  {
    if (this.mPart == null)
      return;
    float num;
    if (this.mPart.partCondition.repairInPit)
    {
      num = this.mPart.partCondition.GetConditionAfterPit();
      this.conditionLabel.color = UIConstants.financeBackingPositiveColor;
      this.criticalStatusContainer.SetActive(false);
    }
    else
    {
      num = this.mPart.partCondition.condition;
      this.conditionLabel.color = UIConstants.whiteColor;
      this.criticalStatusContainer.SetActive(this.mPart.partCondition.IsOnRed());
    }
    this.conditionDangerZone.normalizedValue = this.mPart.partCondition.redZone;
    GameUtility.SetSliderAmountIfDifferent(this.conditionValueSlider, num, 1000f);
    this.conditionValueSlider.fillRect.GetComponent<Image>().color = this.mPart.partCondition.GetConditionColor(num);
    this.conditionBacking.fillAmount = this.mPart.stats.reliability;
    this.conditionLabel.text = Mathf.RoundToInt(num * 100f).ToString() + "%";
    this.fixPartToggle.isOn = this.mPart.partCondition.repairInPit;
    this.UpdateTimeToFixLabel();
  }

  private void UpdateTimeToFixLabel()
  {
    if (this.fixPartToggle.isOn)
    {
      StringVariableParser.ordinalNumberString = this.mPart.partCondition.GetPitTimeToRepair(this.mVehicle).ToString("0.0", (IFormatProvider) Localisation.numberFormatter);
      this.timeToFixLabel.text = Localisation.LocaliseID("PSG_10010812", (GameObject) null);
    }
    else
      this.timeToFixLabel.text = "-";
    UIManager.instance.GetScreen<PitScreen>().optionsSelectionWidget.pitConditionWidget.UpdateTimeEstimate();
  }
}
