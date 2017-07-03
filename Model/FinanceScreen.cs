// Decompiled with JetBrains decompiler
// Type: FinanceScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FinanceScreen : UIScreen
{
  public UIFinanceScreenFooterWidget footerWidgtet;
  public UIFinanceDetailsWidget detailsWidget;

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    if (App.instance.gameStateManager.currentState.type == GameState.Type.TravelArrangements)
    {
      this.continueButtonUpperLabel = Localisation.LocaliseID("PSG_10002714", (GameObject) null);
      this.continueButtonLabel = Localisation.LocaliseID("PSG_10001574", (GameObject) null);
    }
    this.footerWidgtet.OnEnter();
    this.detailsWidget.OnEnter();
    this.detailsWidget.SetToggles();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
  }

  public void Update()
  {
  }
}
