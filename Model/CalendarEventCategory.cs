using System;

[Flags]
public enum CalendarEventCategory : uint
{
  None = 0,
  Unknown = 1,
  Race = 2,
  Politics = 4,
  Design = 8,
  Build = 16,
  Championship = 32,
  Sponsor = 64,
  PreSeasonTest = 128,
  Contract = 256,
  DriverContract = 512,
  Scouting = 1024,
  HQ = 2048,
  DiggingUpDirt = 4096,
  TravelToEvent = 8192,
  Mail = 16384,
}
