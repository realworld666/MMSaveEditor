// Decompiled with JetBrains decompiler
// Type: UIBarGraph
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBarGraph : MonoBehaviour
{
  private List<GameObject> mBars = new List<GameObject>();

  public void AddBar(float inPercentageValue, string inLabelName, Color inColor)
  {
    GameObject gameObject = Object.Instantiate<GameObject>(this.transform.FindChild("Bars").FindChild("Bar").gameObject);
    gameObject.transform.SetParent(this.transform.FindChild("Bars"), false);
    gameObject.SetActive(true);
    gameObject.GetComponent<Image>().fillAmount = inPercentageValue;
    gameObject.GetComponent<Image>().color = inColor;
    gameObject.transform.FindChild("Label").GetComponent<TextMeshProUGUI>().text = inLabelName;
    gameObject.transform.FindChild("Label").GetComponent<TextMeshProUGUI>().color = inColor;
    this.mBars.Add(gameObject);
  }

  public void DestroyBars()
  {
    for (int index = 0; index < this.mBars.Count; ++index)
      Object.Destroy((Object) this.mBars[index]);
  }
}
