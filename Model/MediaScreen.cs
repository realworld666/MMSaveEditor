// Decompiled with JetBrains decompiler
// Type: MediaScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class MediaScreen : UIScreen
{
  public UIMediaReport mediaReport;
  public UIMediaTweetsWidget tweetsWidget;

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    Game.instance.sessionManager.SetCircuitActive(true);
    MediaStory storyForSession = Game.instance.mediaManager.GetStoryForSession(Game.instance.sessionManager.eventDetails);
    if (storyForSession == null)
    {
      this.mediaReport.gameObject.SetActive(false);
    }
    else
    {
      this.mediaReport.gameObject.SetActive(true);
      this.mediaReport.SetStory(storyForSession);
    }
    this.tweetsWidget.OnEnter();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionPressReaction, 0.0f);
  }

  public bool IsPlayerTeamInPodium()
  {
    int num = 3;
    for (int inIndex = 0; inIndex < num; ++inIndex)
    {
      if (Game.instance.player.team.championship.standings.GetDriverEntry(inIndex).GetEntity<Person>().contract.GetTeam() == Game.instance.player.team)
        return true;
    }
    return false;
  }
}
