// Decompiled with JetBrains decompiler
// Type: UIPoliticalVotingPowerEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIPoliticalVotingPowerEntry : MonoBehaviour
{
  public TextMeshProUGUI managerName;
  public TextMeshProUGUI teamName;
  public TextMeshProUGUI votingPower;

  public void Setup(Team inTeam)
  {
    GameUtility.SetActive(this.gameObject, true);
    this.managerName.text = inTeam.contractManager.GetPersonOnJob(Contract.Job.TeamPrincipal).name;
    this.teamName.text = inTeam.name;
    this.votingPower.text = "x" + (object) inTeam.votingPower;
  }
}
