// Decompiled with JetBrains decompiler
// Type: PoliticsVotePopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PoliticsVotePopup : UIDialogBox
{
  private List<VoteChoice> mProcessedVotes = new List<VoteChoice>();
  public TextMeshProUGUI voteTitle;
  public TextMeshProUGUI voteDescription;
  public TextMeshProUGUI votesRemaining;
  public TextMeshProUGUI winnerVote;
  public TextMeshProUGUI votesAgainstText;
  public TextMeshProUGUI votesAbstainedText;
  public TextMeshProUGUI votesForText;
  public UITeamLogo teamLogo;
  public UICharacterPortrait voterPortrait;
  public TextMeshProUGUI voterNameText;
  public TextMeshProUGUI voterOptionText;
  public TextMeshProUGUI winningVote;
  public Image voterOptionBacking;
  public Image voterCircleBacking;
  public Button concludeVoteButton;
  public TextMeshProUGUI concludeButtonText;
  public Button continueVoteButton;
  public Animator voteAnimator;
  public Animator animator;
  public UIGridList listAgainst;
  public UIGridList listFor;
  public UIGridList listAbstain;
  private PoliticsVotePopup.VotingState mState;
  private PoliticalSystem mPoliticalSystem;

  protected override void Awake()
  {
    base.Awake();
    this.concludeVoteButton.onClick.AddListener(new UnityAction(this.OnConcludeVote));
    this.continueVoteButton.onClick.AddListener(new UnityAction(this.OnContinueVote));
  }

  public void Open(PoliticalSystem inSystem)
  {
    this.listAgainst.DestroyListItems();
    this.listFor.DestroyListItems();
    this.listAbstain.DestroyListItems();
    this.voteAnimator.speed = 1f;
    this.concludeButtonText.text = Localisation.LocaliseID("PSG_10010870", (GameObject) null);
    this.mProcessedVotes.Clear();
    this.mPoliticalSystem = inSystem;
    this.mPoliticalSystem.ConcludeVoting();
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) this);
    this.SetState(PoliticsVotePopup.VotingState.VoteInProgress);
    this.voteTitle.text = this.mPoliticalSystem.GetLatestVoteResults().votedSubject.GetName();
    this.voteDescription.text = this.mPoliticalSystem.GetLatestVoteResults().votedSubject.GetDescription();
    this.votesRemaining.text = this.mPoliticalSystem.voteChoices.Count.ToString();
    this.winningVote.text = Localisation.LocaliseEnum((Enum) this.mPoliticalSystem.GetLatestVoteResults().voteResult);
    switch (this.mPoliticalSystem.GetLatestVoteResults().voteResult)
    {
      case PoliticalSystem.VoteResult.Accepted:
        this.winningVote.color = UIConstants.positiveColor;
        break;
      case PoliticalSystem.VoteResult.Rejected:
        this.winningVote.color = UIConstants.negativeColor;
        break;
    }
    this.SetVotePercentages();
  }

  private void OnConcludeVote()
  {
    if ((double) this.voteAnimator.speed == 1.0)
    {
      this.voteAnimator.speed = 5f;
      this.concludeButtonText.text = Localisation.LocaliseID("PSG_10010871", (GameObject) null);
    }
    else
      this.ProcessVotes(true);
  }

  private void OnContinueVote()
  {
    UIManager.instance.dialogBoxManager.HideAll();
  }

  public void ProcessVotes(bool inSkipAnimation)
  {
    if (this.voteAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash != Animator.StringToHash("Default") && !inSkipAnimation)
      return;
    this.SetVotePercentages();
    if (this.mPoliticalSystem.voteChoices.Count == this.mProcessedVotes.Count)
    {
      this.SetState(PoliticsVotePopup.VotingState.VoteCompleted);
      PoliticalSystem.VoteResults latestVoteResults = this.mPoliticalSystem.GetLatestVoteResults();
      StringVariableParser.voteResult = latestVoteResults.voteResult;
      FeedbackPopup.Open(Localisation.LocaliseID("PSG_10010868", (GameObject) null), latestVoteResults.votedSubject.GetName());
    }
    for (int index = 0; index < this.mPoliticalSystem.voteChoices.Count; ++index)
    {
      VoteChoice voteChoice = this.mPoliticalSystem.voteChoices[index];
      if (!this.mProcessedVotes.Contains(voteChoice))
      {
        this.AddVoteToProcessedStack(voteChoice);
        if (!inSkipAnimation)
          break;
      }
    }
    this.votesRemaining.text = (this.mPoliticalSystem.voteChoices.Count - this.mProcessedVotes.Count).ToString();
    if (!inSkipAnimation)
      return;
    this.SetVotePercentages();
  }

  private void AddVoteToProcessedStack(VoteChoice inVote)
  {
    this.mProcessedVotes.Add(inVote);
    Person inPerson;
    if (inVote.IsPresident())
    {
      this.voteAnimator.speed = 1f;
      inPerson = Game.instance.player.team.championship.politicalSystem.president;
      this.voteAnimator.SetTrigger(AnimationHashes.VoteAnimPresident);
    }
    else
    {
      inPerson = inVote.team.contractManager.GetPersonOnJob(Contract.Job.TeamPrincipal);
      this.voteAnimator.SetTrigger(AnimationHashes.VoteAnim);
      this.teamLogo.SetTeam(inVote.team);
    }
    this.voterPortrait.SetPortrait(inPerson);
    this.voterNameText.text = inPerson.name;
    this.voterOptionText.text = Localisation.LocaliseEnum((Enum) inVote.vote);
    switch (inVote.vote)
    {
      case VoteChoice.Vote.Yes:
        this.voterOptionBacking.color = UIConstants.positiveColor;
        UIVoterIconEntry listItem1 = this.listFor.CreateListItem<UIVoterIconEntry>();
        listItem1.Setup(inVote, this.voterOptionBacking.color);
        listItem1.GetComponent<Animator>().speed = this.voteAnimator.speed;
        break;
      case VoteChoice.Vote.No:
        this.voterOptionBacking.color = UIConstants.negativeColor;
        UIVoterIconEntry listItem2 = this.listAgainst.CreateListItem<UIVoterIconEntry>();
        listItem2.Setup(inVote, this.voterOptionBacking.color);
        listItem2.GetComponent<Animator>().speed = this.voteAnimator.speed;
        break;
      case VoteChoice.Vote.Undecided:
      case VoteChoice.Vote.Abstain:
        this.voterOptionBacking.color = Color.gray;
        UIVoterIconEntry listItem3 = this.listAbstain.CreateListItem<UIVoterIconEntry>();
        listItem3.Setup(inVote, this.voterOptionBacking.color);
        listItem3.GetComponent<Animator>().speed = this.voteAnimator.speed;
        break;
    }
    this.voterCircleBacking.color = this.voterOptionBacking.color;
  }

  private void Update()
  {
    switch (this.mState)
    {
      case PoliticsVotePopup.VotingState.VoteInProgress:
        this.ProcessVotes(false);
        break;
    }
  }

  private void SetState(PoliticsVotePopup.VotingState inState)
  {
    this.mState = inState;
    this.animator.SetTrigger(this.mState.ToString());
  }

  private void SetVotePercentages()
  {
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    for (int index = 0; index < this.mProcessedVotes.Count; ++index)
    {
      VoteChoice mProcessedVote = this.mProcessedVotes[index];
      if (mProcessedVote.vote == VoteChoice.Vote.Yes)
        num1 += mProcessedVote.votePowerUsed;
      else if (mProcessedVote.vote == VoteChoice.Vote.No)
        num2 += mProcessedVote.votePowerUsed;
      else
        ++num3;
    }
    if (this.mProcessedVotes.Count != 0)
    {
      StringVariableParser.intValue1 = num3;
      this.votesAbstainedText.text = Localisation.LocaliseID(StringVariableParser.intValue1 != 1 ? "PSG_10010869" : "PSG_10011115", (GameObject) null);
      StringVariableParser.intValue1 = num2;
      this.votesAgainstText.text = Localisation.LocaliseID(StringVariableParser.intValue1 != 1 ? "PSG_10010869" : "PSG_10011115", (GameObject) null);
      StringVariableParser.intValue1 = num1;
      this.votesForText.text = Localisation.LocaliseID(StringVariableParser.intValue1 != 1 ? "PSG_10010869" : "PSG_10011115", (GameObject) null);
    }
    else
    {
      StringVariableParser.intValue1 = 0;
      this.votesAbstainedText.text = Localisation.LocaliseID("PSG_10010869", (GameObject) null);
      this.votesAgainstText.text = Localisation.LocaliseID("PSG_10010869", (GameObject) null);
      this.votesForText.text = Localisation.LocaliseID("PSG_10010869", (GameObject) null);
    }
  }

  public enum VotingState
  {
    VoteInProgress,
    VoteCompleted,
  }
}
