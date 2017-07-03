// Decompiled with JetBrains decompiler
// Type: UICarPartEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICarPartEntry : MonoBehaviour
{
  public Transform iconParent;
  public Image levelBacking;
  public TextMeshProUGUI partLevel;
  public TextMeshProUGUI partName;

  public void Setup(CarPart inPart)
  {
    this.partLevel.text = inPart.GetLevelUIString();
    this.partName.text = Localisation.LocaliseEnum((Enum) inPart.GetPartType());
    this.levelBacking.color = UIConstants.GetPartLevelColor(inPart.stats.level);
    for (int index = 0; index < this.iconParent.childCount; ++index)
    {
      if ((CarPart.PartType) index == inPart.GetPartType())
        this.iconParent.GetChild(index).gameObject.SetActive(true);
      else
        this.iconParent.GetChild(index).gameObject.SetActive(false);
    }
  }
}
