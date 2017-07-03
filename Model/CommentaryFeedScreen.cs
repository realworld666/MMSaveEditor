// Decompiled with JetBrains decompiler
// Type: CommentaryFeedScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommentaryFeedScreen : DataCenterScreen
{
  public UILoadList grid;
  public TextMeshProUGUI eventName;
  public Scrollbar scrollBar;
  private GameObject[] mGridItems;

  public override void OnStart()
  {
    base.OnStart();
    this.grid.OnScrollRect -= new Action(this.OnScrollRect);
    this.grid.OnScrollRect += new Action(this.OnScrollRect);
  }

  private void OnScrollRect()
  {
    this.UpdateGrid(false);
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.SetSessionType();
    this.grid.HideListItems();
    this.UpdateGrid(true);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
    this.scrollBar.onValueChanged.Invoke(1f);
  }

  public void SetSessionType()
  {
    SessionDetails.SessionType sessionType = Game.instance.sessionManager.eventDetails.currentSession.sessionType;
    StringVariableParser.intValue1 = Game.instance.player.team.championship.eventNumber + 1;
    switch (sessionType)
    {
      case SessionDetails.SessionType.Practice:
        this.eventName.text = Localisation.LocaliseID("PSG_10010600", (GameObject) null);
        break;
      case SessionDetails.SessionType.Qualifying:
        this.eventName.text = Localisation.LocaliseID("PSG_10010601", (GameObject) null);
        if (!Game.instance.sessionManager.eventDetails.hasSeveralQualifyingSessions)
          break;
        switch (Game.instance.sessionManager.eventDetails.currentSession.sessionNumberForUI)
        {
          case 1:
            return;
          case 2:
            return;
          case 3:
            return;
          default:
            return;
        }
      case SessionDetails.SessionType.Race:
        this.eventName.text = Localisation.LocaliseID("PSG_10010602", (GameObject) null);
        break;
    }
  }

  private void UpdateGrid(bool inForceUpdate = false)
  {
    List<Comment> commentList = new List<Comment>((IEnumerable<Comment>) Game.instance.sessionManager.commentaryManager.commentsHistory);
    if (!this.grid.SetSize(commentList.Count, inForceUpdate))
      return;
    this.mGridItems = this.grid.activatedItems;
    int length = this.mGridItems.Length;
    int firstActivatedIndex = this.grid.firstActivatedIndex;
    for (int index = 0; index < length; ++index)
    {
      UICommentaryFeedEntry component = this.mGridItems[index].GetComponent<UICommentaryFeedEntry>();
      Comment inComment = commentList[firstActivatedIndex];
      if (component.comment != inComment)
      {
        component.Setup(inComment);
        GameUtility.SetActive(component.gameObject, true);
      }
      ++firstActivatedIndex;
    }
  }
}
