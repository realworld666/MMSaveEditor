// Decompiled with JetBrains decompiler
// Type: UIGraphTooltip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class UIGraphTooltip : MonoBehaviour
{
  public TextMeshProUGUI targetTextLabel;
  public bool isOn;
  private TextMeshProUGUI mTextLabel;
  private CanvasGroup mGroup;

  private void Start()
  {
    this.mTextLabel = this.gameObject.GetComponent<TextMeshProUGUI>();
    this.mGroup = this.gameObject.GetComponent<CanvasGroup>();
  }

  private void Update()
  {
    if (this.isOn)
    {
      this.mGroup.alpha = Mathf.MoveTowards(this.mGroup.alpha, 1f, GameTimer.deltaTime * 5f);
    }
    else
    {
      this.mGroup.alpha = Mathf.MoveTowards(this.mGroup.alpha, 0.0f, GameTimer.deltaTime * 5f);
      if ((double) this.mGroup.alpha != 0.0)
        return;
      this.transform.localPosition = new Vector3(9999f, 9999f, 0.0f);
    }
  }

  public void SetText(string inText)
  {
    this.mTextLabel.text = inText;
    this.targetTextLabel.text = this.mTextLabel.text;
  }
}
