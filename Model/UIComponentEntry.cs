// Decompiled with JetBrains decompiler
// Type: UIComponentEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIComponentEntry : MonoBehaviour
{
  public Color dataColor = new Color();
  private UIComponentEntry.LockedState mLockedState = UIComponentEntry.LockedState.Unlocked;
  public GameObject availableContainer;
  public TextMeshProUGUI description;
  public Image icon;
  public Image backing;
  public Image iconBacking;
  public Toggle selectionToggle;
  public GameObject GfxContainer;
  public GameObject noComponentKnow;
  private CanvasGroup mCanvasGroup;
  private CarPartComponent mComponent;

  public CarPartComponent component
  {
    get
    {
      return this.mComponent;
    }
  }

  private void Awake()
  {
    this.mCanvasGroup = this.gameObject.GetComponent<CanvasGroup>();
    EventTrigger eventTrigger = this.selectionToggle.gameObject.AddComponent<EventTrigger>();
    EventTrigger.Entry entry1 = new EventTrigger.Entry();
    entry1.eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter;
    entry1.callback.AddListener((UnityAction<BaseEventData>) (eventData => this.OnMouseEnter()));
    eventTrigger.get_triggers().Add(entry1);
    EventTrigger.Entry entry2 = new EventTrigger.Entry();
    entry2.eventID = UnityEngine.EventSystems.EventTriggerType.PointerExit;
    entry2.callback.AddListener((UnityAction<BaseEventData>) (eventData => this.OnMouseExit()));
    eventTrigger.get_triggers().Add(entry2);
  }

  private void OnMouseEnter()
  {
    if (this.mComponent == null)
      return;
    UIManager.instance.GetScreen<PartDesignScreen>().componentsDetailWidget.HighLightSlots(this.mComponent.level - 1);
  }

  private void OnMouseExit()
  {
    UIManager.instance.GetScreen<PartDesignScreen>().componentsDetailWidget.HighLightSlots(10);
  }

  public void SetState(UIComponentEntry.LockedState inState)
  {
    this.mLockedState = inState;
  }

  public void ShowTooltip()
  {
    if (this.mComponent == null)
      return;
    string inHeader = string.Empty;
    string inDescription = string.Empty;
    StringVariableParser.stringValue1 = CarPart.GetLevelUIString(this.mComponent.level);
    switch (this.mLockedState)
    {
      case UIComponentEntry.LockedState.NoSlotAvailable:
        if (Game.instance.player.team.carManager.carPartDesign.componentSlots.Count < this.mComponent.level)
        {
          inHeader = Localisation.LocaliseID("PSG_10010793", (GameObject) null);
          inDescription = Localisation.LocaliseID("PSG_10010794", (GameObject) null);
          break;
        }
        inHeader = Localisation.LocaliseID("PSG_10005664", (GameObject) null);
        inDescription = Localisation.LocaliseID("PSG_10010795", (GameObject) null);
        break;
      case UIComponentEntry.LockedState.SetLocked:
        PartTypeSlotSettings typeSlotSettings = Game.instance.partSettingsManager.championshipPartSettings[Game.instance.player.team.championship.championshipID][UIManager.instance.GetScreen<PartDesignScreen>().partType];
        inHeader = Localisation.LocaliseID("PSG_10010792", (GameObject) null);
        inDescription = typeSlotSettings.GetLockedDescription(Game.instance.player.team, this.mComponent.level - 1);
        break;
      case UIComponentEntry.LockedState.Unlocked:
        return;
    }
    UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Open(inHeader, inDescription);
  }

  public void HideTooltip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Close();
  }

  public void AddListener()
  {
    this.selectionToggle.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnSelect(value)));
  }

  public void RemoveListener()
  {
    this.selectionToggle.onValueChanged.RemoveAllListeners();
  }

  private void OnSelect(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    CarPartDesign carPartDesign = Game.instance.player.team.carManager.carPartDesign;
    if (inValue && carPartDesign.HasSlotForLevel(this.mComponent.level))
      carPartDesign.AddComponent(carPartDesign.part, this.mComponent);
    else if (inValue)
    {
      this.selectionToggle.isOn = false;
    }
    else
    {
      if (!carPartDesign.HasComponent(this.mComponent))
        return;
      carPartDesign.RemoveComponent(carPartDesign.part, this.mComponent);
    }
  }

  public void Setup(CarPartComponent inComponent)
  {
    this.mComponent = inComponent;
    if ((UnityEngine.Object) this.GfxContainer != (UnityEngine.Object) null)
    {
      this.noComponentKnow.SetActive(this.mComponent == null);
      this.GfxContainer.SetActive(this.mComponent != null);
    }
    if (this.mComponent == null)
      return;
    string iconPath = inComponent.GetIconPath();
    this.icon.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Frontend1, "PartDevIcons-" + iconPath);
    this.iconBacking.color = UIConstants.GetPartLevelColor(this.mComponent.level);
    CarPartDesign carPartDesign = Game.instance.player.team.carManager.carPartDesign;
    this.availableContainer.SetActive(this.mComponent.IsUnlocked(carPartDesign.team));
    this.description.text = this.mComponent.GetName(carPartDesign.part);
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
    if (this.mComponent == null)
      return;
    this.selectionToggle.graphic.color = UIConstants.GetPartLevelColor(this.mComponent.level);
  }

  public void SetCanvasGroupInteractivity(bool inValue)
  {
    if (this.selectionToggle.isOn || this.mComponent == null || (UnityEngine.Object) this.mCanvasGroup == (UnityEngine.Object) null)
      return;
    this.mCanvasGroup.alpha = !inValue ? 0.3f : 1f;
    this.mCanvasGroup.interactable = inValue;
  }

  public enum LockedState
  {
    NoSlotAvailable,
    SetLocked,
    Unlocked,
  }
}
