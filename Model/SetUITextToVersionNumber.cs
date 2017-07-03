// Decompiled with JetBrains decompiler
// Type: MM2.SetUITextToVersionNumber
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MM2
{
  public class SetUITextToVersionNumber : MonoBehaviour
  {
    [SerializeField]
    private string prefix = string.Empty;

    private void Awake()
    {
      Text component1 = this.GetComponent<Text>();
      if ((Object) component1 != (Object) null)
        component1.text = this.prefix + (object) GameVersionNumber.version;
      TextMeshProUGUI component2 = this.GetComponent<TextMeshProUGUI>();
      if (!((Object) component2 != (Object) null))
        return;
      component2.text = this.prefix + (object) GameVersionNumber.version;
    }
  }
}
