// Decompiled with JetBrains decompiler
// Type: UIMediaTweetEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMediaTweetEntry : MonoBehaviour
{
  public UICharacterPortrait portrait;
  public UICharacterPortrait portraitDriver;
  public TextMeshProUGUI tweetTitle;
  public TextMeshProUGUI tweetHashtag;
  public TextMeshProUGUI tweetTime;
  public TextMeshProUGUI tweetText;
  public TextMeshProUGUI tweeterName;
  public TextMeshProUGUI separatorText;
  public GameObject sessionSeparator;
  public Button closeButton;
  private Tweet mTweet;

  private void Awake()
  {
    this.closeButton.onClick.AddListener(new UnityAction(this.OnCloseButton));
  }

  private void OnCloseButton()
  {
    Game.instance.mediaManager.sessionTweets.Remove(this.mTweet);
    if (this.sessionSeparator.activeSelf)
      UIManager.instance.GetScreen<MediaScreen>().tweetsWidget.OnEnter();
    scSoundManager.Instance.PlaySound(SoundID.Button_Back, 0.0f);
    this.gameObject.SetActive(false);
  }

  public void Setup(Tweet inTweet)
  {
    this.gameObject.SetActive(true);
    this.mTweet = inTweet;
    if (this.mTweet.sender == null)
    {
      Debug.LogErrorFormat("Sender is null for rule {0}, verify the data.", (object) this.mTweet.storyRule.localisationID);
      this.mTweet.sender = (Person) Game.instance.player;
    }
    bool inIsActive = this.mTweet.sender is Driver;
    GameUtility.SetActive(this.portrait.gameObject, !inIsActive);
    GameUtility.SetActive(this.portraitDriver.gameObject, inIsActive);
    this.separatorText.text = Localisation.LocaliseEnum((Enum) this.mTweet.sessionType);
    if (inIsActive)
      this.portraitDriver.SetPortrait(this.mTweet.sender);
    else
      this.portrait.SetPortrait(this.mTweet.sender);
    MediaOutlet mediaOutlet = this.mTweet.sender.contract.GetMediaOutlet();
    if (mediaOutlet != null)
    {
      this.tweetHashtag.text = "@" + mediaOutlet.twitterHandle;
      this.tweetTitle.text = this.mTweet.sender.contract.GetMediaOutlet().name;
      this.tweeterName.text = this.mTweet.sender.name;
    }
    else
    {
      this.tweetHashtag.text = "@" + this.mTweet.sender.twitterHandle;
      this.tweetTitle.text = this.mTweet.sender.name;
      StringVariableParser.subject = inTweet.sender;
      this.tweeterName.text = Localisation.LocaliseEnum((Enum) inTweet.sender.contract.job);
      StringVariableParser.subject = (Person) null;
    }
    this.tweetTime.text = GameUtility.GetLocalisedDay(this.mTweet.storyDate);
    this.tweetText.text = this.mTweet.GetText();
  }
}
