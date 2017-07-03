// Decompiled with JetBrains decompiler
// Type: ActivateForSession
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

public class ActivateForSession : MonoBehaviour
{
  public ActivateForSession.Type type = ActivateForSession.Type.SetInteractable;
  public ActivateForSession.AvailableSession[] availableSessions;

  public void OnStart()
  {
    if (this.type != ActivateForSession.Type.EnableObject)
      return;
    UIManager.OnScreenChange += new Action(this.UpdateState);
    GameStateManager.OnStateChange += new Action(this.UpdateState);
  }

  public void OnUnload()
  {
    if (this.type != ActivateForSession.Type.EnableObject)
      return;
    UIManager.OnScreenChange -= new Action(this.UpdateState);
    GameStateManager.OnStateChange -= new Action(this.UpdateState);
  }

  private void OnEnable()
  {
    if (this.type != ActivateForSession.Type.SetInteractable)
      return;
    this.UpdateState();
  }

  public void UpdateState()
  {
    if (!Game.IsActive() || Game.instance.sessionManager.eventDetails == null)
      return;
    SessionDetails.SessionType sessionType = Game.instance.sessionManager.eventDetails.currentSession.sessionType;
    if (this.availableSessions == null || this.availableSessions.Length <= 0)
      return;
    bool inIsActive = false;
    for (int index = 0; index < this.availableSessions.Length; ++index)
    {
      switch (this.availableSessions[index])
      {
        case ActivateForSession.AvailableSession.Practice:
          if (sessionType == SessionDetails.SessionType.Practice)
          {
            inIsActive = true;
            break;
          }
          break;
        case ActivateForSession.AvailableSession.Qualifying:
          if (sessionType == SessionDetails.SessionType.Qualifying)
          {
            inIsActive = true;
            break;
          }
          break;
        case ActivateForSession.AvailableSession.Race:
          if (sessionType == SessionDetails.SessionType.Race)
          {
            inIsActive = true;
            break;
          }
          break;
        case ActivateForSession.AvailableSession.EventEnded:
          if (Game.instance.sessionManager.eventDetails.hasEventEnded)
          {
            inIsActive = true;
            break;
          }
          break;
      }
    }
    if (this.type == ActivateForSession.Type.EnableObject)
    {
      GameUtility.SetActive(this.gameObject, inIsActive);
    }
    else
    {
      if (this.type != ActivateForSession.Type.SetInteractable)
        return;
      Toggle component1 = this.GetComponent<Toggle>();
      if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
        component1.interactable = inIsActive;
      Button component2 = this.GetComponent<Button>();
      if (!((UnityEngine.Object) component2 != (UnityEngine.Object) null))
        return;
      component2.interactable = inIsActive;
    }
  }

  public enum Type
  {
    EnableObject,
    SetInteractable,
  }

  public enum AvailableSession
  {
    Practice,
    Qualifying,
    Race,
    EventEnded,
  }
}
