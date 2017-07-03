// Decompiled with JetBrains decompiler
// Type: UIHUBUltimatumWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIHUBUltimatumWidget : MonoBehaviour
{
  public TextMeshProUGUI objectiveLabel;

  public void Setup()
  {
    SessionDetails.SessionType sessionType = Game.instance.sessionManager.eventDetails.currentSession.sessionType;
    Chairman chairman = Game.instance.player.team.chairman;
    GameUtility.SetActive(this.gameObject, chairman.hasMadeUltimatum && sessionType == SessionDetails.SessionType.Race);
    if (!this.gameObject.activeSelf)
      return;
    this.objectiveLabel.text = GameUtility.FormatForPosition(chairman.ultimatum.positionExpected, (string) null);
  }
}
