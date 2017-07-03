// Decompiled with JetBrains decompiler
// Type: ActivateForSeries
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class ActivateForSeries : MonoBehaviour
{
  public ActivateForSeries.GameObjectData[] targetData = new ActivateForSeries.GameObjectData[0];
  public ActivateForSeries.ComparisonSource source;

  private void OnEnable()
  {
    if (!Game.IsActive() || this.source != ActivateForSeries.ComparisonSource.PlayerChampionship)
      return;
    GameUtility.SetActiveForSeries(Game.instance.player.team.championship, this.targetData);
  }

  public enum ComparisonSource
  {
    PlayerChampionship,
  }

  [Serializable]
  public struct GameObjectData
  {
    public Championship.Series series;
    public GameObject[] targetObjects;
  }
}
