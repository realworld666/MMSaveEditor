// Decompiled with JetBrains decompiler
// Type: UIPartInfoPopupWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPartInfoPopupWidget : UIDialogBox
{
  public List<UIAdditionalBonusEntry> bonusesEntries = new List<UIAdditionalBonusEntry>();
  private Color mInitialDateColor = new Color();
  public UIKnowledgeBar knowledgeBar;
  public Transform partIconParent;
  public TextMeshProUGUI statName;
  public TextMeshProUGUI partType;
  public TextMeshProUGUI riskLabel;
  public GameObject riskContainer;
  public Image performanceFill;
  public Image maxPerformanceFill;
  public TextMeshProUGUI performanceHeaderLabel;
  public TextMeshProUGUI performanceLabel;
  public TextMeshProUGUI maxPerformanceLabel;
  public UIPartConditionBar conditionBar;
  public TextMeshProUGUI partReliability;
  public TextMeshProUGUI partMaxReliability;
  public TextMeshProUGUI partBuildDate;
  public GameObject partBannedContainer;
  public GameObject bonusesContainerData;
  private RectTransform mTransform;
  private RectTransform mRefTransform;

  protected override void Awake()
  {
    base.Awake();
    this.mInitialDateColor = this.partBuildDate.color;
    UIManager.OnScreenChange += new Action(this.HideTooltip);
  }

  public void OnDestroy()
  {
    UIManager.OnScreenChange -= new Action(this.HideTooltip);
  }

  public void ShowTooltip(CarPart inPart)
  {
    this.ShowTooltip(inPart, (RectTransform) null);
  }

  public void ShowTooltip(CarPart inPart, RectTransform inTransform)
  {
    scSoundManager.BlockSoundEvents = true;
    this.mRefTransform = inTransform;
    this.mTransform = this.GetComponent<RectTransform>();
    GameUtility.SetTooltipTransformInsideScreen(this.mTransform, this.mRefTransform, new Vector3(), false, (RectTransform) null);
    this.Setup(inPart);
    this.gameObject.SetActive(true);
    scSoundManager.BlockSoundEvents = false;
  }

  private void Update()
  {
    GameUtility.SetTooltipTransformInsideScreen(this.mTransform, this.mRefTransform, new Vector3(), false, (RectTransform) null);
  }

  public void HideTooltip()
  {
    this.gameObject.SetActive(false);
  }

  private void Setup(CarPart inPart)
  {
    this.SetIcon(inPart.GetPartType());
    this.statName.text = Localisation.LocaliseEnum((Enum) inPart.stats.statType);
    this.partType.text = Localisation.LocaliseEnum((Enum) inPart.GetPartType());
    Color partLevelColor = UIConstants.GetPartLevelColor(inPart.stats.level);
    this.performanceFill.fillAmount = CarPartStats.GetNormalizedStatValue(inPart.stats.statWithPerformance, inPart.GetPartType());
    this.performanceFill.color = partLevelColor;
    partLevelColor.a = 0.8f;
    this.maxPerformanceFill.fillAmount = CarPartStats.GetNormalizedStatValue(inPart.stats.stat + inPart.stats.maxPerformance, inPart.GetPartType());
    this.maxPerformanceFill.color = partLevelColor;
    this.knowledgeBar.SetupKnowledge(inPart.stats.level);
    this.conditionBar.Setup(inPart);
    GameUtility.SetActive(this.riskContainer, (double) inPart.stats.rulesRisk != 0.0);
    GameUtility.SetActive(this.partBannedContainer, inPart.isBanned);
    StringVariableParser.stringValue1 = inPart.stats.rulesRiskString;
    this.riskLabel.text = Localisation.LocaliseID("PSG_10010428", (GameObject) null);
    this.performanceHeaderLabel.text = Localisation.LocaliseEnum((Enum) inPart.stats.statType);
    this.performanceLabel.text = inPart.stats.statWithPerformance.ToString("N0", (IFormatProvider) Localisation.numberFormatter);
    this.maxPerformanceLabel.text = "/ " + (inPart.stats.stat + Mathf.Max(0.0f, inPart.stats.maxPerformance)).ToString("N0", (IFormatProvider) Localisation.numberFormatter);
    this.partReliability.text = inPart.stats.partCondition.condition.ToString("P0", (IFormatProvider) Localisation.numberFormatter);
    this.partMaxReliability.text = "/ " + inPart.stats.maxReliability.ToString("P0", (IFormatProvider) Localisation.numberFormatter);
    int days = (Game.instance.time.now - inPart.buildDate).Days;
    if (days < 7)
    {
      this.partBuildDate.text = Localisation.LocaliseID("PSG_10010379", (GameObject) null);
      this.partBuildDate.color = UIConstants.positiveColor;
    }
    else
    {
      StringVariableParser.intValue1 = days;
      this.partBuildDate.text = Localisation.LocaliseID("PSG_10010380", (GameObject) null);
      this.partBuildDate.color = this.mInitialDateColor;
    }
    this.ShowAditionalData(inPart);
  }

  private void ShowAditionalData(CarPart inPart)
  {
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

  private void SetIcon(CarPart.PartType inType)
  {
    for (int index = 0; index < this.partIconParent.childCount; ++index)
    {
      if ((CarPart.PartType) index == inType)
        this.partIconParent.GetChild(index).gameObject.SetActive(true);
      else
        this.partIconParent.GetChild(index).gameObject.SetActive(false);
    }
  }
}
