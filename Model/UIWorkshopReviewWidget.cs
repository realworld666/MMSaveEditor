// Decompiled with JetBrains decompiler
// Type: UIWorkshopReviewWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using ModdingSystem;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIWorkshopReviewWidget : MonoBehaviour
{
  public GameObject[] stars;
  public TextMeshProUGUI reviewText;
  [SerializeField]
  private Toggle thumbsUp;
  [SerializeField]
  private Toggle thumbsDown;
  private SteamMod mSteamMod;
  private bool mHasVoteButtons;

  private void Start()
  {
    this.mHasVoteButtons = (UnityEngine.Object) this.thumbsUp != (UnityEngine.Object) null && (UnityEngine.Object) this.thumbsDown != (UnityEngine.Object) null;
    if (!this.mHasVoteButtons)
      return;
    this.thumbsUp.onValueChanged.AddListener((UnityAction<bool>) (value => this.Vote(true, value)));
    this.thumbsDown.onValueChanged.AddListener((UnityAction<bool>) (value => this.Vote(false, value)));
  }

  private void OnEnable()
  {
    if (!SteamManager.Initialized || !this.mHasVoteButtons)
      return;
    SteamModVoter modVoter = App.instance.modManager.modVoter;
    modVoter.OnGetVoteSuccessful += new Action<ulong, bool, bool, bool>(this.OnGetSteamModVote);
    modVoter.OnVoteSuccessful += new Action<ulong, bool>(this.OnVoteSteamMod);
  }

  private void OnDisable()
  {
    if (!SteamManager.Initialized || !this.mHasVoteButtons)
      return;
    SteamModVoter modVoter = App.instance.modManager.modVoter;
    modVoter.OnGetVoteSuccessful -= new Action<ulong, bool, bool, bool>(this.OnGetSteamModVote);
    modVoter.OnVoteSuccessful -= new Action<ulong, bool>(this.OnVoteSteamMod);
  }

  public void SetupReviewForMod(SteamMod inSteamMod)
  {
    this.mSteamMod = inSteamMod;
    StringVariableParser.ordinalNumberString = inSteamMod.totalVotes.ToString((IFormatProvider) Localisation.numberFormatter);
    this.reviewText.text = Localisation.LocaliseID("PSG_10012017", (GameObject) null);
    for (int index = 0; index < this.stars.Length; ++index)
      this.stars[index].SetActive(index < inSteamMod.stars);
    if (!this.mHasVoteButtons)
      return;
    App.instance.modManager.modVoter.GetModVote(this.mSteamMod);
    this.MakeButtonsInteractable(false);
    this.thumbsUp.group.allowSwitchOff = true;
    this.thumbsUp.isOn = false;
    this.thumbsDown.isOn = false;
    this.thumbsDown.group.allowSwitchOff = false;
  }

  public void MakeButtonsInteractable(bool inInteractable)
  {
    this.thumbsUp.interactable = inInteractable;
    this.thumbsDown.interactable = inInteractable;
  }

  public void SetupDefaults()
  {
    this.reviewText.text = "-";
    for (int index = 0; index < this.stars.Length; ++index)
      GameUtility.SetActive(this.stars[index], false);
    this.thumbsUp.group.allowSwitchOff = true;
    this.thumbsUp.isOn = false;
    this.thumbsDown.isOn = false;
    this.thumbsDown.group.allowSwitchOff = false;
  }

  private void Vote(bool inThumbsUp, bool inVoting)
  {
    if (!inVoting || this.mSteamMod == null)
      return;
    App.instance.modManager.modVoter.VoteMod(this.mSteamMod, inThumbsUp);
  }

  private void OnVoteSteamMod(ulong inSteamID, bool inThumbsUp)
  {
    if ((long) this.mSteamMod.modDetails.m_nPublishedFileId.m_PublishedFileId == (long) inSteamID)
    {
      this.thumbsUp.onValueChanged.RemoveAllListeners();
      this.thumbsDown.onValueChanged.RemoveAllListeners();
      this.thumbsUp.isOn = inThumbsUp;
      this.thumbsDown.isOn = !inThumbsUp;
      this.thumbsUp.onValueChanged.AddListener((UnityAction<bool>) (value => this.Vote(true, value)));
      this.thumbsDown.onValueChanged.AddListener((UnityAction<bool>) (value => this.Vote(false, value)));
    }
    this.MakeButtonsInteractable(true);
  }

  private void OnGetSteamModVote(ulong inSteamID, bool inThumbsDown, bool inThumbsUp, bool inNotVoted)
  {
    if ((long) this.mSteamMod.modDetails.m_nPublishedFileId.m_PublishedFileId == (long) inSteamID)
    {
      this.thumbsUp.onValueChanged.RemoveAllListeners();
      this.thumbsDown.onValueChanged.RemoveAllListeners();
      this.thumbsUp.isOn = inThumbsUp;
      this.thumbsDown.isOn = inThumbsDown;
      this.thumbsUp.onValueChanged.AddListener((UnityAction<bool>) (value => this.Vote(true, value)));
      this.thumbsDown.onValueChanged.AddListener((UnityAction<bool>) (value => this.Vote(false, value)));
    }
    this.MakeButtonsInteractable(true);
  }

  private void SetupButtonsColor(bool inGrey = false)
  {
    if (inGrey)
    {
      ColorBlock colors = this.thumbsUp.colors;
      colors.pressedColor = Color.grey;
      this.thumbsUp.colors = colors;
      this.thumbsDown.colors = colors;
    }
    else
    {
      if (!this.thumbsUp.isOn)
      {
        ColorBlock colors = this.thumbsUp.colors;
        colors.pressedColor = Color.green;
        this.thumbsUp.colors = colors;
      }
      if (this.thumbsDown.isOn)
        return;
      ColorBlock colors1 = this.thumbsDown.colors;
      colors1.pressedColor = Color.red;
      this.thumbsDown.colors = colors1;
    }
  }
}
