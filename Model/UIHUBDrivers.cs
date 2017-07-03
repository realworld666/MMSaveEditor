// Decompiled with JetBrains decompiler
// Type: UIHUBDrivers
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIHUBDrivers : MonoBehaviour
{
  public UIHUBDriverEntry[] entries;
  public UIHUBSelection widget;
  private Team mTeam;
  private Driver[] mSelectedDrivers;
  private SessionDetails.SessionType mSessionType;
  private bool mCanDisplayWarningMessage;
  private bool mDisplayDriverNotSelectedWarning;

  public bool selectionComplete
  {
    get
    {
      return this.mTeam.IsDriverSelectionForSessionComplete();
    }
  }

  public void Setup()
  {
    this.mTeam = Game.instance.player.team;
    this.mSessionType = Game.instance.sessionManager.eventDetails.currentSession.sessionType;
    this.mCanDisplayWarningMessage = this.mSessionType == SessionDetails.SessionType.Practice;
    this.UpdateDrivers();
  }

  private void UpdateDrivers()
  {
    this.mSelectedDrivers = this.mTeam.selectedDrivers;
    this.mDisplayDriverNotSelectedWarning = false;
    for (int inIndex = 0; inIndex < this.entries.Length; ++inIndex)
    {
      if (this.mSelectedDrivers[inIndex] != null)
      {
        this.entries[inIndex].Setup(this.mSelectedDrivers[inIndex]);
      }
      else
      {
        if (this.mSessionType != SessionDetails.SessionType.Practice)
          this.entries[inIndex].Setup(this.mTeam.GetSelectedDriver(inIndex));
        else
          this.entries[inIndex].Clear();
        this.mDisplayDriverNotSelectedWarning = true;
      }
    }
    UIManager.instance.GetScreen<SessionHUBScreen>().showDriverWarning = this.mCanDisplayWarningMessage && this.mDisplayDriverNotSelectedWarning;
    this.SetDriverNames();
  }

  private void SetDriverNames()
  {
    for (int index = 0; index < this.entries.Length; ++index)
      this.widget.SetDriverName(this.entries[index].driver, index == 0);
  }

  public void AddDriver(Driver inDriver)
  {
    if (!this.mTeam.IsDriverSelectedForSession(inDriver))
      this.mTeam.SelectDriverForSession(inDriver);
    this.UpdateDrivers();
  }

  public void RemoveDriver(Driver inDriver)
  {
    if (this.mTeam.IsDriverSelectedForSession(inDriver))
      this.mTeam.DeselectDriverForSession(inDriver);
    this.UpdateDrivers();
  }
}
