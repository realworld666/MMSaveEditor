// Decompiled with JetBrains decompiler
// Type: VotesManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class VotesManager
{
  public Dictionary<int, PoliticalVote> votes = new Dictionary<int, PoliticalVote>();
  public List<int> voteIDs = new List<int>();

  public void LoadVotesFromDatabase(Database database)
  {
    List<DatabaseEntry> votesData = database.votesData;
    for (int index = 0; index < votesData.Count; ++index)
    {
      DatabaseEntry databaseEntry = votesData[index];
      PoliticalVote inVote = new PoliticalVote();
      if (Enum.IsDefined(typeof (CarPart.PartType), (object) databaseEntry.GetStringValue("Object Type")))
        inVote.currentPartType = (CarPart.PartType) Enum.Parse(typeof (CarPart.PartType), databaseEntry.GetStringValue("Object Type"));
      inVote.displayRule = databaseEntry.GetStringValue("Display Rule") == "TRUE";
      inVote.group = databaseEntry.GetStringValue("Rule Group");
      inVote.nameID = databaseEntry.GetStringValue("Rule Name ID");
      inVote.descriptionID = databaseEntry.GetStringValue("Rule Description ID");
      inVote.ID = databaseEntry.GetIntValue("ID");
      if (inVote.ID != 71 && inVote.ID != 72)
      {
        inVote.effectType = databaseEntry.GetStringValue("Effect Type");
        inVote.impacts = VotesManager.GetImpacts(inVote.effectType, inVote);
        inVote.messageCriteria = VotesManager.GetMessageCriteria(databaseEntry.GetStringValue("Message Trigger"));
        string stringValue1 = databaseEntry.GetStringValue("Benefits");
        char[] chArray1 = new char[1]{ ';' };
        foreach (string str1 in stringValue1.Split(chArray1))
        {
          string str2 = str1.Trim();
          if (!Enum.IsDefined(typeof (PoliticalVote.TeamCharacteristics), (object) str2))
            Debug.LogWarning((object) (34.ToString() + str2 + (object) '"' + " does not exist in the teamCharacteristicsEnum"), (UnityEngine.Object) null);
          else
            inVote.benificialCharacteristics.Add((PoliticalVote.TeamCharacteristics) Enum.Parse(typeof (PoliticalVote.TeamCharacteristics), str2));
        }
        string stringValue2 = databaseEntry.GetStringValue("Challenges");
        char[] chArray2 = new char[1]{ ';' };
        foreach (string str1 in stringValue2.Split(chArray2))
        {
          string str2 = str1.Trim();
          if (!Enum.IsDefined(typeof (PoliticalVote.TeamCharacteristics), (object) str2))
            Debug.LogWarning((object) (34.ToString() + str2 + (object) '"' + " does not exist in the teamCharacteristicsEnum"), (UnityEngine.Object) null);
          else
            inVote.detrimentalCharacteristics.Add((PoliticalVote.TeamCharacteristics) Enum.Parse(typeof (PoliticalVote.TeamCharacteristics), str2));
        }
        if (this.votes.ContainsKey(inVote.ID))
          Debug.LogWarningFormat("Rule Changes database contains rules with duplicate IDs: {0}", (object) inVote.ID);
        this.votes.Add(inVote.ID, inVote);
        this.voteIDs.Add(inVote.ID);
      }
    }
    Debug.Log((object) "RulesLoaded", (UnityEngine.Object) null);
  }

  private static DialogQuery GetMessageCriteria(string inString)
  {
    DialogQuery dialogQuery = new DialogQuery();
    inString = inString.Trim();
    string[] strArray1 = inString.Split(';');
    dialogQuery.who = new DialogCriteria("Who", "MediaPerson");
    dialogQuery.criteriaList.Add(new DialogCriteria()
    {
      mType = "Type",
      mCriteriaInfo = "Header"
    });
    for (int index1 = 0; index1 < strArray1.Length; ++index1)
    {
      DialogCriteria dialogCriteria = new DialogCriteria();
      string[] strArray2 = strArray1[index1].Split('=');
      for (int index2 = 0; index2 < strArray2.Length; ++index2)
      {
        strArray2[index2] = strArray2[index2].Trim();
        if (index2 == 0)
          dialogCriteria.mType = strArray2[index2];
        else if (index2 == 1)
          dialogCriteria.mCriteriaInfo = strArray2[index2];
        else
          Debug.Log((object) "Error, criteria split in more than 2 strings, database entry badly formated", (UnityEngine.Object) null);
      }
      if (dialogCriteria.mType == "Who")
        dialogQuery.who = dialogCriteria;
      else
        dialogQuery.criteriaList.Add(dialogCriteria);
    }
    return dialogQuery;
  }

  public static List<PoliticalImpact> GetImpacts(string inString, PoliticalVote inVote)
  {
    List<PoliticalImpact> politicalImpactList = new List<PoliticalImpact>();
    string str1 = inString;
    char[] chArray = new char[1]{ ';' };
    foreach (string str2 in str1.Split(chArray))
    {
      string str3 = str2.Trim();
      string inName = string.Empty;
      string inEffect = string.Empty;
      bool flag = false;
      for (int index = 0; index < str3.Length; ++index)
      {
        if (!flag)
        {
          if ((int) str3[index] != 40)
            inName = inName.Insert(inName.Length, str3[index].ToString());
          else if ((int) str3[index] == 40)
            flag = true;
        }
        else if ((int) str3[index] != 40 && (int) str3[index] != 41)
          inEffect = inEffect.Insert(inEffect.Length, str3[index].ToString());
      }
      PoliticalImpact impact = VotesManager.CreateImpact(inName, inEffect, inVote);
      if (impact != null)
        politicalImpactList.Add(impact);
    }
    return politicalImpactList;
  }

  private static PoliticalImpact CreateImpact(string inName, string inEffect, PoliticalVote inVote)
  {
    inEffect = inEffect.Trim();
    inName = inName.Trim();
    string key = inName;
    if (key != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (VotesManager.\u003C\u003Ef__switch\u0024map24 == null)
      {
        // ISSUE: reference to a compiler-generated field
        VotesManager.\u003C\u003Ef__switch\u0024map24 = new Dictionary<string, int>(32)
        {
          {
            "Points",
            0
          },
          {
            "FinalRacePointsDoubled",
            0
          },
          {
            "FastestLapBonus",
            0
          },
          {
            "PoleBonus",
            0
          },
          {
            "TyreSpeed",
            1
          },
          {
            "TyreSupplierSpeedBonus",
            1
          },
          {
            "TyreWearRate",
            1
          },
          {
            "TyreSupplier",
            1
          },
          {
            "TyreType",
            1
          },
          {
            "TyresAvailable",
            1
          },
          {
            "TyreCompoundChoice",
            1
          },
          {
            "TyreCompoundsAvailable",
            1
          },
          {
            "PitlaneSpeed",
            2
          },
          {
            "EnergySystem",
            3
          },
          {
            "FuelLimit",
            4
          },
          {
            "Refuelling",
            4
          },
          {
            "ChangeTrackLayout",
            5
          },
          {
            "ReplaceTrack",
            5
          },
          {
            "AddTrack",
            5
          },
          {
            "RemoveTrack",
            5
          },
          {
            "AddLayout",
            5
          },
          {
            "Practice",
            6
          },
          {
            "Qualifying",
            6
          },
          {
            "Race",
            6
          },
          {
            "PrizePoolCashGap",
            7
          },
          {
            "SafetyCar",
            8
          },
          {
            "SpecPart",
            9
          },
          {
            "Grid",
            10
          },
          {
            "PitStopCrew",
            11
          },
          {
            "Sprinkler",
            12
          },
          {
            "LastPlaceBonus",
            13
          },
          {
            "PromotionBonus",
            14
          }
        };
      }
      int num;
      // ISSUE: reference to a compiler-generated field
      if (VotesManager.\u003C\u003Ef__switch\u0024map24.TryGetValue(key, out num))
      {
        switch (num)
        {
          case 0:
            return (PoliticalImpact) new PoliticalImpactPoints(inName, inEffect);
          case 1:
            return (PoliticalImpact) new PoliticalImpactTyreSettings(inName, inEffect);
          case 2:
            return (PoliticalImpact) new PoliticalImpactPitlaneSpeed(inName, inEffect);
          case 3:
            return (PoliticalImpact) new PoliticalImpactEnergyRecoverySystem(inName, inEffect);
          case 4:
            return (PoliticalImpact) new PoliticalImpactFuelSettings(inName, inEffect);
          case 5:
            return (PoliticalImpact) new PoliticalImpactChangeTrack(inName, inEffect);
          case 6:
            return (PoliticalImpact) new PoliticalImpactSessionLength(inName, inEffect);
          case 7:
            return (PoliticalImpact) new PoliticalImpactPrizePoolAdjustement(inName, inEffect);
          case 8:
            return (PoliticalImpact) new PoliticalImpactSafetyCar(inName, inEffect);
          case 9:
            return (PoliticalImpact) new PoliticalImpactSpecPart(inName, inEffect);
          case 10:
            return (PoliticalImpact) new PoliticalImpactGridSettings(inName, inEffect);
          case 11:
            return (PoliticalImpact) new PoliticalImpactPitStopCrew(inName, inEffect);
          case 12:
            return (PoliticalImpact) new PoliticalImpactSprinklers(inName, inEffect);
          case 13:
            return (PoliticalImpact) new PoliticalImpactLastPlaceBonus(inName, inEffect);
          case 14:
            return (PoliticalImpact) new PoliticalImpactPromotionBonus(inName, inEffect);
        }
      }
    }
    return (PoliticalImpact) null;
  }
}
