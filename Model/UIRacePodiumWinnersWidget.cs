// Decompiled with JetBrains decompiler
// Type: UIRacePodiumWinnersWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIRacePodiumWinnersWidget : MonoBehaviour
{
  public UIRacePodiumDriverEntry[] entries;
  public UIRacePodiumRepresentative representative;

  public void Setup()
  {
    for (int index = 0; index < 3; ++index)
    {
      RaceEventResults.ResultData inResultData = Game.instance.sessionManager.eventDetails.results.GetResultsForSession(SessionDetails.SessionType.Race).resultData[index];
      this.entries[index].SetResultData(inResultData);
    }
    this.representative.Setup((Person) Game.instance.sessionManager.standings[0].driver.contract.GetTeam().teamPrincipal);
  }
}
