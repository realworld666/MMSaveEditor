// Decompiled with JetBrains decompiler
// Type: UIHUBTweets
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHUBTweets : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
  public float letterCooldown = 0.1f;
  private List<Tweet> mTweets = new List<Tweet>();
  private float mLetterCooldown = 1f;
  private float mChangeCooldown = 5f;
  public Button tweetLeftButton;
  public Button tweetRightButton;
  public CanvasGroup canvasGroup;
  public UICharacterPortrait journalistPortrait;
  public TextMeshProUGUI journalistName;
  public TextMeshProUGUI tweetCompany;
  public TextMeshProUGUI tweetHashTag;
  public TextMeshProUGUI tweetDescription;
  public TextMeshProUGUI tweetControlLabel;
  public SessionHUBScreen screen;
  private MediaManager mMediaManager;
  private int mCurrentTweet;
  private int mMaxTweet;
  private float mCooldown;
  private float mCooldownAuto;
  private bool mMouseHover;

  public void OnStart()
  {
    this.tweetLeftButton.onClick.AddListener((UnityAction) (() => this.OnChangeTweet(-1)));
    this.tweetRightButton.onClick.AddListener((UnityAction) (() => this.OnChangeTweet(1)));
  }

  public void Setup()
  {
    this.mMediaManager = Game.instance.mediaManager;
    this.mTweets = this.mMediaManager.GetTweetsForSessionType(this.screen.sessionType);
    this.mCurrentTweet = 0;
    this.mMaxTweet = this.mTweets.Count - 1;
    this.canvasGroup.alpha = this.mMaxTweet != -1 ? 1f : 0.0f;
    this.canvasGroup.interactable = this.mMaxTweet >= 0;
    if (this.mMaxTweet >= 0)
      this.SetTweet(this.mTweets[0]);
    this.mMouseHover = false;
  }

  private void Update()
  {
    this.mLetterCooldown -= GameTimer.deltaTime;
    if ((double) this.mLetterCooldown < 0.0)
    {
      ++this.tweetDescription.maxVisibleCharacters;
      this.mLetterCooldown = this.letterCooldown;
    }
    if (this.mMouseHover || this.mMaxTweet < 0)
      return;
    if ((double) this.mCooldown > 0.0)
      this.mCooldown = Mathf.Max(this.mCooldownAuto - GameTimer.deltaTime, 0.0f);
    if ((double) this.mCooldownAuto > 0.0)
      this.mCooldownAuto = Mathf.Max(this.mCooldownAuto - GameTimer.deltaTime, 0.0f);
    else
      this.OnChangeTweet(1);
  }

  public void SetTweet(Tweet inTweet)
  {
    this.tweetDescription.maxVisibleCharacters = 0;
    this.journalistPortrait.SetPortrait(inTweet.sender);
    MediaOutlet mediaOutlet = inTweet.sender.contract.GetMediaOutlet();
    if (mediaOutlet != null)
    {
      this.tweetHashTag.text = "@" + mediaOutlet.twitterHandle;
      this.tweetCompany.text = mediaOutlet.name;
      this.journalistName.text = inTweet.sender.name;
    }
    else
    {
      this.tweetHashTag.text = "@" + inTweet.sender.twitterHandle;
      this.tweetCompany.text = inTweet.sender.name;
      this.journalistName.text = Localisation.LocaliseEnum((Enum) inTweet.sender.contract.job);
    }
    this.tweetDescription.text = inTweet.GetText();
    this.tweetControlLabel.text = (this.mCurrentTweet + 1).ToString() + "/" + (this.mMaxTweet + 1).ToString();
  }

  public void OnNewTweet(Tweet inTweet)
  {
    if (inTweet.sessionType != this.screen.sessionType)
      return;
    this.mTweets.Add(inTweet);
    this.mMaxTweet = this.mTweets.Count - 1;
    if (this.mMaxTweet >= 0)
    {
      this.canvasGroup.alpha = 1f;
      this.canvasGroup.interactable = true;
    }
    this.SetTweet(inTweet);
    this.UpdateButtonsState();
    this.mCooldownAuto = this.mChangeCooldown * 2f;
  }

  private void OnChangeTweet(int inDirection)
  {
    this.mCurrentTweet += inDirection;
    if (this.mCurrentTweet < 0 || this.mCurrentTweet > this.mMaxTweet)
      this.mCurrentTweet = this.mCurrentTweet >= 0 ? 0 : this.mMaxTweet;
    this.SetTweet(this.mTweets[this.mCurrentTweet]);
    this.mCooldownAuto = this.mChangeCooldown;
  }

  private void UpdateButtonsState()
  {
    this.tweetRightButton.interactable = this.mMaxTweet > 0 && (double) this.mCooldown <= 0.0;
    this.tweetLeftButton.interactable = this.mMaxTweet > 0 && (double) this.mCooldown <= 0.0;
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    this.mMouseHover = true;
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    this.mMouseHover = false;
    this.mCooldownAuto = this.mChangeCooldown;
  }
}
