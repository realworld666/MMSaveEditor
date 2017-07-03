// Decompiled with JetBrains decompiler
// Type: UITopBarObjectiveEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITopBarObjectiveEntry : MonoBehaviour
{
  public TextMeshProUGUI objectiveText;
  public Image statusBackground;
  public TextMeshProUGUI statusText;

  public void Setup(string inObjectiveText, string inStatusText, bool inIsPositiveColor)
  {
    this.objectiveText.text = inObjectiveText;
    this.statusText.text = inStatusText;
    if (inIsPositiveColor)
      this.statusBackground.color = UIConstants.positiveColor;
    else
      this.statusBackground.color = UIConstants.negativeColor;
  }
}
