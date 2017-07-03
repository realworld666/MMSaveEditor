// Decompiled with JetBrains decompiler
// Type: ConditionHoverPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConditionHoverPanel : MonoBehaviour
{
  public TextMeshProUGUI text;

  private void Start()
  {
  }

  public void Open(List<CarPart> inPartList, Color inTextColor)
  {
    this.text.color = inTextColor;
    this.text.text = string.Empty;
    for (int index = 0; index < inPartList.Count; ++index)
    {
      TextMeshProUGUI text = this.text;
      string str = text.text + inPartList[index].GetPartName() + " Condition: " + (object) Mathf.Round(inPartList[index].partCondition.condition * 100f) + "% \n";
      text.text = str;
    }
    this.gameObject.SetActive(true);
  }

  public void Close()
  {
    this.gameObject.SetActive(false);
  }
}
