// Decompiled with JetBrains decompiler
// Type: HomeScreenInfoPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreenInfoPanel : MonoBehaviour
{
  public Button button;
  public TextMeshProUGUI titleLabel;
  public TextMeshProUGUI descriptionLabel;
  public TextMeshProUGUI buttonLabel;
  public TextMeshProUGUI statusLabel;
  public float barValue;

  public virtual void OnStart()
  {
  }

  public virtual void Setup()
  {
  }

  public virtual bool isDefaultState()
  {
    return true;
  }

  public virtual float GetBarScore()
  {
    return (!this.isDefaultState() ? -1f : 1f) + this.barValue;
  }
}
