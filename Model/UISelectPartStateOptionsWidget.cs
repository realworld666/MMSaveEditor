// Decompiled with JetBrains decompiler
// Type: UISelectPartStateOptionsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class UISelectPartStateOptionsWidget : MonoBehaviour
{
  public UICarPartTypeEntry[] partEntries = new UICarPartTypeEntry[0];
  public UICarPartSpecEntry[] partSpecEntries = new UICarPartSpecEntry[0];

  public void OnEnter()
  {
    ChampionshipRules rules = Game.instance.player.team.championship.rules;
    List<CarPart.PartType> partTypeList = new List<CarPart.PartType>((IEnumerable<CarPart.PartType>) CarPart.GetPartType(Game.instance.player.team.championship.series, true));
    for (int index = 0; index < this.partEntries.Length; ++index)
    {
      bool flag = index < partTypeList.Count;
      if (flag)
      {
        this.partEntries[index].partType = partTypeList[index];
        this.partEntries[index].Setup();
      }
      this.partEntries[index].gameObject.SetActive(flag && !rules.specParts.Contains(this.partEntries[index].partType));
    }
    for (int index = 0; index < this.partSpecEntries.Length; ++index)
    {
      if (index < partTypeList.Count)
      {
        this.partSpecEntries[index].partType = partTypeList[index];
        this.partSpecEntries[index].Setup();
      }
      this.partSpecEntries[index].gameObject.SetActive(rules.specParts.Contains(this.partEntries[index].partType));
    }
  }
}
