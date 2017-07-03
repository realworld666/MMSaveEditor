// Decompiled with JetBrains decompiler
// Type: SessionFlagsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class SessionFlagsWidget : MonoBehaviour
{
  public FlagWidget[] flagWidgets;
  private SessionManager mSessionManager;

  public void OnEnter()
  {
    this.mSessionManager = Game.instance.sessionManager;
    this.mSessionManager.FlagChanged -= new Action(this.OnFlagChange);
    this.mSessionManager.FlagChanged += new Action(this.OnFlagChange);
  }

  private void OnDestroy()
  {
    if (this.mSessionManager == null)
      return;
    this.mSessionManager.FlagChanged -= new Action(this.OnFlagChange);
  }

  private void OnEnable()
  {
    for (int index = 0; index < this.flagWidgets.Length; ++index)
      this.flagWidgets[index].FlagChange();
  }

  private void OnDisable()
  {
    for (int index = 0; index < this.flagWidgets.Length; ++index)
      this.flagWidgets[index].Hide();
  }

  private void OnFlagChange()
  {
    for (int index = 0; index < this.flagWidgets.Length; ++index)
      this.flagWidgets[index].FlagChange();
  }
}
