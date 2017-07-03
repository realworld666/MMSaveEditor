// Decompiled with JetBrains decompiler
// Type: PenaltiesPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class PenaltiesPanel : HUDPanel
{
  public GameObject underInvestigationContainer;
  public GameObject penaltyGivenContainer;
  public GameObject pitlaneDriveTroughtPenalty;
  public GameObject driverClearedContainer;
  public TextMeshProUGUI penaltyGivenText;

  public override HUDPanel.Type type
  {
    get
    {
      return HUDPanel.Type.Penalties;
    }
  }

  public void OnUpdate()
  {
    GameUtility.SetActive(this.underInvestigationContainer, this.vehicle.sessionPenalty.state == SessionPenalty.State.UnderInvestigation);
    GameUtility.SetActive(this.penaltyGivenContainer, this.vehicle.sessionPenalty.state == SessionPenalty.State.Underway);
    GameUtility.SetActive(this.driverClearedContainer, this.vehicle.sessionPenalty.state == SessionPenalty.State.Cleared);
    GameUtility.SetActive(this.pitlaneDriveTroughtPenalty, this.vehicle.sessionPenalty.state == SessionPenalty.State.None && this.vehicle.strategy.isServingPitLanePenalty);
    GameUtility.SetActive(this.gameObject, this.vehicle.sessionPenalty.state != SessionPenalty.State.None || this.pitlaneDriveTroughtPenalty.activeSelf);
    if (!this.vehicle.sessionPenalty.hasActivePenalty)
      return;
    this.penaltyGivenText.text = this.vehicle.sessionPenalty.currentPenalty.GetDescription();
  }
}
