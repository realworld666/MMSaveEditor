using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using FullSerializer;
using GalaSoft.MvvmLight.Ioc;
using LZ4;
using Microsoft.Win32;
using MMSaveEditor.ViewModel;
using NBug.Core.Reporting;
using NBug.Core.Reporting.Info;
using NBug.Core.Submission;
using NBug.Core.Util;
using NBug.Enums;

namespace MMSaveEditor.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private fsSerializer serializer;

        private string _openFilePath;
        private static readonly int saveFileVersion = 4;
        private SaveFileInfo _currentSaveInfo;

        public static MainWindow Instance;

        public string VersionString
        {
            get
            {
                return string.Format("Motorsport Manager Save Editor v{0}", Assembly.GetExecutingAssembly()
                    .GetName()
                    .Version);
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
        }

        private static fsSerializer CreateAndConfigureSerializer()
        {
            return new fsSerializer { Config = { DefaultMemberSerialization = fsMemberSerialization.OptOut, SerializeAttributes = new Type[1] { typeof(fsPropertyAttribute) }, IgnoreSerializeAttributes = new Type[2] { typeof(NonSerializedAttribute), typeof(fsIgnoreAttribute) }, SerializeEnumsAsInteger = true, EnablePropertySerialization = false } };
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Save games (*.sav)|*.*";
            openFileDialog.InitialDirectory = Path.Combine(App.Instance.LocalLowFolderPath, "Playsport Games\\Motorsport Manager\\Cloud\\Saves");

            if (openFileDialog.ShowDialog() == true)
            {
                _openFilePath = openFileDialog.FileName;

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
                MessageBoxResult result = MessageBox.Show(string.Format("There was a problem setting the player view {0}", ex.Message), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var teamVM = SimpleIoc.Default.GetInstance<TeamViewModel>();
                teamVM.SetModel(null);
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show(string.Format("There was a problem setting the player view {0}", ex.Message), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var gameVM = SimpleIoc.Default.GetInstance<GameViewModel>();
                gameVM.SetModels(Game.instance.time);
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show(string.Format("There was a problem setting the game view {0}", ex.Message), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var principleVM = SimpleIoc.Default.GetInstance<TeamPrincipalViewModel>();
                principleVM.SetList(Game.instance.teamPrincipalManager.GetEntityList());
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show(string.Format("There was a problem setting the team principal view {0}", ex.Message), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var driverVM = SimpleIoc.Default.GetInstance<DriverViewModel>();
                driverVM.SetList(Game.instance.driverManager.GetEntityList());
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show(string.Format("There was a problem setting the driver view {0}", ex.Message), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var engineerVM = SimpleIoc.Default.GetInstance<EngineerViewModel>();
                engineerVM.SetList(Game.instance.engineerManager.GetEntityList());
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show(string.Format("There was a problem setting the engineer view {0}", ex.Message), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var mechanicVM = SimpleIoc.Default.GetInstance<MechanicViewModel>();
                mechanicVM.SetList(Game.instance.mechanicManager.GetEntityList());
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show(string.Format("There was a problem setting the mechanic view {0}", ex.Message), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var chairmanVM = SimpleIoc.Default.GetInstance<ChairmanViewModel>();
                chairmanVM.SetList(Game.instance.chairmanManager.GetEntityList());
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show(string.Format("There was a problem setting the chairman view {0}", ex.Message), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
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
                _openFilePath = saveFileDialog.FileName;
            }
        }

        public static void SaveFile(string openFilePath, fsSerializer serializer, SaveFileInfo saveFileInfo)
        {
            try
            {
                fsData data1;
                fsResult fsResult1 = serializer.TrySerialize(saveFileInfo, out data1);
                if (fsResult1.Failed)
                    throw new Exception(string.Format("Failed to serialise SaveFileInfo: {0}", fsResult1.FormattedMessages));
                string s1 = fsJsonPrinter.CompressedJson(data1);
                fsData data2;
                fsResult fsResult2 = serializer.TrySerialize(Game.instance, out data2);
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

        public static bool LoadFile(string fileName, fsSerializer serializer, out SaveFileInfo saveFileInfo)
        {
            using (FileStream fileStream = File.Open(fileName, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    if (binaryReader.ReadInt32() != 1932684653)
                    {
                        MessageBoxResult result = MessageBox.Show("Save file is not a valid save file for this game", "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        saveFileInfo = null;
                        return false;
                    }
                    int num1 = binaryReader.ReadInt32();
                    if (num1 < saveFileVersion)
                    {
                        MessageBoxResult result = MessageBox.Show("Save file is an old format, and no upgrade path exists - must be from an old unsupported development version", "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        saveFileInfo = null;
                        return false;
                    }
                    if (num1 > saveFileVersion)
                    {
                        MessageBoxResult result = MessageBox.Show("Save file version is newer than the editor expected. If the game has been updated recently you may need to wait for an update to the editor. Check the forums for updates.", "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        saveFileInfo = null;
                        return false;
                    }
                    int headerCount = binaryReader.ReadInt32();
                    int headerOutputLength = binaryReader.ReadInt32();
                    int gameDataCount = binaryReader.ReadInt32();
                    int gameDataOutputLength = binaryReader.ReadInt32();
                    if (headerOutputLength > 268435456)
                    {
                        MessageBoxResult result = MessageBox.Show("Save file header size is apparently way too big - file has either been tampered with or become corrupt", "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        saveFileInfo = null;
                        return false;
                    }
                    if (gameDataOutputLength > 268435456)
                    {
                        MessageBoxResult result = MessageBox.Show("Save file game data size is apparently way too big - file has either been tampered with or become corrupt", "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        saveFileInfo = null;
                        return false;
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
                        saveFileInfo = null;
                        return false;
                    }

                    saveFileInfo = null;
                    fsResult fsHeaderResult2 = serializer.TryDeserialize(headerData, ref saveFileInfo);
                    if (fsHeaderResult2.Failed)
                    {
                        MessageBoxResult result = MessageBox.Show(string.Format("Error reported whilst deserializing SaveFileInfo: {0}", fsHeaderResult1.FormattedMessages), "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        saveFileInfo = null;
                        return false;
                    }
                    try
                    {
                        FileInfo fileInfo = new FileInfo(fileName);
                        saveFileInfo.fileInfo = fileInfo;
                    }
                    catch (Exception ex)
                    {
                        MessageBoxResult result = MessageBox.Show(string.Format("Could not create FileInfo for {0}. Check that the editor has permissions to access this file.", fileName), "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);

                        saveFileInfo = null;
                        return false;
                    }


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
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBoxResult result = MessageBox.Show(string.Format("Exception thrown whilst parsing serialized Game data string: {0}", fileName), "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }

                    fsResult fsResult2 = new fsResult();
                    try
                    {
                        fsResult2 = serializer.TryDeserialize(gameData, ref targetGame);
                        if (fsResult2.Failed)
                        {
                            MessageBoxResult result = MessageBox.Show(string.Format("Error reported whilst deserializing Game data: {0}", fsResult2.FormattedMessages), "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
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
                        return false;
                    }
                    //foreach (object rawMessage in fsResult2.RawMessages)
                    //  Console.Write(rawMessage);
                }
            }

            return true;
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
            //System.Diagnostics.Process.Start("http://paypal.me/realworld666/5");

        }
		
    }
}
