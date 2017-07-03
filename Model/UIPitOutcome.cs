// Decompiled with JetBrains decompiler
// Type: UIPitOutcome
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPitOutcome : MonoBehaviour
{
  [SerializeField]
  private Image backing;
  [SerializeField]
  private TextMeshProUGUI label;

  public void SetOutcome(SessionSetupChange.Outcome inOutcome)
  {
    this.backing.color = this.GetOutcomeColor(inOutcome);
    this.label.text = Localisation.LocaliseEnum((Enum) inOutcome);
  }

  private Color GetOutcomeColor(SessionSetupChange.Outcome inOutcome)
  {
    switch (inOutcome)
    {
      case SessionSetupChange.Outcome.Bad:
        return UIConstants.pitStopOutcomeBad;
      case SessionSetupChange.Outcome.Good:
        return UIConstants.pitStopOutcomeGood;
      case SessionSetupChange.Outcome.Great:
        return UIConstants.pitStopOutcomeGreat;
      default:
        return UIConstants.pitStopOutcomeGood;
    }
  }
}
