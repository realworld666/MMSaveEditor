// Decompiled with JetBrains decompiler
// Type: UIChampionshipRulesEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIChampionshipRulesEntry : MonoBehaviour
{
  public TextMeshProUGUI title;
  public TextMeshProUGUI description;
  private PoliticalVote mRule;

  public void Setup(PoliticalVote inRule)
  {
    this.mRule = inRule;
    this.title.text = this.mRule.GetName();
    this.description.text = this.mRule.GetDescription();
  }
}
