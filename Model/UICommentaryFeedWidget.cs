// Decompiled with JetBrains decompiler
// Type: UICommentaryFeedWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class UICommentaryFeedWidget : MonoBehaviour
{
  public UIGridList grid;
  public TextMeshProUGUI eventName;
  public GameObject highlightBacking;
  private int mIndex;

  public void Setup(bool resetList)
  {
    if (resetList)
    {
      this.mIndex = 0;
      this.grid.HideListItems();
    }
    this.SetSessionType();
    Game.instance.sessionManager.commentaryManager.onNewCommentAdded += new Action<Comment>(this.OnNewComment);
    GameUtility.SetActive(this.highlightBacking, this.mIndex != 0);
  }

  private void OnDisable()
  {
    Game.instance.sessionManager.commentaryManager.onNewCommentAdded -= new Action<Comment>(this.OnNewComment);
  }

  private void OnDestroy()
  {
    if (!Game.IsActive())
      return;
    CommentaryManager commentaryManager = Game.instance.sessionManager.commentaryManager;
    if (commentaryManager == null)
      return;
    commentaryManager.onNewCommentAdded -= new Action<Comment>(this.OnNewComment);
  }

  private void OnNewComment(Comment comment)
  {
    if (!this.gameObject.activeSelf)
      return;
    UICommentaryFeedEntry commentaryFeedEntry = this.grid.GetOrCreateItem<UICommentaryFeedEntry>(this.mIndex);
    commentaryFeedEntry.Setup(comment);
    commentaryFeedEntry.transform.SetAsFirstSibling();
    GameUtility.SetActive(commentaryFeedEntry.gameObject, true);
    ++this.mIndex;
    GameUtility.SetActive(this.highlightBacking, this.mIndex != 0);
  }

  private void SetSessionType()
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
}
