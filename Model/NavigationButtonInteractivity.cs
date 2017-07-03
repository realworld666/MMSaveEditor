// Decompiled with JetBrains decompiler
// Type: NavigationButtonInteractivity
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class NavigationButtonInteractivity : MonoBehaviour
{
  public NavigationButtonInteractivity.InteractibleStates state;
  private Button mButton;
  private Toggle mToggle;

  private void Awake()
  {
    this.mButton = this.gameObject.GetComponent<Button>();
    this.mToggle = this.gameObject.GetComponent<Toggle>();
  }

  private void OnEnable()
  {
    this.UpdateInteractivity();
  }

  private void Update()
  {
    this.UpdateInteractivity();
  }

  private void UpdateInteractivity()
  {
    if (!Game.IsActive())
      return;
    switch (this.state)
    {
      case NavigationButtonInteractivity.InteractibleStates.PartDesignIdle:
        this.SetInteractability(Game.instance.player.team.carManager.carPartDesign.stage == CarPartDesign.Stage.Idle);
        break;
      case NavigationButtonInteractivity.InteractibleStates.CarDesignIdle:
        this.SetInteractability(Game.instance.player.team.carManager.nextYearCarDesign.state == NextYearCarDesign.State.WaitingForDesign);
        break;
      case NavigationButtonInteractivity.InteractibleStates.PartImprovementNotOnConditionFix:
        this.SetInteractability(!Game.instance.player.team.carManager.partImprovement.FixingCondition());
        break;
    }
  }

  private void SetInteractability(bool inValue)
  {
    if ((Object) this.mButton != (Object) null)
      GameUtility.SetInteractable(this.mButton, inValue);
    if (!((Object) this.mToggle != (Object) null))
      return;
    GameUtility.SetInteractable((Selectable) this.mToggle, inValue);
  }

  public enum InteractibleStates
  {
    PartDesignIdle,
    CarDesignIdle,
    PartImprovementNotOnConditionFix,
  }
}
