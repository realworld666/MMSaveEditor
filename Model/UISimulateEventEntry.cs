// Decompiled with JetBrains decompiler
// Type: UISimulateEventEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UISimulateEventEntry : MonoBehaviour
{
  public TextMeshProUGUI circuitNameLabel;
  public TextMeshProUGUI eventNumberLabel;
  public Flag circuitFlag;
  public UICircuitImage circuitImage;
  public UIChampionshipLogo championshipLogo;
  private Championship mChampionship;
  private RaceEventDetails mEventDetails;
  private int mEventNumber;

  public void Setup(Championship inChampionship)
  {
    this.mChampionship = inChampionship;
    this.mEventDetails = !this.mChampionship.HasSeasonEnded() ? this.mChampionship.GetPreviousEventDetails() : this.mChampionship.GetCurrentEventDetails();
    this.mEventNumber = !this.mChampionship.HasSeasonEnded() ? this.mChampionship.eventNumber : this.mChampionship.eventNumberForUI;
    this.circuitNameLabel.text = Localisation.LocaliseID(this.mEventDetails.circuit.locationNameID, (GameObject) null);
    StringVariableParser.intValue1 = this.mEventNumber;
    StringVariableParser.intValue2 = this.mChampionship.eventCount;
    this.eventNumberLabel.text = Localisation.LocaliseID("PSG_10002217", (GameObject) null);
    this.circuitFlag.SetNationality(this.mEventDetails.circuit.nationality);
    this.circuitImage.SetCircuitIcon(this.mEventDetails.circuit);
    this.championshipLogo.SetChampionship(this.mChampionship);
  }
}
