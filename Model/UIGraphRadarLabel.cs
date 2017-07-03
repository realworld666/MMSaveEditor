// Decompiled with JetBrains decompiler
// Type: UIGraphRadarLabel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIGraphRadarLabel : MonoBehaviour
{
  public TextMeshProUGUI label;
  public CanvasGroup canvasGroup;
  public bool isOn;

  private void Update()
  {
    if (this.isOn)
      this.canvasGroup.alpha = Mathf.MoveTowards(this.canvasGroup.alpha, 1f, GameTimer.deltaTime * 5f);
    else
      this.canvasGroup.alpha = Mathf.MoveTowards(this.canvasGroup.alpha, 0.0f, GameTimer.deltaTime * 5f);
  }

  public void SetText(string inText)
  {
    if (string.IsNullOrEmpty(inText) || !(this.label.text != inText))
      return;
    this.label.text = inText;
  }
}
