// Decompiled with JetBrains decompiler
// Type: UIVoterIconEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIVoterIconEntry : MonoBehaviour
{
  public Image[] voteColorBackings = new Image[0];
  public TextMeshProUGUI votingPower;
  public UICharacterPortrait portrait;

  public void Setup(VoteChoice inVote, Color inColor)
  {
    for (int index = 0; index < this.voteColorBackings.Length; ++index)
      this.voteColorBackings[index].color = inColor;
    if (inVote.vote == VoteChoice.Vote.Abstain)
    {
      this.votingPower.text = "+1";
      this.portrait.SetPortrait(inVote.team.contractManager.GetPersonOnJob(Contract.Job.TeamPrincipal));
    }
    else
    {
      this.votingPower.transform.parent.parent.gameObject.SetActive(inVote.votePowerUsed != 1);
      this.votingPower.text = "x" + inVote.votePowerUsed.ToString();
      if (inVote.IsPresident())
        this.portrait.SetPortrait(Game.instance.player.team.championship.politicalSystem.president);
      else
        this.portrait.SetPortrait(inVote.team.contractManager.GetPersonOnJob(Contract.Job.TeamPrincipal));
    }
  }
}
