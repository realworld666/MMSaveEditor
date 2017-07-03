// Decompiled with JetBrains decompiler
// Type: UIPositionTrackerLap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIPositionTrackerLap : MonoBehaviour
{
  public UIPositionTrackerPlace[] places;
  public TextMeshProUGUI lapLabel;
  public GameObject selected;
  public GameObject finishLine;

  public void Setup(int inLap, int inCurrentLap, int inMaxLap)
  {
    this.lapLabel.text = inLap != 0 ? inLap.ToString() : Localisation.LocaliseID("PSG_10010669", (GameObject) null);
    GameUtility.SetActive(this.selected, inLap == inCurrentLap);
    GameUtility.SetActive(this.finishLine, inLap == inMaxLap);
    for (int index = 0; index < this.places.Length; ++index)
      this.places[index].SetLapIndex(inLap);
  }
}
