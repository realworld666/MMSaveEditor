// Decompiled with JetBrains decompiler
// Type: CarConditionEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarConditionEntry : MonoBehaviour
{
  public CarPart.PartType partTypeBaseGame = CarPart.PartType.None;
  public CarPart.PartType partTypeGT = CarPart.PartType.None;
  private Color mInitialColor = new Color();
  public UIPartConditionBar conditionBar;
  public Transform iconParent;
  public GameObject inCriticalState;
  public Image[] carPartGFX;
  public TextMeshProUGUI partNameLabel;
  public TextMeshProUGUI conditionLabel;
  private CarPart mPart;
  private bool mSetColor;

  private void SetInitialColor()
  {
    if (this.mSetColor)
      return;
    this.mSetColor = true;
    if (this.carPartGFX.Length <= 0)
      return;
    this.mInitialColor = this.carPartGFX[0].color;
  }

  private void SetPartRepairStatus(bool inValue)
  {
    this.mPart.partCondition.SetRepairInPit(inValue);
  }

  public void SetPart(CarPart inPart)
  {
    if (this.mPart != null || inPart == null)
      return;
    this.mPart = inPart;
    this.partNameLabel.text = this.mPart.GetPartName();
    this.UpdateConditionStats();
    CarPart.SetIcon(this.iconParent, this.mPart.GetPartType());
  }

  private void Update()
  {
    this.UpdateConditionStats();
  }

  private void UpdateConditionStats()
  {
    this.SetInitialColor();
    if (this.mPart == null)
      return;
    this.conditionBar.Setup(this.mPart);
    this.conditionLabel.text = this.mPart.partCondition.condition.ToString("P0", (IFormatProvider) Localisation.numberFormatter);
    this.inCriticalState.SetActive(this.mPart.partCondition.IsOnRed());
    if (this.carPartGFX.Length <= 0)
      return;
    Color color = this.mInitialColor;
    if ((double) this.mPart.partCondition.normalizedCondition < 0.5)
      color = UIConstants.conditionCarPartYellow;
    if (this.mPart.partCondition.IsOnRed())
      color = UIConstants.conditionCarPartRed;
    for (int index = 0; index < this.carPartGFX.Length; ++index)
      this.carPartGFX[index].color = color;
  }
}
