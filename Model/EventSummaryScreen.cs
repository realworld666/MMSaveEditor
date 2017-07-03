// Decompiled with JetBrains decompiler
// Type: EventSummaryScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class EventSummaryScreen : UIScreen
{
  public GameObject[] championshipTitles;
  [SerializeField]
  private UIChampionshipLogo championshipLogo;
  [SerializeField]
  private Flag eventFlag;
  [SerializeField]
  private TextMeshProUGUI eventNameLabel;
  [SerializeField]
  private TextMeshProUGUI eventRoundLabel;
  [SerializeField]
  private UICircuitImage circuitImage;
  [SerializeField]
  private UIRacePodiumDriverEntry[] podiumEntry;
  [SerializeField]
  private MiniStandingsWidget standingsWidget;
  [SerializeField]
  private UIGridList resultsGrid;
  private Championship[] mChampionships;
  private Championship mSelectedChampionship;
  private int mChampionshipNum;

  public bool isComplete
  {
    get
    {
      return this.mChampionshipNum + 1 >= this.mChampionships.Length;
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
    this.mChampionships = Game.instance.championshipManager.GetChampionshipsRacingToday(false, Game.instance.player.GetChampionshipSeries());
    this.mSelectedChampionship = !(this.data is Championship) ? this.mChampionships[0] : (Championship) this.data;
    this.Setup(this.mSelectedChampionship);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionScreen, 0.0f);
  }

  public override void OnExit()
  {
    base.OnExit();
    this.standingsWidget.OnExit();
    this.resultsGrid.DestroyListItems();
  }

  private void Setup(Championship inChampionship)
  {
    for (int index = 0; index < this.mChampionships.Length; ++index)
    {
      if (this.mChampionships[index] == this.mSelectedChampionship)
        this.mChampionshipNum = index;
    }
    RaceEventDetails raceEventDetails = (!inChampionship.HasSeasonEnded() ? inChampionship.GetPreviousEventDetails() : inChampionship.GetCurrentEventDetails()) ?? inChampionship.GetCurrentEventDetails();
    RaceEventResults results = raceEventDetails.results;
    int num = !inChampionship.HasSeasonEnded() ? inChampionship.eventNumber : inChampionship.eventNumberForUI;
    for (int index = 0; index < this.championshipTitles.Length; ++index)
      GameUtility.SetActive(this.championshipTitles[index], index == this.mSelectedChampionship.championshipID);
    this.championshipLogo.SetChampionship(inChampionship);
    this.eventNameLabel.text = Localisation.LocaliseID(raceEventDetails.circuit.locationNameID, (GameObject) null);
    StringVariableParser.intValue1 = num;
    StringVariableParser.intValue2 = inChampionship.eventCount;
    this.eventRoundLabel.text = Localisation.LocaliseID("PSG_10002217", (GameObject) null);
    this.eventFlag.SetNationality(raceEventDetails.circuit.nationality);
    this.circuitImage.SetCircuitIcon(raceEventDetails.circuit);
    for (int index = 0; index < this.podiumEntry.Length; ++index)
    {
      RaceEventResults.ResultData inResultData = results.GetResultsForSession(SessionDetails.SessionType.Race).resultData[index];
      this.podiumEntry[index].SetResultData(inResultData);
    }
    this.standingsWidget.OnEnter(inChampionship);
    int count = results.GetResultsForSession(SessionDetails.SessionType.Race).resultData.Count;
    for (int index = 0; index < count; ++index)
      this.resultsGrid.CreateListItem<MiniRaceResultDriver>().SetResultData(results.GetResultsForSession(SessionDetails.SessionType.Race).resultData[index]);
    this.resultsGrid.ResetScrollbar();
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    if (this.mChampionshipNum + 1 >= this.mChampionships.Length)
      return UIScreen.NavigationButtonEvent.LetGameStateHandle;
    ++this.mChampionshipNum;
    UIManager.instance.ChangeScreen("EventSummaryScreen", (Entity) this.mChampionships[this.mChampionshipNum], UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }
}
