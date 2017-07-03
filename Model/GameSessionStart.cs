// Decompiled with JetBrains decompiler
// Type: Sega.EventDefs.GameSessionStart
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;

namespace Sega.EventDefs
{
  internal class GameSessionStart
  {
    public string eventID;
    public string eventName;
    public string eventTimestampClient;
    public string customerGUID;
    public string steamID;
    public string crc;
    public string hwid;
    public string gamecode;
    public string sessionID;
    public string system_os;
    public string system_processor;
    public string system_memory;
    public string system_display;
    public string system_display_memory;

    public GameSessionStart()
    {
      this.eventID = "1.21.01";
      this.eventName = "Game Session-Open";
      this.eventTimestampClient = DateTime.Now.ToString("o");
    }
  }
}
