// Decompiled with JetBrains decompiler
// Type: UISessionCommentaryWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISessionCommentaryWidget : UICommentaryFeedEntry
{
  public Animator animator;
  public Button commentaryScreenButton;
  private Comment mCurrentComment;
  private float mCommentDuration;

  private void Awake()
  {
    this.gameObject.AddComponent<UITextLinkHandler>();
  }

  public void OnStart()
  {
    this.commentaryScreenButton.onClick.AddListener(new UnityAction(this.OnCommentaryScreenButton));
  }

  private void OnCommentaryScreenButton()
  {
    UIManager.instance.ChangeScreen("CommentaryFeedScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  public void OnEnable()
  {
    this.RegisterToCommentaryManager();
    this.mCurrentComment = (Comment) null;
    GameUtility.SetActive(this.retiredContainer, false);
    GameUtility.SetActive(this.crashedContainer, false);
    GameUtility.SetActive(this.overtakenContainer, false);
    GameUtility.SetActive(this.overtookContainer, false);
    GameUtility.SetActive(this.penaltyContainer, false);
    GameUtility.SetActive(this.genericContainer, false);
    GameUtility.SetActive(this.purpleSectorContainer, false);
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

  private void OnDisable()
  {
    Game.instance.sessionManager.commentaryManager.onNewCommentAdded -= new Action<Comment>(this.OnNewComment);
  }

  private void RegisterToCommentaryManager()
  {
    Game.instance.sessionManager.commentaryManager.onNewCommentAdded += new Action<Comment>(this.OnNewComment);
  }

  private void OnNewComment(Comment inComment)
  {
    if (!this.gameObject.activeSelf || this.mCurrentComment != null && (double) this.mCurrentComment.commentPriority > (double) inComment.commentPriority)
      return;
    this.mCurrentComment = inComment;
    this.SetContainer(this.mCurrentComment);
    this.mCommentDuration = (float) (0.200000002980232 + (double) inComment.text.GetText().Length / (double) GameStatsConstants.averageCharactersReadPerSecond);
    this.animator.SetTrigger("Play");
    this.SetupCommentText(this.mCurrentComment);
  }

  private void Update()
  {
    if (this.mCurrentComment == null)
      return;
    if (!Game.instance.time.isPaused)
      this.mCommentDuration -= GameTimer.deltaTime;
    if ((double) this.mCommentDuration > 0.0)
      return;
    this.animator.SetTrigger("Close");
    this.mCurrentComment = (Comment) null;
  }
}
