// Decompiled with JetBrains decompiler
// Type: UIEventCalendarVariationEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIEventCalendarVariationEntry : MonoBehaviour
{
  public TextMeshProUGUI title;
  public TextMeshProUGUI trackVariation;
  public UITrackLayout layout;
  private Circuit mCircuit;

  public void Setup(Circuit inCircuit)
  {
    if (inCircuit == null)
      return;
    this.mCircuit = inCircuit;
    GameUtility.SetActive(this.title.gameObject, false);
    this.SetCircuitDetails();
  }

  public void SetupVote(Circuit inCircuit, string inLabel)
  {
    if (inCircuit == null)
      return;
    this.mCircuit = inCircuit;
    GameUtility.SetActive(this.title.gameObject, true);
    this.title.text = inLabel;
    this.SetCircuitDetails();
  }

  private void SetCircuitDetails()
  {
    this.trackVariation.text = Localisation.LocaliseID("PSG_10009983", (GameObject) null) + " " + this.mCircuit.GetTrackVariation();
    this.layout.SetCircuitIcon(this.mCircuit);
  }
}
