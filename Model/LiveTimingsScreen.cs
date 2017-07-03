// Decompiled with JetBrains decompiler
// Type: LiveTimingsScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LiveTimingsScreen : DataCenterScreen
{
  private SessionDetails.SessionType mSession = SessionDetails.SessionType.Qualifying;
  public UIGridList grid;
  public TextMeshProUGUI eventName;
  public GameObject[] headers;

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.SetSessionType();
    this.UpgradeGrid();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
  }

  public void SetSessionType()
  {
    this.mSession = Game.instance.sessionManager.eventDetails.currentSession.sessionType;
    switch (this.mSession)
    {
      case SessionDetails.SessionType.Practice:
        StringVariableParser.intValue1 = Game.instance.player.team.championship.eventNumber + 1;
        this.eventName.text = Localisation.LocaliseID("PSG_10010590", (GameObject) null);
        break;
      case SessionDetails.SessionType.Qualifying:
        StringVariableParser.intValue1 = Game.instance.player.team.championship.eventNumber + 1;
        this.eventName.text = Localisation.LocaliseID("PSG_10010591", (GameObject) null);
        if (Game.instance.sessionManager.eventDetails.hasSeveralQualifyingSessions)
        {
          switch (Game.instance.sessionManager.eventDetails.currentSession.sessionNumberForUI)
          {
          }
        }
        else
          break;
      case SessionDetails.SessionType.Race:
        StringVariableParser.intValue1 = Game.instance.player.team.championship.eventNumber + 1;
        this.eventName.text = Localisation.LocaliseID("PSG_10010592", (GameObject) null);
        break;
    }
    this.ActivateHeader((int) this.mSession);
  }

  public void ActivateHeader(int inIndex)
  {
    for (int index = 0; index < this.headers.Length; ++index)
      GameUtility.SetActive(this.headers[index].gameObject, index == inIndex);
  }

  public void UpgradeGrid()
  {
    this.grid.DestroyListItems();
    List<RacingVehicle> standings = Game.instance.sessionManager.standings;
    int count = standings.Count;
    for (int inIndex = 0; inIndex < count; ++inIndex)
    {
      RacingVehicle inVehicle = standings[inIndex];
      UILiveTimingsEntry liveTimingsEntry = this.grid.GetOrCreateItem<UILiveTimingsEntry>(inIndex);
      liveTimingsEntry.barType = this.GetBarType(inVehicle, inIndex);
      liveTimingsEntry.Setup(this.mSession, inVehicle);
    }
    GameUtility.SetActive(this.grid.itemPrefab, false);
    this.grid.ResetScrollbar();
  }

  public UILiveTimingsSharedEntry.BarType GetBarType(RacingVehicle inVehicle, int inIndex)
  {
    if (inVehicle.driver.IsPlayersDriver())
      return UILiveTimingsSharedEntry.BarType.PlayerOwned;
    return inIndex % 2 == 0 ? UILiveTimingsSharedEntry.BarType.Lighter : UILiveTimingsSharedEntry.BarType.Darker;
  }
}
