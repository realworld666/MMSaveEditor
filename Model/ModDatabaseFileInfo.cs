// Decompiled with JetBrains decompiler
// Type: ModdingSystem.ModDatabaseFileInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ModdingSystem
{
  public class ModDatabaseFileInfo : ModFileInfo
  {
    public static readonly string[] databaseTags = new string[9]{ "General", "Drivers", "Staff", "Sponsors", "Teams", "Politics", "Chairmen", "Media", "Weather" };
    public static readonly string[] databaseTagsIDs = new string[9]{ "PSG_10005800", "PSG_10006093", "PSG_10003781", "PSG_10003782", "PSG_10002271", "PSG_10003783", "PSG_10003555", "PSG_10001563", "PSG_10004341" };
    private ModDatabaseFileInfo.DatabaseType mDatabaseType = ModDatabaseFileInfo.DatabaseType.None;

    public override ModFileInfo.ModFileInfoType fileInfoType
    {
      get
      {
        return ModFileInfo.ModFileInfoType.Database;
      }
    }

    public ModDatabaseFileInfo.DatabaseType databaseType
    {
      get
      {
        return this.mDatabaseType;
      }
    }

    public override void LoadDataFromFileInfo(FileInfo inFileInfo)
    {
      base.LoadDataFromFileInfo(inFileInfo);
      string nameWithNoExtension = this.GetFileNameWithNoExtension();
      if (nameWithNoExtension != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (ModDatabaseFileInfo.\u003C\u003Ef__switch\u0024map38 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ModDatabaseFileInfo.\u003C\u003Ef__switch\u0024map38 = new Dictionary<string, int>(27)
          {
            {
              "Teams",
              0
            },
            {
              "Championships",
              1
            },
            {
              "Drivers",
              2
            },
            {
              "Engineers",
              3
            },
            {
              "Mechanics",
              4
            },
            {
              "Sponsors",
              5
            },
            {
              "Assistants",
              6
            },
            {
              "Chairman",
              7
            },
            {
              "Climate",
              8
            },
            {
              "Journalists",
              9
            },
            {
              "Politicians",
              10
            },
            {
              "Media Outlets",
              11
            },
            {
              "Team Principals",
              12
            },
            {
              "Scouts",
              13
            },
            {
              "Simulation Settings",
              14
            },
            {
              "Team Colours",
              15
            },
            {
              "Default Parts",
              16
            },
            {
              "Parts",
              17
            },
            {
              "Part Suppliers",
              18
            },
            {
              "Chassis",
              19
            },
            {
              "HQ",
              20
            },
            {
              "Personality Traits",
              21
            },
            {
              "Part Components",
              22
            },
            {
              "Driver Stat Progression",
              23
            },
            {
              "Engineer Stat Progression",
              24
            },
            {
              "Mechanic Stat Progression",
              25
            },
            {
              "Buildings",
              26
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (ModDatabaseFileInfo.\u003C\u003Ef__switch\u0024map38.TryGetValue(nameWithNoExtension, out num))
        {
          switch (num)
          {
            case 0:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.Teams;
              return;
            case 1:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.Championships;
              return;
            case 2:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.Drivers;
              return;
            case 3:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.Engineers;
              return;
            case 4:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.Mechanics;
              return;
            case 5:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.Sponsors;
              return;
            case 6:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.Assistants;
              return;
            case 7:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.Chairman;
              return;
            case 8:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.Climate;
              return;
            case 9:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.Journalists;
              return;
            case 10:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.Politicians;
              return;
            case 11:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.MediaOutlets;
              return;
            case 12:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.TeamPrincipals;
              return;
            case 13:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.Scouts;
              return;
            case 14:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.SimulationSettings;
              return;
            case 15:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.TeamColours;
              return;
            case 16:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.DefaultParts;
              return;
            case 17:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.Parts;
              return;
            case 18:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.PartSuppliers;
              return;
            case 19:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.Chassis;
              return;
            case 20:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.HQ;
              return;
            case 21:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.PersonalityTraits;
              return;
            case 22:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.PartComponents;
              return;
            case 23:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.DriverStatsProgression;
              return;
            case 24:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.EngineerStatsProgression;
              return;
            case 25:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.MechanicStatsProgression;
              return;
            case 26:
              this.mDatabaseType = ModDatabaseFileInfo.DatabaseType.Buildings;
              return;
          }
        }
      }
      Debug.Log((object) ("ModdingSystem::ModDatabaseFiloInfo - Not supporting database \"" + this.mFileInfo.Name + "\" at the moment."), (Object) null);
    }

    public List<DatabaseEntry> ReadDatabase()
    {
      return DatabaseReader.LoadFromText(File.ReadAllText(this.fileInfo.FullName), this.fileInfo.FullName);
    }

    public override string GetModFileTag()
    {
      switch (this.mDatabaseType)
      {
        case ModDatabaseFileInfo.DatabaseType.Teams:
          return ModDatabaseFileInfo.databaseTags[4];
        case ModDatabaseFileInfo.DatabaseType.Championships:
        case ModDatabaseFileInfo.DatabaseType.SimulationSettings:
        case ModDatabaseFileInfo.DatabaseType.TeamColours:
        case ModDatabaseFileInfo.DatabaseType.DefaultParts:
        case ModDatabaseFileInfo.DatabaseType.Parts:
        case ModDatabaseFileInfo.DatabaseType.PartSuppliers:
        case ModDatabaseFileInfo.DatabaseType.Chassis:
        case ModDatabaseFileInfo.DatabaseType.HQ:
        case ModDatabaseFileInfo.DatabaseType.PersonalityTraits:
        case ModDatabaseFileInfo.DatabaseType.PartComponents:
        case ModDatabaseFileInfo.DatabaseType.DriverStatsProgression:
        case ModDatabaseFileInfo.DatabaseType.EngineerStatsProgression:
        case ModDatabaseFileInfo.DatabaseType.MechanicStatsProgression:
        case ModDatabaseFileInfo.DatabaseType.Buildings:
          return ModDatabaseFileInfo.databaseTags[0];
        case ModDatabaseFileInfo.DatabaseType.Drivers:
          return ModDatabaseFileInfo.databaseTags[1];
        case ModDatabaseFileInfo.DatabaseType.Engineers:
        case ModDatabaseFileInfo.DatabaseType.Mechanics:
        case ModDatabaseFileInfo.DatabaseType.TeamPrincipals:
        case ModDatabaseFileInfo.DatabaseType.Assistants:
        case ModDatabaseFileInfo.DatabaseType.Scouts:
          return ModDatabaseFileInfo.databaseTags[2];
        case ModDatabaseFileInfo.DatabaseType.Sponsors:
          return ModDatabaseFileInfo.databaseTags[3];
        case ModDatabaseFileInfo.DatabaseType.Chairman:
          return ModDatabaseFileInfo.databaseTags[6];
        case ModDatabaseFileInfo.DatabaseType.Journalists:
        case ModDatabaseFileInfo.DatabaseType.MediaOutlets:
          return ModDatabaseFileInfo.databaseTags[7];
        case ModDatabaseFileInfo.DatabaseType.Politicians:
          return ModDatabaseFileInfo.databaseTags[5];
        case ModDatabaseFileInfo.DatabaseType.Climate:
          return ModDatabaseFileInfo.databaseTags[8];
        default:
          return (string) null;
      }
    }

    public enum DatabaseType
    {
      Teams,
      Championships,
      Drivers,
      Engineers,
      Mechanics,
      Sponsors,
      TeamPrincipals,
      Chairman,
      Assistants,
      Scouts,
      Journalists,
      MediaOutlets,
      Politicians,
      Climate,
      SimulationSettings,
      TeamColours,
      DefaultParts,
      Parts,
      PartSuppliers,
      Chassis,
      HQ,
      PersonalityTraits,
      PartComponents,
      DriverStatsProgression,
      EngineerStatsProgression,
      MechanicStatsProgression,
      Buildings,
      None,
    }
  }
}
