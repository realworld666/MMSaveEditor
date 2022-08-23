//#define USE_JSON_NET
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using FullSerializer;
using GalaSoft.MvvmLight.Ioc;
using LZ4;
using Microsoft.Win32;
using MMSaveEditor.Annotations;
using MMSaveEditor.ViewModel;
using NBug.Core.Reporting;
using NBug.Core.Reporting.Info;
using NBug.Core.Util;
using MM2;
using UnityEngine;
#if USE_JSON_NET
using Newtonsoft.Json;
#endif

namespace MMSaveEditor.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly fsSerializer serializer;

        private string _openFilePath;
        private static readonly int saveFileVersion = 4;
        private SaveFileInfo _currentSaveInfo;

        public static MainWindow Instance;

        private static readonly VersionNumber[] SupportedVersions =
        {
            new VersionNumber() { major=1, minor=5},
            new VersionNumber() { major=1, minor=51},
            new VersionNumber() { major=1, minor=52},
            new VersionNumber() { major=1, minor=53},
        };

        public string VersionString
        {
            get
            {
                return string.Format("Motorsport Manager Save Editor v{0}", Assembly.GetExecutingAssembly()
                    .GetName()
                    .Version);
            }
        }

        public string WindowTitle
        {
            get
            {
                if (string.IsNullOrWhiteSpace(OpenFilePath))
                {
                    return String.Format("Motorsport Manager Save Game Editor");
                }
                else
                {
                    return String.Format("Motorsport Manager Save Game Editor - {0}", Path.GetFileNameWithoutExtension(OpenFilePath));
                }
            }
        }

        public SaveFileInfo CurrentSaveInfo
        {
            get
            {
                return _currentSaveInfo;
            }

            set
            {
                _currentSaveInfo = value;
            }
        }

        public string OpenFilePath
        {
            get
            {
                return _openFilePath;
            }

            set
            {
                _openFilePath = value;
                NotifyPropertyChanged("WindowTitle");
            }
        }

        public enum TabPage
        {
            TeamPrincipal,
            Driver,
            Team, Game,
            Mechanic,
            Engineer
        }

        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            serializer = CreateAndConfigureSerializer();

            tabControl.Visibility = Visibility.Hidden;
            noSaveLoaded.Visibility = Visibility.Visible;

            Width = MMSaveEditor.Properties.Settings.Default.Width;
            Height = MMSaveEditor.Properties.Settings.Default.Height;
            WindowState = MMSaveEditor.Properties.Settings.Default.IsMaximized ? WindowState.Maximized : WindowState.Normal;

            MMSaveEditor.Properties.Settings.Default.RunCount++;
            MMSaveEditor.Properties.Settings.Default.Save();

            DonateBanner.Visibility = (MMSaveEditor.Properties.Settings.Default.RunCount >= 5 && !MMSaveEditor.Properties.Settings.Default.HasDonated) ? Visibility.Visible : Visibility.Collapsed;

            
            if (ReportGameCrashDialog.HasThereBeenAGameCrash())
            {
                MessageBoxResult result = MessageBox.Show("We have detected that Motorsport Manager crashed on the last run. Could you submit this log to us if it was a result of a change you made to your save file? We will then investigate the cause and fix in an update. Thanks!", "Did you just haz a crash", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    report_Click(null, null);
                }
                return;
            }
            
        }

        private static fsSerializer CreateAndConfigureSerializer()
        {
            return new fsSerializer { Config = { DefaultMemberSerialization = fsMemberSerialization.OptOut, SerializeAttributes = new Type[1] { typeof(fsPropertyAttribute) }, IgnoreSerializeAttributes = new Type[2] { typeof(NonSerializedAttribute), typeof(fsIgnoreAttribute) }, SerializeEnumsAsInteger = true, EnablePropertySerialization = false } };
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Save games (*.sav)|*.*",
                InitialDirectory = Path.Combine(App.Instance.LocalLowFolderPath, "Playsport Games\\Motorsport Manager\\Cloud\\Saves")
            };

            if (openFileDialog.ShowDialog() == true)
            {
                OpenFilePath = openFileDialog.FileName;

                bool success = LoadFile(openFileDialog.FileName, serializer, out _currentSaveInfo);

                if (success)
                {
                    SetupViewModels();
                    tabControl.Visibility = Visibility.Visible;
                    noSaveLoaded.Visibility = Visibility.Hidden;

                    NationalityManager.Instance.GetNationalitiesForContinent(Nationality.Continent.Europe);
                }
            }
        }

        private static void SetupViewModels()
        {
            try
            {
                var playerVM = SimpleIoc.Default.GetInstance<PlayerViewModel>();
                playerVM.SetModel(Game.instance.player);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("There was a problem setting the player view {0}", ex.Message), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var teamVM = SimpleIoc.Default.GetInstance<TeamViewModel>();
                teamVM.SetModel(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("There was a problem setting the player view {0}", ex.Message), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            /*try
            {
                var gameVM = SimpleIoc.Default.GetInstance<GameViewModel>();
                gameVM.SetModels(Game.instance.time);
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show(string.Format("There was a problem setting the game view {0}", ex.Message), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }*/
            try
            {
                var principleVM = SimpleIoc.Default.GetInstance<TeamPrincipalViewModel>();
                principleVM.SetList(Game.instance.teamPrincipalManager.GetEntityList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("There was a problem setting the team principal view {0}", ex.Message), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var driverVM = SimpleIoc.Default.GetInstance<DriverViewModel>();
                driverVM.SetList(Game.instance.driverManager.GetEntityList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("There was a problem setting the driver view {0}", ex.Message), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var engineerVM = SimpleIoc.Default.GetInstance<EngineerViewModel>();
                engineerVM.SetList(Game.instance.engineerManager.GetEntityList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("There was a problem setting the engineer view {0}", ex.Message), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var mechanicVM = SimpleIoc.Default.GetInstance<MechanicViewModel>();
                mechanicVM.SetList(Game.instance.mechanicManager.GetEntityList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("There was a problem setting the mechanic view {0}", ex.Message), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var chairmanVM = SimpleIoc.Default.GetInstance<ChairmanViewModel>();
                chairmanVM.SetList(Game.instance.chairmanManager.GetEntityList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("There was a problem setting the chairman view {0}", ex.Message), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var championshipVM = SimpleIoc.Default.GetInstance<ChampionshipViewModel>();
                championshipVM.SetModel(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("There was a problem setting the championship view {0}", ex.Message), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void import_Click(object Sender, RoutedEventArgs e)
        {
            string folder = @".\import\";
            if(!Directory.Exists(folder) || Directory.GetFiles(folder).Length == 0)
            {
                MessageBox.Show("import folder is empty, please place csv's there from the export folder once edited.", "Load Error", MessageBoxButton.OK);
                return;
            }
            if (Game.instance == null)
            {
                MessageBox.Show("Load a save file first before importing", "Load Error", MessageBoxButton.OK);
                return;
            }

            string[] files = Directory.GetFiles(folder);

            //data[Championship][Team][FullName][NewFirstName, NewLastName]
            Dictionary<string, Dictionary<string, Dictionary<string, (string, string, string, string)>>> data = new Dictionary<string, Dictionary<string, Dictionary<string, (string, string, string, string)>>>();

            foreach(string file in files)
            {
                
                string championship = "";
                string teamName = "";

                Dictionary<string, Dictionary<string, (string, string, string, string)>> Teams = new Dictionary<string, Dictionary<string, (string, string, string, string)>>();
                Dictionary<string, (string, string, string, string)> People = new Dictionary<string, (string, string, string, string)>();

                foreach (string line in File.ReadLines(file))
                {
                    if (String.IsNullOrEmpty(line) || line.StartsWith(",") || line.StartsWith("Job")) continue;             

                    string[] elements = line.Split(',');

                    if (elements[0].Equals("Championship"))
                    {
                        championship = elements[1];
                    }
                    else if (elements[0].Equals("Team"))
                    {
                        if (!teamName.Equals(""))
                        {
                            if (People.Count > 0)
                                Teams.Add(teamName, People);
                            People = new Dictionary<string, (string, string, string, string)>();
                        }
                        teamName = elements[1];
                        if (elements.Length > 2)
                            People.Add(elements[1], (elements[2], elements[3], "", ""));
                    }
                    else if (elements[0].Equals("Unemployed"))
                    {
                        if (championship.Equals(""))
                        {
                            championship = "unemployed";
                            teamName = "unemployed";
                        }
                        if(elements.Length > 2)
                        {
                            // Oldname, first name, last name, nationality, gender
                            People.Add(elements[1], (elements[2], elements[3], elements[4], elements[5]));
                        }
                    }
                    else
                    {
                        // Oldname, first name, last name, nationality, gender
                        if (elements.Length > 2)
                            People.Add(elements[1], (elements[2], elements[3], elements[4], elements[5]));
                    }

                }
                if(People.Count > 0)
                    Teams.Add(teamName, People);
                if(Teams.Count > 0)
                    data.Add(championship, Teams);
            }

            int changed = 0;
            List<Championship> championships = Game.instance.championshipManager.GetEntityList();

            foreach (Championship championship in championships)
            {
                string championshipName = championship.ChampionshipName;
                if (!data.ContainsKey(championshipName))
                    continue;

                for (int i = 0; i < championship.standings.teamEntryCount; i++)
                {

                    Team team = championship.standings.GetTeamEntry(i).GetEntity<Team>();
                    string teamName = team.name;

                    /*
                    if (team.carManager.GetCar(0).GetFrontendCar() != null) {
                        TeamColor.LiveryColour carColour = team.carManager.GetCar(0).GetDataForCar(0).colourData;
                        carColour.primary = new Color(0, 0, 0, 1);
                        Console.WriteLine("Changed livery for " + team.name);
                    } else
                    {
                        Console.WriteLine("Failed to set livery for " + team.name);
                    }
                    */


                    if (!data[championshipName].ContainsKey(teamName))
                        continue;

                    if (data[championshipName][teamName].ContainsKey(teamName)) { 
                        var (newTeamName, newShortTeamName, nationality, _) = data[championshipName][teamName][teamName];

                        team.name = newTeamName;
                        team.ShortName = newShortTeamName;
                        if (NationalityManager.Instance.nationalitiesDict.ContainsKey(nationality))
                            team.nationality = NationalityManager.Instance.nationalitiesDict[nationality];
                    }


                    List<EmployeeSlot> employees = team.contractManager.GetAllEmployeeSlots();

                    foreach (EmployeeSlot employee in employees)
                    {
                        if (employee.personHired == null)
                            continue;

                        if (data[championshipName][teamName].ContainsKey(employee.personHired.name)){
                            var (firstName, lastName, nationality, gender) = data[championshipName][teamName][employee.personHired.name];

                            employee.personHired.SetName(firstName, lastName);

                            if(gender.Equals("Male") || gender.Equals("male") || gender.Equals("M"))
                                employee.personHired.gender = Person.Gender.Male;
                            else
                                employee.personHired.gender = Person.Gender.Female;



                            if(NationalityManager.Instance.nationalitiesDict.ContainsKey(nationality))
                                employee.personHired.nationality = NationalityManager.Instance.nationalitiesDict[nationality];

                            changed++;
                        }
                    }
                }
            }

            if (data.ContainsKey("unemployed"))
            {

                List<Person> unemployed = new List<Person>();

                unemployed.AddRange(Converter<Driver>.Convert(Game.instance.driverManager.GetReplacementPeople()));
                unemployed.AddRange(Converter<Chairman>.Convert(Game.instance.chairmanManager.GetReplacementPeople()));
                unemployed.AddRange(Converter<Engineer>.Convert(Game.instance.engineerManager.GetReplacementPeople()));
                unemployed.AddRange(Converter<Assistant>.Convert(Game.instance.assistantManager.GetReplacementPeople()));
                unemployed.AddRange(Converter<Celebrity>.Convert(Game.instance.celebrityManager.GetReplacementPeople()));
                unemployed.AddRange(Converter<PitCrewMember>.Convert(Game.instance.pitCrewManager.GetReplacementPeople()));
                unemployed.AddRange(Converter<Scout>.Convert(Game.instance.scoutManager.GetReplacementPeople()));
                unemployed.AddRange(Converter<TeamPrincipal>.Convert(Game.instance.teamPrincipalManager.GetReplacementPeople()));

                foreach (Person person in unemployed)
                {

                    if (data["unemployed"]["unemployed"].ContainsKey(person.name))
                    {
                        changed++;
                        var (firstName, lastName, nationality, gender) = data["unemployed"]["unemployed"][person.name];

                        person.SetName(firstName, lastName);

                        if (gender.Equals("Male") || gender.Equals("male") || gender.Equals("M"))
                            person.gender = Person.Gender.Male;
                        else
                            person.gender = Person.Gender.Female;

                        if (NationalityManager.Instance.nationalitiesDict.ContainsKey(nationality))
                            person.nationality = NationalityManager.Instance.nationalitiesDict[nationality];
                    }
                }
            }

                MessageBox.Show("Finished importing data. Changed "+ changed + " names.", "OK", MessageBoxButton.OK);
        }

        private void export_Click(object sender, RoutedEventArgs e)
        {
            if (Game.instance == null)
            {
                MessageBox.Show("Load a save file first before exporting", "Load Error", MessageBoxButton.OK);
                return;
            }
            try
            {

                if (!Directory.Exists(@".\export"))
                    Directory.CreateDirectory(@".\export");


                List<Championship> championships = Game.instance.championshipManager.GetEntityList();

                foreach (Championship championship in championships)
                {
                    List<string> lines = new List<string>();

                    Console.WriteLine(championship.ChampionshipName);

                    lines.Add("Championship," + championship.ChampionshipName);


                    for (int i = 0; i < championship.standings.teamEntryCount; i++)
                    {

                        Team team = championship.standings.GetTeamEntry(i).GetEntity<Team>();

                        Console.WriteLine(team.name);
                        lines.Add("\nTeam," + team.name);
                        lines.Add("Job,Old name,New first name,New last name,Nationality,Gender");

                        List<EmployeeSlot> employees = team.contractManager.GetAllEmployeeSlots();

                        foreach (EmployeeSlot employee in employees)
                        {
                            if (employee.personHired == null)
                                continue;

                            lines.Add(employee.jobType.ToString() + "," + employee.personHired.name);

                        }
                    }
                    File.WriteAllLines(@".\export\" + championship.ChampionshipName + ".csv", lines);
                }



                List<(string, Person)> unemployed = new List<(string, Person)>();

                unemployed.AddRange(PersonZip(Converter<Driver>.Convert(Game.instance.driverManager.GetReplacementPeople()), "Driver"));
                unemployed.AddRange(PersonZip(Converter<Chairman>.Convert(Game.instance.chairmanManager.GetReplacementPeople()), "Chairman"));
                unemployed.AddRange(PersonZip(Converter<Engineer>.Convert(Game.instance.engineerManager.GetReplacementPeople()), "Engineer"));
                unemployed.AddRange(PersonZip(Converter<Assistant>.Convert(Game.instance.assistantManager.GetReplacementPeople()), "Assistant"));
                unemployed.AddRange(PersonZip(Converter<Celebrity>.Convert(Game.instance.celebrityManager.GetEntityList()), "Celebrity"));
                unemployed.AddRange(PersonZip(Converter<PitCrewMember>.Convert(Game.instance.pitCrewManager.GetReplacementPeople()), "PitCrewMember"));
                unemployed.AddRange(PersonZip(Converter<Scout>.Convert(Game.instance.scoutManager.GetReplacementPeople()), "Scout"));
                unemployed.AddRange(PersonZip(Converter<TeamPrincipal>.Convert(Game.instance.teamPrincipalManager.GetReplacementPeople()), "TeamPrincipal"));

                

                List<string> data = new List<string>();

                data.Add("Unemployed,Old name,New first name,New last name,Nationality,Gender");

                foreach (var (job, person) in unemployed)
                    data.Add(job + "," + person.name);


                File.WriteAllLines(@".\export\unemployed.csv", data);

                MessageBox.Show("Finished exporting to the export folder", "OK", MessageBoxButton.OK);

            } catch (Exception exception)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, exception.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, exception.Source);

                MessageBox.Show(errorMessage, "Error");
            } 
        }

        public List<(string, Person)> PersonZip(List<Person> people, string Job)
        {
            List<(string, Person)> zippedPeople = new List<(string, Person)>();
            foreach(Person person in people) 
                zippedPeople.Add((Job, person));
            return zippedPeople;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_openFilePath))
            {
                SaveFile(_openFilePath, serializer, _currentSaveInfo);
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Save games (*.sav)|*.*";
            saveFileDialog.InitialDirectory = Path.Combine(App.Instance.LocalLowFolderPath, "Playsport Games\\Motorsport Manager\\Cloud\\Saves");
            saveFileDialog.DefaultExt = "sav";
            if (saveFileDialog.ShowDialog() == true)
            {
                SaveFile(saveFileDialog.FileName, serializer, _currentSaveInfo);
                OpenFilePath = saveFileDialog.FileName;
            }
        }

        public static void SaveFile(string openFilePath, fsSerializer serializer, SaveFileInfo saveFileInfo)
        {
            try
            {
                fsResult fsResult1 = serializer.TrySerialize(saveFileInfo, out fsData data1);
                if (fsResult1.Failed)
                    throw new Exception(string.Format("Failed to serialise SaveFileInfo: {0}", fsResult1.FormattedMessages));
                string s1 = fsJsonPrinter.CompressedJson(data1);
                fsResult fsResult2 = serializer.TrySerialize(Game.instance, out fsData data2);
                if (fsResult2.Failed)
                    throw new Exception(string.Format("Failed to serialise Game: {0}",
                        fsResult2.FormattedMessages));
                string s2 = fsJsonPrinter.CompressedJson(data2);
                byte[] bytes1 = Encoding.UTF8.GetBytes(s1);
                byte[] bytes2 = Encoding.UTF8.GetBytes(s2);
                Debug.Assert(bytes1.Length < 268435456 && bytes2.Length < 268435456, "Uh-oh. Ben has underestimated how large save files might get, and we're about to save a file so large it will be detected as corrupt when loading. Best increase the limit!", null);
                byte[] buffer1 = LZ4Codec.Encode(bytes1, 0, bytes1.Length);
                byte[] buffer2 = LZ4Codec.Encode(bytes2, 0, bytes2.Length);
                FileInfo fileInfo = new FileInfo(openFilePath);
                using (FileStream fileStream = File.Create(fileInfo.FullName))
                {
                    using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                    {
                        binaryWriter.Write(1932684653);
                        binaryWriter.Write(saveFileVersion);
                        binaryWriter.Write(buffer1.Length);
                        binaryWriter.Write(bytes1.Length);
                        binaryWriter.Write(buffer2.Length);
                        binaryWriter.Write(bytes2.Length);
                        binaryWriter.Write(buffer1);
                        binaryWriter.Write(buffer2);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private class LoadFileResult
        {
            public bool success;
            public SaveFileInfo saveFileInfo;
        }

        public static bool LoadFile(string fileName, fsSerializer serializer, out SaveFileInfo saveFileInfo)
        {
            LoadFileResult result = null;
            Thread loadThread = new Thread(() => result = LoadFileAsync(fileName, serializer), 1024 * 1024 * 3);
            loadThread.Start();
            while (loadThread.IsAlive)
            {

            }
            saveFileInfo = result.success ? result.saveFileInfo : null;
            return result.success;
        }

        private static LoadFileResult LoadFileAsync(string fileName, fsSerializer serializer)
        {
            LoadFileResult loadResult = new LoadFileResult();
            using (FileStream fileStream = File.Open(fileName, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    if (binaryReader.ReadInt32() != 1932684653)
                    {
                        MessageBoxResult result = MessageBox.Show("Save file is not a valid save file for this game", "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        loadResult.success = false;
                        return loadResult;
                    }
                    int num1 = binaryReader.ReadInt32();
                    if (num1 < saveFileVersion)
                    {
                        MessageBoxResult result = MessageBox.Show("Save file is an old format, and no upgrade path exists - must be from an old unsupported development version", "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        loadResult.success = false;
                        return loadResult;
                    }
                    if (num1 > saveFileVersion)
                    {
                        MessageBoxResult result = MessageBox.Show("Save file version is newer than the editor expected. If the game has been updated recently you may need to wait for an update to the editor. Check the forums for updates.", "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        loadResult.success = false;
                        return loadResult;
                    }
                    int headerCount = binaryReader.ReadInt32();
                    int headerOutputLength = binaryReader.ReadInt32();
                    int gameDataCount = binaryReader.ReadInt32();
                    int gameDataOutputLength = binaryReader.ReadInt32();
                    if (headerOutputLength > 268435456)
                    {
                        MessageBoxResult result = MessageBox.Show("Save file header size is apparently way too big - file has either been tampered with or become corrupt", "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        loadResult.success = false;
                        return loadResult;
                    }
                    if (gameDataOutputLength > 268435456)
                    {
                        MessageBoxResult result = MessageBox.Show("Save file game data size is apparently way too big - file has either been tampered with or become corrupt", "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        loadResult.success = false;
                        return loadResult;
                    }

                    //
                    // Load header SaveFileInfo
                    //
                    fsData headerData;
                    string jsonHead = Encoding.UTF8.GetString(LZ4Codec.Decode(binaryReader.ReadBytes(headerCount), 0, headerCount, headerOutputLength));
#if DEBUG
                    File.WriteAllText(@"saveFileJSONHead.txt", jsonHead);
#endif
                    fsResult fsHeaderResult1 = fsJsonParser.Parse(jsonHead, out headerData);
                    if (fsHeaderResult1.Failed)
                    {
                        MessageBoxResult result = MessageBox.Show(string.Format("Error reported whilst parsing serialized SaveFileInfo string: {0}", fsHeaderResult1.FormattedMessages), "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        loadResult.success = false;
                        return loadResult;
                    }

                    loadResult.saveFileInfo = null;
                    fsResult fsHeaderResult2 = serializer.TryDeserialize(headerData, ref loadResult.saveFileInfo);
                    if (fsHeaderResult2.Failed)
                    {
                        MessageBoxResult result = MessageBox.Show(string.Format("Error reported whilst deserializing SaveFileInfo: {0}", fsHeaderResult1.FormattedMessages), "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        loadResult.success = false;
                        return loadResult;
                    }
                    try
                    {
                        FileInfo fileInfo = new FileInfo(fileName);
                        loadResult.saveFileInfo.fileInfo = fileInfo;
                    }
                    catch (Exception ex)
                    {
                        MessageBoxResult result = MessageBox.Show(string.Format("Could not create FileInfo for {0}. Check that the editor has permissions to access this file.", fileName), "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);

                        loadResult.success = false;
                        return loadResult;
                    }


                    // Verify save file is valid
                    if (!IsValidSaveVersion(loadResult.saveFileInfo.saveInfo.version))
                    {
                        MessageBoxResult result = MessageBox.Show(string.Format("Save file is from version {0}.{1} but the editor only supports save files from version {2}.{3} of the game.", loadResult.saveFileInfo.saveInfo.version.major, loadResult.saveFileInfo.saveInfo.version.minor, SupportedVersions[0].major, SupportedVersions[0].minor), "Incorrect version", MessageBoxButton.OK, MessageBoxImage.Error);

                        loadResult.success = false;
                        return loadResult;
                    }
                    // Verify mods don't cause issues
                    /*foreach (var mod in saveFileInfo.subscribedModsInfo)
                    {
                        if (mod.id == 1118440753)
                        {
                            // Fire mod special case
                            MessageBoxResult result = MessageBox.Show(string.Format("We have detected {0} on this save file which is known to be incompatible with this editor.", mod.name), "Incompatible Mod", MessageBoxButton.OK, MessageBoxImage.Error);

                            saveFileInfo = null;
                            return false;
                        }
                        else if (mod.isNewGameRequired)
                        {
                            // Any mod that requires a new game is probably going to cause problems. Warn but allow
                            MessageBoxResult result = MessageBox.Show(string.Format("We have detected {0} on this save file which may be incompatible with this editor. You can continue but the save file may not load successfully.", mod.name), "Possibly Incompatible Mod", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }*/


                    //
                    // Load main save data
                    // 
                    Game targetGame = null;

                    fsData gameData;
                    try
                    {
                        string json = Encoding.UTF8.GetString(LZ4Codec.Decode(binaryReader.ReadBytes(gameDataCount), 0, gameDataCount, gameDataOutputLength));
#if DEBUG
                        File.WriteAllText(@"saveFileJSON.txt", json);
#endif
                        //SaveData saveData = JsonConvert.DeserializeObject<SaveData>( json );
                        //string formattedJSON = JsonConvert.SerializeObject( parsedJson, Formatting.Indented );


                        fsResult fsResult1 = fsJsonParser.Parse(json, out gameData);
                        if (fsResult1.Failed)
                        {
                            MessageBoxResult result = MessageBox.Show(string.Format("Error reported whilst parsing serialized Game data string: {0}", fsResult1.FormattedMessages), "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            loadResult.success = false;
                            return loadResult;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBoxResult result = MessageBox.Show(string.Format("Exception thrown whilst parsing serialized Game data string: {0}", fileName), "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        loadResult.success = false;
                        return loadResult;
                    }

                    fsResult fsResult2 = new fsResult();
                    try
                    {
                        fsResult2 = serializer.TryDeserialize(gameData, ref targetGame);
                        if (fsResult2.Failed)
                        {
                            MessageBoxResult result = MessageBox.Show(string.Format("Error reported whilst deserializing Game data: {0}", fsResult2.FormattedMessages), "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            loadResult.success = false;
                            return loadResult;
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBoxResult result = MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);

                        binaryReader.Close();
                        fileStream.Close();

                        var executionFlow = new BugReport().Report(ex, ExceptionThread.Main);


                        //foreach (object rawMessage in fsResult2.RawMessages)
                        //    Console.Write(rawMessage);

                        //Application.Current.Shutdown();
                        loadResult.success = false;
                        return loadResult;
                    }
                    //foreach (object rawMessage in fsResult2.RawMessages)
                    //  Console.Write(rawMessage);
                }
            }

            loadResult.success = true;
            return loadResult;
        }

        private void TeamPage_OnListBoxUpdated(object sender, Team e)
        {
            var teamVM = SimpleIoc.Default.GetInstance<TeamViewModel>();
            teamVM.SetModel(e);
        }

        private void TeamPrinciplePage_OnListBoxUpdated(object sender, Person e)
        {
            var vm = SimpleIoc.Default.GetInstance<TeamPrincipalViewModel>();
            vm.SetModel(e as TeamPrincipal);
        }

        private void DriverPage_OnListBoxUpdated(object sender, Person e)
        {
            var vm = SimpleIoc.Default.GetInstance<DriverViewModel>();
            vm.SetModel(e as Driver);
        }

        private void EngineerPage_OnListBoxUpdated(object sender, Person e)
        {
            var vm = SimpleIoc.Default.GetInstance<EngineerViewModel>();
            vm.SetModel(e as Engineer);
        }

        private void MechanicPage_OnListBoxUpdated(object sender, Person e)
        {
            var vm = SimpleIoc.Default.GetInstance<MechanicViewModel>();
            vm.SetModel(e as Mechanic);
        }

        private void ChairmanPage_OnListBoxUpdated(object sender, Person e)
        {
            var vm = SimpleIoc.Default.GetInstance<ChairmanViewModel>();
            vm.SetModel(e as Chairman);
        }

        private void ChampionshipPage_OnListBoxUpdated(object sender, Championship e)
        {
            var vm = SimpleIoc.Default.GetInstance<ChampionshipViewModel>();
            vm.SetModel(e as Championship);
        }

        private void Hyperlink_RequestNavigate(object sender,
                                       RequestNavigateEventArgs e)
        {

            Process.Start(e.Uri.ToString());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void SwitchToTab(TabPage driver)
        {
            TabItem chosenTab = null;
            switch (driver)
            {
                case TabPage.TeamPrincipal:
                    break;
                case TabPage.Driver:
                    foreach (TabItem tabControlItem in tabControl.Items)
                    {
                        if (tabControlItem.Name.Equals("DriversTabItem"))
                        {
                            chosenTab = tabControlItem;
                            break;
                        }
                    }
                    break;
                case TabPage.Mechanic:
                    foreach (TabItem tabControlItem in tabControl.Items)
                    {
                        if (tabControlItem.Name.Equals("MechanicTabItem"))
                        {
                            chosenTab = tabControlItem;
                            break;
                        }
                    }
                    break;
                case TabPage.Engineer:
                    foreach (TabItem tabControlItem in tabControl.Items)
                    {
                        if (tabControlItem.Name.Equals("EngineerTabItem"))
                        {
                            chosenTab = tabControlItem;
                            break;
                        }
                    }
                    break;
                case TabPage.Team:
                    foreach (TabItem tabControlItem in tabControl.Items)
                    {
                        if (tabControlItem.Name.Equals("TeamTabItem"))
                        {
                            chosenTab = tabControlItem;
                            break;
                        }
                    }
                    break;
                case TabPage.Game:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(driver), driver, null);
            }
            if (chosenTab != null)
            {
                tabControl.SelectedItem = chosenTab;
            }
        }

        private static bool IsValidSaveVersion(VersionNumber version)
        {
            foreach (var v in SupportedVersions)
            {
                if (v.major == version.major && v.minor == version.minor)
                {
                    return true;
                }
            }
            return false;
        }

        private string gameCrashLog = null;
        private void report_Click(object sender, RoutedEventArgs e)
        {
            ReportGameCrashDialog dialog = new ReportGameCrashDialog();
            dialog.ShowDialog();
            if (dialog.LogPath != null)
            {
                gameCrashLog = dialog.LogPath;
                NBug.Settings.ProcessingException += Settings_ProcessingException;
                try
                {
                    throw new Exception("Game is crashing report");
                }
                catch (Exception ex)
                {
                    new BugReport().Report(ex, ExceptionThread.Main);
                }
            }
        }

        private void Settings_ProcessingException(Exception arg1, Report arg2)
        {
            NBug.Settings.AdditionalReportFiles.Add(gameCrashLog);
            NBug.Settings.ProcessingException -= Settings_ProcessingException;
        }

        private void support_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.racedepartment.com/threads/motorsport-manager-save-game-editor.138488/");

        }

        private void donate_Click(object sender, RoutedEventArgs e)
        {
            Donate donateMenu = new Donate();
            donateMenu.ShowDialog();
        }

        private void request_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://rwscripts.com/tracker/bug_report_page.php");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Image_TouchUp(object sender, System.Windows.Input.TouchEventArgs e)
        {
            Donate donateMenu = new Donate();
            donateMenu.ShowDialog();
        }

        private void Image_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Donate donateMenu = new Donate();
            donateMenu.ShowDialog();
        }

        private void window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MMSaveEditor.Properties.Settings.Default.Width = e.NewSize.Width;
            MMSaveEditor.Properties.Settings.Default.Height = e.NewSize.Height;
            MMSaveEditor.Properties.Settings.Default.Save();
        }

        private void window_StateChanged(object sender, EventArgs e)
        {
            MMSaveEditor.Properties.Settings.Default.IsMaximized = WindowState == WindowState.Maximized;
        }

    }
}
