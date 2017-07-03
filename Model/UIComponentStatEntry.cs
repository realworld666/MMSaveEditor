// Decompiled with JetBrains decompiler
// Type: UIComponentStatEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIComponentStatEntry : MonoBehaviour
{
  public TextMeshProUGUI statNameLabel;
  public TextMeshProUGUI statValueLabel;

  public void SetStat(string inName, string inValue, Color inColor)
  {
    this.statNameLabel.text = inName;
    this.statValueLabel.text = inValue;
    this.statValueLabel.color = inColor;
  }
}
