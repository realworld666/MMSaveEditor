// Decompiled with JetBrains decompiler
// Type: UITravelDestinationWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UITravelDestinationWidget : UITravelStepOption
{
  public Flag circuitFlag;
  public TextMeshProUGUI circuitTitle;
  public TextMeshProUGUI eventRound;
  public TextMeshProUGUI eventDate;
  public TextMeshProUGUI travelCost;

  public override void Setup()
  {
    Championship championship = Game.instance.player.team.championship;
    RaceEventDetails currentEventDetails = championship.GetCurrentEventDetails();
    this.circuitFlag.SetNationality(currentEventDetails.circuit.nationality);
    this.circuitTitle.text = Localisation.LocaliseID(currentEventDetails.circuit.locationNameID, (GameObject) null);
    this.eventDate.text = Game.instance.time.now.ToLongDateString();
    StringVariableParser.intValue1 = championship.eventNumberForUI;
    StringVariableParser.intValue2 = championship.calendar.Count;
    this.eventRound.text = Localisation.LocaliseID("PSG_10002217", (GameObject) null);
  }
}
