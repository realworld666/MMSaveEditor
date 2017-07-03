// Decompiled with JetBrains decompiler
// Type: UIPanelInventoryPartEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPanelInventoryPartEntry : MonoBehaviour
{
  public TextMeshProUGUI[] partNameLabel = new TextMeshProUGUI[0];
  public Button[] buttons = new Button[0];
  public GameObject[] targetCarSprite;
  public GameObject[] carGlowSprite;
  public CarPart.PartType slotType;
  public CarPart.PartType slotTypeGT;
  public Transform iconParent;
  public Image levelBacking;
  public TextMeshProUGUI levelLabel;
  public GameObject riskGraphicContainer;
  public UIGridList modifierIconsList;
  public UINotification notification;
  public Image performanceBar;
  public UIPartConditionBar conditionBar;
  public GameObject partNameLabelParent;
  public Animator animator;
  private CarPart.PartType mSlotType;
  private CarPart mPart;
  private CarPartFittingScreen mScreen;

  public CarPart carPart
  {
    get
    {
      return this.mPart;
    }
  }

  private void Start()
  {
    for (int index = 0; index < this.partNameLabel.Length; ++index)
      this.partNameLabel[index].text = Localisation.LocaliseEnum((Enum) this.mSlotType);
    for (int index = 0; index < this.buttons.Length; ++index)
      this.buttons[index].onClick.AddListener(new UnityAction(this.OnMouseClicked));
  }

  private void OnMouseClicked()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mScreen.itemListWidget.Open(this.mSlotType);
  }

  private void OpenPopup()
  {
    if (this.mPart == null)
      return;
    scSoundManager.BlockSoundEvents = true;
    UIManager.instance.dialogBoxManager.GetDialog<UIPartInfoPopupWidget>().ShowTooltip(this.mPart, (RectTransform) null);
    scSoundManager.BlockSoundEvents = false;
  }

  private void ClosePopup()
  {
    UIManager.instance.dialogBoxManager.GetDialog<UIPartInfoPopupWidget>().HideTooltip();
  }

  public void Refresh(CarPart inPart)
  {
    this.Setup(inPart);
  }

  public void Setup(CarPart inPart)
  {
    this.mScreen = UIManager.instance.GetScreen<CarPartFittingScreen>();
    this.mPart = inPart;
    for (int index = 0; index < this.targetCarSprite.Length; ++index)
      this.targetCarSprite[index].SetActive(this.mPart != null);
    if (this.mPart == null)
    {
      this.SetToEmptySlot();
      if (!((UnityEngine.Object) this.animator != (UnityEngine.Object) null))
        return;
      this.animator.SetTrigger(AnimationHashes.Empty);
    }
    else
    {
      this.mSlotType = this.mPart.GetPartType();
      if ((UnityEngine.Object) this.animator != (UnityEngine.Object) null)
        this.animator.SetTrigger(AnimationHashes.Used);
      this.notification.notificationName = this.mSlotType.ToString();
      this.notification.FindNotification();
      this.SetPartData();
      this.SetFittedSlot();
      CarPart.SetIcon(this.iconParent, this.mSlotType);
    }
  }

  private void SetPartData()
  {
    this.levelBacking.color = UIConstants.GetPartLevelColor(this.mPart.stats.level);
    this.levelLabel.text = this.mPart.GetLevelUIString();
  }

  public void SetToEmptySlot()
  {
    for (int index = 0; index < this.carGlowSprite.Length; ++index)
      GameUtility.SetActive(this.carGlowSprite[index], false);
    GameUtility.SetActive(this.iconParent.gameObject, false);
  }

  private void SetFittedSlot()
  {
    for (int index = 0; index < this.carGlowSprite.Length; ++index)
      GameUtility.SetActive(this.carGlowSprite[index], true);
    GameUtility.SetActive(this.iconParent.gameObject, true);
    this.UpdatePartData();
  }

  public void UpdatePartData()
  {
    if (this.mPart == null)
      return;
    this.conditionBar.Setup(this.mPart);
    this.performanceBar.fillAmount = CarPartStats.GetNormalizedStatValue(this.mPart.stats.statWithPerformance, this.mPart.GetPartType());
    this.performanceBar.color = UIConstants.GetPartLevelColor(this.mPart.stats.level);
    GameUtility.SetActive(this.riskGraphicContainer, !Mathf.Approximately(this.mPart.stats.rulesRisk, 0.0f));
    for (int inIndex = 0; inIndex < this.modifierIconsList.itemCount; ++inIndex)
      GameUtility.SetActive(this.modifierIconsList.GetItem(inIndex).gameObject, false);
    for (int inIndex = 0; inIndex < this.mPart.components.Count; ++inIndex)
    {
      CarPartComponent component = this.mPart.components[inIndex];
      if (component != null && !component.IgnoreBonusForUI() && (component.HasActivationRequirement() || component.bonuses.Count != 0))
        GameUtility.SetActive(this.modifierIconsList.GetOrCreateItem<Transform>(inIndex).gameObject, true);
    }
  }
}
