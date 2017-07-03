// Decompiled with JetBrains decompiler
// Type: NavigationButtonEditorCode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class NavigationButtonEditorCode : MonoBehaviour
{
  private void Start()
  {
    this.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
  }
}
