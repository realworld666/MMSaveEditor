// Decompiled with JetBrains decompiler
// Type: UISupplierSelectionWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISupplierSelectionWidget : MonoBehaviour
{
  private List<UISupplierSelectionStep> mCurrentSteps = new List<UISupplierSelectionStep>();
  public UISupplierSelectionStep engineSupplierStep;
  public UISupplierSelectionStep fuelSupplierStep;
  public UISupplierSelectionStep chassisMaterialSupplierStep;
  public UISupplierSelectionStep breakSupplierStep;
  public UISupplierSelectionStep batterySupplierStep;
  public UISupplierSelectionEntry engineSupplierEntry;
  public UISupplierSelectionEntry fuelSupplierEntry;
  public UISupplierSelectionEntry chassisMaterialSupplierEntry;
  public UISupplierSelectionEntry breakSupplierEntry;
  public UISupplierSelectionEntry batterySupplierEntry;
  public TextMeshProUGUI totalCostLabel;

  public void OnStart()
  {
    this.mCurrentSteps.Clear();
    this.engineSupplierStep.OnStart(this.engineSupplierEntry);
    this.mCurrentSteps.Add(this.engineSupplierStep);
    this.fuelSupplierStep.OnStart(this.fuelSupplierEntry);
    this.mCurrentSteps.Add(this.fuelSupplierStep);
    this.chassisMaterialSupplierStep.OnStart(this.chassisMaterialSupplierEntry);
    this.mCurrentSteps.Add(this.chassisMaterialSupplierStep);
    this.breakSupplierStep.OnStart(this.breakSupplierEntry);
    this.mCurrentSteps.Add(this.breakSupplierStep);
    this.batterySupplierStep.OnStart(this.batterySupplierEntry);
    this.mCurrentSteps.Add(this.batterySupplierStep);
  }

  public void OnEnter()
  {
    this.engineSupplierStep.OnEnter();
    this.fuelSupplierStep.OnEnter();
    this.breakSupplierStep.OnEnter();
    this.chassisMaterialSupplierStep.OnEnter();
    this.batterySupplierStep.OnEnter();
    this.engineSupplierEntry.OnEnter();
    this.fuelSupplierEntry.OnEnter();
    this.breakSupplierEntry.OnEnter();
    this.chassisMaterialSupplierEntry.OnEnter();
    this.batterySupplierEntry.OnEnter();
    this.UpdateTotalCost();
    this.engineSupplierStep.toggle.isOn = true;
    this.batterySupplierStep.gameObject.SetActive(Game.instance.player.team.championship.rules.isEnergySystemActive);
    this.batterySupplierStep.SetComplete(!Game.instance.player.team.championship.rules.isEnergySystemActive);
  }

  public bool IsComplete()
  {
    for (int index = 0; index < this.mCurrentSteps.Count; ++index)
    {
      if (!this.mCurrentSteps[index].isComplete)
        return false;
    }
    return true;
  }

  public void GoToNextStep()
  {
    int count = this.mCurrentSteps.Count;
    for (int index = 0; index < count; ++index)
    {
      UISupplierSelectionStep mCurrentStep = this.mCurrentSteps[index];
      if (!mCurrentStep.isComplete)
      {
        mCurrentStep.toggle.isOn = true;
        break;
      }
    }
  }

  public void UpdateTotalCost()
  {
    int num = 0;
    Team team = Game.instance.player.team;
    if (this.engineSupplierEntry.selectedSupplier != null)
      num += this.engineSupplierEntry.selectedSupplier.GetPrice(team);
    if (this.fuelSupplierEntry.selectedSupplier != null)
      num += this.fuelSupplierEntry.selectedSupplier.GetPrice(team);
    if (this.breakSupplierEntry.selectedSupplier != null)
      num += this.breakSupplierEntry.selectedSupplier.GetPrice(team);
    if (this.chassisMaterialSupplierEntry.selectedSupplier != null)
      num += this.chassisMaterialSupplierEntry.selectedSupplier.GetPrice(team);
    if (this.batterySupplierEntry.selectedSupplier != null)
      num += this.batterySupplierEntry.selectedSupplier.GetPrice(team);
    this.totalCostLabel.text = GameUtility.GetCurrencyString((long) num, 0);
  }

  public void NotifyStepComplete()
  {
    this.UpdateTotalCost();
  }
}
