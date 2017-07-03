// Decompiled with JetBrains decompiler
// Type: UICommentaryFeedEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UICommentaryFeedEntry : MonoBehaviour
{
  public TextMeshProUGUI[] commentText;
  public GameObject genericContainer;
  public GameObject retiredContainer;
  public GameObject crashedContainer;
  public GameObject overtakenContainer;
  public GameObject overtookContainer;
  public GameObject penaltyContainer;
  public GameObject purpleSectorContainer;
  public GameObject blueFlagContainer;
  public TextMeshProUGUI lapTimeHeading;
  public TextMeshProUGUI lapTimeBody;
  private Comment mComment;

  public Comment comment
  {
    get
    {
      return this.mComment;
    }
  }

  public void Setup(Comment inComment)
  {
    if (inComment == null || this.mComment == inComment)
      return;
    this.mComment = inComment;
    this.SetupCommentLapTime();
    this.SetContainer(this.mComment);
    this.SetupCommentText(this.mComment);
  }

  private void SetupCommentLapTime()
  {
    if (Game.instance.sessionManager.endCondition == SessionManager.EndCondition.LapCount)
      this.lapTimeHeading.text = Localisation.LocaliseID("PSG_10000434", (GameObject) null);
    else if (Game.instance.sessionManager.endCondition == SessionManager.EndCondition.Time)
      this.lapTimeHeading.text = Localisation.LocaliseID("PSG_10003466", (GameObject) null);
    this.lapTimeBody.text = this.mComment.timeOrLapOfTheComment;
  }

  public void SetupCommentText(Comment inComment)
  {
    string inString = inComment.text.GetText();
    if (UIManager.instance.currentScreen is CommentaryFeedScreen)
      inString = GameUtility.RemoveUnderlineTags(inString);
    for (int index = 0; index < this.commentText.Length; ++index)
      this.commentText[index].text = inString;
  }

  public void SetContainer(Comment inComment)
  {
    GameUtility.SetActive(this.retiredContainer, inComment.commentType == Comment.CommentType.Retirement);
    GameUtility.SetActive(this.crashedContainer, inComment.commentType == Comment.CommentType.Crashes);
    GameUtility.SetActive(this.overtakenContainer, inComment.commentType == Comment.CommentType.DriverDropsPosition);
    GameUtility.SetActive(this.overtookContainer, inComment.commentType == Comment.CommentType.Overtakes);
    GameUtility.SetActive(this.penaltyContainer, inComment.commentType == Comment.CommentType.PenaltyDriveThrough);
    GameUtility.SetActive(this.purpleSectorContainer, inComment.commentType == Comment.CommentType.NewPurpleSector || inComment.commentType == Comment.CommentType.NewDriverFastestLap);
    GameUtility.SetActive(this.blueFlagContainer, inComment.commentType == Comment.CommentType.BlueFlags);
    GameUtility.SetActive(this.genericContainer, this.ActivateGeneric());
    this.PlaySound(inComment);
  }

  private void PlaySound(Comment inComment)
  {
    if (UIManager.instance.currentScreen is CommentaryFeedScreen)
      return;
    if (this.ActivateGeneric() || inComment.commentType == Comment.CommentType.PenaltyDriveThrough || inComment.commentType == Comment.CommentType.BlueFlags)
      scSoundManager.Instance.PlaySound(SoundID.Sfx_CommentaryFeed_Normal, 0.0f);
    else if (inComment.commentType == Comment.CommentType.Retirement || inComment.commentType == Comment.CommentType.Crashes || inComment.commentType == Comment.CommentType.DriverDropsPosition)
    {
      if (inComment.commentType == Comment.CommentType.Crashes)
        return;
      scSoundManager.Instance.PlaySound(SoundID.Sfx_CommentaryFeed_Red, 0.0f);
    }
    else
    {
      if (inComment.commentType != Comment.CommentType.Overtakes && inComment.commentType != Comment.CommentType.NewPurpleSector && inComment.commentType != Comment.CommentType.NewDriverFastestLap)
        return;
      scSoundManager.Instance.PlaySound(SoundID.Sfx_CommentaryFeed_Green, 0.0f);
    }
  }

  public bool ActivateGeneric()
  {
    if (!this.retiredContainer.activeSelf && !this.crashedContainer.activeSelf && (!this.overtakenContainer.activeSelf && !this.overtookContainer.activeSelf) && (!this.penaltyContainer.activeSelf && !this.purpleSectorContainer.activeSelf))
      return !this.blueFlagContainer.activeSelf;
    return false;
  }
}
