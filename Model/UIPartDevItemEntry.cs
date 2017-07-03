// Decompiled with JetBrains decompiler
// Type: UIPartDevItemEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPartDevItemEntry : MonoBehaviour
{
  public Toggle[] improvementToggles = new Toggle[0];
  private Color mDefaultColor = new Color();
  public GameObject popupTarget;
  public GameObject newContainer;
  public UIKnowledgeBar knowledgeBar;
  public Image riskIcon;
  public Slider mainStatSlider;
  public Image performanceBacking;
  public TextMeshProUGUI partMainStat;
  public TextMeshProUGUI partMaxPerformance;
  public TextMeshProUGUI partCondition;
  public UIPartConditionBar condition;
  public GameObject modifierIconContainer;
  public UINotification notification;
  public GameObject toggleContainer;
  public GameObject bannedContainer;
  private CarPart mPart;
  private bool mPopupOpen;

  public CarPart carPart
  {
    get
    {
      return this.mPart;
    }
  }

  private void Awake()
  {
    this.mDefaultColor = this.partMainStat.color;
    this.improvementToggles[0].onValueChanged.AddListener((UnityAction<bool>) (value => this.OnPerformanceToggle(value)));
    this.improvementToggles[1].onValueChanged.AddListener((UnityAction<bool>) (value => this.OnReliabilityToggle(value)));
  }

  private void Update()
  {
    GameUtility.HandlePopup(ref this.mPopupOpen, this.popupTarget, new Action(this.OpenPopup), new Action(this.ClosePopup));
  }

  private void UpdateNotification()
  {
    Notification notification = Game.instance.notificationManager.GetNotification(this.mPart.name);
    if (notification == null)
      return;
    notification.ResetCount();
    Game.instance.notificationManager.RemoveNotification(notification);
  }

  private void OpenPopup()
  {
    this.UpdateNotification();
    UIManager.instance.dialogBoxManager.GetDialog<UIPartInfoPopupWidget>().ShowTooltip(this.mPart, (RectTransform) null);
  }

  private void ClosePopup()
  {
    UIManager.instance.dialogBoxManager.GetDialog<UIPartInfoPopupWidget>().HideTooltip();
  }

  public void OpenRiskPopup()
  {
    UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Open("Rules risk", this.mPart.stats.rulesRiskString);
  }

  public void CloseRiskPopup()
  {
    UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Close();
  }

  public void RefreshEntry()
  {
    this.Setup(this.mPart);
  }

  public void Setup(CarPart inPart)
  {
    GameUtility.SetActive(this.gameObject, true);
    this.mPart = inPart;
    this.notification.notificationName = this.mPart.name;
    this.notification.FindNotification();
    GameUtility.SetActive(this.riskIcon.gameObject, (double) this.mPart.stats.rulesRisk > 0.0);
    this.riskIcon.color = CarPartStats.GetRiskColor(this.mPart.stats.rulesRisk);
    GameUtility.SetActive(this.newContainer, (Game.instance.time.now - this.mPart.buildDate).Days < 7);
    this.partMainStat.text = Mathf.RoundToInt(inPart.stats.statWithPerformance).ToString();
    this.partMaxPerformance.text = string.Format("({0})", (object) Mathf.RoundToInt(inPart.stats.stat + Mathf.Max(0.0f, inPart.stats.maxPerformance)));
    this.knowledgeBar.SetupKnowledge(this.mPart.stats.level);
    Color partLevelColor = UIConstants.GetPartLevelColor(this.mPart.stats.level);
    this.mainStatSlider.normalizedValue = CarPartStats.GetNormalizedStatValue(this.mPart.stats.statWithPerformance, this.mPart.GetPartType());
    this.mainStatSlider.fillRect.GetComponent<Image>().color = partLevelColor;
    partLevelColor.a = 0.8f;
    this.performanceBacking.fillAmount = CarPartStats.GetNormalizedStatValue(this.mPart.stats.stat + this.mPart.stats.maxPerformance, this.mPart.GetPartType());
    this.performanceBacking.color = partLevelColor;
    this.condition.Setup(this.mPart);
    this.partCondition.text = Mathf.RoundToInt(this.mPart.partCondition.condition * 100f).ToString() + " %";
    this.SetTextColor(this.partMainStat, this.mPart.stats.statType);
    GameUtility.SetActive(this.modifierIconContainer, false);
    GameUtility.SetActive(this.bannedContainer, this.mPart.isBanned);
    for (int index = 0; index < this.mPart.components.Count; ++index)
    {
      CarPartComponent component = this.mPart.components[index];
      if (component != null && !component.IgnoreBonusForUI() && (component.HasActivationRequirement() || component.bonuses.Count != 0))
      {
        GameUtility.SetActive(this.modifierIconContainer, true);
        break;
      }
    }
    PartImprovement partImprovement = Game.instance.player.team.carManager.partImprovement;
    this.improvementToggles[0].interactable = (!PartImprovement.AnyPartNeedsWork(CarPartStats.CarPartStat.Performance, this.mPart) ? 0 : (!this.mPart.isBanned ? 1 : 0)) != 0;
    this.improvementToggles[1].interactable = (!PartImprovement.AnyPartNeedsWork(CarPartStats.CarPartStat.Reliability, this.mPart) ? 0 : (!this.mPart.isBanned ? 1 : 0)) != 0;
    this.improvementToggles[0].isOn = partImprovement.partsToImprove[3].Contains(this.mPart);
    this.improvementToggles[1].isOn = partImprovement.partsToImprove[1].Contains(this.mPart);
  }

  private void SetStatText(TextMeshProUGUI inText, CarStats.StatType inStat, CarPart inPart)
  {
    if (inPart.stats.statType == inStat)
      inText.text = Mathf.RoundToInt(inPart.stats.statWithPerformance).ToString();
    else
      inText.text = "-";
  }

  private void SetTextColor(TextMeshProUGUI inText, CarStats.StatType inStat)
  {
    CarManager carManager = Game.instance.player.team.carManager;
    if (carManager.PartHasBestStatOnGrid(this.mPart, inStat))
      inText.color = UIConstants.sectorSessionFastestColor;
    else if (carManager.PartHasBestStatOfTeam(this.mPart, inStat))
      inText.color = UIConstants.sectorDriverFastestColor;
    else
      inText.color = this.mDefaultColor;
  }

  private void OnReliabilityToggle(bool inBool)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.AddOrRemovePart(inBool, CarPartStats.CarPartStat.Reliability);
    this.UpdateNotification();
  }

  private void OnPerformanceToggle(bool inBool)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.AddOrRemovePart(inBool, CarPartStats.CarPartStat.Performance);
    this.UpdateNotification();
  }

  private void AddOrRemovePart(bool inBool, CarPartStats.CarPartStat inStat)
  {
    PartImprovement partImprovement = Game.instance.player.team.carManager.partImprovement;
    CarPartStats.CarPartStat inStackType = inStat;
    if (inBool && !partImprovement.partsToImprove[(int) inStackType].Contains(this.mPart))
    {
      partImprovement.AddPartToImprove(inStackType, this.mPart);
    }
    else
    {
      if (inBool || !partImprovement.partsToImprove[(int) inStackType].Contains(this.mPart))
        return;
      partImprovement.RemovePartImprove(inStackType, this.mPart, true);
    }
  }
}
