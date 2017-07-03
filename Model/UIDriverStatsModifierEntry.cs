// Decompiled with JetBrains decompiler
// Type: UIDriverStatsModifierEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Text;
using TMPro;
using UnityEngine;

public class UIDriverStatsModifierEntry : MonoBehaviour
{
  public TextMeshProUGUI modifierType;
  public TextMeshProUGUI modifierAmount;

  public void Setup(string inModifierName, PersonalityTrait inPersonalityTrait, PersonalityTrait.StatModified inStatModified)
  {
    this.Setup(inPersonalityTrait, inStatModified);
    this.modifierType.text = inModifierName;
  }

  public void Setup(PersonalityTrait inPersonalityTrait, PersonalityTrait.StatModified inStatModified)
  {
    this.modifierType.text = inPersonalityTrait.name;
    if ((double) inPersonalityTrait.GetSingleModifierForStat(inStatModified) > 0.0)
      this.modifierAmount.color = UIConstants.colorBandGreen;
    else
      this.modifierAmount.color = UIConstants.colorBandRed;
    this.modifierAmount.text = inPersonalityTrait.GetSingleModifierForStatText(inStatModified);
  }

  public void Setup(string inModifierName, string inModifierAmount)
  {
    this.modifierType.text = inModifierName;
    this.modifierAmount.text = inModifierAmount;
  }

  public void Setup(string inModifierName, float inModifierAmount)
  {
    this.modifierType.text = inModifierName;
    string empty = string.Empty;
    using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
    {
      StringBuilder stringBuilder = builderSafe.stringBuilder;
      if ((double) inModifierAmount > 0.0)
        stringBuilder.Append("+");
      stringBuilder.Append(inModifierAmount.ToString((IFormatProvider) Localisation.numberFormatter));
      empty = stringBuilder.ToString();
    }
    if ((double) inModifierAmount > 0.0)
      this.modifierAmount.color = UIConstants.colorBandGreen;
    else
      this.modifierAmount.color = UIConstants.colorBandRed;
    this.modifierAmount.text = empty;
  }
}
