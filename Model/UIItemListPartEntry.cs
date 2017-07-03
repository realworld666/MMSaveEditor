// Decompiled with JetBrains decompiler
// Type: UIItemListPartEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIItemListPartEntry : MonoBehaviour
{
  public Toggle[] driverToggles = new Toggle[0];
  public ActivateForSeries.GameObjectData[] seriesSpecificData = new ActivateForSeries.GameObjectData[0];
  private Color mDefaultColor = new Color();
  public GameObject popupTarget;
  public GameObject newContainer;
  public UIKnowledgeBar knowledgeBar;
  public TextMeshProUGUI partTypeLabel;
  public Image riskIcon;
  public Slider mainStatSlider;
  public Image performanceBacking;
  public TextMeshProUGUI partMainStat;
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
    for (int index = 0; index < CarManager.carCount; ++index)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: reference to a compiler-generated method
      this.driverToggles[index].onValueChanged.AddListener(new UnityAction<bool>(new UIItemListPartEntry.\u003CAwake\u003Ec__AnonStorey77()
      {
        \u003C\u003Ef__this = this,
        identifier = index
      }.\u003C\u003Em__121));
    }
  }

  private void OnToggleChanged(bool inValue, int inIdentifier)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (inValue)
      Game.instance.player.team.carManager.GetCar(inIdentifier).FitPart(this.mPart);
    else
      Game.instance.player.team.carManager.GetCar(inIdentifier).UnfitPart(this.mPart);
    UIManager.instance.GetScreen<CarPartFittingScreen>().RefreshCarInventoryWidgets();
    this.UpdateNotification();
  }

  private void SetFittedData()
  {
    if (this.mPart.isFitted)
    {
      int index = this.mPart.fittedCar.identifier != 0 ? 0 : 1;
      this.driverToggles[this.mPart.fittedCar.identifier].isOn = true;
      this.driverToggles[index].isOn = false;
    }
    if (this.mPart.isBanned)
    {
      this.toggleContainer.SetActive(false);
      this.bannedContainer.SetActive(true);
    }
    else
    {
      this.toggleContainer.SetActive(true);
      this.bannedContainer.SetActive(false);
    }
  }

  private void UpdateNotification()
  {
    Notification notification = Game.instance.notificationManager.GetNotification(this.mPart.name);
    if (notification == null)
      return;
    notification.ResetCount();
    Game.instance.notificationManager.RemoveNotification(notification);
  }

  private void Update()
  {
    for (int index = 0; index < CarManager.carCount; ++index)
    {
      bool flag = this.mPart.isFitted && this.mPart.fittedCar.identifier == index;
      if (this.driverToggles[index].isOn != flag)
        this.driverToggles[index].isOn = flag;
    }
    GameUtility.HandlePopup(ref this.mPopupOpen, this.popupTarget, new Action(this.OpenPopup), new Action(this.ClosePopup));
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
    UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Open(Localisation.LocaliseID("PSG_10010806", (GameObject) null), this.mPart.stats.rulesRiskString);
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
    GameUtility.SetActiveForSeries(Game.instance.player.team.championship, this.seriesSpecificData);
    this.notification.notificationName = this.mPart.name;
    this.notification.FindNotification();
    GameUtility.SetActive(this.riskIcon.gameObject, (double) this.mPart.stats.rulesRisk > 0.0);
    this.riskIcon.color = CarPartStats.GetRiskColor(this.mPart.stats.rulesRisk);
    this.partTypeLabel.text = this.mPart.GetPartName();
    GameUtility.SetActive(this.newContainer, (Game.instance.time.now - this.mPart.buildDate).Days < 7);
    this.partMainStat.text = Mathf.RoundToInt(inPart.stats.statWithPerformance).ToString();
    this.knowledgeBar.SetupKnowledge(this.mPart.stats.level);
    Color partLevelColor = UIConstants.GetPartLevelColor(this.mPart.stats.level);
    this.mainStatSlider.normalizedValue = CarPartStats.GetNormalizedStatValue(this.mPart.stats.statWithPerformance, this.mPart.GetPartType());
    this.mainStatSlider.fillRect.GetComponent<Image>().color = partLevelColor;
    partLevelColor.a = 0.8f;
    this.performanceBacking.fillAmount = CarPartStats.GetNormalizedStatValue(this.mPart.stats.stat + this.mPart.stats.maxPerformance, this.mPart.GetPartType());
    this.performanceBacking.color = partLevelColor;
    this.condition.Setup(this.mPart);
    this.partCondition.text = Mathf.RoundToInt(this.mPart.partCondition.condition * 100f).ToString() + " %";
    this.SetFittedData();
    this.SetTextColor(this.partMainStat, this.mPart.stats.statType);
    this.modifierIconContainer.SetActive(false);
    for (int index = 0; index < this.mPart.components.Count; ++index)
    {
      CarPartComponent component = this.mPart.components[index];
      if (component != null && !component.IgnoreBonusForUI() && (component.HasActivationRequirement() || component.bonuses.Count != 0))
      {
        GameUtility.SetActive(this.modifierIconContainer, true);
        break;
      }
    }
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

  public static string GetDateText(DateTime inDateTime)
  {
    TimeSpan timeSpan = Game.instance.time.now - inDateTime;
    if (timeSpan.Days < 7)
      return GameUtility.ColorToRichTextHex(UIConstants.positiveColor) + Localisation.LocaliseID("PSG_10010379", (GameObject) null) + "</color>";
    return Mathf.RoundToInt((float) timeSpan.Days).ToString() + " d";
  }
}
