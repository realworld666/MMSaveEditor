// Decompiled with JetBrains decompiler
// Type: UIMediaTweetsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIMediaTweetsWidget : MonoBehaviour
{
  public UIGridList list;
  public GameObject noTweets;
  private bool mRecreateList;

  public void OnEnter()
  {
    this.CreateTweetList();
  }

  private void Update()
  {
    if (!this.mRecreateList)
      return;
    this.mRecreateList = false;
    this.list.HideListItems();
    int count = Game.instance.mediaManager.sessionTweets.Count;
    for (int inIndex = 0; inIndex < count; ++inIndex)
    {
      Tweet tweet = inIndex - 1 < 0 ? (Tweet) null : Game.instance.mediaManager.sessionTweets[inIndex - 1];
      Tweet sessionTweet = Game.instance.mediaManager.sessionTweets[inIndex];
      UIMediaTweetEntry uiMediaTweetEntry = this.list.GetOrCreateItem<UIMediaTweetEntry>(inIndex);
      uiMediaTweetEntry.Setup(sessionTweet);
      if (tweet != null && tweet.storyDate.DayOfWeek != sessionTweet.storyDate.DayOfWeek || inIndex == 0)
        GameUtility.SetActive(uiMediaTweetEntry.sessionSeparator, true);
      else
        GameUtility.SetActive(uiMediaTweetEntry.sessionSeparator, false);
    }
    GameUtility.SetActive(this.noTweets.gameObject, this.list.itemCount == 0);
  }

  private void CreateTweetList()
  {
    this.mRecreateList = true;
  }
}
