// Decompiled with JetBrains decompiler
// Type: StandingsScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StandingsScreen : UIScreen
{
  public Toggle[] championshipToggles = new Toggle[2];
  public UIStandingsOverviewWidget overviewWidget;
  public UIStandingsTeamsTableWidget teamsWidget;
  public UIStandingsDriversTableWidget driversWidget;
  public GameObject championshipToggleContainter;
  private ChampionshipStandings mStandings;
  private Championship mChampionship;

  public Championship championship
  {
    get
    {
      return this.mChampionship;
    }
  }

  public override void OnStart()
  {
    base.OnStart();
    this.overviewWidget.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.mStandings = (ChampionshipStandings) this.data;
    GameUtility.SetActive(this.championshipToggleContainter, Game.instance.championshipManager.isGTSeriesActive);
    if (this.mStandings == null)
    {
      this.mChampionship = Game.instance.player.team.championship;
      if (this.mChampionship is NullChampionship)
        this.mChampionship = Game.instance.championshipManager.GetMainChampionship(Championship.Series.SingleSeaterSeries);
      this.mStandings = this.mChampionship.standings;
      this.data = (Entity) this.mStandings;
    }
    else
      this.mChampionship = this.mStandings.championship;
    this.Refresh();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
    this.RefreshSeriesToggles();
  }

  private void RefreshSeriesToggles()
  {
    for (int index = 0; index < this.championshipToggles.Length; ++index)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      StandingsScreen.\u003CRefreshSeriesToggles\u003Ec__AnonStorey73 togglesCAnonStorey73 = new StandingsScreen.\u003CRefreshSeriesToggles\u003Ec__AnonStorey73();
      // ISSUE: reference to a compiler-generated field
      togglesCAnonStorey73.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      togglesCAnonStorey73.series = (Championship.Series) index;
      this.championshipToggles[index].onValueChanged.RemoveAllListeners();
      // ISSUE: reference to a compiler-generated field
      this.championshipToggles[index].isOn = togglesCAnonStorey73.series == this.mChampionship.series;
      // ISSUE: reference to a compiler-generated method
      this.championshipToggles[index].onValueChanged.AddListener(new UnityAction<bool>(togglesCAnonStorey73.\u003C\u003Em__FB));
    }
  }

  private void SetChampionshipCategory(bool inValue, Championship.Series series)
  {
    if (!inValue || series == this.mChampionship.series)
      return;
    Championship mainChampionship = Game.instance.championshipManager.GetMainChampionship(series);
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (mainChampionship != null)
    {
      this.mChampionship = mainChampionship;
      this.mStandings = this.mChampionship.standings;
      this.Refresh();
    }
    else
      this.RefreshSeriesToggles();
  }

  public void SetStandings(ChampionshipStandings inStandings)
  {
    if (inStandings == null)
      return;
    this.mStandings = inStandings;
    this.mChampionship = inStandings.championship;
    this.data = (Entity) this.mStandings;
    this.Refresh();
  }

  private void Refresh()
  {
    this.overviewWidget.Setup(this.mStandings);
    this.teamsWidget.Setup(this.mStandings);
    this.driversWidget.Setup(this.mStandings);
  }

  public override void OnExit()
  {
    base.OnExit();
    UIChampionshipRulesDialog.HideRollover();
  }
}
