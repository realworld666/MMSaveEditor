// Decompiled with JetBrains decompiler
// Type: PoliticsOverview
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PoliticsOverview : UIDialogBox
{
  private List<Transform> mPortraitsTransforms = new List<Transform>();
  private List<UICharacterPortrait> mPortraitsIcons = new List<UICharacterPortrait>();
  public UICharacterPortrait president;
  public TextMeshProUGUI presidentName;
  public TextMeshProUGUI presidentComment;
  public TextMeshProUGUI ruleTitle;
  public TextMeshProUGUI ruleDescription;
  public TextMeshProUGUI playerVotePower;
  public TextMeshProUGUI bribedLabel;
  public GameObject bribedObject;
  public Transform portraitsParent;
  public Transform voteAgainstParent;
  public Transform voteForParent;
  public Transform voteUndecidedParent;
  public Toggle voteAgainst;
  public Toggle voteFor;
  public Toggle abstainVote;
  public Button continueButton;
  public Button addVotingPower;
  public Button removeVotingPower;
  public Button trackChangesButton;
  private VoteChoice mPlayerVote;
  private PoliticalSystem mPoliticalSystem;

  protected override void Awake()
  {
    base.Awake();
    this.voteAgainst.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnVoteAgainst(value)));
    this.voteFor.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnVoteFor(value)));
    this.abstainVote.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnVoteAbstain(value)));
    this.continueButton.onClick.AddListener(new UnityAction(this.OnContinueButton));
    this.addVotingPower.onClick.AddListener(new UnityAction(this.AddVotingPower));
    this.removeVotingPower.onClick.AddListener(new UnityAction(this.RemoveVotingPower));
    this.trackChangesButton.onClick.AddListener(new UnityAction(this.OnTrackChangesButton));
    for (int index = 1; index < this.portraitsParent.childCount; ++index)
    {
      this.mPortraitsTransforms.Add(this.portraitsParent.GetChild(index));
      this.mPortraitsIcons.Add(this.portraitsParent.GetChild(index).GetChild(1).GetComponent<UICharacterPortrait>());
    }
  }

  private void AddVotingPower()
  {
    if (this.mPlayerVote.votePowerUsed - 1 >= Game.instance.player.team.votingPower)
      return;
    ++this.mPlayerVote.votePowerUsed;
    this.playerVotePower.text = this.mPlayerVote.votePowerUsed.ToString();
  }

  private void RemoveVotingPower()
  {
    if (this.mPlayerVote.votePowerUsed <= 1)
      return;
    --this.mPlayerVote.votePowerUsed;
    this.playerVotePower.text = this.mPlayerVote.votePowerUsed.ToString();
  }

  private void OnContinueButton()
  {
    this.gameObject.SetActive(false);
    DilemmaSystem dilemmaSystem = Game.instance.dilemmaSystem;
    this.mPoliticalSystem.activeVote.playerBribe = dilemmaSystem.currentBribe;
    dilemmaSystem.currentBribe = DilemmaSystem.BribedOption.None;
    UIManager.instance.dialogBoxManager.GetDialog<PoliticsVotePopup>().Open(this.mPoliticalSystem);
  }

  public void Open(PoliticalSystem inSystem)
  {
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) this);
    this.mPoliticalSystem = inSystem;
    this.president.SetPortrait(this.mPoliticalSystem.president);
    this.presidentName.text = this.mPoliticalSystem.president.name;
    this.ruleTitle.text = this.mPoliticalSystem.activeVote.GetName();
    this.ruleDescription.text = this.mPoliticalSystem.activeVote.GetDescription();
    this.presidentComment.text = string.Empty;
    GameUtility.SetActive(this.trackChangesButton.gameObject, this.mPoliticalSystem.activeVote.HasImpactOfType<PoliticalImpactChangeTrack>());
    this.SetPresidentComment();
    this.SetCurrentVotes();
    this.playerVotePower.text = this.mPlayerVote.votePowerUsed.ToString();
    this.SetupVoteTogglesAndBribe();
  }

  private void SetupVoteTogglesAndBribe()
  {
    DilemmaSystem.BribedOption currentBribe = Game.instance.dilemmaSystem.currentBribe;
    bool inIsActive = currentBribe != DilemmaSystem.BribedOption.None;
    string str = string.Empty;
    switch (currentBribe)
    {
      case DilemmaSystem.BribedOption.None:
        this.abstainVote.isOn = true;
        break;
      case DilemmaSystem.BribedOption.InFavor:
        this.voteFor.isOn = true;
        this.OnVoteFor(true);
        str = Localisation.LocaliseID("PSG_10010843", (GameObject) null);
        break;
      case DilemmaSystem.BribedOption.Against:
        this.voteAgainst.isOn = true;
        this.OnVoteAgainst(true);
        str = Localisation.LocaliseID("PSG_10010844", (GameObject) null);
        break;
      case DilemmaSystem.BribedOption.Abstain:
        this.abstainVote.isOn = true;
        this.OnVoteAbstain(true);
        str = Localisation.LocaliseID("PSG_10010845", (GameObject) null);
        break;
    }
    this.abstainVote.interactable = !inIsActive;
    this.voteAgainst.interactable = !inIsActive;
    this.voteFor.interactable = !inIsActive;
    GameUtility.SetActive(this.bribedObject, inIsActive);
    this.bribedLabel.text = str;
  }

  private void SetPresidentComment()
  {
    Dictionary<PoliticalVote.TeamCharacteristics, string> dictionary1 = new Dictionary<PoliticalVote.TeamCharacteristics, string>();
    for (int index1 = 0; index1 < this.mPoliticalSystem.voteChoices.Count; ++index1)
    {
      VoteChoice voteChoice = this.mPoliticalSystem.voteChoices[index1];
      for (int index2 = 0; index2 < voteChoice.benificialCharacteristics.Count; ++index2)
      {
        PoliticalVote.TeamCharacteristics benificialCharacteristic = voteChoice.benificialCharacteristics[index2];
        string str1 = this.GetTeamColor(voteChoice.team);
        if (voteChoice.team == Game.instance.player.team)
          str1 = "<u>" + voteChoice.team.name + "</u>";
        string str2 = str1 + ", ";
        if (!dictionary1.ContainsKey(benificialCharacteristic))
        {
          dictionary1.Add(benificialCharacteristic, Localisation.LocaliseEnum((Enum) benificialCharacteristic) + "\n");
          Dictionary<PoliticalVote.TeamCharacteristics, string> dictionary2;
          PoliticalVote.TeamCharacteristics index3;
          (dictionary2 = dictionary1)[index3 = benificialCharacteristic] = dictionary2[index3] + str2;
        }
        else
        {
          Dictionary<PoliticalVote.TeamCharacteristics, string> dictionary2;
          PoliticalVote.TeamCharacteristics index3;
          (dictionary2 = dictionary1)[index3 = benificialCharacteristic] = dictionary2[index3] + str2;
        }
      }
    }
    Dictionary<PoliticalVote.TeamCharacteristics, string> dictionary3 = new Dictionary<PoliticalVote.TeamCharacteristics, string>();
    for (int index1 = 0; index1 < this.mPoliticalSystem.voteChoices.Count; ++index1)
    {
      VoteChoice voteChoice = this.mPoliticalSystem.voteChoices[index1];
      for (int index2 = 0; index2 < voteChoice.detrimentalCharacteristics.Count; ++index2)
      {
        PoliticalVote.TeamCharacteristics detrimentalCharacteristic = voteChoice.detrimentalCharacteristics[index2];
        string str1 = this.GetTeamColor(voteChoice.team);
        if (voteChoice.team == Game.instance.player.team)
          str1 = "<u>" + voteChoice.team.name + "</u>";
        string str2 = str1 + ", ";
        if (!dictionary3.ContainsKey(detrimentalCharacteristic))
        {
          dictionary3.Add(detrimentalCharacteristic, Localisation.LocaliseEnum((Enum) detrimentalCharacteristic) + "\n");
          Dictionary<PoliticalVote.TeamCharacteristics, string> dictionary2;
          PoliticalVote.TeamCharacteristics index3;
          (dictionary2 = dictionary3)[index3 = detrimentalCharacteristic] = dictionary2[index3] + str2;
        }
        else
        {
          Dictionary<PoliticalVote.TeamCharacteristics, string> dictionary2;
          PoliticalVote.TeamCharacteristics index3;
          (dictionary2 = dictionary3)[index3 = detrimentalCharacteristic] = dictionary2[index3] + str2;
        }
      }
    }
    if (dictionary1.Keys.Count == 0 && dictionary3.Keys.Count == 0)
    {
      this.presidentComment.text = Localisation.LocaliseID("PSG_10008512", (GameObject) null);
    }
    else
    {
      if (dictionary1.Keys.Count > 0)
      {
        this.presidentComment.text = Localisation.LocaliseID("PSG_10008513", (GameObject) null);
        using (Dictionary<PoliticalVote.TeamCharacteristics, string>.ValueCollection.Enumerator enumerator = dictionary1.Values.GetEnumerator())
        {
          while (enumerator.MoveNext())
            this.presidentComment.text += enumerator.Current;
        }
        this.presidentComment.text = this.presidentComment.text.Remove(this.presidentComment.text.Length - 2);
      }
      if (dictionary3.Keys.Count <= 0)
        return;
      TextMeshProUGUI presidentComment = this.presidentComment;
      string str = presidentComment.text + "\n\n" + Localisation.LocaliseID("PSG_10008514", (GameObject) null);
      presidentComment.text = str;
      using (Dictionary<PoliticalVote.TeamCharacteristics, string>.ValueCollection.Enumerator enumerator = dictionary3.Values.GetEnumerator())
      {
        while (enumerator.MoveNext())
          this.presidentComment.text += enumerator.Current;
      }
      this.presidentComment.text = this.presidentComment.text.Remove(this.presidentComment.text.Length - 2);
    }
  }

  private void SetCurrentVotes()
  {
    int num1 = 5;
    int num2 = 0;
    for (int index = 0; index < this.mPoliticalSystem.voteChoices.Count; ++index)
    {
      VoteChoice voteChoice = this.mPoliticalSystem.voteChoices[index];
      bool flag = voteChoice.team == Game.instance.player.team;
      if (flag)
        this.mPlayerVote = voteChoice;
      VoteChoice.Vote vote = voteChoice.vote;
      if (!flag && num2 >= num1)
        vote = VoteChoice.Vote.Undecided;
      switch (vote)
      {
        case VoteChoice.Vote.Yes:
          if (!flag)
            ++num2;
          this.mPortraitsTransforms[index].GetChild(0).GetComponent<Image>().color = UIConstants.positiveColor;
          this.SetVoteParent(this.mPortraitsTransforms[index], this.voteForParent);
          break;
        case VoteChoice.Vote.No:
          if (!flag)
            ++num2;
          this.mPortraitsTransforms[index].GetChild(0).GetComponent<Image>().color = UIConstants.negativeColor;
          this.SetVoteParent(this.mPortraitsTransforms[index], this.voteAgainstParent);
          break;
        case VoteChoice.Vote.Undecided:
        case VoteChoice.Vote.Abstain:
          this.mPortraitsTransforms[index].GetChild(0).GetComponent<Image>().color = Color.gray;
          this.SetVoteParent(this.mPortraitsTransforms[index], this.voteUndecidedParent);
          break;
      }
      this.mPortraitsIcons[index].SetPortrait(voteChoice.team.contractManager.GetPersonOnJob(Contract.Job.TeamPrincipal));
    }
    this.voteUndecidedParent.gameObject.SetActive(this.voteUndecidedParent.childCount > 1);
    this.voteAgainstParent.gameObject.SetActive(this.voteAgainstParent.childCount > 1);
    this.voteForParent.gameObject.SetActive(this.voteForParent.childCount > 1);
  }

  private void Update()
  {
    if (this.mPlayerVote == null)
      return;
    this.addVotingPower.interactable = this.mPlayerVote.votePowerUsed - 1 < Game.instance.player.team.votingPower;
    this.removeVotingPower.interactable = this.mPlayerVote.votePowerUsed > 1;
  }

  private void OnVoteAgainst(bool inValue)
  {
    if (!inValue)
      return;
    this.mPlayerVote.vote = VoteChoice.Vote.No;
    this.SetCurrentVotes();
  }

  private void OnVoteFor(bool inValue)
  {
    if (!inValue)
      return;
    this.mPlayerVote.vote = VoteChoice.Vote.Yes;
    this.SetCurrentVotes();
  }

  private void OnVoteAbstain(bool inValue)
  {
    if (!inValue)
      return;
    this.mPlayerVote.vote = VoteChoice.Vote.Abstain;
    this.SetCurrentVotes();
  }

  private void OnTrackChangesButton()
  {
    if (this.mPoliticalSystem == null)
      return;
    Action inAction = (Action) (() => this.Open(this.mPoliticalSystem));
    this.Hide();
    UIEventCalendarVariationsPopup.ShowTrackLayoutPolitics(this.mPoliticalSystem.activeVote, inAction);
  }

  public void SetVoteParent(Transform inPortrait, Transform inParent)
  {
    inPortrait.SetParent(inParent);
  }

  private string GetTeamColor(Team inTeam)
  {
    return GameUtility.ColorToRichTextHex(UIConstants.mailPolitics) + inTeam.name + "</color>";
  }
}
