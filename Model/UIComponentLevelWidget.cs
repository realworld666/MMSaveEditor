// Decompiled with JetBrains decompiler
// Type: UIComponentLevelWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIComponentLevelWidget : MonoBehaviour
{
  public Color bonusesTextColor = Color.white;
  public TextMeshProUGUI[] levelLabels = new TextMeshProUGUI[0];
  public List<UIComponentEntry> entries = new List<UIComponentEntry>();
  public int level;
  public GameObject setCostContainer;
  public GameObject costTextContaiter;
  public GameObject timeTextContainer;
  public TextMeshProUGUI buildTimeLabel;
  public TextMeshProUGUI buildCostLabel;
  public UIComponentEntry engineerEntry;
  public GameObject activeContainer;
  public GameObject lockedContainer;
  private PartDesignScreen mScreen;
  private bool mIsLevelUnlocked;
  private bool mRefreshFlag;

  public void Setup()
  {
    this.SetComponentsColor();
    this.mScreen = UIManager.instance.GetScreen<PartDesignScreen>();
    PartTypeSlotSettings typeSlotSettings = Game.instance.partSettingsManager.championshipPartSettings[Game.instance.player.team.championship.championshipID][this.mScreen.partType];
    this.mIsLevelUnlocked = typeSlotSettings.IsUnlocked(Game.instance.player.team, this.level);
    this.setCostContainer.SetActive(this.mIsLevelUnlocked);
    this.activeContainer.SetActive(this.mIsLevelUnlocked);
    this.lockedContainer.SetActive(!this.mIsLevelUnlocked);
    string levelUiString = CarPart.GetLevelUIString(this.level + 1);
    for (int index = 0; index < this.levelLabels.Length; ++index)
      this.levelLabels[index].text = levelUiString;
    if (this.mIsLevelUnlocked)
    {
      if ((double) typeSlotSettings.timePerLevel[this.level] != 0.0)
      {
        StringVariableParser.ordinalNumberString = typeSlotSettings.timePerLevel[this.level].ToString((IFormatProvider) Localisation.numberFormatter);
        this.buildTimeLabel.text = Localisation.LocaliseID("PSG_10010377", (GameObject) null);
      }
      else
        this.buildTimeLabel.text = Localisation.LocaliseID("PSG_10005815", (GameObject) null);
      this.costTextContaiter.SetActive((int) typeSlotSettings.costPerLevel[this.level] != 0);
      StringVariableParser.ordinalNumberString = GameUtility.GetCurrencyString((long) (int) typeSlotSettings.costPerLevel[this.level], 0);
      this.buildCostLabel.text = Localisation.LocaliseID("PSG_10010527", (GameObject) null);
    }
    this.SetComponentsData();
    this.RefreshUI();
  }

  public void Hide()
  {
    this.gameObject.SetActive(false);
    this.engineerEntry.gameObject.SetActive(false);
  }

  public void Show()
  {
    this.gameObject.SetActive(true);
    this.engineerEntry.gameObject.SetActive(true);
  }

  private bool HasEntrySelected()
  {
    for (int index = 0; index < this.entries.Count; ++index)
    {
      if (this.entries[index].selectionToggle.isOn)
        return true;
    }
    return this.engineerEntry.selectionToggle.isOn;
  }

  private void SetRefreshFlag()
  {
    this.mRefreshFlag = true;
  }

  private void LateUpdate()
  {
    if (!this.mRefreshFlag)
      return;
    this.mRefreshFlag = false;
    this.RefreshUI();
  }

  private void RefreshUI()
  {
    CarPartDesign carPartDesign = Game.instance.player.team.carManager.carPartDesign;
    bool inValue = carPartDesign.HasSlotForLevel(this.level + 1);
    this.SetComponentsLockState(this.mIsLevelUnlocked && inValue);
    this.engineerEntry.SetCanvasGroupInteractivity(inValue);
    for (int index = 0; index < this.entries.Count; ++index)
    {
      if (!inValue && this.mIsLevelUnlocked && !this.entries[index].selectionToggle.isOn)
        this.entries[index].SetState(UIComponentEntry.LockedState.NoSlotAvailable);
      else if (!this.mIsLevelUnlocked)
        this.entries[index].SetState(UIComponentEntry.LockedState.SetLocked);
      else
        this.entries[index].SetState(UIComponentEntry.LockedState.Unlocked);
      CarPartComponent component = this.entries[index].component;
      if (this.entries[index].component != null && !carPartDesign.HasComponent(component))
        this.entries[index].selectionToggle.isOn = false;
    }
    if (!inValue && !this.engineerEntry.selectionToggle.isOn)
      this.engineerEntry.SetState(UIComponentEntry.LockedState.NoSlotAvailable);
    else
      this.engineerEntry.SetState(UIComponentEntry.LockedState.Unlocked);
    if (this.engineerEntry.component == null || carPartDesign.HasComponent(this.engineerEntry.component))
      return;
    this.engineerEntry.selectionToggle.isOn = false;
  }

  public void SetComponentsLockState(bool inValue)
  {
    for (int index = 0; index < this.entries.Count; ++index)
      this.entries[index].SetCanvasGroupInteractivity(inValue);
  }

  private void SetComponentsData()
  {
    CarPartDesign carPartDesign = Game.instance.player.team.carManager.carPartDesign;
    List<CarPartComponent> carPartComponentList = carPartDesign.GetComponentsForPartType(this.mScreen.partType)[this.level];
    for (int index = 0; index < carPartComponentList.Count; ++index)
      this.entries[index].Setup(carPartComponentList[index]);
    this.engineerEntry.SetCanvasGroupInteractivity(carPartDesign.HasSlotForLevel(this.level + 1));
  }

  private void SetComponentsColor()
  {
    Color bonusesTextColor = this.bonusesTextColor;
    for (int index = 0; index < this.entries.Count; ++index)
      this.entries[index].dataColor = bonusesTextColor;
    this.engineerEntry.dataColor = bonusesTextColor;
  }

  public void ResetChoices()
  {
    for (int index = 0; index < this.entries.Count; ++index)
    {
      this.entries[index].RemoveListener();
      this.entries[index].selectionToggle.isOn = false;
      this.entries[index].AddListener();
    }
    this.engineerEntry.RemoveListener();
    this.engineerEntry.selectionToggle.isOn = false;
    this.engineerEntry.AddListener();
  }

  private void OnEnable()
  {
    if (!Game.IsActive())
      return;
    Game.instance.player.team.carManager.carPartDesign.OnDesignModified += new Action(this.SetRefreshFlag);
  }

  private void OnDisable()
  {
    if (!Game.IsActive())
      return;
    Game.instance.player.team.carManager.carPartDesign.OnDesignModified -= new Action(this.SetRefreshFlag);
  }
}
