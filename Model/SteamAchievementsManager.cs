// Decompiled with JetBrains decompiler
// Type: MM2.SteamAchievementsManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using Steamworks;
using UnityEngine;

namespace MM2
{
  public class SteamAchievementsManager
  {
    private static Achievements.AchievementEnum[] unlockableInSingleRace = new Achievements.AchievementEnum[9]{ Achievements.AchievementEnum.Finish_First_Race, Achievements.AchievementEnum.Finish_Race_Using_All_Compounds, Achievements.AchievementEnum.Finish_Race_With_5pct_Engine, Achievements.AchievementEnum.Last_To_First_In_One_Race, Achievements.AchievementEnum.Qualify_Pole_By_1s, Achievements.AchievementEnum.Race_On_All_Track_Variations, Achievements.AchievementEnum.Win_1_Race, Achievements.AchievementEnum.Win_Practice, Achievements.AchievementEnum.Win_Race_By_1m };
    public AchievementSteamData[] achievementSteamData = new AchievementSteamData[61];
    private Achievements _achievements;
    private Callback<UserStatsReceived_t> _userStatsReceived;
    private Callback<UserStatsStored_t> _userStatsStored;
    private Callback<UserAchievementStored_t> _userAchievementStored;

    public SteamAchievementsManager(Achievements inAchievements)
    {
      this.InitialiseAchievementSteamData();
      if (!SteamManager.Initialized)
        return;
      this._achievements = inAchievements;
      this.InitialiseSteamCallbacks();
      if (!SteamUserStats.RequestCurrentStats())
        UnityEngine.Debug.LogWarning((object) "SteamUserStats.RequestCurrentStats failed to send");
      SteamUtils.SetOverlayNotificationPosition(ENotificationPosition.k_EPositionTopLeft);
    }

    private void InitialiseAchievementSteamData()
    {
      for (int index = 0; index < this.achievementSteamData.Length; ++index)
        this.achievementSteamData[index] = new AchievementSteamData();
      this.achievementSteamData[5].unlockedViaStat = true;
    }

    private void InitialiseSteamCallbacks()
    {
      this._userStatsReceived = Callback<UserStatsReceived_t>.Create(new Callback<UserStatsReceived_t>.DispatchDelegate(this.OnUserStatsReceived));
      if (this._userStatsReceived == null)
        UnityEngine.Debug.LogWarning((object) "Failed to create UserStatsReceived callback");
      this._userStatsStored = Callback<UserStatsStored_t>.Create(new Callback<UserStatsStored_t>.DispatchDelegate(this.OnUserStatsStored));
      if (this._userStatsStored == null)
        UnityEngine.Debug.LogWarning((object) "Failed to create UserStatsReceived callback");
      this._userAchievementStored = Callback<UserAchievementStored_t>.Create(new Callback<UserAchievementStored_t>.DispatchDelegate(this.OnUserAchievementStored));
      if (this._userAchievementStored != null)
        return;
      UnityEngine.Debug.LogWarning((object) "Failed to create UserStatsReceived callback");
    }

    private void OnUserStatsReceived(UserStatsReceived_t inResult)
    {
      if ((long) inResult.m_nGameID != (long) SteamManager.MM2AppIDAsLong)
        UnityEngine.Debug.LogWarning((object) "OnUserStatsReceived received callback for wrong game.");
      else if (inResult.m_eResult != EResult.k_EResultOK)
      {
        UnityEngine.Debug.LogWarning((object) "OnUserStatsReceived received failure");
      }
      else
      {
        for (int index = 0; index < 61; ++index)
        {
          string pchName = ((Achievements.AchievementEnum) index).ToString();
          AchievementStatus achievementStatu = this._achievements.achievementStatus[index];
          bool pbAchieved;
          uint punUnlockTime;
          if (!SteamUserStats.GetAchievementAndUnlockTime(pchName, out pbAchieved, out punUnlockTime))
          {
            UnityEngine.Debug.LogWarningFormat("SteamUserStats.GetAchievement failed for {0}", (object) pchName);
          }
          else
          {
            achievementStatu.achieved = pbAchieved;
            achievementStatu.unlockedTime = GameUtility.FromUnixTime(punUnlockTime);
          }
          AchievementSteamData achievementSteamData = this.achievementSteamData[index];
          achievementSteamData.name = SteamUserStats.GetAchievementDisplayAttribute(pchName, "name");
          achievementSteamData.description = SteamUserStats.GetAchievementDisplayAttribute(pchName, "desc");
          achievementSteamData.hidden = SteamUserStats.GetAchievementDisplayAttribute(pchName, "hidden") == "1";
        }
        for (uint iAchievement = 0; iAchievement < SteamUserStats.GetNumAchievements(); ++iAchievement)
        {
          string achievementName = SteamUserStats.GetAchievementName(iAchievement);
          if (Achievements.GetAchievementEnumByName(achievementName) == Achievements.AchievementEnum.Invalid)
            UnityEngine.Debug.LogWarningFormat("Steam lists an achievement that the game doesn't know about: {0}", (object) achievementName);
        }
      }
    }

    private void OnUserStatsStored(UserStatsStored_t inResult)
    {
      if ((long) inResult.m_nGameID != (long) SteamManager.MM2AppIDAsLong)
      {
        UnityEngine.Debug.LogWarning((object) "OnUserStatsStored received callback for wrong game.");
      }
      else
      {
        if (inResult.m_eResult == EResult.k_EResultOK)
          return;
        UnityEngine.Debug.LogWarning((object) "OnUserStatsStored received failure");
      }
    }

    private void OnUserAchievementStored(UserAchievementStored_t inResult)
    {
      if ((long) inResult.m_nGameID != (long) SteamManager.MM2AppIDAsLong)
      {
        UnityEngine.Debug.LogWarning((object) "OnUserAchievementStored received callback for wrong game.");
      }
      else
      {
        string rgchAchievementName = inResult.m_rgchAchievementName;
        AchievementStatus achievementStatusByName = this._achievements.GetAchievementStatusByName(rgchAchievementName);
        if (achievementStatusByName == null)
        {
          UnityEngine.Debug.LogWarningFormat("OnUserAchievementStored received achievement callback for {0}, which doesn't exist locally", (object) rgchAchievementName);
        }
        else
        {
          bool pbAchieved;
          uint punUnlockTime;
          if (!SteamUserStats.GetAchievementAndUnlockTime(rgchAchievementName, out pbAchieved, out punUnlockTime))
          {
            UnityEngine.Debug.LogWarningFormat("SteamUserStats.GetAchievement failed for {0}", (object) rgchAchievementName);
          }
          else
          {
            achievementStatusByName.achieved = pbAchieved;
            achievementStatusByName.unlockedTime = GameUtility.FromUnixTime(punUnlockTime);
          }
          achievementStatusByName.achieved = pbAchieved;
        }
      }
    }

    public bool ShouldCheckToUnlockAchievement(Achievements.AchievementEnum inAchievement)
    {
      if (!SteamManager.Initialized)
        return false;
      AchievementStatus achievementStatu = this._achievements.achievementStatus[(int) inAchievement];
      if (!achievementStatu.achieved)
        return !achievementStatu.pending;
      return false;
    }

    public void UnlockAchievement(Achievements.AchievementEnum inAchievement)
    {
      if (!SteamManager.Initialized)
        return;
      GameUtility.Assert(!this.achievementSteamData[(int) inAchievement].unlockedViaStat, "SteamAchievements.UnlockAchievement: Attempting to unlock an achievement that should be unlocked automatically using stats. Did you forget to update a stat? Achievement name: " + (object) inAchievement, (Object) null);
      AchievementStatus achievementStatu = this._achievements.achievementStatus[(int) inAchievement];
      if (achievementStatu.achieved || achievementStatu.pending)
        return;
      if (Game.instance.gameType == Game.GameType.SingleEvent)
      {
        bool flag = false;
        for (int index = 0; index < SteamAchievementsManager.unlockableInSingleRace.Length; ++index)
        {
          if (SteamAchievementsManager.unlockableInSingleRace[index] == inAchievement)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          return;
      }
      if (!SteamUserStats.SetAchievement(inAchievement.ToString()))
      {
        UnityEngine.Debug.LogWarningFormat("Failed to unlock steam achievement \"{0}\"", (object) inAchievement);
      }
      else
      {
        this._achievements.achievementStatus[(int) inAchievement].pending = true;
        SteamUserStats.StoreStats();
      }
    }

    public void SetFloatStat(Achievements.FloatStatsEnum inStat, float inValue)
    {
      if (!SteamManager.Initialized)
        return;
      if (!SteamUserStats.SetStat(inStat.ToString(), inValue))
      {
        UnityEngine.Debug.LogWarningFormat("Failed to set Steam float stat \"{0}\" to \"{1}\"", new object[2]
        {
          (object) inStat,
          (object) inValue
        });
      }
      else
      {
        if (SteamUserStats.StoreStats())
          return;
        UnityEngine.Debug.LogWarning((object) "Failed to store Steam stats");
      }
    }

    public bool isAchievementUnlocked(Achievements.AchievementEnum inAchievement)
    {
      if (!SteamManager.Initialized)
        return false;
      AchievementStatus achievementStatu = this._achievements.achievementStatus[(int) inAchievement];
      return achievementStatu.achieved || achievementStatu.pending;
    }
  }
}
