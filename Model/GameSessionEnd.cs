// Decompiled with JetBrains decompiler
// Type: Sega.EventDefs.GameSessionEnd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;

namespace Sega.EventDefs
{
  internal class GameSessionEnd
  {
    public string eventID;
    public string eventName;
    public string eventTimestampClient;
    public string steamID;
    public string gamecode;
    public string sessionID;

    public GameSessionEnd()
    {
      this.eventID = "1.22.01";
      this.eventName = "Game Session-Close";
      this.eventTimestampClient = DateTime.Now.ToString("o");
    }
  }
}
