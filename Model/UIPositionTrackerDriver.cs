// Decompiled with JetBrains decompiler
// Type: UIPositionTrackerDriver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIPositionTrackerDriver : MonoBehaviour
{
  public TextMeshProUGUI driverName;

  public void Setup(Driver inDriver)
  {
    this.driverName.text = inDriver.shortName;
  }
}
