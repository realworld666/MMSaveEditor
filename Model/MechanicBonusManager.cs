using FullSerializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class MechanicBonusManager
{
    public Dictionary<int, MechanicBonus> mechanicBonuses = new Dictionary<int, MechanicBonus>();
    public static MechanicBonusManager Instance = new MechanicBonusManager();

    public MechanicBonusManager()
    {
        Instance = this;
        try
        {
            var uri = new Uri("pack://application:,,,/Assets/MechanicBonus.txt");
            System.Windows.Resources.StreamResourceInfo resourceStream = Application.GetResourceStream(uri);

            using (var reader = new StreamReader(resourceStream.Stream))
            {
                string traitsText = reader.ReadToEnd();
                List<DatabaseEntry> result = DatabaseReader.LoadFromText(traitsText);

                LoadMechanicBonusesFromDatabase(result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            //throw;
        }

    }

    public void LoadMechanicBonusesFromDatabase(List<DatabaseEntry> mechanicBonuses)
    {
        for (int index = 0; index < mechanicBonuses.Count; ++index)
        {
            MechanicBonus mechanicBonus = new MechanicBonus();
            mechanicBonus.bonusID = mechanicBonuses[index].GetIntValue("ID");
            mechanicBonus.trait = (MechanicBonus.Trait)(mechanicBonus.bonusID - 1);
            mechanicBonus.traitName = mechanicBonuses[index].GetStringValue("Trait Name");
            mechanicBonus.nameLocalisationID = mechanicBonuses[index].GetStringValue("Name ID");
            mechanicBonus.flavourText = mechanicBonuses[index].GetStringValue("Flavour Text");
            mechanicBonus.textLocalisationID = mechanicBonuses[index].GetStringValue("Text ID");
            mechanicBonus.icon = mechanicBonuses[index].GetIntValue("Icon");
            mechanicBonus.level = mechanicBonuses[index].GetIntValue("Level");
            mechanicBonus.objectType = mechanicBonuses[index].GetStringValue("Object Type");
            mechanicBonus.bonus = mechanicBonuses[index].GetStringValue("Bonus");
            string stringValue = mechanicBonuses[index].GetStringValue("Session");
            if (stringValue != string.Empty && stringValue != "0")
            {
                mechanicBonus.usefullForAnySession = false;
                mechanicBonus.usefullForSpecificSession = (SessionDetails.SessionType)Enum.Parse(typeof(SessionDetails.SessionType), stringValue);
            }
            if (mechanicBonus.level == 1)
                mechanicBonus.bonusUnlockAt = RandomUtility.GetRandom(20, 60);
            else if (mechanicBonus.level == 2)
                mechanicBonus.bonusUnlockAt = 100;
            this.mechanicBonuses.Add(mechanicBonus.bonusID, mechanicBonus);
        }
    }
}
