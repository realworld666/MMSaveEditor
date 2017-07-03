// Decompiled with JetBrains decompiler
// Type: ConditionNotification
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConditionNotification : MonoBehaviour
{
  public Color textColor = new Color();
  private List<CarPart> mPartList = new List<CarPart>();
  public TextMeshProUGUI counter;
  public ConditionHoverPanel hoverPanel;

  public void Display(List<CarPart> inPartList)
  {
    this.mPartList = inPartList;
    this.counter.text = this.mPartList.Count.ToString();
    this.gameObject.SetActive(true);
  }

  public void Hide()
  {
    this.gameObject.SetActive(false);
  }

  private void OpenHoverPanel()
  {
    this.hoverPanel.Open(this.mPartList, this.textColor);
  }

  private void CloseHoverPanel()
  {
    this.hoverPanel.Close();
  }
}
