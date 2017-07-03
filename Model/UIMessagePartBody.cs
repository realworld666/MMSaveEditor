// Decompiled with JetBrains decompiler
// Type: UIMessagePartBody
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMessagePartBody : UIMailMessageBody
{
  [Header("Derived Class Settings")]
  public TextMeshProUGUI partName;
  public TextMeshProUGUI partStat;
  public TextMeshProUGUI previousBestPartData;
  public Transform partIconParent;
  public UIKnowledgeBar knowledgeBar;
  public TextMeshProUGUI reliabilityText;
  public UIPartConditionBar conditionBar;
  public TextMeshProUGUI mainStatText;
  public TextMeshProUGUI performanceText;
  public Image mainStatBacking;
  public Image performanceBacking;
  public GameObject partDetailsContainer;
  private CarPart mPart;

  public override void SetupSpecialObjectText()
  {
    this.mPart = this.message.specialObject[0] as CarPart;
    CarPart carPart = this.message.specialObject[1] as CarPart;
    if (this.mPart != null && !Game.instance.player.IsUnemployed())
    {
      GameUtility.SetActive(this.partDetailsContainer, true);
      this.knowledgeBar.SetupKnowledge(this.mPart.stats.level);
      CarPart.PartType partType = this.mPart.GetPartType();
      this.partName.text = Localisation.LocaliseEnum((Enum) partType);
      this.partStat.text = Localisation.LocaliseEnum((Enum) CarPart.GetStatForPartType(partType));
      this.performanceBacking.fillAmount = CarPartStats.GetNormalizedStatValue(this.mPart.stats.stat + this.mPart.stats.maxPerformance, this.mPart.GetPartType());
      this.mainStatBacking.fillAmount = CarPartStats.GetNormalizedStatValue(this.mPart.stats.statWithPerformance, this.mPart.GetPartType());
      Color partLevelColor = UIConstants.GetPartLevelColor(this.mPart.stats.level);
      this.mainStatBacking.color = partLevelColor;
      partLevelColor.a = 0.8f;
      this.performanceBacking.color = partLevelColor;
      this.conditionBar.Setup(this.mPart);
      this.mainStatText.text = Mathf.RoundToInt(this.mPart.stats.statWithPerformance).ToString();
      this.performanceText.text = "/" + Mathf.RoundToInt(this.mPart.stats.stat + this.mPart.stats.maxPerformance).ToString();
      this.reliabilityText.text = this.mPart.stats.reliability.ToString("P0", (IFormatProvider) Localisation.numberFormatter);
      StringVariableParser.partForUI = carPart;
      this.previousBestPartData.text = Localisation.LocaliseID("PSG_10009193", (GameObject) null);
      CarPart.SetIcon(this.partIconParent, partType);
    }
    else
    {
      if (!Game.instance.player.IsUnemployed())
        return;
      GameUtility.SetActive(this.partDetailsContainer, false);
    }
  }

  public void ShowToolTip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<UIPartInfoPopupWidget>().ShowTooltip(this.mPart, (RectTransform) null);
  }

  public void HideToolTip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<UIPartInfoPopupWidget>().HideTooltip();
  }
}
