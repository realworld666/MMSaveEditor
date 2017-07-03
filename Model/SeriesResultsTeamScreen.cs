// Decompiled with JetBrains decompiler
// Type: SeriesResultsTeamScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class SeriesResultsTeamScreen : UIScreen
{
  public UISeriesResultTeamInfoWidget dataWidget;
  public UIRacePodiumTrophyWidget sceneWidget;
  private Person mTeamPrincipal;

  public Person teamPrincipal
  {
    get
    {
      return this.mTeamPrincipal;
    }
  }

  public override void OnStart()
  {
    base.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionResults, 0.0f);
  }

  public void Setup(ChampionshipWinnersEntry inEntry, Championship inChampionship)
  {
    this.mTeamPrincipal = inEntry.teamsTeamPrincipal;
    this.dataWidget.Setup(inEntry, inChampionship);
    this.sceneWidget.Setup(inChampionship);
  }

  public void Setup(Trophy inTrophy)
  {
    this.dataWidget.Setup(inTrophy);
    this.sceneWidget.Setup(inTrophy);
  }

  public override void OnExit()
  {
    base.OnExit();
  }
}
