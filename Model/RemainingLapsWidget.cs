// Decompiled with JetBrains decompiler
// Type: RemainingLapsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class RemainingLapsWidget : MonoBehaviour
{
  public TextMeshProUGUI remainingLapsLabel;
  private float mDisplayTimer;

  public void OnEnter()
  {
    Game.instance.sessionManager.OnLeaderLapEnd += new Action(this.OnLeaderLapEnd);
    this.gameObject.SetActive(false);
  }

  public void OnExit()
  {
    if (!Game.IsActive() || Game.instance.sessionManager == null)
      return;
    Game.instance.sessionManager.OnLeaderLapEnd -= new Action(this.OnLeaderLapEnd);
  }

  public void OnLeaderLapEnd()
  {
    this.Show();
  }

  public void Show()
  {
    int lapsRemaining = Game.instance.sessionManager.GetLapsRemaining();
    if (lapsRemaining <= 0)
      return;
    if (lapsRemaining == 1)
    {
      this.remainingLapsLabel.text = Localisation.LocaliseID("PSG_10011024", (GameObject) null);
    }
    else
    {
      StringVariableParser.intValue1 = lapsRemaining;
      this.remainingLapsLabel.text = Localisation.LocaliseID("PSG_10010840", (GameObject) null);
    }
    this.mDisplayTimer = 0.0f;
    this.gameObject.SetActive(true);
  }

  private void Update()
  {
    this.mDisplayTimer += GameTimer.deltaTime;
    if ((double) this.mDisplayTimer < 8.0)
      return;
    this.gameObject.SetActive(false);
  }
}
