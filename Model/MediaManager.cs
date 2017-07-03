using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class MediaManager : GenericManager<Person>
{
  private List<MediaOutlet> mMediaOutlets = new List<MediaOutlet>();
  private List<Tweet> mSessionTweets = new List<Tweet>();
  private MediaOutlet mPreviousOutletUsed;
  private MediaStory mPostPracticeStory;
  private MediaStory mPostQualifyingStory;
  private MediaStory mPostRaceStory;
    
  public enum TweetType
  {
    PreSessionTweet,
    PostSessionTweet,
    RandomFanTweet,
  }
}
