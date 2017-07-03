// Decompiled with JetBrains decompiler
// Type: UIComponentsChoiceWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class UIComponentsChoiceWidget : MonoBehaviour
{
  public UIComponentLevelWidget[] componentsWidget = new UIComponentLevelWidget[0];
  public UIEngineerComponentsWidget engineerComponentsWidget;
  public TextMeshProUGUI partTitle;
  public TextMeshProUGUI componentsSelected;
  public TextMeshProUGUI componentsTotal;
  public GameObject emptySlotsContainer;
  public Transform iconTransform;
  private CarPartDesign mDesign;

  public void Setup()
  {
    PartDesignScreen screen = UIManager.instance.GetScreen<PartDesignScreen>();
    this.mDesign = Game.instance.player.team.carManager.carPartDesign;
    this.engineerComponentsWidget.Setup();
    for (int index = 0; index < this.componentsWidget.Length; ++index)
      this.componentsWidget[index].Setup();
    StringVariableParser.partFrontendUI = screen.partType;
    this.partTitle.text = Localisation.LocaliseID("PSG_10010946", (GameObject) null);
    this.componentsTotal.text = "/" + (this.mDesign.componentSlots.Count + this.mDesign.componentBonusSlots.Count).ToString();
    this.SetIcon(screen.partType);
    this.mDesign.OnDesignModified += new Action(this.UpdateComponentFitted);
    this.UpdateComponentFitted();
  }

  private void OnDisable()
  {
    if (!Game.IsActive() || this.mDesign == null)
      return;
    this.mDesign.OnDesignModified -= new Action(this.UpdateComponentFitted);
  }

  private void UpdateComponentFitted()
  {
    if (this.mDesign.part == null)
      return;
    this.componentsSelected.text = this.mDesign.part.GetComponentsFittedCount().ToString();
    this.emptySlotsContainer.SetActive(this.mDesign.part.hasComponentSlotsAvailable);
    int numberOfSlots = this.mDesign.GetNumberOfSlots(this.mDesign.part.GetPartType());
    for (int index = 0; index < this.componentsWidget.Length; ++index)
    {
      UIComponentLevelWidget componentLevelWidget = this.componentsWidget[index];
      if (numberOfSlots > componentLevelWidget.level || !this.mDesign.part.hasComponentSlotsAvailable && numberOfSlots == componentLevelWidget.level)
        this.componentsWidget[index].Show();
      else
        this.componentsWidget[index].Hide();
    }
  }

  private void SetIcon(CarPart.PartType inType)
  {
    for (int index = 0; index < this.iconTransform.childCount; ++index)
    {
      if ((CarPart.PartType) index == inType)
        this.iconTransform.GetChild(index).gameObject.SetActive(true);
      else
        this.iconTransform.GetChild(index).gameObject.SetActive(false);
    }
  }

  public void ResetChoices()
  {
    for (int index = 0; index < this.componentsWidget.Length; ++index)
      this.componentsWidget[index].ResetChoices();
  }
}
