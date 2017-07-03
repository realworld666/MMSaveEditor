// Decompiled with JetBrains decompiler
// Type: ChampionshipStatusWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ChampionshipStatusWidget : MonoBehaviour
{
  public GameObject[] preSeasonActive = new GameObject[0];
  public GameObject[] preSeasonInactive = new GameObject[0];

  private void OnEnable()
  {
    bool inBool = App.instance.gameStateManager.currentState is PreSeasonState;
    this.SetState(this.preSeasonActive, inBool);
    this.SetState(this.preSeasonInactive, !inBool);
  }

  private void SetState(GameObject[] inArray, bool inBool)
  {
    for (int index = 0; index < inArray.Length; ++index)
      GameUtility.SetActive(inArray[index], inBool);
  }
}
