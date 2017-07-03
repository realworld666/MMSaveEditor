// Decompiled with JetBrains decompiler
// Type: UIComponentsDetailsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIComponentsDetailsWidget : MonoBehaviour
{
  public List<UIComponentSlotEntry> slotEntries = new List<UIComponentSlotEntry>();
  public List<UIComponentSlotEntry> bonusSlotEntries = new List<UIComponentSlotEntry>();
  public List<UIAdditionalBonusEntry> bonusesEntries = new List<UIAdditionalBonusEntry>();
  private AnimatedFloat[] animatedFloats = new AnimatedFloat[6];
  public TextMeshProUGUI rulesRiskAmount;
  public TextMeshProUGUI mainStatNameLabel;
  public Image mainStatFill;
  public Image mainMaxStatFill;
  public Slider currentPartStatSlider;
  public Image mainStatLegend;
  public Image perfomanceStatLegend;
  public TextMeshProUGUI mainStatLabel;
  public TextMeshProUGUI mainStatMaxLabel;
  public Slider reliabilitySlider;
  public Slider redZoneSlider;
  public Image maxReliabilityFill;
  public TextMeshProUGUI reliabilityLabel;
  public TextMeshProUGUI reliabilityMaxLabel;
  public TextMeshProUGUI buildTime;
  public TextMeshProUGUI buildTimeForEvent;
  public TextMeshProUGUI buildCost;
  public GameObject timeComponentAdjustmentContainer;
  public GameObject costComponentAdjustmentContainer;
  public TextMeshProUGUI timeComponentAdjustmentText;
  public TextMeshProUGUI costComponentAdjustmentText;
  public GameObject bonusesContainerData;
  public UICarPartEntry partEntry;
  public Animator costAnimator;
  public Animator timeAnimator;
  private PartDesignScreen mScreen;
  private CarPartDesign mDesign;

  private void Awake()
  {
  }

  private void OnCancelDesign()
  {
    this.mScreen.CancelDesign();
  }

  public void HighLightSlots(int inLevel)
  {
    for (int index = 0; index < this.slotEntries.Count; ++index)
      this.slotEntries[index].highLight.SetActive(index >= inLevel);
    if (this.mDesign.componentBonusSlotsLevel.Count <= 0)
      return;
    for (int index = 0; index < this.bonusSlotEntries.Count && index < this.mDesign.componentBonusSlotsLevel.Count; ++index)
      this.bonusSlotEntries[index].highLight.SetActive(this.mDesign.componentBonusSlotsLevel[index] >= inLevel);
    if (this.mDesign.componentBonusSlotsLevel.Count <= this.bonusSlotEntries.Count)
      return;
    Debug.LogError((object) "Not enought UI slots for all bonus components allocated to part", (UnityEngine.Object) null);
  }

  public void Setup()
  {
    this.mScreen = UIManager.instance.GetScreen<PartDesignScreen>();
    for (int index = 0; index < this.animatedFloats.Length; ++index)
      this.animatedFloats[index] = new AnimatedFloat();
    this.RefreshUI();
  }

  private void OnEnable()
  {
    if (!Game.IsActive())
      return;
    Game.instance.player.team.carManager.carPartDesign.OnDesignModified += new Action(this.RefreshUI);
  }

  private void OnDisable()
  {
    if (!Game.IsActive())
      return;
    Game.instance.player.team.carManager.carPartDesign.OnDesignModified -= new Action(this.RefreshUI);
  }

  private void RefreshUI()
  {
    this.mDesign = Game.instance.player.team.carManager.carPartDesign;
    for (int inLevel = 0; inLevel < this.mDesign.componentSlots.Count; ++inLevel)
    {
      CarPartComponent componentSlot = this.mDesign.componentSlots[inLevel];
      if (componentSlot != null)
        this.slotEntries[inLevel].Setup(UIComponentSlotEntry.State.Used, componentSlot, inLevel);
      else
        this.slotEntries[inLevel].Setup(UIComponentSlotEntry.State.Empty, (CarPartComponent) null, inLevel);
    }
    for (int count = this.mDesign.componentSlots.Count; count < this.slotEntries.Count; ++count)
      this.slotEntries[count].Setup(UIComponentSlotEntry.State.Locked, (CarPartComponent) null, count);
    for (int index = 0; index < this.mDesign.componentBonusSlots.Count; ++index)
    {
      if (index >= this.bonusSlotEntries.Count)
      {
        Debug.LogError((object) "Not enought UI slots for all bonus components allocated to part", (UnityEngine.Object) null);
        break;
      }
      CarPartComponent componentBonusSlot = this.mDesign.componentBonusSlots[index];
      if (componentBonusSlot != null)
        this.bonusSlotEntries[index].Setup(UIComponentSlotEntry.State.Used, componentBonusSlot, this.mDesign.componentBonusSlotsLevel[index] - 1);
      else
        this.bonusSlotEntries[index].Setup(UIComponentSlotEntry.State.Empty, (CarPartComponent) null, this.mDesign.componentBonusSlotsLevel[index] - 1);
      GameUtility.SetActive(this.bonusSlotEntries[index].gameObject, true);
    }
    for (int count = this.mDesign.componentBonusSlots.Count; count < this.bonusSlotEntries.Count; ++count)
      GameUtility.SetActive(this.bonusSlotEntries[count].gameObject, false);
    this.SetPartData(this.mDesign.part);
  }

  private void SetPartData(CarPart inPart)
  {
    Color partLevelColor = UIConstants.GetPartLevelColor(inPart.stats.level);
    this.mainStatFill.color = partLevelColor;
    this.mainStatLegend.color = partLevelColor;
    this.mainStatLabel.color = partLevelColor;
    partLevelColor.a = 0.8f;
    this.mainMaxStatFill.color = partLevelColor;
    this.perfomanceStatLegend.color = partLevelColor;
    this.mainStatMaxLabel.color = partLevelColor;
    StringVariableParser.stringValue1 = inPart.stats.rulesRiskString;
    this.rulesRiskAmount.text = Localisation.LocaliseID("PSG_10010428", (GameObject) null);
    this.SetAnimatedFloats(inPart);
    this.mainStatNameLabel.text = Localisation.LocaliseEnum((Enum) inPart.stats.statType);
    this.reliabilityLabel.text = ((double) Mathf.Round(inPart.stats.reliability * 100f)).ToString() + "%";
    this.reliabilityMaxLabel.text = "/" + (object) Mathf.Round(inPart.stats.maxReliability * 100f) + "%";
    this.mainStatLabel.text = inPart.stats.statWithPerformance.ToString("0", (IFormatProvider) Localisation.numberFormatter);
    this.mainStatMaxLabel.text = "/" + (inPart.stats.stat + Mathf.Max(0.0f, inPart.stats.maxPerformance)).ToString("0", (IFormatProvider) Localisation.numberFormatter);
    TimeSpan designDurationBonus = this.mDesign.GetComponentsDesignDurationBonus();
    this.timeComponentAdjustmentContainer.SetActive(designDurationBonus.TotalDays != 0.0);
    StringVariableParser.ordinalNumberString = designDurationBonus.TotalDays.ToString("N0");
    this.timeComponentAdjustmentText.text = Localisation.LocaliseID("PSG_10010377", (GameObject) null);
    TimeSpan designDuration = this.mDesign.GetDesignDuration();
    if (!Game.instance.player.team.championship.GetCurrentEventDetails().hasEventEnded)
      this.buildTimeForEvent.text = UIPartDevStatImprovementWidget.GetDateText(Game.instance.time.now + designDuration) + " " + UIPartDevStatImprovementWidget.GetEventText(Game.instance.time.now + designDuration);
    else
      this.buildTimeForEvent.text = string.Empty;
    StringVariableParser.ordinalNumberString = designDuration.TotalDays.ToString("0.#");
    string str = Localisation.LocaliseID("PSG_10010377", (GameObject) null);
    if (this.buildTime.text != str)
      this.timeAnimator.SetTrigger(AnimationHashes.Highlight);
    this.buildTime.text = str;
    this.costComponentAdjustmentContainer.SetActive(this.mDesign.GetComponentDesignCostBonus() != 0);
    this.costComponentAdjustmentText.text = GameUtility.GetCurrencyString((long) this.mDesign.GetComponentDesignCostBonus(), 0);
    if (this.buildCost.text != GameUtility.GetCurrencyString((long) this.mDesign.GetDesignCost(), 0))
      this.costAnimator.SetTrigger(AnimationHashes.Highlight);
    this.buildCost.text = GameUtility.GetCurrencyString((long) this.mDesign.GetDesignCost(), 0);
    bool flag = false;
    int inIndex = 0;
    for (int index = 0; index < inPart.components.Count; ++index)
    {
      CarPartComponent component = inPart.components[index];
      if (component != null && !component.IgnoreBonusForUI() && (component.HasActivationRequirement() || component.bonuses.Count != 0))
      {
        string name = component.GetName(inPart);
        flag = true;
        this.AddBonusText(name, inIndex, component);
        ++inIndex;
      }
    }
    this.bonusesContainerData.SetActive(flag);
    for (int index = inIndex; index < this.bonusesEntries.Count; ++index)
      this.bonusesEntries[index].gameObject.SetActive(false);
    this.partEntry.Setup(inPart);
  }

  private void SetAnimatedFloats(CarPart inPart)
  {
    CarPartInventory partInventory = Game.instance.player.team.carManager.partInventory;
    float inDuration = 0.5f;
    float inDelay = 0.0f;
    this.animatedFloats[0].SetValue(CarPartStats.GetNormalizedStatValue(inPart.stats.stat + inPart.stats.maxPerformance, inPart.GetPartType()), AnimatedFloat.Animation.Animate, inDelay, inDuration, EasingUtility.Easing.InOutSin);
    this.animatedFloats[1].SetValue(CarPartStats.GetNormalizedStatValue(inPart.stats.statWithPerformance, inPart.GetPartType()), AnimatedFloat.Animation.Animate, inDelay, inDuration, EasingUtility.Easing.InOutSin);
    this.animatedFloats[2].SetValue(CarPartStats.GetNormalizedStatValue(partInventory.GetHighestStatOfType(inPart.GetPartType(), CarPartStats.CarPartStat.MainStat), inPart.GetPartType()), AnimatedFloat.Animation.Animate, inDelay, inDuration, EasingUtility.Easing.InOutSin);
    this.animatedFloats[3].SetValue(inPart.stats.reliability, AnimatedFloat.Animation.Animate, inDelay, inDuration, EasingUtility.Easing.InOutSin);
    this.animatedFloats[4].SetValue(inPart.stats.partCondition.redZone, AnimatedFloat.Animation.Animate, inDelay, inDuration, EasingUtility.Easing.InOutSin);
    this.animatedFloats[5].SetValue(inPart.stats.maxReliability, AnimatedFloat.Animation.Animate, inDelay, inDuration, EasingUtility.Easing.InOutSin);
  }

  private void Update()
  {
    for (int index = 0; index < this.animatedFloats.Length; ++index)
    {
      if (this.animatedFloats[index] == null)
        return;
      this.animatedFloats[index].Update();
    }
    GameUtility.SetImageFillAmountIfDifferent(this.mainMaxStatFill, this.animatedFloats[0].value, 1f / 512f);
    GameUtility.SetImageFillAmountIfDifferent(this.mainStatFill, this.animatedFloats[1].value, 1f / 512f);
    this.currentPartStatSlider.value = this.animatedFloats[2].value;
    this.reliabilitySlider.normalizedValue = this.animatedFloats[3].value;
    this.redZoneSlider.normalizedValue = this.animatedFloats[4].value;
    this.maxReliabilityFill.fillAmount = this.animatedFloats[5].value;
  }

  public void ShowTooltip(string inType)
  {
    string outHeader = string.Empty;
    string outDescription = string.Empty;
    string key = inType;
    if (key != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (UIComponentsDetailsWidget.\u003C\u003Ef__switch\u0024map3D == null)
      {
        // ISSUE: reference to a compiler-generated field
        UIComponentsDetailsWidget.\u003C\u003Ef__switch\u0024map3D = new Dictionary<string, int>(9)
        {
          {
            "Performance",
            0
          },
          {
            "Reliability",
            1
          },
          {
            "Cost",
            2
          },
          {
            "Time",
            3
          },
          {
            "SlotLocked1",
            4
          },
          {
            "SlotLocked2",
            5
          },
          {
            "SlotLocked3",
            6
          },
          {
            "SlotLocked4",
            7
          },
          {
            "SlotLocked5",
            8
          }
        };
      }
      int num;
      // ISSUE: reference to a compiler-generated field
      if (UIComponentsDetailsWidget.\u003C\u003Ef__switch\u0024map3D.TryGetValue(key, out num))
      {
        switch (num)
        {
          case 0:
            outHeader = Localisation.LocaliseID("PSG_10010510", (GameObject) null);
            outDescription = this.mDesign.GetPerformanceBreakdown();
            break;
          case 1:
            outHeader = Localisation.LocaliseID("PSG_10010511", (GameObject) null);
            outDescription = this.mDesign.GetReliabilityBreakdown();
            break;
          case 2:
            outHeader = Localisation.LocaliseID("PSG_10010512", (GameObject) null);
            outDescription = this.mDesign.GetCostBreakdown();
            break;
          case 3:
            outHeader = Localisation.LocaliseID("PSG_10010513", (GameObject) null);
            outDescription = this.mDesign.GetDesignTimeBreakdown();
            break;
          case 4:
            this.SetSlotLockedData(out outHeader, out outDescription, 1);
            break;
          case 5:
            this.SetSlotLockedData(out outHeader, out outDescription, 2);
            break;
          case 6:
            this.SetSlotLockedData(out outHeader, out outDescription, 3);
            break;
          case 7:
            this.SetSlotLockedData(out outHeader, out outDescription, 4);
            break;
          case 8:
            this.SetSlotLockedData(out outHeader, out outDescription, 5);
            break;
        }
      }
    }
    UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Open(outHeader, outDescription);
  }

  private void SetSlotLockedData(out string outHeader, out string outDescription, int inIndex)
  {
    outHeader = Localisation.LocaliseID("PSG_10010504", (GameObject) null);
    switch (inIndex)
    {
      case 1:
        outDescription = Localisation.LocaliseID("PSG_10010505", (GameObject) null);
        break;
      case 2:
        outDescription = Localisation.LocaliseID("PSG_10010506", (GameObject) null);
        break;
      case 3:
        outDescription = Localisation.LocaliseID("PSG_10010507", (GameObject) null);
        break;
      case 4:
        outDescription = Localisation.LocaliseID("PSG_10010508", (GameObject) null);
        break;
      case 5:
        outDescription = Localisation.LocaliseID("PSG_10010509", (GameObject) null);
        break;
      default:
        outDescription = string.Empty;
        break;
    }
  }

  public void HideTooltip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Close();
  }

  private void AddBonusText(string inDescription, int inIndex, CarPartComponent inComponent)
  {
    UIAdditionalBonusEntry additionalBonusEntry;
    if (this.bonusesEntries.Count <= inIndex)
    {
      additionalBonusEntry = UnityEngine.Object.Instantiate<UIAdditionalBonusEntry>(this.bonusesEntries[0]);
      additionalBonusEntry.transform.SetParent(this.bonusesEntries[0].transform.parent, false);
      this.bonusesEntries.Add(additionalBonusEntry);
    }
    else
      additionalBonusEntry = this.bonusesEntries[inIndex];
    additionalBonusEntry.Setup(inComponent);
    additionalBonusEntry.gameObject.SetActive(true);
    additionalBonusEntry.text.text = inDescription;
  }
}
