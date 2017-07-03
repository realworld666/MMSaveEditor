// Decompiled with JetBrains decompiler
// Type: MovieLocationScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class MovieLocationScreen : BaseMovieScreen
{
  public UIChampionshipLogo championshipLogo;
  public Flag flag;
  public TextMeshProUGUI eventNameLabel;
  public TextMeshProUGUI countryNameLabel;
  public TextMeshProUGUI roundLabel;

  public override void OnEnter()
  {
    base.OnEnter();
    Championship championship = Game.instance.player.team.championship;
    RaceEventDetails currentEventDetails = championship.GetCurrentEventDetails();
    this.championshipLogo.SetChampionship(championship);
    this.flag.SetNationality(currentEventDetails.circuit.nationality);
    StringVariableParser.randomCircuit = currentEventDetails.circuit;
    this.eventNameLabel.text = Localisation.LocaliseID("PSG_10010221", (GameObject) null);
    this.countryNameLabel.text = Localisation.LocaliseID(currentEventDetails.circuit.countryNameID, (GameObject) null);
    StringVariableParser.intValue1 = championship.eventNumberForUI;
    this.roundLabel.text = Localisation.LocaliseID("PSG_10010536", (GameObject) null);
    scMusicController.Transition();
    scSoundManager.Instance.PlaySound(SoundID.Video_TrackIntro, 0.0f);
  }
}
