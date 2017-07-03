// Decompiled with JetBrains decompiler
// Type: PoliticsPlayerVoteStageProposeRule
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PoliticsPlayerVoteStageProposeRule : PoliticsPlayerVoteStage
{
  public UICharacterPortrait president;
  public TextMeshProUGUI presidentName;
  public TextMeshProUGUI presidentComment;
  public TextMeshProUGUI ruleName;
  public TextMeshProUGUI ruleDescription;
  public GameObject voteCostContainer;
  public TextMeshProUGUI voteCost;
  private PoliticalVote mVote;

  public override void OnStart()
  {
  }

  public override void Setup()
  {
    this.mVote = this.widget.selectedVote;
    this.president.SetPortrait(this.widget.politicalSystem.president);
    this.presidentName.text = this.widget.politicalSystem.president.name;
    this.ruleName.text = this.mVote.GetName();
    this.ruleDescription.text = this.mVote.GetDescription();
    GameUtility.SetActive(this.voteCostContainer, Game.instance.player.playerBackStoryType != PlayerBackStory.PlayerBackStoryType.Politico);
    if (this.voteCostContainer.activeSelf)
      this.voteCost.text = GameUtility.GetCurrencyString(-GameStatsConstants.playerVotePrice, 0);
    this.SetPresidentComment();
    base.Setup();
  }

  public override void Hide()
  {
    base.Hide();
  }

  private void SetPresidentComment()
  {
    this.presidentComment.text = string.Empty;
    Dictionary<PoliticalVote.TeamCharacteristics, string> dictionary1 = new Dictionary<PoliticalVote.TeamCharacteristics, string>();
    Dictionary<PoliticalVote.TeamCharacteristics, string> dictionary2 = new Dictionary<PoliticalVote.TeamCharacteristics, string>();
    List<VoteChoice> voteChoices = this.widget.politicalSystem.GetVoteChoices(this.mVote);
    for (int index1 = 0; index1 < voteChoices.Count; ++index1)
    {
      VoteChoice voteChoice = voteChoices[index1];
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
          Dictionary<PoliticalVote.TeamCharacteristics, string> dictionary3;
          PoliticalVote.TeamCharacteristics index3;
          (dictionary3 = dictionary1)[index3 = benificialCharacteristic] = dictionary3[index3] + str2;
        }
        else
        {
          Dictionary<PoliticalVote.TeamCharacteristics, string> dictionary3;
          PoliticalVote.TeamCharacteristics index3;
          (dictionary3 = dictionary1)[index3 = benificialCharacteristic] = dictionary3[index3] + str2;
        }
      }
      for (int index2 = 0; index2 < voteChoice.detrimentalCharacteristics.Count; ++index2)
      {
        PoliticalVote.TeamCharacteristics detrimentalCharacteristic = voteChoice.detrimentalCharacteristics[index2];
        string str1 = this.GetTeamColor(voteChoice.team);
        if (voteChoice.team == Game.instance.player.team)
          str1 = "<u>" + voteChoice.team.name + "</u>";
        string str2 = str1 + ", ";
        if (!dictionary2.ContainsKey(detrimentalCharacteristic))
        {
          dictionary2.Add(detrimentalCharacteristic, Localisation.LocaliseEnum((Enum) detrimentalCharacteristic) + "\n");
          Dictionary<PoliticalVote.TeamCharacteristics, string> dictionary3;
          PoliticalVote.TeamCharacteristics index3;
          (dictionary3 = dictionary2)[index3 = detrimentalCharacteristic] = dictionary3[index3] + str2;
        }
        else
        {
          Dictionary<PoliticalVote.TeamCharacteristics, string> dictionary3;
          PoliticalVote.TeamCharacteristics index3;
          (dictionary3 = dictionary2)[index3 = detrimentalCharacteristic] = dictionary3[index3] + str2;
        }
      }
    }
    if (dictionary1.Keys.Count == 0 && dictionary2.Keys.Count == 0)
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
      if (dictionary2.Keys.Count <= 0)
        return;
      TextMeshProUGUI presidentComment = this.presidentComment;
      string str = presidentComment.text + "\n\n" + Localisation.LocaliseID("PSG_10008514", (GameObject) null);
      presidentComment.text = str;
      using (Dictionary<PoliticalVote.TeamCharacteristics, string>.ValueCollection.Enumerator enumerator = dictionary2.Values.GetEnumerator())
      {
        while (enumerator.MoveNext())
          this.presidentComment.text += enumerator.Current;
      }
      this.presidentComment.text = this.presidentComment.text.Remove(this.presidentComment.text.Length - 2);
    }
  }

  private string GetTeamColor(Team inTeam)
  {
    return GameUtility.ColorToRichTextHex(UIConstants.mailPolitics) + inTeam.name + "</color>";
  }
}
