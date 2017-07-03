// Decompiled with JetBrains decompiler
// Type: RoundInfoPip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class RoundInfoPip : MonoBehaviour
{
  public GameObject empty;
  public GameObject completed;
  public GameObject next;

  public void SetRaceEventDetails(Championship inChampionship, RaceEventDetails inRaceEventDetails)
  {
    bool inIsActive = inChampionship.GetCurrentEventDetails() == inRaceEventDetails;
    bool hasEventEnded = inRaceEventDetails.hasEventEnded;
    GameUtility.SetActive(this.empty, !inIsActive && !hasEventEnded);
    GameUtility.SetActive(this.next, inIsActive);
    GameUtility.SetActive(this.completed, hasEventEnded);
  }
}
