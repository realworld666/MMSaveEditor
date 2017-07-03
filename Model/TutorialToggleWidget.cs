// Decompiled with JetBrains decompiler
// Type: TutorialToggleWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialToggleWidget : MonoBehaviour
{
  public TextMeshProUGUI name;
  public Toggle toggle;
  public GameObject tick;

  public void Setup(string inName)
  {
    this.name.text = inName;
  }

  public void Tick(bool inTickValue)
  {
    this.tick.gameObject.SetActive(inTickValue);
  }
}
