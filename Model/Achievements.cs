// Decompiled with JetBrains decompiler
// Type: MM2.Achievements
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;

namespace MM2
{
  public class Achievements
  {
    public AchievementStatus[] achievementStatus = new AchievementStatus[61];

    public Achievements()
    {
      for (int index = 0; index < this.achievementStatus.Length; ++index)
        this.achievementStatus[index] = new AchievementStatus();
    }

    public static Achievements.AchievementEnum GetAchievementEnumByName(string name)
    {
      try
      {
        return (Achievements.AchievementEnum) Enum.Parse(typeof (Achievements.AchievementEnum), name);
      }
      catch (ArgumentException ex)
      {
        return Achievements.AchievementEnum.Invalid;
      }
    }

    public AchievementStatus GetAchievementStatusByName(string name)
    {
      Achievements.AchievementEnum achievementEnumByName = Achievements.GetAchievementEnumByName(name);
      if (achievementEnumByName == Achievements.AchievementEnum.Invalid)
        return (AchievementStatus) null;
      return this.achievementStatus[(int) achievementEnumByName];
    }

    public enum AchievementEnum
    {
      Hund_Millionaire,
      Ten_Pole_Positions_In_One_Season,
      Last_To_First_In_One_Race,
      Hire_A_Driver,
      Fire_A_Driver,
      Hire_Driver_20m_pa,
      Bottom_To_Top,
      Race_On_All_Track_Variations,
      Win_1_Race,
      Win_3_Races_In_A_Row,
      Win_Practice,
      Qualify_3_Poles_In_A_Row,
      Qualify_Pole_By_1s,
      Win_Race_By_1m,
      Finish_Race_With_5pct_Engine,
      Finish_Race_Using_All_Compounds,
      Reach_Max_Marketability,
      Fully_Upgrade_HQ,
      Podium_Reserve_Driver,
      Hire_Driver_Mech_Combo,
      Promote_Reserve_Driver,
      Both_Driver_Mech_Relationships_Maxed,
      Scout_Five_Star_Driver,
      Scout_Five_Star_Pot_Youngster,
      Max_Morale_Driver,
      Min_Morale_Driver,
      Win_A_Vote,
      Win_A_Vote_With_3_Power,
      Build_Level_5_Part,
      Fit_All_Level_5_Parts,
      Spend_30m_On_New_Car,
      Win_Any_Champ_With_Min_HQ,
      Sign_Sponsor_1m,
      Sign_Five_Star_Sponsor,
      Win_Team_Champ,
      Win_Both_Champ,
      Win_Driver_Champ,
      Win_Any_Champ_No_Part_Upgrades,
      Win_Any_Champ_No_Sponsors,
      Finish_A_Season,
      Win_7_Champs,
      Avoid_Chairman_Ultimatum,
      Get_Fired,
      Get_Hired,
      Bottom_To_Top_Manager,
      Rogue_Driver_Victory,
      Finish_First_Race,
      Caught_Breaking_Rules,
      Win_WMC_With_Silva,
      Win_Driver_Champ_Predator,
      Win_WMC_With_Kruger,
      Hire_Five_Star_Designer_Zamp,
      Team_Champ_Thornton,
      Win_Both_Champs_Together_Chariot,
      Create_A_Team,
      Win_Both_WMC_Custom_Team,
      Will_Evans_Win_Race,
      Win_By_2017_ERS_Predator,
      Win_By_2018_WMC_Kitano,
      Rafael_Win_2016_IGTC_Driver_as_Oranje,
      Win_Both_IGTC_Custom_Team,
      Count,
      Invalid,
    }

    public enum FloatStatsEnum
    {
      Most_Spent_On_A_Driver,
    }
  }
}
