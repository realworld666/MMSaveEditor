// Decompiled with JetBrains decompiler
// Type: EventCalendarScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class EventCalendarScreen : UIScreen
{
  private int mSelectedEvent = -1;
  public UIEventCalendarSelectionWidget selectionWidget;
  public UIEventCalendarOverviewWidget infoWidget;
  private Championship mChampionship;

  public Championship championship
  {
    get
    {
      return this.mChampionship;
    }
  }

  public int nextEvent
  {
    get
    {
      return this.mSelectedEvent;
    }
    set
    {
      this.mSelectedEvent = value;
    }
  }

  public int eventsCount
  {
    get
    {
      return this.mChampionship.calendar.Count;
    }
  }

  public static void ChangeScreen(Championship inChampionship, int inEventToShowIndex)
  {
    if (inChampionship == null)
      return;
    UIManager.instance.GetScreen<EventCalendarScreen>().SelectCircuit(inChampionship, inEventToShowIndex);
    if (UIManager.instance.IsScreenOpen("EventCalendarScreen"))
      UIManager.instance.RefreshCurrentPage((Entity) inChampionship);
    else
      UIManager.instance.ChangeScreen("EventCalendarScreen", (Entity) inChampionship, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }

  public override void OnStart()
  {
    base.OnStart();
    this.infoWidget.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    scSoundManager.BlockSoundEvents = true;
    this.showNavigationBars = true;
    this.mChampionship = (Championship) this.data;
    if (this.mChampionship == null)
    {
      this.mChampionship = Game.instance.player.team.championship;
      if (this.mChampionship is NullChampionship)
        this.mChampionship = Game.instance.championshipManager.GetMainChampionship(Championship.Series.SingleSeaterSeries);
    }
    this.selectionWidget.Setup(this.mChampionship);
    this.mSelectedEvent = -1;
    scSoundManager.BlockSoundEvents = false;
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
  }

  public override void OnExit()
  {
    base.OnExit();
    UIEventCalendarVariationsPopup.HidePopup();
  }

  public void SelectCircuit(Championship inChampionship, int inEventNumber)
  {
    if (inChampionship == null)
      return;
    this.mChampionship = inChampionship;
    this.mSelectedEvent = inEventNumber;
  }

  public void SelectCircuit(RaceEventDetails inRaceEvent, int inEventNumber)
  {
    if (inRaceEvent == null)
      return;
    this.infoWidget.Setup(inRaceEvent, inEventNumber);
  }
}
