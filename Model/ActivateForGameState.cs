// Decompiled with JetBrains decompiler
// Type: ActivateForGameState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

public class ActivateForGameState : MonoBehaviour
{
  public ActivateForGameState.Type type = ActivateForGameState.Type.SetInteractable;
  public GameState.Type[] availableStates;

  public void OnStart()
  {
    if (this.type != ActivateForGameState.Type.EnableObject)
      return;
    UIManager.OnScreenChange += new Action(this.UpdateState);
    GameStateManager.OnStateChange += new Action(this.UpdateState);
  }

  public void OnUnload()
  {
    if (this.type != ActivateForGameState.Type.EnableObject)
      return;
    UIManager.OnScreenChange -= new Action(this.UpdateState);
    GameStateManager.OnStateChange -= new Action(this.UpdateState);
  }

  private void OnEnable()
  {
    if (this.type != ActivateForGameState.Type.SetInteractable)
      return;
    this.UpdateState();
  }

  public void UpdateState()
  {
    if ((UnityEngine.Object) this.gameObject != (UnityEngine.Object) null)
    {
      bool flag = false;
      if (this.availableStates != null && this.availableStates.Length > 0)
      {
        for (int index = 0; index < this.availableStates.Length; ++index)
        {
          flag = App.instance.gameStateManager.currentState.type == this.availableStates[index];
          if (flag)
            break;
        }
      }
      if (this.type == ActivateForGameState.Type.EnableObject)
      {
        this.gameObject.SetActive(flag);
      }
      else
      {
        if (this.type != ActivateForGameState.Type.SetInteractable)
          return;
        Toggle component1 = this.GetComponent<Toggle>();
        if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
          component1.interactable = flag;
        Button component2 = this.GetComponent<Button>();
        if (!((UnityEngine.Object) component2 != (UnityEngine.Object) null))
          return;
        component2.interactable = flag;
      }
    }
    else
    {
      int num = 0 + 1;
    }
  }

  public enum Type
  {
    EnableObject,
    SetInteractable,
  }
}
