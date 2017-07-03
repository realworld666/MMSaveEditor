// Decompiled with JetBrains decompiler
// Type: UITopBarPreseasonStageWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class UITopBarPreseasonStageWidget : MonoBehaviour
{
  private PreSeasonState.PreSeasonStage mStage = PreSeasonState.PreSeasonStage.Count;
  public GameObject preSeasonIcon;
  public GameObject carDevIcon;
  public TextMeshProUGUI header;
  public TextMeshProUGUI timeLeft;

  public void SetListener()
  {
    UIManager.OnScreenChange += new Action(this.UpdateVisibility);
  }

  public void OnUnload()
  {
    UIManager.OnScreenChange -= new Action(this.UpdateVisibility);
  }

  public void OnDestroy()
  {
    UIManager.OnScreenChange -= new Action(this.UpdateVisibility);
  }

  public void UpdateVisibility()
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
    if (currentState == null || this.mStage == currentState.stage)
      return;
    this.mStage = currentState.stage;
    bool inIsActive = currentState.stage <= PreSeasonState.PreSeasonStage.ChooseLivery;
    GameUtility.SetActive(this.preSeasonIcon, !inIsActive);
    GameUtility.SetActive(this.carDevIcon, inIsActive);
    switch (currentState.stage)
    {
      case PreSeasonState.PreSeasonStage.DesignCar:
      case PreSeasonState.PreSeasonStage.DesigningCar:
      case PreSeasonState.PreSeasonStage.ChooseLivery:
      case PreSeasonState.PreSeasonStage.ChoosingLivery:
        StringVariableParser.stringValue1 = Localisation.LocaliseEnum((Enum) currentState.stage);
        this.header.text = Localisation.LocaliseID("PSG_10010990", (GameObject) null);
        this.timeLeft.text = Localisation.LocaliseID("PSG_10010989", (GameObject) null);
        break;
      case PreSeasonState.PreSeasonStage.PartAdapting:
      case PreSeasonState.PreSeasonStage.PreseasonTest:
      case PreSeasonState.PreSeasonStage.InPreSeasonTest:
        this.header.text = Localisation.LocaliseEnum((Enum) currentState.stage);
        this.timeLeft.text = Localisation.LocaliseID("PSG_10010989", (GameObject) null);
        break;
      case PreSeasonState.PreSeasonStage.Finished:
        this.header.text = Localisation.LocaliseID("PSG_10010988", (GameObject) null);
        this.timeLeft.text = Localisation.LocaliseID("PSG_10010989", (GameObject) null);
        break;
      default:
        int days = (Game.instance.calendar.GetNextEventForGameState(GameState.Type.PreSeasonState).triggerDate - Game.instance.time.now).Days;
        StringVariableParser.stringValue1 = Localisation.LocaliseEnum((Enum) currentState.stage);
        this.header.text = Localisation.LocaliseID("PSG_10010990", (GameObject) null);
        StringVariableParser.intValue1 = days;
        this.timeLeft.text = Localisation.LocaliseID("PSG_10010991", (GameObject) null);
        break;
    }
  }
}
