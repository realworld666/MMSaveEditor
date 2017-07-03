// Decompiled with JetBrains decompiler
// Type: Sega.RaceEnd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;

namespace Sega
{
  internal class RaceEnd
  {
    public string eventID;
    public string eventName;
    public string eventTimestampClient;
    public string steamID;
    public string gamecode;
    public string sessionID;
    public string TeamName;
    public string TrackName;
    public string TrackLayout;
    public int D1RacePosition;
    public int D1GridPosition;
    public float D1BestLapTime;
    public int D1Laps;
    public int D1Stops;
    public int D2RacePosition;
    public int D2GridPosition;
    public float D2BestLapTime;
    public int D2Laps;
    public int D2Stops;

    public RaceEnd()
    {
      this.eventID = "4.1.01";
      this.eventName = "Race End";
      this.eventTimestampClient = DateTime.Now.ToString("o");
    }
  }
}
