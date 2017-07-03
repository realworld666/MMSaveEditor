// Decompiled with JetBrains decompiler
// Type: UITopBarCarDesignStages
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class UITopBarCarDesignStages : MonoBehaviour
{
  public List<GameObject> carDesignStage = new List<GameObject>();
  public List<GameObject> partAdaptationStage = new List<GameObject>();
  public List<GameObject> liveryStage = new List<GameObject>();

  public void SetListener()
  {
    UIManager.OnScreenChange += new Action(this.UpdateVisibility);
  }

  public void OnDestroy()
  {
    UIManager.OnScreenChange -= new Action(this.UpdateVisibility);
  }

  public void OnUnload()
  {
    UIManager.OnScreenChange -= new Action(this.UpdateVisibility);
  }

  private void UpdateVisibility()
  {
    if (!Game.IsActive())
      return;
    this.gameObject.SetActive(App.instance.gameStateManager.currentState is PreSeasonState && !Game.instance.player.IsUnemployed());
  }

  private void Update()
  {
    if (!Game.IsActive())
      return;
    PreSeasonState currentState = (PreSeasonState) App.instance.gameStateManager.currentState;
    if (currentState == null)
      return;
    for (int index = 0; index < this.carDesignStage.Count; ++index)
      GameUtility.SetActive(this.carDesignStage[index], currentState.stage > PreSeasonState.PreSeasonStage.DesigningCar);
    for (int index = 0; index < this.partAdaptationStage.Count; ++index)
      GameUtility.SetActive(this.partAdaptationStage[index], currentState.stage > PreSeasonState.PreSeasonStage.PartAdapting);
    for (int index = 0; index < this.liveryStage.Count; ++index)
      GameUtility.SetActive(this.liveryStage[index], currentState.stage > PreSeasonState.PreSeasonStage.ChoosingLivery);
  }
}
