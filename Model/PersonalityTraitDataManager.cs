
using System.Collections.Generic;
using FullSerializer;
using MM2;
using System;
using System.IO;
using System.Windows;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PersonalityTraitDataManager
{
    [fsIgnore]
    public static List<PersonalityTrait.StatModified> raceStats = new List<PersonalityTrait.StatModified>() { PersonalityTrait.StatModified.Adaptability, PersonalityTrait.StatModified.Braking, PersonalityTrait.StatModified.Consistency, PersonalityTrait.StatModified.Cornering, PersonalityTrait.StatModified.Feedback, PersonalityTrait.StatModified.Fitness, PersonalityTrait.StatModified.Focus, PersonalityTrait.StatModified.Overtakng, PersonalityTrait.StatModified.Smoothness };
    public Dictionary<int, PersonalityTraitData> personalityTraits = new Dictionary<int, PersonalityTraitData>();
    private VersionNumber mVersion;

    public PersonalityTraitDataManager()
    {
        try
        {
            var uri = new Uri("pack://application:,,,/Assets/PersonalityTraits.txt");
            System.Windows.Resources.StreamResourceInfo resourceStream = Application.GetResourceStream(uri);

            using (var reader = new StreamReader(resourceStream.Stream))
            {
                string traitsText = reader.ReadToEnd();
                List<DatabaseEntry> result = DatabaseReader.LoadFromText(traitsText);

                LoadPersonalityTraitsFromDatabase(result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            //throw;
        }

    }

    public void LoadPersonalityTraitsFromDatabase(List<DatabaseEntry> personalityTraitsData)
    {
        for (int index = 0; index < personalityTraitsData.Count; ++index)
        {
            bool flag = false;
            DatabaseEntry inEntry = personalityTraitsData[index];
            int intValue = inEntry.GetIntValue("ID");
            PersonalityTraitData inTrait;
            if (this.personalityTraits.ContainsKey(intValue))
            {
                inTrait = this.personalityTraits[intValue];
            }
            else
            {
                inTrait = new PersonalityTraitData();
                flag = true;
            }
            inTrait.ID = intValue;
            inTrait.SetNameID(inEntry.GetStringValue("Trait Name ID"));
            inTrait.SetDescriptionID(inEntry.GetStringValue("Description ID"));
            inTrait.customTraitName = inEntry.GetStringValue("Trait Name");
            inTrait.customTraitDescription = inEntry.GetStringValue("Trait Description");
            this.LoadTraitType(inTrait, inEntry);
            inTrait.possibleLength = this.ParseStringToIntegerArray(inEntry, "Length");
            this.LoadTraitRequirements(inTrait, inEntry);
            inTrait.probability = (float)(1.0 - (double)inEntry.GetFloatValue("Probability") / 100.0);
            inTrait.evolvesInto = this.ParseStringToIntegerArray(inEntry, "Evolves Into");
            inTrait.opposites = this.ParseStringToIntegerArray(inEntry, "Opposites");
            inTrait.removesTraits = this.ParseStringToIntegerArray(inEntry, "Removes Traits");
            this.LoadTraitTriggerSource(inTrait, inEntry);
            this.LoadTriatShownType(inTrait, inEntry);
            this.LoadTraitModifiers(inTrait, inEntry);
            this.LoadTraitSpecialCaseData(inTrait, inEntry);
            this.LoadEventTriggerType(inTrait, inEntry);
            this.LoadIsRepeatableFlag(inTrait, inEntry);
            if (flag)
                this.personalityTraits.Add(inTrait.ID, inTrait);
        }
    }

    private int[] ParseStringToIntegerArray(DatabaseEntry inEntry, string fieldName)
    {
        string[] strArray = inEntry.GetStringValue(fieldName).Split(new char[2] { ';', '-' });
        List<int> intList = new List<int>();
        for (int index = 0; index < strArray.Length; ++index)
        {
            string s = strArray[index].Trim();
            if (!(s == string.Empty))
            {
                int num = int.Parse(s);
                if (num != 0)
                    intList.Add(num);
            }
        }
        return intList.ToArray();
    }

    private void LoadTraitType(PersonalityTraitData inTrait, DatabaseEntry inEntry)
    {
        string stringValue = inEntry.GetStringValue("Type");
        if (stringValue == "Permanent")
        {
            inTrait.type = PersonalityTraitData.TraitType.Permanent;
        }
        else
        {
            if (!(stringValue == "Temp"))
                return;
            inTrait.type = PersonalityTraitData.TraitType.Temporary;
        }
    }

    private void LoadTraitTriggerSource(PersonalityTraitData inTrait, DatabaseEntry inEntry)
    {
        inTrait.triggerCriteria.Clear();
        string stringValue = inEntry.GetStringValue("Triggers");
        if (string.IsNullOrEmpty(stringValue))
            return;
        string[] strArray1 = stringValue.Split(';');
        for (int index = 0; index < strArray1.Length; ++index)
        {
            if (!string.IsNullOrEmpty(strArray1[index]) && !(strArray1[index] == "0"))
            {
                string[] strArray2 = strArray1[index].Split('=');
                if (strArray2.Length == 2)
                {
                    inTrait.triggerCriteria.Add(new DialogCriteria("Type", "Header"));
                    inTrait.triggerCriteria.Add(new DialogCriteria(strArray2[0].Trim(), strArray2[1].Trim()));
                }
            }
        }
    }

    private void LoadTriatShownType(PersonalityTraitData inTrait, DatabaseEntry inEntry)
    {
        if (inEntry.GetStringValue("Shown") == "PlayerDriverOnly")
            inTrait.shownType = PersonalityTraitData.TriggerShownType.PlayerDriverOnly;
        else
            inTrait.shownType = PersonalityTraitData.TriggerShownType.AllDrivers;
    }

    private void LoadTraitRequirements(PersonalityTraitData inTrait, DatabaseEntry inEntry)
    {
        List<DialogCriteria> dialogCriteriaList = new List<DialogCriteria>();
        string stringValue = inEntry.GetStringValue("Requirement");
        if (!string.IsNullOrEmpty(stringValue))
        {
            string str = stringValue;
            char[] chArray = new char[1] { ';' };
            foreach (string inString in str.Split(chArray))
            {
                DialogCriteria personalityCriteria = this.GetPersonalityCriteria(inTrait, inString);
                if (personalityCriteria != null)
                    dialogCriteriaList.Add(personalityCriteria);
            }
        }
        inTrait.requirements = dialogCriteriaList;
    }

    private DialogCriteria GetPersonalityCriteria(PersonalityTraitData inTrait, string inString)
    {
        DialogCriteria dialogCriteria = (DialogCriteria)null;
        DialogCriteria.CriteriaOperator criteriaOperator = !inString.Contains(">=") ? (!inString.Contains("<=") ? (!inString.Contains("<") ? (!inString.Contains(">") ? DialogCriteria.CriteriaOperator.Equals : DialogCriteria.CriteriaOperator.Greater) : DialogCriteria.CriteriaOperator.Smaller) : DialogCriteria.CriteriaOperator.SmallerOrEquals) : DialogCriteria.CriteriaOperator.GreaterOrEquals;
        string[] separator = new string[5] { "=", ">", ">=", "<", "<=" };
        string[] strArray = inString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        if (strArray.Length == 1)
        {
            if (!string.IsNullOrEmpty(strArray[0]) && strArray[0] != "0")
            {
                dialogCriteria = new DialogCriteria(strArray[0].Trim(), string.Empty);
                dialogCriteria.criteriaOperator = criteriaOperator;
            }
        }
        else if (strArray.Length == 2)
        {
            dialogCriteria = new DialogCriteria(strArray[0].Trim(), strArray[1].Trim());
            dialogCriteria.criteriaOperator = criteriaOperator;
        }
        else
            throw new Exception(string.Format("Personality Trait with ID {0} has bad formatted requirements! Check database Personality Traits", (object)inTrait.ID));
        return dialogCriteria;
    }


    private void LoadTraitModifiers(PersonalityTraitData inTrait, DatabaseEntry inEntry)
    {
        inTrait.allStats = inEntry.GetIntValue("All Stats");
        inTrait.driverStatsModifier.Braking = (float)inEntry.GetIntValue("Braking");
        inTrait.driverStatsModifier.Cornering = (float)inEntry.GetIntValue("Cornering");
        inTrait.driverStatsModifier.Smoothness = (float)inEntry.GetIntValue("Smoothness");
        inTrait.driverStatsModifier.Overtaking = (float)inEntry.GetIntValue("Overtaking");
        inTrait.driverStatsModifier.Consistency = (float)inEntry.GetIntValue("Consistency");
        inTrait.driverStatsModifier.Adaptability = (float)inEntry.GetIntValue("Adaptability");
        inTrait.driverStatsModifier.Fitness = (float)inEntry.GetIntValue("Fitness");
        inTrait.driverStatsModifier.Feedback = (float)inEntry.GetIntValue("Feedback");
        inTrait.driverStatsModifier.Focus = (float)inEntry.GetIntValue("Focus");
        inTrait.driverStatsModifier.Marketability = (float)inEntry.GetIntValue("Marketability") / 100f;
        inTrait.moraleModifier = (float)inEntry.GetIntValue("Morale") / 100f;
        inTrait.mechanicModifier = inEntry.GetIntValue("Mechanic");
        inTrait.teammateModifier = (float)inEntry.GetIntValue("Teammate") / 100f;
        inTrait.chairmanModifier = inEntry.GetIntValue("Chairman");
        inTrait.improveabilityModifier = (float)inEntry.GetIntValue("Improveability") / 100f;
        inTrait.potentialModifier = inEntry.GetIntValue("Potential");
        inTrait.desiredWinsModifier = inEntry.GetIntValue("Desired Wins");
        inTrait.desiredEarningsModifier = (long)inEntry.GetIntValue("Desired Earnings") * 1000L;
    }

    private void LoadTraitSpecialCaseData(PersonalityTraitData inTrait, DatabaseEntry inEntry)
    {
        inTrait.specialCases.Clear();
        string stringValue1 = inEntry.GetStringValue("Special Case");
        if (!string.IsNullOrEmpty(stringValue1) && stringValue1 != "0")
        {
            string str1 = stringValue1;
            char[] chArray = new char[1] { ';' };
            foreach (string str2 in str1.Split(chArray))
            {
                string inSpecialCaseString = str2.Trim();
                if (!string.IsNullOrEmpty(inSpecialCaseString) && inSpecialCaseString != "0")
                    inTrait.specialCases.Add(this.GetSpecialCaseType(inSpecialCaseString));
            }
        }
        string stringValue2 = inEntry.GetStringValue("Special Case Description");
        if (string.IsNullOrEmpty(stringValue2) || !(stringValue2 != "0"))
            return;
        inTrait.specialCaseDescriptionID = stringValue2;
    }

    private void LoadEventTriggerType(PersonalityTraitData inTrait, DatabaseEntry inEntry)
    {
        string stringValue = inEntry.GetStringValue("EventTrigger");
        if (string.IsNullOrEmpty(stringValue))
            return;
        if (stringValue == "PostRace")
            inTrait.eventTriggerType = PersonalityTraitData.EventTriggerType.PostRace;
        else
            inTrait.eventTriggerType = PersonalityTraitData.EventTriggerType.None;
    }

    private void LoadIsRepeatableFlag(PersonalityTraitData inTrait, DatabaseEntry inEntry)
    {
        if (inEntry.GetStringValue("Repeatable") == "Yes")
            inTrait.isRepeatable = true;
        else
            inTrait.isRepeatable = false;
    }

    private PersonalityTrait.SpecialCaseType GetSpecialCaseType(string inSpecialCaseString)
    {
        string text = inSpecialCaseString.ToLower();
        switch (text)
        {
            case "neckinjury":
                return PersonalityTrait.SpecialCaseType.NeckInjury;
            case "faceinjury":
                return PersonalityTrait.SpecialCaseType.FaceInjury;
            case "carhappinessbonus":
                return PersonalityTrait.SpecialCaseType.CarHappinessBonus;
            case "carhappinessnegative":
                return PersonalityTrait.SpecialCaseType.CarHappinessNegative;
            case "p2inrace":
                return PersonalityTrait.SpecialCaseType.P2InRace;
            case "p1inrace":
                return PersonalityTrait.SpecialCaseType.P1InRace;
            case "racelap1":
                return PersonalityTrait.SpecialCaseType.RaceLap1;
            case "fuelburn(low)":
                return PersonalityTrait.SpecialCaseType.FuelBurnLow;
            case "fuelburn(high)":
                return PersonalityTrait.SpecialCaseType.FuelBurnHigh;
            case "tyrewear(low)":
                return PersonalityTrait.SpecialCaseType.TyreWearLow;
            case "tyrewear(high)":
                return PersonalityTrait.SpecialCaseType.TyreWearHigh;
            case "race":
                return PersonalityTrait.SpecialCaseType.Race;
            case "qualifying":
                return PersonalityTrait.SpecialCaseType.Qualifying;
            case "practice":
                return PersonalityTrait.SpecialCaseType.Practice;
            case "wetsession":
                return PersonalityTrait.SpecialCaseType.WetSession;
            case "{track}":
                return PersonalityTrait.SpecialCaseType.Track;
            case "homerace":
                return PersonalityTrait.SpecialCaseType.HomeRace;
            case "randompayment(low)":
                return PersonalityTrait.SpecialCaseType.RandomPaymentLow;
            case "randompayment(med)":
                return PersonalityTrait.SpecialCaseType.RandomPaymentMed;
            case "paydriver":
                return PersonalityTrait.SpecialCaseType.PayDriver;
            case "willnotjoinrival":
                return PersonalityTrait.SpecialCaseType.WillNotJoinRival;
            case "willnotrenewcontract":
                return PersonalityTrait.SpecialCaseType.WIllNotRenewContract;
            case "fightwithteammate":
                return PersonalityTrait.SpecialCaseType.FightWithTeammate;
            case "randomnewhairstyle":
                return PersonalityTrait.SpecialCaseType.RandomNewHairstyle;
            case "intermediatetyres":
                return PersonalityTrait.SpecialCaseType.IntermediateTyres;
            case "sponsortop3":
                return PersonalityTrait.SpecialCaseType.SponsorTop3;
            case "oneonone":
                return PersonalityTrait.SpecialCaseType.OneOnOne;
            case "turnoffteamorders":
                return PersonalityTrait.SpecialCaseType.TurnOffTeamOrders;
            case "turnoffstrategy":
                return PersonalityTrait.SpecialCaseType.TurnOffStrategy;
            case "notnumberone":
                return PersonalityTrait.SpecialCaseType.NotNumberOne;
            case "mechanicworsethandriver":
                return PersonalityTrait.SpecialCaseType.MechanicWorseThanDriver;
            case "teammatebetterthandriver":
                return PersonalityTrait.SpecialCaseType.TeammateBetterThanDriver;
            case "teammateearningmorethandriver":
                return PersonalityTrait.SpecialCaseType.TeammateEarningMoreThanDriver;
            case "carpartpromise":
                return PersonalityTrait.SpecialCaseType.CarPartPromise;
            case "incomepromise":
                return PersonalityTrait.SpecialCaseType.IncomeIncreasePromise;
            case "championshippromise":
                return PersonalityTrait.SpecialCaseType.ChampionshipPositionPromise;
            case "hqpromise":
                return PersonalityTrait.SpecialCaseType.HQBuildingPromise;
            case "mechanicpromise":
                return PersonalityTrait.SpecialCaseType.FireMechanicPromise;
            case "engineerpromise":
                return PersonalityTrait.SpecialCaseType.FireEngineerPromise;
            case "reservepromise":
                return PersonalityTrait.SpecialCaseType.FireReservePromise;
            case "guildford":
                return PersonalityTrait.SpecialCaseType.Guildford;
            case "ardennes":
                return PersonalityTrait.SpecialCaseType.Ardennes;
            case "phoenix":
                return PersonalityTrait.SpecialCaseType.Phoenix;
            case "yokohama":
                return PersonalityTrait.SpecialCaseType.Yokohama;
            case "munich":
                return PersonalityTrait.SpecialCaseType.Munich;
            case "capetown":
                return PersonalityTrait.SpecialCaseType.CapeTown;
            case "doha":
                return PersonalityTrait.SpecialCaseType.Doha;
            case "milan":
                return PersonalityTrait.SpecialCaseType.Milan;
            case "singapore":
                return PersonalityTrait.SpecialCaseType.Singapore;
            case "beijing":
                return PersonalityTrait.SpecialCaseType.Beijing;
            case "dubai":
                return PersonalityTrait.SpecialCaseType.Dubai;
            case "tondela":
                return PersonalityTrait.SpecialCaseType.Tondela;
            case "riodejaneiro":
                return PersonalityTrait.SpecialCaseType.RioDeJaneiro;
            case "blacksea":
                return PersonalityTrait.SpecialCaseType.BlackSea;
            case "sydney":
                return PersonalityTrait.SpecialCaseType.Sydney;
            case "vancouver":
                return PersonalityTrait.SpecialCaseType.Vancouver;
            case "needmoretrophiesbeforeretiring":
                return PersonalityTrait.SpecialCaseType.NeedMoreTrophiesBeforeRetiring;
            case "needlesstrophiesbeforeretiring":
                return PersonalityTrait.SpecialCaseType.NeedLessTrophiesBeforeRetiring;
            case "mentorimproveabilityboost":
                return PersonalityTrait.SpecialCaseType.MentorImproveabilityBoost;
            case "mentorimproveabilitydebuff":
                return PersonalityTrait.SpecialCaseType.MentorImproveabilityDebuff;
            case "offendedbyinterview":
                return PersonalityTrait.SpecialCaseType.OffendedByInterview;
            case "butteredupbyinterview":
                return PersonalityTrait.SpecialCaseType.ButteredUpByInterview;
            case "gtseries":
                return PersonalityTrait.SpecialCaseType.GTSeries;
            case "singleseaters":
                return PersonalityTrait.SpecialCaseType.SingleSeaters;
            case "shockretirement":
                return PersonalityTrait.SpecialCaseType.ShockRetirement;
        }
        throw new Exception(string.Format("Personality trait special case not supported, name: {0}. Check Personality traits database entries.", inSpecialCaseString));
        return PersonalityTrait.SpecialCaseType.Count;
    }
}
