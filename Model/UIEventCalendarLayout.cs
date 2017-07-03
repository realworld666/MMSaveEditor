// Decompiled with JetBrains decompiler
// Type: UIEventCalendarLayout
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIEventCalendarLayout : MonoBehaviour
{
  public UICircuitImage circuitImage;
  public UITrackLayout minimap;
  public Flag circuitFlag;
  public TextMeshProUGUI circuitName;
  public TextMeshProUGUI circuitVariation;
  public TextMeshProUGUI circuitDate;
  public TextMeshProUGUI circuitRound;
  public TextMeshProUGUI trackVariation;
  public Animator animator;
  public EventCalendarScreen screen;
  private RaceEventDetails mRaceEvent;
  private int mEventNumber;

  public void Setup(RaceEventDetails inRaceEvent, int inEventNumber)
  {
    if (inRaceEvent == null)
      return;
    if (this.mRaceEvent != inRaceEvent || this.mEventNumber != inEventNumber)
      this.PlayAnimation();
    this.mRaceEvent = inRaceEvent;
    this.mEventNumber = inEventNumber;
    this.SetDetails();
  }

  private void SetDetails()
  {
    this.circuitImage.SetCircuitIcon(this.mRaceEvent.circuit);
    this.minimap.SetCircuitIcon(this.mRaceEvent.circuit);
    this.circuitFlag.SetNationality(this.mRaceEvent.circuit.nationality);
    StringVariableParser.randomCircuit = this.mRaceEvent.circuit;
    this.circuitName.text = Localisation.LocaliseID("PSG_10010221", (GameObject) null);
    StringVariableParser.newTrackLayout = this.mRaceEvent.circuit.GetTrackVariation();
    this.circuitVariation.text = Localisation.LocaliseID("PSG_10010220", (GameObject) null);
    this.trackVariation.text = Localisation.LocaliseID("PSG_10010220", (GameObject) null);
    this.circuitDate.text = GameUtility.FormatDateTimesToLongDateRange(this.mRaceEvent.eventDate, this.mRaceEvent.eventDate.AddDays(2.0), string.Empty);
    StringVariableParser.intValue1 = this.mEventNumber + 1;
    StringVariableParser.intValue2 = this.screen.eventsCount;
    this.circuitRound.text = Localisation.LocaliseID("PSG_10002217", (GameObject) null);
  }

  private void PlayAnimation()
  {
    this.animator.Play(AnimationHashes.Show, 0, 0.0f);
  }
}
