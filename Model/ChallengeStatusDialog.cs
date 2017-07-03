// Decompiled with JetBrains decompiler
// Type: ChallengeStatusDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ChallengeStatusDialog : UIDialogBox
{
  public Transform firstObjectiveToggle;
  public Transform secondObjectiveToggle;
  public Transform optionalObjectiveToggle;
  private Championship mChampionship;

  protected override void OnEnable()
  {
    base.OnEnable();
    this.UnderDogsObjectives();
  }

  private void UnderDogsObjectives()
  {
    this.mChampionship = Game.instance.player.team.championship;
    ChampionshipEntry_v1 entry1 = this.mChampionship.standings.GetEntry((Entity) Game.instance.player.team.GetDriver(0));
    ChampionshipEntry_v1 entry2 = this.mChampionship.standings.GetEntry((Entity) Game.instance.player.team.GetDriver(1));
    ChampionshipEntry_v1 entry3 = this.mChampionship.standings.GetEntry((Entity) Game.instance.player.team);
    ChallengeStatusDialog.ChallengeState inState = this.mChampionship.eventCount - 1 != this.mChampionship.eventNumber || !this.mChampionship.GetCurrentEventDetails().hasEventEnded ? ChallengeStatusDialog.ChallengeState.Ongoing : ChallengeStatusDialog.ChallengeState.Final;
    if (entry1.HasWonChampionship() || entry2.HasWonChampionship())
      this.SetObjective(true, inState, this.firstObjectiveToggle);
    else
      this.SetObjective(false, inState, this.firstObjectiveToggle);
    this.SetObjective(entry3.HasWonChampionship(), inState, this.secondObjectiveToggle);
    if (entry1.HasWonChampionship() && entry3.HasWonChampionship() || entry2.HasWonChampionship() && entry3.HasWonChampionship())
      this.SetObjective(true, inState, this.optionalObjectiveToggle);
    else
      this.SetObjective(false, inState, this.optionalObjectiveToggle);
  }

  private void SetObjective(bool inResult, ChallengeStatusDialog.ChallengeState inState, Transform inObjectiveStateParent)
  {
    int num = 3;
    for (int index = 0; index < num; ++index)
      inObjectiveStateParent.GetChild(index).gameObject.SetActive(false);
    if (inResult)
      inObjectiveStateParent.GetChild(1).gameObject.SetActive(true);
    else if (inState == ChallengeStatusDialog.ChallengeState.Ongoing)
      inObjectiveStateParent.GetChild(2).gameObject.SetActive(true);
    else
      inObjectiveStateParent.GetChild(0).gameObject.SetActive(true);
  }

  public enum ChallengeState
  {
    Ongoing,
    Final,
  }
}
