// Decompiled with JetBrains decompiler
// Type: ScoutingScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;

public class ScoutingScreen : UIScreen
{
  public UIScoutingSearchResultsWidget searchWidget;
  public UIScoutingScoutWidget scoutWidget;

  public static void ChangeScreen(UIScoutingFilterJobRole.Filter inJobRole)
  {
    UIManager.instance.GetScreen<ScoutingScreen>().searchWidget.jobRole.SetFilter(inJobRole);
    UIManager.instance.ChangeScreen("ScoutingScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  public override void OnStart()
  {
    base.OnStart();
    this.searchWidget.OnStart();
    this.scoutWidget.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.searchWidget.Refresh();
    this.scoutWidget.Setup();
    this.showNavigationBars = true;
    this.RemoveNotifications();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
  }

  public override void OnExit()
  {
    base.OnExit();
    this.searchWidget.ClearGrid();
    this.searchWidget.HideTooltips();
  }

  public void Refresh()
  {
    this.searchWidget.RefreshScoutingStatus();
    this.scoutWidget.Refresh();
  }

  private void RemoveNotifications()
  {
    Game.instance.notificationManager.GetNotification("Scouting").ResetCount();
    Game.instance.notificationManager.GetNotification("ScoutingNewEntitiesToScout").ResetCount();
  }
}
