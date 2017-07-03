// Decompiled with JetBrains decompiler
// Type: UIPanelCarStarsStatEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIPanelCarStarsStatEntry : MonoBehaviour
{
  public GameObject[] stars = new GameObject[0];
  public TextMeshProUGUI statName;

  public void Setup(string inStatName, int inStars)
  {
    this.statName.text = inStatName;
    for (int index = 0; index < this.stars.Length; ++index)
    {
      if (index <= inStars)
        this.stars[index].SetActive(true);
      else
        this.stars[index].SetActive(false);
    }
  }
}
