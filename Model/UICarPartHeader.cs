// Decompiled with JetBrains decompiler
// Type: UICarPartHeader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class UICarPartHeader : MonoBehaviour
{
  public TextMeshProUGUI[] driverNames = new TextMeshProUGUI[0];
  public TextMeshProUGUI partType;

  public void Setup(CarPart.PartType inType)
  {
    this.gameObject.SetActive(true);
    if ((UnityEngine.Object) this.partType != (UnityEngine.Object) null)
      this.partType.text = Localisation.LocaliseEnum((Enum) inType);
    for (int inIndex = 0; inIndex < this.driverNames.Length; ++inIndex)
      this.driverNames[inIndex].text = Game.instance.player.team.GetDriver(inIndex).lastName;
  }
}
