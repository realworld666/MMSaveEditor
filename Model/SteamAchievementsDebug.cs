// Decompiled with JetBrains decompiler
// Type: MM2.SteamAchievementsDebug
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using Steamworks;
using System;
using Wenzil.Console;

namespace MM2
{
  public static class SteamAchievementsDebug
  {
    public static void RegisterConsoleCommands()
    {
      ConsoleCommandsDatabase.RegisterCommand("ResetAllSteamStats", "Resets all Steam stats", "ResetAllSteamStats", new ConsoleCommandCallback(SteamAchievementsDebug.ResetAllSteamStats));
      ConsoleCommandsDatabase.RegisterCommand("ResetTracksRacedOn", "Resets the local cache of tracks raced on", "ResetTracksRacedOn", new ConsoleCommandCallback(CrossGameAchievementDataStorageSaveFile.Delete));
      ConsoleCommandsDatabase.RegisterCommand("ShowTracksRacedOn", "Displays a list of tracks raced on", "ShowTracksRacedOn", new ConsoleCommandCallback(CrossGameAchievementDataStorage.DisplayTracksRacedOn));
      ConsoleCommandsDatabase.RegisterCommand("ShowTracksNotRacedOn", "Displays a list of tracks remaining to be raced on", "ShowTracksNotRacedOn", new ConsoleCommandCallback(CrossGameAchievementDataStorage.DisplayTracksNotRacedOn));
      ConsoleCommandsDatabase.RegisterCommand("ResetAllSteamStatsAndAchievements", "Resets all Steam stats and achievements", "ResetAllSteamStatsAndAchievements", new ConsoleCommandCallback(SteamAchievementsDebug.ResetAllSteamStatsAndAchievements));
      ConsoleCommandsDatabase.RegisterCommand("ResetSteamAchievement", "Resets one named Steam achievement", "ResetSteamAchievement Acievement_Name", new ConsoleCommandCallback(SteamAchievementsDebug.ResetSteamAchievement));
      ConsoleCommandsDatabase.RegisterCommand("UnlockSteamAchievement", "Unlocks one named Steam achievement", "UnlockSteamAchievement Acievement_Name", new ConsoleCommandCallback(SteamAchievementsDebug.UnlockSteamAchievement));
      ConsoleCommandsDatabase.RegisterCommand("SetSteamFloatStat", "Sets one named Steam float stat to a specified value", "SetSteamFloatStat Stat_Name 123.456", new ConsoleCommandCallback(SteamAchievementsDebug.SetSteamFloatStat));
      ConsoleCommandsDatabase.RegisterCommand("SetSteamIntStat", "Sets one named Steam float stat to a specified value", "SetSteamIntStat Stat_Name 123", new ConsoleCommandCallback(SteamAchievementsDebug.SetSteamIntStat));
    }

    private static ConsoleCommandResult ResetAllSteamStats(params string[] inStrings)
    {
      return SteamAchievementsDebug.ResetAllSteamStatsImplementation(false, inStrings);
    }

    private static ConsoleCommandResult ResetAllSteamStatsAndAchievements(params string[] inStrings)
    {
      return SteamAchievementsDebug.ResetAllSteamStatsImplementation(true, inStrings);
    }

    private static ConsoleCommandResult ResetAllSteamStatsImplementation(bool achievementsToo, params string[] inStrings)
    {
      if ((UnityEngine.Object) App.instance == (UnityEngine.Object) null)
        return ConsoleCommandResult.Failed("No App");
      if (!SteamManager.Initialized)
        return ConsoleCommandResult.Failed("SteamManager is not initialized");
      if (!SteamUserStats.ResetAllStats(achievementsToo))
        return ConsoleCommandResult.Failed("Error returned from Steam");
      if (!SteamUserStats.StoreStats())
        return ConsoleCommandResult.Failed("Possibly failed; achievements/stats were cleared may not have been stored");
      return ConsoleCommandResult.Succeeded((string) null);
    }

    private static ConsoleCommandResult ResetSteamAchievement(params string[] inStrings)
    {
      if ((UnityEngine.Object) App.instance == (UnityEngine.Object) null)
        return ConsoleCommandResult.Failed("No App");
      if (!SteamManager.Initialized)
        return ConsoleCommandResult.Failed("SteamManager is not initialized");
      if (inStrings.Length != 1)
        return ConsoleCommandResult.Failed("ResetSteamAchievement expects one argument");
      if (!SteamUserStats.ClearAchievement(inStrings[0]))
        return ConsoleCommandResult.Failed("Error returned from Steam");
      if (!SteamUserStats.StoreStats())
        return ConsoleCommandResult.Failed("Possibly failed; achievement was cleared but may not have been stored");
      return ConsoleCommandResult.Succeeded((string) null);
    }

    private static ConsoleCommandResult UnlockSteamAchievement(params string[] inStrings)
    {
      if ((UnityEngine.Object) App.instance == (UnityEngine.Object) null)
        return ConsoleCommandResult.Failed("No App");
      if (!SteamManager.Initialized)
        return ConsoleCommandResult.Failed("SteamManager is not initialized");
      if (inStrings.Length != 1)
        return ConsoleCommandResult.Failed("UnlockAchievement expects one argument");
      if (!SteamUserStats.SetAchievement(inStrings[0]))
        return ConsoleCommandResult.Failed("Error returned from Steam");
      if (!SteamUserStats.StoreStats())
        return ConsoleCommandResult.Failed("Possibly failed; achievement was set but may not have been stored");
      return ConsoleCommandResult.Succeeded((string) null);
    }

    private static ConsoleCommandResult SetSteamFloatStat(params string[] inStrings)
    {
      if ((UnityEngine.Object) App.instance == (UnityEngine.Object) null)
        return ConsoleCommandResult.Failed("No App");
      if (!SteamManager.Initialized)
        return ConsoleCommandResult.Failed("SteamManager is not initialized");
      if (inStrings.Length != 2)
        return ConsoleCommandResult.Failed("SetSteamFloatStat expects two arguments");
      try
      {
        if (!SteamUserStats.SetStat(inStrings[0], Convert.ToSingle(inStrings[1])))
          return ConsoleCommandResult.Failed("Error returned from Steam");
        if (!SteamUserStats.StoreStats())
          return ConsoleCommandResult.Failed("Possibly failed; stat was set but may not have been stored");
      }
      catch (Exception ex)
      {
        return ConsoleCommandResult.Failed("Exception parsing arguments or running command: " + (object) ex);
      }
      return ConsoleCommandResult.Succeeded((string) null);
    }

    private static ConsoleCommandResult SetSteamIntStat(params string[] inStrings)
    {
      if ((UnityEngine.Object) App.instance == (UnityEngine.Object) null)
        return ConsoleCommandResult.Failed("No App");
      if (!SteamManager.Initialized)
        return ConsoleCommandResult.Failed("SteamManager is not initialized");
      if (inStrings.Length != 2)
        return ConsoleCommandResult.Failed("SetSteamFloatStat expects two arguments");
      try
      {
        if (!SteamUserStats.SetStat(inStrings[0], Convert.ToInt32(inStrings[1])))
          return ConsoleCommandResult.Failed("Error returned from Steam");
        if (!SteamUserStats.StoreStats())
          return ConsoleCommandResult.Failed("Possibly failed; stat was set but may not have been stored");
      }
      catch (Exception ex)
      {
        return ConsoleCommandResult.Failed("Exception parsing arguments or running command: " + (object) ex);
      }
      return ConsoleCommandResult.Succeeded((string) null);
    }
  }
}
