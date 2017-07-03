// Decompiled with JetBrains decompiler
// Type: UILiveTimingsEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UILiveTimingsEntry : MonoBehaviour
{
  public UILiveTimingsSharedEntry.BarType barType;
  public UILiveTimingsPractiseEntry practise;
  public UILiveTimingsQualifyEntry qualify;
  public UILiveTimingsRaceEntry race;

  public void OnStart(SessionDetails.SessionType inSessionType)
  {
    switch (inSessionType)
    {
      case SessionDetails.SessionType.Practice:
        this.practise.OnStart();
        break;
      case SessionDetails.SessionType.Qualifying:
        this.qualify.OnStart();
        break;
      case SessionDetails.SessionType.Race:
        this.race.OnStart();
        break;
    }
  }

  public void Setup(SessionDetails.SessionType inSessionType, RacingVehicle inVehicle)
  {
    GameUtility.SetActive(this.practise.gameObject, inSessionType == SessionDetails.SessionType.Practice);
    GameUtility.SetActive(this.qualify.gameObject, inSessionType == SessionDetails.SessionType.Qualifying);
    GameUtility.SetActive(this.race.gameObject, inSessionType == SessionDetails.SessionType.Race);
    switch (inSessionType)
    {
      case SessionDetails.SessionType.Practice:
        this.practise.barType = this.barType;
        this.practise.Setup(inVehicle);
        break;
      case SessionDetails.SessionType.Qualifying:
        this.qualify.barType = this.barType;
        this.qualify.Setup(inVehicle);
        break;
      case SessionDetails.SessionType.Race:
        this.race.barType = this.barType;
        this.race.Setup(inVehicle);
        break;
    }
  }
}
