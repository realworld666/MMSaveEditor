// Decompiled with JetBrains decompiler
// Type: UIPlayerStatsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIPlayerStatsWidget : MonoBehaviour
{
  public UIPlayerStatsEntry[] stats;

  public void Setup(Player inPlayer)
  {
    if (inPlayer == null)
      return;
    TeamPrincipalStats stats = inPlayer.stats;
    for (int index = 0; index < this.stats.Length; ++index)
    {
      switch (index)
      {
        case 0:
          this.stats[index].Setup(stats.raceManagement);
          break;
        case 1:
          this.stats[index].Setup(stats.financial);
          break;
        case 2:
          this.stats[index].Setup(stats.loyalty);
          break;
      }
    }
  }
}
