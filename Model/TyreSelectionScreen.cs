// Decompiled with JetBrains decompiler
// Type: TyreSelectionScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class TyreSelectionScreen : DataCenterScreen
{
  public UIGridList grid;

  public override void OnEnter()
  {
    base.OnEnter();
    this.SetGrid();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
  }

  public void SetGrid()
  {
    Championship championship = Game.instance.player.team.championship;
    int teamEntryCount = championship.standings.teamEntryCount;
    for (int inIndex = 0; inIndex < teamEntryCount; ++inIndex)
      this.grid.GetOrCreateItem<UITyreSelectionEntry>(inIndex).Setup(championship.standings.GetTeamEntry(inIndex).GetEntity<Team>());
  }
}
