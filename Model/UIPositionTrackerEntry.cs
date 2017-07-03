// Decompiled with JetBrains decompiler
// Type: UIPositionTrackerEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPositionTrackerEntry : MonoBehaviour
{
  public int index;
  public Flag flag;
  public UICharacterPortrait driverPortrait;
  public TextMeshProUGUI driverName;
  public TextMeshProUGUI teamName;
  public Toggle toggle;
  public Image toggleGFX;
  private UILineGraph mGraph;

  public void SetInfo(Driver inDriver, UILineGraph inGraph)
  {
    this.mGraph = inGraph;
  }

  public void ToggleGraphLine(bool value)
  {
    this.mGraph.SetGraphVisibility(this.index, value);
  }
}
