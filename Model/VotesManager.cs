using FullSerializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class VotesManager
{
    public static VotesManager Instance = new VotesManager();

    public Dictionary<int, PoliticalVote> votes = new Dictionary<int, PoliticalVote>();
    public List<int> voteIDs = new List<int>();

    public VotesManager()
    {
        try
        {
            var uri = new Uri("pack://application:,,,/Assets/RuleChanges.txt");
            System.Windows.Resources.StreamResourceInfo resourceStream = Application.GetResourceStream(uri);

            using (var reader = new StreamReader(resourceStream.Stream))
            {
                string traitsText = reader.ReadToEnd();
                List<DatabaseEntry> result = DatabaseReader.LoadFromText(traitsText);

                LoadVotesFromDatabase(result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            //throw;
        }
    }

    public void LoadVotesFromDatabase(List<DatabaseEntry> votesData)
    {
        for (int index = 0; index < votesData.Count; ++index)
        {
            DatabaseEntry databaseEntry = votesData[index];
            PoliticalVote inVote = new PoliticalVote();
            if (Enum.IsDefined(typeof(CarPart.PartType), (object)databaseEntry.GetStringValue("Object Type")))
                inVote.currentPartType = (CarPart.PartType)Enum.Parse(typeof(CarPart.PartType), databaseEntry.GetStringValue("Object Type"));
            inVote.displayRule = databaseEntry.GetBoolValue("Display Rule");
            inVote.group = databaseEntry.GetStringValue("Rule Group");
            inVote.nameID = databaseEntry.GetStringValue("Rule Name ID");
            inVote.descriptionID = databaseEntry.GetStringValue("Rule Description ID");
            inVote.ID = databaseEntry.GetIntValue("ID");
            inVote.lockedToPlayerVote = databaseEntry.GetBoolValue("Locked To Player Vote");
            inVote.effectType = databaseEntry.GetStringValue("Effect Type");
            inVote.impacts = VotesManager.GetImpacts(inVote.effectType, inVote);
            inVote.messageCriteria = VotesManager.GetMessageCriteria(databaseEntry.GetStringValue("Message Trigger"));
            string stringValue1 = databaseEntry.GetStringValue("Benefits");
            char[] chArray1 = new char[1] { ';' };
            foreach (string str1 in stringValue1.Split(chArray1))
            {
                string str2 = str1.Trim();
                if (!Enum.IsDefined(typeof(PoliticalVote.TeamCharacteristics), (object)str2))
                    Console.WriteLine(34.ToString() + str2 + (object)'"' + " does not exist in the teamCharacteristicsEnum");
                else
                    inVote.benificialCharacteristics.Add((PoliticalVote.TeamCharacteristics)Enum.Parse(typeof(PoliticalVote.TeamCharacteristics), str2));
            }
            string stringValue2 = databaseEntry.GetStringValue("Challenges");
            char[] chArray2 = new char[1] { ';' };
            foreach (string str1 in stringValue2.Split(chArray2))
            {
                string str2 = str1.Trim();
                if (!Enum.IsDefined(typeof(PoliticalVote.TeamCharacteristics), (object)str2))
                    Console.WriteLine(34.ToString() + str2 + (object)'"' + " does not exist in the teamCharacteristicsEnum");
                else
                    inVote.detrimentalCharacteristics.Add((PoliticalVote.TeamCharacteristics)Enum.Parse(typeof(PoliticalVote.TeamCharacteristics), str2));
            }
            if (this.votes.ContainsKey(inVote.ID))
                Console.WriteLine("Rule Changes database contains rules with duplicate IDs: {0}", (object)inVote.ID);
            this.votes.Add(inVote.ID, inVote);
            this.voteIDs.Add(inVote.ID);
        }
        Console.WriteLine("RulesLoaded");
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
                    Console.WriteLine("Error, criteria split in more than 2 strings, database entry badly formated");
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
        char[] chArray = new char[1] { ';' };
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
                    if ((int)str3[index] != 40)
                        inName = inName.Insert(inName.Length, str3[index].ToString());
                    else if ((int)str3[index] == 40)
                        flag = true;
                }
                else if ((int)str3[index] != 40 && (int)str3[index] != 41)
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
        string text = inName;
        switch (text)
        {
            case "Points":
            case "FinalRacePointsDoubled":
            case "FastestLapBonus":
            case "PoleBonus":
                return new PoliticalImpactPoints(inName, inEffect);
            case "TyreSpeed":
            case "TyreSupplierSpeedBonus":
            case "TyreWearRate":
            case "TyreSupplier":
            case "TyreType":
            case "TyresAvailable":
            case "TyreCompoundChoice":
            case "TyreCompoundsAvailable":
                return new PoliticalImpactTyreSettings(inName, inEffect);
            case "PitlaneSpeed":
                return new PoliticalImpactPitlaneSpeed(inName, inEffect);
            case "EnergySystem":
                return new PoliticalImpactEnergyRecoverySystem(inName, inEffect);
            case "FuelLimit":
            case "Refuelling":
                return new PoliticalImpactFuelSettings(inName, inEffect);
            case "ChangeTrackLayout":
            case "ReplaceTrack":
            case "AddTrack":
            case "RemoveTrack":
            case "AddLayout":
                return new PoliticalImpactChangeTrack(inName, inEffect);
            case "Practice":
            case "Qualifying":
            case "Race":
                return new PoliticalImpactSessionLength(inName, inEffect);
            case "PrizePoolCashGap":
                return new PoliticalImpactPrizePoolAdjustement(inName, inEffect);
            case "SafetyCar":
                return new PoliticalImpactSafetyCar(inName, inEffect);
            case "SpecPart":
                return new PoliticalImpactSpecPart(inName, inEffect);
            case "Grid":
                return new PoliticalImpactGridSettings(inName, inEffect);
            case "PitStopCrew":
                return new PoliticalImpactPitStopCrew(inName, inEffect);
            case "Sprinkler":
                return new PoliticalImpactSprinklers(inName, inEffect);
            case "LastPlaceBonus":
                return new PoliticalImpactLastPlaceBonus(inName, inEffect);
            case "PromotionBonus":
                return new PoliticalImpactPromotionBonus(inName, inEffect);
        }
        return null;
    }
}
